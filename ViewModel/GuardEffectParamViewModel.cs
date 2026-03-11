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
using Microsoft.WindowsAPICodePack.Shell;
using static System.Windows.Forms.AxHost;
using Microsoft.Win32;
using System.IO;
using NSC_ModManager.Model;
using NSC_ModManager;

namespace NSC_Toolbox.ViewModel
{
    public class GuardEffectParamViewModel : INotifyPropertyChanged
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

        private int _characodeID;
        public int CharacodeID
        {
            get { return _characodeID; }
            set
            {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private int _state;
        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        private string _effectPath;
        public string EffectPath
        {
            get { return _effectPath; }
            set
            {
                _effectPath = value;
                OnPropertyChanged("EffectPath");
            }
        }
        private string _startEffect;
        public string StartEffect
        {
            get { return _startEffect; }
            set
            {
                _startEffect = value;
                OnPropertyChanged("StartEffect");
            }
        }
        private string _loopEffect;
        public string LoopEffect
        {
            get { return _loopEffect; }
            set
            {
                _loopEffect = value;
                OnPropertyChanged("LoopEffect");
            }
        }
        private string _endEffect;
        public string EndEffect
        {
            get { return _endEffect; }
            set
            {
                _endEffect = value;
                OnPropertyChanged("EndEffect");
            }
        }
        private string _attachBone;
        public string AttachBone
        {
            get { return _attachBone; }
            set
            {
                _attachBone = value;
                OnPropertyChanged("AttachBone");
            }
        }
        public ObservableCollection<GuardEffectParamModel> GuardEffectParamList { get; set; }
        private GuardEffectParamModel _selectedGuardEffectParam;
        public GuardEffectParamModel SelectedGuardEffectParam
        {
            get { return _selectedGuardEffectParam; }
            set
            {
                _selectedGuardEffectParam = value;
                if (value is not null)
                {
                    CharacodeID = value.CharacodeID;
                    State = value.State;
                    EffectPath = value.EffectPath;
                    StartEffect = value.StartEffect;
                    LoopEffect = value.LoopEffect;
                    EndEffect = value.EndEffect;
                    AttachBone = value.AttachBone;
                }
                OnPropertyChanged("SelectedGuardEffectParam");
            }
        }
        private int _selectedGuardEffectParamIndex;
        public int SelectedGuardEffectParamIndex
        {
            get { return _selectedGuardEffectParamIndex; }
            set
            {
                _selectedGuardEffectParamIndex = value;
                OnPropertyChanged("SelectedGuardEffectParamIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public GuardEffectParamViewModel()
        {

            LoadingStatePlay = Visibility.Hidden;
            GuardEffectParamList = new ObservableCollection<GuardEffectParamModel>();
            filePath = "";
        }

        public void Clear()
        {
            GuardEffectParamList.Clear();
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
                int entryCount = fileByte.Length / 0xF8;
                for (int c = 0; c < entryCount; c++)
                {
                    int ptr = (c * 0xF8);
                    GuardEffectParamModel GuardEffectParamEntry = new GuardEffectParamModel();
                    GuardEffectParamEntry.CharacodeID = BinaryReaderV2.ReadInt32LittleEndian(fileByte, ptr);
                    GuardEffectParamEntry.State = BinaryReaderV2.ReadInt32LittleEndian(fileByte, ptr + 0x04);
                    GuardEffectParamEntry.EffectPath = BinaryReaderV2.ReadNullTerminatedString(fileByte, ptr + 0x08);
                    GuardEffectParamEntry.StartEffect = BinaryReaderV2.ReadNullTerminatedString(fileByte, ptr + 0x38);
                    GuardEffectParamEntry.LoopEffect = BinaryReaderV2.ReadNullTerminatedString(fileByte, ptr + 0x68);
                    GuardEffectParamEntry.EndEffect = BinaryReaderV2.ReadNullTerminatedString(fileByte, ptr + 0x98);
                    GuardEffectParamEntry.AttachBone = BinaryReaderV2.ReadNullTerminatedString(fileByte, ptr + 0xC8);
                    GuardEffectParamList.Add( GuardEffectParamEntry );
                }
            }

        }

        public void RemoveEntry()
        {
            if (SelectedGuardEffectParam is not null)
            {
                GuardEffectParamList.Remove(SelectedGuardEffectParam);
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_2"]);
            }
        }
        public void SaveEntry()
        {
            if (SelectedGuardEffectParam is not null)
            {
                SelectedGuardEffectParam.CharacodeID = CharacodeID;
                SelectedGuardEffectParam.State = State;
                SelectedGuardEffectParam.EffectPath = EffectPath;
                SelectedGuardEffectParam.StartEffect = StartEffect;
                SelectedGuardEffectParam.LoopEffect = LoopEffect;
                SelectedGuardEffectParam.EndEffect = EndEffect;
                SelectedGuardEffectParam.AttachBone = AttachBone;
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_1"]);
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_2"]);
            }
        }
        public int SearchByteIndex(ObservableCollection<GuardEffectParamModel> FunctionList, int member_index, int Selected)
        {
            for (int x = 0; x < FunctionList.Count; x++)
            {
                if (FunctionList[x].CharacodeID == member_index)
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
                if (SearchByteIndex(GuardEffectParamList, SearchIndex_field, SelectedGuardEffectParamIndex) != -1)
                {
                    SelectedGuardEffectParamIndex = SearchByteIndex(GuardEffectParamList, SearchIndex_field, SelectedGuardEffectParamIndex);
                    CollectionViewSource.GetDefaultView(GuardEffectParamList).MoveCurrentTo(SelectedGuardEffectParam);
                } else
                {
                    if (SearchByteIndex(GuardEffectParamList, SearchIndex_field, 0) != -1)
                    {
                        SelectedGuardEffectParamIndex = SearchByteIndex(GuardEffectParamList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(GuardEffectParamList).MoveCurrentTo(SelectedGuardEffectParam);
                    } else
                    {
                        ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_3"], "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_4"]);
            }
        }


        public void AddDupEntry()
        {
            GuardEffectParamModel guardEffectParamEntry = new GuardEffectParamModel();
            if (SelectedGuardEffectParam is not null)
            {
                guardEffectParamEntry = (GuardEffectParamModel)SelectedGuardEffectParam.Clone();
            } else
            {
                guardEffectParamEntry.CharacodeID = 0;
                guardEffectParamEntry.State = 0;
                guardEffectParamEntry.EffectPath = "";
                guardEffectParamEntry.StartEffect = "";
                guardEffectParamEntry.LoopEffect = "";
                guardEffectParamEntry.EndEffect = "";
                guardEffectParamEntry.AttachBone = "";
            }
            GuardEffectParamList.Add(guardEffectParamEntry);
            ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_2"]);
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
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_3"] + filePath + ".");
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
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_3"] + filePath + ".");
        }

        public byte[] ConvertToFile()
        {

            byte[] fileBytes = new byte[0];

            fileBytes = BinaryWriterV2.InsertBytes(fileBytes, new byte[GuardEffectParamList.Count * 0xF8]);

            for (int x = 0; x < GuardEffectParamList.Count; x++)
            {
                int ptr = (x * 0xF8); 
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, BitConverter.GetBytes(GuardEffectParamList[x].CharacodeID), ptr);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, BitConverter.GetBytes(GuardEffectParamList[x].State), ptr + 0x04);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, Encoding.ASCII.GetBytes(GuardEffectParamList[x].EffectPath), ptr + 0x08);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, Encoding.ASCII.GetBytes(GuardEffectParamList[x].StartEffect), ptr + 0x38);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, Encoding.ASCII.GetBytes(GuardEffectParamList[x].LoopEffect), ptr + 0x68);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, Encoding.ASCII.GetBytes(GuardEffectParamList[x].EndEffect), ptr + 0x98);
                fileBytes = BinaryWriterV2.ReplaceBytes(fileBytes, Encoding.ASCII.GetBytes(GuardEffectParamList[x].AttachBone), ptr + 0xC8);
            }
            return fileBytes;
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
