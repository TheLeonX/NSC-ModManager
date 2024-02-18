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
using static System.Windows.Forms.AxHost;

namespace NSC_ModManager.ViewModel
{
    public class SpTypeSupportParamViewModel : INotifyPropertyChanged {
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

        private int _characodeID_field;
        public int CharacodeID_field {
            get { return _characodeID_field; }
            set {
                _characodeID_field = value;
                OnPropertyChanged("CharacodeID_field");
            }
        }
        private int _state_field;
        public int State_field {
            get { return _state_field; }
            set {
                _state_field = value;
                OnPropertyChanged("State_field");
            }
        }
        private int _direction_field;
        public int Direction_field {
            get { return _direction_field; }
            set {
                _direction_field = value;
                OnPropertyChanged("Direction_field");
            }
        }
        private string _leftJutsuName_field;
        public string LeftJutsuName_field {
            get { return _leftJutsuName_field; }
            set {
                _leftJutsuName_field = value;
                OnPropertyChanged("LeftJutsuName_field");
            }
        }
        private bool _leftJutsuEnableOnGround_field;
        public bool LeftJutsuEnableOnGround_field {
            get { return _leftJutsuEnableOnGround_field; }
            set {
                _leftJutsuEnableOnGround_field = value;
                OnPropertyChanged("LeftJutsuEnableOnGround_field");
            }
        }
        private bool _leftJutsuEnableInAir_field;
        public bool LeftJutsuEnableInAir_field {
            get { return _leftJutsuEnableInAir_field; }
            set {
                _leftJutsuEnableInAir_field = value;
                OnPropertyChanged("LeftJutsuEnableInAir_field");
            }
        }
        private bool _leftJutsuEnableSpecialCondition_field;
        public bool LeftJutsuEnableSpecialCondition_field {
            get { return _leftJutsuEnableSpecialCondition_field; }
            set {
                _leftJutsuEnableSpecialCondition_field = value;
                OnPropertyChanged("LeftJutsuEnableSpecialCondition_field");
            }
        }
        private int _leftJutsuCostumeIndex_field;
        public int LeftJutsuCostumeIndex_field {
            get { return _leftJutsuCostumeIndex_field; }
            set {
                _leftJutsuCostumeIndex_field = value;
                OnPropertyChanged("LeftJutsuCostumeIndex_field");
            }
        }
        private string _rightJutsuName_field;
        public string RightJutsuName_field {
            get { return _rightJutsuName_field; }
            set {
                _rightJutsuName_field = value;
                OnPropertyChanged("RightJutsuName_field");
            }
        }
        private bool _rightJutsuEnableOnGround_field;
        public bool RightJutsuEnableOnGround_field {
            get { return _rightJutsuEnableOnGround_field; }
            set {
                _rightJutsuEnableOnGround_field = value;
                OnPropertyChanged("RightJutsuEnableOnGround_field");
            }
        }
        private bool _rightJutsuEnableInAir_field;
        public bool RightJutsuEnableInAir_field {
            get { return _rightJutsuEnableInAir_field; }
            set {
                _rightJutsuEnableInAir_field = value;
                OnPropertyChanged("RightJutsuEnableInAir_field");
            }
        }
        private bool _rightJutsuEnableSpecialCondition_field;
        public bool RightJutsuEnableSpecialCondition_field {
            get { return _rightJutsuEnableSpecialCondition_field; }
            set {
                _rightJutsuEnableSpecialCondition_field = value;
                OnPropertyChanged("RightJutsuEnableSpecialCondition_field");
            }
        }
        private int _rightJutsuCostumeIndex_field;
        public int RightJutsuCostumeIndex_field {
            get { return _rightJutsuCostumeIndex_field; }
            set {
                _rightJutsuCostumeIndex_field = value;
                OnPropertyChanged("RightJutsuCostumeIndex_field");
            }
        }
        private string _upJutsuName_field;
        public string UpJutsuName_field {
            get { return _upJutsuName_field; }
            set {
                _upJutsuName_field = value;
                OnPropertyChanged("UpJutsuName_field");
            }
        }
        private bool _upJutsuEnableOnGround_field;
        public bool UpJutsuEnableOnGround_field {
            get { return _upJutsuEnableOnGround_field; }
            set {
                _upJutsuEnableOnGround_field = value;
                OnPropertyChanged("UpJutsuEnableOnGround_field");
            }
        }
        private bool _upJutsuEnableInAir_field;
        public bool UpJutsuEnableInAir_field {
            get { return _upJutsuEnableInAir_field; }
            set {
                _upJutsuEnableInAir_field = value;
                OnPropertyChanged("UpJutsuEnableInAir_field");
            }
        }
        private bool _upJutsuEnableSpecialCondition_field;
        public bool UpJutsuEnableSpecialCondition_field {
            get { return _upJutsuEnableSpecialCondition_field; }
            set {
                _upJutsuEnableSpecialCondition_field = value;
                OnPropertyChanged("UpJutsuEnableSpecialCondition_field");
            }
        }
        private int _upJutsuCostumeIndex_field;
        public int UpJutsuCostumeIndex_field {
            get { return _upJutsuCostumeIndex_field; }
            set {
                _upJutsuCostumeIndex_field = value;
                OnPropertyChanged("UpJutsuCostumeIndex_field");
            }
        }
        private string _downJutsuName_field;
        public string DownJutsuName_field {
            get { return _downJutsuName_field; }
            set {
                _downJutsuName_field = value;
                OnPropertyChanged("DownJutsuName_field");
            }
        }
        private bool _downJutsuEnableOnGround_field;
        public bool DownJutsuEnableOnGround_field {
            get { return _downJutsuEnableOnGround_field; }
            set {
                _downJutsuEnableOnGround_field = value;
                OnPropertyChanged("DownJutsuEnableOnGround_field");
            }
        }
        private bool _downJutsuEnableInAir_field;
        public bool DownJutsuEnableInAir_field {
            get { return _downJutsuEnableInAir_field; }
            set {
                _downJutsuEnableInAir_field = value;
                OnPropertyChanged("DownJutsuEnableInAir_field");
            }
        }
        private bool _downJutsuEnableSpecialCondition_field;
        public bool DownJutsuEnableSpecialCondition_field {
            get { return _downJutsuEnableSpecialCondition_field; }
            set {
                _downJutsuEnableSpecialCondition_field = value;
                OnPropertyChanged("DownJutsuEnableSpecialCondition_field");
            }
        }
        private int _downJutsuCostumeIndex_field;
        public int DownJutsuCostumeIndex_field {
            get { return _downJutsuCostumeIndex_field; }
            set {
                _downJutsuCostumeIndex_field = value;
                OnPropertyChanged("DownJutsuCostumeIndex_field");
            }
        }

        public ObservableCollection<SpTypeSupportParamModel> SpTypeSupportParamList { get; set; }
        private SpTypeSupportParamModel _selectedSpTypeSupport;
        public SpTypeSupportParamModel SelectedSpTypeSupport {
            get { return _selectedSpTypeSupport; }
            set {
                _selectedSpTypeSupport = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    State_field = value.State;
                    Direction_field = value.Direction;
                    LeftJutsuName_field = value.LeftJutsuName;
                    LeftJutsuCostumeIndex_field = value.LeftJutsuCostumeIndex;
                    LeftJutsuEnableOnGround_field = value.LeftJutsuEnableOnGround;
                    LeftJutsuEnableInAir_field = value.LeftJutsuEnableInAir;
                    LeftJutsuEnableSpecialCondition_field = value.LeftJutsuEnableSpecialCondition;
                    RightJutsuName_field = value.RightJutsuName;
                    RightJutsuCostumeIndex_field = value.RightJutsuCostumeIndex;
                    RightJutsuEnableOnGround_field = value.RightJutsuEnableOnGround;
                    RightJutsuEnableInAir_field = value.RightJutsuEnableInAir;
                    RightJutsuEnableSpecialCondition_field = value.RightJutsuEnableSpecialCondition;
                    UpJutsuName_field = value.UpJutsuName;
                    UpJutsuCostumeIndex_field = value.UpJutsuCostumeIndex;
                    UpJutsuEnableOnGround_field = value.UpJutsuEnableOnGround;
                    UpJutsuEnableInAir_field = value.UpJutsuEnableInAir;
                    UpJutsuEnableSpecialCondition_field = value.UpJutsuEnableSpecialCondition;
                    DownJutsuName_field = value.DownJutsuName;
                    DownJutsuCostumeIndex_field = value.DownJutsuCostumeIndex;
                    DownJutsuEnableOnGround_field = value.DownJutsuEnableOnGround;
                    DownJutsuEnableInAir_field = value.DownJutsuEnableInAir;
                    DownJutsuEnableSpecialCondition_field = value.DownJutsuEnableSpecialCondition;
                }

                OnPropertyChanged("SelectedSpTypeSupport");
            }
        }
        private int _selectedSpTypeSupportIndex;
        public int SelectedSpTypeSupportIndex {
            get { return _selectedSpTypeSupportIndex; }
            set {
                _selectedSpTypeSupportIndex = value;
                OnPropertyChanged("SelectedSpTypeSupportIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public SpTypeSupportParamViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            SpTypeSupportParamList = new ObservableCollection<SpTypeSupportParamModel>();
            filePath = "";
        }

        public void Clear() {
            SpTypeSupportParamList.Clear();
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
                if (BinName.Contains("spTypeSupportParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0xB0);
                        SpTypeSupportParamModel SpTypeSupportEntry = new SpTypeSupportParamModel();
                        SpTypeSupportEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        SpTypeSupportEntry.Direction = BinaryReader.b_ReadInt(fileByte, ptr + 0x04);
                        SpTypeSupportEntry.State = BinaryReader.b_ReadInt(fileByte, ptr + 0x08);
                        SpTypeSupportEntry.UpJutsuName = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        SpTypeSupportEntry.UpJutsuEnableOnGround = BinaryReader.b_ReadInt(fileByte, ptr + 0x18) == 1;
                        SpTypeSupportEntry.UpJutsuEnableInAir = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C) == 1;
                        SpTypeSupportEntry.UpJutsuEnableSpecialCondition = BinaryReader.b_ReadInt(fileByte, ptr + 0x20) == 1;
                        SpTypeSupportEntry.UpJutsuCostumeIndex = BinaryReader.b_ReadInt(fileByte, ptr + 0x24);

                        SpTypeSupportEntry.DownJutsuName = BinaryReader.b_ReadString(fileByte, ptr + 0x38 + BinaryReader.b_ReadInt(fileByte, ptr + 0x38));
                        SpTypeSupportEntry.DownJutsuEnableOnGround = BinaryReader.b_ReadInt(fileByte, ptr + 0x40) == 1;
                        SpTypeSupportEntry.DownJutsuEnableInAir = BinaryReader.b_ReadInt(fileByte, ptr + 0x44) == 1;
                        SpTypeSupportEntry.DownJutsuEnableSpecialCondition = BinaryReader.b_ReadInt(fileByte, ptr + 0x48) == 1;
                        SpTypeSupportEntry.DownJutsuCostumeIndex = BinaryReader.b_ReadInt(fileByte, ptr + 0x4C);

                        SpTypeSupportEntry.LeftJutsuName = BinaryReader.b_ReadString(fileByte, ptr + 0x60 + BinaryReader.b_ReadInt(fileByte, ptr + 0x60));
                        SpTypeSupportEntry.LeftJutsuEnableOnGround = BinaryReader.b_ReadInt(fileByte, ptr + 0x68) == 1;
                        SpTypeSupportEntry.LeftJutsuEnableInAir = BinaryReader.b_ReadInt(fileByte, ptr + 0x6C) == 1;
                        SpTypeSupportEntry.LeftJutsuEnableSpecialCondition = BinaryReader.b_ReadInt(fileByte, ptr + 0x70) == 1;
                        SpTypeSupportEntry.LeftJutsuCostumeIndex = BinaryReader.b_ReadInt(fileByte, ptr + 0x74);

                        SpTypeSupportEntry.RightJutsuName = BinaryReader.b_ReadString(fileByte, ptr + 0x88 + BinaryReader.b_ReadInt(fileByte, ptr + 0x88));
                        SpTypeSupportEntry.RightJutsuEnableOnGround = BinaryReader.b_ReadInt(fileByte, ptr + 0x90) == 1;
                        SpTypeSupportEntry.RightJutsuEnableInAir = BinaryReader.b_ReadInt(fileByte, ptr + 0x94) == 1;
                        SpTypeSupportEntry.RightJutsuEnableSpecialCondition = BinaryReader.b_ReadInt(fileByte, ptr + 0x98) == 1;
                        SpTypeSupportEntry.RightJutsuCostumeIndex = BinaryReader.b_ReadInt(fileByte, ptr + 0x9C);

                        SpTypeSupportParamList.Add(SpTypeSupportEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedSpTypeSupport is not null) {
                SpTypeSupportParamList.Remove(SelectedSpTypeSupport);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedSpTypeSupport is not null) {
                SelectedSpTypeSupport.CharacodeID = CharacodeID_field;
                SelectedSpTypeSupport.State = State_field;
                SelectedSpTypeSupport.Direction = Direction_field;
                SelectedSpTypeSupport.LeftJutsuName = LeftJutsuName_field;
                SelectedSpTypeSupport.LeftJutsuCostumeIndex = LeftJutsuCostumeIndex_field;
                SelectedSpTypeSupport.LeftJutsuEnableOnGround = LeftJutsuEnableOnGround_field;
                SelectedSpTypeSupport.LeftJutsuEnableInAir = LeftJutsuEnableInAir_field;
                SelectedSpTypeSupport.LeftJutsuEnableSpecialCondition = LeftJutsuEnableSpecialCondition_field;
                SelectedSpTypeSupport.RightJutsuName = RightJutsuName_field;
                SelectedSpTypeSupport.RightJutsuCostumeIndex = RightJutsuCostumeIndex_field;
                SelectedSpTypeSupport.RightJutsuEnableOnGround = RightJutsuEnableOnGround_field;
                SelectedSpTypeSupport.RightJutsuEnableInAir = RightJutsuEnableInAir_field;
                SelectedSpTypeSupport.RightJutsuEnableSpecialCondition = RightJutsuEnableSpecialCondition_field;
                SelectedSpTypeSupport.UpJutsuName = UpJutsuName_field;
                SelectedSpTypeSupport.UpJutsuCostumeIndex = UpJutsuCostumeIndex_field;
                SelectedSpTypeSupport.UpJutsuEnableOnGround = UpJutsuEnableOnGround_field;
                SelectedSpTypeSupport.UpJutsuEnableInAir = UpJutsuEnableInAir_field;
                SelectedSpTypeSupport.UpJutsuEnableSpecialCondition = UpJutsuEnableSpecialCondition_field;
                SelectedSpTypeSupport.DownJutsuName = DownJutsuName_field;
                SelectedSpTypeSupport.DownJutsuCostumeIndex = DownJutsuCostumeIndex_field;
                SelectedSpTypeSupport.DownJutsuEnableOnGround = DownJutsuEnableOnGround_field;
                SelectedSpTypeSupport.DownJutsuEnableInAir = DownJutsuEnableInAir_field;
                SelectedSpTypeSupport.DownJutsuEnableSpecialCondition = DownJutsuEnableSpecialCondition_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<SpTypeSupportParamModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].CharacodeID == member_index) {
                    return x;
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(SpTypeSupportParamList, SearchIndex_field, SelectedSpTypeSupportIndex) != -1) {
                    SelectedSpTypeSupportIndex = SearchByteIndex(SpTypeSupportParamList, SearchIndex_field, SelectedSpTypeSupportIndex);
                    CollectionViewSource.GetDefaultView(SpTypeSupportParamList).MoveCurrentTo(SelectedSpTypeSupportIndex);
                } else {
                    if (SearchByteIndex(SpTypeSupportParamList, SearchIndex_field, 0) != -1) {
                        SelectedSpTypeSupportIndex = SearchByteIndex(SpTypeSupportParamList, SearchIndex_field, -1);
                        CollectionViewSource.GetDefaultView(SpTypeSupportParamList).MoveCurrentTo(SelectedSpTypeSupportIndex);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            SpTypeSupportParamModel SpTypeSupportEntry = new SpTypeSupportParamModel();
            if (SelectedSpTypeSupport is not null) {
                SpTypeSupportEntry = (SpTypeSupportParamModel)SelectedSpTypeSupport.Clone();
            } else {
                SpTypeSupportEntry.CharacodeID = 0;
                SpTypeSupportEntry.State = 0;
                SpTypeSupportEntry.Direction = 0;
                SpTypeSupportEntry.UpJutsuName = "";
                SpTypeSupportEntry.UpJutsuEnableOnGround = false;
                SpTypeSupportEntry.UpJutsuEnableInAir = false;
                SpTypeSupportEntry.UpJutsuEnableSpecialCondition = false;
                SpTypeSupportEntry.UpJutsuCostumeIndex = -1;

                SpTypeSupportEntry.DownJutsuName = "";
                SpTypeSupportEntry.DownJutsuEnableOnGround = false;
                SpTypeSupportEntry.DownJutsuEnableInAir = false;
                SpTypeSupportEntry.DownJutsuEnableSpecialCondition = false;
                SpTypeSupportEntry.DownJutsuCostumeIndex = -1;

                SpTypeSupportEntry.LeftJutsuName = "";
                SpTypeSupportEntry.LeftJutsuEnableOnGround = true;
                SpTypeSupportEntry.LeftJutsuEnableInAir = false;
                SpTypeSupportEntry.LeftJutsuEnableSpecialCondition = false;
                SpTypeSupportEntry.LeftJutsuCostumeIndex = -1;

                SpTypeSupportEntry.RightJutsuName = "";
                SpTypeSupportEntry.RightJutsuEnableOnGround = true;
                SpTypeSupportEntry.RightJutsuEnableInAir = false;
                SpTypeSupportEntry.RightJutsuEnableSpecialCondition = false;
                SpTypeSupportEntry.RightJutsuCostumeIndex = -1;
            }
            SpTypeSupportParamList.Add(SpTypeSupportEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/spTypeSupportParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "spTypeSupportParam");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[SpTypeSupportParamList.Count * 0xB0]);

            int addSize = 0;

            List<int> UpName_pointer = new List<int>();
            List<int> DownName_pointer = new List<int>();
            List<int> LeftName_pointer = new List<int>();
            List<int> RightName_pointer = new List<int>();
            for (int x = 0; x < SpTypeSupportParamList.Count; x++) {
                int ptr = startPtr + (x * 0xB0);

                UpName_pointer.Add(fileBytes36.Length);
                if (SpTypeSupportParamList[x].UpJutsuName != "" && SpTypeSupportParamList[x].UpJutsuName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpTypeSupportParamList[x].UpJutsuName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = UpName_pointer[x] - startPtr - x * 0xB0 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += SpTypeSupportParamList[x].UpJutsuName.Length + 1;
                }
                DownName_pointer.Add(fileBytes36.Length);
                if (SpTypeSupportParamList[x].DownJutsuName != "" && SpTypeSupportParamList[x].DownJutsuName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpTypeSupportParamList[x].DownJutsuName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = DownName_pointer[x] - startPtr - x * 0xB0 - 0x38;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x38);
                    addSize += SpTypeSupportParamList[x].DownJutsuName.Length + 1;
                }
                LeftName_pointer.Add(fileBytes36.Length);
                if (SpTypeSupportParamList[x].LeftJutsuName != "" && SpTypeSupportParamList[x].LeftJutsuName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpTypeSupportParamList[x].LeftJutsuName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = LeftName_pointer[x] - startPtr - x * 0xB0 - 0x60;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x60);
                    addSize += SpTypeSupportParamList[x].LeftJutsuName.Length + 1;
                }
                RightName_pointer.Add(fileBytes36.Length);
                if (SpTypeSupportParamList[x].RightJutsuName != "" && SpTypeSupportParamList[x].RightJutsuName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpTypeSupportParamList[x].RightJutsuName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = RightName_pointer[x] - startPtr - x * 0xB0 - 0x88;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x88);
                    addSize += SpTypeSupportParamList[x].RightJutsuName.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].Direction), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].State), ptr + 0x08);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].UpJutsuEnableOnGround), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].UpJutsuEnableInAir), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].UpJutsuEnableSpecialCondition), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].UpJutsuCostumeIndex), ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x28);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x2C);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].DownJutsuEnableOnGround), ptr + 0x40);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].DownJutsuEnableInAir), ptr + 0x44);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].DownJutsuEnableSpecialCondition), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].DownJutsuCostumeIndex), ptr + 0x4C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x54);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].LeftJutsuEnableOnGround), ptr + 0x68);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].LeftJutsuEnableInAir), ptr + 0x6C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].LeftJutsuEnableSpecialCondition), ptr + 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].LeftJutsuCostumeIndex), ptr + 0x74);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x78);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0x7C);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].RightJutsuEnableOnGround), ptr + 0x90);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].RightJutsuEnableInAir), ptr + 0x94);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].RightJutsuEnableSpecialCondition), ptr + 0x98);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList[x].RightJutsuCostumeIndex), ptr + 0x9C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0xA0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((int)-1), ptr + 0xA4);


            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SpTypeSupportParamList.Count * 0xB0) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SpTypeSupportParamList.Count * 0xB0) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpTypeSupportParamList.Count), count_index);
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
