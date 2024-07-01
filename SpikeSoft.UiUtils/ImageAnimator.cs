using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SpikeSoft.UiUtils
{
    public class ImageAnimator
    {
        private PictureBox picBox;
        private ImageList imgList;
        private System.Timers.Timer Tmr;
        private int prevIndex = -1;
        private int nextIndex = -1;
        private float opacity = 0.0f;
        private bool forceEnd = false;

        public ImageAnimator(PictureBox pictureBox, ImageList source, int prevIndex, int nextIndex)
        {
            picBox = pictureBox;
            imgList = source;
            this.prevIndex = prevIndex;
            this.nextIndex = nextIndex;
        }

        public void SetTimer(System.ComponentModel.ISynchronizeInvoke source, int ms, ElapsedEventHandler func, bool autoReset)
        {
            Task.Run(() =>
            {
                // Create a timer with a milisecond interval.
                Tmr = new System.Timers.Timer(ms);

                // Hook up the Elapsed event for the timer. 
                Tmr.Elapsed += func;
                Tmr.AutoReset = autoReset;
                Tmr.SynchronizingObject = source;
                Tmr.Start();
            });
        }

        public void ImageTransition (Object sender, ElapsedEventArgs e)
        {
            try
            {
                // Initialize Image to Set
                Image img = null;

                // If there's an image in the Control already, use it instead of Null
                if (picBox.Image != null)
                {
                    img = ImageTransparency.ChangeOpacity(picBox.Image, opacity);
                }

                // If there is an Item Selected in ListView and there's no previous image, set new Image
                if (prevIndex == -1)
                {
                    img = ImageTransparency.ChangeOpacity(imgList.Images[nextIndex], opacity);
                }
                else
                {
                    // Blend previous and new image
                    img = ImageTransparency.BlendImage(imgList.Images[prevIndex], imgList.Images[nextIndex], opacity);
                }

                // Set New Image
                picBox.Image = img;
                picBox.Refresh();

                // Increase Opacity Value
                opacity += 0.25f;
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                // If Opacity value is not Full, repeat process
                if ((opacity <= 1.0f) && !forceEnd)
                {
                    Tmr.Start();
                }
                else
                {
                    EndTimer();
                }
            }
        }

        private void EndTimer()
        {
            Tmr.Stop();
            Tmr.Dispose();
            opacity = 0.0f;
            prevIndex = nextIndex;
        }

        public void ForceEnd()
        {
            forceEnd = true;
        }
    }
}
