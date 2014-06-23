using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using System.Data;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    /// <summary>汎用コンボボックス コレクション データクラス</summary>
    public class GpCbxCollectionData : Collection<GpCbxItemData>
    {
        public GpCbxCollectionData(string key)
            : base()
        {
            DataTable dt = (DataTable)Application.Current.Properties[key];

            if (dt != null) // デザイナでエラーにならないよう対策
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.Add(new GpCbxItemData((int)dr["value"], (string)dr["display"]));
                }
            }
        }
    }

    /// <summary>汎用コンボボックス アイテム データクラス</summary>
    public class GpCbxItemData
    {
        private int _value;
        private string _display;

        public GpCbxItemData(int value, string display)
        {
            this._value = value;
            this._display = display;
        }

        public int Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
        public string Display
        {
            get { return this._display; }
            set { this._display = value; }
        }
    }
}
