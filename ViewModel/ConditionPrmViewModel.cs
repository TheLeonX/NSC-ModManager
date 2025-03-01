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
using System.Drawing;
using Microsoft.Win32;
using System.IO;

namespace NSC_ModManager.ViewModel
{
    public class ConditionPrmViewModel : INotifyPropertyChanged
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

        private int _conditionDuration_field;
        public int ConditionDuration_field
        {
            get { return _conditionDuration_field; }
            set
            {
                _conditionDuration_field = value;
                OnPropertyChanged("ConditionDuration_field");
            }
        }

        private float _conditionATK_field;
        public float ConditionATK_field
        {
            get { return _conditionATK_field; }
            set
            {
                _conditionATK_field = value;
                OnPropertyChanged("ConditionATK_field");
            }
        }

        private float _conditionDEF_field;
        public float ConditionDEF_field
        {
            get { return _conditionDEF_field; }
            set
            {
                _conditionDEF_field = value;
                OnPropertyChanged("ConditionDEF_field");
            }
        }

        private float _conditionSPD_field;
        public float ConditionSPD_field
        {
            get { return _conditionSPD_field; }
            set
            {
                _conditionSPD_field = value;
                OnPropertyChanged("ConditionSPD_field");
            }
        }

        private float _conditionSPT_ATK_field;
        public float ConditionSPT_ATK_field
        {
            get { return _conditionSPT_ATK_field; }
            set
            {
                _conditionSPT_ATK_field = value;
                OnPropertyChanged("ConditionSPT_ATK_field");
            }
        }

        private float _conditionHP_Recover_field;
        public float ConditionHP_Recover_field
        {
            get { return _conditionHP_Recover_field; }
            set
            {
                _conditionHP_Recover_field = value;
                OnPropertyChanged("ConditionHP_Recover_field");
            }
        }

        private float _conditionPoison_field;
        public float ConditionPoison_field
        {
            get { return _conditionPoison_field; }
            set
            {
                _conditionPoison_field = value;
                OnPropertyChanged("ConditionPoison_field");
            }
        }

        private float _conditionChakra_recover_field;
        public float ConditionChakra_recover_field
        {
            get { return _conditionChakra_recover_field; }
            set
            {
                _conditionChakra_recover_field = value;
                OnPropertyChanged("ConditionChakra_recover_field");
            }
        }

        private float _conditionChakra_shave_field;
        public float ConditionChakra_shave_field
        {
            get { return _conditionChakra_shave_field; }
            set
            {
                _conditionChakra_shave_field = value;
                OnPropertyChanged("ConditionChakra_shave_field");
            }
        }
        private float _conditionChakra_unk_field;
        public float ConditionChakra_unk_field
        {
            get { return _conditionChakra_unk_field; }
            set
            {
                _conditionChakra_unk_field = value;
                OnPropertyChanged("ConditionChakra_unk_field");
            }
        }
        private float _conditionChakra_revival_field;
        public float ConditionChakra_revival_field
        {
            get { return _conditionChakra_revival_field; }
            set
            {
                _conditionChakra_revival_field = value;
                OnPropertyChanged("ConditionChakra_revival_field");
            }
        }

        private float _conditionChakra_Drain_field;
        public float ConditionChakra_Drain_field
        {
            get { return _conditionChakra_Drain_field; }
            set
            {
                _conditionChakra_Drain_field = value;
                OnPropertyChanged("ConditionChakra_Drain_field");
            }
        }

        private float _conditionChakra_field;
        public float ConditionChakra_field
        {
            get { return _conditionChakra_field; }
            set
            {
                _conditionChakra_field = value;
                OnPropertyChanged("ConditionChakra_field");
            }
        }

        private float _conditionChakra_Usage_field;
        public float ConditionChakra_Usage_field
        {
            get { return _conditionChakra_Usage_field; }
            set
            {
                _conditionChakra_Usage_field = value;
                OnPropertyChanged("ConditionChakra_Usage_field");
            }
        }

        private float _conditionSupport_field;
        public float ConditionSupport_field
        {
            get { return _conditionSupport_field; }
            set
            {
                _conditionSupport_field = value;
                OnPropertyChanged("ConditionSupport_field");
            }
        }

        private float _conditionTeam_field;
        public float ConditionTeam_field
        {
            get { return _conditionTeam_field; }
            set
            {
                _conditionTeam_field = value;
                OnPropertyChanged("ConditionTeam_field");
            }
        }

        private float _conditionGuardBreak_field;
        public float ConditionGuardBreak_field
        {
            get { return _conditionGuardBreak_field; }
            set
            {
                _conditionGuardBreak_field = value;
                OnPropertyChanged("ConditionGuardBreak_field");
            }
        }

        private float _conditionDodge_field;
        public float ConditionDodge_field
        {
            get { return _conditionDodge_field; }
            set
            {
                _conditionDodge_field = value;
                OnPropertyChanged("ConditionDodge_field");
            }
        }

        private bool _conditionProjectile_field;
        public bool ConditionProjectile_field
        {
            get { return _conditionProjectile_field; }
            set
            {
                _conditionProjectile_field = value;
                OnPropertyChanged("ConditionProjectile_field");
            }
        }

        private bool _conditionAutoDodge_field;
        public bool ConditionAutoDodge_field
        {
            get { return _conditionAutoDodge_field; }
            set
            {
                _conditionAutoDodge_field = value;
                OnPropertyChanged("ConditionAutoDodge_field");
            }
        }

        private bool _conditionSeal_field;
        public bool ConditionSeal_field
        {
            get { return _conditionSeal_field; }
            set
            {
                _conditionSeal_field = value;
                OnPropertyChanged("ConditionSeal_field");
            }
        }

        private bool _conditionSleep_field;
        public bool ConditionSleep_field
        {
            get { return _conditionSleep_field; }
            set
            {
                _conditionSleep_field = value;
                OnPropertyChanged("ConditionSleep_field");
            }
        }

        private bool _conditionStun_field;
        public bool ConditionStun_field
        {
            get { return _conditionStun_field; }
            set
            {
                _conditionStun_field = value;
                OnPropertyChanged("ConditionStun_field");
            }
        }

        private int _conditionFlashType_field;
        public int ConditionFlashType_field
        {
            get { return _conditionFlashType_field; }
            set
            {
                _conditionFlashType_field = value;
                OnPropertyChanged("ConditionFlashType_field");
            }
        }

        private Color _conditionFlashColor_field;
        public Color ConditionFlashColor_field
        {
            get { return _conditionFlashColor_field; }
            set
            {
                _conditionFlashColor_field = value;
                OnPropertyChanged("ConditionFlashColor_field");
            }
        }

        private float _conditionFlash_unk1_field;
        public float ConditionFlash_unk1_field
        {
            get { return _conditionFlash_unk1_field; }
            set
            {
                _conditionFlash_unk1_field = value;
                OnPropertyChanged("ConditionFlash_unk1_field");
            }
        }

        private float _conditionFlash_interval_field;
        public float ConditionFlash_interval_field
        {
            get { return _conditionFlash_interval_field; }
            set
            {
                _conditionFlash_interval_field = value;
                OnPropertyChanged("ConditionFlash_interval_field");
            }
        }

        private float _conditionFlash_unk2_field;
        public float ConditionFlash_unk2_field
        {
            get { return _conditionFlash_unk2_field; }
            set
            {
                _conditionFlash_unk2_field = value;
                OnPropertyChanged("ConditionFlash_unk2_field");
            }
        }

        private float _conditionFlash_opacity_field;
        public float ConditionFlash_opacity_field
        {
            get { return _conditionFlash_opacity_field; }
            set
            {
                _conditionFlash_opacity_field = value;
                OnPropertyChanged("ConditionFlash_opacity_field");
            }
        }
        private ObservableCollection<ConditionPrmModel> _conditionList;
        public ObservableCollection<ConditionPrmModel> ConditionList
        {
            get { return _conditionList; }
            set
            {
                _conditionList = value;
                OnPropertyChanged("ConditionList");
            }
        }

        private ConditionPrmModel _selectedCondition;
        public ConditionPrmModel SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                _selectedCondition = value;
                if (value is not null)
                {
                    ConditionName_field = value.ConditionName;
                    ConditionDuration_field = value.ConditionDuration;
                    ConditionATK_field = value.ConditionATK;
                    ConditionDEF_field = value.ConditionDEF;
                    ConditionSPD_field = value.ConditionSPD;
                    ConditionSPT_ATK_field = value.ConditionSPT_ATK;
                    ConditionHP_Recover_field = value.ConditionHP_Recover;
                    ConditionPoison_field = value.ConditionPoison;
                    ConditionChakra_recover_field = value.ConditionChakra_recover;
                    ConditionChakra_shave_field = value.ConditionChakra_shave;
                    ConditionChakra_revival_field = value.ConditionChakra_revival;
                    ConditionChakra_Drain_field = value.ConditionChakra_Drain;
                    ConditionChakra_field = value.ConditionChakra;
                    ConditionChakra_Usage_field = value.ConditionChakra_Usage;
                    ConditionChakra_unk_field = value.ConditionChakra_unk;
                    ConditionSupport_field = value.ConditionSupport;
                    ConditionTeam_field = value.ConditionTeam;
                    ConditionGuardBreak_field = value.ConditionGuardBreak;
                    ConditionDodge_field = value.ConditionDodge;
                    ConditionProjectile_field = value.ConditionProjectile;
                    ConditionAutoDodge_field = value.ConditionAutoDodge;
                    ConditionSeal_field = value.ConditionSeal;
                    ConditionSleep_field = value.ConditionSleep;
                    ConditionStun_field = value.ConditionStun;
                    ConditionFlashType_field = value.ConditionFlashType;
                    ConditionFlashColor_field = value.ConditionFlashColor;
                    ConditionFlash_unk1_field = value.ConditionFlash_unk1;
                    ConditionFlash_interval_field = value.ConditionFlash_interval;
                    ConditionFlash_unk2_field = value.ConditionFlash_unk2;
                    ConditionFlash_opacity_field = value.ConditionFlash_opacity;

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
        public ConditionPrmViewModel()
        {

            LoadingStatePlay = Visibility.Hidden;
            ConditionList = new ObservableCollection<ConditionPrmModel>();
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
                int StartOfFile = 0x34 + BinaryReader.b_ReadIntRev(fileByte, 16);
                if (BinName.Contains("conditionprm"))
                {
                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++)
                    {
                        int ptr = StartOfFile + 8 + (c * 0xB0);
                        ConditionPrmModel conditionEntry = new ConditionPrmModel();
                        conditionEntry.ConditionName = BinaryReader.b_ReadString(fileByte, ptr);
                        conditionEntry.ConditionDuration = BinaryReader.b_ReadInt(fileByte, ptr + 0x40);
                        conditionEntry.ConditionATK = BinaryReader.b_ReadFloat(fileByte, ptr + 0x44);
                        conditionEntry.ConditionDEF = BinaryReader.b_ReadFloat(fileByte, ptr + 0x48);
                        conditionEntry.ConditionSPD = BinaryReader.b_ReadFloat(fileByte, ptr + 0x4C);
                        conditionEntry.ConditionSPT_ATK = BinaryReader.b_ReadFloat(fileByte, ptr + 0x50);
                        conditionEntry.ConditionHP_Recover = BinaryReader.b_ReadFloat(fileByte, ptr + 0x54);
                        conditionEntry.ConditionPoison = BinaryReader.b_ReadFloat(fileByte, ptr + 0x58);
                        conditionEntry.ConditionChakra_recover = BinaryReader.b_ReadFloat(fileByte, ptr + 0x5C);
                        conditionEntry.ConditionChakra_shave = BinaryReader.b_ReadFloat(fileByte, ptr + 0x60);
                        conditionEntry.ConditionChakra_revival = BinaryReader.b_ReadFloat(fileByte, ptr + 0x64);
                        conditionEntry.ConditionChakra_unk = BinaryReader.b_ReadFloat(fileByte, ptr + 0x68);
                        conditionEntry.ConditionChakra_Drain = BinaryReader.b_ReadFloat(fileByte, ptr + 0x6C);
                        conditionEntry.ConditionChakra = BinaryReader.b_ReadFloat(fileByte, ptr + 0x70);
                        conditionEntry.ConditionChakra_Usage = BinaryReader.b_ReadFloat(fileByte, ptr + 0x74);
                        conditionEntry.ConditionSupport = BinaryReader.b_ReadFloat(fileByte, ptr + 0x78);
                        conditionEntry.ConditionTeam = BinaryReader.b_ReadFloat(fileByte, ptr + 0x7C);
                        conditionEntry.ConditionGuardBreak = BinaryReader.b_ReadFloat(fileByte, ptr + 0x80);
                        conditionEntry.ConditionDodge = BinaryReader.b_ReadFloat(fileByte, ptr + 0x84);
                        conditionEntry.ConditionProjectile = BinaryReader.b_ReadBool16(fileByte, ptr + 0x88);
                        conditionEntry.ConditionAutoDodge = BinaryReader.b_ReadBool16(fileByte, ptr + 0x8A);
                        conditionEntry.ConditionSeal = BinaryReader.b_ReadBool16(fileByte, ptr + 0x8C);
                        conditionEntry.ConditionSleep = BinaryReader.b_ReadBool16(fileByte, ptr + 0x8E);
                        conditionEntry.ConditionStun = BinaryReader.b_ReadBool16(fileByte, ptr + 0x90);
                        conditionEntry.ConditionFlashType = BinaryReader.b_ReadInt16(fileByte, ptr + 0x92);

                        float ColorR = BinaryReader.b_ReadFloat(fileByte, ptr + 0x94);
                        float ColorG = BinaryReader.b_ReadFloat(fileByte, ptr + 0x98);
                        float ColorB = BinaryReader.b_ReadFloat(fileByte, ptr + 0x9C);
                        conditionEntry.ConditionFlashColor = Color.FromArgb(255, (int)(ColorR), (int)(ColorG), (int)(ColorB));


                        conditionEntry.ConditionFlash_unk1 = BinaryReader.b_ReadFloat(fileByte, ptr + 0xA0);
                        conditionEntry.ConditionFlash_interval = BinaryReader.b_ReadFloat(fileByte, ptr + 0xA4);
                        conditionEntry.ConditionFlash_unk2 = BinaryReader.b_ReadFloat(fileByte, ptr + 0xA8);
                        conditionEntry.ConditionFlash_opacity = BinaryReader.b_ReadFloat(fileByte, ptr + 0xAC);
                        ConditionList.Add(conditionEntry);
                    }
                } else
                {
                    ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_1"]);
                    return;
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
                SelectedCondition.ConditionDuration = ConditionDuration_field;
                SelectedCondition.ConditionATK = ConditionATK_field;
                SelectedCondition.ConditionDEF = ConditionDEF_field;
                SelectedCondition.ConditionSPD = ConditionSPD_field;
                SelectedCondition.ConditionSPT_ATK = ConditionSPT_ATK_field;
                SelectedCondition.ConditionHP_Recover = ConditionHP_Recover_field;
                SelectedCondition.ConditionPoison = ConditionPoison_field;
                SelectedCondition.ConditionChakra_recover = ConditionChakra_recover_field;
                SelectedCondition.ConditionChakra_shave = ConditionChakra_shave_field;
                SelectedCondition.ConditionChakra_unk = ConditionChakra_unk_field;
                SelectedCondition.ConditionChakra_revival = ConditionChakra_revival_field;
                SelectedCondition.ConditionChakra_Drain = ConditionChakra_Drain_field;
                SelectedCondition.ConditionChakra = ConditionChakra_field;
                SelectedCondition.ConditionChakra_Usage = ConditionChakra_Usage_field;
                SelectedCondition.ConditionSupport = ConditionSupport_field;
                SelectedCondition.ConditionTeam = ConditionTeam_field;
                SelectedCondition.ConditionGuardBreak = ConditionGuardBreak_field;
                SelectedCondition.ConditionDodge = ConditionDodge_field;
                SelectedCondition.ConditionProjectile = ConditionProjectile_field;
                SelectedCondition.ConditionAutoDodge = ConditionAutoDodge_field;
                SelectedCondition.ConditionSeal = ConditionSeal_field;
                SelectedCondition.ConditionSleep = ConditionSleep_field;
                SelectedCondition.ConditionStun = ConditionStun_field;
                SelectedCondition.ConditionFlashType = ConditionFlashType_field;
                SelectedCondition.ConditionFlashColor = ConditionFlashColor_field;
                SelectedCondition.ConditionFlash_unk1 = ConditionFlash_unk1_field;
                SelectedCondition.ConditionFlash_interval = ConditionFlash_interval_field;
                SelectedCondition.ConditionFlash_unk2 = ConditionFlash_unk2_field;
                SelectedCondition.ConditionFlash_opacity = ConditionFlash_opacity_field;

                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_1"]);
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_2"]);
            }

        }
        public int SearchStringIndex(ObservableCollection<ConditionPrmModel> FunctionList, string member_name, int Selected)
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
            ConditionPrmModel conditionEntry = new ConditionPrmModel();
            if (SelectedCondition is not null)
            {
                conditionEntry = (ConditionPrmModel)SelectedCondition.Clone();
            } else
            {
                conditionEntry.ConditionName = "NEW_CONDITION";
                conditionEntry.ConditionDuration = -1;
                conditionEntry.ConditionATK = 0;
                conditionEntry.ConditionDEF = 0;
                conditionEntry.ConditionSPD = 0;
                conditionEntry.ConditionSPT_ATK = 0;
                conditionEntry.ConditionHP_Recover = 0;
                conditionEntry.ConditionPoison = 0;
                conditionEntry.ConditionChakra_recover = 0;
                conditionEntry.ConditionChakra_shave = 0;
                conditionEntry.ConditionChakra_revival = 0;
                conditionEntry.ConditionChakra_Drain = 0;
                conditionEntry.ConditionChakra_unk = 0;
                conditionEntry.ConditionChakra = 0;
                conditionEntry.ConditionChakra_Usage = 0;
                conditionEntry.ConditionSupport = 0;
                conditionEntry.ConditionTeam = 0;
                conditionEntry.ConditionGuardBreak = 0;
                conditionEntry.ConditionDodge = 0;
                conditionEntry.ConditionProjectile = false;
                conditionEntry.ConditionAutoDodge = false;
                conditionEntry.ConditionSeal = false;
                conditionEntry.ConditionSleep = false;
                conditionEntry.ConditionStun = false;
                conditionEntry.ConditionFlashType = 0;

                conditionEntry.ConditionFlashColor = Color.FromArgb(255, 0, 0, 0);


                conditionEntry.ConditionFlash_unk1 = 0;
                conditionEntry.ConditionFlash_interval = 0;
                conditionEntry.ConditionFlash_unk2 = 0;
                conditionEntry.ConditionFlash_opacity = 0;
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

            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x63, 0x40, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/NSW/param/player/Converter/bin/conditionprm.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "conditionprm");
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

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[28]
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0x79,0x76,0x00,0x00,0x08,0x98,0x00,0x00,0x00,0x01,0x00,0x63,0x79,0x76,0x00,0x00,0x08,0x94
                });






            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]);
            int startOfFile = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0xB0 * ConditionList.Count]);
            for (int c = 0; c < ConditionList.Count; c++)
            {
                int ptr = startOfFile + (c * 0xB0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, Encoding.ASCII.GetBytes(ConditionList[c].ConditionName ?? ""), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionDuration), ptr + 0x40);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionATK), ptr + 0x44);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionDEF), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionSPD), ptr + 0x4C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionSPT_ATK), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionHP_Recover), ptr + 0x54);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionPoison), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_recover), ptr + 0x5C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_shave), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_revival), ptr + 0x64);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_unk), ptr + 0x68);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_Drain), ptr + 0x6C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra), ptr + 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionChakra_Usage), ptr + 0x74);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionSupport), ptr + 0x78);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionTeam), ptr + 0x7C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionGuardBreak), ptr + 0x80);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionDodge), ptr + 0x84);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionProjectile ? (short)1 : (short)0), ptr + 0x88);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionAutoDodge ? (short)1 : (short)0), ptr + 0x8A);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionSeal ? (short)1 : (short)0), ptr + 0x8C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionSleep ? (short)1 : (short)0), ptr + 0x8E);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionStun ? (short)1 : (short)0), ptr + 0x90);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionFlashType), ptr + 0x92);

                float ColorR = ConditionList[c].ConditionFlashColor.R;
                float ColorG = ConditionList[c].ConditionFlashColor.G;
                float ColorB = ConditionList[c].ConditionFlashColor.B;

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((float)ColorR), ptr + 0x94);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((float)ColorG), ptr + 0x98);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((float)ColorB), ptr + 0x9C);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionFlash_unk1), ptr + 0xA0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionFlash_interval), ptr + 0xA4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionFlash_unk2), ptr + 0xA8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList[c].ConditionFlash_opacity), ptr + 0xAC);
            }

            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((ConditionList.Count * 0xB0) + 8), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((ConditionList.Count * 0xB0) + 4), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(ConditionList.Count), count_index);
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
