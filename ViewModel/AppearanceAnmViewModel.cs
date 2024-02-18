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

namespace NSC_ModManager.ViewModel
{
    public class AppearanceAnmViewModel : INotifyPropertyChanged {
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
        private string _chunkName;
        public string ChunkName_field {
            get { return _chunkName; }
            set {
                _chunkName = value;
                OnPropertyChanged("ChunkName_field");
            }
        }
        private bool _toggleEntry;
        public bool ToggleEntry_field {
            get { return _toggleEntry; }
            set {
                _toggleEntry = value;
                OnPropertyChanged("ToggleEntry_field");
            }
        }
        private bool _enableNormalState;
        public bool EnableNormalState_field {
            get { return _enableNormalState; }
            set {
                _enableNormalState = value;
                OnPropertyChanged("EnableNormalState_field");
            }
        }
        private int _timing;
        public int Timing_field {
            get { return _timing; }
            set {
                _timing = value;
                OnPropertyChanged("Timing_field");
            }
        }
        private bool _enableAwakeningState;
        public bool EnableAwakeningState_field {
            get { return _enableAwakeningState; }
            set {
                _enableAwakeningState = value;
                OnPropertyChanged("EnableAwakeningState_field");
            }
        }
        private bool _toggleReverseAfterAwakening;
        public bool ToggleReverseAfterAwakening_field {
            get { return _toggleReverseAfterAwakening; }
            set {
                _toggleReverseAfterAwakening = value;
                OnPropertyChanged("ToggleReverseAfterAwakening_field");
            }
        }
        private bool _toggleSpAtkCutNC;
        public bool ToggleSpAtkCutNC_field {
            get { return _toggleSpAtkCutNC; }
            set {
                _toggleSpAtkCutNC = value;
                OnPropertyChanged("ToggleSpAtkCutNC_field");
            }
        }
        private bool _toggleSpAtk;
        public bool ToggleSpAtk_field {
            get { return _toggleSpAtk; }
            set {
                _toggleSpAtk = value;
                OnPropertyChanged("ToggleSpAtk_field");
            }
        }
        private int _chunkType;
        public int ChunkType_field {
            get { return _chunkType; }
            set {
                _chunkType = value;
                OnPropertyChanged("ChunkType_field");
            }
        }
        private float _blendValue;
        public float BlendValue_field {
            get { return _blendValue; }
            set {
                _blendValue = value;
                OnPropertyChanged("BlendValue_field");
            }
        }
        private bool _toggleWin;
        public bool ToggleWin_field {
            get { return _toggleWin; }
            set {
                _toggleWin = value;
                OnPropertyChanged("ToggleWin_field");
            }
        }
        private bool _toggleArmorBreak;
        public bool ToggleArmorBreak_field {
            get { return _toggleArmorBreak; }
            set {
                _toggleArmorBreak = value;
                OnPropertyChanged("ToggleArmorBreak_field");
            }
        }
        private bool _enableSlot1;
        public bool EnableSlot1_field {
            get { return _enableSlot1; }
            set {
                _enableSlot1 = value;
                OnPropertyChanged("EnableSlot1_field");
            }
        }
        private bool _enableSlot2;
        public bool EnableSlot2_field {
            get { return _enableSlot2; }
            set {
                _enableSlot2 = value;
                OnPropertyChanged("EnableSlot2_field");
            }
        }
        private bool _enableSlot3;
        public bool EnableSlot3_field {
            get { return _enableSlot3; }
            set {
                _enableSlot3 = value;
                OnPropertyChanged("EnableSlot3_field");
            }
        }
        private bool _enableSlot4;
        public bool EnableSlot4_field {
            get { return _enableSlot4; }
            set {
                _enableSlot4 = value;
                OnPropertyChanged("EnableSlot4_field");
            }
        }
        private bool _enableSlot5;
        public bool EnableSlot5_field {
            get { return _enableSlot5; }
            set {
                _enableSlot5 = value;
                OnPropertyChanged("EnableSlot5_field");
            }
        }
        private bool _enableSlot6;
        public bool EnableSlot6_field {
            get { return _enableSlot6; }
            set {
                _enableSlot6 = value;
                OnPropertyChanged("EnableSlot6_field");
            }
        }
        private bool _enableSlot7;
        public bool EnableSlot7_field {
            get { return _enableSlot7; }
            set {
                _enableSlot7 = value;
                OnPropertyChanged("EnableSlot7_field");
            }
        }
        private bool _enableSlot8;
        public bool EnableSlot8_field {
            get { return _enableSlot8; }
            set {
                _enableSlot8 = value;
                OnPropertyChanged("EnableSlot8_field");
            }
        }
        private bool _enableSlot9;
        public bool EnableSlot9_field {
            get { return _enableSlot9; }
            set {
                _enableSlot9 = value;
                OnPropertyChanged("EnableSlot9_field");
            }
        }
        private bool _enableSlot10;
        public bool EnableSlot10_field {
            get { return _enableSlot10; }
            set {
                _enableSlot10 = value;
                OnPropertyChanged("EnableSlot10_field");
            }
        }
        private bool _enableSlot11;
        public bool EnableSlot11_field {
            get { return _enableSlot11; }
            set {
                _enableSlot11 = value;
                OnPropertyChanged("EnableSlot11_field");
            }
        }
        private bool _enableSlot12;
        public bool EnableSlot12_field {
            get { return _enableSlot12; }
            set {
                _enableSlot12 = value;
                OnPropertyChanged("EnableSlot12_field");
            }
        }
        private bool _enableSlot13;
        public bool EnableSlot13_field {
            get { return _enableSlot13; }
            set {
                _enableSlot13 = value;
                OnPropertyChanged("EnableSlot13_field");
            }
        }
        private bool _enableSlot14;
        public bool EnableSlot14_field {
            get { return _enableSlot14; }
            set {
                _enableSlot14 = value;
                OnPropertyChanged("EnableSlot14_field");
            }
        }
        private bool _enableSlot15;
        public bool EnableSlot15_field {
            get { return _enableSlot15; }
            set {
                _enableSlot15 = value;
                OnPropertyChanged("EnableSlot15_field");
            }
        }
        private bool _enableSlot16;
        public bool EnableSlot16_field {
            get { return _enableSlot16; }
            set {
                _enableSlot16 = value;
                OnPropertyChanged("EnableSlot16_field");
            }
        }
        private bool _enableSlot17;
        public bool EnableSlot17_field {
            get { return _enableSlot17; }
            set {
                _enableSlot17 = value;
                OnPropertyChanged("EnableSlot17_field");
            }
        }
        private bool _enableSlot18;
        public bool EnableSlot18_field {
            get { return _enableSlot18; }
            set {
                _enableSlot18 = value;
                OnPropertyChanged("EnableSlot18_field");
            }
        }
        private bool _enableSlot19;
        public bool EnableSlot19_field {
            get { return _enableSlot19; }
            set {
                _enableSlot19 = value;
                OnPropertyChanged("EnableSlot19_field");
            }
        }
        private bool _enableSlot20;
        public bool EnableSlot20_field {
            get { return _enableSlot20; }
            set {
                _enableSlot20 = value;
                OnPropertyChanged("EnableSlot20_field");
            }
        }
        public ObservableCollection<AppearanceAnmModel> AppearanceAnmList { get; set; }
        private AppearanceAnmModel _selectedAppearanceAnm;
        public AppearanceAnmModel SelectedAppearanceAnm {
            get { return _selectedAppearanceAnm; }
            set {
                _selectedAppearanceAnm = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    ChunkName_field = value.ChunkName;
                    ToggleEntry_field = value.ToggleEntry;
                    EnableNormalState_field = value.EnableNormalState;
                    Timing_field = value.Timing;
                    EnableAwakeningState_field = value.EnableAwakeningState;
                    ToggleReverseAfterAwakening_field = value.ToggleReverseAfterAwakening;
                    ToggleSpAtkCutNC_field = value.ToggleSpAtkCutNC;
                    ToggleSpAtk_field = value.ToggleSpAtk;
                    ChunkType_field = value.ChunkType;
                    BlendValue_field = value.BlendValue;
                    ToggleWin_field = value.ToggleWin;
                    ToggleArmorBreak_field = value.ToggleArmorBreak;
                    EnableSlot1_field = value.EnableSlot1;
                    EnableSlot2_field = value.EnableSlot2;
                    EnableSlot3_field = value.EnableSlot3;
                    EnableSlot4_field = value.EnableSlot4;
                    EnableSlot5_field = value.EnableSlot5;
                    EnableSlot6_field = value.EnableSlot6;
                    EnableSlot7_field = value.EnableSlot7;
                    EnableSlot8_field = value.EnableSlot8;
                    EnableSlot9_field = value.EnableSlot9;
                    EnableSlot10_field = value.EnableSlot10;
                    EnableSlot11_field = value.EnableSlot11;
                    EnableSlot12_field = value.EnableSlot12;
                    EnableSlot13_field = value.EnableSlot13;
                    EnableSlot14_field = value.EnableSlot14;
                    EnableSlot15_field = value.EnableSlot15;
                    EnableSlot16_field = value.EnableSlot16;
                    EnableSlot17_field = value.EnableSlot17;
                    EnableSlot18_field = value.EnableSlot18;
                    EnableSlot19_field = value.EnableSlot19;
                    EnableSlot20_field = value.EnableSlot20;
                }
                OnPropertyChanged("SelectedAppearanceAnm");
            }
        }
        private int _selectedAppearanceAnmIndex;
        public int SelectedAppearanceAnmIndex {
            get { return _selectedAppearanceAnmIndex; }
            set {
                _selectedAppearanceAnmIndex = value;
                OnPropertyChanged("SelectedAppearanceAnmIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public AppearanceAnmViewModel() {

            LoadingStatePlay = Visibility.Hidden;
            AppearanceAnmList = new ObservableCollection<AppearanceAnmModel>();
            filePath = "";
        }

        public void Clear() {
            AppearanceAnmList.Clear();
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
                if (BinName.Contains("appearanceAnm")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0xA0);
                        AppearanceAnmModel AppearanceAnmEntry = new AppearanceAnmModel();
                        AppearanceAnmEntry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        AppearanceAnmEntry.ChunkName = BinaryReader.b_ReadString(fileByte, ptr + 0x08 + BinaryReader.b_ReadInt(fileByte, ptr + 0x08));
                        AppearanceAnmEntry.ToggleEntry = BinaryReader.b_ReadInt(fileByte, ptr + 0x10) == 1;
                        AppearanceAnmEntry.EnableNormalState = BinaryReader.b_ReadInt(fileByte, ptr + 0x14) == 1;
                        AppearanceAnmEntry.Timing = BinaryReader.b_ReadInt(fileByte, ptr + 0x18);
                        AppearanceAnmEntry.EnableAwakeningState = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C) == 1;
                        AppearanceAnmEntry.ToggleReverseAfterAwakening = BinaryReader.b_ReadInt(fileByte, ptr + 0x20) == 1;
                        AppearanceAnmEntry.ToggleSpAtkCutNC = BinaryReader.b_ReadInt(fileByte, ptr + 0x24) == 1;
                        AppearanceAnmEntry.ToggleSpAtk = BinaryReader.b_ReadInt(fileByte, ptr + 0x28) == 1;
                        int ChunkType1 = BinaryReader.b_ReadInt(fileByte, ptr + 0x34);
                        float BlendValue1 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x38);
                        AppearanceAnmEntry.ToggleWin = BinaryReader.b_ReadInt(fileByte, ptr + 0x3C) == 1;
                        int ChunkType2 = BinaryReader.b_ReadInt(fileByte, ptr + 0x40);
                        float BlendValue2 = BinaryReader.b_ReadFloat(fileByte, ptr + 0x44);
                        if (ChunkType1 != 1 && ChunkType2 != 1) {
                            AppearanceAnmEntry.ChunkType = 0;
                            AppearanceAnmEntry.BlendValue = 0;
                        }
                        else if (ChunkType1 == 1 && ChunkType2 != 1) {
                            AppearanceAnmEntry.ChunkType = 1;
                            AppearanceAnmEntry.BlendValue = BlendValue1;
                        }
                        else if (ChunkType1 != 1 && ChunkType2 == 1) {
                            AppearanceAnmEntry.ChunkType = 2;
                            AppearanceAnmEntry.BlendValue = BlendValue2;

                        }
                        AppearanceAnmEntry.ToggleArmorBreak = BinaryReader.b_ReadInt(fileByte, ptr + 0x4C) == 1;
                        AppearanceAnmEntry.EnableSlot1 = BinaryReader.b_ReadInt(fileByte, ptr + 0x50) == 1;
                        AppearanceAnmEntry.EnableSlot2 = BinaryReader.b_ReadInt(fileByte, ptr + 0x54) == 1;
                        AppearanceAnmEntry.EnableSlot3 = BinaryReader.b_ReadInt(fileByte, ptr + 0x58) == 1;
                        AppearanceAnmEntry.EnableSlot4 = BinaryReader.b_ReadInt(fileByte, ptr + 0x5C) == 1;
                        AppearanceAnmEntry.EnableSlot5 = BinaryReader.b_ReadInt(fileByte, ptr + 0x60) == 1;
                        AppearanceAnmEntry.EnableSlot6 = BinaryReader.b_ReadInt(fileByte, ptr + 0x64) == 1;
                        AppearanceAnmEntry.EnableSlot7 = BinaryReader.b_ReadInt(fileByte, ptr + 0x68) == 1;
                        AppearanceAnmEntry.EnableSlot8 = BinaryReader.b_ReadInt(fileByte, ptr + 0x6C) == 1;
                        AppearanceAnmEntry.EnableSlot9 = BinaryReader.b_ReadInt(fileByte, ptr + 0x70) == 1;
                        AppearanceAnmEntry.EnableSlot10 = BinaryReader.b_ReadInt(fileByte, ptr + 0x74) == 1;
                        AppearanceAnmEntry.EnableSlot11 = BinaryReader.b_ReadInt(fileByte, ptr + 0x78) == 1;
                        AppearanceAnmEntry.EnableSlot12 = BinaryReader.b_ReadInt(fileByte, ptr + 0x7C) == 1;
                        AppearanceAnmEntry.EnableSlot13 = BinaryReader.b_ReadInt(fileByte, ptr + 0x80) == 1;
                        AppearanceAnmEntry.EnableSlot14 = BinaryReader.b_ReadInt(fileByte, ptr + 0x84) == 1;
                        AppearanceAnmEntry.EnableSlot15 = BinaryReader.b_ReadInt(fileByte, ptr + 0x88) == 1;
                        AppearanceAnmEntry.EnableSlot16 = BinaryReader.b_ReadInt(fileByte, ptr + 0x8C) == 1;
                        AppearanceAnmEntry.EnableSlot17 = BinaryReader.b_ReadInt(fileByte, ptr + 0x90) == 1;
                        AppearanceAnmEntry.EnableSlot18 = BinaryReader.b_ReadInt(fileByte, ptr + 0x94) == 1;
                        AppearanceAnmEntry.EnableSlot19 = BinaryReader.b_ReadInt(fileByte, ptr + 0x98) == 1;
                        AppearanceAnmEntry.EnableSlot20 = BinaryReader.b_ReadInt(fileByte, ptr + 0x9C) == 1;
                        AppearanceAnmList.Add(AppearanceAnmEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedAppearanceAnm is not null) {
                AppearanceAnmList.Remove(SelectedAppearanceAnm);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void EnableSlots() {
            if (SelectedAppearanceAnm is not null) {
                SelectedAppearanceAnm.EnableSlot1 = true;
                SelectedAppearanceAnm.EnableSlot2 = true;
                SelectedAppearanceAnm.EnableSlot3 = true;
                SelectedAppearanceAnm.EnableSlot4 = true;
                SelectedAppearanceAnm.EnableSlot5 = true;
                SelectedAppearanceAnm.EnableSlot6 = true;
                SelectedAppearanceAnm.EnableSlot7 = true;
                SelectedAppearanceAnm.EnableSlot8 = true;
                SelectedAppearanceAnm.EnableSlot9 = true;
                SelectedAppearanceAnm.EnableSlot10 = true;
                SelectedAppearanceAnm.EnableSlot11 = true;
                SelectedAppearanceAnm.EnableSlot12 = true;
                SelectedAppearanceAnm.EnableSlot13 = true;
                SelectedAppearanceAnm.EnableSlot14 = true;
                SelectedAppearanceAnm.EnableSlot15 = true;
                SelectedAppearanceAnm.EnableSlot16 = true;
                SelectedAppearanceAnm.EnableSlot17 = true;
                SelectedAppearanceAnm.EnableSlot18 = true;
                SelectedAppearanceAnm.EnableSlot19 = true;
                SelectedAppearanceAnm.EnableSlot20 = true;
                EnableSlot1_field = true;
                EnableSlot2_field = true;
                EnableSlot3_field = true;
                EnableSlot4_field = true;
                EnableSlot5_field = true;
                EnableSlot6_field = true;
                EnableSlot7_field = true;
                EnableSlot8_field = true;
                EnableSlot9_field = true;
                EnableSlot10_field = true;
                EnableSlot11_field = true;
                EnableSlot12_field = true;
                EnableSlot13_field = true;
                EnableSlot14_field = true;
                EnableSlot15_field = true;
                EnableSlot16_field = true;
                EnableSlot17_field = true;
                EnableSlot18_field = true;
                EnableSlot19_field = true;
                EnableSlot20_field = true;
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void DisableSlots() {
            if (SelectedAppearanceAnm is not null) {
                SelectedAppearanceAnm.EnableSlot1 = false;
                SelectedAppearanceAnm.EnableSlot2 = false;
                SelectedAppearanceAnm.EnableSlot3 = false;
                SelectedAppearanceAnm.EnableSlot4 = false;
                SelectedAppearanceAnm.EnableSlot5 = false;
                SelectedAppearanceAnm.EnableSlot6 = false;
                SelectedAppearanceAnm.EnableSlot7 = false;
                SelectedAppearanceAnm.EnableSlot8 = false;
                SelectedAppearanceAnm.EnableSlot9 = false;
                SelectedAppearanceAnm.EnableSlot10 = false;
                SelectedAppearanceAnm.EnableSlot11 = false;
                SelectedAppearanceAnm.EnableSlot12 = false;
                SelectedAppearanceAnm.EnableSlot13 = false;
                SelectedAppearanceAnm.EnableSlot14 = false;
                SelectedAppearanceAnm.EnableSlot15 = false;
                SelectedAppearanceAnm.EnableSlot16 = false;
                SelectedAppearanceAnm.EnableSlot17 = false;
                SelectedAppearanceAnm.EnableSlot18 = false;
                SelectedAppearanceAnm.EnableSlot19 = false;
                SelectedAppearanceAnm.EnableSlot20 = false;
                EnableSlot1_field = false;
                EnableSlot2_field = false;
                EnableSlot3_field = false;
                EnableSlot4_field = false;
                EnableSlot5_field = false;
                EnableSlot6_field = false;
                EnableSlot7_field = false;
                EnableSlot8_field = false;
                EnableSlot9_field = false;
                EnableSlot10_field = false;
                EnableSlot11_field = false;
                EnableSlot12_field = false;
                EnableSlot13_field = false;
                EnableSlot14_field = false;
                EnableSlot15_field = false;
                EnableSlot16_field = false;
                EnableSlot17_field = false;
                EnableSlot18_field = false;
                EnableSlot19_field = false;
                EnableSlot20_field = false;
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedAppearanceAnm is not null) {
                SelectedAppearanceAnm.CharacodeID = CharacodeID_field;
                SelectedAppearanceAnm.ChunkName = ChunkName_field;
                SelectedAppearanceAnm.ToggleEntry = ToggleEntry_field;
                SelectedAppearanceAnm.EnableNormalState = EnableNormalState_field;
                SelectedAppearanceAnm.Timing = Timing_field;
                SelectedAppearanceAnm.EnableAwakeningState = EnableAwakeningState_field;
                SelectedAppearanceAnm.ToggleReverseAfterAwakening = ToggleReverseAfterAwakening_field;
                SelectedAppearanceAnm.ToggleSpAtkCutNC = ToggleSpAtkCutNC_field;
                SelectedAppearanceAnm.ToggleSpAtk = ToggleSpAtk_field;
                SelectedAppearanceAnm.ChunkType = ChunkType_field;
                SelectedAppearanceAnm.BlendValue = BlendValue_field;
                SelectedAppearanceAnm.ToggleWin = ToggleWin_field;
                SelectedAppearanceAnm.ToggleArmorBreak = ToggleArmorBreak_field;
                SelectedAppearanceAnm.EnableSlot1 = EnableSlot1_field;
                SelectedAppearanceAnm.EnableSlot2 = EnableSlot2_field;
                SelectedAppearanceAnm.EnableSlot3 = EnableSlot3_field;
                SelectedAppearanceAnm.EnableSlot4 = EnableSlot4_field;
                SelectedAppearanceAnm.EnableSlot5 = EnableSlot5_field;
                SelectedAppearanceAnm.EnableSlot6 = EnableSlot6_field;
                SelectedAppearanceAnm.EnableSlot7 = EnableSlot7_field;
                SelectedAppearanceAnm.EnableSlot8 = EnableSlot8_field;
                SelectedAppearanceAnm.EnableSlot9 = EnableSlot9_field;
                SelectedAppearanceAnm.EnableSlot10 = EnableSlot10_field;
                SelectedAppearanceAnm.EnableSlot11 = EnableSlot11_field;
                SelectedAppearanceAnm.EnableSlot12 = EnableSlot12_field;
                SelectedAppearanceAnm.EnableSlot13 = EnableSlot13_field;
                SelectedAppearanceAnm.EnableSlot14 = EnableSlot14_field;
                SelectedAppearanceAnm.EnableSlot15 = EnableSlot15_field;
                SelectedAppearanceAnm.EnableSlot16 = EnableSlot16_field;
                SelectedAppearanceAnm.EnableSlot17 = EnableSlot17_field;
                SelectedAppearanceAnm.EnableSlot18 = EnableSlot18_field;
                SelectedAppearanceAnm.EnableSlot19 = EnableSlot19_field;
                SelectedAppearanceAnm.EnableSlot20 = EnableSlot20_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<AppearanceAnmModel> FunctionList, int member_index, int Selected) {
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
                if (SearchByteIndex(AppearanceAnmList, SearchIndex_field, SelectedAppearanceAnmIndex) != -1) {
                    SelectedAppearanceAnmIndex = SearchByteIndex(AppearanceAnmList, SearchIndex_field, SelectedAppearanceAnmIndex);
                    CollectionViewSource.GetDefaultView(AppearanceAnmList).MoveCurrentTo(SelectedAppearanceAnm);
                } else {
                    if (SearchByteIndex(AppearanceAnmList, SearchIndex_field, 0) != -1) {
                        SelectedAppearanceAnmIndex = SearchByteIndex(AppearanceAnmList, SearchIndex_field, 0);
                        CollectionViewSource.GetDefaultView(AppearanceAnmList).MoveCurrentTo(SelectedAppearanceAnm);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            AppearanceAnmModel AppearanceAnmEntry = new AppearanceAnmModel();
            if (SelectedAppearanceAnm is not null) {
                AppearanceAnmEntry = (AppearanceAnmModel)SelectedAppearanceAnm.Clone();
            } else {
                AppearanceAnmEntry.CharacodeID = 0;
                AppearanceAnmEntry.ChunkName = "";
                AppearanceAnmEntry.ToggleEntry = true;
                AppearanceAnmEntry.EnableNormalState = true;
                AppearanceAnmEntry.Timing = -1;
                AppearanceAnmEntry.EnableAwakeningState = true;
                AppearanceAnmEntry.ToggleReverseAfterAwakening = false;
                AppearanceAnmEntry.ToggleSpAtkCutNC = true;
                AppearanceAnmEntry.ToggleSpAtk = true;
                AppearanceAnmEntry.ChunkType = 0;
                AppearanceAnmEntry.BlendValue = 0;
                AppearanceAnmEntry.ToggleWin = true;
                AppearanceAnmEntry.ToggleArmorBreak = true;
                AppearanceAnmEntry.EnableSlot1 = true;
                AppearanceAnmEntry.EnableSlot2 = true;
                AppearanceAnmEntry.EnableSlot3 = true;
                AppearanceAnmEntry.EnableSlot4 = true;
                AppearanceAnmEntry.EnableSlot5 = true;
                AppearanceAnmEntry.EnableSlot6 = true;
                AppearanceAnmEntry.EnableSlot7 = true;
                AppearanceAnmEntry.EnableSlot8 = true;
                AppearanceAnmEntry.EnableSlot9 = true;
                AppearanceAnmEntry.EnableSlot10 = true;
                AppearanceAnmEntry.EnableSlot11 = true;
                AppearanceAnmEntry.EnableSlot12 = true;
                AppearanceAnmEntry.EnableSlot13 = true;
                AppearanceAnmEntry.EnableSlot14 = true;
                AppearanceAnmEntry.EnableSlot15 = true;
                AppearanceAnmEntry.EnableSlot16 = true;
                AppearanceAnmEntry.EnableSlot17 = true;
                AppearanceAnmEntry.EnableSlot18 = true;
                AppearanceAnmEntry.EnableSlot19 = true;
                AppearanceAnmEntry.EnableSlot20 = true;
            }
            AppearanceAnmList.Add(AppearanceAnmEntry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/appearanceAnm.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "appearanceAnm");
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




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[AppearanceAnmList.Count * 0xA0]);

            int addSize = 0;

            List<int> chunkname_pointer = new List<int>();
            for (int x = 0; x < AppearanceAnmList.Count; x++) {
                int ptr = startPtr + (x * 0xA0);
                chunkname_pointer.Add(fileBytes36.Length);
                if (AppearanceAnmList[x].ChunkName != "" && AppearanceAnmList[x].ChunkName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(AppearanceAnmList[x].ChunkName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = chunkname_pointer[x] - startPtr - x * 0xA0 - 0x08;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x08);
                    addSize += AppearanceAnmList[x].ChunkName.Length + 1;
                }

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleEntry), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableNormalState), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].Timing), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableAwakeningState), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleReverseAfterAwakening), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleSpAtkCutNC), ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleSpAtk), ptr + 0x28);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(-1), ptr + 0x2C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(-1), ptr + 0x30);

                switch (AppearanceAnmList[x].ChunkType) {
                    case 1:
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(true), ptr + 0x34);
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].BlendValue), ptr + 0x38);
                        break;
                    case 2:
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(true), ptr + 0x40);
                        fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].BlendValue), ptr + 0x44);
                        break;

                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleWin), ptr + 0x3C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].ToggleArmorBreak), ptr + 0x4C);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot1), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot2), ptr + 0x54);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot3), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot4), ptr + 0x5C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot5), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot6), ptr + 0x64);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot7), ptr + 0x68);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot8), ptr + 0x6C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot9), ptr + 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot10), ptr + 0x74);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot11), ptr + 0x78);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot12), ptr + 0x7C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot13), ptr + 0x80);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot14), ptr + 0x84);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot15), ptr + 0x88);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot16), ptr + 0x8C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot17), ptr + 0x90);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot18), ptr + 0x94);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot19), ptr + 0x98);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList[x].EnableSlot20), ptr + 0x9C);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AppearanceAnmList.Count * 0xA0) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((AppearanceAnmList.Count * 0xA0) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(AppearanceAnmList.Count), count_index);
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
        private RelayCommand _enableSlotsCommand;
        public RelayCommand EnableSlotsCommand {
            get {
                return _enableSlotsCommand ??
                  (_enableSlotsCommand = new RelayCommand(obj => {
                      EnableSlotsAsync();
                  }));
            }
        }
        private RelayCommand _disableSlotsCommand;
        public RelayCommand DisableSlotsCommand {
            get {
                return _disableSlotsCommand ??
                  (_disableSlotsCommand = new RelayCommand(obj => {
                      DisableSlotsAsync();
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
        public async void EnableSlotsAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => EnableSlots()));

        }
        public async void DisableSlotsAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => DisableSlots()));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
