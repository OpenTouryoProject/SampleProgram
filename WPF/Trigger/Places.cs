//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

namespace Trigger
{
    public class Places : ObservableCollection<Place>
    {
        /// <summary>コンストラクタで</summary>
        public Places()
        {
            this.Add(new Place("Bellevue", "WA"));
            this.Add(new Place("Gold Beach", "OR"));
            this.Add(new Place("Kirkland", "WA"));
            this.Add(new Place("Los Angeles", "CA"));
            this.Add(new Place("Portland", "ME"));
            this.Add(new Place("Portland", "OR"));
            this.Add(new Place("Redmond", "WA"));
            this.Add(new Place("San Diego", "CA"));
            this.Add(new Place("San Francisco", "CA"));
            this.Add(new Place("San Jose", "CA"));
            this.Add(new Place("Seattle", "WA"));
        }
    }
}
