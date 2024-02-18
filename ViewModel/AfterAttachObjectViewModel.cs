using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static System.Windows.Forms.AxHost;

namespace NSC_ModManager.ViewModel {
    public class AfterAttachObjectViewModel : INotifyPropertyChanged {
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
        private string _costume;
        public string Costume_field {
            get { return _costume; }
            set {
                _costume = value;
                OnPropertyChanged("Costume_field");
            }
        }
        private string _attachBone1;
        public string AttachBone1_field {
            get { return _attachBone1; }
            set {
                _attachBone1 = value;
                OnPropertyChanged("AttachBone1_field");
            }
        }
        private string _filePath;
        public string FilePath_field {
            get { return _filePath; }
            set {
                _filePath = value;
                OnPropertyChanged("FilePath_field");
            }
        }
        private int _type;
        public int Type_field {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type_field");
            }
        }
        private string _chunkName;
        public string ChunkName_field {
            get { return _chunkName; }
            set {
                _chunkName = value;
                OnPropertyChanged("ChunkName_field");
            }
        }
        private string _attachBone2;
        public string AttachBone2_field {
            get { return _attachBone2; }
            set {
                _attachBone2 = value;
                OnPropertyChanged("AttachBone2_field");
            }
        }
        private float _posX;
        public float PosX_field {
            get { return _posX; }
            set {
                _posX = value;
                OnPropertyChanged("PosX_field");
            }
        }
        private float _posY;
        public float PosY_field {
            get { return _posY; }
            set {
                _posY = value;
                OnPropertyChanged("PosY_field");
            }
        }
        private float _posZ;
        public float PosZ_field {
            get { return _posZ; }
            set {
                _posZ = value;
                OnPropertyChanged("PosZ_field");
            }
        }
        private float _rotX;
        public float RotX_field {
            get { return _rotX; }
            set {
                _rotX = value;
                OnPropertyChanged("RotX_field");
            }
        }
        private float _rotY;
        public float RotY_field {
            get { return _rotY; }
            set {
                _rotY = value;
                OnPropertyChanged("RotY_field");
            }
        }
        private float _rotZ;
        public float RotZ_field {
            get { return _rotZ; }
            set {
                _rotZ = value;
                OnPropertyChanged("RotZ_field");
            }
        }
        private float _scaleX;
        public float ScaleX_field {
            get { return _scaleX; }
            set {
                _scaleX = value;
                OnPropertyChanged("ScaleX_field");
            }
        }
        private float _scaleY;
        public float ScaleY_field {
            get { return _scaleY; }
            set {
                _scaleY = value;
                OnPropertyChanged("ScaleY_field");
            }
        }
        private float _scaleZ;
        public float ScaleZ_field {
            get { return _scaleZ; }
            set {
                _scaleZ = value;
                OnPropertyChanged("ScaleZ_field");
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
        private int _state;
        public int State_field {
            get { return _state; }
            set {
                _state = value;
                OnPropertyChanged("State_field");
            }
        }
        public ObservableCollection<AfterAttachObjectModel> AfterAttachObjectList { get; set; }
        private AfterAttachObjectModel _selectedAfterAttachObject;
        public AfterAttachObjectModel SelectedAfterAttachObject {
            get { return _selectedAfterAttachObject; }
            set {
                _selectedAfterAttachObject = value;
                if (value is not null) {
                    Characode_field = value.Characode;
                    Costume_field = value.Costume;
                    AttachBone1_field = value.AttachBone1;
                    FilePath_field = value.FilePath;
                    Type_field = value.Type;
                    ChunkName_field = value.ChunkName;
                    AttachBone2_field = value.AttachBone2;
                    PosX_field = value.PosX;
                    PosY_field = value.PosY;
                    PosZ_field = value.PosZ;
                    RotX_field = value.RotX;
                    RotY_field = value.RotY;
                    RotZ_field = value.RotZ;
                    ScaleX_field = value.ScaleX;
                    ScaleY_field = value.ScaleY;
                    ScaleZ_field = value.ScaleZ;
                    Condition_field = value.Condition;
                    State_field = value.State;
                }
                OnPropertyChanged("SelectedAfterAttachObject");
            }
        }
        private int _selectedAfterAttachObjectIndex;
        public int SelectedAfterAttachObjectIndex {
            get { return _selectedAfterAttachObjectIndex; }
            set {
                _selectedAfterAttachObjectIndex = value;
                OnPropertyChanged("SelectedAfterAttachObjectIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public AfterAttachObjectViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            AfterAttachObjectList = new ObservableCollection<AfterAttachObjectModel>();
            filePath = "";
        }

        public void Clear() {
            AfterAttachObjectList.Clear();
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
                if (BinName.Contains("afterAttachObject")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x68);
                        AfterAttachObjectModel AfterAttachObjectEntry = new AfterAttachObjectModel();
                        AfterAttachObjectEntry.Characode = BinaryReader.b_ReadString(fileByte, ptr + BinaryReader.b_ReadInt(fileByte, ptr));
                        AfterAttachObjectEntry.Costume = BinaryReader.b_ReadString(fileByte, ptr + 0x08 + BinaryReader.b_ReadInt(fileByte, ptr + 0x08));
                        AfterAttachObjectEntry.AttachBone1 = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        AfterAttachObjectEntry.FilePath = BinaryReader.b_ReadString(fileByte, ptr + 0x18 + BinaryReader.b_ReadInt(fileByte, ptr + 0x18));
                        AfterAttachObjectEntry.Type = BinaryReader.b_ReadInt(fileByte, ptr + 0x20);
                        AfterAttachObjectEntry.ChunkName = BinaryReader.b_ReadString(fileByte, ptr + 0x28 + BinaryReader.b_ReadInt(fileByte, ptr + 0x28));
                        AfterAttachObjectEntry.AttachBone2 = BinaryReader.b_ReadString(fileByte, ptr + 0x30 + BinaryReader.b_ReadInt(fileByte, ptr + 0x30));
                        AfterAttachObjectEntry.PosX = BinaryReader.b_ReadFloat(fileByte, ptr + 0x38);
                        AfterAttachObjectEntry.PosY = BinaryReader.b_ReadFloat(fileByte, ptr + 0x3C);
                        AfterAttachObjectEntry.PosZ = BinaryReader.b_ReadFloat(fileByte, ptr + 0x40);
                        AfterAttachObjectEntry.RotX = BinaryReader.b_ReadFloat(fileByte, ptr + 0x44);
                        AfterAttachObjectEntry.RotY = BinaryReader.b_ReadFloat(fileByte, ptr + 0x48);
                        AfterAttachObjectEntry.RotZ = BinaryReader.b_ReadFloat(fileByte, ptr + 0x4C);
                        AfterAttachObjectEntry.ScaleX = BinaryReader.b_ReadFloat(fileByte, ptr + 0x50);
                        AfterAttachObjectEntry.ScaleY = BinaryReader.b_ReadFloat(fileByte, ptr + 0x54);
                        AfterAttachObjectEntry.ScaleZ = BinaryReader.b_ReadFloat(fileByte, ptr + 0x58);
                        AfterAttachObjectEntry.Condition = BinaryReader.b_ReadInt(fileByte, ptr + 0x60);
                        AfterAttachObjectEntry.State = BinaryReader.b_ReadInt(fileByte, ptr + 0x64);
                        AfterAttachObjectList.Add(AfterAttachObjectEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedAfterAttachObject is not null) {
                AfterAttachObjectList.Remove(SelectedAfterAttachObject);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedAfterAttachObject is not null) {
                SelectedAfterAttachObject.Characode = Characode_field;
                SelectedAfterAttachObject.Costume = Costume_field;
                SelectedAfterAttachObject.AttachBone1 = AttachBone1_field;
                SelectedAfterAttachObject.FilePath = FilePath_field;
                SelectedAfterAttachObject.Type = Type_field;
                SelectedAfterAttachObject.ChunkName = ChunkName_field;
                SelectedAfterAttachObject.AttachBone2 = AttachBone2_field;
                SelectedAfterAttachObject.PosX = PosX_field;
                SelectedAfterAttachObject.PosY = PosY_field;
                SelectedAfterAttachObject.PosZ = PosZ_field;
                SelectedAfterAttachObject.RotX = RotX_field;
                SelectedAfterAttachObject.RotY = RotY_field;
                SelectedAfterAttachObject.RotZ = RotZ_field;
                SelectedAfterAttachObject.ScaleX = ScaleX_field;
                SelectedAfterAttachObject.ScaleY = ScaleY_field;
                SelectedAfterAttachObject.ScaleZ = ScaleZ_field;
                SelectedAfterAttachObject.Condition = Condition_field;
                SelectedAfterAttachObject.State = State_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchStringIndex(ObservableCollection<AfterAttachObjectModel> FunctionList, string member_name, int Selected) {
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
                if (SearchStringIndex(AfterAttachObjectList, SearchTextBox_field, SelectedAfterAttachObjectIndex) != -1) {
                    SelectedAfterAttachObjectIndex = SearchStringIndex(AfterAttachObjectList, SearchTextBox_field, SelectedAfterAttachObjectIndex);
                    CollectionViewSource.GetDefaultView(AfterAttachObjectList).MoveCurrentTo(SelectedAfterAttachObject);
                } else {
                    if (SearchStringIndex(AfterAttachObjectList, SearchTextBox_field, 0) != -1) {
                        SelectedAfterAttachObjectIndex = SearchStringIndex(AfterAttachObjectList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(AfterAttachObjectList).MoveCurrentTo(SelectedAfterAttachObject);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that characode.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }


        public void AddDupEntry() {
            AfterAttachObjectModel AfterAttachEntry = new AfterAttachObjectModel();
            if (SelectedAfterAttachObject is not null) {
                AfterAttachEntry = (AfterAttachObjectModel)SelectedAfterAttachObject.Clone();
            } else {
                AfterAttachEntry.Characode = "";
                AfterAttachEntry.Costume = "";
                AfterAttachEntry.AttachBone1 = "";
                AfterAttachEntry.FilePath = "";
                AfterAttachEntry.Type = 0;
                AfterAttachEntry.ChunkName = "";
                AfterAttachEntry.AttachBone2 = "";
                AfterAttachEntry.PosX = 0;
                AfterAttachEntry.PosY = 0;
                AfterAttachEntry.PosZ = 0;
                AfterAttachEntry.RotX = 0;
                AfterAttachEntry.RotY = 0;
                AfterAttachEntry.RotZ = 0;
                AfterAttachEntry.ScaleX = 1;
                AfterAttachEntry.ScaleY = 1;
                AfterAttachEntry.ScaleZ = 1;
                AfterAttachEntry.Condition = 1;
                AfterAttachEntry.State = 0;
            }
            AfterAttachObjectList.Add(AfterAttachEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/afterAttachObject.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "afterAttachObject");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[AfterAttachObjectList.Count * 0x68]);

            int addSize = 0;

            List<int> characode_pointer = new List<int>();
            List<int> costume_pointer = new List<int>();
            List<int> attachBone1_pointer = new List<int>();
            List<int> filePath_pointer = new List<int>();
            List<int> chunkname_pointer = new List<int>();
            List<int> attachBone2_pointer = new List<int>();
            for (int x = 0; x < AfterAttachObjectList.Count; x++) {
                int ptr = startPtr + (x * 0x68);
                characode_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].Characode != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].Characode));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = characode_pointer[x] - startPtr - x * 0x68;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr);
                    addSize += AfterAttachObjectList[x].Characode.Length + 1;
                }
                costume_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].Costume != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].Costume));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = costume_pointer[x] - startPtr - x * 0x68 - 0x08;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x08);
                    addSize += AfterAttachObjectList[x].Costume.Length + 1;
                }
                attachBone1_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].AttachBone1 != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].AttachBone1));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = attachBone1_pointer[x] - startPtr - x * 0x68 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += AfterAttachObjectList[x].AttachBone1.Length + 1;
                }
                filePath_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].FilePath != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].FilePath));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = filePath_pointer[x] - startPtr - x * 0x68 - 0x18;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x18);
                    addSize += AfterAttachObjectList[x].FilePath.Length + 1;
                }
                chunkname_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].ChunkName != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].ChunkName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = chunkname_pointer[x] - startPtr - x * 0x68 - 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x28);
                    addSize += AfterAttachObjectList[x].ChunkName.Length + 1;
                }
                attachBone2_pointer.Add(fileBytes36.Length);
                if (AfterAttachObjectList[x].AttachBone2 != "" && AfterAttachObjectList[x].Characode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AfterAttachObjectList[x].AttachBone2));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = attachBone2_pointer[x] - startPtr - x * 0x68 - 0x30;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x30);
                    addSize += AfterAttachObjectList[x].AttachBone2.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].Type), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].PosX), ptr + 0x38);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].PosY), ptr + 0x3C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].PosZ), ptr + 0x40);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].RotX), ptr + 0x44);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].RotY), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].RotZ), ptr + 0x4C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].ScaleX), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].ScaleY), ptr + 0x54);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].ScaleZ), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].Condition), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList[x].State), ptr + 0x64);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AfterAttachObjectList.Count * 0x68) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AfterAttachObjectList.Count * 0x68) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AfterAttachObjectList.Count), count_index);
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
