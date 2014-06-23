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

namespace InterProcComm
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new NdPipe();
            w.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window w = new UDP();
            w.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window w = new TCP();
            w.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window w = new SharedMem();
            w.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window w = new SharedMem2();
            w.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window w = new SharedMemCStrct();
            w.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Window w = new SharedMemCStrct2();
            w.Show();
        }
    }
}
