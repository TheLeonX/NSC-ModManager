using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NSC_ModManager.ViewModel
{
    public class CostumeBreakColorParamViewModel : INotifyPropertyChanged {
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

        private int _playerSettingParamID;
        public int PlayerSettingParamID_field {
            get { return _playerSettingParamID; }
            set {
                _playerSettingParamID = value;
                OnPropertyChanged("PlayerSettingParamID_field");
            }
        }
        private Color _altColor1;
        public Color AltColor1_field {
            get { return _altColor1; }
            set {
                _altColor1 = value;
                OnPropertyChanged("AltColor1_field");
            }
        }
        private Color _altColor2;
        public Color AltColor2_field {
            get { return _altColor2; }
            set {
                _altColor2 = value;
                OnPropertyChanged("AltColor2_field");
            }
        }
        private Color _altColor3;
        public Color AltColor3_field {
            get { return _altColor3; }
            set {
                _altColor3 = value;
                OnPropertyChanged("AltColor3_field");
            }
        }
        private Color _altColor4;
        public Color AltColor4_field {
            get { return _altColor4; }
            set {
                _altColor4 = value;
                OnPropertyChanged("AltColor4_field");
            }
        }
        public ObservableCollection<CostumeBreakColorParamModel> CostumeBreakColorParamList { get; set; }
        private CostumeBreakColorParamModel _selectedCostumeBreakColorParam;
        public CostumeBreakColorParamModel SelectedCostumeBreakColorParam {
            get { return _selectedCostumeBreakColorParam; }
            set {
                _selectedCostumeBreakColorParam = value;
                if (value is not null) {
                    PlayerSettingParamID_field = value.PlayerSettingParamID;
                    AltColor1_field = value.AltColor1;
                    AltColor2_field = value.AltColor2;
                    AltColor3_field = value.AltColor3;
                    AltColor4_field = value.AltColor4;
                }

                OnPropertyChanged("SelectedCostumeBreakColorParam");
            }
        }
        private int _selectedCostumeBreakColorParamIndex;
        public int SelectedCostumeBreakColorParamIndex {
            get { return _selectedCostumeBreakColorParamIndex; }
            set {
                _selectedCostumeBreakColorParamIndex = value;
                OnPropertyChanged("SelectedCostumeBreakColorParamIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public CostumeBreakColorParamViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            CostumeBreakColorParamList = new ObservableCollection<CostumeBreakColorParamModel>();
            filePath = "";
        }

        public void Clear() {
            CostumeBreakColorParamList.Clear();
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
                if (BinName.Contains("costumeBreakColorParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x44);
                        CostumeBreakColorParamModel CostumeBreakColorParamEntry = new CostumeBreakColorParamModel();
                        CostumeBreakColorParamEntry.PlayerSettingParamID = BinaryReader.b_ReadInt(fileByte, ptr);
                        CostumeBreakColorParamEntry.AltColor1 = Color.FromArgb(BinaryReader.b_ReadInt(fileByte, ptr + 0x10), BinaryReader.b_ReadInt(fileByte, ptr + 0x04), BinaryReader.b_ReadInt(fileByte, ptr + 0x08), BinaryReader.b_ReadInt(fileByte, ptr + 0x0C));
                        CostumeBreakColorParamEntry.AltColor2 = Color.FromArgb(BinaryReader.b_ReadInt(fileByte, ptr + 0x20), BinaryReader.b_ReadInt(fileByte, ptr + 0x14), BinaryReader.b_ReadInt(fileByte, ptr + 0x18), BinaryReader.b_ReadInt(fileByte, ptr + 0x1C));
                        CostumeBreakColorParamEntry.AltColor3 = Color.FromArgb(BinaryReader.b_ReadInt(fileByte, ptr + 0x30), BinaryReader.b_ReadInt(fileByte, ptr + 0x24), BinaryReader.b_ReadInt(fileByte, ptr + 0x28), BinaryReader.b_ReadInt(fileByte, ptr + 0x2C));
                        CostumeBreakColorParamEntry.AltColor4 = Color.FromArgb(BinaryReader.b_ReadInt(fileByte, ptr + 0x40), BinaryReader.b_ReadInt(fileByte, ptr + 0x34), BinaryReader.b_ReadInt(fileByte, ptr + 0x38), BinaryReader.b_ReadInt(fileByte, ptr + 0x3C));
                        CostumeBreakColorParamList.Add(CostumeBreakColorParamEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedCostumeBreakColorParam is not null) {
                CostumeBreakColorParamList.Remove(SelectedCostumeBreakColorParam);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedCostumeBreakColorParam is not null) {
                SelectedCostumeBreakColorParam.PlayerSettingParamID = PlayerSettingParamID_field;
                SelectedCostumeBreakColorParam.AltColor1 = AltColor1_field;
                SelectedCostumeBreakColorParam.AltColor2 = AltColor2_field;
                SelectedCostumeBreakColorParam.AltColor3 = AltColor3_field;
                SelectedCostumeBreakColorParam.AltColor4 = AltColor4_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<CostumeBreakColorParamModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].PlayerSettingParamID == member_index) {
                    if (Selected < x) {
                        return x;
                    }
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(CostumeBreakColorParamList, SearchIndex_field, SelectedCostumeBreakColorParamIndex) != -1) {
                    SelectedCostumeBreakColorParamIndex = SearchByteIndex(CostumeBreakColorParamList, SearchIndex_field, SelectedCostumeBreakColorParamIndex);
                    CollectionViewSource.GetDefaultView(CostumeBreakColorParamList).MoveCurrentTo(SelectedCostumeBreakColorParam);
                } else {
                    if (SearchByteIndex(CostumeBreakColorParamList, SearchIndex_field, 0) != -1) {
                        SelectedCostumeBreakColorParamIndex = SearchByteIndex(CostumeBreakColorParamList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(CostumeBreakColorParamList).MoveCurrentTo(SelectedCostumeBreakColorParam);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            CostumeBreakColorParamModel CostumeBreakColorParamEntry = new CostumeBreakColorParamModel();
            if (SelectedCostumeBreakColorParam is not null) {
                CostumeBreakColorParamEntry = (CostumeBreakColorParamModel)SelectedCostumeBreakColorParam.Clone();
            } else {
                CostumeBreakColorParamEntry.PlayerSettingParamID = 0;
                CostumeBreakColorParamEntry.AltColor1 = Color.FromKnownColor(KnownColor.White);
                CostumeBreakColorParamEntry.AltColor2 = Color.FromKnownColor(KnownColor.White);
                CostumeBreakColorParamEntry.AltColor3 = Color.FromKnownColor(KnownColor.White);
                CostumeBreakColorParamEntry.AltColor4 = Color.FromKnownColor(KnownColor.White);
            }
            CostumeBreakColorParamList.Add(CostumeBreakColorParamEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/costumeBreakColorParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "costumeBreakColorParam");
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



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE8, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[CostumeBreakColorParamList.Count * 0x44]);

            for (int x = 0; x < CostumeBreakColorParamList.Count; x++) {
                int ptr = startPtr + (x * 0x44);
                
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeBreakColorParamList[x].PlayerSettingParamID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[16] { CostumeBreakColorParamList[x].AltColor1.R, 0, 0, 0, CostumeBreakColorParamList[x].AltColor1.G, 0, 0, 0, CostumeBreakColorParamList[x].AltColor1.B, 0, 0, 0, CostumeBreakColorParamList[x].AltColor1.A, 0, 0, 0 }, ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[16] { CostumeBreakColorParamList[x].AltColor2.R, 0, 0, 0, CostumeBreakColorParamList[x].AltColor2.G, 0, 0, 0, CostumeBreakColorParamList[x].AltColor2.B, 0, 0, 0, CostumeBreakColorParamList[x].AltColor2.A, 0, 0, 0 }, ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[16] { CostumeBreakColorParamList[x].AltColor3.R, 0, 0, 0, CostumeBreakColorParamList[x].AltColor3.G, 0, 0, 0, CostumeBreakColorParamList[x].AltColor3.B, 0, 0, 0, CostumeBreakColorParamList[x].AltColor3.A, 0, 0, 0 }, ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[16] { CostumeBreakColorParamList[x].AltColor4.R, 0, 0, 0, CostumeBreakColorParamList[x].AltColor4.G, 0, 0, 0, CostumeBreakColorParamList[x].AltColor4.B, 0, 0, 0, CostumeBreakColorParamList[x].AltColor4.A, 0, 0, 0 }, ptr + 0x34);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CostumeBreakColorParamList.Count * 0x44) + 0x14), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CostumeBreakColorParamList.Count * 0x44) + 0x10), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeBreakColorParamList.Count), count_index);
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
