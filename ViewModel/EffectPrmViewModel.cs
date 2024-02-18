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
using DynamicData;

namespace NSC_ModManager.ViewModel
{
    public class EffectPrmViewModel : INotifyPropertyChanged {
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

        private int _effectPrmID;
        public int EffectPrmID_field {
            get { return _effectPrmID; }
            set {
                _effectPrmID = value;
                OnPropertyChanged("EffectPrmID_field");
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
        private string _filePath;
        public string FilePath_field {
            get { return _filePath; }
            set {
                _filePath = value;
                OnPropertyChanged("FilePath_field");
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
        public ObservableCollection<EffectPrmModel> EffectPrmList { get; set; }
        private EffectPrmModel _selectedEffectPrm;
        public EffectPrmModel SelectedEffectPrm {
            get { return _selectedEffectPrm; }
            set {
                _selectedEffectPrm = value;
                if (value is not null) {
                    EffectPrmID_field = value.EffectPrmID;
                    Type_field = value.Type;
                    FilePath_field = value.FilePath;
                    ChunkName_field = value.ChunkName;
                }

                OnPropertyChanged("SelectedEffectPrm");
            }
        }
        private int _selectedEffectPrmIndex;
        public int SelectedEffectPrmIndex {
            get { return _selectedEffectPrmIndex; }
            set {
                _selectedEffectPrmIndex = value;
                OnPropertyChanged("SelectedEffectPrmIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public EffectPrmViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            EffectPrmList = new ObservableCollection<EffectPrmModel>();
            filePath = "";
        }

        public void Clear() {
            EffectPrmList.Clear();
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
                int StartOfFile = 0x34 + BinaryReader.b_ReadIntRev(fileByte, 16);
                if (BinName.Contains("effectprm")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 8 + (c * 0x88);
                        EffectPrmModel EffectPrmEntry = new EffectPrmModel();
                        EffectPrmEntry.EffectPrmID = BinaryReader.b_ReadInt(fileByte, ptr);
                        EffectPrmEntry.Type = BinaryReader.b_ReadInt(fileByte, ptr + 0x04);
                        EffectPrmEntry.FilePath = BinaryReader.b_ReadString(fileByte, ptr + 0x08);
                        EffectPrmEntry.ChunkName = BinaryReader.b_ReadString(fileByte, ptr + 0x48);
                        EffectPrmList.Add(EffectPrmEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedEffectPrm is not null) {
                EffectPrmList.Remove(SelectedEffectPrm);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedEffectPrm is not null) {
                SelectedEffectPrm.EffectPrmID = EffectPrmID_field;
                SelectedEffectPrm.Type = Type_field;
                SelectedEffectPrm.FilePath = FilePath_field;
                SelectedEffectPrm.ChunkName = ChunkName_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<EffectPrmModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].EffectPrmID == member_index) {
                    if (Selected < x) {
                        return x;
                    }
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(EffectPrmList, SearchIndex_field, SelectedEffectPrmIndex) != -1) {
                    SelectedEffectPrmIndex = SearchByteIndex(EffectPrmList, SearchIndex_field, SelectedEffectPrmIndex);
                    CollectionViewSource.GetDefaultView(EffectPrmList).MoveCurrentTo(SelectedEffectPrm);
                } else {
                    if (SearchByteIndex(EffectPrmList, SearchIndex_field, 0) != -1) {
                        SelectedEffectPrmIndex = SearchByteIndex(EffectPrmList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(EffectPrmList).MoveCurrentTo(SelectedEffectPrm);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }

        public int MaxEffectID() {
            if (EffectPrmList.Count > 0) {
                List<int> EffectID = new List<int>();
                for (int i = 0; i< EffectPrmList.Count; i++) {
                    EffectID.Add(EffectPrmList[i].EffectPrmID);
                }
                return EffectID.Max();
            }
            return 1;
        }

        public void AddDupEntry() {
            EffectPrmModel EffectPrmEntry = new EffectPrmModel();
            if (SelectedEffectPrm is not null) {
                EffectPrmEntry = (EffectPrmModel)SelectedEffectPrm.Clone();
            } else {
                EffectPrmEntry.EffectPrmID = 0;
                EffectPrmEntry.Type = 1;
                EffectPrmEntry.FilePath = "data/";
                EffectPrmEntry.ChunkName = "";
            }
            EffectPrmList.Add(EffectPrmEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/effectprm.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "effectprm");
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

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[28]
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0x79,0x76,0x00,0x00,0x08,0x98,0x00,0x00,0x00,0x01,0x00,0x63,0x79,0x76,0x00,0x00,0x08,0x94
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]);
            int startOfFile = fileBytes36.Length;

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[EffectPrmList.Count * 0x88]);
            for (int x = 0; x < EffectPrmList.Count; x++) {
                int ptr = startOfFile + (x * 0x88);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(EffectPrmList[x].EffectPrmID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(EffectPrmList[x].Type), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, EffectPrmList[x].FilePath, ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, EffectPrmList[x].ChunkName, ptr + 0x48);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((EffectPrmList.Count * 0x88) + 0x8), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((EffectPrmList.Count * 0x88) + 0x4), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(EffectPrmList.Count), count_index);
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
