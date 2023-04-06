namespace SpikeSoft
{
    partial class MainWindow
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.quickOpenFileBtn = new System.Windows.Forms.ToolStripButton();
            this.quickSaveFileBtn = new System.Windows.Forms.ToolStripButton();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.packagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnUnpackSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnUnpackAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnRepackSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnRepackAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickOpenFileBtn,
            this.quickSaveFileBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(466, 25);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // quickOpenFileBtn
            // 
            this.quickOpenFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.quickOpenFileBtn.Image = global::SpikeSoft.Properties.Resources.folder;
            this.quickOpenFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.quickOpenFileBtn.Name = "quickOpenFileBtn";
            this.quickOpenFileBtn.Size = new System.Drawing.Size(23, 22);
            this.quickOpenFileBtn.Text = "Open File";
            this.quickOpenFileBtn.Click += new System.EventHandler(this.OpenFile);
            // 
            // quickSaveFileBtn
            // 
            this.quickSaveFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.quickSaveFileBtn.Enabled = false;
            this.quickSaveFileBtn.Image = global::SpikeSoft.Properties.Resources.diskette;
            this.quickSaveFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.quickSaveFileBtn.Name = "quickSaveFileBtn";
            this.quickSaveFileBtn.Size = new System.Drawing.Size(23, 22);
            this.quickSaveFileBtn.Text = "Quick Save";
            this.quickSaveFileBtn.Click += new System.EventHandler(this.QuickSave);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.mainMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.packagingToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(466, 24);
            this.mainMenuStrip.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnOpen,
            this.toolBtnSaveAs,
            this.toolBtnSettings});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolBtnOpen
            // 
            this.toolBtnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnOpen.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnOpen.Name = "toolBtnOpen";
            this.toolBtnOpen.Size = new System.Drawing.Size(152, 22);
            this.toolBtnOpen.Text = "Open";
            this.toolBtnOpen.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnSaveAs.Enabled = false;
            this.toolBtnSaveAs.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(152, 22);
            this.toolBtnSaveAs.Text = "Save As...";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.SaveNewFile);
            // 
            // toolBtnSettings
            // 
            this.toolBtnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnSettings.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnSettings.Name = "toolBtnSettings";
            this.toolBtnSettings.Size = new System.Drawing.Size(152, 22);
            this.toolBtnSettings.Text = "Settings";
            this.toolBtnSettings.Click += new System.EventHandler(this.OpenSettings);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.mainPanel.BackgroundImage = global::SpikeSoft.Properties.Resources.MainLogo;
            this.mainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Consolas", 12F);
            this.mainPanel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.mainPanel.Location = new System.Drawing.Point(0, 49);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(466, 160);
            this.mainPanel.TabIndex = 1;
            // 
            // packagingToolStripMenuItem
            // 
            this.packagingToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.packagingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnUnpackSingle,
            this.toolBtnUnpackAll,
            this.toolBtnRepackSingle,
            this.toolBtnRepackAll});
            this.packagingToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.packagingToolStripMenuItem.Name = "packagingToolStripMenuItem";
            this.packagingToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.packagingToolStripMenuItem.Text = "Packaging";
            // 
            // toolBtnUnpackSingle
            // 
            this.toolBtnUnpackSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnUnpackSingle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnUnpackSingle.Name = "toolBtnUnpackSingle";
            this.toolBtnUnpackSingle.Size = new System.Drawing.Size(180, 22);
            this.toolBtnUnpackSingle.Text = "Unpack File";
            this.toolBtnUnpackSingle.Click += new System.EventHandler(this.toolBtnUnpackSingle_Click);
            // 
            // toolBtnUnpackAll
            // 
            this.toolBtnUnpackAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnUnpackAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnUnpackAll.Name = "toolBtnUnpackAll";
            this.toolBtnUnpackAll.Size = new System.Drawing.Size(180, 22);
            this.toolBtnUnpackAll.Text = "Unpack All in Folder";
            this.toolBtnUnpackAll.Click += new System.EventHandler(this.toolBtnUnpackAll_Click);
            // 
            // toolBtnRepackSingle
            // 
            this.toolBtnRepackSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnRepackSingle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnRepackSingle.Name = "toolBtnRepackSingle";
            this.toolBtnRepackSingle.Size = new System.Drawing.Size(180, 22);
            this.toolBtnRepackSingle.Text = "Repack";
            this.toolBtnRepackSingle.Click += new System.EventHandler(this.toolBtnRepackSingle_Click);
            // 
            // toolBtnRepackAll
            // 
            this.toolBtnRepackAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.toolBtnRepackAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolBtnRepackAll.Name = "toolBtnRepackAll";
            this.toolBtnRepackAll.Size = new System.Drawing.Size(180, 22);
            this.toolBtnRepackAll.Text = "Repack All in Folder";
            this.toolBtnRepackAll.Click += new System.EventHandler(this.toolBtnRepackAll_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(466, 209);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainWindow";
            this.Text = "SpikeSoft";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ExecuteDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TryDragDrop);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton quickOpenFileBtn;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripButton quickSaveFileBtn;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBtnOpen;
        private System.Windows.Forms.ToolStripMenuItem toolBtnSaveAs;
        private System.Windows.Forms.ToolStripMenuItem toolBtnSettings;
        private System.Windows.Forms.ToolStripMenuItem packagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBtnUnpackSingle;
        private System.Windows.Forms.ToolStripMenuItem toolBtnUnpackAll;
        private System.Windows.Forms.ToolStripMenuItem toolBtnRepackSingle;
        private System.Windows.Forms.ToolStripMenuItem toolBtnRepackAll;
    }
}

