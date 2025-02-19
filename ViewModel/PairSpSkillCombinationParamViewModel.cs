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
using System.Windows.Automation;
using DynamicData;

namespace NSC_ModManager.ViewModel
{
    public class PairSpSkillCombinationParamViewModel : INotifyPropertyChanged
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

        private string _TUJ_Name_field;
        public string TUJ_Name_field
        {
            get { return _TUJ_Name_field; }
            set
            {
                _TUJ_Name_field = value;
                OnPropertyChanged("TUJ_Name_field");
            }
        }
        private int _TUJ_ID_field;
        public int TUJ_ID_field
        {
            get { return _TUJ_ID_field; }
            set
            {
                _TUJ_ID_field = value;
                OnPropertyChanged("TUJ_ID_field");
            }
        }
        private float _unk1_field;
        public float Unk1_field
        {
            get { return _unk1_field; }
            set
            {
                _unk1_field = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        private float _unk2_field;
        public float Unk2_field
        {
            get { return _unk2_field; }
            set
            {
                _unk2_field = value;
                OnPropertyChanged("Unk2_field");
            }
        }
        private int _memberCount_field;
        public int MemberCount_field
        {
            get { return _memberCount_field; }
            set
            {
                _memberCount_field = value;
                OnPropertyChanged("MemberCount_field");
            }
        }
        private bool _condition1_field;
        public bool Condition1_field
        {
            get { return _condition1_field; }
            set
            {
                _condition1_field = value;
                OnPropertyChanged("Condition1_field");
            }
        }
        private bool _condition2_field;
        public bool Condition2_field
        {
            get { return _condition2_field; }
            set
            {
                _condition2_field = value;
                OnPropertyChanged("Condition2_field");
            }
        }

        private int _selectedCharacodeID_field;
        public int SelectedCharacodeID_field
        {
            get { return _selectedCharacodeID_field; }
            set
            {
                _selectedCharacodeID_field = value;
                OnPropertyChanged("SelectedCharacodeID_field");
            }
        }
        private ObservableCollection<int> _characodeIDList;
        public ObservableCollection<int> CharacodeIDList
        {
            get { return _characodeIDList; }
            set
            {
                _characodeIDList = value;
                OnPropertyChanged("CharacodeIDList");
            }
        }
        private int _selectedCharacodeIDIndex;
        public int SelectedCharacodeIDIndex
        {
            get { return _selectedCharacodeIDIndex; }
            set
            {
                if (_selectedCharacodeIDIndex == value)
                    return;

                _selectedCharacodeIDIndex = value;

                if (value == -1)
                {
                    // When set to -1, clear the selected value.
                    SelectedCharacodeID_field = default;
                } else if (value >= 0 && value < CharacodeIDList.Count)
                {
                    SelectedCharacodeID_field = CharacodeIDList[value];
                }

                OnPropertyChanged(nameof(SelectedCharacodeIDIndex));
                OnPropertyChanged(nameof(SelectedCharacodeID_field));
            }
        }
        public ObservableCollection<PairSpSkillCombinationParamModel> pairSpSkillList { get; set; }
        private PairSpSkillCombinationParamModel _selectedPairSpSkill;
        public PairSpSkillCombinationParamModel SelectedPairSpSkill
        {
            get { return _selectedPairSpSkill; }
            set
            {
                _selectedPairSpSkill = value;
                if (value is not null)
                {
                    TUJ_ID_field = value.TUJ_ID;
                    TUJ_Name_field = value.TUJ_Name;
                    Unk1_field = value.Unk1;
                    Unk2_field = value.Unk2;
                    MemberCount_field = value.MemberCount;
                    Condition1_field = value.Condition1;
                    Condition2_field = value.Condition2;
                    CharacodeIDList = value.CharacodeList;
                    SelectedCharacodeIDIndex = -1;
                }

                OnPropertyChanged("SelectedPairSpSkill");
            }
        }
        


        private int _selectedPairSpSkillIndex;
        public int SelectedPairSpSkillIndex
        {
            get { return _selectedPairSpSkillIndex; }
            set
            {
                _selectedPairSpSkillIndex = value;
                OnPropertyChanged("SelectedPairSpSkillIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public PairSpSkillCombinationParamViewModel()
        {

            LoadingStatePlay = Visibility.Hidden;
            pairSpSkillList = new ObservableCollection<PairSpSkillCombinationParamModel>();
            CharacodeIDList = new ObservableCollection<int>();

            filePath = "";
        }

        public void Clear()
        {
            pairSpSkillList.Clear();
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
                if (BinName.Contains("pairSpSkillCombinationParam"))
                {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++)
                    {
                        int ptr = StartOfFile + 0x10 + (c * 0x70);
                        PairSpSkillCombinationParamModel pairSpSkillEntry = new PairSpSkillCombinationParamModel();
                        pairSpSkillEntry.TUJ_Name = BinaryReader.b_ReadStringPtr(fileByte, ptr);
                        pairSpSkillEntry.TUJ_ID = BinaryReader.b_ReadInt(fileByte, ptr + 0x08);
                        pairSpSkillEntry.Unk1 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x0C);
                        pairSpSkillEntry.Unk2 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x10);
                        pairSpSkillEntry.MemberCount = BinaryReader.b_ReadInt(fileByte, ptr + 0x14);
                        pairSpSkillEntry.Condition1 = BinaryReader.b_ReadBool(fileByte, ptr + 0x18);
                        pairSpSkillEntry.Condition2 = BinaryReader.b_ReadBool(fileByte, ptr + 0x1C);
                        pairSpSkillEntry.CharacodeList = new ObservableCollection<int>();

                        for (int i = 0; i < 20; i++)
                        {
                            int char_id = BinaryReader.b_ReadInt(fileByte, ptr + 0x20 + (i * 0x04));
                            if (char_id != 0)
                                pairSpSkillEntry.CharacodeList.Add(char_id);
                        }

                        pairSpSkillList.Add(pairSpSkillEntry);
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
            if (SelectedPairSpSkill is not null)
            {
                pairSpSkillList.Remove(SelectedPairSpSkill);
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry()
        {
            if (SelectedPairSpSkill is null)
            {
                ModernWpf.MessageBox.Show("Select entry!");
                return;
            }

            // Check if any other entry already has the same TUJ_ID_field.
            if (pairSpSkillList.Any(x => x.TUJ_ID == TUJ_ID_field && x != SelectedPairSpSkill))
            {
                ModernWpf.MessageBox.Show("Section already exists with that ID");
                return;
            }

            SelectedPairSpSkill.TUJ_Name = TUJ_Name_field;
            SelectedPairSpSkill.TUJ_ID = TUJ_ID_field;
            SelectedPairSpSkill.Unk1 = Unk1_field;
            SelectedPairSpSkill.Unk2 = Unk2_field;
            SelectedPairSpSkill.MemberCount = MemberCount_field;
            SelectedPairSpSkill.Condition1 = Condition1_field;
            SelectedPairSpSkill.Condition2 = Condition2_field;
            ModernWpf.MessageBox.Show("Entry was saved!");
        }

        public int SearchByteIndex(ObservableCollection<PairSpSkillCombinationParamModel> FunctionList, int tuj_index, int Selected)
        {
            for (int x = 0; x < FunctionList.Count; x++)
            {
                if (FunctionList[x].TUJ_ID == tuj_index)
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
                if (SearchByteIndex(pairSpSkillList, SearchIndex_field, SelectedPairSpSkillIndex) != -1)
                {
                    SelectedPairSpSkillIndex = SearchByteIndex(pairSpSkillList, SearchIndex_field, SelectedPairSpSkillIndex);
                    CollectionViewSource.GetDefaultView(pairSpSkillList).MoveCurrentTo(SelectedPairSpSkill);
                } else
                {
                    if (SearchByteIndex(pairSpSkillList, SearchIndex_field, 0) != -1)
                    {
                        SelectedPairSpSkillIndex = SearchByteIndex(pairSpSkillList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(pairSpSkillList).MoveCurrentTo(SelectedPairSpSkill);
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
            PairSpSkillCombinationParamModel pairSpSkillEntry = new PairSpSkillCombinationParamModel();
            if (SelectedPairSpSkill is not null)
            {
                pairSpSkillEntry = (PairSpSkillCombinationParamModel)SelectedPairSpSkill.Clone();
                pairSpSkillEntry.TUJ_ID = pairSpSkillList.Count();
            } else
            {
                pairSpSkillEntry.TUJ_Name = TUJ_Name_field;
                pairSpSkillEntry.TUJ_ID = pairSpSkillList.Count();
                pairSpSkillEntry.Unk1 = Unk1_field;
                pairSpSkillEntry.Unk2 = Unk2_field;
                pairSpSkillEntry.MemberCount = MemberCount_field;
                pairSpSkillEntry.Condition1 = Condition1_field;
                pairSpSkillEntry.Condition2 = Condition2_field;
                pairSpSkillEntry.CharacodeList = new ObservableCollection<int>();
            }
            pairSpSkillList.Add(pairSpSkillEntry);
            SelectedPairSpSkillIndex = pairSpSkillList.Count - 1;
            CollectionViewSource.GetDefaultView(pairSpSkillList).MoveCurrentTo(pairSpSkillList[SelectedPairSpSkillIndex]);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void RemoveCharacodeEntry()
        {
            if (SelectedCharacodeIDIndex != -1)
            {
                CharacodeIDList.RemoveAt(SelectedCharacodeIDIndex);
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveCharacodeEntry()
        {
            if (CharacodeIDList.Contains(SelectedCharacodeID_field) && SelectedCharacodeID_field != CharacodeIDList[SelectedCharacodeIDIndex])
            {
                ModernWpf.MessageBox.Show("You already added that entry.");
                return;
            }

            if (SelectedCharacodeIDIndex != -1)
            {
                CharacodeIDList[SelectedCharacodeIDIndex] = SelectedCharacodeID_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else
            {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }



        public void AddDupCharacodeEntry()
        {
            if (CharacodeIDList.Contains(SelectedCharacodeID_field))
            {
                ModernWpf.MessageBox.Show("You already added that entry.");
                return;
            }
            if (CharacodeIDList.Count < 20)
            {
                CharacodeIDList.Add(SelectedCharacodeID_field);
                SelectedCharacodeIDIndex = CharacodeIDList.Count - 1;
                CollectionViewSource.GetDefaultView(CharacodeIDList).MoveCurrentTo(CharacodeIDList[SelectedCharacodeIDIndex]);
                ModernWpf.MessageBox.Show("Entry was added!");
            } else
            {
                ModernWpf.MessageBox.Show("You can't add more entries!");
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
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/pairSpSkillCombinationParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "pairSpSkillCombinationParam");
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



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE9, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[pairSpSkillList.Count * 0x70]);

            int addSize = 0;

            List<int> TUJ_Name_pointer = new List<int>();
            for (int x = 0; x < pairSpSkillList.Count; x++)
            {
                int ptr = startPtr + (x * 0x70);
                TUJ_Name_pointer.Add(fileBytes36.Length);
                if (pairSpSkillList[x].TUJ_Name != "" && pairSpSkillList[x].TUJ_Name is not null)
                {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(pairSpSkillList[x].TUJ_Name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = TUJ_Name_pointer[x] - startPtr - x * 0x70;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr);
                    addSize += pairSpSkillList[x].TUJ_Name.Length + 1;
                }
                
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].TUJ_ID), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].Unk1), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].Unk2), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].MemberCount), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].Condition1), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].Condition2), ptr + 0x1C);
                for (int i = 0; i < pairSpSkillList[x].CharacodeList.Count; i++)
                {

                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList[x].CharacodeList[i]), ptr + 0x20 + (i * 0x04));
                }
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((pairSpSkillList.Count * 0x70) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((pairSpSkillList.Count * 0x70) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(pairSpSkillList.Count), count_index);
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

        private RelayCommand _deleteCharacodeEntryCommand;
        public RelayCommand DeleteCharacodeEntryCommand
        {
            get
            {
                return _deleteCharacodeEntryCommand ??
                  (_deleteCharacodeEntryCommand = new RelayCommand(obj => {
                      RemoveCharacodeEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupCharacodeEntryCommand;
        public RelayCommand AddDupCharacodeEntryCommand
        {
            get
            {
                return _addDupCharacodeEntryCommand ??
                  (_addDupCharacodeEntryCommand = new RelayCommand(obj => {
                      AddDupCharacodeEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveCharacodeEntryCommand;
        public RelayCommand SaveCharacodeEntryCommand
        {
            get
            {
                return _saveCharacodeEntryCommand ??
                  (_saveCharacodeEntryCommand = new RelayCommand(obj => {
                      SaveCharacodeEntryAsync();
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

        public async void AddDupCharacodeEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupCharacodeEntry()));

        }
        public async void SaveCharacodeEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveCharacodeEntry()));

        }
        public async void RemoveCharacodeEntryAsync()
        {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveCharacodeEntry()));

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
