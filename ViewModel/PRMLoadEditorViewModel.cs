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
using System.Windows.Forms;

namespace NSC_ModManager.ViewModel
{
    public class PRMLoadEditorViewModel : INotifyPropertyChanged {

        private Visibility _loadingStatePlay;
        public Visibility LoadingStatePlay {
            get { return _loadingStatePlay; }
            set {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }

        private string _filepath_field;
        public string FilePath_field {
            get { return _filepath_field; }
            set {
                _filepath_field = value;
                OnPropertyChanged("FilePath_field");
            }
        }
        private string _fileName_field;
        public string FileName_field {
            get { return _fileName_field; }
            set {
                _fileName_field = value;
                OnPropertyChanged("FileName_field");
            }
        }
        private int _type_field;
        public int Type_field {
            get { return _type_field; }
            set {
                _type_field = value;
                OnPropertyChanged("Type_field");
            }
        }
        private int _costumeIndex_field;
        public int CostumeIndex_field {
            get { return _costumeIndex_field; }
            set {
                _costumeIndex_field = value;
                OnPropertyChanged("CostumeIndex_field");
            }
        }
        private int _condition_field;
        public int Condition_field {
            get { return _condition_field; }
            set {
                _condition_field = value;
                OnPropertyChanged("Condition_field");
            }
        }
        public ObservableCollection<PRMLoad_Model> PRMLoadList { get; set; }
        private PRMLoad_Model _selectedPRMLoad;
        public PRMLoad_Model SelectedPRMLoad {
            get { return _selectedPRMLoad; }
            set {
                _selectedPRMLoad = value;
                if (value is not null) {
                    FilePath_field = value.FilePath;
                    FileName_field = value.FileName;
                    Type_field = value.Type;
                    CostumeIndex_field = value.CostumeIndex;
                    switch (value.Condition) {
                        case 0x01:
                            Condition_field = 0;
                            break;
                        case 0x02:
                            Condition_field = 1;
                            break;
                        case 0x03:
                            Condition_field = 2;
                            break;
                        case 0x05:
                            Condition_field = 3;
                            break;
                        case 0x0B:
                            Condition_field = 4;
                            break;
                        case 0x13:
                            Condition_field = 5;
                            break;
                        case 0x23:
                            Condition_field = 6;
                            break;
                        case 0x43:
                            Condition_field = 7;
                            break;
                        case 0x83:
                            Condition_field = 8;
                            break;
                        case 0x320:
                            Condition_field = 9;
                            break;
                        case 0x101:
                            Condition_field = 10;
                            break;
                        case 0x201:
                            Condition_field = 11;
                            break;
                        case 0x401:
                            Condition_field = 12;
                            break;
                        case 0x801:
                            Condition_field = 13;
                            break;
                        default:
                            Condition_field = 0;
                            break;
                    }
                }
                OnPropertyChanged("SelectedPRMLoad");
            }
        }

        public byte[] fileByte;
        public string filePath;
        private string _characodeBinName;
        public string CharacodeBinName {
            get { return _characodeBinName; }
            set {
                _characodeBinName = value;
                OnPropertyChanged("CharacodeBinName");
            }
        }

        public PRMLoadEditorViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            PRMLoadList = new ObservableCollection<PRMLoad_Model>();
            filePath = "";
        }

        public void Clear() {
            CharacodeBinName = "";
            PRMLoadList.Clear();
            filePath = "";
        }
        public void OpenFile(string basepath = "") {
            Clear();
            if (basepath == "") {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "XFBIN Container (*.xfbin)|*.xfbin";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == DialogResult.OK) {
                    filePath = myDialog.FileName;
                } else {
                    return;
                }
            } else {
                filePath = basepath;
            }
            if (File.Exists(filePath)) {
                byte[] FileBytes = File.ReadAllBytes(filePath);
                int fileSectionIndex = XfbinParser.GetFileSectionIndex(FileBytes);
                int startIndex = fileSectionIndex + 0x1C;

                string BinName = "";
                // Get character name
                BinName = XfbinParser.GetNameList(FileBytes)[0];
                CharacodeBinName = BinName.Substring(0, BinName.IndexOf("prm_load"));
                if (BinName.Contains("prm_load")) {
                    int entryCount = BinaryReader.b_ReadInt(FileBytes, startIndex);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = startIndex + 4 + (c * 0x50);
                        PRMLoad_Model prm_load_entry = new PRMLoad_Model();
                        prm_load_entry.FilePath = BinaryReader.b_ReadString(FileBytes, ptr + 0x04);
                        prm_load_entry.FileName = BinaryReader.b_ReadString(FileBytes, ptr + 0x24);
                        prm_load_entry.Type = BinaryReader.b_ReadInt(FileBytes, ptr + 0x44);
                        prm_load_entry.CostumeIndex = BinaryReader.b_ReadInt(FileBytes, ptr + 0x48);
                        prm_load_entry.Condition = BinaryReader.b_ReadInt(FileBytes, ptr + 0x4C);
                        PRMLoadList.Add(prm_load_entry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedPRMLoad is not null) {
                PRMLoadList.Remove(SelectedPRMLoad);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedPRMLoad is not null) {
                SelectedPRMLoad.FilePath = FilePath_field;
                SelectedPRMLoad.FileName = FileName_field;
                SelectedPRMLoad.Type = Type_field;
                SelectedPRMLoad.CostumeIndex = CostumeIndex_field;

                switch (Condition_field) {
                    case 0:
                        SelectedPRMLoad.Condition = 0x01;
                        break;
                    case 1:
                        SelectedPRMLoad.Condition = 0x02;
                        break;
                    case 2:
                        SelectedPRMLoad.Condition = 0x03;
                        break;
                    case 3:
                        SelectedPRMLoad.Condition = 0x05;
                        break;
                    case 4:
                        SelectedPRMLoad.Condition = 0x0B;
                        break;
                    case 5:
                        SelectedPRMLoad.Condition = 0x13;
                        break;
                    case 6:
                        SelectedPRMLoad.Condition = 0x23;
                        break;
                    case 7:
                        SelectedPRMLoad.Condition = 0x43;
                        break;
                    case 8:
                        SelectedPRMLoad.Condition = 0x83;
                        break;
                    case 9:
                        SelectedPRMLoad.Condition = 0x2003;
                        break;
                    case 10:
                        SelectedPRMLoad.Condition = 0x101;
                        break;
                    case 11:
                        SelectedPRMLoad.Condition = 0x102;
                        break;
                    case 12:
                        SelectedPRMLoad.Condition = 0x104;
                        break;
                    case 13:
                        SelectedPRMLoad.Condition = 0x108;
                        break;
                }


            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public void AddEntry() {

            PRMLoad_Model prm_load_entry = new PRMLoad_Model();
            prm_load_entry.FilePath = "";
            prm_load_entry.FileName = "";
            prm_load_entry.Type = 3;
            prm_load_entry.CostumeIndex = -1;
            prm_load_entry.Condition = 1;
            PRMLoadList.Add(prm_load_entry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/" + CharacodeBinName + "/" + CharacodeBinName + "prm_load.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, CharacodeBinName + "prm_load");
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
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0xDE,0x75,0x00,0x00,0x08,0x98,0x00,0x00,0x00,0x01,0x00,0x63,0xDE,0x75,0x00,0x00,0x08,0x94
                });






            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]);
            int startOfFile = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x50 * PRMLoadList.Count]);
            for (int c = 0; c < PRMLoadList.Count; c++) {
                int ptr = startOfFile + (c * 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0.5f), ptr + 0x00,1);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, PRMLoadList[c].FilePath, ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, PRMLoadList[c].FileName, ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PRMLoadList[c].Type), ptr + 0x44);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PRMLoadList[c].CostumeIndex), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PRMLoadList[c].Condition), ptr + 0x4C);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PRMLoadList.Count * 0x50) + 8), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PRMLoadList.Count * 0x50) + 4), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(PRMLoadList.Count), count_index);
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

        private RelayCommand _addEntryCommand;
        public RelayCommand AddEntryCommand {
            get {
                return _addEntryCommand ??
                  (_addEntryCommand = new RelayCommand(obj => {
                      AddEntryAsync();
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
        public async void AddEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddEntry()));

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
