namespace SpikeSoft.ZS3Editor.CharaInfo
{
    partial class ZS3EditorCharaInfo
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
            this.hudPicture = new System.Windows.Forms.PictureBox();
            this.hpBarNumeric = new System.Windows.Forms.NumericUpDown();
            this.hpBarLabel = new System.Windows.Forms.Label();
            this.kiBarLabel = new System.Windows.Forms.Label();
            this.blastLabel = new System.Windows.Forms.Label();
            this.kiBarNumeric = new System.Windows.Forms.NumericUpDown();
            this.blastNumeric = new System.Windows.Forms.NumericUpDown();
            this.charaList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.hudPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hpBarNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiBarNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blastNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // hudPicture
            // 
            this.hudPicture.BackgroundImage = global::SpikeSoft.ZS3Editor.CharaInfo.Properties.Resources.HealthBar_0_Base;
            this.hudPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.hudPicture.Location = new System.Drawing.Point(370, 11);
            this.hudPicture.Margin = new System.Windows.Forms.Padding(4);
            this.hudPicture.Name = "hudPicture";
            this.hudPicture.Size = new System.Drawing.Size(384, 94);
            this.hudPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.hudPicture.TabIndex = 1;
            this.hudPicture.TabStop = false;
            // 
            // hpBarNumeric
            // 
            this.hpBarNumeric.AutoSize = true;
            this.hpBarNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.hpBarNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hpBarNumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.hpBarNumeric.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.hpBarNumeric.Location = new System.Drawing.Point(385, 120);
            this.hpBarNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.hpBarNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.hpBarNumeric.Name = "hpBarNumeric";
            this.hpBarNumeric.Size = new System.Drawing.Size(80, 26);
            this.hpBarNumeric.TabIndex = 1;
            this.hpBarNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hpBarNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.hpBarNumeric.ValueChanged += new System.EventHandler(this.UpdateHUDImage);
            // 
            // hpBarLabel
            // 
            this.hpBarLabel.AutoSize = true;
            this.hpBarLabel.Font = new System.Drawing.Font("Consolas", 12F);
            this.hpBarLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.hpBarLabel.Location = new System.Drawing.Point(473, 123);
            this.hpBarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hpBarLabel.Name = "hpBarLabel";
            this.hpBarLabel.Size = new System.Drawing.Size(72, 19);
            this.hpBarLabel.TabIndex = 5;
            this.hpBarLabel.Text = "HP Bars";
            // 
            // kiBarLabel
            // 
            this.kiBarLabel.AutoSize = true;
            this.kiBarLabel.Font = new System.Drawing.Font("Consolas", 12F);
            this.kiBarLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.kiBarLabel.Location = new System.Drawing.Point(473, 169);
            this.kiBarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.kiBarLabel.Name = "kiBarLabel";
            this.kiBarLabel.Size = new System.Drawing.Size(72, 19);
            this.kiBarLabel.TabIndex = 6;
            this.kiBarLabel.Text = "Ki Bars";
            // 
            // blastLabel
            // 
            this.blastLabel.AutoSize = true;
            this.blastLabel.Font = new System.Drawing.Font("Consolas", 12F);
            this.blastLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.blastLabel.Location = new System.Drawing.Point(473, 216);
            this.blastLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blastLabel.Name = "blastLabel";
            this.blastLabel.Size = new System.Drawing.Size(162, 19);
            this.blastLabel.TabIndex = 7;
            this.blastLabel.Text = "Blast Stock Units";
            // 
            // kiBarNumeric
            // 
            this.kiBarNumeric.AutoSize = true;
            this.kiBarNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.kiBarNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kiBarNumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.kiBarNumeric.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.kiBarNumeric.Location = new System.Drawing.Point(385, 166);
            this.kiBarNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.kiBarNumeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.kiBarNumeric.Name = "kiBarNumeric";
            this.kiBarNumeric.Size = new System.Drawing.Size(80, 26);
            this.kiBarNumeric.TabIndex = 2;
            this.kiBarNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.kiBarNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.kiBarNumeric.ValueChanged += new System.EventHandler(this.UpdateHUDImage);
            // 
            // blastNumeric
            // 
            this.blastNumeric.AutoSize = true;
            this.blastNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.blastNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blastNumeric.Font = new System.Drawing.Font("Consolas", 12F);
            this.blastNumeric.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.blastNumeric.Location = new System.Drawing.Point(385, 213);
            this.blastNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.blastNumeric.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.blastNumeric.Name = "blastNumeric";
            this.blastNumeric.Size = new System.Drawing.Size(80, 26);
            this.blastNumeric.TabIndex = 3;
            this.blastNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.blastNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.blastNumeric.ValueChanged += new System.EventHandler(this.UpdateHUDImage);
            // 
            // charaList
            // 
            this.charaList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.charaList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.charaList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charaList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.charaList.Dock = System.Windows.Forms.DockStyle.Left;
            this.charaList.Font = new System.Drawing.Font("Consolas", 10F);
            this.charaList.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.charaList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.charaList.Location = new System.Drawing.Point(0, 0);
            this.charaList.Margin = new System.Windows.Forms.Padding(4);
            this.charaList.MultiSelect = false;
            this.charaList.Name = "charaList";
            this.charaList.Size = new System.Drawing.Size(362, 251);
            this.charaList.TabIndex = 0;
            this.charaList.UseCompatibleStateImageBehavior = false;
            this.charaList.View = System.Windows.Forms.View.Details;
            this.charaList.SelectedIndexChanged += new System.EventHandler(this.CharaListIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Character List";
            this.columnHeader1.Width = 220;
            // 
            // ZS3EditorCharaInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.charaList);
            this.Controls.Add(this.blastNumeric);
            this.Controls.Add(this.kiBarNumeric);
            this.Controls.Add(this.blastLabel);
            this.Controls.Add(this.kiBarLabel);
            this.Controls.Add(this.hpBarLabel);
            this.Controls.Add(this.hpBarNumeric);
            this.Controls.Add(this.hudPicture);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZS3EditorCharaInfo";
            this.Size = new System.Drawing.Size(764, 251);
            ((System.ComponentModel.ISupportInitialize)(this.hudPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hpBarNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiBarNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blastNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox hudPicture;
        private System.Windows.Forms.NumericUpDown hpBarNumeric;
        private System.Windows.Forms.Label hpBarLabel;
        private System.Windows.Forms.Label kiBarLabel;
        private System.Windows.Forms.Label blastLabel;
        private System.Windows.Forms.NumericUpDown kiBarNumeric;
        private System.Windows.Forms.NumericUpDown blastNumeric;
        private System.Windows.Forms.ListView charaList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
