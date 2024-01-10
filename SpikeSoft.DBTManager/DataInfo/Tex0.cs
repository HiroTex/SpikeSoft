namespace GifReg
{
    public struct Tex0
    {
        public ulong Data;

        public enum PixelStorageFormat
        {
            /// <summary>
            /// RGBA32, uses 32-bit per pixel.
            /// </summary>
            PSMCT32 = 0,
            /// <summary>
            /// RGB24, uses 24-bit per pixel with the upper 8 bit unused.
            /// </summary>
            PSMCT24 = 1,
            /// <summary>
            /// RGBA16 unsigned, pack two pixels in 32-bit in little endian order.
            /// </summary>
            PSMCT16 = 2,
            /// <summary>
            /// RGBA16 signed, pack two pixels in 32-bit in little endian order.
            /// </summary>
            PSMCT16S = 0xa,
            /// <summary>
            /// 8-bit indexed, packing 4 pixels per 32-bit.
            /// </summary>
            PSMT8 = 0x13,
            /// <summary>
            /// 8-bit indexed, packing 4 pixels per 32-bit.
            /// </summary>
            PSMT4 = 0x14,
            /// <summary>
            /// 8-bit indexed, but the upper 24-bit are unused.
            /// </summary>
            PSMT8H = 0x1b,
            /// <summary>
            /// 4-bit indexed, but the upper 24-bit are unused.
            /// </summary>
            PSMT4HL = 0x24,
            /// <summary>
            /// 4-bit indexed, where the bits 4-7 are evaluated and the rest discarded.
            /// </summary>
            PSMT4HH = 0x2c,
            /// <summary>
            /// 32-bit Z buffer.
            /// </summary>
            PSMZ32 = 0x30,
            /// <summary>
            /// 24-bit Z buffer with the upper 8-bit unused.
            /// </summary>
            PSMZ24 = 0x31,
            /// <summary>
            /// 16-bit unsigned Z buffer, pack two pixels in 32-bit in little endian order.
            /// </summary>
            PSMZ16 = 0x32,
            /// <summary>
            /// 16-bit signed Z buffer, pack two pixels in 32-bit in little endian order.
            /// </summary>
            PSMZ16S = 0x3a
        }

        public enum TextureFunction
        {
            Modulate = 0,
            Decal = 1,
            Hilight = 2,
            Hilight_2 = 3
        }

        public enum CLUTStorageFormat
        {
            /// <summary>
            /// 32-bit color palette.
            /// </summary>
            PSMCT32 = 0,
            /// <summary>
            /// 24-bit color palette.
            /// </summary>
            PSMCT24 = 1,
            /// <summary>
            /// 16-bit color palette.
            /// </summary>
            PSMCT16 = 2,
            /// <summary>
            /// 16-bit color palette.
            /// </summary>
            PSMCT16S = 0xa
        }

        /// <summary>
        /// Texture buffer location. Multiply it by 0x100 to get the raw VRAM pointer.
        /// </summary>
        public ushort TBP0
        {
            get { return (ushort)(Data & 0x3fff); }
            set
            {
                if (value > 0x3fff) value = 0x3fff;
                Data = Data & 0xffffffffffffc000 | value;
            }
        }

        /// <summary>
        /// Texture buffer width.
        /// </summary>
        public byte TBW
        {
            get { return (byte)(Data >> 14 & 0x3f); }
            set
            {
                if (value > 0x3f) value = 0x3f;
                Data = Data & 0xfffffffffff03fff | (ulong)value << 14;
            }
        }

        /// <summary>
        /// Pixel storage format.
        /// </summary>
        public PixelStorageFormat PSM
        {
            get { return (PixelStorageFormat)(Data >> 20 & 0x3f); }
            set { Data = Data & 0xfffffffffc0fffff | (ulong)value << 14; }
        }

        /// <summary>
        /// Texture width.
        /// </summary>
        public byte TW
        {
            get { return (byte)(Data >> 26 & 0xf); }
            set
            {
                if (value > 0xf) value = 0xf;
                Data = Data & 0xffffffffc3ffffff | (ulong)value << 26;
            }
        }

        /// <summary>
        /// Texture height.
        /// </summary>
        public byte TH
        {
            get { return (byte)(Data >> 30 & 0xf); }
            set
            {
                if (value > 0xf) value = 0xf;
                Data = Data & 0xffffffff3fffffff | (ulong)value << 30;
            }
        }

        /// <summary>
        /// The texture or the clut contains an alpha channel.
        /// </summary>
        public bool TCC
        {
            get { return (Data >> 34 & 0x1) != 0; }
            set { Data = Data & 0xffffffbfffffffff | (value ? 0x400000000ul : 0ul); }
        }

        /// <summary>
        /// Texture function.
        /// </summary>
        public TextureFunction TFX
        {
            get { return (TextureFunction)(Data >> 35 & 0x3); }
            set { Data = Data & 0xffffffe7ffffffff | (ulong)value << 35; }
        }

        /// <summary>
        /// Clut buffer location. Multiply it by 0x100 to get the raw VRAM pointer.
        /// </summary>
        public ushort CBP
        {
            get { return (ushort)(Data >> 37 & 0x3fff); }
            set
            {
                if (value > 0x3fff) value = 0x3fff;
                Data = Data & 0xfff8001fffffffff | (ulong)value << 37;
            }
        }

        /// <summary>
        /// Clut storage format.
        /// </summary>
        public CLUTStorageFormat CPSM
        {
            get { return (CLUTStorageFormat)(Data >> 51 & 0xf); }
            set { Data = Data & 0xff87ffffffffffff | (ulong)value << 51; }
        }

        /// <summary>
        /// Clut Storage mode. True = Swizzled.
        /// </summary>
        public bool CSM
        {
            get { return (Data >> 55 & 0x1) != 0; }
            set { Data = Data & 0xff7fffffffffffff | (value ? 0x80000000000000ul : 0ul); }
        }

        /// <summary>
        /// Clut Entry Offset. Mostly used by 4-bit images.
        /// </summary>
        public byte CSA
        {
            get { return (byte)(Data >> 56 & 0x1f); }
            set
            {
                if (value > 0x1f) value = 0x1f;
                Data = Data & 0xe0ffffffffffffff | (ulong)value << 56;
            }
        }
        /// <summary>
        /// Load control. Purpose unknown.
        /// </summary>
        public byte CLD
        {
            get { return (byte)(Data >> 61 & 3); }
            set
            {
                if (value > 3) value = 3;
                Data = Data & 0x9fffffffffffffff | (ulong)value << 61;
            }
        }
    }
}