using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigger
{
    /// <summary>Place</summary>
    public class Place
    {
        /// <summary>名前</summary>
        private string _name;

        /// <summary>状態</summary>
        private string _state;

        /// <summary>名前</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>状態</summary>
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>コンストラクタ</summary>
        public Place(string name, string state)
        {
            this._name = name;
            this._state = state;
        }
    }
}
