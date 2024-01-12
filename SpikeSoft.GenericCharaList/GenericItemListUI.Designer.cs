namespace SpikeSoft.GenericItemList
{
    partial class GenericItemListUI
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
            this.ItemList = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.ListContainer = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ItemPic = new System.Windows.Forms.PictureBox();
            this.ItemBox = new System.Windows.Forms.ComboBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListContainer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemList
            // 
            this.ItemList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ItemList.AllowDrop = true;
            this.ItemList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemList.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemList.GridLines = true;
            this.ItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ItemList.HideSelection = false;
            this.ItemList.LargeImageList = this.imageList;
            this.ItemList.Location = new System.Drawing.Point(3, 16);
            this.ItemList.Name = "ItemList";
            this.ItemList.ShowGroups = false;
            this.ItemList.ShowItemToolTips = true;
            this.ItemList.Size = new System.Drawing.Size(660, 445);
            this.ItemList.TabIndex = 0;
            this.ItemList.UseCompatibleStateImageBehavior = false;
            this.ItemList.SelectedIndexChanged += new System.EventHandler(this.UpdateItemInfo);
            this.ItemList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDown);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListContainer
            // 
            this.ListContainer.Controls.Add(this.ItemList);
            this.ListContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListContainer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ListContainer.Location = new System.Drawing.Point(0, 0);
            this.ListContainer.Name = "ListContainer";
            this.ListContainer.Size = new System.Drawing.Size(666, 464);
            this.ListContainer.TabIndex = 1;
            this.ListContainer.TabStop = false;
            this.ListContainer.Text = "FileName";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ItemBox);
            this.groupBox2.Controls.Add(this.ItemPic);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(672, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 464);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Info";
            // 
            // ItemPic
            // 
            this.ItemPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemPic.Location = new System.Drawing.Point(25, 19);
            this.ItemPic.Name = "ItemPic";
            this.ItemPic.Size = new System.Drawing.Size(287, 191);
            this.ItemPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ItemPic.TabIndex = 0;
            this.ItemPic.TabStop = false;
            // 
            // ItemBox
            // 
            this.ItemBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ItemBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ItemBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.ItemBox.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ItemBox.FormattingEnabled = true;
            this.ItemBox.IntegralHeight = false;
            this.ItemBox.Location = new System.Drawing.Point(25, 225);
            this.ItemBox.Name = "ItemBox";
            this.ItemBox.Size = new System.Drawing.Size(287, 27);
            this.ItemBox.TabIndex = 1;
            this.ItemBox.SelectedIndexChanged += new System.EventHandler(this.UpdateListItemData);
            // 
            // contextMenu
            // 
            this.contextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(128, 26);
            // 
            // GenericItemListUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ListContainer);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "GenericItemListUI";
            this.Size = new System.Drawing.Size(1009, 464);
            this.ListContainer.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ItemList;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox ListContainer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ItemBox;
        private System.Windows.Forms.PictureBox ItemPic;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}
