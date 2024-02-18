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
using Microsoft.WindowsAPICodePack.Shell;
using static System.Windows.Forms.AxHost;
using Microsoft.Win32;
using System.IO;

namespace NSC_ModManager.ViewModel {
    public class AwakeAuraViewModel : INotifyPropertyChanged {
        private string _searchTextBox_field;
        public string SearchTextBox_field {
            get { return _searchTextBox_field; }
            set {
                _searchTextBox_field = value;
                OnPropertyChanged("SearchTextBox_field");
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

        private string _characode;
        public string Characode_field {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("Characode_field");
            }
        }
        private string _skillFile;
        public string SkillFile_field {
            get { return _skillFile; }
            set {
                _skillFile = value;
                OnPropertyChanged("SkillFile_field");
            }
        }
        private string _skillEntryName;
        public string SkillEntryName_field {
            get { return _skillEntryName; }
            set {
                _skillEntryName = value;
                OnPropertyChanged("SkillEntryName_field");
            }
        }
        private string _mainBone;
        public string MainBone_field {
            get { return _mainBone; }
            set {
                _mainBone = value;
                OnPropertyChanged("MainBone_field");
            }
        }
        private string _secondaryBone;
        public string SecondaryBone_field {
            get { return _secondaryBone; }
            set {
                _secondaryBone = value;
                OnPropertyChanged("SecondaryBone_field");
            }
        }
        private bool _mainBoneUsePlayerModel;
        public bool MainBoneUsePlayerModel_field {
            get { return _mainBoneUsePlayerModel; }
            set {
                _mainBoneUsePlayerModel = value;
                OnPropertyChanged("MainBoneUsePlayerModel_field");
            }
        }
        private bool _secondaryBoneUsePlayerModel;
        public bool SecondaryBoneUsePlayerModel_field {
            get { return _secondaryBoneUsePlayerModel; }
            set {
                _secondaryBoneUsePlayerModel = value;
                OnPropertyChanged("SecondaryBoneUsePlayerModel_field");
            }
        }
        private int _state;
        public int State_field {
            get { return _state; }
            set {
                _state = value;
                OnPropertyChanged("State_field");
            }
        }
        private int _condition;
        public int Condition_field {
            get { return _condition; }
            set {
                _condition = value;
                OnPropertyChanged("Condition_field");
            }
        }
        private bool _unk1;
        public bool Unk1_field {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        public ObservableCollection<AwakeAuraModel> AwakeAuraList { get; set; }
        private AwakeAuraModel _selectedAwakeAura;
        public AwakeAuraModel SelectedAwakeAura {
            get { return _selectedAwakeAura; }
            set {
                _selectedAwakeAura = value;
                if (value is not null) {
                    Characode_field = value.Characode;
                    SkillFile_field = value.SkillFile;
                    SkillEntryName_field = value.SkillEntryName;
                    MainBone_field = value.MainBone;
                    SecondaryBone_field = value.SecondaryBone;
                    MainBoneUsePlayerModel_field = value.MainBoneUsePlayerModel;
                    SecondaryBoneUsePlayerModel_field = value.SecondaryBoneUsePlayerModel;
                    State_field = value.State;
                    Condition_field = value.Condition;
                    Unk1_field = value.Unk1;
                }
                OnPropertyChanged("SelectedAwakeAura");
            }
        }
        private int _selectedAwakeAuraIndex;
        public int SelectedAwakeAuraIndex {
            get { return _selectedAwakeAuraIndex; }
            set {
                _selectedAwakeAuraIndex = value;
                OnPropertyChanged("SelectedAwakeAuraIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public AwakeAuraViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            AwakeAuraList = new ObservableCollection<AwakeAuraModel>();
            filePath = "";
        }

        public void Clear() {
            AwakeAuraList.Clear();
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
                if (BinName.Contains("awakeAura")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x48);
                        AwakeAuraModel AwakeAuraEntry = new AwakeAuraModel();
                        AwakeAuraEntry.Characode = BinaryReader.b_ReadString(fileByte, ptr + BinaryReader.b_ReadInt(fileByte, ptr));


                        bool NormalStateEnabled = Convert.ToBoolean(BinaryReader.b_ReadInt(fileByte, ptr + 0x08));
                        bool AwakeStateEnabled = Convert.ToBoolean(BinaryReader.b_ReadInt(fileByte, ptr + 0x0C));
                        AwakeAuraEntry.State = 0;
                        if (NormalStateEnabled && !AwakeStateEnabled)
                            AwakeAuraEntry.State = 1;
                        else if (!NormalStateEnabled && AwakeStateEnabled)
                            AwakeAuraEntry.State = 2;
                        AwakeAuraEntry.SkillFile = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        AwakeAuraEntry.SkillEntryName = BinaryReader.b_ReadString(fileByte, ptr + 0x18 + BinaryReader.b_ReadInt(fileByte, ptr + 0x18));
                        AwakeAuraEntry.MainBoneUsePlayerModel = Convert.ToBoolean(BinaryReader.b_ReadInt(fileByte, ptr + 0x20));
                        AwakeAuraEntry.MainBone = BinaryReader.b_ReadString(fileByte, ptr + 0x28 + BinaryReader.b_ReadInt(fileByte, ptr + 0x28));
                        AwakeAuraEntry.SecondaryBoneUsePlayerModel = Convert.ToBoolean(BinaryReader.b_ReadInt(fileByte, ptr + 0x30));
                        AwakeAuraEntry.SecondaryBone = BinaryReader.b_ReadString(fileByte, ptr + 0x38 + BinaryReader.b_ReadInt(fileByte, ptr + 0x38));
                        AwakeAuraEntry.Condition = BinaryReader.b_ReadInt(fileByte, ptr + 0x40);
                        AwakeAuraEntry.Unk1 = Convert.ToBoolean(BinaryReader.b_ReadInt(fileByte, ptr + 0x44));
                        if (AwakeAuraEntry.Characode != "")
                            AwakeAuraList.Add(AwakeAuraEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedAwakeAura is not null) {
                AwakeAuraList.Remove(SelectedAwakeAura);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedAwakeAura is not null) {
                SelectedAwakeAura.Characode = Characode_field;
                SelectedAwakeAura.SkillFile = SkillFile_field;
                SelectedAwakeAura.SkillEntryName = SkillEntryName_field;
                SelectedAwakeAura.MainBone = MainBone_field;
                SelectedAwakeAura.SecondaryBone = SecondaryBone_field;
                SelectedAwakeAura.MainBoneUsePlayerModel = MainBoneUsePlayerModel_field;
                SelectedAwakeAura.SecondaryBoneUsePlayerModel = SecondaryBoneUsePlayerModel_field;
                SelectedAwakeAura.State = State_field;
                SelectedAwakeAura.Condition = Condition_field;
                SelectedAwakeAura.Unk1 = Unk1_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchStringIndex(ObservableCollection<AwakeAuraModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].Characode;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchEntry() {
            if (SearchTextBox_field is not null) {
                if (SearchStringIndex(AwakeAuraList, SearchTextBox_field, SelectedAwakeAuraIndex) != -1) {
                    SelectedAwakeAuraIndex = SearchStringIndex(AwakeAuraList, SearchTextBox_field, SelectedAwakeAuraIndex);
                    CollectionViewSource.GetDefaultView(AwakeAuraList).MoveCurrentTo(SelectedAwakeAura);
                } else {
                    if (SearchStringIndex(AwakeAuraList, SearchTextBox_field, 0) != -1) {
                        SelectedAwakeAuraIndex = SearchStringIndex(AwakeAuraList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(AwakeAuraList).MoveCurrentTo(SelectedAwakeAura);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that characode.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }


        public void AddDupEntry() {
            AwakeAuraModel awakeAuraEntry = new AwakeAuraModel();
            if (SelectedAwakeAura is not null) {
                awakeAuraEntry = (AwakeAuraModel)SelectedAwakeAura.Clone();
            } else {
                awakeAuraEntry.Characode = "";
                awakeAuraEntry.SkillFile = "";
                awakeAuraEntry.SkillEntryName = "";
                awakeAuraEntry.MainBone = "";
                awakeAuraEntry.SecondaryBone = "";
                awakeAuraEntry.MainBoneUsePlayerModel = true;
                awakeAuraEntry.SecondaryBoneUsePlayerModel = true;
                awakeAuraEntry.State = 2;
                awakeAuraEntry.Condition = 0;
                awakeAuraEntry.Unk1 = false;
            }
            AwakeAuraList.Add(awakeAuraEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/awakeAura.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "awakeAura");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[AwakeAuraList.Count * 0x48]);

            int addSize = 0;

            List<int> characode_pointer = new List<int>();
            List<int> skillFile_pointer = new List<int>();
            List<int> skillEntry_pointer = new List<int>();
            List<int> MainBone_pointer = new List<int>();
            List<int> SecondaryBone_pointer = new List<int>();
            for (int x = 0; x < AwakeAuraList.Count; x++) {
                int ptr = startPtr + (x * 0x48);
                characode_pointer.Add(fileBytes36.Length);
                if (AwakeAuraList[x].Characode != "" && AwakeAuraList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AwakeAuraList[x].Characode));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = characode_pointer[x] - startPtr - x * 0x48;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr);
                    addSize += AwakeAuraList[x].Characode.Length + 1;
                }
                skillFile_pointer.Add(fileBytes36.Length);
                if (AwakeAuraList[x].SkillFile != "" && AwakeAuraList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AwakeAuraList[x].SkillFile));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = skillFile_pointer[x] - startPtr - x * 0x48 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += AwakeAuraList[x].SkillFile.Length + 1;
                }
                skillEntry_pointer.Add(fileBytes36.Length);
                if (AwakeAuraList[x].SkillEntryName != "" && AwakeAuraList[x].SkillEntryName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AwakeAuraList[x].SkillEntryName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = skillEntry_pointer[x] - startPtr - x * 0x48 - 0x18;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x18);
                    addSize += AwakeAuraList[x].SkillEntryName.Length + 1;
                }
                MainBone_pointer.Add(fileBytes36.Length);
                if (AwakeAuraList[x].MainBone != "" && AwakeAuraList[x].MainBone is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AwakeAuraList[x].MainBone));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = MainBone_pointer[x] - startPtr - x * 0x48 - 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x28);
                    addSize += AwakeAuraList[x].MainBone.Length + 1;
                }
                SecondaryBone_pointer.Add(fileBytes36.Length);
                if (AwakeAuraList[x].SecondaryBone != "" && AwakeAuraList[x].SecondaryBone is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AwakeAuraList[x].SecondaryBone));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = SecondaryBone_pointer[x] - startPtr - x * 0x48 - 0x38;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x38);
                    addSize += AwakeAuraList[x].SecondaryBone.Length + 1;
                }

                switch (AwakeAuraList[x].State) {
                    case 1:
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(true), ptr + 0x08);
                        break;
                    case 2:
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(true), ptr + 0x0C);
                        break;
                }

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AwakeAuraList[x].MainBoneUsePlayerModel), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AwakeAuraList[x].SecondaryBoneUsePlayerModel), ptr + 0x30);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AwakeAuraList[x].Condition), ptr + 0x40);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AwakeAuraList[x].Unk1), ptr + 0x44);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AwakeAuraList.Count * 0x48) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AwakeAuraList.Count * 0x48) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AwakeAuraList.Count), count_index);
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
