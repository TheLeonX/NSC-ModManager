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
    public class SpecialInteractionManagerViewModel : INotifyPropertyChanged
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
        private int _mainCharacterID_field;
        public int MainCharacterID_field
        {
            get { return _mainCharacterID_field; }
            set
            {
                _mainCharacterID_field = value;
                OnPropertyChanged("MainCharacterID_field");
            }
        }
        private int _triggerID_field;
        public int TriggerID_field
        {
            get { return _triggerID_field; }
            set
            {
                _triggerID_field = value;
                OnPropertyChanged("TriggerID_field");
            }
        }
        private ObservableCollection<int> _triggerList;
        public ObservableCollection<int> TriggerList
        {
            get { return _triggerList; }
            set
            {
                _triggerList = value;
                OnPropertyChanged("TriggerList");
            }
        }
        private int _selectedTrigger;
        public int SelectedTrigger
        {
            get { return _selectedTrigger; }
            set
            {
                _selectedTrigger = value;
                OnPropertyChanged("SelectedTrigger");
            }
        }
        private int _selectedTriggerIndex;
        public int SelectedTriggerIndex
        {
            get { return _selectedTriggerIndex; }
            set
            {
                _selectedTriggerIndex = value;
                OnPropertyChanged("SelectedTriggerIndex");
            }
        }
        public ObservableCollection<SpecialInteractionManagerModel> SpecialInteractionList { get; set; }
        private SpecialInteractionManagerModel _selectedSpecialInteraction;
        public SpecialInteractionManagerModel SelectedSpecialInteraction
        {
            get { return _selectedSpecialInteraction; }
            set
            {
                _selectedSpecialInteraction = value;
                if (value is not null)
                {
                    MainCharacterID_field = value.MainCharacodeID;
                    TriggerList = value.TriggerList;
                }

                OnPropertyChanged("SelectedSpecialInteraction");
            }
        }
        private int _selectedSpecialInteractionIndex;
        public int SelectedSpecialInteractionIndex
        {
            get { return _selectedSpecialInteractionIndex; }
            set
            {
                _selectedSpecialInteractionIndex = value;
                OnPropertyChanged("SelectedSpecialInteractionIndex");
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
        
        public string filePath;
        public byte[] fileByte;
        public SpecialInteractionManagerViewModel()
        {
            LoadingStatePlay = Visibility.Hidden;
            SpecialInteractionList = new ObservableCollection<SpecialInteractionManagerModel>();
            TriggerList = new ObservableCollection<int>();
            filePath = "";
        }
        public void Clear()
        {
            SelectedSpecialInteractionIndex = -1;
            SpecialInteractionList.Clear();
            TriggerList.Clear();
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
                int pos = 0;
                // Ensure file has at least 4 bytes for the total entry count.
                if (fileByte.Length < 4)
                {
                    ModernWpf.MessageBox.Show("File too small.");
                    return;
                }

                // Read total entry count.
                int totalEntryCount = BinaryReader.b_ReadInt(fileByte, pos);
                pos += 4;
                //ModernWpf.MessageBox.Show($"Total entries: {totalEntryCount}");

                // Process each entry.
                for (int i = 0; i < totalEntryCount; i++)
                {
                    // Check that we have at least 8 bytes for main id and trigger count.
                    if (pos + 8 > fileByte.Length)
                    {
                        ModernWpf.MessageBox.Show($"Unexpected end of file for entry {i}.");
                        break;
                    }

                    // Read main character ID.
                    int mainCharacodeID = BinaryReader.b_ReadInt(fileByte, pos);
                    pos += 4;

                    // Read trigger count.
                    int triggerCount = BinaryReader.b_ReadInt(fileByte, pos);
                    pos += 4;

                    // Create new model.
                    SpecialInteractionManagerModel entry = new SpecialInteractionManagerModel();
                    entry.MainCharacodeID = mainCharacodeID;
                    entry.TriggerList = new ObservableCollection<int>();

                    // Read each trigger ID.
                    for (int j = 0; j < triggerCount; j++)
                    {
                        if (pos + 4 > fileByte.Length)
                        {
                            ModernWpf.MessageBox.Show($"Unexpected end of file while reading trigger {j} for entry {i}.");
                            break;
                        }
                        int triggerId = BinaryReader.b_ReadInt(fileByte, pos);
                        pos += 4;
                        entry.TriggerList.Add(triggerId);
                    }

                    // Add the entry to your list (assumed to be declared elsewhere, e.g., SpecialInteractionManagerModelList).
                    SpecialInteractionList.Add(entry);
                }
            }

        }

        public void RemoveEntry()
        {
            if (SelectedSpecialInteraction is not null)
            {
                SpecialInteractionList.Remove(SelectedSpecialInteraction);
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry()
        {
            if (SelectedSpecialInteraction is not null)
            {
                SelectedSpecialInteraction.MainCharacodeID = MainCharacterID_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public int SearchByteIndex(ObservableCollection<SpecialInteractionManagerModel> FunctionList, int member_index, int Selected)
        {
            for (int x = 0; x < FunctionList.Count; x++)
            {
                if (FunctionList[x].MainCharacodeID == member_index)
                {
                    return x;
                }

            }
            return -1;
        }

        public void SearchEntry()
        {
            if (SearchIndex_field > 0)
            {
                if (SearchByteIndex(SpecialInteractionList, SearchIndex_field, SelectedSpecialInteractionIndex) != -1)
                {
                    SelectedSpecialInteractionIndex = SearchByteIndex(SpecialInteractionList, SearchIndex_field, SelectedSpecialInteractionIndex);
                    CollectionViewSource.GetDefaultView(SpecialInteractionList).MoveCurrentTo(SelectedSpecialInteraction);
                } else
                {
                    if (SearchByteIndex(SpecialInteractionList, SearchIndex_field, 0) != -1)
                    {
                        SelectedSpecialInteractionIndex = SearchByteIndex(SpecialInteractionList, SearchIndex_field, -1);
                        CollectionViewSource.GetDefaultView(SpecialInteractionList).MoveCurrentTo(SelectedSpecialInteraction);
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
            SpecialInteractionManagerModel SpecialInteractionEntry = new SpecialInteractionManagerModel();
            if (SelectedSpecialInteraction is not null)
            {
                SpecialInteractionEntry = (SpecialInteractionManagerModel)SelectedSpecialInteraction.Clone();
            } else
            {

                SpecialInteractionEntry.MainCharacodeID = 0;
                SpecialInteractionEntry.TriggerList = new ObservableCollection<int>();
            }
            SpecialInteractionList.Add(SpecialInteractionEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void AddTriggerEntry()
        {
            if (SelectedSpecialInteraction is not null)
            {
                SelectedSpecialInteraction.TriggerList.Add(TriggerID_field);
            } else
            {

                ModernWpf.MessageBox.Show("Select Entry!");
            }
        }
        public void RemoveTriggerEntry()
        {
            if (SelectedSpecialInteraction is not null && SelectedTriggerIndex != -1)
            {
                SelectedSpecialInteraction.TriggerList.RemoveAt(SelectedTriggerIndex);
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
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
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                // Write the total number of entries.
                writer.Write(SpecialInteractionList.Count);

                // Write each entry.
                foreach (SpecialInteractionManagerModel model in SpecialInteractionList)
                {
                    // Write main character ID.
                    writer.Write(model.MainCharacodeID);

                    // Write trigger count.
                    writer.Write(model.TriggerList.Count);

                    // Write each trigger ID.
                    foreach (int trigger in model.TriggerList)
                    {
                        writer.Write(trigger);
                    }
                }

                return ms.ToArray();
            }
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
        private RelayCommand _deleteTriggerEntryCommand;
        public RelayCommand DeleteTriggerEntryCommand
        {
            get
            {
                return _deleteTriggerEntryCommand ??
                  (_deleteTriggerEntryCommand = new RelayCommand(obj => {
                      RemoveTriggerEntry();
                  }));
            }
        }

        private RelayCommand _addTriggerEntryCommand;
        public RelayCommand AddTriggerEntryCommand
        {
            get
            {
                return _addTriggerEntryCommand ??
                  (_addTriggerEntryCommand = new RelayCommand(obj => {
                      AddTriggerEntry();
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
