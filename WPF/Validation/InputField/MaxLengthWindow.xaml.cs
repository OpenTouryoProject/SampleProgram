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

namespace WpfApplication1
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
