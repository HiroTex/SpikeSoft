namespace SpikeSoft.ZS3Editor.Mission
{
    partial class CharacterSettingsWindow
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
            this.components = new System.ComponentModel.Container();
            this.PicBoxPanel = new System.Windows.Forms.Panel();
            this.charaPicBox = new System.Windows.Forms.PictureBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.charaBox = new System.Windows.Forms.ComboBox();
            this.boxZItem1 = new System.Windows.Forms.ComboBox();
            this.boxZItem2 = new System.Windows.Forms.ComboBox();
            this.boxZItem3 = new System.Windows.Forms.ComboBox();
            this.boxZItem4 = new System.Windows.Forms.ComboBox();
            this.boxZItem5 = new System.Windows.Forms.ComboBox();
            this.boxZItem6 = new System.Windows.Forms.ComboBox();
            this.boxZItem7 = new System.Windows.Forms.ComboBox();
            this.boxZItem8 = new System.Windows.Forms.ComboBox();
            this.costumeNumeric = new System.Windows.Forms.NumericUpDown();
            this.AINumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.PicBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charaPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costumeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AINumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBoxPanel
            // 
            this.PicBoxPanel.BackgroundImage = global::SpikeSoft.ZS3Editor.Mission.Properties.Resources.square;
            this.PicBoxPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicBoxPanel.Controls.Add(this.charaPicBox);
            this.PicBoxPanel.Location = new System.Drawing.Point(55, 61);
            this.PicBoxPanel.Margin = new System.Windows.Forms.Padding(4);
            this.PicBoxPanel.Name = "PicBoxPanel";
            this.PicBoxPanel.Size = new System.Drawing.Size(149, 123);
            this.PicBoxPanel.TabIndex = 12;
            // 
            // charaPicBox
            // 
            this.charaPicBox.BackColor = System.Drawing.Color.Transparent;
            this.charaPicBox.Location = new System.Drawing.Point(-9, -16);
            this.charaPicBox.Margin = new System.Windows.Forms.Padding(4);
            this.charaPicBox.Name = "charaPicBox";
            this.charaPicBox.Size = new System.Drawing.Size(169, 160);
            this.charaPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.charaPicBox.TabIndex = 0;
            this.charaPicBox.TabStop = false;
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(64, 64);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // charaBox
            // 
            this.charaBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.charaBox.Enabled = false;
            this.charaBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charaBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.charaBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.charaBox.IntegralHeight = false;
            this.charaBox.Location = new System.Drawing.Point(233, 86);
            this.charaBox.Margin = new System.Windows.Forms.Padding(4);
            this.charaBox.Name = "charaBox";
            this.charaBox.Size = new System.Drawing.Size(304, 27);
            this.charaBox.TabIndex = 0;
            this.charaBox.SelectedIndexChanged += new System.EventHandler(this.charaBox_SelectedIndexChanged);
            // 
            // boxZItem1
            // 
            this.boxZItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem1.Enabled = false;
            this.boxZItem1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem1.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem1.IntegralHeight = false;
            this.boxZItem1.Location = new System.Drawing.Point(128, 208);
            this.boxZItem1.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem1.Name = "boxZItem1";
            this.boxZItem1.Size = new System.Drawing.Size(351, 27);
            this.boxZItem1.TabIndex = 3;
            this.boxZItem1.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem2
            // 
            this.boxZItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem2.Enabled = false;
            this.boxZItem2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem2.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem2.IntegralHeight = false;
            this.boxZItem2.Location = new System.Drawing.Point(128, 243);
            this.boxZItem2.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem2.Name = "boxZItem2";
            this.boxZItem2.Size = new System.Drawing.Size(351, 27);
            this.boxZItem2.TabIndex = 4;
            this.boxZItem2.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem3
            // 
            this.boxZItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem3.Enabled = false;
            this.boxZItem3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem3.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem3.IntegralHeight = false;
            this.boxZItem3.Location = new System.Drawing.Point(128, 278);
            this.boxZItem3.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem3.Name = "boxZItem3";
            this.boxZItem3.Size = new System.Drawing.Size(351, 27);
            this.boxZItem3.TabIndex = 5;
            this.boxZItem3.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem4
            // 
            this.boxZItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem4.Enabled = false;
            this.boxZItem4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem4.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem4.IntegralHeight = false;
            this.boxZItem4.Location = new System.Drawing.Point(128, 313);
            this.boxZItem4.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem4.Name = "boxZItem4";
            this.boxZItem4.Size = new System.Drawing.Size(351, 27);
            this.boxZItem4.TabIndex = 6;
            this.boxZItem4.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem5
            // 
            this.boxZItem5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem5.Enabled = false;
            this.boxZItem5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem5.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem5.IntegralHeight = false;
            this.boxZItem5.Location = new System.Drawing.Point(128, 348);
            this.boxZItem5.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem5.Name = "boxZItem5";
            this.boxZItem5.Size = new System.Drawing.Size(351, 27);
            this.boxZItem5.TabIndex = 7;
            this.boxZItem5.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem6
            // 
            this.boxZItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem6.Enabled = false;
            this.boxZItem6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem6.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem6.IntegralHeight = false;
            this.boxZItem6.Location = new System.Drawing.Point(128, 383);
            this.boxZItem6.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem6.Name = "boxZItem6";
            this.boxZItem6.Size = new System.Drawing.Size(351, 27);
            this.boxZItem6.TabIndex = 8;
            this.boxZItem6.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem7
            // 
            this.boxZItem7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem7.Enabled = false;
            this.boxZItem7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem7.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem7.IntegralHeight = false;
            this.boxZItem7.Location = new System.Drawing.Point(128, 418);
            this.boxZItem7.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem7.Name = "boxZItem7";
            this.boxZItem7.Size = new System.Drawing.Size(351, 27);
            this.boxZItem7.TabIndex = 9;
            this.boxZItem7.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // boxZItem8
            // 
            this.boxZItem8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem8.Enabled = false;
            this.boxZItem8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem8.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem8.IntegralHeight = false;
            this.boxZItem8.Location = new System.Drawing.Point(128, 453);
            this.boxZItem8.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem8.Name = "boxZItem8";
            this.boxZItem8.Size = new System.Drawing.Size(351, 27);
            this.boxZItem8.TabIndex = 10;
            this.boxZItem8.SelectedIndexChanged += new System.EventHandler(this.zItemBox_SelectedIndexChanged);
            // 
            // costumeNumeric
            // 
            this.costumeNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.costumeNumeric.Enabled = false;
            this.costumeNumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.costumeNumeric.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.costumeNumeric.Location = new System.Drawing.Point(233, 149);
            this.costumeNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.costumeNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.costumeNumeric.Name = "costumeNumeric";
            this.costumeNumeric.Size = new System.Drawing.Size(146, 26);
            this.costumeNumeric.TabIndex = 1;
            this.costumeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.costumeNumeric.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // AINumeric
            // 
            this.AINumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.AINumeric.Enabled = false;
            this.AINumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.AINumeric.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AINumeric.Location = new System.Drawing.Point(391, 149);
            this.AINumeric.Margin = new System.Windows.Forms.Padding(4);
            this.AINumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AINumeric.Name = "AINumeric";
            this.AINumeric.Size = new System.Drawing.Size(146, 26);
            this.AINumeric.TabIndex = 2;
            this.AINumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AINumeric.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label3.Location = new System.Drawing.Point(229, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "Costume";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.Location = new System.Drawing.Point(387, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "IA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label2.Location = new System.Drawing.Point(229, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Character";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnSave.Location = new System.Drawing.Point(233, 506);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 29);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CharacterSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.BackgroundImage = global::SpikeSoft.ZS3Editor.Mission.Properties.Resources.boxSingle;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(607, 593);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AINumeric);
            this.Controls.Add(this.costumeNumeric);
            this.Controls.Add(this.boxZItem8);
            this.Controls.Add(this.boxZItem7);
            this.Controls.Add(this.boxZItem6);
            this.Controls.Add(this.boxZItem5);
            this.Controls.Add(this.boxZItem4);
            this.Controls.Add(this.boxZItem3);
            this.Controls.Add(this.boxZItem2);
            this.Controls.Add(this.boxZItem1);
            this.Controls.Add(this.charaBox);
            this.Controls.Add(this.PicBoxPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(623, 632);
            this.MinimumSize = new System.Drawing.Size(623, 632);
            this.Name = "CharacterSettingsWindow";
            this.Text = "Character Settings";
            this.PicBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.charaPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costumeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AINumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PicBoxPanel;
        private System.Windows.Forms.PictureBox charaPicBox;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ComboBox charaBox;
        private System.Windows.Forms.ComboBox boxZItem1;
        private System.Windows.Forms.ComboBox boxZItem2;
        private System.Windows.Forms.ComboBox boxZItem3;
        private System.Windows.Forms.ComboBox boxZItem4;
        private System.Windows.Forms.ComboBox boxZItem5;
        private System.Windows.Forms.ComboBox boxZItem6;
        private System.Windows.Forms.ComboBox boxZItem7;
        private System.Windows.Forms.ComboBox boxZItem8;
        private System.Windows.Forms.NumericUpDown costumeNumeric;
        private System.Windows.Forms.NumericUpDown AINumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
    }
}