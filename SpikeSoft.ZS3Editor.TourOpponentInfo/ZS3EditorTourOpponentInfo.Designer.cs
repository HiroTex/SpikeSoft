namespace SpikeSoft.ZS3Editor.TourOpponentInfo
{
    partial class ZS3EditorTourOpponentInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.boxSelectDiff = new System.Windows.Forms.ComboBox();
            this.aiNumeric = new System.Windows.Forms.NumericUpDown();
            this.aiLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boxSelectRound = new System.Windows.Forms.ComboBox();
            this.boxZItem8 = new System.Windows.Forms.ComboBox();
            this.boxZItem7 = new System.Windows.Forms.ComboBox();
            this.boxZItem6 = new System.Windows.Forms.ComboBox();
            this.boxZItem5 = new System.Windows.Forms.ComboBox();
            this.boxZItem4 = new System.Windows.Forms.ComboBox();
            this.boxZItem3 = new System.Windows.Forms.ComboBox();
            this.boxZItem2 = new System.Windows.Forms.ComboBox();
            this.boxZItem1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureLevel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.aiNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(180, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 31;
            this.label1.Text = "Level";
            // 
            // boxSelectDiff
            // 
            this.boxSelectDiff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.boxSelectDiff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.boxSelectDiff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.boxSelectDiff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxSelectDiff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.boxSelectDiff.Font = new System.Drawing.Font("Consolas", 14F);
            this.boxSelectDiff.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxSelectDiff.FormattingEnabled = true;
            this.boxSelectDiff.Items.AddRange(new object[] {
            "Easy",
            "Normal",
            "Hard"});
            this.boxSelectDiff.Location = new System.Drawing.Point(8, 15);
            this.boxSelectDiff.MaxDropDownItems = 5;
            this.boxSelectDiff.Name = "boxSelectDiff";
            this.boxSelectDiff.Size = new System.Drawing.Size(165, 30);
            this.boxSelectDiff.TabIndex = 28;
            this.boxSelectDiff.SelectedIndexChanged += new System.EventHandler(this.UpdateLevelImage);
            // 
            // aiNumeric
            // 
            this.aiNumeric.AutoSize = true;
            this.aiNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.aiNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aiNumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.aiNumeric.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.aiNumeric.Location = new System.Drawing.Point(8, 123);
            this.aiNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.aiNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.aiNumeric.Name = "aiNumeric";
            this.aiNumeric.Size = new System.Drawing.Size(80, 26);
            this.aiNumeric.TabIndex = 32;
            this.aiNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.aiNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.aiNumeric.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // aiLabel
            // 
            this.aiLabel.AutoSize = true;
            this.aiLabel.Font = new System.Drawing.Font("Consolas", 12F);
            this.aiLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.aiLabel.Location = new System.Drawing.Point(96, 125);
            this.aiLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aiLabel.Name = "aiLabel";
            this.aiLabel.Size = new System.Drawing.Size(81, 19);
            this.aiLabel.TabIndex = 33;
            this.aiLabel.Text = "AI Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 14F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(180, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 22);
            this.label2.TabIndex = 35;
            this.label2.Text = "Round";
            // 
            // boxSelectRound
            // 
            this.boxSelectRound.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.boxSelectRound.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.boxSelectRound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.boxSelectRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxSelectRound.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.boxSelectRound.Font = new System.Drawing.Font("Consolas", 14F);
            this.boxSelectRound.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxSelectRound.FormattingEnabled = true;
            this.boxSelectRound.Items.AddRange(new object[] {
            "First Battle",
            "Second Battle",
            "Quarterfinals",
            "Semifinals",
            "Finals"});
            this.boxSelectRound.Location = new System.Drawing.Point(8, 66);
            this.boxSelectRound.MaxDropDownItems = 5;
            this.boxSelectRound.Name = "boxSelectRound";
            this.boxSelectRound.Size = new System.Drawing.Size(165, 30);
            this.boxSelectRound.TabIndex = 34;
            this.boxSelectRound.SelectedIndexChanged += new System.EventHandler(this.UpdateEditorData);
            // 
            // boxZItem8
            // 
            this.boxZItem8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem8.Enabled = false;
            this.boxZItem8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem8.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem8.IntegralHeight = false;
            this.boxZItem8.Location = new System.Drawing.Point(8, 442);
            this.boxZItem8.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem8.Name = "boxZItem8";
            this.boxZItem8.Size = new System.Drawing.Size(351, 27);
            this.boxZItem8.TabIndex = 43;
            this.boxZItem8.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem7
            // 
            this.boxZItem7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem7.Enabled = false;
            this.boxZItem7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem7.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem7.IntegralHeight = false;
            this.boxZItem7.Location = new System.Drawing.Point(8, 407);
            this.boxZItem7.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem7.Name = "boxZItem7";
            this.boxZItem7.Size = new System.Drawing.Size(351, 27);
            this.boxZItem7.TabIndex = 42;
            this.boxZItem7.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem6
            // 
            this.boxZItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem6.Enabled = false;
            this.boxZItem6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem6.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem6.IntegralHeight = false;
            this.boxZItem6.Location = new System.Drawing.Point(8, 372);
            this.boxZItem6.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem6.Name = "boxZItem6";
            this.boxZItem6.Size = new System.Drawing.Size(351, 27);
            this.boxZItem6.TabIndex = 41;
            this.boxZItem6.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem5
            // 
            this.boxZItem5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem5.Enabled = false;
            this.boxZItem5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem5.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem5.IntegralHeight = false;
            this.boxZItem5.Location = new System.Drawing.Point(8, 337);
            this.boxZItem5.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem5.Name = "boxZItem5";
            this.boxZItem5.Size = new System.Drawing.Size(351, 27);
            this.boxZItem5.TabIndex = 40;
            this.boxZItem5.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem4
            // 
            this.boxZItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem4.Enabled = false;
            this.boxZItem4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem4.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem4.IntegralHeight = false;
            this.boxZItem4.Location = new System.Drawing.Point(8, 302);
            this.boxZItem4.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem4.Name = "boxZItem4";
            this.boxZItem4.Size = new System.Drawing.Size(351, 27);
            this.boxZItem4.TabIndex = 39;
            this.boxZItem4.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem3
            // 
            this.boxZItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem3.Enabled = false;
            this.boxZItem3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem3.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem3.IntegralHeight = false;
            this.boxZItem3.Location = new System.Drawing.Point(8, 267);
            this.boxZItem3.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem3.Name = "boxZItem3";
            this.boxZItem3.Size = new System.Drawing.Size(351, 27);
            this.boxZItem3.TabIndex = 38;
            this.boxZItem3.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem2
            // 
            this.boxZItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem2.Enabled = false;
            this.boxZItem2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem2.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem2.IntegralHeight = false;
            this.boxZItem2.Location = new System.Drawing.Point(8, 232);
            this.boxZItem2.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem2.Name = "boxZItem2";
            this.boxZItem2.Size = new System.Drawing.Size(351, 27);
            this.boxZItem2.TabIndex = 37;
            this.boxZItem2.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // boxZItem1
            // 
            this.boxZItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.boxZItem1.Enabled = false;
            this.boxZItem1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxZItem1.Font = new System.Drawing.Font("Consolas", 12F);
            this.boxZItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.boxZItem1.IntegralHeight = false;
            this.boxZItem1.Location = new System.Drawing.Point(8, 197);
            this.boxZItem1.Margin = new System.Windows.Forms.Padding(4);
            this.boxZItem1.Name = "boxZItem1";
            this.boxZItem1.Size = new System.Drawing.Size(351, 27);
            this.boxZItem1.TabIndex = 36;
            this.boxZItem1.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(7, 165);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 19);
            this.label3.TabIndex = 44;
            this.label3.Text = "Z Item Customization";
            // 
            // pictureLevel
            // 
            this.pictureLevel.Image = global::SpikeSoft.ZS3Editor.TourOpponentInfo.Properties.Resources.Level_1;
            this.pictureLevel.Location = new System.Drawing.Point(285, 3);
            this.pictureLevel.Name = "pictureLevel";
            this.pictureLevel.Size = new System.Drawing.Size(74, 51);
            this.pictureLevel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureLevel.TabIndex = 30;
            this.pictureLevel.TabStop = false;
            // 
            // ZS3EditorTourOpponentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.boxZItem8);
            this.Controls.Add(this.boxZItem7);
            this.Controls.Add(this.boxZItem6);
            this.Controls.Add(this.boxZItem5);
            this.Controls.Add(this.boxZItem4);
            this.Controls.Add(this.boxZItem3);
            this.Controls.Add(this.boxZItem2);
            this.Controls.Add(this.boxZItem1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxSelectRound);
            this.Controls.Add(this.aiNumeric);
            this.Controls.Add(this.aiLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureLevel);
            this.Controls.Add(this.boxSelectDiff);
            this.Font = new System.Drawing.Font("Consolas", 14F);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ZS3EditorTourOpponentInfo";
            this.Size = new System.Drawing.Size(368, 480);
            ((System.ComponentModel.ISupportInitialize)(this.aiNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureLevel;
        private System.Windows.Forms.ComboBox boxSelectDiff;
        private System.Windows.Forms.NumericUpDown aiNumeric;
        private System.Windows.Forms.Label aiLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox boxSelectRound;
        private System.Windows.Forms.ComboBox boxZItem8;
        private System.Windows.Forms.ComboBox boxZItem7;
        private System.Windows.Forms.ComboBox boxZItem6;
        private System.Windows.Forms.ComboBox boxZItem5;
        private System.Windows.Forms.ComboBox boxZItem4;
        private System.Windows.Forms.ComboBox boxZItem3;
        private System.Windows.Forms.ComboBox boxZItem2;
        private System.Windows.Forms.ComboBox boxZItem1;
        private System.Windows.Forms.Label label3;
    }
}
