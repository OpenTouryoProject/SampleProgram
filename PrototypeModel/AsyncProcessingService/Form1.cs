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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AsyncProcessingService
{
    /// <summary>This program is a prototype model of async processing service.</summary>
    public partial class Form1 : Form
    {
        #region Form1
        
        /// <summary>Form</summary>
        public Form1()
        {
            InitializeComponent();
            this.btnRegisterAsyncTask.Anchor = AnchorStyles.Top | AnchorStyles.Right; 
            this.btnDeleteLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtLogView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this._infiniteLoop = false;
            this.btnOnStart.Enabled = true;
            this.btnOnStop.Enabled = false;
            this.btnRegisterAsyncTask.Enabled = false;
            this.nudMaxWorkerThreadCount.Enabled = true;

            this.nudMaxWorkerThreadCount.Value = 5;
            this.nudAsyncTask2.Value = 5;
        }

        #endregion

        #region Member variables

        /// <summary>Main thread</summary>
        private Thread _mainThread;

        /// <summary>Infinite loop flag (thread-safe)</summary>
        private volatile bool _infiniteLoop = true;
        
        /// <summary>Asynchronous task (thread-safe)</summary>
        private volatile int _asynchronousTask = 0;

        /// <summary>nudAsyncTask_ValueChanged</summary>
        private void nudAsyncTask_ValueChanged(object sender, EventArgs e)
        {
            // Move the value because UI-Control can not touch from other than UI-Thread to thread-safe variable.
            this._asynchronousTask = (int)((NumericUpDown)sender).Value;
        }

        #endregion

        #region Control.Invoke

        /// <summary>ControlInvokeDelegate</summary>
        /// <param name="param">param</param>
        private delegate void ControlInvokeDelegate(object param);

        /// <summary>ClearAsyncTask</summary>
        /// <param name="param">param</param>
        private void ClearAsyncTask(object param)
        {
            // Clear async task
            this.nudAsyncTask.Value = 0;
        }

        /// <summary>OutPutMessageLog</summary>
        /// <param name="param">message</param>
        private void OutPutMessageLog(object message)
        {
            // output the message log.
            this.txtLogView.AppendText(
                DateTime.Now.ToString("HH:mm:ss.fff") + " : " +  (string)message);
            // scroll the text box.
            this.txtLogView.SelectionStart = this.txtLogView.Text.Length;
        }

        #endregion

        #region Button click

        /// <summary>Worker thread count (Thread-safe)</summary>
        volatile uint WorkerThreadCount = 0;

        /// <summary>btnOnStop_Click</summary>
        private void btnOnStop_Click(object sender, EventArgs e)
        {
            // Setting change of UI controls and flag.
            this._infiniteLoop = false;
            this.btnOnStart.Enabled = true;
            this.btnOnStop.Enabled = false;
            this.btnRegisterAsyncTask.Enabled = false;
            this.nudMaxWorkerThreadCount.Enabled = true;

            // Wait the end of the main thread.
            this._mainThread.Join();

            // output the message log.
            // This statement is necessary in order to clear the UIcontrol from other than UIThread.
            this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog), new object[] { "End main thread.\r\n" });

            //// Check the number of worker threads.
            //int workerThreads = 0;
            //int completionPortThreads = 0;

            //// Get available threads.
            //ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

            //while (workerThreads != (int)this.nudMaxWorkerThreadCount.Value)
            while (this.WorkerThreadCount != 0)
            {
                //// Get available threads.
                //ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

                // Sleep(xxx msec leave the time slice.)
                // wait for the completion of the worker thread.
                Thread.Sleep(1000); // This parameter should be changed that  can be defined in the config file for tuning.
            }

            // output the message log.
            // This statement is necessary in order to clear the UIcontrol from other than UIThread.
            this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog), new object[] { "End all of worker thread.\r\n" });
        }

        /// <summary>btnOnStart_Click</summary>
        private void btnOnStart_Click(object sender, EventArgs e)
        {
            // Setting change of UI controls and flag.
            this._infiniteLoop = true;
            this.btnOnStart.Enabled = false;
            this.btnOnStop.Enabled = true;
            this.btnRegisterAsyncTask.Enabled = true;
            this.nudMaxWorkerThreadCount.Enabled = false;

            // Initialization of ThreadPool.
            ThreadPool.SetMinThreads(1, 0);
            ThreadPool.SetMaxThreads((int)this.nudMaxWorkerThreadCount.Value, 0);
            
            // The execution of the main thread
            this._mainThread = new Thread(new ThreadStart(MainThreadMethod));
            this._mainThread.Start();
        }

        /// <summary>btnRegisterAsyncTask_Click</summary>
        private void btnRegisterAsyncTask_Click(object sender, EventArgs e)
        {
            this.nudAsyncTask.Value = this.nudAsyncTask2.Value;
        }

        /// <summary>btnDeleteLog_Click</summary>
        private void btnDeleteLog_Click(object sender, EventArgs e)
        {
            this.txtLogView.Text = "";
        }

        #endregion

        #region Thread method
        
        /// <summary>main thread method</summary>
        private void MainThreadMethod()
        {
            // output the message log.
            // This statement is necessary in order to clear the UIcontrol from other than UIThread.
            this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog), new object[] { "Start main thread.\r\n" });

            while (this._infiniteLoop)
            {
                // Get asynchronous task.
                int asynchronousTask = this._asynchronousTask;
                this._asynchronousTask = 0;

                if (asynchronousTask == 0)
                {
                    // Asynchronous task does not exist.

                    // Sleep(xxx msec leave the time slice.)
                    // wait for the asynchronous task is registered.
                    Thread.Sleep(1000); // This parameter should be changed that  can be defined in the config file for tuning.

                    // Continue to this infinite loop.
                    continue;
                }
                else
                {
                    // Asynchronous task exists.

                    // Clear asynchronous task.
                    // This statement is necessary in order to clear the UIcontrol from other than UIThread.
                    this.BeginInvoke(new ControlInvokeDelegate(this.ClearAsyncTask), new object[] { null });

                    // Check the number of worker threads.
                    int workerThreads = 0;
                    int completionPortThreads = 0;

                    // Get available threads.
                    ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
                    
                    while (workerThreads == 0)
                    {
                        // Get available threads.
                        ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

                        // Sleep(xxx msec leave the time slice.)
                        // wait for the completion of the worker thread.
                        Thread.Sleep(1000); // This parameter should be changed that  can be defined in the config file for tuning.
                    }

                    string guid = Guid.NewGuid().ToString();

                    // output the message log.
                    // This statement is necessary in order to clear the UIcontrol from other than UIThread.
                    this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog),
                        new object[] { string.Format(
                                "workerThreads={0}, completionPortThreads={1}\r\n"
                                + "                     " + guid + "\r\n"
                                + "                     " +  "Just before the assignment of asynchronous task.\r\n",
                                workerThreads, completionPortThreads) });

                    // Assign the task to the worker thread
                    ThreadPool.QueueUserWorkItem(this.WorkerThreadMethod, new object[] { asynchronousTask, guid });
                }
            }
        }

        /// <summary>worker thread method</summary>
        /// <param name="threadContext">parameter</param>
        private void WorkerThreadMethod(Object threadContext)
        {
            this.WorkerThreadCount++;

            try
            {
                object[] arryobj = (object[])threadContext;

                // output the message log.
                // This statement is necessary in order to clear the UIcontrol from other than UIThread.
                this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog),
                    new object[] { string.Format("Start={0}\r\n", arryobj[1]) });

                // Sleep(xxx sec leave the time slice.)
                // wait for end of execution of the asynchronous task simulation.
                Thread.Sleep(((int)arryobj[0]) * 1000); //

                // output the message log.
                // This statement is necessary in order to clear the UIcontrol from other than UIThread.
                this.BeginInvoke(new ControlInvokeDelegate(this.OutPutMessageLog),
                    new object[] { string.Format("End={0}\r\n", arryobj[1]) });
            }
            finally
            {
                this.WorkerThreadCount--;
            }
        }

        #endregion
    }
}
