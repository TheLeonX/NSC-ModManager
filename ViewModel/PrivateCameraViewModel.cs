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
    public class PrivateCameraViewModel : INotifyPropertyChanged {
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
        private int _characodeIndex;
        public int CharacodeIndex_field {
            get { return _characodeIndex; }
            set {
                _characodeIndex = value;
                OnPropertyChanged("CharacodeIndex_field");
            }
        }
        private float _cameraDistance;
        public float CameraDistance_field {
            get { return _cameraDistance; }
            set {
                _cameraDistance = value;
                OnPropertyChanged("CameraDistance_field");
            }
        }
        private float _cameraSpeed;
        public float CameraSpeed_field {
            get { return _cameraSpeed; }
            set {
                _cameraSpeed = value;
                OnPropertyChanged("CameraSpeed_field");
            }
        }
        private float _cameraMovement;
        public float CameraMovement_field {
            get { return _cameraMovement; }
            set {
                _cameraMovement = value;
                OnPropertyChanged("CameraMovement_field");
            }
        }
        private float _unk1;
        public float Unk1_field {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        private float _cameraHeight;
        public float CameraHeight_field {
            get { return _cameraHeight; }
            set {
                _cameraHeight = value;
                OnPropertyChanged("CameraHeight_field");
            }
        }
        private float _cameraAngle;
        public float CameraAngle_field {
            get { return _cameraAngle; }
            set {
                _cameraAngle = value;
                OnPropertyChanged("CameraAngle_field");
            }
        }
        private float _cameraHeight2;
        public float CameraHeight2_field {
            get { return _cameraHeight2; }
            set {
                _cameraHeight2 = value;
                OnPropertyChanged("CameraHeight2_field");
            }
        }
        private float _fov;
        public float FOV_field {
            get { return _fov; }
            set {
                _fov = value;
                OnPropertyChanged("FOV_field");
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
        private float _cameraDistance2;
        public float CameraDistance2_field {
            get { return _cameraDistance2; }
            set {
                _cameraDistance2 = value;
                OnPropertyChanged("CameraDistance2_field");
            }
        }
        private float _fov2;
        public float FOV2_field {
            get { return _fov2; }
            set {
                _fov2 = value;
                OnPropertyChanged("FOV2_field");
            }
        }
        public ObservableCollection<PrivateCameraModel> PrivateCameraList { get; set; }
        private PrivateCameraModel _selectedPrivateCamera;
        public PrivateCameraModel SelectedPrivateCamera {
            get { return _selectedPrivateCamera; }
            set {
                _selectedPrivateCamera = value;
                if (value is not null) {
                    CharacodeIndex_field = value.CharacodeIndex;
                    CameraDistance_field = value.CameraDistance;
                    CameraSpeed_field = value.CameraSpeed;
                    CameraMovement_field = value.CameraMovement;
                    Unk1_field = value.Unk1;
                    CameraHeight_field = value.CameraHeight;
                    CameraAngle_field = value.CameraAngle;
                    CameraHeight2_field = value.CameraHeight2;
                    FOV_field = value.FOV;
                    Unk2_field = value.Unk2;
                    CameraDistance2_field = value.CameraDistance2;
                    FOV2_field = value.FOV2;
                }
                OnPropertyChanged("SelectedPrivateCamera");
            }
        }
        private int _selectedPrivateCameraIndex;
        public int SelectedPrivateCameraIndex {
            get { return _selectedPrivateCameraIndex; }
            set {
                _selectedPrivateCameraIndex = value;
                OnPropertyChanged("SelectedPrivateCameraIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public PrivateCameraViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            PrivateCameraList = new ObservableCollection<PrivateCameraModel>();
            filePath = "";
        }

        public void Clear() {
            PrivateCameraList.Clear();
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
                byte[] FileBytes = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(FileBytes, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++) {
                    string name = BinaryReader.b_ReadString(FileBytes, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x34 + BinaryReader.b_ReadIntRev(FileBytes, 16);
                if (BinName.Contains("privateCamera")) {
                    int entryCount = BinaryReader.b_ReadInt(FileBytes, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 8 + (c * 0x2C);
                        PrivateCameraModel privateCameraEntry = new PrivateCameraModel();
                        privateCameraEntry.CharacodeIndex = c + 1;
                        privateCameraEntry.CameraDistance = BinaryReader.b_ReadFloat(FileBytes, ptr);
                        privateCameraEntry.CameraSpeed = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x04);
                        privateCameraEntry.CameraMovement = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x08);
                        privateCameraEntry.Unk1 = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x0C);
                        privateCameraEntry.CameraHeight = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x10);
                        privateCameraEntry.CameraAngle = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x14);
                        privateCameraEntry.CameraHeight2 = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x18);
                        privateCameraEntry.FOV = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x1C);
                        privateCameraEntry.Unk2 = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x20);
                        privateCameraEntry.CameraDistance2 = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x24);
                        privateCameraEntry.FOV2 = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x28);
                        PrivateCameraList.Add(privateCameraEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedPrivateCamera is not null) {
                for (int c = SelectedPrivateCamera.CharacodeIndex; c < PrivateCameraList.Count; c++) {
                    PrivateCameraList[c].CharacodeIndex = c;
                }
                PrivateCameraList.Remove(SelectedPrivateCamera);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedPrivateCamera is not null) {
                SelectedPrivateCamera.CameraDistance = CameraDistance_field;
                SelectedPrivateCamera.CameraSpeed = CameraSpeed_field;
                SelectedPrivateCamera.CameraMovement = CameraMovement_field;
                SelectedPrivateCamera.Unk1 = Unk1_field;
                SelectedPrivateCamera.CameraHeight = CameraHeight_field;
                SelectedPrivateCamera.CameraAngle = CameraAngle_field;
                SelectedPrivateCamera.CameraHeight2 = CameraHeight2_field;
                SelectedPrivateCamera.FOV = FOV_field;
                SelectedPrivateCamera.Unk2 = Unk2_field;
                SelectedPrivateCamera.CameraDistance2 = CameraDistance2_field;
                SelectedPrivateCamera.FOV2 = FOV2_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0 && PrivateCameraList.Count > 0 && (SearchIndex_field - 1 < PrivateCameraList.Count)) {

                SelectedPrivateCameraIndex = SearchIndex_field - 1;
                CollectionViewSource.GetDefaultView(PrivateCameraList).MoveCurrentTo(SelectedPrivateCamera);
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }


        public void AddDupEntry() {
            PrivateCameraModel privateCameraEntry = new PrivateCameraModel();
            if (SelectedPrivateCamera is not null) {
                privateCameraEntry = (PrivateCameraModel)SelectedPrivateCamera.Clone();
                privateCameraEntry.CharacodeIndex = PrivateCameraList.Count + 1;
            } else {
                privateCameraEntry.CharacodeIndex = PrivateCameraList.Count + 1;
                privateCameraEntry.CameraDistance = -1;
                privateCameraEntry.CameraSpeed = -1;
                privateCameraEntry.CameraMovement = -1;
                privateCameraEntry.Unk1 = -1;
                privateCameraEntry.CameraHeight = -1;
                privateCameraEntry.CameraAngle = -1;
                privateCameraEntry.CameraHeight2 = -1;
                privateCameraEntry.FOV = 50;
                privateCameraEntry.Unk2 = -1;
                privateCameraEntry.CameraDistance2 = -1;
                privateCameraEntry.FOV2 = 50;
            }
            PrivateCameraList.Add(privateCameraEntry);
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

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x63, 0x40, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/privateCamera.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "privateCamera");
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
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x2C * PrivateCameraList.Count]);
            for (int c = 0; c < PrivateCameraList.Count; c++) {
                int ptr = startOfFile + (c * 0x2C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraDistance), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraSpeed), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraMovement), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].Unk1), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraHeight), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraAngle), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraHeight2), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].FOV), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].Unk2), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].CameraDistance2), ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList[c].FOV2), ptr + 0x28);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PrivateCameraList.Count * 0x2C) + 8), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PrivateCameraList.Count * 0x2C) + 4), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PrivateCameraList.Count), count_index);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0,
                0,
                0,
                8,
                0,
                0,
                0,
                2,
                0,
                99,
                0,
                0,
                0,
                0,
                0,
                4,
                0,
                0,
                0,
                0
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
