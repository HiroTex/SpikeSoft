namespace SpikeSoft.DBTManager
{
    partial class UIDBTManager
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.itemImgList = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.itemDbtList = new System.Windows.Forms.ListBox();
            this.imgFull = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlFullImg = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imgFull)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlFullImg.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemImgList
            // 
            this.itemImgList.AutoArrange = false;
            this.itemImgList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.itemImgList.ForeColor = System.Drawing.SystemColors.Control;
            this.itemImgList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.itemImgList.Location = new System.Drawing.Point(3, 104);
            this.itemImgList.Name = "itemImgList";
            this.itemImgList.Size = new System.Drawing.Size(159, 411);
            this.itemImgList.TabIndex = 3;
            this.itemImgList.UseCompatibleStateImageBehavior = false;
            this.itemImgList.View = System.Windows.Forms.View.SmallIcon;
            this.itemImgList.SelectedIndexChanged += new System.EventHandler(this.DisplayMainImg);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(64, 64);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // itemDbtList
            // 
            this.itemDbtList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.itemDbtList.ForeColor = System.Drawing.SystemColors.Control;
            this.itemDbtList.FormattingEnabled = true;
            this.itemDbtList.Location = new System.Drawing.Point(3, 3);
            this.itemDbtList.Name = "itemDbtList";
            this.itemDbtList.Size = new System.Drawing.Size(159, 95);
            this.itemDbtList.TabIndex = 2;
            this.itemDbtList.SelectedIndexChanged += new System.EventHandler(this.SetImageList);
            // 
            // imgFull
            // 
            this.imgFull.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgFull.BackColor = System.Drawing.Color.Transparent;
            this.imgFull.BackgroundImage = global::SpikeSoft.DBTManager.Properties.Resources.alpha;
            this.imgFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgFull.Location = new System.Drawing.Point(0, 3);
            this.imgFull.MaximumSize = new System.Drawing.Size(512, 512);
            this.imgFull.Name = "imgFull";
            this.imgFull.Size = new System.Drawing.Size(512, 512);
            this.imgFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgFull.TabIndex = 3;
            this.imgFull.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statLabel
            // 
            this.statLabel.Name = "statLabel";
            this.statLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pnlMain.Controls.Add(this.pnlFullImg);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.menuStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(690, 548);
            this.pnlMain.TabIndex = 5;
            // 
            // pnlFullImg
            // 
            this.pnlFullImg.Controls.Add(this.imgFull);
            this.pnlFullImg.Location = new System.Drawing.Point(168, 24);
            this.pnlFullImg.Name = "pnlFullImg";
            this.pnlFullImg.Size = new System.Drawing.Size(512, 512);
            this.pnlFullImg.TabIndex = 6;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pnlLeft.Controls.Add(this.itemDbtList);
            this.pnlLeft.Controls.Add(this.itemImgList);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 24);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLeft.Size = new System.Drawing.Size(165, 524);
            this.pnlLeft.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(690, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem2});
            this.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.ToolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.openToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem2.Text = "Export";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundImageToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // backgroundImageToolStripMenuItem
            // 
            this.backgroundImageToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.backgroundImageToolStripMenuItem.Checked = true;
            this.backgroundImageToolStripMenuItem.CheckOnClick = true;
            this.backgroundImageToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundImageToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.backgroundImageToolStripMenuItem.Name = "backgroundImageToolStripMenuItem";
            this.backgroundImageToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.backgroundImageToolStripMenuItem.Text = "Background Image";
            this.backgroundImageToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SetBackgroundImage);
            // 
            // UIDBTManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.MaximumSize = new System.Drawing.Size(690, 570);
            this.MinimumSize = new System.Drawing.Size(689, 569);
            this.Name = "UIDBTManager";
            this.Size = new System.Drawing.Size(690, 570);
            ((System.ComponentModel.ISupportInitialize)(this.imgFull)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFullImg.ResumeLayout(false);
            this.pnlFullImg.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView itemImgList;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ListBox itemDbtList;
        private System.Windows.Forms.PictureBox imgFull;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statLabel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundImageToolStripMenuItem;
        private System.Windows.Forms.Panel pnlFullImg;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
