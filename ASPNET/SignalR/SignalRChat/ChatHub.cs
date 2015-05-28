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
// * Class name: ChatHub
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created Send method and implemented code to send response. 
//**********************************************************************************
// system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// SignalR
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    /// <summary>
    /// ChatHub class
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Sends response to all connected clients.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }

}