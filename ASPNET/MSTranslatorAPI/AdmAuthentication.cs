//**********************************************************************************
//* Copyright (C) 2007,2015 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

// ************************************************ **********************************
// * Class name: AdminAuthentication
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created AdminAuthentication class to defines properties.
//*  2015/05/21  Supragyan       Created AdmAuthentication constructor  to initialize clientId and clientToken properties.
//*  2015/05/21  Supragyan       Created RenewAccessToken method to renew acess token after 10 mins.
//**********************************************************************************
//system
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;

namespace MicrosoftTranslator
{
	/// <summary>
	/// http://msdn.microsoft.com/en-us/library/ff512437.aspx
	/// </summary>
	public class AdmAuthentication
	{
		public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
		private string clientId;
		private string clientSecret;
		private string request;
		private AdmAccessToken token;
		private Timer accessTokenRenewer;

		//Access token expires every 10 minutes. Renew it every 9 minutes only.
		private const int RefreshTokenDuration = 9;

        /// <summary>
        /// Creates token by authenticate clientId and clientSecret.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
		public AdmAuthentication(string clientId, string clientSecret)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
			//If clientid or client secret has special characters, encode before sending request
			this.request = string.Format(
				"grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", 
				HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
			this.token = HttpPost(DatamarketAccessUri, this.request);
			//renew the token every specfied minutes
			accessTokenRenewer = new Timer(OnTokenExpiredCallback, this, TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
		}

        /// <summary>
        /// Gets AcessToken
        /// </summary>
        /// <returns></returns>
		public AdmAccessToken GetAccessToken()
		{
			return this.token;
		}

        /// <summary>
        /// Renews Acess Token after every 9 minutes
        /// </summary>
		private void RenewAccessToken()
		{
			//swap the new token with old one
			//Note: the swap is thread unsafe
			this.token = HttpPost(DatamarketAccessUri, this.request); 
			Trace.WriteLine(string.Format("Renewed token for user: {0} is: {1}", this.clientId, this.token.access_token));
		}

        /// <summary>
        /// Checks if token expired after 9 minutes renew Acesstoken. 
        /// </summary>
        /// <param name="stateInfo"></param>
		private void OnTokenExpiredCallback(object stateInfo)
		{
			try
			{
                // renew aceess token
				RenewAccessToken();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
			}
			finally
			{
				try
				{
					accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
				}
				catch (Exception ex)
				{
					Trace.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
				}
			}
		}

        /// <summary>
        /// Posts encoded clientId and clentSecret data to DatamarketAccessUri and returns deserialized acess token
        /// </summary>
        /// <param name="DatamarketAccessUri"></param>
        /// <param name="requestDetails"></param>
        /// <returns></returns>
		private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
		{
			//Prepare OAuth request 
			var webRequest = WebRequest.Create(DatamarketAccessUri);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
			webRequest.ContentLength = bytes.Length;

			using (var outputStream = webRequest.GetRequestStream())
			{
				outputStream.Write(bytes, 0, bytes.Length);
			}
			using (var webResponse = webRequest.GetResponse())
			{
				var serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
				//Get deserialized object from JSON stream
				return (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
			}
		}
	}
}
