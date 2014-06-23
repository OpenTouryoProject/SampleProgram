using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNET_COM
{
    /// <summary>
    /// .NETクラスライブラリのCOM化
    /// ・「アセンブリをCOM参照可能にする」チェックボックスをON
    /// ・「COM 相互運用機能の登録」チェックボックスをON
    /// 前者は必須、後者はビルド終了時に RegAsm を実行する
    /// →ビルドされた.dllをCOMとしてレジストリに登録。
    /// 　当該マシンでのみ有効（別のマシンでは手動登録）
    /// </summary>
    /// <see cref="http://www.sev.or.jp/ijupiter/world/dc_interrop/dotnet_com_interrop.html"/>
    /// <see cref="http://bbs.wankuma.com/index.cgi?mode=al2&namber=8298&KLOG=20"/>
    public class ClassTest
    {
        /// <summary>COM化テスト</summary>
        /// <param name="str1">文字列（in）</param>
        /// <param name="i1">数値（in）</param>
        /// <param name="str2">文字列（in、out）</param>
        /// <param name="i2">数値（in、out）</param>
        /// <returns>数値（retval）</returns>
        /// <see cref="http://msdn.microsoft.com/ja-jp/library/8tesw2eh.aspx"/>
        public int MethodTest(string str1, int i1, ref string str2, ref int i2)
        {
            // string str1
            if (str1 == "")
            {
                throw new Exception("ClassTest.MethodTest - str1が空です");
            }
            else
            {
                str1 = str1 + str1; // 変更は返らない
            }

            //int i1
            if (i1 == 0)
            {
                throw new Exception("ClassTest.MethodTest - i1が0です");
            }
            else
            {
                i1 = i1 + i1; // ByValなので変更は返らない
            }

            // ref string str2
            if (str2 == "")
            {
                throw new Exception("ClassTest.MethodTest - str2が空です");
            }
            else
            {
                str2 = str2 + str2; // refなので変更は返る
            }

            // ref int i2
            if (i2 == 0)
            {
                throw new Exception("ClassTest.MethodTest - i2が0です");
            }
            else
            {
                i2 = i2 + i2; // refなので変更は返る
            }

            Random rnd = new Random();

            // 0 以上 100 未満の乱数を取得し戻す。
            return rnd.Next(100);
        }
    }
}
