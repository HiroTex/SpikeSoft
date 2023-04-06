using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.GUI
{
    public partial class BWorkWindow : Form
    {
        bool ShowCompleteDialog = true;

        public BWorkWindow()
        {
            InitializeComponent();
        }

        public void SetLabel(string text)
        {
            ProgressLabel.Text = text;
        }

        public void SetProgressValue(int value)
        {
            if (value > 100)
            {
                pBar.Maximum = value;
            }

            pBar.Value = value;
        }

        public void InitializeNewThread(DoWorkEventHandler work, object[] args, string Title, bool ShowCompleteDialog)
        {
            pBar.Maximum = 100;
            SetLabel(Title);
            this.ShowCompleteDialog = ShowCompleteDialog;

            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += ReportProgress;
            bw.RunWorkerCompleted += ProcessCompleted;
            bw.DoWork += work;

            Thread.Sleep(500);

            bw.RunWorkerAsync(args);
        }

        public void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            SetProgressValue(e.ProgressPercentage);
        }

        public void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Hide();
            if (ShowCompleteDialog) MessageBox.Show("The Task was Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
