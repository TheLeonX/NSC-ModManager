using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace NSC_ModManager.ViewModel
{
    public class PlayerDoubleEffectParamViewModel : INotifyPropertyChanged {
        private int _searchIndex_field;
        public int SearchIndex_field {
            get { return _searchIndex_field; }
            set {
                _searchIndex_field = value;
                OnPropertyChanged("SearchIndex_field");
            }
        }

        private Visibility _loadingStatePlay;
        public Visibility LoadingStatePlay {
            get { return _loadingStatePlay; }
            set {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }

        private int _characodeID;
        public int CharacodeID_field {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID_field");
            }
        }
        private string _attachBone;
        public string AttachBone_field {
            get { return _attachBone; }
            set {
                _attachBone = value;
                OnPropertyChanged("AttachBone_field");
            }
        }
        private bool _playAtStart;
        public bool PlayAtStart_field {
            get { return _playAtStart; }
            set {
                _playAtStart = value;
                OnPropertyChanged("PlayAtStart_field");
            }
        }
        private bool _playAtEnd;
        public bool PlayAtEnd_field {
            get { return _playAtEnd; }
            set {
                _playAtEnd = value;
                OnPropertyChanged("PlayAtEnd_field");
            }
        }
        private int _unk1;
        public int Unk1_field {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        private string _effectEntry;
        public string EffectEntry_field {
            get { return _effectEntry; }
            set {
                _effectEntry = value;
                OnPropertyChanged("EffectEntry_field");
            }
        }
        private string _plAnmEntry1;
        public string PlAnmEntry1_field {
            get { return _plAnmEntry1; }
            set {
                _plAnmEntry1 = value;
                OnPropertyChanged("PlAnmEntry1_field");
            }
        }
        private string _plAnmEntry2;
        public string PlAnmEntry2_field {
            get { return _plAnmEntry2; }
            set {
                _plAnmEntry2 = value;
                OnPropertyChanged("PlAnmEntry2_field");
            }
        }
        private string _plAnmEntry3;
        public string PlAnmEntry3_field {
            get { return _plAnmEntry3; }
            set {
                _plAnmEntry3 = value;
                OnPropertyChanged("PlAnmEntry3_field");
            }
        }
        private string _plAnmEntry4;
        public string PlAnmEntry4_field {
            get { return _plAnmEntry4; }
            set {
                _plAnmEntry4 = value;
                OnPropertyChanged("PlAnmEntry4_field");
            }
        }
        private string _plAnmEntry5;
        public string PlAnmEntry5_field {
            get { return _plAnmEntry5; }
            set {
                _plAnmEntry5 = value;
                OnPropertyChanged("PlAnmEntry5_field");
            }
        }
        private float _heightSpawn;
        public float HeightSpawn_field {
            get { return _heightSpawn; }
            set {
                _heightSpawn = value;
                OnPropertyChanged("HeightSpawn_field");
            }
        }
        private int _soundID;
        public int SoundID_field {
            get { return _soundID; }
            set {
                _soundID = value;
                OnPropertyChanged("SoundID_field");
            }
        }
        private float _unk2;
        public float Unk2_field {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2_field");
            }
        }
        private bool _unk3;
        public bool Unk3_field {
            get { return _unk3; }
            set {
                _unk3 = value;
                OnPropertyChanged("Unk3_field");
            }
        }
        private bool _enableNearestGroundPos;
        public bool EnableNearestGroundPos_field {
            get { return _enableNearestGroundPos; }
            set {
                _enableNearestGroundPos = value;
                OnPropertyChanged("EnableNearestGroundPos_field");
            }
        }
        public ObservableCollection<PlayerDoubleEffectParamModel> PlayerDoubleEffectParamList { get; set; }
        private PlayerDoubleEffectParamModel _selectedPlayerDoubleEffectParam;
        public PlayerDoubleEffectParamModel SelectedPlayerDoubleEffectParam {
            get { return _selectedPlayerDoubleEffectParam; }
            set {
                _selectedPlayerDoubleEffectParam = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    AttachBone_field = value.AttachBone;
                    PlayAtStart_field = value.PlayAtStart;
                    PlayAtEnd_field = value.PlayAtEnd;
                    Unk1_field = value.Unk1;
                    EffectEntry_field = value.EffectEntry;
                    PlAnmEntry1_field = value.PlAnmEntry1;
                    PlAnmEntry2_field = value.PlAnmEntry2;
                    PlAnmEntry3_field = value.PlAnmEntry3;
                    PlAnmEntry4_field = value.PlAnmEntry4;
                    PlAnmEntry5_field = value.PlAnmEntry5;
                    HeightSpawn_field = value.HeightSpawn;
                    SoundID_field = value.SoundID;
                    Unk2_field = value.Unk2;
                    Unk3_field = value.Unk3;
                    EnableNearestGroundPos_field = value.EnableNearestGroundPos;
                }

                OnPropertyChanged("SelectedPlayerDoubleEffectParam");
            }
        }
        private int _selectedPlayerDoubleEffectParamIndex;
        public int SelectedPlayerDoubleEffectParamIndex {
            get { return _selectedPlayerDoubleEffectParamIndex; }
            set {
                _selectedPlayerDoubleEffectParamIndex = value;
                OnPropertyChanged("SelectedPlayerDoubleEffectParamIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public PlayerDoubleEffectParamViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            PlayerDoubleEffectParamList = new ObservableCollection<PlayerDoubleEffectParamModel>();
            filePath = "";
        }

        public void Clear() {
            PlayerDoubleEffectParamList.Clear();
        }

        public void OpenFile(string basepath = "") {
            Clear();
            if (basepath == "") {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "XFBIN Container (*.xfbin)|*.xfbin";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true) {
                    filePath = myDialog.FileName;
                } else {
                    return;
                }
            } else {
                filePath = basepath;
            }
            if (File.Exists(filePath)) {
                fileByte = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(fileByte, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++) {
                    string name = BinaryReader.b_ReadString(fileByte, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(fileByte, 16);
                if (BinName.Contains("playerDoubleEffectParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x78);
                        PlayerDoubleEffectParamModel PlayerDoubleEffectParamEntry = new PlayerDoubleEffectParamModel();
                        PlayerDoubleEffectParamEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        PlayerDoubleEffectParamEntry.AttachBone = BinaryReader.b_ReadString(fileByte, ptr + 0x08 + BinaryReader.b_ReadInt(fileByte, ptr + 0x08));
                        PlayerDoubleEffectParamEntry.PlayAtStart = BinaryReader.b_ReadInt(fileByte, ptr + 0x10) == 1;
                        PlayerDoubleEffectParamEntry.PlayAtEnd = BinaryReader.b_ReadInt(fileByte, ptr + 0x14) == 1;
                        PlayerDoubleEffectParamEntry.Unk1 = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C);
                        PlayerDoubleEffectParamEntry.EffectEntry = BinaryReader.b_ReadString(fileByte, ptr + 0x20 + BinaryReader.b_ReadInt(fileByte, ptr + 0x20));
                        PlayerDoubleEffectParamEntry.PlAnmEntry1 = BinaryReader.b_ReadString(fileByte, ptr + 0x28 + BinaryReader.b_ReadInt(fileByte, ptr + 0x28));
                        PlayerDoubleEffectParamEntry.PlAnmEntry2 = BinaryReader.b_ReadString(fileByte, ptr + 0x30 + BinaryReader.b_ReadInt(fileByte, ptr + 0x30));
                        PlayerDoubleEffectParamEntry.PlAnmEntry3 = BinaryReader.b_ReadString(fileByte, ptr + 0x38 + BinaryReader.b_ReadInt(fileByte, ptr + 0x38));
                        PlayerDoubleEffectParamEntry.PlAnmEntry4 = BinaryReader.b_ReadString(fileByte, ptr + 0x40 + BinaryReader.b_ReadInt(fileByte, ptr + 0x40));
                        PlayerDoubleEffectParamEntry.PlAnmEntry5 = BinaryReader.b_ReadString(fileByte, ptr + 0x48 + BinaryReader.b_ReadInt(fileByte, ptr + 0x48));
                        PlayerDoubleEffectParamEntry.HeightSpawn = BinaryReader.b_ReadFloat(fileByte, ptr + 0x54);
                        PlayerDoubleEffectParamEntry.SoundID = BinaryReader.b_ReadInt(fileByte, ptr + 0x58);
                        PlayerDoubleEffectParamEntry.Unk2 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x5C);
                        PlayerDoubleEffectParamEntry.Unk3 = BinaryReader.b_ReadInt(fileByte, ptr + 0x60) == 1;
                        PlayerDoubleEffectParamEntry.EnableNearestGroundPos = BinaryReader.b_ReadInt(fileByte, ptr + 0x70) == 1;

                        if (BinaryReader.b_ReadInt(fileByte, ptr + 0x18) == 2)
                            PlayerDoubleEffectParamList.Add(PlayerDoubleEffectParamEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedPlayerDoubleEffectParam is not null) {
                PlayerDoubleEffectParamList.Remove(SelectedPlayerDoubleEffectParam);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedPlayerDoubleEffectParam is not null) {
                SelectedPlayerDoubleEffectParam.CharacodeID = CharacodeID_field;
                SelectedPlayerDoubleEffectParam.AttachBone = AttachBone_field;
                SelectedPlayerDoubleEffectParam.PlayAtStart = PlayAtStart_field;
                SelectedPlayerDoubleEffectParam.PlayAtEnd = PlayAtEnd_field;
                SelectedPlayerDoubleEffectParam.Unk1 = Unk1_field;
                SelectedPlayerDoubleEffectParam.EffectEntry = EffectEntry_field;
                SelectedPlayerDoubleEffectParam.PlAnmEntry1 = PlAnmEntry1_field;
                SelectedPlayerDoubleEffectParam.PlAnmEntry2 = PlAnmEntry2_field;
                SelectedPlayerDoubleEffectParam.PlAnmEntry3 = PlAnmEntry3_field;
                SelectedPlayerDoubleEffectParam.PlAnmEntry4 = PlAnmEntry4_field;
                SelectedPlayerDoubleEffectParam.PlAnmEntry5 = PlAnmEntry5_field;
                SelectedPlayerDoubleEffectParam.HeightSpawn = HeightSpawn_field;
                SelectedPlayerDoubleEffectParam.SoundID = SoundID_field;
                SelectedPlayerDoubleEffectParam.Unk2 = Unk2_field;
                SelectedPlayerDoubleEffectParam.Unk3 = Unk3_field;
                SelectedPlayerDoubleEffectParam.EnableNearestGroundPos = EnableNearestGroundPos_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<PlayerDoubleEffectParamModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].CharacodeID == member_index) {
                    if (Selected < x) {
                        return x;
                    }
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(PlayerDoubleEffectParamList, SearchIndex_field, SelectedPlayerDoubleEffectParamIndex) != -1) {
                    SelectedPlayerDoubleEffectParamIndex = SearchByteIndex(PlayerDoubleEffectParamList, SearchIndex_field, SelectedPlayerDoubleEffectParamIndex);
                    CollectionViewSource.GetDefaultView(PlayerDoubleEffectParamList).MoveCurrentTo(SelectedPlayerDoubleEffectParam);
                } else {
                    if (SearchByteIndex(PlayerDoubleEffectParamList, SearchIndex_field, 0) != -1) {
                        SelectedPlayerDoubleEffectParamIndex = SearchByteIndex(PlayerDoubleEffectParamList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(PlayerDoubleEffectParamList).MoveCurrentTo(SelectedPlayerDoubleEffectParam);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            PlayerDoubleEffectParamModel PlayerDoubleEffectParamEntry = new PlayerDoubleEffectParamModel();
            if (SelectedPlayerDoubleEffectParam is not null) {
                PlayerDoubleEffectParamEntry = (PlayerDoubleEffectParamModel)SelectedPlayerDoubleEffectParam.Clone();
            } else {
                PlayerDoubleEffectParamEntry.CharacodeID = 0;
                PlayerDoubleEffectParamEntry.AttachBone = "";
                PlayerDoubleEffectParamEntry.PlayAtStart = true;
                PlayerDoubleEffectParamEntry.PlayAtEnd = false;
                PlayerDoubleEffectParamEntry.Unk1 = -1;
                PlayerDoubleEffectParamEntry.EffectEntry = "";
                PlayerDoubleEffectParamEntry.PlAnmEntry1 = "";
                PlayerDoubleEffectParamEntry.PlAnmEntry2 = "";
                PlayerDoubleEffectParamEntry.PlAnmEntry3 = "";
                PlayerDoubleEffectParamEntry.PlAnmEntry4 = "";
                PlayerDoubleEffectParamEntry.PlAnmEntry5 = "";
                PlayerDoubleEffectParamEntry.HeightSpawn = 0;
                PlayerDoubleEffectParamEntry.SoundID = -1;
                PlayerDoubleEffectParamEntry.Unk2 = 0;
                PlayerDoubleEffectParamEntry.Unk3 = false;
                PlayerDoubleEffectParamEntry.EnableNearestGroundPos = false;
            }
            PlayerDoubleEffectParamList.Add(PlayerDoubleEffectParamEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }

        public void SaveFile() {
            if (filePath != "") {

                if (File.Exists(filePath + ".backup")) {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
                File.WriteAllBytes(filePath, ConvertToFile());
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
            } else {
                SaveFileAs();
            }
        }

        public void SaveFileAs(string basepath = "") {
            SaveFileDialog s = new SaveFileDialog();
            {
                s.DefaultExt = ".xfbin";
                s.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "")
                s.FileName = basepath;
            else
                s.ShowDialog();
            if (s.FileName == "") {
                return;
            }
            if (s.FileName == filePath) {
                if (File.Exists(filePath + ".backup")) {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
            } else {
                filePath = s.FileName;
            }
            File.WriteAllBytes(filePath, ConvertToFile());
            if (basepath == "")
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
        }

        public byte[] ConvertToFile() {
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/playerDoubleEffectParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "playerDoubleEffectParam");
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
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x48,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x48,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0x5C,0x00,0x00,0x00,0x00,0x00
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length + 0x4;



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE9, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[PlayerDoubleEffectParamList.Count * 0x78]);

            int addSize = 0;

            List<int> AttachBone_pointer = new List<int>();
            List<int> EffectEntry_pointer = new List<int>();
            List<int> PlAnm1_pointer = new List<int>();
            List<int> PlAnm2_pointer = new List<int>();
            List<int> PlAnm3_pointer = new List<int>();
            List<int> PlAnm4_pointer = new List<int>();
            List<int> PlAnm5_pointer = new List<int>();
            for (int x = 0; x < PlayerDoubleEffectParamList.Count; x++) {
                int ptr = startPtr + (x * 0x78);
                AttachBone_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].AttachBone != "" && PlayerDoubleEffectParamList[x].AttachBone is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].AttachBone));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = AttachBone_pointer[x] - startPtr - x * 0x78 - 0x08;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x08);
                    addSize += PlayerDoubleEffectParamList[x].AttachBone.Length + 1;
                }
                EffectEntry_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].EffectEntry != "" && PlayerDoubleEffectParamList[x].EffectEntry is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].EffectEntry));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = EffectEntry_pointer[x] - startPtr - x * 0x78 - 0x20;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x20);
                    addSize += PlayerDoubleEffectParamList[x].EffectEntry.Length + 1;
                }
                PlAnm1_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].PlAnmEntry1 != "" && PlayerDoubleEffectParamList[x].PlAnmEntry1 is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].PlAnmEntry1));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PlAnm1_pointer[x] - startPtr - x * 0x78 - 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x28);
                    addSize += PlayerDoubleEffectParamList[x].PlAnmEntry1.Length + 1;
                }
                PlAnm2_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].PlAnmEntry2 != "" && PlayerDoubleEffectParamList[x].PlAnmEntry2 is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].PlAnmEntry2));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PlAnm2_pointer[x] - startPtr - x * 0x78 - 0x30;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x30);
                    addSize += PlayerDoubleEffectParamList[x].PlAnmEntry2.Length + 1;
                }
                PlAnm3_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].PlAnmEntry3 != "" && PlayerDoubleEffectParamList[x].PlAnmEntry3 is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].PlAnmEntry3));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PlAnm3_pointer[x] - startPtr - x * 0x78 - 0x38;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x38);
                    addSize += PlayerDoubleEffectParamList[x].PlAnmEntry3.Length + 1;
                }
                PlAnm4_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].PlAnmEntry4 != "" && PlayerDoubleEffectParamList[x].PlAnmEntry4 is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].PlAnmEntry4));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PlAnm4_pointer[x] - startPtr - x * 0x78 - 0x40;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x40);
                    addSize += PlayerDoubleEffectParamList[x].PlAnmEntry4.Length + 1;
                }
                PlAnm5_pointer.Add(fileBytes36.Length);
                if (PlayerDoubleEffectParamList[x].PlAnmEntry5 != "" && PlayerDoubleEffectParamList[x].PlAnmEntry5 is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerDoubleEffectParamList[x].PlAnmEntry5));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PlAnm5_pointer[x] - startPtr - x * 0x78 - 0x48;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x48);
                    addSize += PlayerDoubleEffectParamList[x].PlAnmEntry5.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].PlayAtStart), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].PlayAtEnd), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(2), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].Unk1), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((float)1), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].HeightSpawn), ptr + 0x54);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].SoundID), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].Unk2), ptr + 0x5C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].Unk3), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList[x].EnableNearestGroundPos), ptr + 0x70);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerDoubleEffectParamList.Count * 0x78) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerDoubleEffectParamList.Count * 0x78) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerDoubleEffectParamList.Count), count_index);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x8D,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            });
        }

        private RelayCommand _saveFileAsCommand;
        public RelayCommand SaveFileAsCommand {
            get {
                return _saveFileAsCommand ??
                  (_saveFileAsCommand = new RelayCommand(obj => {
                      SaveFileAsAsync();
                  }));
            }
        }
        private RelayCommand _saveFileCommand;
        public RelayCommand SaveFileCommand {
            get {
                return _saveFileCommand ??
                  (_saveFileCommand = new RelayCommand(obj => {
                      SaveFileAsync();
                  }));
            }
        }
        private RelayCommand _openFileCommand;
        public RelayCommand OpenFileCommand {
            get {
                return _openFileCommand ??
                  (_openFileCommand = new RelayCommand(obj => {
                      OpenFileAsync();
                  }));
            }
        }
        private RelayCommand _deleteEntryCommand;
        public RelayCommand DeleteEntryCommand {
            get {
                return _deleteEntryCommand ??
                  (_deleteEntryCommand = new RelayCommand(obj => {
                      RemoveEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupEntryCommand;
        public RelayCommand AddDupEntryCommand {
            get {
                return _addDupEntryCommand ??
                  (_addDupEntryCommand = new RelayCommand(obj => {
                      AddDupEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveEntryCommand;
        public RelayCommand SaveEntryCommand {
            get {
                return _saveEntryCommand ??
                  (_saveEntryCommand = new RelayCommand(obj => {
                      SaveEntryAsync();
                  }));
            }
        }
        private RelayCommand _searchEntryCommand;
        public RelayCommand SearchEntryCommand {
            get {
                return _searchEntryCommand ??
                  (_searchEntryCommand = new RelayCommand(obj => {
                      SearchEntryAsync();
                  }));
            }
        }
        public async void SaveFileAsync() {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFile()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SaveFileAsAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFileAs(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void OpenFileAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => OpenFile(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SearchEntryAsync() {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SearchEntry()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void AddDupEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupEntry()));

        }
        public async void SaveEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveEntry()));

        }
        public async void RemoveEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveEntry()));

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
