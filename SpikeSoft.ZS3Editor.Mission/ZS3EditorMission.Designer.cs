namespace SpikeSoft.ZS3Editor.Mission
{
    partial class ZS3EditorMission
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
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.MapBox = new System.Windows.Forms.ComboBox();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.MissionBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dpBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.conditionBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TransformableBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bgmBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mapDesBox = new System.Windows.Forms.ComboBox();
            this.RefereeBox = new System.Windows.Forms.ComboBox();
            this.charaSetBox = new System.Windows.Forms.ComboBox();
            this.picCharaH = new System.Windows.Forms.PictureBox();
            this.picCharaG = new System.Windows.Forms.PictureBox();
            this.picCharaF = new System.Windows.Forms.PictureBox();
            this.picCharaE = new System.Windows.Forms.PictureBox();
            this.picCharaD = new System.Windows.Forms.PictureBox();
            this.picCharaC = new System.Windows.Forms.PictureBox();
            this.picCharaB = new System.Windows.Forms.PictureBox();
            this.picCharaA = new System.Windows.Forms.PictureBox();
            this.charaImages = new System.Windows.Forms.ImageList(this.components);
            this.mapImages = new System.Windows.Forms.ImageList(this.components);
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaA)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlPanel.BackgroundImage = global::SpikeSoft.ZS3Editor.Mission.Properties.Resources.box;
            this.ControlPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ControlPanel.Controls.Add(this.MapBox);
            this.ControlPanel.Controls.Add(this.picMap);
            this.ControlPanel.Controls.Add(this.MissionBox);
            this.ControlPanel.Controls.Add(this.label7);
            this.ControlPanel.Controls.Add(this.dpBox);
            this.ControlPanel.Controls.Add(this.label6);
            this.ControlPanel.Controls.Add(this.conditionBox);
            this.ControlPanel.Controls.Add(this.label5);
            this.ControlPanel.Controls.Add(this.TransformableBox);
            this.ControlPanel.Controls.Add(this.label4);
            this.ControlPanel.Controls.Add(this.bgmBox);
            this.ControlPanel.Controls.Add(this.label3);
            this.ControlPanel.Controls.Add(this.timeBox);
            this.ControlPanel.Controls.Add(this.label2);
            this.ControlPanel.Controls.Add(this.label1);
            this.ControlPanel.Controls.Add(this.mapDesBox);
            this.ControlPanel.Controls.Add(this.RefereeBox);
            this.ControlPanel.Controls.Add(this.charaSetBox);
            this.ControlPanel.Controls.Add(this.picCharaH);
            this.ControlPanel.Controls.Add(this.picCharaG);
            this.ControlPanel.Controls.Add(this.picCharaF);
            this.ControlPanel.Controls.Add(this.picCharaE);
            this.ControlPanel.Controls.Add(this.picCharaD);
            this.ControlPanel.Controls.Add(this.picCharaC);
            this.ControlPanel.Controls.Add(this.picCharaB);
            this.ControlPanel.Controls.Add(this.picCharaA);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(905, 599);
            this.ControlPanel.TabIndex = 0;
            // 
            // MapBox
            // 
            this.MapBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.MapBox.Enabled = false;
            this.MapBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MapBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.MapBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MapBox.FormattingEnabled = true;
            this.MapBox.IntegralHeight = false;
            this.MapBox.Location = new System.Drawing.Point(528, 468);
            this.MapBox.Name = "MapBox";
            this.MapBox.Size = new System.Drawing.Size(235, 27);
            this.MapBox.TabIndex = 5;
            this.MapBox.SelectedIndexChanged += new System.EventHandler(this.MapBox_SelectedIndexChanged);
            // 
            // picMap
            // 
            this.picMap.BackgroundImage = global::SpikeSoft.ZS3Editor.Mission.Properties.Resources.square;
            this.picMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMap.Enabled = false;
            this.picMap.Location = new System.Drawing.Point(383, 428);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(117, 108);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMap.TabIndex = 25;
            this.picMap.TabStop = false;
            // 
            // MissionBox
            // 
            this.MissionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.MissionBox.Enabled = false;
            this.MissionBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MissionBox.Font = new System.Drawing.Font("Consolas", 16F);
            this.MissionBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MissionBox.FormattingEnabled = true;
            this.MissionBox.IntegralHeight = false;
            this.MissionBox.Location = new System.Drawing.Point(289, 52);
            this.MissionBox.Name = "MissionBox";
            this.MissionBox.Size = new System.Drawing.Size(545, 32);
            this.MissionBox.TabIndex = 1;
            this.MissionBox.SelectedIndexChanged += new System.EventHandler(this.MissionBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label7.Location = new System.Drawing.Point(723, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "DP Mode";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dpBox
            // 
            this.dpBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dpBox.Enabled = false;
            this.dpBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dpBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.dpBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dpBox.FormattingEnabled = true;
            this.dpBox.IntegralHeight = false;
            this.dpBox.Items.AddRange(new object[] {
            "Disabled",
            "10",
            "15",
            "20"});
            this.dpBox.Location = new System.Drawing.Point(705, 144);
            this.dpBox.Name = "dpBox";
            this.dpBox.Size = new System.Drawing.Size(117, 27);
            this.dpBox.TabIndex = 3;
            this.dpBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label6.Location = new System.Drawing.Point(455, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "Condition";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // conditionBox
            // 
            this.conditionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.conditionBox.Enabled = false;
            this.conditionBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.conditionBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.conditionBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.conditionBox.FormattingEnabled = true;
            this.conditionBox.IntegralHeight = false;
            this.conditionBox.Location = new System.Drawing.Point(332, 144);
            this.conditionBox.Name = "conditionBox";
            this.conditionBox.Size = new System.Drawing.Size(362, 27);
            this.conditionBox.TabIndex = 2;
            this.conditionBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label5.Location = new System.Drawing.Point(13, 488);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Transformable Enemies";
            // 
            // TransformableBox
            // 
            this.TransformableBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.TransformableBox.Enabled = false;
            this.TransformableBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TransformableBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.TransformableBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TransformableBox.FormattingEnabled = true;
            this.TransformableBox.IntegralHeight = false;
            this.TransformableBox.Items.AddRange(new object[] {
            "Disabled",
            "Enabled"});
            this.TransformableBox.Location = new System.Drawing.Point(17, 510);
            this.TransformableBox.Name = "TransformableBox";
            this.TransformableBox.Size = new System.Drawing.Size(186, 27);
            this.TransformableBox.TabIndex = 10;
            this.TransformableBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label4.Location = new System.Drawing.Point(13, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 19);
            this.label4.TabIndex = 14;
            this.label4.Text = "BGM";
            // 
            // bgmBox
            // 
            this.bgmBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.bgmBox.Enabled = false;
            this.bgmBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bgmBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.bgmBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bgmBox.FormattingEnabled = true;
            this.bgmBox.IntegralHeight = false;
            this.bgmBox.Location = new System.Drawing.Point(17, 427);
            this.bgmBox.Name = "bgmBox";
            this.bgmBox.Size = new System.Drawing.Size(186, 27);
            this.bgmBox.TabIndex = 9;
            this.bgmBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label3.Location = new System.Drawing.Point(13, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "Time";
            // 
            // timeBox
            // 
            this.timeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.timeBox.Enabled = false;
            this.timeBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timeBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.timeBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.timeBox.FormattingEnabled = true;
            this.timeBox.IntegralHeight = false;
            this.timeBox.Items.AddRange(new object[] {
            "Unlimited",
            "60",
            "90",
            "180",
            "240",
            "45"});
            this.timeBox.Location = new System.Drawing.Point(17, 344);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(186, 27);
            this.timeBox.TabIndex = 8;
            this.timeBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label2.Location = new System.Drawing.Point(13, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Referee";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.Location = new System.Drawing.Point(13, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Map Destruction";
            // 
            // mapDesBox
            // 
            this.mapDesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.mapDesBox.Enabled = false;
            this.mapDesBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mapDesBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.mapDesBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.mapDesBox.FormattingEnabled = true;
            this.mapDesBox.IntegralHeight = false;
            this.mapDesBox.Items.AddRange(new object[] {
            "Disabled",
            "Enabled"});
            this.mapDesBox.Location = new System.Drawing.Point(17, 261);
            this.mapDesBox.Name = "mapDesBox";
            this.mapDesBox.Size = new System.Drawing.Size(186, 27);
            this.mapDesBox.TabIndex = 7;
            this.mapDesBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // RefereeBox
            // 
            this.RefereeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.RefereeBox.Enabled = false;
            this.RefereeBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefereeBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.RefereeBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.RefereeBox.FormattingEnabled = true;
            this.RefereeBox.IntegralHeight = false;
            this.RefereeBox.Location = new System.Drawing.Point(17, 177);
            this.RefereeBox.Name = "RefereeBox";
            this.RefereeBox.Size = new System.Drawing.Size(186, 27);
            this.RefereeBox.TabIndex = 6;
            this.RefereeBox.SelectedIndexChanged += new System.EventHandler(this.BattleSetting_SelectedIndexChanged);
            // 
            // charaSetBox
            // 
            this.charaSetBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.charaSetBox.Enabled = false;
            this.charaSetBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charaSetBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.charaSetBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.charaSetBox.FormattingEnabled = true;
            this.charaSetBox.IntegralHeight = false;
            this.charaSetBox.Location = new System.Drawing.Point(506, 186);
            this.charaSetBox.Name = "charaSetBox";
            this.charaSetBox.Size = new System.Drawing.Size(143, 27);
            this.charaSetBox.TabIndex = 4;
            this.charaSetBox.SelectedIndexChanged += new System.EventHandler(this.MissionBox_SelectedIndexChanged);
            // 
            // picCharaH
            // 
            this.picCharaH.Enabled = false;
            this.picCharaH.Location = new System.Drawing.Point(705, 314);
            this.picCharaH.Name = "picCharaH";
            this.picCharaH.Size = new System.Drawing.Size(117, 108);
            this.picCharaH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaH.TabIndex = 7;
            this.picCharaH.TabStop = false;
            this.picCharaH.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaH.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaH.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaG
            // 
            this.picCharaG.Enabled = false;
            this.picCharaG.Location = new System.Drawing.Point(577, 314);
            this.picCharaG.Name = "picCharaG";
            this.picCharaG.Size = new System.Drawing.Size(117, 108);
            this.picCharaG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaG.TabIndex = 6;
            this.picCharaG.TabStop = false;
            this.picCharaG.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaG.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaG.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaF
            // 
            this.picCharaF.Enabled = false;
            this.picCharaF.Location = new System.Drawing.Point(450, 314);
            this.picCharaF.Name = "picCharaF";
            this.picCharaF.Size = new System.Drawing.Size(117, 108);
            this.picCharaF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaF.TabIndex = 5;
            this.picCharaF.TabStop = false;
            this.picCharaF.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaF.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaF.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaE
            // 
            this.picCharaE.Enabled = false;
            this.picCharaE.Location = new System.Drawing.Point(323, 314);
            this.picCharaE.Name = "picCharaE";
            this.picCharaE.Size = new System.Drawing.Size(117, 108);
            this.picCharaE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaE.TabIndex = 4;
            this.picCharaE.TabStop = false;
            this.picCharaE.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaE.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaE.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaD
            // 
            this.picCharaD.Enabled = false;
            this.picCharaD.Location = new System.Drawing.Point(705, 218);
            this.picCharaD.Name = "picCharaD";
            this.picCharaD.Size = new System.Drawing.Size(117, 108);
            this.picCharaD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaD.TabIndex = 3;
            this.picCharaD.TabStop = false;
            this.picCharaD.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaD.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaD.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaC
            // 
            this.picCharaC.Enabled = false;
            this.picCharaC.Location = new System.Drawing.Point(577, 218);
            this.picCharaC.Name = "picCharaC";
            this.picCharaC.Size = new System.Drawing.Size(117, 108);
            this.picCharaC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaC.TabIndex = 2;
            this.picCharaC.TabStop = false;
            this.picCharaC.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaC.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaC.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaB
            // 
            this.picCharaB.Enabled = false;
            this.picCharaB.Location = new System.Drawing.Point(450, 218);
            this.picCharaB.Name = "picCharaB";
            this.picCharaB.Size = new System.Drawing.Size(117, 108);
            this.picCharaB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaB.TabIndex = 1;
            this.picCharaB.TabStop = false;
            this.picCharaB.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaB.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaB.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // picCharaA
            // 
            this.picCharaA.Enabled = false;
            this.picCharaA.Location = new System.Drawing.Point(323, 218);
            this.picCharaA.Name = "picCharaA";
            this.picCharaA.Size = new System.Drawing.Size(117, 108);
            this.picCharaA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCharaA.TabIndex = 0;
            this.picCharaA.TabStop = false;
            this.picCharaA.Click += new System.EventHandler(this.picChara_Click);
            this.picCharaA.MouseEnter += new System.EventHandler(this.picChara_MouseEnter);
            this.picCharaA.MouseLeave += new System.EventHandler(this.picChara_MouseLeave);
            // 
            // charaImages
            // 
            this.charaImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.charaImages.ImageSize = new System.Drawing.Size(64, 64);
            this.charaImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mapImages
            // 
            this.mapImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.mapImages.ImageSize = new System.Drawing.Size(64, 64);
            this.mapImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ZS3EditorMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.BackgroundImage = global::SpikeSoft.ZS3Editor.Mission.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.ControlPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZS3EditorMission";
            this.Size = new System.Drawing.Size(905, 599);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCharaA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.PictureBox picCharaA;
        private System.Windows.Forms.PictureBox picCharaH;
        private System.Windows.Forms.PictureBox picCharaG;
        private System.Windows.Forms.PictureBox picCharaF;
        private System.Windows.Forms.PictureBox picCharaE;
        private System.Windows.Forms.PictureBox picCharaD;
        private System.Windows.Forms.PictureBox picCharaC;
        private System.Windows.Forms.PictureBox picCharaB;
        private System.Windows.Forms.ComboBox charaSetBox;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.ComboBox MissionBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dpBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox conditionBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TransformableBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox bgmBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox timeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mapDesBox;
        private System.Windows.Forms.ComboBox RefereeBox;
        private System.Windows.Forms.ComboBox MapBox;
        private System.Windows.Forms.ImageList charaImages;
        private System.Windows.Forms.ImageList mapImages;
    }
}
