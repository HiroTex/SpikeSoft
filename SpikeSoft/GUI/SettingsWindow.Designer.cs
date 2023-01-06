namespace SpikeSoft.GUI
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.groupBoxPaths = new System.Windows.Forms.GroupBox();
            this.btnSearchIMGPath = new System.Windows.Forms.Button();
            this.txtBoxIMGPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchTXTPath = new System.Windows.Forms.Button();
            this.txtBoxTXTPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchResourcePath = new System.Windows.Forms.Button();
            this.txtBoxResourcePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxGameMode = new System.Windows.Forms.GroupBox();
            this.btnGameModeSetting3 = new System.Windows.Forms.RadioButton();
            this.btnGameModeSetting2 = new System.Windows.Forms.RadioButton();
            this.btnGameModeSetting1 = new System.Windows.Forms.RadioButton();
            this.groupBoxConsoleMode = new System.Windows.Forms.GroupBox();
            this.btnConsoleModeSetting2 = new System.Windows.Forms.RadioButton();
            this.btnConsoleModeSetting1 = new System.Windows.Forms.RadioButton();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.checkListSettings = new System.Windows.Forms.CheckedListBox();
            this.txtBoxGAMEPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnResetDefaultSettings = new System.Windows.Forms.Button();
            this.btnCloseSettingsWindow = new System.Windows.Forms.Button();
            this.btnSaveSettingsWindow = new System.Windows.Forms.Button();
            this.groupBoxPaths.SuspendLayout();
            this.groupBoxGameMode.SuspendLayout();
            this.groupBoxConsoleMode.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPaths
            // 
            this.groupBoxPaths.Controls.Add(this.btnSearchIMGPath);
            this.groupBoxPaths.Controls.Add(this.txtBoxIMGPath);
            this.groupBoxPaths.Controls.Add(this.label3);
            this.groupBoxPaths.Controls.Add(this.btnSearchTXTPath);
            this.groupBoxPaths.Controls.Add(this.txtBoxTXTPath);
            this.groupBoxPaths.Controls.Add(this.label2);
            this.groupBoxPaths.Controls.Add(this.btnSearchResourcePath);
            this.groupBoxPaths.Controls.Add(this.txtBoxResourcePath);
            this.groupBoxPaths.Controls.Add(this.label1);
            this.groupBoxPaths.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPaths.Font = new System.Drawing.Font("Consolas", 12F);
            this.groupBoxPaths.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxPaths.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPaths.Name = "groupBoxPaths";
            this.groupBoxPaths.Size = new System.Drawing.Size(779, 130);
            this.groupBoxPaths.TabIndex = 1;
            this.groupBoxPaths.TabStop = false;
            this.groupBoxPaths.Text = "Common Resources Paths";
            // 
            // btnSearchIMGPath
            // 
            this.btnSearchIMGPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSearchIMGPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSearchIMGPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchIMGPath.Font = new System.Drawing.Font("Consolas", 8F);
            this.btnSearchIMGPath.Location = new System.Drawing.Point(725, 84);
            this.btnSearchIMGPath.Name = "btnSearchIMGPath";
            this.btnSearchIMGPath.Size = new System.Drawing.Size(44, 26);
            this.btnSearchIMGPath.TabIndex = 8;
            this.btnSearchIMGPath.Text = "...";
            this.btnSearchIMGPath.UseVisualStyleBackColor = false;
            // 
            // txtBoxIMGPath
            // 
            this.txtBoxIMGPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtBoxIMGPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxIMGPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBoxIMGPath.Location = new System.Drawing.Point(312, 84);
            this.txtBoxIMGPath.Name = "txtBoxIMGPath";
            this.txtBoxIMGPath.ReadOnly = true;
            this.txtBoxIMGPath.Size = new System.Drawing.Size(407, 26);
            this.txtBoxIMGPath.TabIndex = 7;
            this.txtBoxIMGPath.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Image Resources Folder Path";
            // 
            // btnSearchTXTPath
            // 
            this.btnSearchTXTPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSearchTXTPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSearchTXTPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchTXTPath.Font = new System.Drawing.Font("Consolas", 8F);
            this.btnSearchTXTPath.Location = new System.Drawing.Point(725, 51);
            this.btnSearchTXTPath.Name = "btnSearchTXTPath";
            this.btnSearchTXTPath.Size = new System.Drawing.Size(44, 26);
            this.btnSearchTXTPath.TabIndex = 5;
            this.btnSearchTXTPath.Text = "...";
            this.btnSearchTXTPath.UseVisualStyleBackColor = false;
            // 
            // txtBoxTXTPath
            // 
            this.txtBoxTXTPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtBoxTXTPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxTXTPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBoxTXTPath.Location = new System.Drawing.Point(312, 51);
            this.txtBoxTXTPath.Name = "txtBoxTXTPath";
            this.txtBoxTXTPath.ReadOnly = true;
            this.txtBoxTXTPath.Size = new System.Drawing.Size(407, 26);
            this.txtBoxTXTPath.TabIndex = 4;
            this.txtBoxTXTPath.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Text Resources Folder Path";
            // 
            // btnSearchResourcePath
            // 
            this.btnSearchResourcePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSearchResourcePath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSearchResourcePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchResourcePath.Font = new System.Drawing.Font("Consolas", 8F);
            this.btnSearchResourcePath.Location = new System.Drawing.Point(725, 19);
            this.btnSearchResourcePath.Name = "btnSearchResourcePath";
            this.btnSearchResourcePath.Size = new System.Drawing.Size(44, 26);
            this.btnSearchResourcePath.TabIndex = 2;
            this.btnSearchResourcePath.Text = "...";
            this.btnSearchResourcePath.UseVisualStyleBackColor = false;
            this.btnSearchResourcePath.Click += new System.EventHandler(this.SearchPath);
            // 
            // txtBoxResourcePath
            // 
            this.txtBoxResourcePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtBoxResourcePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxResourcePath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBoxResourcePath.Location = new System.Drawing.Point(312, 19);
            this.txtBoxResourcePath.Name = "txtBoxResourcePath";
            this.txtBoxResourcePath.ReadOnly = true;
            this.txtBoxResourcePath.Size = new System.Drawing.Size(407, 26);
            this.txtBoxResourcePath.TabIndex = 1;
            this.txtBoxResourcePath.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resource Folder Path";
            // 
            // groupBoxGameMode
            // 
            this.groupBoxGameMode.Controls.Add(this.btnGameModeSetting3);
            this.groupBoxGameMode.Controls.Add(this.btnGameModeSetting2);
            this.groupBoxGameMode.Controls.Add(this.btnGameModeSetting1);
            this.groupBoxGameMode.Font = new System.Drawing.Font("Consolas", 12F);
            this.groupBoxGameMode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxGameMode.Location = new System.Drawing.Point(6, 134);
            this.groupBoxGameMode.Name = "groupBoxGameMode";
            this.groupBoxGameMode.Size = new System.Drawing.Size(202, 127);
            this.groupBoxGameMode.TabIndex = 2;
            this.groupBoxGameMode.TabStop = false;
            this.groupBoxGameMode.Text = "Game Mode";
            // 
            // btnGameModeSetting3
            // 
            this.btnGameModeSetting3.AutoSize = true;
            this.btnGameModeSetting3.Location = new System.Drawing.Point(12, 87);
            this.btnGameModeSetting3.Name = "btnGameModeSetting3";
            this.btnGameModeSetting3.Size = new System.Drawing.Size(171, 23);
            this.btnGameModeSetting3.TabIndex = 2;
            this.btnGameModeSetting3.TabStop = true;
            this.btnGameModeSetting3.Text = "Sparking! Meteor";
            this.btnGameModeSetting3.UseVisualStyleBackColor = true;
            this.btnGameModeSetting3.CheckedChanged += new System.EventHandler(this.SetGameMode);
            // 
            // btnGameModeSetting2
            // 
            this.btnGameModeSetting2.AutoSize = true;
            this.btnGameModeSetting2.Location = new System.Drawing.Point(12, 58);
            this.btnGameModeSetting2.Name = "btnGameModeSetting2";
            this.btnGameModeSetting2.Size = new System.Drawing.Size(144, 23);
            this.btnGameModeSetting2.TabIndex = 1;
            this.btnGameModeSetting2.TabStop = true;
            this.btnGameModeSetting2.Text = "Sparking! NEO";
            this.btnGameModeSetting2.UseVisualStyleBackColor = true;
            this.btnGameModeSetting2.CheckedChanged += new System.EventHandler(this.SetGameMode);
            // 
            // btnGameModeSetting1
            // 
            this.btnGameModeSetting1.AutoSize = true;
            this.btnGameModeSetting1.Location = new System.Drawing.Point(12, 29);
            this.btnGameModeSetting1.Name = "btnGameModeSetting1";
            this.btnGameModeSetting1.Size = new System.Drawing.Size(108, 23);
            this.btnGameModeSetting1.TabIndex = 0;
            this.btnGameModeSetting1.TabStop = true;
            this.btnGameModeSetting1.Text = "Sparking!";
            this.btnGameModeSetting1.UseVisualStyleBackColor = true;
            this.btnGameModeSetting1.CheckedChanged += new System.EventHandler(this.SetGameMode);
            // 
            // groupBoxConsoleMode
            // 
            this.groupBoxConsoleMode.Controls.Add(this.btnConsoleModeSetting2);
            this.groupBoxConsoleMode.Controls.Add(this.btnConsoleModeSetting1);
            this.groupBoxConsoleMode.Font = new System.Drawing.Font("Consolas", 12F);
            this.groupBoxConsoleMode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxConsoleMode.Location = new System.Drawing.Point(214, 134);
            this.groupBoxConsoleMode.Name = "groupBoxConsoleMode";
            this.groupBoxConsoleMode.Size = new System.Drawing.Size(202, 127);
            this.groupBoxConsoleMode.TabIndex = 4;
            this.groupBoxConsoleMode.TabStop = false;
            this.groupBoxConsoleMode.Text = "Console Mode";
            // 
            // btnConsoleModeSetting2
            // 
            this.btnConsoleModeSetting2.AutoSize = true;
            this.btnConsoleModeSetting2.Location = new System.Drawing.Point(16, 76);
            this.btnConsoleModeSetting2.Name = "btnConsoleModeSetting2";
            this.btnConsoleModeSetting2.Size = new System.Drawing.Size(54, 23);
            this.btnConsoleModeSetting2.TabIndex = 1;
            this.btnConsoleModeSetting2.TabStop = true;
            this.btnConsoleModeSetting2.Text = "Wii";
            this.btnConsoleModeSetting2.UseVisualStyleBackColor = true;
            this.btnConsoleModeSetting2.CheckedChanged += new System.EventHandler(this.SetWiiMode);
            // 
            // btnConsoleModeSetting1
            // 
            this.btnConsoleModeSetting1.AutoSize = true;
            this.btnConsoleModeSetting1.Location = new System.Drawing.Point(16, 38);
            this.btnConsoleModeSetting1.Name = "btnConsoleModeSetting1";
            this.btnConsoleModeSetting1.Size = new System.Drawing.Size(54, 23);
            this.btnConsoleModeSetting1.TabIndex = 0;
            this.btnConsoleModeSetting1.TabStop = true;
            this.btnConsoleModeSetting1.Text = "PS2";
            this.btnConsoleModeSetting1.UseVisualStyleBackColor = true;
            this.btnConsoleModeSetting1.CheckedChanged += new System.EventHandler(this.SetWiiMode);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.checkListSettings);
            this.groupBoxSettings.Controls.Add(this.txtBoxGAMEPath);
            this.groupBoxSettings.Controls.Add(this.label6);
            this.groupBoxSettings.Font = new System.Drawing.Font("Consolas", 12F);
            this.groupBoxSettings.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxSettings.Location = new System.Drawing.Point(424, 134);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(348, 127);
            this.groupBoxSettings.TabIndex = 5;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Game Mode Settings";
            // 
            // checkListSettings
            // 
            this.checkListSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkListSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkListSettings.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkListSettings.FormattingEnabled = true;
            this.checkListSettings.Items.AddRange(new object[] {
            "Delete File at Unpack",
            "Unpack Complete"});
            this.checkListSettings.Location = new System.Drawing.Point(10, 59);
            this.checkListSettings.Name = "checkListSettings";
            this.checkListSettings.Size = new System.Drawing.Size(330, 42);
            this.checkListSettings.TabIndex = 2;
            this.checkListSettings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.UpdateSettingFlags);
            // 
            // txtBoxGAMEPath
            // 
            this.txtBoxGAMEPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtBoxGAMEPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxGAMEPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBoxGAMEPath.Location = new System.Drawing.Point(238, 22);
            this.txtBoxGAMEPath.Name = "txtBoxGAMEPath";
            this.txtBoxGAMEPath.Size = new System.Drawing.Size(102, 26);
            this.txtBoxGAMEPath.TabIndex = 1;
            this.txtBoxGAMEPath.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Resources ID";
            // 
            // btnResetDefaultSettings
            // 
            this.btnResetDefaultSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnResetDefaultSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnResetDefaultSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetDefaultSettings.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnResetDefaultSettings.Location = new System.Drawing.Point(6, 279);
            this.btnResetDefaultSettings.Name = "btnResetDefaultSettings";
            this.btnResetDefaultSettings.Size = new System.Drawing.Size(156, 29);
            this.btnResetDefaultSettings.TabIndex = 6;
            this.btnResetDefaultSettings.Text = "Reset Defaults";
            this.btnResetDefaultSettings.UseVisualStyleBackColor = false;
            this.btnResetDefaultSettings.Click += new System.EventHandler(this.ResetDefaultSettings);
            // 
            // btnCloseSettingsWindow
            // 
            this.btnCloseSettingsWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCloseSettingsWindow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCloseSettingsWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseSettingsWindow.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnCloseSettingsWindow.Location = new System.Drawing.Point(446, 279);
            this.btnCloseSettingsWindow.Name = "btnCloseSettingsWindow";
            this.btnCloseSettingsWindow.Size = new System.Drawing.Size(156, 29);
            this.btnCloseSettingsWindow.TabIndex = 7;
            this.btnCloseSettingsWindow.Text = "Close";
            this.btnCloseSettingsWindow.UseVisualStyleBackColor = false;
            this.btnCloseSettingsWindow.Click += new System.EventHandler(this.CloseWindow);
            // 
            // btnSaveSettingsWindow
            // 
            this.btnSaveSettingsWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSaveSettingsWindow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSaveSettingsWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettingsWindow.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnSaveSettingsWindow.Location = new System.Drawing.Point(616, 279);
            this.btnSaveSettingsWindow.Name = "btnSaveSettingsWindow";
            this.btnSaveSettingsWindow.Size = new System.Drawing.Size(156, 29);
            this.btnSaveSettingsWindow.TabIndex = 8;
            this.btnSaveSettingsWindow.Text = "Save";
            this.btnSaveSettingsWindow.UseVisualStyleBackColor = false;
            this.btnSaveSettingsWindow.Click += new System.EventHandler(this.SaveSettings);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(779, 330);
            this.Controls.Add(this.btnSaveSettingsWindow);
            this.Controls.Add(this.btnCloseSettingsWindow);
            this.Controls.Add(this.btnResetDefaultSettings);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxConsoleMode);
            this.Controls.Add(this.groupBoxGameMode);
            this.Controls.Add(this.groupBoxPaths);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.Text = "SettingsWindow";
            this.groupBoxPaths.ResumeLayout(false);
            this.groupBoxPaths.PerformLayout();
            this.groupBoxGameMode.ResumeLayout(false);
            this.groupBoxGameMode.PerformLayout();
            this.groupBoxConsoleMode.ResumeLayout(false);
            this.groupBoxConsoleMode.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPaths;
        private System.Windows.Forms.Button btnSearchIMGPath;
        private System.Windows.Forms.TextBox txtBoxIMGPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchTXTPath;
        private System.Windows.Forms.TextBox txtBoxTXTPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchResourcePath;
        private System.Windows.Forms.TextBox txtBoxResourcePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxGameMode;
        private System.Windows.Forms.RadioButton btnGameModeSetting3;
        private System.Windows.Forms.RadioButton btnGameModeSetting2;
        private System.Windows.Forms.RadioButton btnGameModeSetting1;
        private System.Windows.Forms.GroupBox groupBoxConsoleMode;
        private System.Windows.Forms.RadioButton btnConsoleModeSetting2;
        private System.Windows.Forms.RadioButton btnConsoleModeSetting1;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.CheckedListBox checkListSettings;
        private System.Windows.Forms.TextBox txtBoxGAMEPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnResetDefaultSettings;
        private System.Windows.Forms.Button btnCloseSettingsWindow;
        private System.Windows.Forms.Button btnSaveSettingsWindow;
    }
}