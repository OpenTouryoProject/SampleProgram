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

namespace TimelineAndFrame
{
    /// <summary>
    /// FrameBaseAnimationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FrameBaseAnimationWindow : Window
    {
        // 速度ｘ
        double SpeedX = 15;
        // 減速ｘ
        double DecelerationX = 0.99;

        // 速度ｙ
        double SpeedY = 1;
        // 加速ｙ
        double AccelerationY = 0.3;

        public FrameBaseAnimationWindow()
        {
            InitializeComponent();

            // フレーム周期で呼び出されるイベント ハンドラ
            CompositionTarget.Rendering +=
              new EventHandler(CompositionTarget_Rendering);
        }

        /// <summary>フレーム周期で呼び出されるイベント ハンドラ</summary>
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // 加減速
            SpeedX *= DecelerationX;
            SpeedY += AccelerationY;

            // 添付プロパティの更新
            double x = (double)this.ellipse.GetValue(Canvas.LeftProperty);
            double y = (double)this.ellipse.GetValue(Canvas.TopProperty);

            this.ellipse.SetValue(Canvas.LeftProperty, x + SpeedX);
            this.ellipse.SetValue(Canvas.TopProperty, y + SpeedY);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SpeedX = 15;
            SpeedY = 1;

            this.ellipse.SetValue(Canvas.LeftProperty, 30.0);
            this.ellipse.SetValue(Canvas.TopProperty, 30.0);
        }
    }
}
