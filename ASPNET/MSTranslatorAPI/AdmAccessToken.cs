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
// * Class name: AdmAccessToken
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created AdmAccessToken class to defines properties.
//**********************************************************************************

//system
using System.Runtime.Serialization;

namespace MicrosoftTranslator
{
    /// <summary>
    /// AdmAccessToken class
    /// </summary>
    [DataContract]
    public class AdmAccessToken
    {
        //access_token property
        [DataMember]
        public string access_token { get; set; }

        //token_type property
        [DataMember]
        public string token_type { get; set; }

        //expires_in property
        [DataMember]
        public string expires_in { get; set; }

        //scope property
        [DataMember]
        public string scope { get; set; }
    }
}