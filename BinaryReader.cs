using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager {
    internal class BinaryReader {
        public static byte[] b_ReadByteArray(byte[] actual, int index, int count) {
            List<byte> a = new List<byte>();
            for (int x = 0; x < count; x++) {
                a.Add(actual[index + x]);
            }
            return a.ToArray();
        }
        public static byte[] b_ReadByteArrayOfString(byte[] actual, int index) {
            List<byte> a = new List<byte>();
            int count = 65535;
            for (int x = 0; x < count; x++) {
                if (actual[index + x] != 0)
                    a.Add(actual[index + x]);
                else
                    x = count;
            }
            return a.ToArray();
        }
        public static int b_byteArrayToInt(byte[] actual) {

            return actual[0] + actual[1] * 256 + actual[2] * 65536 + actual[3] * 16777216;
        }
        public static int b_byteArrayToIntTwoBytes(byte[] actual) {
            return actual[0] + actual[1] * 256;
        }
        public static Int16 b_byteArrayToInt16(byte[] actual) {
            return (short)(actual[0] + actual[1] * 256);
        }
        public static UInt16 b_byteArrayToUInt16(byte[] actual) {
            return (ushort)(actual[0] + actual[1] * 256);
        }
        public static int b_byteArrayToIntRevTwoBytes(byte[] actual) {
            return actual[1] + actual[0] * 256;
        }
        public static int b_byteArrayToIntRev(byte[] actual) {
            return actual[3] + actual[2] * 256 + actual[1] * 65536 + actual[0] * 16777216;
        }

        public static int b_ReadInt(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToInt(BinaryReader.b_ReadByteArray(fileBytes, index, 4));
        }
        public static Int16 b_ReadInt16(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToInt16(BinaryReader.b_ReadByteArray(fileBytes, index, 2));
        }
        public static UInt16 b_ReadUInt16(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToUInt16(BinaryReader.b_ReadByteArray(fileBytes, index, 2));
        }
        public static int b_ReadIntFromTwoBytes(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToIntTwoBytes(BinaryReader.b_ReadByteArray(fileBytes, index, 2));
        }
        public static int b_ReadIntRevFromTwoBytes(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToIntRevTwoBytes(BinaryReader.b_ReadByteArray(fileBytes, index, 2));
        }
        public static int b_ReadIntRev(byte[] fileBytes, int index) {
            return BinaryReader.b_byteArrayToIntRev(BinaryReader.b_ReadByteArray(fileBytes, index, 4));
        }

        public static float b_ReadFloat(byte[] actual, int index) {

            return BitConverter.ToSingle(actual, index);
        }
        public static float b_ReadFloatRev(byte[] actual, int index) {

            List<byte> a = new List<byte>();
            for (int x = 0; x < 4; x++) {
                a.Add(actual[index + 3 - x]);
            }
            return BitConverter.ToSingle(a.ToArray(), 0);
        }

        public static string b_ReadString(byte[] actual, int index, int count = -1) {
            string a = "";
            if (count == -1) {
                for (int x2 = index; x2 < actual.Length; x2++) {
                    if (actual[x2] != 0) {
                        string str = a;
                        char c = (char)actual[x2];
                        a = str + c;
                    } else {
                        x2 = actual.Length;
                    }
                }
            } else {
                for (int x = index; x < count; x++) {
                    string str2 = a;
                    char c = (char)actual[x];
                    a = str2 + c;
                }
            }
            return a;
        }

        public static string b_ReadString3(byte[] actual, int index, int count = -1, int skip = 0) {
            string a = "";
            if (count == -1) {
                for (int x2 = index; x2 < actual.Length; x2++) {
                    if (actual[x2] != 0 && actual[x2] != skip) {
                        string str = a;
                        char c = (char)actual[x2];
                        a = str + c;
                    } else {
                        x2 = actual.Length;
                    }
                }
            } else {
                for (int x = index; x < index + count; x++) {
                    string str2 = a;
                    char c = (char)actual[x];
                    a = str2 + c;
                }
            }
            return a;
        }

        public static byte[] b_ReplaceBytes(byte[] actual, byte[] bytesToReplace, int index, int invert = 0, int count = -1)
        {
            int length = (count == -1) ? bytesToReplace.Length : count;

            if (invert == 0)
            {
                // Copy bytes directly for normal replacement
                Array.Copy(bytesToReplace, 0, actual, index, length);
            } else
            {
                // Replace with inverted bytes
                for (int x = 0; x < length; x++)
                {
                    actual[index + x] = bytesToReplace[bytesToReplace.Length - 1 - x];
                }
            }

            return actual;
        }



        public static byte[] b_ReplaceString(byte[] actual, string str, int Index, int Count = -1) {
            if (Count == -1) {
                for (int x2 = 0; x2 < str.Length; x2++) {
                    actual[Index + x2] = (byte)str[x2];
                }
            } else {
                for (int x = 0; x < Count; x++) {
                    if (str is not null) {
                        if (str.Length > x) {
                            actual[Index + x] = (byte)str[x];
                        } else {
                            actual[Index + x] = 0;
                        }
                    }
                    
                }
            }
            return actual;
        }
        public static string b_ReplaceRealString(string actual, string str, int Index, int Count = -1) {
            char[] test = actual.ToCharArray();
            string output = "";
            if (Count == -1) {
                for (int x2 = 0; x2 < str.Length; x2++) {

                    test[Index + x2] = str[x2];
                }
            } else {
                for (int x = 0; x < Count; x++) {
                    if (str.Length > x) {
                        test[Index + x] = str[x];
                    } else {
                        test[Index + x] = '\0';
                    }
                }
            }
            for (int i = 0; i < test.Length; i++) {
                output = output + test[i];
            }
            return output;
        }

        public static byte[] b_AddBytes(byte[] actual, byte[] bytesToAdd, int Reverse = 0, int index = 0, int count = -1)
        {
            if (bytesToAdd == null || bytesToAdd.Length == 0)
                return actual;

            count = count == -1 ? bytesToAdd.Length : count;

            // Ensure index and count are within bounds
            index = Math.Clamp(index, 0, bytesToAdd.Length);
            count = Math.Clamp(count, 0, bytesToAdd.Length - index);

            IEnumerable<byte> segment = Reverse == 0
                ? bytesToAdd.Skip(index).Take(count)
                : bytesToAdd.Reverse().Skip(index).Take(count);

            return actual.Concat(segment).ToArray();
        }

        public static byte[] b_AddInt(byte[] actual, int _num) {
            List<byte> a = actual.ToList();
            byte[] b = BitConverter.GetBytes(_num);
            for (int x = 0; x < 4; x++) {
                a.Add(b[x]);
            }
            return a.ToArray();
        }

        public static byte[] b_StringToBytes(string hexstr) {
            int length = hexstr.Length;
            string NewCode = "";
            for (int i = 0; i < length; i++) {
                if (hexstr[i] != (char)32) {
                    NewCode = NewCode + hexstr[i];
                }
            }
            if (string.IsNullOrWhiteSpace(NewCode))
                throw new ArgumentNullException("Wrong Format");

            if ((NewCode.Length % 2) != 0)
                throw new ArgumentException("Wrong Format");

            var arr = new byte[NewCode.Length / 2];

            for (int i = 0, j = 0; j < NewCode.Length; i++, j += 2) {
                var bstr = NewCode.Substring(j, 2);
                arr[i] = byte.Parse(bstr, NumberStyles.AllowHexSpecifier);
            }

            return arr;
        }

        public static byte[] b_AddString(byte[] actual, string _str, int count = -1)
        {
            if (_str is null) return actual;

            // Convert the string to bytes
            var strBytes = Encoding.UTF8.GetBytes(_str);

            // Calculate padding size if count is specified
            int paddingSize = count > 0 ? Math.Max(0, count - strBytes.Length) : 0;

            // Combine the original bytes, string bytes, and padding
            return actual.Concat(strBytes)
                         .Concat(new byte[paddingSize])
                         .ToArray();
        }

        public static byte[] b_AddFloat(byte[] actual, float f) {
            List<byte> a = actual.ToList();
            byte[] floatBytes = BitConverter.GetBytes(f);
            for (int x = 0; x < 4; x++) {
                a.Add(floatBytes[x]);
            }
            return a.ToArray();
        }

        public static int b_FindBytes(byte[] actual, byte[] bytes, int index = 0) {
            int actualIndex = index;
            byte[] actualBytes = new byte[bytes.Length];
            bool f = false;

            int foundIndex = -1;

            for (int a = actualIndex; a < (actual.Length - bytes.Length); a++) {
                f = true;

                for (int x = 0; x < bytes.Length; x++) {
                    actualBytes[x] = actual[a + x];

                    if (actualBytes[x] != bytes[x]) {
                        x = bytes.Length;
                        f = false;
                    }
                }

                if (f) {
                    foundIndex = a;
                    a = actual.Length;
                }
            }

            return foundIndex;
        }
        public static bool b_FindBytesBool(byte[] actual, byte[] bytes, int index = 0) {
            int actualIndex = index;
            byte[] actualBytes = new byte[bytes.Length];
            bool found = false;
            bool f = false;

            int foundIndex = -1;

            for (int a = actualIndex; a < (actual.Length - bytes.Length); a++) {
                f = true;

                for (int x = 0; x < bytes.Length; x++) {
                    actualBytes[x] = actual[a + x];

                    if (actualBytes[x] != bytes[x]) {
                        x = bytes.Length;
                        f = false;
                    }
                }

                if (f) {
                    found = true;
                    foundIndex = a;
                    a = actual.Length;
                }
            }

            return found;
        }
        public static int b_FindString(string actual, string str, int index = 0) {
            int actualIndex = index;
            char[] actualString = new char[str.Length];
            bool f = false;

            int foundIndex = -1;

            for (int a = actualIndex; a < (actual.Length - str.Length); a++) {
                f = true;

                for (int x = 0; x < str.Length; x++) {
                    actualString[x] = actual[a + x];

                    if (actualString[x] != str[x]) {
                        x = str.Length;
                        f = false;
                    }
                }

                if (f) {
                    foundIndex = a;
                    a = actual.Length;
                }
            }

            return foundIndex;
        }
        public static List<int> b_FindBytesList(byte[] actual, byte[] bytes, int index = 0) {
            int actualIndex = index;
            List<int> indexes = new List<int>();

            int lastFound = 0;
            while (lastFound != -1) {
                lastFound = b_FindBytes(actual, bytes, actualIndex);
                if (lastFound != -1) {
                    actualIndex = lastFound + 1;
                    indexes.Add(lastFound);
                }
            }

            return indexes;
        }

        public static List<long> crc32_table() {
            var a = new List<long>();
            foreach (var i in Enumerable.Range(0, 256)) {
                var k = i << 24;
                foreach (var _ in Enumerable.Range(0, 8)) {
                    if (Convert.ToBoolean(k & 0x80000000))
                        k = k << 1 ^ 0x4c11db7;
                    else
                        k = k << 1;
                }
                a.Add(k & 0xffffffff);
            }
            return a;
        }

        public static byte[] crc32(string str) {
            byte[] bytestream = Encoding.ASCII.GetBytes(str);
            var crc_table = crc32_table();
            var crc = 0xffffffff;
            foreach (var bytes in bytestream) {
                var lookup_index = (crc >> 24 ^ bytes) & 0xff;
                crc = (uint)((crc & 0xffffff) << 8 ^ crc_table[(int)lookup_index]);
            }
            crc = ~crc & 0xffffffff;
            return BitConverter.GetBytes(crc);
        }

        public static byte[] MakeXfbinBinary(string path, string name, byte[] file) {

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

            while (fileBytes36.Length % 4 != 0) {
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
}
