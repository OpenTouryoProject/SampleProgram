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

namespace TimelineAndFrame
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TimelineAnimationWindow1();
            w.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TimelineAnimationWindow2();
            w.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window w = new LayoutTransformOnlyWindow();
            w.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RenderTransformOnlyWindow();
            w.Show();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RenderTransformAnimationWindow();
            w.Show();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TransformGroupAnimationWindow();
            w.Show();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Window w = new PathAnimationWindow();
            w.Show();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Window w = new KeyFrameAnimationWindow();
            w.Show();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Window w = new FrameBaseAnimationWindow();
            w.Show();
        }
    }
}
