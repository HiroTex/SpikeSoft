using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.GUI
{
    public static class BWorkerMan
    {
        public static Dictionary<int, BackgroundWorker> BACKTHREADS = new Dictionary<int, BackgroundWorker>();
        public static List<Form> ProgressWindows = new List<Form>();
        public static int ID;

        public static void CleanThreads()
        {
            foreach (var thread in BACKTHREADS)
            {
                if (!thread.Value.IsBusy)
                {
                    BACKTHREADS.Remove(thread.Key);
                }
            }
        }

        public static void InitializeNewThread(DoWorkEventHandler work, object[] args, string Title)
        {
            ID += 1;

            ProgressBar bar = new ProgressBar();
            bar.Name = "bar";
            bar.Dock = DockStyle.Fill;
            bar.Maximum = 100;
            bar.Location = new System.Drawing.Point(13, 50);
            bar.Size = new System.Drawing.Size(352, 24);
            bar.TabIndex = 1;

            Form pbar = new Form();
            pbar.Tag = ID;
            pbar.Text = Title;
            pbar.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            pbar.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            pbar.AutoSize = true;
            pbar.BackColor = System.Drawing.SystemColors.Control;
            pbar.ClientSize = new System.Drawing.Size(377, 95);
            pbar.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            pbar.MaximizeBox = false;
            pbar.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            pbar.Controls.Add(bar);

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += ReportProgress;
            bw.RunWorkerCompleted += ProcessCompleted;
            bw.DoWork += work;

            KeyValuePair<int, BackgroundWorker> newbW = new KeyValuePair<int, BackgroundWorker>(ID, bw);

            if (args == null)
            {
                args = new object[1];
                args[0] = newbW;
            }
            else
            {
                object[] args2 = new object[args.Length + 1];
                for (int i = 0; i < args.Length; i++) { args2[i] = args[i]; }
                args2[args2.Length - 1] = newbW;
                args = args2;
            }

            BACKTHREADS.Add(newbW.Key, newbW.Value);
            pbar.Show();
            bw.RunWorkerAsync(args);
        }

        public static void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            int key = (int)e.UserState;
            (ProgressWindows.FirstOrDefault(x => ((int)x.Tag == key))
                .Controls.Find("bar", false).FirstOrDefault() as ProgressBar)
                .Value = e.ProgressPercentage;
        }

        public static void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CleanThreads();
        }
    }
}
