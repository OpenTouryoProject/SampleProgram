//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License

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
//**********************************************************************************
// * Class name            : Startup.cs
// * Class name class name :
// *
// * Author                : Sandeep
// * Class Japanese name   :
// * Change log
// * Date:        Author  :       Comments:
// * ----------  ---------------  -------------------------------------------------
// * 2016/02/11  Sandeep          Added OWIN startup class
//**********************************************************************************

// System
using System;
using System.Threading.Tasks;

// Microsoft OWIN
using Microsoft.Owin;
using Owin;

// Assembly
[assembly: OwinStartup(typeof(OpenIDConnect_sample.Startup))]

namespace OpenIDConnect_sample
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configuration Methode
        /// </summary>
        /// <param name="app">Owin.IAppBuilder Interface</param>
        public void Configuration(IAppBuilder app)
        {
            // Calling ConfigureAuth Methode
            ConfigureAuth(app);
        }
    }
}
