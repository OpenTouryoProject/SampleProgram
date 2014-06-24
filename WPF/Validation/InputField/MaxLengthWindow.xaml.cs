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
using System.Windows.Shapes;

namespace InputField
{
    /// <summary>
    /// IMEWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class IMEWindow : Window
    {
        public IMEWindow()
        {
            InitializeComponent();
        }

        private void GotFocus_IME_OFF(object sender, RoutedEventArgs e)
        {
            InputMethod.Current.ImeState = InputMethodState.Off;
        }

        private void GotFocus_IME_ON(object sender, RoutedEventArgs e)
        {
            InputMethod.Current.ImeState = InputMethodState.On;
        }

        private void GotFocus_IME_Katakana(object sender, RoutedEventArgs e)
        {
            InputMethod.Current.ImeState = InputMethodState.On;
            InputMethod.Current.ImeConversionMode = ImeConversionModeValues.Native
            | ImeConversionModeValues.FullShape | ImeConversionModeValues.Katakana;
        }

        private void GotFocus_IME_Hiragana(object sender, RoutedEventArgs e)
        {
            InputMethod.Current.ImeState = InputMethodState.On;
            InputMethod.Current.ImeConversionMode = ImeConversionModeValues.Native
            | ImeConversionModeValues.FullShape | ImeConversionModeValues.Native;
        }

        //private void LostFocus_IME(object sender, RoutedEventArgs e)
        //{
        //    InputMethod.Current.ImeState = ims;
        //    InputMethod.Current.ImeConversionMode = imc;
        //}
    }
}
