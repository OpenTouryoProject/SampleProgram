using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterProcComm
{
    public class CmnClass
    {
        /// <summary>SetResultのDelegate</summary>
        public delegate void SetResultDelegate(string result);

        /// <summary>文字列を反転処理を関数化</summary>
        public static string strrev(string str)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        /// <summary>バッファを初期化する</summary>
        /// <param name="size">バッファのサイズ</param>
        /// <returns>初期化したバッファ</returns>
        public static byte[] InitBuff(int size)
        {
            byte[] temp = new byte[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = 0;
            }

            return temp;
        }
    }
}
