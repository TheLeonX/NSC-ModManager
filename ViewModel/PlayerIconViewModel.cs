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
using System.Reflection;

namespace NSC_ModManager.ViewModel {
    public class PlayerIconViewModel : INotifyPropertyChanged {
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
        private int _costumeID_field;
        public int CostumeID_field {
            get { return _costumeID_field; }
            set {
                _costumeID_field = value;
                OnPropertyChanged("CostumeID_field");
            }
        }
        private string _baseIcon_field;
        public string BaseIcon_field {
            get { return _baseIcon_field; }
            set {
                _baseIcon_field = value;
                OnPropertyChanged("BaseIcon_field");
            }
        }
        private string _awakeIcon_field;
        public string AwakeIcon_field {
            get { return _awakeIcon_field; }
            set {
                _awakeIcon_field = value;
                OnPropertyChanged("AwakeIcon_field");
            }
        }
        private string _playerIconName_field;
        public string PlayerIconName_field {
            get { return _playerIconName_field; }
            set {
                _playerIconName_field = value;
                OnPropertyChanged("PlayerIconName_field");
            }
        }
        private string _subJutsuIcon_field;
        public string SubJutsuIcon_field {
            get { return _subJutsuIcon_field; }
            set {
                _subJutsuIcon_field = value;
                OnPropertyChanged("SubJutsuIcon_field");
            }
        }
        public ObservableCollection<PlayerIconModel> playerIconList { get; set; }
        private PlayerIconModel _selectedPlayerIcon;
        public PlayerIconModel SelectedPlayerIcon {
            get { return _selectedPlayerIcon; }
            set {
                _selectedPlayerIcon = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    CostumeID_field = value.CostumeID;
                    BaseIcon_field = value.BaseIcon;
                    AwakeIcon_field = value.AwakeIcon;
                    PlayerIconName_field = value.PlayerIconName;
                    SubJutsuIcon_field = value.SubJutsuIcon;
                }

                OnPropertyChanged("SelectedPlayerIcon");
            }
        }
        private int _selectedPlayerIconIndex;
        public int SelectedPlayerIconIndex {
            get { return _selectedPlayerIconIndex; }
            set {
                _selectedPlayerIconIndex = value;
                OnPropertyChanged("SelectedPlayerIconIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public PlayerIconViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            playerIconList = new ObservableCollection<PlayerIconModel>();
            filePath = "";
        }

        public void Clear() {
            playerIconList.Clear();
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
                if (BinName.Contains("player_icon")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x28);
                        PlayerIconModel PlayerIconEntry = new PlayerIconModel();
                        PlayerIconEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        PlayerIconEntry.CostumeID = BinaryReader.b_ReadInt(fileByte, ptr + 0x04);
                        PlayerIconEntry.BaseIcon = BinaryReader.b_ReadString(fileByte, ptr + 0x08 + BinaryReader.b_ReadInt(fileByte, ptr + 0x08));
                        PlayerIconEntry.AwakeIcon = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        PlayerIconEntry.PlayerIconName = BinaryReader.b_ReadString(fileByte, ptr + 0x18 + BinaryReader.b_ReadInt(fileByte, ptr + 0x18));
                        PlayerIconEntry.SubJutsuIcon = BinaryReader.b_ReadString(fileByte, ptr + 0x20 + BinaryReader.b_ReadInt(fileByte, ptr + 0x20));

                        playerIconList.Add(PlayerIconEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedPlayerIcon is not null) {
                playerIconList.Remove(SelectedPlayerIcon);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedPlayerIcon is not null) {
                SelectedPlayerIcon.CharacodeID = CharacodeID_field;
                SelectedPlayerIcon.CostumeID = CostumeID_field;
                SelectedPlayerIcon.BaseIcon = BaseIcon_field;
                SelectedPlayerIcon.AwakeIcon = AwakeIcon_field;
                SelectedPlayerIcon.PlayerIconName = PlayerIconName_field;
                SelectedPlayerIcon.SubJutsuIcon = SubJutsuIcon_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<PlayerIconModel> FunctionList, int member_index, int Selected) {
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
                if (SearchByteIndex(playerIconList, SearchIndex_field, SelectedPlayerIconIndex) != -1) {
                    SelectedPlayerIconIndex = SearchByteIndex(playerIconList, SearchIndex_field, SelectedPlayerIconIndex);
                    CollectionViewSource.GetDefaultView(playerIconList).MoveCurrentTo(SelectedPlayerIcon);
                } else {
                    if (SearchByteIndex(playerIconList, SearchIndex_field, 0) != -1) {
                        SelectedPlayerIconIndex = SearchByteIndex(playerIconList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(playerIconList).MoveCurrentTo(SelectedPlayerIcon);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            PlayerIconModel PlayerIconEntry = new PlayerIconModel();
            if (SelectedPlayerIcon is not null) {
                PlayerIconEntry = (PlayerIconModel)SelectedPlayerIcon.Clone();
            } else {
                PlayerIconEntry.CharacodeID = 0;
                PlayerIconEntry.CostumeID = 0;
                PlayerIconEntry.BaseIcon = "";
                PlayerIconEntry.AwakeIcon = "";
                PlayerIconEntry.PlayerIconName = "";
                PlayerIconEntry.SubJutsuIcon = "";
            }
            playerIconList.Add(PlayerIconEntry);
            SelectedPlayerIconIndex = playerIconList.Count-1;
            CollectionViewSource.GetDefaultView(playerIconList).MoveCurrentTo(playerIconList[SelectedPlayerIconIndex]);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/player_icon.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "player_icon");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[playerIconList.Count * 0x28]);

            int addSize = 0;

            List<int> baseIcon_pointer = new List<int>();
            List<int> awakeningIcon_pointer = new List<int>();
            List<int> nameIcon_pointer = new List<int>();
            List<int> subjutsuIcon_pointer = new List<int>();
            for (int x = 0; x < playerIconList.Count; x++) {
                int ptr = startPtr + (x * 0x28);
                baseIcon_pointer.Add(fileBytes36.Length);
                if (playerIconList[x].BaseIcon != "" && playerIconList[x].BaseIcon is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(playerIconList[x].BaseIcon));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = baseIcon_pointer[x] - startPtr - x * 0x28 - 0x08;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x08);
                    addSize += playerIconList[x].BaseIcon.Length + 1;
                }
                awakeningIcon_pointer.Add(fileBytes36.Length);
                if (playerIconList[x].AwakeIcon != "" && playerIconList[x].AwakeIcon is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(playerIconList[x].AwakeIcon));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = awakeningIcon_pointer[x] - startPtr - x * 0x28 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += playerIconList[x].AwakeIcon.Length + 1;
                }
                nameIcon_pointer.Add(fileBytes36.Length);
                if (playerIconList[x].BaseIcon != "" && playerIconList[x].BaseIcon is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(playerIconList[x].PlayerIconName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = nameIcon_pointer[x] - startPtr - x * 0x28 - 0x18;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x18);
                    addSize += playerIconList[x].PlayerIconName.Length + 1;
                }
                subjutsuIcon_pointer.Add(fileBytes36.Length);
                if (playerIconList[x].SubJutsuIcon != "" && playerIconList[x].SubJutsuIcon is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(playerIconList[x].SubJutsuIcon));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = subjutsuIcon_pointer[x] - startPtr - x * 0x28 - 0x20;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x20);
                    addSize += playerIconList[x].SubJutsuIcon.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(playerIconList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(playerIconList[x].CostumeID), ptr + 0x04);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((playerIconList.Count * 0x28) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((playerIconList.Count * 0x28) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(playerIconList.Count), count_index);
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
