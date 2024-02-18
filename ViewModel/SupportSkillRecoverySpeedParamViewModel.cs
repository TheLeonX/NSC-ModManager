using NSC_ModManager.Model;
using NSC_ModManager.Properties;
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

namespace NSC_ModManager.ViewModel {
    public class SupportSkillRecoverySpeedParamViewModel : INotifyPropertyChanged {
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
        private float _jutsu1;
        public float Jutsu1_field {
            get { return _jutsu1; }
            set {
                _jutsu1 = value;
                OnPropertyChanged("Jutsu1_field");
            }
        }
        private float _jutsu2;
        public float Jutsu2_field {
            get { return _jutsu2; }
            set {
                _jutsu2 = value;
                OnPropertyChanged("Jutsu2_field");
            }
        }
        private float _jutsu3;
        public float Jutsu3_field {
            get { return _jutsu3; }
            set {
                _jutsu3 = value;
                OnPropertyChanged("Jutsu3_field");
            }
        }
        private float _jutsu4;
        public float Jutsu4_field {
            get { return _jutsu4; }
            set {
                _jutsu4 = value;
                OnPropertyChanged("Jutsu4_field");
            }
        }
        private float _jutsu5;
        public float Jutsu5_field {
            get { return _jutsu5; }
            set {
                _jutsu5 = value;
                OnPropertyChanged("Jutsu5_field");
            }
        }
        private float _jutsu6;
        public float Jutsu6_field {
            get { return _jutsu6; }
            set {
                _jutsu6 = value;
                OnPropertyChanged("Jutsu6_field");
            }
        }
        private float _jutsu1_awa;
        public float Jutsu1_awa_field {
            get { return _jutsu1_awa; }
            set {
                _jutsu1_awa = value;
                OnPropertyChanged("Jutsu1_awa_field");
            }
        }
        private float _jutsu2_awa;
        public float Jutsu2_awa_field {
            get { return _jutsu2_awa; }
            set {
                _jutsu2_awa = value;
                OnPropertyChanged("Jutsu2_awa_field");
            }
        }

        public ObservableCollection<SupportSkillRecoverySpeedParamModel> SupportSkillRecoverySpeedParamList { get; set; }
        private SupportSkillRecoverySpeedParamModel _selectedSupportSkillRecoverySpeedParam;
        public SupportSkillRecoverySpeedParamModel SelectedSupportSkillRecoverySpeedParam {
            get { return _selectedSupportSkillRecoverySpeedParam; }
            set {
                _selectedSupportSkillRecoverySpeedParam = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    Jutsu1_field = value.Jutsu1;
                    Jutsu2_field = value.Jutsu2;
                    Jutsu3_field = value.Jutsu3;
                    Jutsu4_field = value.Jutsu4;
                    Jutsu5_field = value.Jutsu5;
                    Jutsu6_field = value.Jutsu6;
                    Jutsu1_awa_field = value.Jutsu1_awa;
                    Jutsu2_awa_field = value.Jutsu2_awa;
                }

                OnPropertyChanged("SelectedSupportSkillRecoverySpeedParam");
            }
        }
        private int _selectedSupportSkillRecoverySpeedParamIndex;
        public int SelectedSupportSkillRecoverySpeedParamIndex {
            get { return _selectedSupportSkillRecoverySpeedParamIndex; }
            set {
                _selectedSupportSkillRecoverySpeedParamIndex = value;
                OnPropertyChanged("SelectedSupportSkillRecoverySpeedParamIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public SupportSkillRecoverySpeedParamViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            SupportSkillRecoverySpeedParamList = new ObservableCollection<SupportSkillRecoverySpeedParamModel>();
            filePath = "";
        }

        public void Clear() {
            SupportSkillRecoverySpeedParamList.Clear();
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
                if (BinName.Contains("supportSkillRecoverySpeedParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x24);
                        SupportSkillRecoverySpeedParamModel SupportSkillRecoverySpeedParamEntry = new SupportSkillRecoverySpeedParamModel();
                        SupportSkillRecoverySpeedParamEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        SupportSkillRecoverySpeedParamEntry.Jutsu1 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x04);
                        SupportSkillRecoverySpeedParamEntry.Jutsu2 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x08);
                        SupportSkillRecoverySpeedParamEntry.Jutsu3 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x0C);
                        SupportSkillRecoverySpeedParamEntry.Jutsu4 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x10);
                        SupportSkillRecoverySpeedParamEntry.Jutsu5 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x14);
                        SupportSkillRecoverySpeedParamEntry.Jutsu6 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x18);
                        SupportSkillRecoverySpeedParamEntry.Jutsu1_awa = BinaryReader.b_ReadFloat(fileByte, ptr + 0x1C);
                        SupportSkillRecoverySpeedParamEntry.Jutsu2_awa = BinaryReader.b_ReadFloat(fileByte, ptr + 0x20);
                        SupportSkillRecoverySpeedParamList.Add(SupportSkillRecoverySpeedParamEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedSupportSkillRecoverySpeedParam is not null) {
                SupportSkillRecoverySpeedParamList.Remove(SelectedSupportSkillRecoverySpeedParam);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedSupportSkillRecoverySpeedParam is not null) {
                SelectedSupportSkillRecoverySpeedParam.CharacodeID = CharacodeID_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu1 = Jutsu1_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu2 = Jutsu2_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu3 = Jutsu3_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu4 = Jutsu4_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu5 = Jutsu5_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu6 = Jutsu6_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu1_awa = Jutsu1_awa_field;
                SelectedSupportSkillRecoverySpeedParam.Jutsu2_awa = Jutsu2_awa_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<SupportSkillRecoverySpeedParamModel> FunctionList, int member_index, int Selected) {
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
                if (SearchByteIndex(SupportSkillRecoverySpeedParamList, SearchIndex_field, SelectedSupportSkillRecoverySpeedParamIndex) != -1) {
                    SelectedSupportSkillRecoverySpeedParamIndex = SearchByteIndex(SupportSkillRecoverySpeedParamList, SearchIndex_field, SelectedSupportSkillRecoverySpeedParamIndex);
                    CollectionViewSource.GetDefaultView(SupportSkillRecoverySpeedParamList).MoveCurrentTo(SelectedSupportSkillRecoverySpeedParam);
                } else {
                    if (SearchByteIndex(SupportSkillRecoverySpeedParamList, SearchIndex_field, 0) != -1) {
                        SelectedSupportSkillRecoverySpeedParamIndex = SearchByteIndex(SupportSkillRecoverySpeedParamList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(SupportSkillRecoverySpeedParamList).MoveCurrentTo(SelectedSupportSkillRecoverySpeedParam);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            SupportSkillRecoverySpeedParamModel SupportSkillRecoverySpeedParamEntry = new SupportSkillRecoverySpeedParamModel();
            if (SelectedSupportSkillRecoverySpeedParam is not null) {
                SupportSkillRecoverySpeedParamEntry = (SupportSkillRecoverySpeedParamModel)SelectedSupportSkillRecoverySpeedParam.Clone();
            } else {
                SupportSkillRecoverySpeedParamEntry.CharacodeID = 0;
                SupportSkillRecoverySpeedParamEntry.Jutsu1 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu2 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu3 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu4 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu5 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu6 = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu1_awa = 0.3f;
                SupportSkillRecoverySpeedParamEntry.Jutsu2_awa = 0.3f;
            }
            SupportSkillRecoverySpeedParamList.Add(SupportSkillRecoverySpeedParamEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/supportSkillRecoverySpeedParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "supportSkillRecoverySpeedParam");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[SupportSkillRecoverySpeedParamList.Count * 0x24]);

            for (int x = 0; x < SupportSkillRecoverySpeedParamList.Count; x++) {
                int ptr = startPtr + (x * 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu1), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu2), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu3), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu4), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu5), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu6), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu1_awa), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList[x].Jutsu1_awa), ptr + 0x20);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SupportSkillRecoverySpeedParamList.Count * 0x24) + 0x14), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SupportSkillRecoverySpeedParamList.Count * 0x24) + 0x10), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SupportSkillRecoverySpeedParamList.Count), count_index);
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
