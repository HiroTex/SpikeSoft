using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.UiUtils
{
    public static class ListViewUtils
    {
        // Sorts ListViewItem objects by index.
        public class ListViewIndexComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
            }
        }

        #region DragDropOperation

        // Starts the drag-and-drop operation when an item is dragged.
        public static void lst_ItemDrag(object sender, ItemDragEventArgs e)
        {
            (sender as ListView).DoDragDrop(e.Item, DragDropEffects.Move);
        }

        // See if we should allow this kind of drag.
        public static void lst_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        // Select the item under the mouse during a drag.
        public static void lst_DragOver(object sender, DragEventArgs e)
        {
            // Do nothing if the drag is not allowed.
            if (e.Effect != DragDropEffects.Move) return;

            // cast
            ListView lv = sender as ListView;

            // Retrieve the client coordinates of the mouse pointer.
            Point targetPoint = lv.PointToClient(new Point(e.X, e.Y));

            // Retrieve the index of the item closest to the mouse pointer.
            int targetIndex = lv.InsertionMark.NearestIndex(targetPoint);

            // Confirm that the mouse pointer is not over the dragged item.
            if (targetIndex > -1)
            {
                // Determine whether the mouse pointer is to the left or
                // the right of the midpoint of the closest item and set
                // the InsertionMark.AppearsAfterItem property accordingly.
                Rectangle itemBounds = lv.GetItemRect(targetIndex);
                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    lv.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    lv.InsertionMark.AppearsAfterItem = false;
                }
            }

            // Set the location of the insertion mark. If the mouse is
            // over the dragged item, the targetIndex value is -1 and
            // the insertion mark disappears.
            lv.InsertionMark.Index = targetIndex;
        }

        // Removes the insertion mark when the mouse leaves the control.
        public static void lst_DragLeave(object sender, EventArgs e)
        {
            (sender as ListView).InsertionMark.Index = -1;
        }

        // Drop the item here.
        public static void lst_DragDrop(object sender, DragEventArgs e)
        {
            // cast
            ListView lv = sender as ListView;

            // Retrieve the index of the insertion mark;
            int targetIndex = lv.InsertionMark.Index;

            // If the insertion mark is not visible, exit the method.
            if (targetIndex == -1)
            {
                return;
            }

            // If the insertion mark is to the right of the item with
            // the corresponding index, increment the target index.
            if (lv.InsertionMark.AppearsAfterItem)
            {
                targetIndex++;
            }

            // Retrieve the dragged item.
            foreach (ListViewItem item in lv.SelectedItems)
            {
                int prevIndex = item.Index;
                // Insert a copy of the dragged item at the target index.
                // A copy must be inserted before the original item is removed
                // to preserve item index values. 
                lv.Items.Insert(targetIndex, (ListViewItem)item.Clone());

                // Remove the original copy of the dragged item.
                lv.Items.Remove(item);

                // Increase Index if moving from last to first
                if (prevIndex > targetIndex) targetIndex++;
            }
        }

        #endregion
    }
}
