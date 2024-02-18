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

namespace NSC_ModManager.ViewModel
{
    public class SpSkillCustomizeParamViewModel : INotifyPropertyChanged
    {
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

        private float _ultimate1Cost;
        public float Ultimate1Cost_field {
            get { return _ultimate1Cost; }
            set {
                _ultimate1Cost = value;
                OnPropertyChanged("Ultimate1Cost_field");
            }
        }
        private int _ultimate1prior;
        public int Ultimate1prior_field {
            get { return _ultimate1prior; }
            set {
                _ultimate1prior = value;
                OnPropertyChanged("Ultimate1prior_field");
            }
        }
        private string _ultimate1name;
        public string Ultimate1name_field {
            get { return _ultimate1name; }
            set {
                _ultimate1name = value;
                OnPropertyChanged("Ultimate1name_field");
            }
        }
        private float _ultimate2Cost;
        public float Ultimate2Cost_field {
            get { return _ultimate2Cost; }
            set {
                _ultimate2Cost = value;
                OnPropertyChanged("Ultimate2Cost_field");
            }
        }
        private int _ultimate2prior;
        public int Ultimate2prior_field {
            get { return _ultimate2prior; }
            set {
                _ultimate2prior = value;
                OnPropertyChanged("Ultimate2prior_field");
            }
        }
        private string _ultimate2name;
        public string Ultimate2name_field {
            get { return _ultimate2name; }
            set {
                _ultimate2name = value;
                OnPropertyChanged("Ultimate2name_field");
            }
        }
        private float _ultimate3Cost;
        public float Ultimate3Cost_field {
            get { return _ultimate3Cost; }
            set {
                _ultimate3Cost = value;
                OnPropertyChanged("Ultimate3Cost_field");
            }
        }
        private int _ultimate3prior;
        public int Ultimate3prior_field {
            get { return _ultimate3prior; }
            set {
                _ultimate3prior = value;
                OnPropertyChanged("Ultimate3prior_field");
            }
        }
        private string _ultimate3name;
        public string Ultimate3name_field {
            get { return _ultimate3name; }
            set {
                _ultimate3name = value;
                OnPropertyChanged("Ultimate3name_field");
            }
        }
        private float _ultimate4Cost;
        public float Ultimate4Cost_field {
            get { return _ultimate4Cost; }
            set {
                _ultimate4Cost = value;
                OnPropertyChanged("Ultimate4Cost_field");
            }
        }
        private int _ultimate4prior;
        public int Ultimate4prior_field {
            get { return _ultimate4prior; }
            set {
                _ultimate4prior = value;
                OnPropertyChanged("Ultimate4prior_field");
            }
        }
        private string _ultimate4name;
        public string Ultimate4name_field {
            get { return _ultimate4name; }
            set {
                _ultimate4name = value;
                OnPropertyChanged("Ultimate4name_field");
            }
        }
        private float _ultimate1DamageMultiplier;
        public float Ultimate1DamageMultiplier_field {
            get { return _ultimate1DamageMultiplier; }
            set {
                _ultimate1DamageMultiplier = value;
                OnPropertyChanged("Ultimate1DamageMultiplier_field");
            }
        }
        private float _ultimate2DamageMultiplier;
        public float Ultimate2DamageMultiplier_field {
            get { return _ultimate2DamageMultiplier; }
            set {
                _ultimate2DamageMultiplier = value;
                OnPropertyChanged("Ultimate2DamageMultiplier_field");
            }
        }

        private float _ultimate3DamageMultiplier;
        public float Ultimate3DamageMultiplier_field {
            get { return _ultimate3DamageMultiplier; }
            set {
                _ultimate3DamageMultiplier = value;
                OnPropertyChanged("Ultimate3DamageMultiplier_field");
            }
        }

        private float _ultimate4DamageMultiplier;
        public float Ultimate4DamageMultiplier_field {
            get { return _ultimate4DamageMultiplier; }
            set {
                _ultimate4DamageMultiplier = value;
                OnPropertyChanged("Ultimate4DamageMultiplier_field");
            }
        }
        public ObservableCollection<SpSkillCustomizeParamModel> SpSkillCustomizeParamList { get; set; }
        private SpSkillCustomizeParamModel _selectedSpSkill;
        public SpSkillCustomizeParamModel SelectedSpSkill {
            get { return _selectedSpSkill; }
            set {
                _selectedSpSkill = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    Ultimate1Cost_field = value.Ultimate1Cost;
                    Ultimate1name_field = value.Ultimate1name;
                    Ultimate1prior_field = value.Ultimate1prior;
                    Ultimate2Cost_field = value.Ultimate2Cost;
                    Ultimate2name_field = value.Ultimate2name;
                    Ultimate2prior_field = value.Ultimate2prior;
                    Ultimate3Cost_field = value.Ultimate3Cost;
                    Ultimate3name_field = value.Ultimate3name;
                    Ultimate3prior_field = value.Ultimate3prior;
                    Ultimate4Cost_field = value.Ultimate4Cost;
                    Ultimate4name_field = value.Ultimate4name;
                    Ultimate4prior_field = value.Ultimate4prior;
                    Ultimate1DamageMultiplier_field = value.Ultimate1DamageMultiplier;
                    Ultimate2DamageMultiplier_field = value.Ultimate2DamageMultiplier;
                    Ultimate3DamageMultiplier_field = value.Ultimate3DamageMultiplier;
                    Ultimate4DamageMultiplier_field = value.Ultimate4DamageMultiplier;
                }

                OnPropertyChanged("SelectedSpSkill");
            }
        }
        private int _selectedSpSkillIndex;
        public int SelectedSpSkillIndex {
            get { return _selectedSpSkillIndex; }
            set {
                _selectedSpSkillIndex = value;
                OnPropertyChanged("SelectedSpSkillIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public SpSkillCustomizeParamViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            SpSkillCustomizeParamList = new ObservableCollection<SpSkillCustomizeParamModel>();
            filePath = "";
        }

        public void Clear() {
            SpSkillCustomizeParamList.Clear();
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
                if (BinName.Contains("spSkillCustomizeParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x70);
                        SpSkillCustomizeParamModel SpSkillEntry = new SpSkillCustomizeParamModel();
                        SpSkillEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        SpSkillEntry.Ultimate1Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x4);
                        SpSkillEntry.Ultimate2Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x8);
                        SpSkillEntry.Ultimate3Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0xC);
                        SpSkillEntry.Ultimate4Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x10);
                        SpSkillEntry.Ultimate1DamageMultiplier = BinaryReader.b_ReadFloat(fileByte, ptr + 0x18);
                        SpSkillEntry.Ultimate2DamageMultiplier = BinaryReader.b_ReadFloat(fileByte, ptr + 0x1C);
                        SpSkillEntry.Ultimate3DamageMultiplier = BinaryReader.b_ReadFloat(fileByte, ptr + 0x20);
                        SpSkillEntry.Ultimate4DamageMultiplier = BinaryReader.b_ReadFloat(fileByte, ptr + 0x24);

                        SpSkillEntry.Ultimate1name = BinaryReader.b_ReadString(fileByte, ptr + 0x30 + BinaryReader.b_ReadInt(fileByte, ptr + 0x30));
                        SpSkillEntry.Ultimate1prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x38);
                        SpSkillEntry.Ultimate2name = BinaryReader.b_ReadString(fileByte, ptr + 0x40 + BinaryReader.b_ReadInt(fileByte, ptr + 0x40));
                        SpSkillEntry.Ultimate2prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x48);
                        SpSkillEntry.Ultimate3name = BinaryReader.b_ReadString(fileByte, ptr + 0x50 + BinaryReader.b_ReadInt(fileByte, ptr + 0x50));
                        SpSkillEntry.Ultimate3prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x58);
                        SpSkillEntry.Ultimate4name = BinaryReader.b_ReadString(fileByte, ptr + 0x60 + BinaryReader.b_ReadInt(fileByte, ptr + 0x60));
                        SpSkillEntry.Ultimate4prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x68);

                        SpSkillCustomizeParamList.Add(SpSkillEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedSpSkill is not null) {
                SpSkillCustomizeParamList.Remove(SelectedSpSkill);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedSpSkill is not null) {
                SelectedSpSkill.CharacodeID = CharacodeID_field;
                SelectedSpSkill.Ultimate1name = Ultimate1name_field;
                SelectedSpSkill.Ultimate1prior = Ultimate1prior_field;
                SelectedSpSkill.Ultimate1Cost = Ultimate1Cost_field;
                SelectedSpSkill.Ultimate2name = Ultimate2name_field;
                SelectedSpSkill.Ultimate2prior = Ultimate2prior_field;
                SelectedSpSkill.Ultimate2Cost = Ultimate2Cost_field;
                SelectedSpSkill.Ultimate3name = Ultimate3name_field;
                SelectedSpSkill.Ultimate3prior = Ultimate3prior_field;
                SelectedSpSkill.Ultimate3Cost = Ultimate3Cost_field;
                SelectedSpSkill.Ultimate4name = Ultimate4name_field;
                SelectedSpSkill.Ultimate4prior = Ultimate4prior_field;
                SelectedSpSkill.Ultimate4Cost = Ultimate4Cost_field;
                SelectedSpSkill.Ultimate1DamageMultiplier = Ultimate1DamageMultiplier_field;
                SelectedSpSkill.Ultimate2DamageMultiplier = Ultimate2DamageMultiplier_field;
                SelectedSpSkill.Ultimate3DamageMultiplier = Ultimate3DamageMultiplier_field;
                SelectedSpSkill.Ultimate4DamageMultiplier = Ultimate4DamageMultiplier_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<SpSkillCustomizeParamModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].CharacodeID == member_index) {
                    return x;
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(SpSkillCustomizeParamList, SearchIndex_field, SelectedSpSkillIndex) != -1) {
                    SelectedSpSkillIndex = SearchByteIndex(SpSkillCustomizeParamList, SearchIndex_field, SelectedSpSkillIndex);
                    CollectionViewSource.GetDefaultView(SpSkillCustomizeParamList).MoveCurrentTo(SelectedSpSkillIndex);
                } else {
                    if (SearchByteIndex(SpSkillCustomizeParamList, SearchIndex_field, 0) != -1) {
                        SelectedSpSkillIndex = SearchByteIndex(SpSkillCustomizeParamList, SearchIndex_field, -1);
                        CollectionViewSource.GetDefaultView(SpSkillCustomizeParamList).MoveCurrentTo(SelectedSpSkillIndex);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            SpSkillCustomizeParamModel SpSkillEntry = new SpSkillCustomizeParamModel();
            if (SelectedSpSkill is not null) {
                SpSkillEntry = (SpSkillCustomizeParamModel)SelectedSpSkill.Clone();
            } else {
                SpSkillEntry.CharacodeID = 0;
                SpSkillEntry.Ultimate1Cost = 40;
                SpSkillEntry.Ultimate1name = "skill1_message_id";
                SpSkillEntry.Ultimate1prior = 0;
                SpSkillEntry.Ultimate2Cost = 40;
                SpSkillEntry.Ultimate2name = "";
                SpSkillEntry.Ultimate2prior = 0;
                SpSkillEntry.Ultimate3Cost = 40;
                SpSkillEntry.Ultimate3name = "";
                SpSkillEntry.Ultimate3prior = 0;
                SpSkillEntry.Ultimate4Cost = 40;
                SpSkillEntry.Ultimate4name = "";
                SpSkillEntry.Ultimate4prior = 0;
                SpSkillEntry.Ultimate1DamageMultiplier = 30;
                SpSkillEntry.Ultimate2DamageMultiplier = 30;
                SpSkillEntry.Ultimate3DamageMultiplier = 30;
                SpSkillEntry.Ultimate4DamageMultiplier = 30;
            }
            SpSkillCustomizeParamList.Add(SpSkillEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/spSkillCustomizeParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "spSkillCustomizeParam");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[SpSkillCustomizeParamList.Count * 0x70]);

            int addSize = 0;

            List<int> ultimate1_name_pointer = new List<int>();
            List<int> ultimate2_name_pointer = new List<int>();
            List<int> ultimate3_name_pointer = new List<int>();
            List<int> ultimate4_name_pointer = new List<int>();
            for (int x = 0; x < SpSkillCustomizeParamList.Count; x++) {
                int ptr = startPtr + (x * 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[0x28] { 0x00, 0x00, 0x20, 0x42, 0x00, 0x00, 0x20, 0x42, 0x00, 0x00, 0x20, 0x42, 0x00, 0x00, 0x20, 0x42, 0x00, 0x00, 0x20, 0x42, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0xF0, 0x41, 0x00, 0x00, 0xF0, 0x41 }, ptr + 0x04);

                ultimate1_name_pointer.Add(fileBytes36.Length);
                if (SpSkillCustomizeParamList[x].Ultimate1name != "" && SpSkillCustomizeParamList[x].Ultimate1name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpSkillCustomizeParamList[x].Ultimate1name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = ultimate1_name_pointer[x] - startPtr - x * 0x70 - 0x30;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x30);
                    addSize += SpSkillCustomizeParamList[x].Ultimate1name.Length + 1;
                }
                ultimate2_name_pointer.Add(fileBytes36.Length);
                if (SpSkillCustomizeParamList[x].Ultimate2name != "" && SpSkillCustomizeParamList[x].Ultimate2name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpSkillCustomizeParamList[x].Ultimate2name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = ultimate2_name_pointer[x] - startPtr - x * 0x70 - 0x40;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x40);
                    addSize += SpSkillCustomizeParamList[x].Ultimate2name.Length + 1;
                }
                ultimate3_name_pointer.Add(fileBytes36.Length);
                if (SpSkillCustomizeParamList[x].Ultimate3name != "" && SpSkillCustomizeParamList[x].Ultimate3name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpSkillCustomizeParamList[x].Ultimate3name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = ultimate3_name_pointer[x] - startPtr - x * 0x70 - 0x50;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x50);
                    addSize += SpSkillCustomizeParamList[x].Ultimate3name.Length + 1;
                }
                ultimate4_name_pointer.Add(fileBytes36.Length);
                if (SpSkillCustomizeParamList[x].Ultimate4name != "" && SpSkillCustomizeParamList[x].Ultimate4name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SpSkillCustomizeParamList[x].Ultimate4name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = ultimate4_name_pointer[x] - startPtr - x * 0x70 - 0x60;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x60);
                    addSize += SpSkillCustomizeParamList[x].Ultimate4name.Length + 1;
                }

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate1Cost), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate2Cost), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate3Cost), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate4Cost), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate1DamageMultiplier), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate2DamageMultiplier), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate3DamageMultiplier), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate4DamageMultiplier), ptr + 0x24);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate1prior), ptr + 0x38);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate2prior), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate3prior), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList[x].Ultimate4prior), ptr + 0x68);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SpSkillCustomizeParamList.Count * 0x70) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SpSkillCustomizeParamList.Count * 0x70) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SpSkillCustomizeParamList.Count), count_index);
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
