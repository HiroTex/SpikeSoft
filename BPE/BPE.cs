using System;
using System.Collections.Generic;
using System.IO;

namespace SpikeSoft.ZLib
{
    public class BPE
    {
        // EXTERNAL LIBRARY
        // modified by Luigi Auriemma
        // Adapted to work with Sparking! Binaries by ZeroDevs and HiroTex
        // bpe.c - rewritten to handle parameterised command line input

        #region Internal methods
        /* from compress.c */
        /* Copyright Philip Gage */
        /* printed in 'The C Users Journal' February, 1994 */
        //private static bool bpe_yuke = false;

        private static readonly int BLOCKSIZE = 20000;
        /* maximum block size */
        private static readonly int HASHSIZE = 65536 + 1;
        /* size of hash table */
        private static readonly int MAXCHARS = 220;
        /* char set per block */
        private static readonly int THRESHOLD = 3;
        /* minimum pair count */

        private static readonly byte[] buffer = new byte[BLOCKSIZE];
        /* data block */
        private static readonly byte[] leftcode = new byte[256];
        /* pair table */
        private static readonly byte[] rightcode = new byte[256];
        /* pair table */
        private static readonly byte[] stack = new byte[256];
        /* pair table */
        private static readonly byte[] left = new byte[HASHSIZE];
        /* hash table */
        private static readonly byte[] right = new byte[HASHSIZE];
        /* hash table */
        private static readonly byte[] count = new byte[HASHSIZE];
        /* pair count */
        private static int size;/* size of current data block */


        private static void reset()
        {
            Array.Clear(left, 0, HASHSIZE);
            Array.Clear(right, 0, HASHSIZE);
            Array.Clear(count, 0, HASHSIZE);

            Array.Clear(leftcode, 0, 256);
            Array.Clear(rightcode, 0, 256);

            Array.Clear(buffer, 0, BLOCKSIZE);
        }

        /* return index of character pair in hash table */
        /* deleted nodes have a count of 1 for hashing */
        private static int lookup(int a, int b, int hs)
        {
            int index;
            /* ?  - will add question marks until I understand each variable */


            /* compute hash key from both characters */
            index = (a ^ (b << 5)) & (hs - 1);
            /* ? */
            /* if b = 10110101 then '(b << 5)' --> b = 10100000. */
            /* ie shift the bits in b left by five positions and fill holes with zeros */

            /* search for pair or first empty slot */
            while ((((left[index] & 255) != a || (right[index] & 255) != b)) && count[index] != 0)
            {
                index = (index + 1);//& (hs - 1);
            }

            left[index] = (byte)a;
            right[index] = (byte)b;
            return index;
        }

        private static int fileread(byte[] input, int pos, int sz, int bs, int hs, int mc)
        {
            int c, index, used = 0;

            /* reset hash table and pair table */

            Array.Clear(count, 0, hs);
            Array.Clear(rightcode, 0, 256);

            for (c = 0; c < 256; c++)
            {
                leftcode[c] = (byte)c;
            }

            size = 0;

            /* read data until full or few unused chars */
            while (size < bs && used < mc && pos < sz)
            {
                c = input[pos++] & 255;

                if (size > 0)
                {
                    index = lookup(buffer[size - 1] & 255, c, hs);
                    int val = (count[index] & 255);
                    if (val < 255)
                    {
                        count[index] = (byte)(val + 1);
                    }
                }

                buffer[size++] = (byte)c;

                /* use right code to flag data chars found */
                if (rightcode[c] == 0)
                {
                    rightcode[c] = 1;
                    used++;
                }
            }

            return pos >= sz ? 1 : 0;
        }

        private static int filewrite(byte[] output, int pos, bool yuke)
        {
            int i, len, c = 0;

            /* for each character 0..255 */
            while (c < 256)
            {
                /* if not a pair code, count run of literals */
                if (c == (leftcode[c] & 255))
                {
                    len = 1;
                    c++;
                    while (len < 127 && c < 256 && c == (leftcode[c] & 255))
                    {
                        len++;
                        c++;
                    }
                    output[pos++] = (byte)(len + 127);
                    len = 0;
                    if (c == 256)
                    {
                        break;
                    }
                } /* else count run of pair codes */
                else
                {
                    len = 0;
                    c++;
                    
                    /* original, will add extra brackets per compiler suggestions:      while ( len < 127 && c < 256 && c != leftcode[c] || len < 125 && c < 254 && c+1 != leftcode[c+1]) */
                    while ((len < 127 && c < 256 && c != (leftcode[c] & 255)) || (len < 125
                            && c < 254 && c + 1 != (leftcode[c + 1] & 255)))
                    {
                        len++;
                        c++;
                    }

                    output[pos++] = (byte)len;

                    c -= len + 1;
                }

                /* write range of pairs to output */
                for (i = 0; i <= len; i++)
                {
                    output[pos++] = leftcode[c];
                    if (c != (leftcode[c] & 255))
                    {
                        output[pos++] = rightcode[c];
                    }
                    c++;
                }
            }

            /* write size bytes and compressed data block */
            output[pos++] = yuke ? (byte)(size % 256) : (byte)(size / 256);
            output[pos++] = yuke ? (byte)(size / 256) : (byte)(size % 256);

            Array.Copy(buffer, 0, output, pos, size);
            return pos + size;
        }

        private static int xgetc(byte[] in0, int pi, int inl)
        {
            if (pi >= inl)
            {
                return -1;
            }
            return in0[pi] & 255;
        }
        #endregion

        #region Algorithm Methods
        private byte[] compress(byte[] infile, int inOff, int zs, byte[] outfile, int outOff, int outsz, int bs, int hs, int mc, int th, bool yuke)
        {
            reset();
            zs += inOff;
            int leftch = 0, rightch = 0, code, oldsize;
            int index, r, w, best, done = 0;
            int ala = 0, opz = 0;

            /* compress each data block until end of file */
            while (done == 0)
            {
                done = fileread(infile, inOff + ala, zs, bs, hs, mc);
                ala += size;
                code = 256;
                /* compress this block */
                for (;;)
                {
                    /* get next unused chr for pair code */
                    for (code--; code >= 0; code--)
                    {
                        if (code == (leftcode[code] & 255) && rightcode[code] == 0)
                        {
                            break;
                        }
                    }

                    /* must quit if no unused chars left */
                    if (code < 0)
                    {
                        break;
                    }

                    /* find most frequent pair of chars */
                    for (best = 2, index = 0; index < hs; index++)
                    {
                        if ((count[index] & 255) > best)
                        {
                            best = count[index] & 255;
                            leftch = left[index] & 255;
                            rightch = right[index] & 255;
                        }
                    }

                    /* done if no more compression possible */
                    if (best < th)
                    {
                        break;
                    }

                    /* Replace pairs in data, adjust pair counts */
                    oldsize = size - 1;
                    for (w = 0, r = 0; r < oldsize; r++)
                    {
                        if ((buffer[r] & 255) == leftch && (buffer[r + 1] & 255) == rightch)
                        {
                            if (r > 0)
                            {
                                index = lookup(buffer[w - 1] & 255, leftch, hs);
                                if ((count[index] & 255) > 1)
                                {
                                    count[index] = (byte)((count[index] & 255) - 1);
                                }
                                index = lookup(buffer[w - 1] & 255, code, hs);
                                if ((count[index] & 255) < 255)
                                {
                                    count[index] = (byte)((count[index] & 255) + 1);
                                }
                            }
                            if (r < oldsize - 1)
                            {
                                index = lookup(rightch, buffer[r + 2] & 255, hs);
                                if ((count[index] & 255) > 1)
                                {
                                    count[index] = (byte)((count[index] & 255) - 1);
                                }
                                index = lookup(code, buffer[r + 2] & 255, hs);
                                if ((count[index] & 255) < 255)
                                {
                                    count[index] = (byte)((count[index] & 255) + 1);
                                }
                            }

                            buffer[w++] = (byte)code;
                            r++;
                            size--;
                        }
                        else
                        {
                            buffer[w++] = buffer[r];
                        }
                    }

                    buffer[w] = buffer[r];

                    /* add to pair substitution table */
                    leftcode[code] = (byte)leftch;
                    rightcode[code] = (byte)rightch;

                    /* delete pair from hash table */
                    index = lookup(leftch, rightch, hs);
                    count[index] = 1;
                }

                opz = filewrite(outfile, outOff + opz, yuke) - outOff;
            }

            byte[] _outputf = new byte[opz];
            Array.Copy(outfile, _outputf, _outputf.Length);
            return _outputf;
        }
        private byte[] decompress(byte[] in0, int pi, int insz, byte[] out0, int po, int outsz, bool yuke)
        {
            reset();
            int c, count, i, size, n;
            int in1 = pi, inl = pi + insz, o = po, outl = po + outsz;

            /* unpack each block until end of file */
            while ((count = xgetc(in0, in1++, inl)) >= 0)
            {
                /* set left to itself as literal flag */
                for (i = 0; i < 256; i++)
                {
                    left[i] = (byte)i;
                }

                /* read pair table */
                for (c = 0; ;)
                {
                    /* skip range of literal bytes */
                    if (count > 127)
                    {
                        c += count - 127;
                        count = 0;
                    }
                    if (c >= 256)
                    {
                        break;
                    }

                    /* read pairs, skip right if literal */
                    for (i = 0; i <= count; i++, c++)
                    {
                        if ((n = xgetc(in0, in1++, inl)) < 0)
                        {
                            break;
                        }
                        if (c >= 256)
                        {
                            return null;
                        }
                        left[c] = (byte)n;
                        if (c != left[c])
                        {
                            if ((n = xgetc(in0, in1++, inl)) < 0)
                            {
                                break;
                            }
                            if (c >= 256)
                            {
                                return null;
                            }
                            right[c] = (byte)n;
                        }
                    }
                    if (c >= 256)
                    {
                        break;
                    }
                    count = xgetc(in0, in1++, inl);
                    if (count < 0)
                    {
                        break;
                    }
                }

                /* calculate packed data block size */
                if ((n = xgetc(in0, in1++, inl)) < 0)
                {
                    break;
                }
                size = yuke ? n : (n << 8);
                if ((n = xgetc(in0, in1++, inl)) < 0)
                {
                    break;
                }
                size |= yuke ? (n << 8) : n;

                /* unpack data block */
                for (i = 0; ;)
                {
                    /* pop byte from stack or read byte */
                    if (i != 0)
                    {
                        c = stack[--i];
                    }
                    else
                    {
                        if (size-- == 0)
                        {
                            break;
                        }
                        c = xgetc(in0, in1++, inl);

                        if (c < 0)
                        {
                            break;
                        }
                    }

                    /* output byte or push pair on stack */
                    if (c >= 256)
                    {
                        return null;
                    }
                    if (c == left[c])
                    {
                        //if(xputc(c, &o, outl) < 0) return(-1);
                        if (o >= outl)
                        {
                            return null;
                        }
                        out0[o++] = (byte)c;
                    }
                    else
                    {
                        if ((i + 2) > stack.Length)
                        {
                            return null;
                        }
                        if (c >= 256)
                        {
                            return null; // valid for both l&r
                        }
                        stack[i++] = right[c];
                        stack[i++] = left[c];
                    }
                }
            }

            return out0;
        }
        #endregion

        #region Public Access Methods
        /// <summary>
        /// Compress File with BPE Algorithm
        /// </summary>
        /// <param name="in0"></param>
        /// <returns></returns>
        public byte[] compress(byte[] in0)
        {
            int bs = 0x4e20, hs = 0x4000;
            int mc = MAXCHARS, th = THRESHOLD;
            bs = 0x2710;     // best value in my tests
            hs = 0x10000;     // best value in my tests

            byte[] compressed = compress(in0, 0, in0.Length, new byte[in0.Length], 0, in0.Length, bs, hs, mc, th, false);

            if (compressed == null || compressed.Length == 0)
            {
                return null;
            }

            byte[] result = new byte[compressed.Length + 8];
            byte[] osize = BitConverter.GetBytes(in0.Length);
            byte[] zsize = BitConverter.GetBytes(compressed.Length);
            if (SpikeSoft.UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(osize);
            if (SpikeSoft.UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(zsize);
            Array.Copy(osize, 0, result, 0, 0x4);
            Array.Copy(zsize, 0, result, 0x4, 0x4);
            Array.Copy(compressed, 0, result, 0x8, compressed.Length);

            if (result.Length % 32 != 0)
            {
                byte[] tmp = new byte[result.Length - (result.Length % 32) + 32];
                Array.Copy(result, tmp, result.Length);
                result = tmp;
            }

            return result;
        }
        public byte[] decompress(byte[] in0)
        {
            // UZ Size
            int outsz = SpikeSoft.UtilityManager.BinMan.GetBinaryData_Int32(in0, 0);

            // Z Size
            int insz = SpikeSoft.UtilityManager.BinMan.GetBinaryData_Int32(in0, 4);

            // UZ File
            byte[] out0 = new byte[outsz];

            // Erase 8 byte Header
            Array.Copy(in0, 8, in0, 0, in0.Length - 8);

            return decompress(in0, 0, insz, out0, 0, outsz, false);
        }
        #endregion
    }
}
