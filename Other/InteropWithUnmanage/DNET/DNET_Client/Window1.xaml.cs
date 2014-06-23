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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Reflection;
using System.Runtime.InteropServices;

namespace DNET_Client
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>コンストラクタ</summary>
        public Window1()
        {
            InitializeComponent();
        }

        /// <summary>C#でCreateObject</summary>
        /// <param name="progId">COMのプログラムID</param>
        /// <returns>COM Object</returns>
        private static object CreateObject(string progId)
        {
            Type t = Type.GetTypeFromProgID(progId);
            return Activator.CreateInstance(t);
        }

        /// <summary>VC_COM呼び出し</summary>
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            string ret = "";

            // レイトバインド用
            object[] args;
            ParameterModifier p;
            ParameterModifier[] mods = null;

            try
            {
                //object o = CreateObject("VC_COM.ClassTest");// レイトバインド
                VC_COMLib.ClassTest c = new VC_COMLib.ClassTest();// アーリーバインド

                //正常終了

                //// レイトバインド＠リフレクションで参照渡しを行う。
                //// ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[2] { "だいすけ", "にしの" };
                //p = new ParameterModifier(2);

                //p[0] = false;
                //p[1] = false;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (string)o.GetType().InvokeMember(
                //        "MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                ret = c.MethodTest("だいすけ", "にしの");// アーリーバインド

                //MessageBox
                MessageBox.Show(ret, "結果", MessageBoxButton.OK);

                //異常終了（catchコード ブロックへ）

                //// レイトバインド＠リフレクションで参照渡しを行う。
                //// ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[2] { "だいすけ", "" };
                //p = new ParameterModifier(2);

                //p[0] = false;
                //p[1] = false;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (string)o.GetType().InvokeMember(
                //        "MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                ret = c.MethodTest("だいすけ", "");// アーリーバインド

                //MessageBox
                MessageBox.Show(ret, "結果", MessageBoxButton.OK);
            }
            catch (COMException ex)
            {
                //MessageBox
                MessageBox.Show(
                    "Err.Number:" + ex.ErrorCode + "\r\n" +
                    "Err.Description:" + ex.Message,
                    "例外", MessageBoxButton.OK);
            }
        }

        /// <summary>VB_COM呼び出し</summary>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            short ret = 0;

            string str1 = "";
            short i1 = 0;
            string str2 = "";
            short i2 = 0;

            str1 = "test1,";
            i1 = 1;
            str2 = "test2,";
            i2 = 2;

            // レイトバインド用
            object[] args;
            ParameterModifier p;
            ParameterModifier[] mods = null;

            try
            {
                //object o = CreateObject("VB_COM.ClassTest");// レイトバインド
                VB_COM.ClassTest c = new VB_COM.ClassTest();// アーリーバインド

                //正常終了

                //// レイトバインド＠リフレクションで参照渡しを行う。
                //// ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[4] { str1, i1, str2, i2 };
                //p = new ParameterModifier(4);

                //p[0] = false;
                //p[1] = false;
                //p[2] = true;
                //p[3] = true;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (short)o.GetType().InvokeMember("MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                //str1 = (string)args[0];
                //i1 = (short)args[1];
                //str2 = (string)args[2];
                //i2 = (short)args[3];

                ret = c.MethodTest(str1, i1, ref str2, ref i2);// アーリーバインド

                //MessageBox
                MessageBox.Show("ret:" + ret + "\r\n" +
                    "str1:" + str1 + "\r\n" + "i1:" + i1.ToString() + "\r\n" +
                    "str2:" + str2 + "\r\n" + "i2:" + i2.ToString(), "結果", MessageBoxButton.OK);

                //異常終了（catchコード ブロックへ）

                //// レイトバインド＠リフレクションで参照渡しを行う。
                //// ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[4] { "", i1, str2, i2 };
                //p = new ParameterModifier(4);

                //p[0] = false;
                //p[1] = false;
                //p[2] = true;
                //p[3] = true;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (short)o.GetType().InvokeMember("MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                //str1 = (string)args[0];
                //i1 = (short)args[1];
                //str2 = (string)args[2];
                //i2 = (short)args[3];

                ret = c.MethodTest("", i1, ref str2, ref i2);

                //MessageBox
                MessageBox.Show("ret:" + ret + "\r\n" +
                    "str1:" + str1 + "\r\n" + "i1:" + i1.ToString() + "\r\n" +
                    "str2:" + str2 + "\r\n" + "i2:" + i2.ToString(), "結果", MessageBoxButton.OK);
            }
            catch (COMException ex)
            {
                //MessageBox
                MessageBox.Show(
                    "Err.Number:" + ex.ErrorCode + "\r\n" +
                    "Err.Description:" + ex.Message,
                    "例外", MessageBoxButton.OK);
            }
        }

        /// <summary>DNET_COM呼び出し</summary>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            int ret = 0;

            string str1 = "";
            int i1 = 0;
            string str2 = "";
            int i2 = 0;

            str1 = "test1,";
            i1 = 1;
            str2 = "test2,";
            i2 = 2;

            // レイトバインド用
            object[] args;
            ParameterModifier p;
            ParameterModifier[] mods = null;

            try
            {
                //object o = CreateObject("DNET_COM.ClassTest");// レイトバインド
                DNET_COM.ClassTest c = new DNET_COM.ClassTest();// アーリーバインド

                //正常終了

                //// レイトバインド＠リフレクションで参照渡しを行う。
                //// ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[4] { str1, i1, str2, i2 };
                //p = new ParameterModifier(4);

                //p[0] = false;
                //p[1] = false;
                //p[2] = true;
                //p[3] = true;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (int)o.GetType().InvokeMember("MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                //str1 = (string)args[0];
                //i1 = (int)args[1];
                //str2 = (string)args[2];
                //i2 = (int)args[3];

                ret = c.MethodTest(str1, i1, ref str2, ref i2);

                //MessageBox
                MessageBox.Show("ret:" + ret + "\r\n" +
                    "str1:" + str1 + "\r\n" + "i1:" + i1.ToString() + "\r\n" +
                    "str2:" + str2 + "\r\n" + "i2:" + i2.ToString(), "結果", MessageBoxButton.OK);

                //異常終了（catchコード ブロックへ）

                // レイトバインド＠リフレクションで参照渡しを行う。
                // ParameterModifierを使用してCOMパラメータに修飾子を指定
                //args = new object[4] { "", i1, str2, i2 };
                //p = new ParameterModifier(4);

                //p[0] = false;
                //p[1] = false;
                //p[2] = true;
                //p[3] = true;

                //mods = new ParameterModifier[] { p };

                //try
                //{
                //    ret = (int)o.GetType().InvokeMember("MethodTest", BindingFlags.InvokeMethod, null,
                //        o, args, mods, null, null);// レイトバインド
                //}
                //catch (Exception ex)
                //{
                //    throw ex.InnerException;
                //}

                //str1 = (string)args[0];
                //i1 = (int)args[1];
                //str2 = (string)args[2];
                //i2 = (int)args[3];

                ret = c.MethodTest("", i1, ref str2, ref i2);

                //MessageBox
                MessageBox.Show("ret:" + ret + "\r\n" +
                    "str1:" + str1 + "\r\n" + "i1:" + i1.ToString() + "\r\n" +
                    "str2:" + str2 + "\r\n" + "i2:" + i2.ToString(), "結果", MessageBoxButton.OK);
            }
            catch (Exception ex) // COMExceptionにはならない。
            {
                //MessageBox
                MessageBox.Show(
                    "Err.Number:" + "・・・" + "\r\n" +
                    "Err.Description:" + ex.Message,
                    "例外", MessageBoxButton.OK);
            }
        }

        /// <summary>VC_DLL.TestArrayMethod</summary>
        [DllImport("VC_DLL.dll")]
        private extern static int TestArrayMethod(A a, ref A pa, int len, [In, Out] A[] aa);
        // 第4引数(構造体配列)を、結果受け取り用で宣言する場合は 
        // "ref" では不可、"[Out]" 属性を設定する必要がある

        /// <summary>DLL呼び出し</summary>
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            A a1 = new A();
            A a2 = new A();
            A[] a3 = new A[3];

            a1.m1 = 8;
            a1.m2_in = "{入力値側}";

            try
            {
                // 正常系: 第3引数(配列サイズ)を正しく指定
                int ret1 = TestArrayMethod(a1, ref a2, a3.Length, a3);

                StringBuilder sb = new StringBuilder();
                sb.Append("a2.m1=").Append(a2.m1).Append(", a2.m3_out=[").Append(a2.m3_out).Append("]\r\n");

                for (int i = 0; i < a3.Length; i++)
                {
                    sb.Append("a3[").Append(i).Append("].m1=").Append(a3[i].m1).Append(", a3[").Append(i).Append("].m3_out=[").Append(a3[i].m3_out).Append("]\r\n");
                }

                MessageBox.Show(sb.ToString(), "結果", MessageBoxButton.OK);


                // 異常系: 第3引数(配列サイズ)に、サイズ(-1)の不正値を指定
                int ret2 = TestArrayMethod(a1, ref a2, (-1), a3);

                sb = new StringBuilder();
                sb.Append("a2.m1=").Append(a2.m1).Append(", a2.m3_out=[").Append(a2.m3_out).Append("]\r\n");

                for (int i = 0; i < a3.Length; i++)
                {
                    sb.Append("a3[").Append(i).Append("].m1=").Append(a3[i].m1).Append(", a3[").Append(i).Append("].m3_out=[").Append(a3[i].m3_out).Append("]\r\n");
                }

                MessageBox.Show(sb.ToString(), "結果", MessageBoxButton.OK);
            }
            catch (SEHException ex)
            {
                //MessageBox
                MessageBox.Show(
                    "Err.Number:" + ex.ErrorCode + "\r\n" +
                    "Err.Description:" + ex.Message,
                    "例外", MessageBoxButton.OK);
            }
        }

        /// <summary>VC_DLL.Test_MYLIBAPI</summary>
        [DllImport("VC_DLL.dll", CharSet = CharSet.Unicode)]
        private extern static void Test_MYLIBAPI(string text, string caption);

        /// <summary>DLL呼び出し</summary>
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Test_MYLIBAPI("text", "caption");
        }
    }
}
