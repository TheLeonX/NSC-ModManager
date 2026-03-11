using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NSC_ModManager
{
    sealed class FastBytes
    {
        private byte[] _buf;
        public int Length { get; private set; }

        public FastBytes(int capacity = 4096) { _buf = new byte[capacity]; }

        private void Ensure(int extra)
        {
            int need = Length + extra;
            if (need <= _buf.Length) return;
            int newSize = _buf.Length * 2;
            if (newSize < need) newSize = need;
            Array.Resize(ref _buf, newSize);
        }

        public void WriteByte(byte b) { Ensure(1); _buf[Length++] = b; }
        public void Write(byte[] src) { if (src == null || src.Length == 0) return; Ensure(src.Length); Buffer.BlockCopy(src, 0, _buf, Length, src.Length); Length += src.Length; }
        public void Write(ReadOnlySpan<byte> src) { Ensure(src.Length); for (int i = 0; i < src.Length; i++) _buf[Length + i] = src[i]; Length += src.Length; }
        public void WriteZeros(int n) { Ensure(n); Array.Clear(_buf, Length, n); Length += n; }
        public void Align4() { int pad = (-Length) & 3; if (pad != 0) WriteZeros(pad); }

        public void WriteLE32(int v) { Write(BitConverter.GetBytes(v)); }
        public void WriteLE32(uint v) { Write(BitConverter.GetBytes(v)); }
        public void WriteBE32(int v) { var b = BitConverter.GetBytes(v); Array.Reverse(b); Write(b); }

        public void WriteAt(int offset, byte[] src) => Buffer.BlockCopy(src, 0, _buf, offset, src.Length);
        public void WriteLE32At(int offset, int v) => Buffer.BlockCopy(BitConverter.GetBytes(v), 0, _buf, offset, 4);
        public void WriteBE32At(int offset, int v) { var b = BitConverter.GetBytes(v); Array.Reverse(b); Buffer.BlockCopy(b, 0, _buf, offset, 4); }

        // NEW: 1-byte bool
        public void WriteBool8At(int offset, bool v) { _buf[offset] = v ? (byte)1 : (byte)0; }
        // NEW: 4-byte float LE
        public void WriteF32At(int offset, float f) => Buffer.BlockCopy(BitConverter.GetBytes(f), 0, _buf, offset, 4);

        // "string + NUL"
        public int WriteCStringUtf8(string s)
        {
            if (string.IsNullOrEmpty(s)) { WriteByte(0); return 1; }
            var bytes = Encoding.UTF8.GetBytes(s);
            Write(bytes);
            WriteByte(0);
            return bytes.Length + 1;
        }

        public byte[] ToArray()
        {
            var arr = new byte[Length];
            Buffer.BlockCopy(_buf, 0, arr, 0, Length);
            return arr;
        }
    }
    internal static class BinaryReader
    {
        // ---------- Tiny helpers ----------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlySpan<byte> Slice(byte[] src, int index, int count)
            => src.AsSpan(index, Math.Clamp(count, 0, src.Length - index));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureRange(byte[] src, int index, int count)
        {
            if ((uint)index > (uint)src.Length || (uint)count > (uint)(src.Length - index))
                throw new ArgumentOutOfRangeException();
        }

        // ---------- Fast readers / converters ----------
        public static byte[] b_ReadByteArray(byte[] actual, int index, int count)
        {
            EnsureRange(actual, index, count);
            return actual.AsSpan(index, count).ToArray();
        }

        // Reads bytes until NUL (or maxLen), starting at index
        public static byte[] b_ReadByteArrayOfString(byte[] actual, int index)
        {
            const int maxLen = 65535;
            EnsureRange(actual, index, actual.Length - index);
            var span = actual.AsSpan(index, Math.Min(maxLen, actual.Length - index));
            int nul = span.IndexOf((byte)0);
            if (nul < 0) nul = span.Length;
            return span.Slice(0, nul).ToArray();
        }

        public static int b_byteArrayToInt(byte[] actual) => BinaryPrimitives.ReadInt32LittleEndian(actual);
        public static int b_byteArrayToUIntTwoBytes(byte[] a) => BinaryPrimitives.ReadUInt16LittleEndian(a);
        public static short b_byteArrayToInt16(byte[] actual) => BinaryPrimitives.ReadInt16LittleEndian(actual);
        public static ushort b_byteArrayToUInt16(byte[] actual) => BinaryPrimitives.ReadUInt16LittleEndian(actual);
        public static uint b_byteArrayToUInt32(byte[] actual) => BinaryPrimitives.ReadUInt32LittleEndian(actual);
        public static int b_byteArrayToInt32(byte[] actual) => BinaryPrimitives.ReadInt32LittleEndian(actual);

        public static int b_byteArrayToIntRevTwoBytes(byte[] a) => BinaryPrimitives.ReadUInt16BigEndian(a);
        public static int b_byteArrayToIntRev(byte[] a) => BinaryPrimitives.ReadInt32BigEndian(a);

        public static int b_ReadInt(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 4);
            return BinaryPrimitives.ReadInt32LittleEndian(fileBytes.AsSpan(index));
        }
        public static short b_ReadInt16(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 2);
            return BinaryPrimitives.ReadInt16LittleEndian(fileBytes.AsSpan(index));
        }
        public static ushort b_ReadUInt16(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 2);
            return BinaryPrimitives.ReadUInt16LittleEndian(fileBytes.AsSpan(index));
        }
        public static uint b_ReadUInt32(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 4);
            return BinaryPrimitives.ReadUInt32LittleEndian(fileBytes.AsSpan(index));
        }
        public static int b_ReadInt32(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 4);
            return BinaryPrimitives.ReadInt32LittleEndian(fileBytes.AsSpan(index));
        }
        public static int b_ReadIntFromTwoBytes(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 2);
            return BinaryPrimitives.ReadUInt16LittleEndian(fileBytes.AsSpan(index));
        }
        public static int b_ReadIntRevFromTwoBytes(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 2);
            return BinaryPrimitives.ReadUInt16BigEndian(fileBytes.AsSpan(index));
        }
        public static int b_ReadIntRev(byte[] fileBytes, int index)
        {
            EnsureRange(fileBytes, index, 4);
            return BinaryPrimitives.ReadInt32BigEndian(fileBytes.AsSpan(index));
        }

        public static bool b_ReadBool(byte[] fileBytes, int index) => b_ReadInt(fileBytes, index) != 0;
        public static bool b_ReadBool16(byte[] fileBytes, int index) => b_ReadInt16(fileBytes, index) != 0;

        public static float b_ReadFloat(byte[] actual, int index)
        {
            EnsureRange(actual, index, 4);
            return BitConverter.ToSingle(actual, index);
        }

        // Interpret 4 bytes at index as big-endian IEEE754 float
        public static float b_ReadFloatRev(byte[] actual, int index)
        {
            EnsureRange(actual, index, 4);
            uint v = BinaryPrimitives.ReadUInt32BigEndian(actual.AsSpan(index));
            return BitConverter.UInt32BitsToSingle(v);
        }

        // ---------- Strings ----------
        // Fast ASCII/Latin-1 null-terminated or fixed-length read
        public static string b_ReadString(byte[] actual, int index, int count = -1)
        {
            if (count < 0)
            {
                EnsureRange(actual, index, actual.Length - index);
                var span = actual.AsSpan(index);
                int nul = span.IndexOf((byte)0);
                if (nul < 0) nul = span.Length;
                return Encoding.ASCII.GetString(span.Slice(0, nul));
            }
            EnsureRange(actual, index, count);
            return Encoding.ASCII.GetString(actual, index, count);
        }

        // Same as above but skips a specific byte value; stops at NUL when count < 0
        public static string b_ReadString3(byte[] actual, int index, int count = -1, int skip = 0)
        {
            ReadOnlySpan<byte> src;
            if (count < 0)
            {
                var rest = actual.AsSpan(index);
                int nul = rest.IndexOf((byte)0);
                if (nul < 0) nul = rest.Length;
                src = rest.Slice(0, nul);
            } else
            {
                EnsureRange(actual, index, count);
                src = actual.AsSpan(index, count);
            }

            // Filter without building intermediate strings
            var tmp = new char[src.Length];
            int w = 0;
            for (int i = 0; i < src.Length; i++)
                if (src[i] != (byte)skip)
                    tmp[w++] = (char)src[i];

            return new string(tmp, 0, w);
        }

        // Pointer-relative string: *(int32) at index is offset from index to the string start
        public static string b_ReadStringPtr(byte[] actual, int index, int count = -1)
        {
            EnsureRange(actual, index, 4);
            int offset = BinaryPrimitives.ReadInt32LittleEndian(actual.AsSpan(index));
            int start = index + offset;
            if (count < 0)
            {
                var rest = actual.AsSpan(start);
                int nul = rest.IndexOf((byte)0);
                if (nul < 0) nul = rest.Length;
                return Encoding.ASCII.GetString(rest.Slice(0, nul));
            }
            EnsureRange(actual, start, count);
            return Encoding.ASCII.GetString(actual, start, count);
        }

        // ---------- In-place replace ----------
        public static byte[] b_ReplaceBytes(byte[] actual, byte[] bytesToReplace, int Index, int Invert = 0, int count = -1)
        {
            int length = (count == -1) ? bytesToReplace.Length : count;
            if (bytesToReplace is null) return actual;
            EnsureRange(actual, Index, length);

            if (Invert == 0)
            {
                Buffer.BlockCopy(bytesToReplace, 0, actual, Index, length);
            } else
            {
                // Keep old semantics: Reverse().Skip(IndexFromEnd).Take(len)
                int len = length;
                for (int i = 0; i < len; i++)
                    actual[Index + i] = bytesToReplace[len - 1 - i];
            }
            return actual;
        }

        public static byte[] b_ReplaceString(byte[] actual, string str, int Index, int Count = -1)
        {
            var s = str ?? string.Empty;
            if (Count < 0)
            {
                EnsureRange(actual, Index, s.Length);
                var src = Encoding.ASCII.GetBytes(s);
                Buffer.BlockCopy(src, 0, actual, Index, src.Length);
            } else
            {
                EnsureRange(actual, Index, Count);
                var dest = actual.AsSpan(Index, Count);
                var src = Encoding.ASCII.GetBytes(s);
                int n = Math.Min(dest.Length, src.Length);
                src.AsSpan(0, n).CopyTo(dest);
                if (n < dest.Length) dest.Slice(n).Clear(); // zero-pad
            }
            return actual;
        }

        public static string b_ReplaceRealString(string actual, string str, int Index, int Count = -1)
        {
            var s = str ?? string.Empty;
            var chars = actual.ToCharArray();
            if (Count < 0)
            {
                Array.Copy(s.ToCharArray(), 0, chars, Index, s.Length);
            } else
            {
                int n = Math.Min(Count, s.Length);
                s.AsSpan(0, n).CopyTo(chars.AsSpan(Index, n));
                for (int i = n; i < Count; i++) chars[Index + i] = '\0';
            }
            return new string(chars);
        }

        // ---------- Append ----------
        public static byte[] b_AddBytes(byte[] actual, byte[] bytesToAdd, int Reverse = 0, int index = 0, int count = -1)
        {
            if (bytesToAdd == null || bytesToAdd.Length == 0) return actual;

            index = Math.Clamp(index, 0, bytesToAdd.Length);
            int available = bytesToAdd.Length - index;
            count = (count < 0 || count > available) ? available : count;

            var dst = new byte[actual.Length + count];
            Buffer.BlockCopy(actual, 0, dst, 0, actual.Length);

            if (Reverse == 0)
            {
                Buffer.BlockCopy(bytesToAdd, index, dst, actual.Length, count);
            } else
            {
                // Preserve prior semantics: Reverse().Skip(index).Take(count)
                // => copy from the source tail backward with an additional offset of 'index' from the end
                int srcStartFromEnd = index;
                for (int i = 0; i < count; i++)
                    dst[actual.Length + i] = bytesToAdd[bytesToAdd.Length - 1 - (srcStartFromEnd + i)];
            }
            return dst;
        }

        public static byte[] b_AddInt(byte[] actual, int _num)
        {
            var dst = new byte[actual.Length + 4];
            Buffer.BlockCopy(actual, 0, dst, 0, actual.Length);
            BinaryPrimitives.WriteInt32LittleEndian(dst.AsSpan(actual.Length, 4), _num);
            return dst;
        }

        public static byte[] b_AddFloat(byte[] actual, float f)
        {
            var dst = new byte[actual.Length + 4];
            Buffer.BlockCopy(actual, 0, dst, 0, actual.Length);
            var u = BitConverter.SingleToUInt32Bits(f);
            BinaryPrimitives.WriteUInt32LittleEndian(dst.AsSpan(actual.Length, 4), u);
            return dst;
        }

        public static byte[] b_AddString(byte[] actual, string _str, int count = -1)
        {
            if (_str is null) return actual;

            var src = Encoding.UTF8.GetBytes(_str);
            if (count > 0)
            {
                // append with padding to fixed count
                int n = Math.Min(count, src.Length);
                var dst = new byte[actual.Length + count];
                Buffer.BlockCopy(actual, 0, dst, 0, actual.Length);
                Buffer.BlockCopy(src, 0, dst, actual.Length, n);
                // remaining bytes are already zero
                return dst;
            } else
            {
                var dst = new byte[actual.Length + src.Length];
                Buffer.BlockCopy(actual, 0, dst, 0, actual.Length);
                Buffer.BlockCopy(src, 0, dst, actual.Length, src.Length);
                return dst;
            }
        }

        public static byte[] b_StringToBytes(string hexstr)
        {
            if (string.IsNullOrWhiteSpace(hexstr))
                throw new ArgumentNullException(nameof(hexstr), "Wrong Format");

            // strip spaces without allocations
            Span<char> buf = stackalloc char[hexstr.Length];
            int w = 0;
            foreach (var ch in hexstr)
                if (ch != ' ') buf[w++] = ch;

            if ((w & 1) != 0) throw new ArgumentException("Wrong Format");

            var arr = new byte[w / 2];
            for (int i = 0, j = 0; j < w; i++, j += 2)
            {
                int hi = HexToNibble(buf[j]);
                int lo = HexToNibble(buf[j + 1]);
                if ((hi | lo) < 0) throw new ArgumentException("Wrong Format");
                arr[i] = (byte)((hi << 4) | lo);
            }
            return arr;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int HexToNibble(int c)
            {
                if ((uint)(c - '0') <= 9) return c - '0';
                c |= 0x20; // to lower
                if ((uint)(c - 'a') <= 5) return c - 'a' + 10;
                return -1;
            }
        }

        // ---------- Search ----------
        public static int b_FindBytes(byte[] actual, byte[] bytes, int index = 0)
        {
            if (bytes == null || bytes.Length == 0) return -1;
            EnsureRange(actual, index, actual.Length - index);
            int pos = actual.AsSpan(index).IndexOf(bytes);
            return pos >= 0 ? index + pos : -1;
        }

        public static bool b_FindBytesBool(byte[] actual, byte[] bytes, int index = 0)
            => b_FindBytes(actual, bytes, index) >= 0;

        public static int b_FindString(string actual, string str, int index = 0)
        {
            if (str is null) return -1;
            return actual.IndexOf(str, index, StringComparison.Ordinal);
        }

        public static List<int> b_FindBytesList(byte[] actual, byte[] bytes, int index = 0)
        {
            var result = new List<int>();
            if (bytes == null || bytes.Length == 0) return result;
            int start = index;
            var span = actual.AsSpan();
            while (start <= actual.Length - bytes.Length)
            {
                int pos = span.Slice(start).IndexOf(bytes);
                if (pos < 0) break;
                start += pos;
                result.Add(start);
                start++; // next possible position (overlapping allowed)
            }
            return result;
        }

        // ---------- CRC32 (MSB-first, polynomial 0x04C11DB7) ----------
        private static readonly uint[] CRC_TABLE = BuildCrcTable();

        private static uint[] BuildCrcTable()
        {
            var t = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint k = (uint)i << 24;
                for (int b = 0; b < 8; b++)
                {
                    k = (k & 0x80000000) != 0 ? (k << 1) ^ 0x04C11DB7u : (k << 1);
                }
                t[i] = k;
            }
            return t;
        }

        public static List<long> crc32_table()
        {
            // Maintain old API (long) but reuse cached table
            var list = new List<long>(256);
            foreach (var v in CRC_TABLE) list.Add(v & 0xFFFFFFFFL);
            return list;
        }

        public static byte[] crc32(string str)
        {
            if (str == null) return new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };

            var data = Encoding.ASCII.GetBytes(str);
            uint crc = 0xFFFFFFFFu;
            for (int i = 0; i < data.Length; i++)
            {
                uint idx = ((crc >> 24) ^ data[i]) & 0xFFu;
                crc = ((crc & 0xFFFFFFu) << 8) ^ CRC_TABLE[idx];
            }
            crc = ~crc;

            // Keep platform endianness (your original returned BitConverter.GetBytes)
            return BitConverter.GetBytes(crc);
        }
        public static byte[] MakeXfbinBinary(string path, string name, byte[] file)
        {

            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x63, 0x40, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, path);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, name);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "Page0");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "index");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrName = fileBytes36.Length;
            totalLength4 = PtrName;
            int AddedBytes = 0;

            while (fileBytes36.Length % 4 != 0)
            {
                AddedBytes++;
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

            // Build bin1
            totalLength4 = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[48]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03
            });

            int PtrSection = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[16]
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                0,
                0,
                0,
                2,
                0,
                0,
                0,
                3
            });

            totalLength4 = fileBytes36.Length;

            int PathLength = PtrPath - 127;
            int NameLength = PtrName - PtrPath;
            int Section1Length = PtrSection - PtrName - AddedBytes;
            int FullLength = totalLength4 - 68 + 40;
            int ReplaceIndex8 = 16;
            byte[] buffer8 = BitConverter.GetBytes(FullLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 36;
            buffer8 = BitConverter.GetBytes(2);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 40;
            buffer8 = BitConverter.GetBytes(PathLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 44;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 48;
            buffer8 = BitConverter.GetBytes(NameLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 52;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 56;
            buffer8 = BitConverter.GetBytes(Section1Length);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 60;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[40]
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x77,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x77,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0x77,0x00,0x00,0x00,0x00,0x00
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, file);

            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(file.Length + 4), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(file.Length), size2_index, 1);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0,
                0,
                0,
                8,
                0,
                0,
                0,
                2,
                0,
                99,
                0,
                0,
                0,
                0,
                0,
                4,
                0,
                0,
                0,
                0
           });
        }

    }
    public static class BinaryReaderV2
    {
        public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadUInt32BigEndian(buffer.Slice(offset, 4));
        }
        public static int ReadInt32BigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadInt32BigEndian(buffer.Slice(offset, 4));
        }
        public static int ReadInt32LittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(offset, 4));
        }
        public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(offset, 4));
        }
        public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(offset, 2));
        }
        public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(offset, 2));
        }

        public static short ReadInt16BigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadInt16BigEndian(buffer.Slice(offset, 2));
        }
        public static short ReadInt16LittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            return BinaryPrimitives.ReadInt16LittleEndian(buffer.Slice(offset, 2));
        }
        public static byte ReadUInt8(byte[] data, int offset)
        {
            return data[offset];
        }

        public static sbyte ReadInt8(byte[] data, int offset)
        {
            return unchecked((sbyte)data[offset]);
        }
        public static string ReadFixedString(ReadOnlySpan<byte> buffer, int offset, int length, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;
            if (offset < 0 || length < 0 || offset + length > buffer.Length) throw new ArgumentOutOfRangeException();
            return encoding.GetString(buffer.Slice(offset, length));
        }

        public static string ReadNullTerminatedString(ReadOnlySpan<byte> buffer, int offset, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException();
            int len = 0;
            while (offset + len < buffer.Length && buffer[offset + len] != 0) len++;
            return encoding.GetString(buffer.Slice(offset, len));
        }

        public static byte[] ReadBytes(ReadOnlySpan<byte> buffer, int offset, int size)
        {
            if (offset < 0 || size < 0 || offset + size > buffer.Length) throw new ArgumentOutOfRangeException();
            byte[] result = new byte[size];
            buffer.Slice(offset, size).CopyTo(result);
            return result;
        }

        public static bool MatchesAscii(ReadOnlySpan<byte> buffer, int offset, string ascii)
        {
            if (offset < 0 || offset + ascii.Length > buffer.Length) return false;
            for (int i = 0; i < ascii.Length; i++)
                if (buffer[offset + i] != (byte)ascii[i]) return false;
            return true;
        }

        public static float ReadSingleLittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            int bits = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(offset, 4));
            return BitConverter.Int32BitsToSingle(bits);
        }

        public static float ReadSingleBigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            int bits = BinaryPrimitives.ReadInt32BigEndian(buffer.Slice(offset, 4));
            return BitConverter.Int32BitsToSingle(bits);
        }

        public static float ReadHalfLittleEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            ushort half = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(offset, 2));
            return HalfToSingle(half);
        }

        public static float ReadHalfBigEndian(ReadOnlySpan<byte> buffer, int offset)
        {
            ushort half = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(offset, 2));
            return HalfToSingle(half);
        }

        // IEEE-754 binary16 -> IEEE-754 binary32 conversion
        private static float HalfToSingle(ushort half)
        {
            int sign = (half >> 15) & 0x00000001;
            int exp = (half >> 10) & 0x0000001F;
            int mant = half & 0x03FF;

            if (exp == 0)
            {
                if (mant == 0)
                {
                    // +/- 0
                    int bits = sign << 31;
                    return BitConverter.Int32BitsToSingle(bits);
                } else
                {
                    // subnormal -> normalize
                    while ((mant & 0x400) == 0)
                    {
                        mant <<= 1;
                        exp -= 1;
                    }
                    // remove leading 1
                    mant &= 0x3FF;
                    exp += 1;
                }
            } else if (exp == 31)
            {
                // Inf or NaN
                int bits = (sign << 31) | 0x7F800000 | (mant << 13);
                return BitConverter.Int32BitsToSingle(bits);
            }

            exp = exp + (127 - 15);
            int resultBits = (sign << 31) | (exp << 23) | (mant << 13);
            return BitConverter.Int32BitsToSingle(resultBits);
        }

        public static bool IsFlagBitSet(int flag, int bitIndex)
        {
            if (bitIndex < 1 || bitIndex > 8) return false;
            return (((flag >> (8 - bitIndex)) & 1) != 0);
        }
        public static int FindString(byte[] data, string text, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;

            byte[] pattern = encoding.GetBytes(text);
            int limit = data.Length - pattern.Length;

            for (int i = 0; i <= limit; i++)
            {
                int j = 0;
                for (; j < pattern.Length; j++)
                {
                    if (data[i + j] != pattern[j])
                        break;
                }
                if (j == pattern.Length)
                    return i;
            }

            return -1; // not found
        }
    }
    public static class BinaryWriterV2
    {
        public static void WriteUInt32BigEndian(Span<byte> buffer, int offset, uint value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteUInt32BigEndian(buffer.Slice(offset, 4), value);
        }
        public static void WriteInt32BigEndian(Span<byte> buffer, int offset, int value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteInt32BigEndian(buffer.Slice(offset, 4), value);
        }
        public static void WriteInt32LittleEndian(Span<byte> buffer, int offset, int value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(offset, 4), value);
        }
        public static void WriteUInt32LittleEndian(Span<byte> buffer, int offset, uint value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteUInt32LittleEndian(buffer.Slice(offset, 4), value);
        }
        public static void WriteUInt16BigEndian(Span<byte> buffer, int offset, ushort value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteUInt16BigEndian(buffer.Slice(offset, 2), value);
        }
        public static void WriteUInt16LittleEndian(Span<byte> buffer, int offset, ushort value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteUInt16LittleEndian(buffer.Slice(offset, 2), value);
        }
        public static void WriteInt16BigEndian(Span<byte> buffer, int offset, short value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteInt16BigEndian(buffer.Slice(offset, 2), value);
        }
        public static void WriteInt16LittleEndian(Span<byte> buffer, int offset, short value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            BinaryPrimitives.WriteInt16LittleEndian(buffer.Slice(offset, 2), value);
        }
        public static void WriteUInt8(Span<byte> buffer, int offset, byte value)
        {
            if (offset < 0 || offset + 1 > buffer.Length) throw new ArgumentOutOfRangeException();
            buffer[offset] = value;
        }

        public static void WriteInt8(Span<byte> buffer, int offset, sbyte value)
        {
            if (offset < 0 || offset + 1 > buffer.Length) throw new ArgumentOutOfRangeException();
            buffer[offset] = unchecked((byte)value);
        }

        public static void WriteFixedString(Span<byte> buffer, int offset, int length, string value, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;
            if (offset < 0 || length < 0 || offset + length > buffer.Length) throw new ArgumentOutOfRangeException();
            byte[] bytes = encoding.GetBytes(value ?? string.Empty);
            int toCopy = Math.Min(length, bytes.Length);
            bytes.AsSpan(0, toCopy).CopyTo(buffer.Slice(offset, length));
            if (toCopy < length)
                buffer.Slice(offset + toCopy, length - toCopy).Clear();
        }

        public static void WriteNullTerminatedString(Span<byte> buffer, int offset, string value, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException();
            byte[] bytes = encoding.GetBytes(value ?? string.Empty);
            if (offset + bytes.Length + 1 > buffer.Length) throw new ArgumentOutOfRangeException();
            bytes.AsSpan().CopyTo(buffer.Slice(offset, bytes.Length));
            buffer[offset + bytes.Length] = 0;
        }

        public static void WriteBytes(Span<byte> buffer, int offset, ReadOnlySpan<byte> data)
        {
            if (offset < 0 || offset + data.Length > buffer.Length) throw new ArgumentOutOfRangeException();
            data.CopyTo(buffer.Slice(offset, data.Length));
        }

        public static void WriteSingleLittleEndian(Span<byte> buffer, int offset, float value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            int bits = BitConverter.SingleToInt32Bits(value);
            BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(offset, 4), bits);
        }

        public static void WriteSingleBigEndian(Span<byte> buffer, int offset, float value)
        {
            if (offset < 0 || offset + 4 > buffer.Length) throw new ArgumentOutOfRangeException();
            int bits = BitConverter.SingleToInt32Bits(value);
            BinaryPrimitives.WriteInt32BigEndian(buffer.Slice(offset, 4), bits);
        }

        public static void WriteHalfLittleEndian(Span<byte> buffer, int offset, float value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            ushort half = SingleToHalf(value);
            BinaryPrimitives.WriteUInt16LittleEndian(buffer.Slice(offset, 2), half);
        }

        public static void WriteHalfBigEndian(Span<byte> buffer, int offset, float value)
        {
            if (offset < 0 || offset + 2 > buffer.Length) throw new ArgumentOutOfRangeException();
            ushort half = SingleToHalf(value);
            BinaryPrimitives.WriteUInt16BigEndian(buffer.Slice(offset, 2), half);
        }

        // IEEE-754 binary32 -> IEEE-754 binary16 conversion
        private static ushort SingleToHalf(float f)
        {
            uint fbits = (uint)BitConverter.SingleToInt32Bits(f);
            uint sign = (fbits >> 16) & 0x8000u;
            uint val = fbits & 0x7FFFFFFFu;

            // NaN or Inf
            if (val >= 0x7F800000u)
            {
                // Infinity
                if ((fbits & 0x007FFFFFu) == 0)
                {
                    return (ushort)(sign | 0x7C00u);
                }
                // NaN - preserve some mantissa, ensure quiet NaN
                uint mant = (fbits & 0x007FFFFFu) >> 13;
                if (mant == 0) mant = 1;
                return (ushort)(sign | 0x7C00u | mant);
            }

            // Overflow - value too large for half -> set to max half (Infinity handling above)
            if (val > 0x477FE000u)
            {
                return (ushort)(sign | 0x7BFFu); // max half value (not-inf)
            }

            // Underflow - value too small for normalized half
            if (val < 0x33000000u)
            {
                // may become zero or subnormal
                // round to zero
                return (ushort)sign;
            }

            uint exp = (fbits >> 23) & 0xFFu;
            int newexp = (int)exp - 127 + 15;

            if (newexp >= 31)
            {
                // overflow to max
                return (ushort)(sign | 0x7BFFu);
            } else if (newexp <= 0)
            {
                // subnormal
                uint mant = (fbits & 0x007FFFFFu) | 0x00800000u; // add implicit 1
                int shift = 14 - newexp;
                uint halfMant = mant >> shift;
                // round
                uint roundBit = 1u << (shift - 1);
                if ((mant & roundBit) != 0) halfMant += 1;
                return (ushort)(sign | halfMant);
            } else
            {
                uint half = (uint)newexp << 10;
                half |= (fbits & 0x007FFFFFu) >> 13;
                // round to nearest
                if ((fbits & 0x00001000u) != 0) half += 1;
                return (ushort)(sign | half);
            }
        }
        /// <summary>
        /// Вставляет bytesToAdd в fileBytes по указанному offset, не перезаписывая существующие байты (сдвигает их вправо).
        /// Возвращает новый массив.
        /// offset может быть от 0 до fileBytes.Length (включительно).
        /// </summary>
        public static byte[] InsertBytes(byte[] fileBytes, byte[] bytesToAdd, int offset = -1)
        {
            if (fileBytes == null) throw new ArgumentNullException(nameof(fileBytes));
            if (bytesToAdd == null || bytesToAdd.Length == 0) return fileBytes;

            if (offset == -1) offset = fileBytes.Length;
            if (offset < 0 || offset > fileBytes.Length) throw new ArgumentOutOfRangeException(nameof(offset));

            int resultLength = fileBytes.Length + bytesToAdd.Length;
            byte[] result = new byte[resultLength];

            if (offset > 0)
                Buffer.BlockCopy(fileBytes, 0, result, 0, offset);

            Buffer.BlockCopy(bytesToAdd, 0, result, offset, bytesToAdd.Length);

            int suffixLen = fileBytes.Length - offset;
            if (suffixLen > 0)
                Buffer.BlockCopy(fileBytes, offset, result, offset + bytesToAdd.Length, suffixLen);

            return result;
        }

        /// <summary>
        /// Заменяет байты в fileBytes начиная с offset массивом bytesToAdd.
        /// Если bytesToAdd выходит за конец исходного массива, возвращает новый массив, расширенный до (offset + bytesToAdd.Length).
        /// Если offset > fileBytes.Length — промежуток между концом исходного массива и offset будет заполнен нулями.
        /// </summary>
        public static byte[] ReplaceBytes(byte[] fileBytes, byte[] bytesToAdd, int offset)
        {
            if (fileBytes == null) throw new ArgumentNullException(nameof(fileBytes));
            if (bytesToAdd == null || bytesToAdd.Length == 0) return fileBytes;
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

            int requiredLength = Math.Max(fileBytes.Length, offset + bytesToAdd.Length);
            byte[] result = new byte[requiredLength];

            // copy prefix (all bytes up to offset or full file if offset > length)
            int prefixLen = Math.Min(fileBytes.Length, offset);
            if (prefixLen > 0)
                Buffer.BlockCopy(fileBytes, 0, result, 0, prefixLen);

            // copy new bytes at offset
            Buffer.BlockCopy(bytesToAdd, 0, result, offset, bytesToAdd.Length);

            // copy remaining suffix from original file (the part after overwritten region), if any
            int suffixSrcIndex = offset + bytesToAdd.Length;
            if (suffixSrcIndex < fileBytes.Length)
            {
                int suffixLen = fileBytes.Length - suffixSrcIndex;
                Buffer.BlockCopy(fileBytes, suffixSrcIndex, result, offset + bytesToAdd.Length, suffixLen);
            }

            return result;
        }

        public static void SetFlagBit<T>(ref T value, int bitIndex, bool state) where T : struct
        {
            if (typeof(T) == typeof(byte))
            {
                const int bits = 8;
                if (bitIndex < 1 || bitIndex > bits) return;
                int shift = bits - bitIndex; // MSB = 1
                byte mask = (byte)(1 << shift);
                byte v = (byte)(object)value;
                if (state) v |= mask; else v &= (byte)~mask;
                value = (T)(object)v;
                return;
            }

            if (typeof(T) == typeof(int))
            {
                const int bits = 32;
                if (bitIndex < 1 || bitIndex > bits) return;
                int shift = bits - bitIndex; // MSB = 1
                uint mask = 1u << shift;
                int v = (int)(object)value;
                uint uv = (uint)v;
                if (state) uv |= mask; else uv &= ~mask;
                value = (T)(object)(int)uv;
                return;
            }

            throw new NotSupportedException("Only byte and int are supported.");
        }
    }

    public class ByteBuilder
    {
        private byte[] _buffer;
        public int Length { get; private set; }

        public ByteBuilder(int initialCapacity = 256)
        {
            _buffer = new byte[Math.Max(1, initialCapacity)];
            Length = 0;
        }

        private void EnsureCapacity(int required)
        {
            if (required <= _buffer.Length) return;
            int newCap = _buffer.Length;
            while (newCap < required) newCap = newCap * 2;
            var nb = new byte[newCap];
            Buffer.BlockCopy(_buffer, 0, nb, 0, Length);
            _buffer = nb;
        }

        /// <summary>Добавляет count нулевых байт, возвращает offset (начало добавления).</summary>
        public int AppendZeros(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            int off = Length;
            EnsureCapacity(Length + count);
            Length += count;
            return off;
        }

        /// <summary>Добавляет данные, возвращает offset (начало добавления).</summary>
        public int AppendBytes(ReadOnlySpan<byte> data)
        {
            int off = Length;
            EnsureCapacity(Length + data.Length);
            data.CopyTo(_buffer.AsSpan(Length));
            Length += data.Length;
            return off;
        }

        // --- low-level write-at helpers (use BinaryWriterV2 Span methods) ---
        public void WriteFixedStringAt(int offset, int length, string value, Encoding? encoding = null)
        {
            BinaryWriterV2.WriteFixedString(_buffer.AsSpan(), offset, length, value, encoding);
        }

        public void WriteInt32LittleEndianAt(int offset, int value)
        {
            BinaryWriterV2.WriteInt32LittleEndian(_buffer.AsSpan(), offset, value);
        }
        public void WriteUInt32LittleEndianAt(int offset, uint value)
        {
            BinaryWriterV2.WriteUInt32LittleEndian(_buffer.AsSpan(), offset, value);
        }
        public void WriteUInt32BigEndianAt(int offset, uint value)
        {
            BinaryWriterV2.WriteUInt32BigEndian(_buffer.AsSpan(), offset, value);
        }
        public void WriteInt32BigEndianAt(int offset, int value)
        {
            BinaryWriterV2.WriteInt32BigEndian(_buffer.AsSpan(), offset, value);
        }
        public void WriteUInt16BigEndianAt(int offset, ushort value)
        {
            BinaryWriterV2.WriteUInt16BigEndian(_buffer.AsSpan(), offset, value);
        }

        public void WriteBytesAt(int offset, ReadOnlySpan<byte> data)
        {
            BinaryWriterV2.WriteBytes(_buffer.AsSpan(), offset, data);
        }

        // --- high-level append-or-write API ---

        // Write Int32 LE: append (returns offset) or write at offset
        public int WriteInt32LittleEndian(int value)
        {
            int off = AppendZeros(4);
            WriteInt32LittleEndianAt(off, value);
            return off;
        }
        public void WriteInt32LittleEndian(int offset, int value) => WriteInt32LittleEndianAt(offset, value);

        // Write UInt32 BE
        public int WriteUInt32BigEndian(uint value)
        {
            int off = AppendZeros(4);
            WriteUInt32BigEndianAt(off, value);
            return off;
        }
        public void WriteUInt32BigEndian(int offset, uint value) => WriteUInt32BigEndianAt(offset, value);

        // Write UInt16 BE
        public int WriteUInt16BigEndian(ushort value)
        {
            int off = AppendZeros(2);
            WriteUInt16BigEndianAt(off, value);
            return off;
        }
        public void WriteUInt16BigEndian(int offset, ushort value) => WriteUInt16BigEndianAt(offset, value);

        // Write bytes (append or write at offset)
        public int WriteBytes(ReadOnlySpan<byte> data)
        {
            return AppendBytes(data);
        }
        public void WriteBytes(int offset, ReadOnlySpan<byte> data) => WriteBytesAt(offset, data);

        // Write fixed string (append or write at offset)
        public int WriteFixedString(string value, int length, Encoding? encoding = null)
        {
            int off = AppendZeros(length);
            WriteFixedStringAt(off, length, value, encoding);
            return off;
        }
        public void WriteFixedString(int offset, string value, int length, Encoding? encoding = null)
            => WriteFixedStringAt(offset, length, value, encoding);

        // Write null-terminated string (append or write at offset)
        public int WriteNullTerminatedString(string value, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(value ?? string.Empty);
            int off = AppendBytes(bytes);
            // append null terminator
            AppendZeros(1);
            _buffer[off + bytes.Length] = 0;
            return off;
        }
        public void WriteNullTerminatedString(int offset, string value, Encoding? encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(value ?? string.Empty);
            EnsureCapacity(offset + bytes.Length + 1);
            bytes.AsSpan().CopyTo(_buffer.AsSpan(offset));
            _buffer[offset + bytes.Length] = 0;
            if (offset + bytes.Length + 1 > Length) Length = offset + bytes.Length + 1;
        }

        // Write single (little/big) append/write
        public int WriteSingleLittleEndian(float value)
        {
            int off = AppendZeros(4);
            BinaryWriterV2.WriteSingleLittleEndian(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteSingleLittleEndian(int offset, float value) => BinaryWriterV2.WriteSingleLittleEndian(_buffer.AsSpan(), offset, value);

        public int WriteSingleBigEndian(float value)
        {
            int off = AppendZeros(4);
            BinaryWriterV2.WriteSingleBigEndian(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteSingleBigEndian(int offset, float value) => BinaryWriterV2.WriteSingleBigEndian(_buffer.AsSpan(), offset, value);

        // Write half (append/write)
        public int WriteHalfLittleEndian(float value)
        {
            int off = AppendZeros(2);
            BinaryWriterV2.WriteHalfLittleEndian(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteHalfLittleEndian(int offset, float value) => BinaryWriterV2.WriteHalfLittleEndian(_buffer.AsSpan(), offset, value);

        public int WriteHalfBigEndian(float value)
        {
            int off = AppendZeros(2);
            BinaryWriterV2.WriteHalfBigEndian(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteHalfBigEndian(int offset, float value) => BinaryWriterV2.WriteHalfBigEndian(_buffer.AsSpan(), offset, value);

        // Write 8-bit
        public int WriteUInt8(byte value)
        {
            int off = AppendZeros(1);
            BinaryWriterV2.WriteUInt8(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteUInt8(int offset, byte value) => BinaryWriterV2.WriteUInt8(_buffer.AsSpan(), offset, value);

        public int WriteInt8(sbyte value)
        {
            int off = AppendZeros(1);
            BinaryWriterV2.WriteInt8(_buffer.AsSpan(), off, value);
            return off;
        }
        public void WriteInt8(int offset, sbyte value) => BinaryWriterV2.WriteInt8(_buffer.AsSpan(), offset, value);

        // --- finalize / access ---
        public byte[] ToArray()
        {
            var res = new byte[Length];
            Buffer.BlockCopy(_buffer, 0, res, 0, Length);
            return res;
        }

        public Span<byte> AsSpan() => _buffer.AsSpan(0, Length);
        public Span<byte> FullSpan() => _buffer.AsSpan();

        // If user wrote at arbitrary offset beyond current Length, update Length accordingly
        private void UpdateLengthAfterWrite(int offset, int writtenLength)
        {
            int end = offset + writtenLength;
            if (end > Length) Length = end;
        }
    }
}
