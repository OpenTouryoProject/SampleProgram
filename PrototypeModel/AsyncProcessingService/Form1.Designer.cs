namespace AsyncProcessingService
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.nudMaxWorkerThreadCount = new System.Windows.Forms.NumericUpDown();
            this.btnOnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAsyncTask = new System.Windows.Forms.NumericUpDown();
            this.txtLogView = new System.Windows.Forms.TextBox();
            this.btnOnStop = new System.Windows.Forms.Button();
            this.btnDeleteLog = new System.Windows.Forms.Button();
            this.btnRegisterAsyncTask = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudAsyncTask2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWorkerThreadCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsyncTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsyncTask2)).BeginInit();
            this.SuspendLayout();
            // 
            // nudMaxWorkerThreadCount
            // 
            this.nudMaxWorkerThreadCount.Location = new System.Drawing.Point(198, 9);
            this.nudMaxWorkerThreadCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMaxWorkerThreadCount.Name = "nudMaxWorkerThreadCount";
            this.nudMaxWorkerThreadCount.Size = new System.Drawing.Size(82, 22);
            this.nudMaxWorkerThreadCount.TabIndex = 0;
            // 
            // btnOnStart
            // 
            this.btnOnStart.Location = new System.Drawing.Point(289, 9);
            this.btnOnStart.Name = "btnOnStart";
            this.btnOnStart.Size = new System.Drawing.Size(87, 23);
            this.btnOnStart.TabIndex = 1;
            this.btnOnStart.Text = "OnStart";
            this.btnOnStart.UseVisualStyleBackColor = true;
            this.btnOnStart.Click += new System.EventHandler(this.btnOnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ｍax worker thread count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Asynchronous task";
            // 
            // nudAsyncTask
            // 
            this.nudAsyncTask.Location = new System.Drawing.Point(198, 45);
            this.nudAsyncTask.Name = "nudAsyncTask";
            this.nudAsyncTask.ReadOnly = true;
            this.nudAsyncTask.Size = new System.Drawing.Size(82, 22);
            this.nudAsyncTask.TabIndex = 5;
            this.nudAsyncTask.ValueChanged += new System.EventHandler(this.nudAsyncTask_ValueChanged);
            // 
            // txtLogView
            // 
            this.txtLogView.Location = new System.Drawing.Point(12, 103);
            this.txtLogView.Multiline = true;
            this.txtLogView.Name = "txtLogView";
            this.txtLogView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogView.Size = new System.Drawing.Size(738, 412);
            this.txtLogView.TabIndex = 6;
            // 
            // btnOnStop
            // 
            this.btnOnStop.Location = new System.Drawing.Point(382, 9);
            this.btnOnStop.Name = "btnOnStop";
            this.btnOnStop.Size = new System.Drawing.Size(87, 23);
            this.btnOnStop.TabIndex = 7;
            this.btnOnStop.Text = "OnStop";
            this.btnOnStop.UseVisualStyleBackColor = true;
            this.btnOnStop.Click += new System.EventHandler(this.btnOnStop_Click);
            // 
            // btnDeleteLog
            // 
            this.btnDeleteLog.Location = new System.Drawing.Point(12, 521);
            this.btnDeleteLog.Name = "btnDeleteLog";
            this.btnDeleteLog.Size = new System.Drawing.Size(738, 23);
            this.btnDeleteLog.TabIndex = 8;
            this.btnDeleteLog.Text = "DeleteLog";
            this.btnDeleteLog.UseVisualStyleBackColor = true;
            this.btnDeleteLog.Click += new System.EventHandler(this.btnDeleteLog_Click);
            // 
            // btnRegisterAsyncTask
            // 
            this.btnRegisterAsyncTask.Location = new System.Drawing.Point(371, 73);
            this.btnRegisterAsyncTask.Name = "btnRegisterAsyncTask";
            this.btnRegisterAsyncTask.Size = new System.Drawing.Size(379, 23);
            this.btnRegisterAsyncTask.TabIndex = 9;
            this.btnRegisterAsyncTask.Text = "Register the async task.";
            this.btnRegisterAsyncTask.UseVisualStyleBackColor = true;
            this.btnRegisterAsyncTask.Click += new System.EventHandler(this.btnRegisterAsyncTask_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "<<-------";
            // 
            // nudAsyncTask2
            // 
            this.nudAsyncTask2.Location = new System.Drawing.Point(371, 45);
            this.nudAsyncTask2.Name = "nudAsyncTask2";
            this.nudAsyncTask2.Size = new System.Drawing.Size(82, 22);
            this.nudAsyncTask2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(459, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "sec will be spent by the async task.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 556);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudAsyncTask2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRegisterAsyncTask);
            this.Controls.Add(this.btnDeleteLog);
            this.Controls.Add(this.btnOnStop);
            this.Controls.Add(this.txtLogView);
            this.Controls.Add(this.nudAsyncTask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOnStart);
            this.Controls.Add(this.nudMaxWorkerThreadCount);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWorkerThreadCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsyncTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsyncTask2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMaxWorkerThreadCount;
        private System.Windows.Forms.Button btnOnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudAsyncTask;
        private System.Windows.Forms.TextBox txtLogView;
        private System.Windows.Forms.Button btnOnStop;
        private System.Windows.Forms.Button btnDeleteLog;
        private System.Windows.Forms.Button btnRegisterAsyncTask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudAsyncTask2;
        private System.Windows.Forms.Label label4;
    }
}

