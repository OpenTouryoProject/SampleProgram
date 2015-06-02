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
// * Class name: Startup
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created Configuration method and implemented code to configure application. 
//**********************************************************************************

//system
using System;

//Owin
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]

namespace SignalRChat
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures to the SignalR 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();            
        }
    }
}
