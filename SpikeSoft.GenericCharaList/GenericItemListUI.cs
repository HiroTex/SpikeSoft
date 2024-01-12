using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpikeSoft.GenericItemList
{
    public partial class GenericItemListUI: UserControl
    {
        public GenericItemListUI()
        {
            // Debug Settings
            InitializeComponent();
            InitializeImageList(null);
            InitializeItemBox(null);
            InitializeListView(null);
        }
        
        public GenericItemListUI(string filePath, ImageList images, string[] items, int[] list)
        {
            InitializeComponent();
            contextMenu.Renderer = new MyRenderer();
            InitializeImageList(images);
            InitializeItemBox(items);
            InitializeListView(list);
            ListContainer.Text = Path.GetFileNameWithoutExtension(filePath);
        }

        private void InitializeImageList(ImageList images)
        {
            if (images == null) return;
            imageList = images;
        }

        private void InitializeItemBox(string[] items)
        {
            ItemBox.MaxDropDownItems = 8;
            if (items == null) return;
            ItemBox.Items.AddRange(items);
        }

        public void InitializeListView(int[] list)
        {
            // Set Behaviour
            ItemList.ListViewItemSorter = new ListViewIndexComparer();
            ItemList.LargeImageList = imageList;

            // Initialize the drag-and-drop operation when running
            // under Windows XP or a later operating system.
            if (OSFeature.Feature.IsPresent(OSFeature.Themes))
            {
                ItemList.AllowDrop = true;
                ItemList.ItemDrag += new ItemDragEventHandler(lst_ItemDrag);
                ItemList.DragEnter += new DragEventHandler(lst_DragEnter);
                ItemList.DragOver += new DragEventHandler(lst_DragOver);
                ItemList.DragLeave += new EventHandler(lst_DragLeave);
                ItemList.DragDrop += new DragEventHandler(lst_DragDrop);
            }

            // Add Items
            foreach (var i in list)
            {
                ItemList.Items.Add(ItemBox.Items[i].ToString(), i);
            }
        }

        #region DragDropOperation

        // Starts the drag-and-drop operation when an item is dragged.
        private void lst_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ItemList.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        // See if we should allow this kind of drag.
        private void lst_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        
        // Select the item under the mouse during a drag.
        private void lst_DragOver(object sender, DragEventArgs e)
        {
            // Do nothing if the drag is not allowed.
            if (e.Effect != DragDropEffects.Move) return;
            
            // Retrieve the client coordinates of the mouse pointer.
            Point targetPoint = ItemList.PointToClient(new Point(e.X, e.Y));

            // Retrieve the index of the item closest to the mouse pointer.
            int targetIndex = ItemList.InsertionMark.NearestIndex(targetPoint);

            // Confirm that the mouse pointer is not over the dragged item.
            if (targetIndex > -1)
            {
                // Determine whether the mouse pointer is to the left or
                // the right of the midpoint of the closest item and set
                // the InsertionMark.AppearsAfterItem property accordingly.
                Rectangle itemBounds = ItemList.GetItemRect(targetIndex);
                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    ItemList.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    ItemList.InsertionMark.AppearsAfterItem = false;
                }
            }

            // Set the location of the insertion mark. If the mouse is
            // over the dragged item, the targetIndex value is -1 and
            // the insertion mark disappears.
            ItemList.InsertionMark.Index = targetIndex;
        }

        // Removes the insertion mark when the mouse leaves the control.
        private void lst_DragLeave(object sender, EventArgs e)
        {
            ItemList.InsertionMark.Index = -1;
        }

        // Drop the item here.
        private void lst_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the index of the insertion mark;
            int targetIndex = ItemList.InsertionMark.Index;

            // If the insertion mark is not visible, exit the method.
            if (targetIndex == -1)
            {
                return;
            }

            // If the insertion mark is to the right of the item with
            // the corresponding index, increment the target index.
            if (ItemList.InsertionMark.AppearsAfterItem)
            {
                targetIndex++;
            }

            // Retrieve the dragged item.
            foreach (ListViewItem item in ItemList.SelectedItems)
            {
                int prevIndex = item.Index;
                // Insert a copy of the dragged item at the target index.
                // A copy must be inserted before the original item is removed
                // to preserve item index values. 
                ItemList.Items.Insert(targetIndex, (ListViewItem)item.Clone());

                // Remove the original copy of the dragged item.
                ItemList.Items.Remove(item);

                // Increase Index if moving from last to first
                if (prevIndex > targetIndex) targetIndex++;
            }

            UpdateBinary();
        }

        // Sorts ListViewItem objects by index.
        private class ListViewIndexComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
            }
        }

        #endregion

        #region ContextMenuOperations

        public void lst_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            contextMenu.Items.Clear();
            contextMenu.Items.Add("Add       ");
            contextMenu.Items[0].Click += AddNewItem;
            contextMenu.Items[0].ToolTipText = "Add new Item";

            if (ItemList.SelectedItems.Count > 0)
            {
                contextMenu.Items.Add("Remove");
                contextMenu.Items[1].Click += RemoveSelectedItems;
                contextMenu.Items[1].ToolTipText = "Remove Selected Items";
            }

            foreach (ToolStripMenuItem item in contextMenu.Items)
            {
                item.ForeColor = Color.White;
                item.Margin = new Padding(3);
                item.Padding = new Padding(3);
            }


            contextMenu.Show(Cursor.Position);
        }

        public void AddNewItem(object sender, EventArgs e)
        {
            int index = (ItemList.SelectedItems.Count > 0 && (ItemList.SelectedItems[0].Index + 1) < ItemList.SelectedItems.Count) ? ItemList.SelectedItems[0].Index : ItemList.Items.Count;
            ItemList.Items.Insert(index, new ListViewItem(ItemBox.Items[0].ToString(), 0));
        }

        public void RemoveSelectedItems(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ItemList.SelectedItems)
            {
                ItemList.Items.RemoveAt(item.Index);
            }
        }

        #endregion

        #region UpdateData

        private void UpdateItemInfo(object sender, EventArgs e)
        {
            if (ItemList.SelectedItems.Count < 1) return;
            if (ItemList.SelectedItems[0].ImageIndex < 0) return;

            ItemPic.Image = imageList.Images[ItemList.SelectedItems[0].ImageIndex];
            ItemBox.SelectedIndex = ItemList.SelectedItems[0].ImageIndex;
        }

        private void UpdateListItemData(object sender, EventArgs e)
        {
            if (ItemBox.SelectedIndex < 0) return;

            ItemList.SelectedItems[0].Text = ItemBox.Items[ItemBox.SelectedIndex].ToString();
            ItemList.SelectedItems[0].ImageIndex = ItemBox.SelectedIndex;
            ItemPic.Image = imageList.Images[ItemBox.SelectedIndex];

            UpdateBinary();
        }

        private void UpdateBinary()
        {
            string tmpFile = SpikeSoft.UtilityManager.TmpMan.GetDefaultTmpFile();
            if (tmpFile == string.Empty)
            {
                return;
            }

            using (var fs = new FileStream(tmpFile, FileMode.Create, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            {
                foreach (ListViewItem item in ItemList.Items)
                {
                    bw.Write((int)item.ImageIndex);
                }
            }

        }

        #endregion
    }
}
