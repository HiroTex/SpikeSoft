using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DBTManager
{
    class DBT2BMP
    {
        private byte[] pxData;
        private byte[] plData;
        private int width;
        private int height;
        private int CLUTSize;
        private bool Converted = false;

        public DBT2BMP(byte[] pxData, byte[] plData, int width, int height, int CLUTSize)
        {
            this.pxData = pxData;
            this.plData = plData;
            this.width = width;
            this.height = height;
            this.CLUTSize = CLUTSize;
        }

        public Bitmap GetBitmap()
        {
            if (!Converted)
            {
                switch (CLUTSize)
                {
                    case 01:
                        ReorderPixelData();
                        break;
                    case 04:
                        pxData = TransformToBMPOrder();
                        plData = ReorderPalData();
                        break;
                }
            }

            return CreateBitmapFromRawData();
        }

        public void ReorderPixelData()
        {
            byte[] output = new byte[pxData.Length];

            int nPointer = 0;
            int oPointer = 0;
            int baseOffset = 0;
            int columnOffset = 0;
            int blockColumnOffset = 0;

            int columnSize = 32;
            int blockColumnsCount = width / 32;
            int blockCount = 1;

            if (width > 128)
            {
                blockCount = width / 128;
                blockColumnsCount = 4; // 128 / 32
            }

            // Block Row = W x 16
            for (int blockRow = 0; blockRow < (height / 16); blockRow++)
            {
                // Column Row = W x 2
                for (int columnRow = 0; columnRow < 8; columnRow++)
                {
                    blockColumnOffset = columnOffset;
                    // Block = 128 x 16
                    for (int block = 0; block < blockCount; block++)
                    {
                        // Column = 32 x 2 interlaced
                        for (int column = 0; column < blockColumnsCount; column++)
                        {
                            Array.Copy(pxData, oPointer, output, nPointer, columnSize);
                            oPointer += width * 16;
                            nPointer += columnSize;
                        }
                        // Next Block
                        blockColumnOffset += 256; // 256 = 8 * columnSize
                        oPointer = blockColumnOffset;
                    }
                    // Next column
                    columnOffset += width * 2;
                    oPointer = columnOffset;
                }
                baseOffset += columnSize;
                columnOffset = baseOffset;
                oPointer = baseOffset;
                if (((blockRow + 1) % 8) == 0)
                {
                    // Next Page
                    baseOffset = 128 * width * (((blockRow + 1) / 8)) / 2;
                    columnOffset = baseOffset;
                    oPointer = baseOffset;
                }
            }

            pxData = output;
            pxData = Interlacing();
            pxData = TexTo0X();
            pxData = TransformToBMPOrder();
        }

        public byte[] ReorderPalData()
        {
            byte[] output = new byte[plData.Length];
            int basePtr = 0;
            int outPtr = 0;

            Array.Copy(plData, basePtr, output, outPtr, 32);
            basePtr += 32;
            outPtr = 64;

            for (int j = 0; j < 7; j++)
            {
                Array.Copy(plData, basePtr, output, outPtr, 32);
                basePtr += 32;
                outPtr -= 32;

                Array.Copy(plData, basePtr, output, outPtr, 32);
                basePtr += 32;
                outPtr += 64;

                Array.Copy(plData, basePtr, output, outPtr, 64);
                basePtr += 64;
                outPtr += 96;
            }

            Array.Copy(plData, basePtr, output, outPtr, 32);
            basePtr += 32;
            outPtr -= 32;

            Array.Copy(plData, basePtr, output, outPtr, 32);
            basePtr += 32;
            outPtr += 32;

            Array.Copy(plData, basePtr, output, basePtr, 32);

            return output;
        }

        private byte[] Interlacing()
        {
            int nPointer = 0;
            int oPointer = 0;

            byte[] b = new byte[pxData.Length];

            for (int i = 0; i < pxData.Length / 32; i++)
            {
                for (int g = 0; g < 2; g++)
                {
                    int counter = 0;

                    for (int j = 0; j < 8; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            b[nPointer++] = pxData[oPointer + counter + k];
                        }

                        counter += 4;
                    }

                    oPointer += 2;
                }

                oPointer += 28;
            }

            return b;
        }

        private byte[] TexTo0X()
        {
            string hex = BitConverter.ToString(pxData).Replace("-", string.Empty);

            char[] charArray = new char[hex.Length];
            using (StringReader sr = new StringReader(hex))
            {
                sr.Read(charArray, 0, hex.Length);
            }

            string[] stringArray = new string[hex.Length];

            for (int i = 0; i < hex.Length; i++)
            {
                if ((i % 2) == 0) stringArray[i] = "0" + charArray[i + 1];
                else stringArray[i] = "0" + charArray[i - 1];
            }

            hex = (String.Concat(stringArray));
            return Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
        }

        public byte[] TransformToBMPOrder()
        {
            byte[] output = new byte[pxData.Length];

            int reducirPNF = width * 2;
            int oPointer = 0;
            int nPointer = output.Length - width;

            for (int i = 0; i < (width * height) / ((width * 4) * 2); i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int j = 0; j < (width / 16); j++)
                    {
                        output = TexBufRearrange8(pxData, output, oPointer, nPointer);
                        nPointer += 16;
                        oPointer += 32;
                    }

                    nPointer -= reducirPNF;
                }

                oPointer -= (reducirPNF * 2) - 17;

                for (int k = 0; k < 2; k++)
                {
                    for (int j = 0; j < (width / 16); j++)
                    {
                        output = TexBufRearrange4(pxData, output, oPointer, nPointer);
                        nPointer += 16;
                        oPointer += 32;
                    }

                    nPointer -= reducirPNF;
                }

                oPointer -= 1;

                for (int j = 0; j < (width / 16); j++)
                {
                    output = TexBufRearrange4(pxData, output, oPointer, nPointer);
                    nPointer += 16;
                    oPointer += 32;
                }

                nPointer -= reducirPNF;

                for (int j = 0; j < (width / 16); j++)
                {
                    output = TexBufRearrange4(pxData, output, oPointer, nPointer);
                    nPointer += 16;
                    oPointer += 32;
                }

                oPointer -= (reducirPNF * 2) + 15;
                nPointer -= reducirPNF;

                for (int k = 0; k < 2; k++)
                {
                    for (int j = 0; j < (width / 16); j++)
                    {
                        output = TexBufRearrange8(pxData, output, oPointer, nPointer);
                        nPointer += 16;
                        oPointer += 32;
                    }

                    nPointer -= reducirPNF;
                }

                oPointer -= 1;
            }

            return output;
        }

        private byte[] TexBufRearrange(byte[] file, byte[] output, int oPointer, int nPointer, int loops)
        {
            for (int k = 0; k < loops; k++)
            {
                output[nPointer++] = file[oPointer];
                oPointer += 4;
            }

            return output;
        }

        private byte[] TexBufRearrange8(byte[] file, byte[] output, int oPointer, int nPointer)
        {
            output = TexBufRearrange(file, output, oPointer, nPointer, 8);
            output = TexBufRearrange(file, output, oPointer + 2, nPointer + 8, 8);
            return output;
        }

        private byte[] TexBufRearrange4(byte[] file, byte[] output, int oPointer, int nPointer)
        {
            output = TexBufRearrange(file, output, oPointer, nPointer, 4);
            oPointer -= 16; //Posición 1
            output = TexBufRearrange(file, output, oPointer, nPointer + 4, 4);
            oPointer += 18; //Posición 19
            output = TexBufRearrange(file, output, oPointer, nPointer + 8, 4);
            oPointer -= 16; //Posición 3
            output = TexBufRearrange(file, output, oPointer, nPointer + 12, 4);
            return output;
        }

        private Bitmap CreateBitmapFromRawData()
        {
            // Create a new 8bpp indexed image
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            // Set the palette
            ColorPalette bmpPalette = bmp.Palette;

            int paletteEntries = plData.Length / 4; // Each palette entry is 4 bytes (RGBA)
            for (int i = 0; i < paletteEntries; i++)
            {
                int paletteIndex = i * 4;
                byte r = plData[paletteIndex];
                byte g = plData[paletteIndex + 1];
                byte b = plData[paletteIndex + 2];

                // Convert the alpha value based on your specified criteria
                byte a = plData[paletteIndex + 3];
                if ((plData[paletteIndex + 3] * 2) != 0)
                {
                    a = (byte)((plData[paletteIndex + 3] * 2) - 1);
                }

                bmpPalette.Entries[i] = Color.FromArgb(a, r, g, b);
            }

            bmp.Palette = bmpPalette;

            // Set the pixel data
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            byte[] bmpBytes = new byte[bmpData.Stride * height];

            // Copy the BMP-formatted pixel data into the image
            Buffer.BlockCopy(pxData, 0, bmpBytes, 0, bmpBytes.Length);

            // Flip the image vertically
            for (int y = 0; y < height / 2; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int topIndex = y * bmpData.Stride + x;
                    int bottomIndex = (height - y - 1) * bmpData.Stride + x;

                    byte temp = bmpBytes[topIndex];
                    bmpBytes[topIndex] = bmpBytes[bottomIndex];
                    bmpBytes[bottomIndex] = temp;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(bmpBytes, 0, bmpData.Scan0, bmpBytes.Length);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}
