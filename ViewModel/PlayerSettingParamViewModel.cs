using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NSC_ModManager.ViewModel
{
    public class PlayerSettingParamViewModel : INotifyPropertyChanged {
        private string _searchTextBox_field;
        public string SearchTextBox_field {
            get { return _searchTextBox_field; }
            set {
                _searchTextBox_field = value;
                OnPropertyChanged("SearchTextBox_field");
            }
        }
        public ObservableCollection<PlayerSettingParamModel> PlayerSettingParamList { get; set; }
        private PlayerSettingParamModel _selectedPSP;
        public PlayerSettingParamModel SelectedPSP {
            get { return _selectedPSP; }
            set {
                _selectedPSP = value;
                if (value is not null) {
                    PSP_ID_field = value.PSP_ID;
                    CharacodeID_field = value.CharacodeID;
                    CostumeID_field = value.CostumeID;
                    Unk_field = value.Unk;
                    PSP_code_field = value.PSP_code;
                    DefaultJutsu_field = value.DefaultJutsu;
                    DefaultUltimateJutsu_field = value.DefaultUltimateJutsu;
                    CharacterNameMessageID_field = value.CharacterNameMessageID;
                    CostumeDescriptionMessageID_field = value.CostumeDescriptionMessageID;
                    DLC_ID_field = value.DLC_ID;
                    MainPSP_ID_field = value.MainPSP_ID;
                    ReferenceCharacodeID_field = value.ReferenceCharacodeID;
                    Unk1_field = value.Unk1;
                }
                    
                OnPropertyChanged("SelectedPSP");
            }
        }
        private int _selectedPSPIndex;
        public int SelectedPSPIndex {
            get { return _selectedPSPIndex; }
            set {
                _selectedPSPIndex = value;
                OnPropertyChanged("SelectedPSPIndex");
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

        private int _psp_id_field;
        public int PSP_ID_field {
            get { return _psp_id_field; }
            set {
                _psp_id_field = value;
                OnPropertyChanged("PSP_ID_field");
            }
        }
        private int _characodeID_field;
        public int CharacodeID_field {
            get { return _characodeID_field; }
            set {
                _characodeID_field = value;
                OnPropertyChanged("CharacodeID_field");
            }
        }

        private int _costumeID_field;
        public int CostumeID_field {
            get { return _costumeID_field; }
            set {
                _costumeID_field = value;
                OnPropertyChanged("CostumeID_field");
            }
        }

        private int _unk_field;
        public int Unk_field {
            get { return _unk_field; }
            set {
                _unk_field = value;
                OnPropertyChanged("Unk_field");
            }
        }
        private string _PSP_code_field;
        public string PSP_code_field {
            get { return _PSP_code_field; }
            set {
                _PSP_code_field = value;
                OnPropertyChanged("PSP_code_field");
            }
        }
        private int _defaultJutsu_field;
        public int DefaultJutsu_field {
            get { return _defaultJutsu_field; }
            set {
                _defaultJutsu_field = value;
                OnPropertyChanged("DefaultJutsu_field");
            }
        }

        private int _defaultUltimateJutsu_field;
        public int DefaultUltimateJutsu_field {
            get { return _defaultUltimateJutsu_field; }
            set {
                _defaultUltimateJutsu_field = value;
                OnPropertyChanged("DefaultUltimateJutsu_field");
            }
        }

        private string _characterNameMessageID_field;
        public string CharacterNameMessageID_field {
            get { return _characterNameMessageID_field; }
            set {
                _characterNameMessageID_field = value;
                OnPropertyChanged("CharacterNameMessageID_field");
            }
        }
        private string _costumeDescriptionMessageID_field;
        public string CostumeDescriptionMessageID_field {
            get { return _costumeDescriptionMessageID_field; }
            set {
                _costumeDescriptionMessageID_field = value;
                OnPropertyChanged("CostumeDescriptionMessageID_field");
            }
        }
        private int _dlc_id_field;
        public int DLC_ID_field {
            get { return _dlc_id_field; }
            set {
                _dlc_id_field = value;
                OnPropertyChanged("DLC_ID_field");
            }
        }
        private int _mainPSP_ID_field;
        public int MainPSP_ID_field {
            get { return _mainPSP_ID_field; }
            set {
                _mainPSP_ID_field = value;
                OnPropertyChanged("MainPSP_ID_field");
            }
        }
        private int _referenceCharacodeID_field;
        public int ReferenceCharacodeID_field {
            get { return _referenceCharacodeID_field; }
            set {
                _referenceCharacodeID_field = value;
                OnPropertyChanged("ReferenceCharacodeID_field");
            }
        }

        private int _unk1_field;
        public int Unk1_field {
            get { return _unk1_field; }
            set {
                _unk1_field = value;
                OnPropertyChanged("Unk1_field");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public PlayerSettingParamViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            PlayerSettingParamList = new ObservableCollection<PlayerSettingParamModel>();
            filePath = "";

        }

        public void Clear() {
            SelectedPSPIndex = -1;
            PlayerSettingParamList.Clear();
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
                if (BinName.Contains("playerSettingParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x40);
                        PlayerSettingParamModel PSP_entry = new PlayerSettingParamModel();
                        PSP_entry.PSP_ID = BinaryReader.b_ReadInt(fileByte, ptr);
                        PSP_entry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr + 0x04);
                        PSP_entry.CostumeID = BinaryReader.b_ReadInt(fileByte, ptr + 0x08);
                        PSP_entry.Unk = BinaryReader.b_ReadInt(fileByte, ptr + 0x0C);
                        PSP_entry.PSP_code = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        PSP_entry.DefaultJutsu = BinaryReader.b_ReadInt(fileByte, ptr + 0x18);
                        PSP_entry.DefaultUltimateJutsu = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C);
                        PSP_entry.CharacterNameMessageID = BinaryReader.b_ReadString(fileByte, ptr + 0x20 + BinaryReader.b_ReadInt(fileByte, ptr + 0x20));
                        PSP_entry.CostumeDescriptionMessageID = BinaryReader.b_ReadString(fileByte, ptr + 0x28 + BinaryReader.b_ReadInt(fileByte, ptr + 0x28));
                        PSP_entry.DLC_ID = BinaryReader.b_ReadInt(fileByte, ptr + 0x30);
                        PSP_entry.MainPSP_ID = BinaryReader.b_ReadInt(fileByte, ptr + 0x34);
                        PSP_entry.ReferenceCharacodeID = BinaryReader.b_ReadInt(fileByte, ptr + 0x38);
                        PSP_entry.Unk1 = BinaryReader.b_ReadInt(fileByte, ptr + 0x3C);
                        PlayerSettingParamList.Add(PSP_entry);
                    }
                }
                else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedPSP is not null) {
                PlayerSettingParamList.Remove(SelectedPSP);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedPSP is not null) {
                PlayerSettingParamList[SelectedPSPIndex].PSP_ID = PSP_ID_field;
                PlayerSettingParamList[SelectedPSPIndex].CharacodeID = CharacodeID_field;
                PlayerSettingParamList[SelectedPSPIndex].CostumeID = CostumeID_field;
                PlayerSettingParamList[SelectedPSPIndex].Unk = Unk_field;
                PlayerSettingParamList[SelectedPSPIndex].PSP_code = PSP_code_field;
                PlayerSettingParamList[SelectedPSPIndex].DefaultJutsu = DefaultJutsu_field;
                PlayerSettingParamList[SelectedPSPIndex].DefaultUltimateJutsu = DefaultUltimateJutsu_field;
                PlayerSettingParamList[SelectedPSPIndex].CharacterNameMessageID = CharacterNameMessageID_field;
                PlayerSettingParamList[SelectedPSPIndex].CostumeDescriptionMessageID = CostumeDescriptionMessageID_field;
                PlayerSettingParamList[SelectedPSPIndex].DLC_ID = DLC_ID_field;
                PlayerSettingParamList[SelectedPSPIndex].MainPSP_ID = MainPSP_ID_field;
                PlayerSettingParamList[SelectedPSPIndex].ReferenceCharacodeID = ReferenceCharacodeID_field;
                PlayerSettingParamList[SelectedPSPIndex].Unk1 = Unk1_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public int SearchStringIndex(ObservableCollection<PlayerSettingParamModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].PSP_code;
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
                if (SearchStringIndex(PlayerSettingParamList, SearchTextBox_field, SelectedPSPIndex) != -1) {
                    SelectedPSPIndex = SearchStringIndex(PlayerSettingParamList, SearchTextBox_field, SelectedPSPIndex);
                    CollectionViewSource.GetDefaultView(PlayerSettingParamList).MoveCurrentTo(SelectedPSP);
                } else {
                    if (SearchStringIndex(PlayerSettingParamList, SearchTextBox_field, 0) != -1) {
                        SelectedPSPIndex = SearchStringIndex(PlayerSettingParamList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(PlayerSettingParamList).MoveCurrentTo(SelectedPSP);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }

        public int FreeSlot() {
            if (PlayerSettingParamList.Count > 0) {
                List<int> PSP_ID_List = new List<int>();
                for (int c = 0; c < PlayerSettingParamList.Count; c++) {
                    PSP_ID_List.Add(PlayerSettingParamList[c].PSP_ID);
                }
                int new_presetID = 0;
                do {
                    new_presetID++;
                }
                while (PSP_ID_List.Contains(new_presetID));
                return new_presetID;
            }
            else
                return 0;
        }

        public void AddDupEntry() {
            PlayerSettingParamModel PSP_entry = new PlayerSettingParamModel();
            if (SelectedPSP is not null) {
                PSP_entry = (PlayerSettingParamModel)PlayerSettingParamList[SelectedPSPIndex].Clone();
                
            } else {
                PSP_entry.CharacodeID = 0;
                PSP_entry.CostumeID = 0;
                PSP_entry.Unk = 0;
                PSP_entry.PSP_code = "1acb00";
                PSP_entry.DefaultJutsu = 0;
                PSP_entry.DefaultUltimateJutsu = 0;
                PSP_entry.CharacterNameMessageID = "c_cha_";
                PSP_entry.CostumeDescriptionMessageID = "c_cha_";
                PSP_entry.DLC_ID = -1;
                PSP_entry.MainPSP_ID = -1;
                PSP_entry.ReferenceCharacodeID = 0;
                PSP_entry.Unk1 = 0;
            }
            List<int> PSP_ID_List = new List<int>();
            for (int c = 0; c < PlayerSettingParamList.Count; c++) {
                PSP_ID_List.Add(PlayerSettingParamList[c].PSP_ID);
            }
            int new_presetID = 0;
            do {
                new_presetID++;
            }
            while (PSP_ID_List.Contains(new_presetID));
            PSP_entry.PSP_ID = new_presetID;
            PlayerSettingParamList.Add(PSP_entry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/playerSettingParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "playerSettingParam");
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
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x9C,0x8D,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x9C,0x89
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length + 0x4;



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE8, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[PlayerSettingParamList.Count * 0x40]);

            int addSize = 0;

            List<int> PSP_Code_pointer = new List<int>();
            List<int> CharacterNamePointer = new List<int>();
            List<int> DescriptionPointer = new List<int>();
            for (int x = 0; x < PlayerSettingParamList.Count; x++) {
                int ptr = startPtr + (x * 0x40);
                PSP_Code_pointer.Add(fileBytes36.Length);
                if (PlayerSettingParamList[x].PSP_code != "" && PlayerSettingParamList[x].PSP_code is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerSettingParamList[x].PSP_code));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = PSP_Code_pointer[x] - startPtr - x * 0x40 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += PlayerSettingParamList[x].PSP_code.Length + 1;
                }
                CharacterNamePointer.Add(fileBytes36.Length);
                if (PlayerSettingParamList[x].CharacterNameMessageID != "" && PlayerSettingParamList[x].CharacterNameMessageID is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerSettingParamList[x].CharacterNameMessageID));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = CharacterNamePointer[x] - startPtr - x * 0x40 - 0x20;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x20);
                    addSize += PlayerSettingParamList[x].CharacterNameMessageID.Length + 1;
                }
                DescriptionPointer.Add(fileBytes36.Length);
                if (PlayerSettingParamList[x].CostumeDescriptionMessageID != "" && PlayerSettingParamList[x].CostumeDescriptionMessageID is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(PlayerSettingParamList[x].CostumeDescriptionMessageID));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = DescriptionPointer[x] - startPtr - x * 0x40 - 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x28);
                    addSize += PlayerSettingParamList[x].CostumeDescriptionMessageID.Length + 1;
                }

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].PSP_ID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].CharacodeID), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].CostumeID), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].Unk), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].DefaultJutsu), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].DefaultUltimateJutsu), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].DLC_ID), ptr + 0x30);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].MainPSP_ID), ptr + 0x34);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].ReferenceCharacodeID), ptr + 0x38);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList[x].Unk1), ptr + 0x3C);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerSettingParamList.Count * 0x40) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerSettingParamList.Count * 0x40) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PlayerSettingParamList.Count), count_index);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
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

