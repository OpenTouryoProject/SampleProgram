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
