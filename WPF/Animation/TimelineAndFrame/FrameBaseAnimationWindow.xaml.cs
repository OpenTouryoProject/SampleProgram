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

        /// <summary>コンストラクタ</summary>
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

        /// <summary>button1_Click</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SpeedX = 15;
            SpeedY = 1;

            this.ellipse.SetValue(Canvas.LeftProperty, 30.0);
            this.ellipse.SetValue(Canvas.TopProperty, 30.0);
        }
    }
}
