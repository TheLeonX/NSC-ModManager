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
    public class CostumeParamViewModel : INotifyPropertyChanged
    {
        private int _searchIndex_field;
        public int SearchIndex_field
        {
            get { return _searchIndex_field; }
            set
            {
                _searchIndex_field = value;
                OnPropertyChanged("SearchIndex_field");
            }
        }

        private Visibility _loadingStatePlay;
        public Visibility LoadingStatePlay
        {
            get { return _loadingStatePlay; }
            set
            {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }

        private string _entryString;
        public string EntryString_field
        {
            get { return _entryString; }
            set
            {
                _entryString = value;
                OnPropertyChanged("EntryString_field");
            }
        }
        private int _entryIndex;
        public int EntryIndex_field
        {
            get { return _entryIndex; }
            set
            {
                _entryIndex = value;
                OnPropertyChanged("EntryIndex_field");
            }
        }
        private int _playerSettingParamID;
        public int PlayerSettingParamID_field
        {
            get { return _playerSettingParamID; }
            set
            {
                _playerSettingParamID = value;
                OnPropertyChanged("PlayerSettingParamID_field");
            }
        }
        private string _characterName;
        public string CharacterName_field
        {
            get { return _characterName; }
            set
            {
                _characterName = value;
                OnPropertyChanged("CharacterName_field");
            }
        }
        private int _entryType;
        public int EntryType_field
        {
            get { return _entryType; }
            set
            {
                _entryType = value;
                OnPropertyChanged("EntryType_field");
            }
        }
        private int _unlockCost;
        public int UnlockCost_field
        {
            get { return _unlockCost; }
            set
            {
                _unlockCost = value;
                OnPropertyChanged("UnlockCost_field");
            }
        }
        private int _unlockCondition;
        public int UnlockCondition_field
        {
            get { return _unlockCondition; }
            set
            {
                _unlockCondition = value;
                OnPropertyChanged("UnlockCondition_field");

            }
        }
        public ObservableCollection<CostumeParamModel> CostumeParamList { get; set; }
        private CostumeParamModel _selectedCostumeParam;
        public CostumeParamModel SelectedCostumeParam
        {
            get { return _selectedCostumeParam; }
            set
            {
                _selectedCostumeParam = value;
                if (value is not null)
                {
                    EntryString_field = value.EntryString;
                    EntryIndex_field = value.EntryIndex;
                    PlayerSettingParamID_field = value.PlayerSettingParamID;
                    CharacterName_field = value.CharacterName;
                    EntryType_field = value.EntryType;
                    UnlockCost_field = value.UnlockCost;
                    UnlockCondition_field = value.UnlockCondition;
                }

                OnPropertyChanged("SelectedCostumeParam");
            }
        }
        private int _selectedCostumeParamIndex;
        public int SelectedCostumeParamIndex
        {
            get { return _selectedCostumeParamIndex; }
            set
            {
                _selectedCostumeParamIndex = value;
                OnPropertyChanged("SelectedCostumeParamIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public CostumeParamViewModel()
        {

            LoadingStatePlay = Visibility.Hidden;
            CostumeParamList = new ObservableCollection<CostumeParamModel>();
            filePath = "";
        }

        public void Clear()
        {
            CostumeParamList.Clear();
        }

        public void OpenFile(string basepath = "")
        {
            Clear();
            if (basepath == "")
            {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "XFBIN Container (*.xfbin)|*.xfbin";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true)
                {
                    filePath = myDialog.FileName;
                } else
                {
                    return;
                }
            } else
            {
                filePath = basepath;
            }
            if (File.Exists(filePath))
            {
                fileByte = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(fileByte, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++)
                {
                    string name = BinaryReader.b_ReadString(fileByte, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(fileByte, 16);
                if (BinName.Contains("costumeParam"))
                {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++)
                    {
                        int ptr = StartOfFile + 0x10 + (c * 0x28);
                        CostumeParamModel CostumeParamEntry = new CostumeParamModel();
                        CostumeParamEntry.EntryString = BinaryReader.b_ReadString(fileByte, ptr + BinaryReader.b_ReadInt(fileByte, ptr));
                        CostumeParamEntry.EntryIndex = BinaryReader.b_ReadInt(fileByte, ptr + 0x08);
                        CostumeParamEntry.PlayerSettingParamID = BinaryReader.b_ReadInt(fileByte, ptr + 0x0C);
                        CostumeParamEntry.CharacterName = BinaryReader.b_ReadString(fileByte, ptr + 0x10 + BinaryReader.b_ReadInt(fileByte, ptr + 0x10));
                        CostumeParamEntry.EntryType = BinaryReader.b_ReadInt(fileByte, ptr + 0x18);
                        CostumeParamEntry.UnlockCost = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C);
                        CostumeParamEntry.UnlockCondition = BinaryReader.b_ReadInt(fileByte, ptr + 0x20);

                        CostumeParamList.Add(CostumeParamEntry);
                    }
                } else
                {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry()
        {
            if (SelectedCostumeParam is not null)
            {
                CostumeParamList.Remove(SelectedCostumeParam);
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry()
        {
            if (SelectedCostumeParam is not null)
            {
                SelectedCostumeParam.EntryString = EntryString_field;
                SelectedCostumeParam.EntryIndex = EntryIndex_field;
                SelectedCostumeParam.PlayerSettingParamID = PlayerSettingParamID_field;
                SelectedCostumeParam.CharacterName = CharacterName_field;
                SelectedCostumeParam.EntryType = EntryType_field;
                SelectedCostumeParam.UnlockCost = UnlockCost_field;
                SelectedCostumeParam.UnlockCondition = UnlockCondition_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<CostumeParamModel> FunctionList, int member_index, int Selected)
        {
            for (int x = 0; x < FunctionList.Count; x++)
            {
                if (FunctionList[x].PlayerSettingParamID == member_index)
                {
                    if (Selected < x)
                    {
                        return x;
                    }
                }

            }
            return -1;
        }

        public void SearchEntry()
        {
            if (SearchIndex_field > 0)
            {
                if (SearchByteIndex(CostumeParamList, SearchIndex_field, SelectedCostumeParamIndex) != -1)
                {
                    SelectedCostumeParamIndex = SearchByteIndex(CostumeParamList, SearchIndex_field, SelectedCostumeParamIndex);
                    CollectionViewSource.GetDefaultView(CostumeParamList).MoveCurrentTo(SelectedCostumeParam);
                } else
                {
                    if (SearchByteIndex(CostumeParamList, SearchIndex_field, 0) != -1)
                    {
                        SelectedCostumeParamIndex = SearchByteIndex(CostumeParamList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(CostumeParamList).MoveCurrentTo(SelectedCostumeParam);
                    } else
                    {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else
            {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry()
        {
            CostumeParamModel CostumeParamEntry = new CostumeParamModel();
            if (SelectedCostumeParam is not null)
            {
                CostumeParamEntry = (CostumeParamModel)SelectedCostumeParam.Clone();

            } else
            {
                CostumeParamEntry.EntryString = EntryString_field;
                CostumeParamEntry.EntryIndex = EntryIndex_field;
                CostumeParamEntry.PlayerSettingParamID = PlayerSettingParamID_field;
                CostumeParamEntry.CharacterName = CharacterName_field;
                CostumeParamEntry.EntryType = EntryType_field;
                CostumeParamEntry.UnlockCost = UnlockCost_field;
                CostumeParamEntry.UnlockCondition = UnlockCondition_field;
            }
            List<int> Index_List = new List<int>();
            for (int c = 0; c < CostumeParamList.Count; c++)
            {
                Index_List.Add(CostumeParamList[c].EntryIndex);
            }
            int new_Index = 0;
            do
            {
                new_Index++;
            }
            while (Index_List.Contains(new_Index));
            CostumeParamEntry.EntryIndex = new_Index;
            CostumeParamList.Add(CostumeParamEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }

        // Modified LastCostume method to avoid parsing errors
        public string LastCostume()
        {
            List<int> CostumeIds = new List<int>();

            for (int i = 0; i < CostumeParamList.Count; i++)
            {
                if (!string.IsNullOrEmpty(CostumeParamList[i].EntryString))
                {
                    int underscoreIndex = CostumeParamList[i].EntryString.IndexOf("_");
                    if (underscoreIndex >= 0 && underscoreIndex < CostumeParamList[i].EntryString.Length - 1)
                    {
                        string numPart = CostumeParamList[i].EntryString.Substring(underscoreIndex + 1);
                        if (int.TryParse(numPart, out int id))
                        {
                            CostumeIds.Add(id);
                        }
                    }
                }
            }
            int maxCostumeId = CostumeIds.Count > 0 ? CostumeIds.Max() + 10 : 10;
            return "COSTUME_" + maxCostumeId.ToString("D5");
        }
        public int LastEntry()
        {
            List<int> Ids = new List<int>();

            for (int i = 0; i < CostumeParamList.Count; i++)
            {
                Ids.Add(CostumeParamList[i].EntryIndex);
            }
            int maxId = Ids.Max();
            return maxId + 1;
        }
        public void SaveFile()
        {
            if (filePath != "")
            {

                if (File.Exists(filePath + ".backup"))
                {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
                File.WriteAllBytes(filePath, ConvertToFile());
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
            } else
            {
                SaveFileAs();
            }
        }

        public void SaveFileAs(string basepath = "")
        {
            SaveFileDialog s = new SaveFileDialog();
            {
                s.DefaultExt = ".xfbin";
                s.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "")
                s.FileName = basepath;
            else
                s.ShowDialog();
            if (s.FileName == "")
            {
                return;
            }
            if (s.FileName == filePath)
            {
                if (File.Exists(filePath + ".backup"))
                {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
            } else
            {
                filePath = s.FileName;
            }
            File.WriteAllBytes(filePath, ConvertToFile());
            if (basepath == "")
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
        }

        public byte[] ConvertToFile()
        {
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/costumeParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "costumeParam");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "Page0");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "index");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrName = fileBytes36.Length;
            totalLength4 = PtrName;
            int AddedBytes = 0;

            while (fileBytes36.Length % 4 != 0)
            {
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



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[CostumeParamList.Count * 0x28]);

            int addSize = 0;

            List<int> EntryString_pointer = new List<int>();
            List<int> CharacterName_pointer = new List<int>();
            for (int x = 0; x < CostumeParamList.Count; x++)
            {
                int ptr = startPtr + (x * 0x28);
                EntryString_pointer.Add(fileBytes36.Length);
                if (CostumeParamList[x].EntryString != "" && CostumeParamList[x].EntryString is not null)
                {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CostumeParamList[x].EntryString));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = EntryString_pointer[x] - startPtr - x * 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr);
                    addSize += CostumeParamList[x].EntryString.Length + 1;
                }
                CharacterName_pointer.Add(fileBytes36.Length);
                if (CostumeParamList[x].CharacterName != "" && CostumeParamList[x].CharacterName is not null)
                {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CostumeParamList[x].CharacterName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = CharacterName_pointer[x] - startPtr - x * 0x28 - 0x10;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x10);
                    addSize += CostumeParamList[x].CharacterName.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList[x].EntryIndex), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList[x].PlayerSettingParamID), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList[x].EntryType), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList[x].UnlockCost), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList[x].UnlockCondition), ptr + 0x20);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CostumeParamList.Count * 0x28) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CostumeParamList.Count * 0x28) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CostumeParamList.Count), count_index);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x8D,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            });
        }

        private RelayCommand _saveFileAsCommand;
        public RelayCommand SaveFileAsCommand
        {
            get
            {
                return _saveFileAsCommand ??
                  (_saveFileAsCommand = new RelayCommand(obj => {
                      SaveFileAsAsync();
                  }));
            }
        }
        private RelayCommand _saveFileCommand;
        public RelayCommand SaveFileCommand
        {
            get
            {
                return _saveFileCommand ??
                  (_saveFileCommand = new RelayCommand(obj => {
                      SaveFileAsync();
                  }));
            }
        }
        private RelayCommand _openFileCommand;
        public RelayCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ??
                  (_openFileCommand = new RelayCommand(obj => {
                      OpenFileAsync();
                  }));
            }
        }
        private RelayCommand _deleteEntryCommand;
        public RelayCommand DeleteEntryCommand
        {
            get
            {
                return _deleteEntryCommand ??
                  (_deleteEntryCommand = new RelayCommand(obj => {
                      RemoveEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupEntryCommand;
        public RelayCommand AddDupEntryCommand
        {
            get
            {
                return _addDupEntryCommand ??
                  (_addDupEntryCommand = new RelayCommand(obj => {
                      AddDupEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveEntryCommand;
        public RelayCommand SaveEntryCommand
        {
            get
            {
                return _saveEntryCommand ??
                  (_saveEntryCommand = new RelayCommand(obj => {
                      SaveEntryAsync();
                  }));
            }
        }
        private RelayCommand _searchEntryCommand;
        public RelayCommand SearchEntryCommand
        {
            get
            {
                return _searchEntryCommand ??
                  (_searchEntryCommand = new RelayCommand(obj => {
                      SearchEntryAsync();
                  }));
            }
        }
        public async void SaveFileAsync()
        {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFile()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SaveFileAsAsync(string basepath = "")
        {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFileAs(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void OpenFileAsync(string basepath = "")
        {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => OpenFile(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SearchEntryAsync()
        {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SearchEntry()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void AddDupEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupEntry()));

        }
        public async void SaveEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveEntry()));

        }
        public async void RemoveEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveEntry()));

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
