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
    public class ConditionManagerViewModel : INotifyPropertyChanged
    {
        private string _searchTextBox_field;
        public string SearchTextBox_field
        {
            get { return _searchTextBox_field; }
            set
            {
                _searchTextBox_field = value;
                OnPropertyChanged("SearchTextBox_field");
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
        private string _conditionName_field;
        public string ConditionName_field
        {
            get { return _conditionName_field; }
            set
            {
                _conditionName_field = value;
                OnPropertyChanged("ConditionName_field");
            }
        }

        private string _afterConditionName_field;
        public string AfterConditionName_field
        {
            get { return _afterConditionName_field; }
            set
            {
                _afterConditionName_field = value;
                OnPropertyChanged("AfterConditionName_field");
            }
        }

        private int _conditionValue_field;
        public int ConditionValue_field
        {
            get { return _conditionValue_field; }
            set
            {
                _conditionValue_field = value;
                OnPropertyChanged("ConditionValue_field");
            }
        }

        private int _conditionIcon_field;
        public int ConditionIcon_field
        {
            get { return _conditionIcon_field; }
            set
            {
                _conditionIcon_field = value;
                if(value <= 31)
                {
                    SelectConditionIcon = value;
                } else if (value == 40)
                {

                    SelectConditionIcon = 32;
                } else if (value == 41)
                {

                    SelectConditionIcon = 33;
                }
                OnPropertyChanged("ConditionIcon_field");
            }
        }
        private int _selectConditionIcon;
        public int SelectConditionIcon
        {
            get { return _selectConditionIcon; }
            set
            {
                _selectConditionIcon = value;
                OnPropertyChanged("SelectConditionIcon");
            }
        }
        private int _conditionStatusEffect_field;
        public int ConditionStatusEffect_field
        {
            get { return _conditionStatusEffect_field; }
            set
            {
                _conditionStatusEffect_field = value;
                OnPropertyChanged("ConditionStatusEffect_field");
            }
        }

        private int _conditionAuraEffect_field;
        public int ConditionAuraEffect_field
        {
            get { return _conditionAuraEffect_field; }
            set
            {
                _conditionAuraEffect_field = value;
                OnPropertyChanged("ConditionAuraEffect_field");
            }
        }

        private ObservableCollection<ConditionManagerModel> _conditionList;
        public ObservableCollection<ConditionManagerModel> ConditionList
        {
            get { return _conditionList; }
            set
            {
                _conditionList = value;
                OnPropertyChanged("ConditionList");
            }
        }

        private ConditionManagerModel _selectedCondition;
        public ConditionManagerModel SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                _selectedCondition = value;
                if (value is not null)
                {
                    ConditionName_field = value.ConditionName;
                    AfterConditionName_field = value.AfterConditionName;
                    ConditionValue_field = value.ConditionValue;
                    ConditionIcon_field = value.ConditionIcon;
                    ConditionStatusEffect_field = value.ConditionStatusEffect;
                    ConditionAuraEffect_field = value.ConditionAuraEffect;
                }
                OnPropertyChanged("SelectedCondition");
            }
        }
        private int _selectedConditionIndex;
        public int SelectedConditionIndex
        {
            get { return _selectedConditionIndex; }
            set
            {
                _selectedConditionIndex = value;
                OnPropertyChanged("SelectedConditionIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public ConditionManagerViewModel()
        {

            LoadingStatePlay = Visibility.Hidden;
            ConditionList = new ObservableCollection<ConditionManagerModel>();
            filePath = "";
        }

        public void Clear()
        {
            ConditionList.Clear();
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
                byte[] fileByte = File.ReadAllBytes(filePath);
                int entryCount = fileByte.Length / 0x70;
                for (int c = 0; c < entryCount; c++)
                {
                    int ptr = (c * 0x70);
                    ConditionManagerModel conditionEntry = new ConditionManagerModel();
                    conditionEntry.ConditionName = BinaryReader.b_ReadString(fileByte, ptr);
                    conditionEntry.AfterConditionName = BinaryReader.b_ReadString(fileByte, ptr + 0x30);
                    conditionEntry.ConditionValue = BinaryReader.b_ReadInt(fileByte, ptr + 0x60);
                    conditionEntry.ConditionIcon = BinaryReader.b_ReadInt(fileByte, ptr + 0x64);
                    conditionEntry.ConditionStatusEffect = BinaryReader.b_ReadInt(fileByte, ptr + 0x68);
                    conditionEntry.ConditionAuraEffect = BinaryReader.b_ReadInt(fileByte, ptr + 0x6C);
                    ConditionList.Add(conditionEntry);
                }
            }

        }

        public void RemoveEntry()
        {
            if (SelectedCondition is not null)
            {
                ConditionList.Remove(SelectedCondition);
                OnPropertyChanged("ConditionList");
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_2"]);
            }
        }
        public void SaveEntry()
        {
            if (SelectedCondition is not null)
            {
                SelectedCondition.ConditionName = ConditionName_field;
                SelectedCondition.AfterConditionName = AfterConditionName_field;
                SelectedCondition.ConditionValue = ConditionValue_field;
                SelectedCondition.ConditionStatusEffect = ConditionStatusEffect_field;
                SelectedCondition.ConditionAuraEffect = ConditionAuraEffect_field;


                if (SelectConditionIcon <= 31)
                {
                    SelectedCondition.ConditionIcon = SelectConditionIcon;
                } else if (SelectConditionIcon == 32)
                {

                    SelectedCondition.ConditionIcon = 40;
                } else if (SelectConditionIcon == 33)
                {

                    SelectedCondition.ConditionIcon = 41;
                }


                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_1"]);
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_2"]);
            }

        }
        public int SearchStringIndex(ObservableCollection<ConditionManagerModel> FunctionList, string member_name, int Selected)
        {
            for (int x = 0; x < FunctionList.Count; x++)
            {

                string mainString = FunctionList[x].ConditionName;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x)
                {
                    return x;
                }

            }
            return -1;
        }
        public void SearchEntry()
        {
            if (SearchTextBox_field is not null)
            {
                if (SearchStringIndex(ConditionList, SearchTextBox_field, SelectedConditionIndex) != -1)
                {
                    SelectedConditionIndex = SearchStringIndex(ConditionList, SearchTextBox_field, SelectedConditionIndex);
                    CollectionViewSource.GetDefaultView(ConditionList).MoveCurrentTo(SelectedCondition);
                } else
                {
                    if (SearchStringIndex(ConditionList, SearchTextBox_field, 0) != -1)
                    {
                        SelectedConditionIndex = SearchStringIndex(ConditionList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(ConditionList).MoveCurrentTo(SelectedCondition);
                    } else
                    {
                        ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_5"], "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_17"]);
            }
        }


        public void AddDupEntry()
        {
            ConditionManagerModel conditionEntry = new ConditionManagerModel();
            if (SelectedCondition is not null)
            {
                conditionEntry = (ConditionManagerModel)SelectedCondition.Clone();
            } else
            {
                conditionEntry.ConditionName = "NEW_CONDITION";
                conditionEntry.AfterConditionName = "";
                conditionEntry.ConditionValue = 1;
                conditionEntry.ConditionIcon = 0;
                conditionEntry.ConditionStatusEffect = 5;
                conditionEntry.ConditionAuraEffect = 5;
            }
            ConditionList.Add(conditionEntry);
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
            byte[] fileBytes36 = new byte[0];

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x70 * ConditionList.Count]);
            for (int c = 0; c < ConditionList.Count; c++)
            {
                int ptr = (c * 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, Encoding.ASCII.GetBytes(ConditionList[c].ConditionName ?? ""), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, Encoding.ASCII.GetBytes(ConditionList[c].AfterConditionName ?? ""), ptr + 0x30);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionValue), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionIcon), ptr + 0x64);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionStatusEffect), ptr + 0x68);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionAuraEffect), ptr + 0x6C);
            }
            
            return fileBytes36;
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
