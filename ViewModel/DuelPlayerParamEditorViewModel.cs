using DynamicData;
using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NSC_ModManager.ViewModel {
    public class DuelPlayerParamEditorViewModel : INotifyPropertyChanged {
        private string _searchTextBox_field;
        public string SearchTextBox_field {
            get { return _searchTextBox_field; }
            set {
                _searchTextBox_field = value;
                OnPropertyChanged("SearchTextBox_field");
            }
        }
        private string _characode_field;
        public string Characode_field {
            get { return _characode_field; }
            set {
                _characode_field = value;
                OnPropertyChanged("Characode_field");
            }
        }

        private string _motionCode_field;
        public string MotionCode_field {
            get { return _motionCode_field; }
            set {
                _motionCode_field = value;
                OnPropertyChanged("MotionCode_field");
            }
        }
        private ObservableCollection<CostumeModel> _baseCostumesList;
        public ObservableCollection<CostumeModel> BaseCostumesList {
            get { return _baseCostumesList; }
            set {
                _baseCostumesList = value;
                OnPropertyChanged("BaseCostumesList");
            }
        }
        private CostumeModel _selectedBaseCostume;
        public CostumeModel SelectedBaseCostume {
            get { return _selectedBaseCostume; }
            set {
                _selectedBaseCostume = value;
                if (value is not null && value.CostumeName is not null) {
                    BaseCostume_field = value.CostumeName;
                }
                OnPropertyChanged("SelectedBaseCostume");
            }
        }
        private int _selectedBaseCostumeIndex;
        public int SelectedBaseCostumeIndex {
            get { return _selectedBaseCostumeIndex; }
            set {
                _selectedBaseCostumeIndex = value;
                OnPropertyChanged("SelectedBaseCostumeIndex");
            }
        }

        private ObservableCollection<CostumeModel> _awakeCostumesList;
        public ObservableCollection<CostumeModel> AwakeCostumesList {
            get { return _awakeCostumesList; }
            set {
                _awakeCostumesList = value;
                OnPropertyChanged("AwakeCostumesList");
            }
        }
        private CostumeModel _selectedAwakeCostume;
        public CostumeModel SelectedAwakeCostume {
            get { return _selectedAwakeCostume; }
            set {
                _selectedAwakeCostume = value;
                if (value is not null && value.CostumeName is not null) {
                    AwakeCostume_field = value.CostumeName;
                }
                OnPropertyChanged("SelectedAwakeCostume");
            }
        }

        private string _baseCostume_field;
        public string BaseCostume_field {
            get { return _baseCostume_field; }
            set {
                _baseCostume_field = value;
                OnPropertyChanged("BaseCostume_field");
            }
        }
        private string _awakeCostume_field;
        public string AwakeCostume_field {
            get { return _awakeCostume_field; }
            set {
                _awakeCostume_field = value;
                OnPropertyChanged("AwakeCostume_field");
            }
        }
        private int _selectedAwakeCostumeIndex;
        public int SelectedAwakeCostumeIndex {
            get { return _selectedAwakeCostumeIndex; }
            set {
                _selectedAwakeCostumeIndex = value;
                OnPropertyChanged("SelectedAwakeCostumeIndex");
            }
        }

        private string _partner_field;
        public string Partner_field {
            get { return _partner_field; }
            set {
                _partner_field = value;
                OnPropertyChanged("Partner_field");
            }
        }

        private byte _chakraDashCondition_field;
        public byte ChakraDashCondition_field {
            get { return _chakraDashCondition_field; }
            set {
                _chakraDashCondition_field = value;
                OnPropertyChanged("ChakraDashCondition_field");
            }
        }
        private byte _awakeningCondition1_field;
        public byte AwakeningCondition1_field {
            get { return _awakeningCondition1_field; }
            set {
                _awakeningCondition1_field = value;
                OnPropertyChanged("AwakeningCondition1_field");
            }
        }
        private byte _awakeningCondition2_field;
        public byte AwakeningCondition2_field {
            get { return _awakeningCondition2_field; }
            set {
                _awakeningCondition2_field = value;
                OnPropertyChanged("AwakeningCondition2_field");
            }
        }
        private byte _awakeningJutsuController_field;
        public byte AwakeningJutsuController_field {
            get { return _awakeningJutsuController_field; }
            set {
                _awakeningJutsuController_field = value;
                OnPropertyChanged("AwakeningJutsuController_field");
            }
        }
        private uint _awaBodyPriority_field;
        public uint AwaBodyPriority_field {
            get { return _awaBodyPriority_field; }
            set {
                _awaBodyPriority_field = value;
                OnPropertyChanged("AwaBodyPriority_field");
            }
        }
        private int _defaultAwaSkillIndex_field;
        public int DefaultAwaSkillIndex_field {
            get { return _defaultAwaSkillIndex_field; }
            set {
                _defaultAwaSkillIndex_field = value;
                OnPropertyChanged("DefaultAwaSkillIndex_field");
            }
        }

        private string _support1_field;
        public string Support1_field {
            get { return _support1_field; }
            set {
                _support1_field = value;
                OnPropertyChanged("Support1_field");
            }
        }
        private string _support2_field;
        public string Support2_field {
            get { return _support2_field; }
            set {
                _support2_field = value;
                OnPropertyChanged("Support2_field");
            }
        }
        private uint _cameraDistance_field;
        public uint CameraDistance_field {
            get { return _cameraDistance_field; }
            set {
                _cameraDistance_field = value;
                OnPropertyChanged("CameraDistance_field");
            }
        }
        private uint _unk1_field;
        public uint Unk1_field {
            get { return _unk1_field; }
            set {
                _unk1_field = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        private uint _victoryCameraAngle_field;
        public uint VictoryCameraAngle_field {
            get { return _victoryCameraAngle_field; }
            set {
                _victoryCameraAngle_field = value;
                OnPropertyChanged("VictoryCameraAngle_field");
            }
        }
        private uint _unk2_field;
        public uint Unk2_field {
            get { return _unk2_field; }
            set {
                _unk2_field = value;
                OnPropertyChanged("Unk2_field");
            }
        }
        private uint _unk3_field;
        public uint Unk3_field {
            get { return _unk3_field; }
            set {
                _unk3_field = value;
                OnPropertyChanged("Unk3_field");
            }
        }
        private uint _unk4_field;
        public uint Unk4_field {
            get { return _unk4_field; }
            set {
                _unk4_field = value;
                OnPropertyChanged("Unk4_field");
            }
        }
        private float _baseMovementSpeed_field;
        public float BaseMovementSpeed_field {
            get { return _baseMovementSpeed_field; }
            set {
                _baseMovementSpeed_field = value;
                OnPropertyChanged("BaseMovementSpeed_field");
            }
        }
        private float _baseChakraDashSpeed_field;
        public float BaseChakraDashSpeed_field {
            get { return _baseChakraDashSpeed_field; }
            set {
                _baseChakraDashSpeed_field = value;
                OnPropertyChanged("BaseChakraDashSpeed_field");
            }
        }
        private float _guardPrssure_field;
        public float GuardPressure_field {
            get { return _guardPrssure_field; }
            set {
                _guardPrssure_field = value;
                OnPropertyChanged("GuardPressure_field");
            }
        }
        private float _airDashSpeed_field;
        public float AirDashSpeed_field {
            get { return _airDashSpeed_field; }
            set {
                _airDashSpeed_field = value;
                OnPropertyChanged("AirDashSpeed_field");
            }
        }
        private float _attack_field;
        public float Attack_field {
            get { return _attack_field; }
            set {
                _attack_field = value;
                OnPropertyChanged("Attack_field");
            }
        }
        private float _defense_field;
        public float Defense_field {
            get { return _defense_field; }
            set {
                _defense_field = value;
                OnPropertyChanged("Defense_field");
            }
        }
        private float _assistDamage_field;
        public float AssistDamage_field {
            get { return _assistDamage_field; }
            set {
                _assistDamage_field = value;
                OnPropertyChanged("AssistDamage_field");
            }
        }
        private float _itemBuffDuration_field;
        public float ItemBuffDuration_field {
            get { return _itemBuffDuration_field; }
            set {
                _itemBuffDuration_field = value;
                OnPropertyChanged("ItemBuffDuration_field");
            }
        }
        private float _chakraChargeSpeed_field;
        public float ChakraChargeSpeed_field {
            get { return _chakraChargeSpeed_field; }
            set {
                _chakraChargeSpeed_field = value;
                OnPropertyChanged("ChakraChargeSpeed_field");
            }
        }
        private string _awakeningCondition_field;
        public string AwakeningCondition_field {
            get { return _awakeningCondition_field; }
            set {
                _awakeningCondition_field = value;
                OnPropertyChanged("AwakeningCondition_field");
            }
        }
        private float _awakeHPRequirement_field;
        public float AwakeHPRequirement_field {
            get { return _awakeHPRequirement_field; }
            set {
                _awakeHPRequirement_field = value;
                OnPropertyChanged("AwakeHPRequirement_field");
            }
        }
        private uint _baseNinjaDashSpeed_field;
        public uint BaseNinjaDashSpeed_field {
            get { return _baseNinjaDashSpeed_field; }
            set {
                _baseNinjaDashSpeed_field = value;
                OnPropertyChanged("BaseNinjaDashSpeed_field");
            }
        }
        private uint _baseAirDashDuration_field;
        public uint BaseAirDashDuration_field {
            get { return _baseAirDashDuration_field; }
            set {
                _baseAirDashDuration_field = value;
                OnPropertyChanged("BaseAirDashDuration_field");
            }
        }
        private uint _baseGroundedChakraDashDuration_field;
        public uint BaseGroundedChakraDashDuration_field {
            get { return _baseGroundedChakraDashDuration_field; }
            set {
                _baseGroundedChakraDashDuration_field = value;
                OnPropertyChanged("BaseGroundedChakraDashDuration_field");
            }
        }
        private string _items1_field;
        public string Items1_field {
            get { return _items1_field; }
            set {
                _items1_field = value;
                OnPropertyChanged("Items1_field");
            }
        }
        private string _items2_field;
        public string Items2_field {
            get { return _items2_field; }
            set {
                _items2_field = value;
                OnPropertyChanged("Items2_field");
            }
        }
        private string _items3_field;
        public string Items3_field {
            get { return _items3_field; }
            set {
                _items3_field = value;
                OnPropertyChanged("Items3_field");
            }
        }
        private string _items4_field;
        public string Items4_field {
            get { return _items4_field; }
            set {
                _items4_field = value;
                OnPropertyChanged("Items4_field");
            }
        }
        private int _itemsDuration1_field;
        public int ItemsDuration1_field {
            get { return _itemsDuration1_field; }
            set {
                _itemsDuration1_field = value;
                OnPropertyChanged("ItemsDuration1_field");
            }
        }
        private int _itemsDuration2_field;
        public int ItemsDuration2_field {
            get { return _itemsDuration2_field; }
            set {
                _itemsDuration2_field = value;
                OnPropertyChanged("ItemsDuration2_field");
            }
        }
        private int _itemsDuration3_field;
        public int ItemsDuration3_field {
            get { return _itemsDuration3_field; }
            set {
                _itemsDuration3_field = value;
                OnPropertyChanged("ItemsDuration3_field");
            }
        }
        private int _itemsDuration4_field;
        public int ItemsDuration4_field {
            get { return _itemsDuration4_field; }
            set {
                _itemsDuration4_field = value;
                OnPropertyChanged("ItemsDuration4_field");
            }
        }
        private float _awakeMovementSpeed_field;
        public float AwakeMovementSpeed_field {
            get { return _awakeMovementSpeed_field; }
            set {
                _awakeMovementSpeed_field = value;
                OnPropertyChanged("AwakeMovementSpeed_field");
            }
        }
        private float _awakeChakraDashSpeed_field;
        public float AwakeChakraDashSpeed_field {
            get { return _awakeChakraDashSpeed_field; }
            set {
                _awakeChakraDashSpeed_field = value;
                OnPropertyChanged("AwakeChakraDashSpeed_field");
            }
        }
        private uint _awakeNinjaDashSpeed_field;
        public uint AwakeNinjaDashSpeed_field {
            get { return _awakeNinjaDashSpeed_field; }
            set {
                _awakeNinjaDashSpeed_field = value;
                OnPropertyChanged("AwakeNinjaDashSpeed_field");
            }
        }
        private uint _awakeAirDashDuration_field;
        public uint AwakeAirDashDuration_field {
            get { return _awakeAirDashDuration_field; }
            set {
                _awakeAirDashDuration_field = value;
                OnPropertyChanged("AwakeAirDashDuration_field");
            }
        }
        private uint _awakeGroundedChakraDashDuration_field;
        public uint AwakeGroundedChakraDashDuration_field {
            get { return _awakeGroundedChakraDashDuration_field; }
            set {
                _awakeGroundedChakraDashDuration_field = value;
                OnPropertyChanged("AwakeGroundedChakraDashDuration_field");
            }
        }
        private bool _enableAwaChakraDashPriority_field;
        public bool EnableChakraDashPriority_field {
            get { return _enableAwaChakraDashPriority_field; }
            set {
                _enableAwaChakraDashPriority_field = value;
                OnPropertyChanged("EnableChakraDashPriority_field");
            }
        }
        private bool _awakeningDebuffEnabler_field;
        public bool AwakeningDebuffEnabler_field {
            get { return _awakeningDebuffEnabler_field; }
            set {
                _awakeningDebuffEnabler_field = value;
                OnPropertyChanged("AwakeningDebuffEnabler_field");
            }
        }
        private float _chakraCostAwakening_field;
        public float ChakraCostAwakening_field {
            get { return _chakraCostAwakening_field; }
            set {
                _chakraCostAwakening_field = value;
                OnPropertyChanged("ChakraCostAwakening_field");
            }
        }
        private float _chakraBlockRecoverySpeed_field;
        public float ChakraBlockRecoverySpeed_field {
            get { return _chakraBlockRecoverySpeed_field; }
            set {
                _chakraBlockRecoverySpeed_field = value;
                OnPropertyChanged("ChakraBlockRecoverySpeed_field");
            }
        }
        private float _awakeningActionChargeSpeed_field;
        public float AwakeningActionChargeSpeed_field {
            get { return _awakeningActionChargeSpeed_field; }
            set {
                _awakeningActionChargeSpeed_field = value;
                OnPropertyChanged("AwakeningActionChargeSpeed_field");
            }
        }
        public ObservableCollection<DuelPlayerParamModel> DuelPlayerParamList { get; set; }
        private DuelPlayerParamModel _selectedDPP;
        public DuelPlayerParamModel SelectedDPP {
            get { return _selectedDPP; }
            set {
                _selectedDPP = value;
                if (value is not null && value.BinName is not null) {
                    Characode_field = value.BinName.Substring(0, value.BinName.Length - 7);
                    MotionCode_field = value.MotionCode;
                    Partner_field = value.Partner;
                    ChakraDashCondition_field = value.ChakraDashCondition;
                    AwakeningCondition1_field = value.AwakeningCondition1;
                    AwakeningCondition2_field = value.AwakeningCondition2;
                    AwakeningJutsuController_field = value.AwakeningJutsuController;
                    AwaBodyPriority_field = value.AwaBodyPriority;
                    DefaultAwaSkillIndex_field = value.DefaultAwaSkillIndex;
                    Support1_field = value.Support1;
                    Support2_field = value.Support2;
                    CameraDistance_field = value.CameraDistance;
                    Unk1_field = value.Unk1;
                    VictoryCameraAngle_field = value.VictoryCameraAngle;
                    Unk2_field = value.Unk2;
                    Unk3_field = value.Unk3;
                    Unk4_field = value.Unk4;
                    BaseMovementSpeed_field = value.BaseMovementSpeed;
                    BaseChakraDashSpeed_field = value.BaseChakraDashSpeed;
                    GuardPressure_field = value.GuardPressure;
                    AirDashSpeed_field = value.AirDashSpeed;
                    Attack_field = value.Attack;
                    Defense_field = value.Defense;
                    AssistDamage_field = value.AssistDamage;
                    ItemBuffDuration_field = value.ItemBuffDuration;
                    ChakraChargeSpeed_field = value.ChakraChargeSpeed;
                    AwakeningCondition_field = value.AwakeningCondition;
                    AwakeHPRequirement_field = value.AwakeHPRequirement;
                    BaseNinjaDashSpeed_field = value.BaseNinjaDashSpeed;
                    BaseAirDashDuration_field = value.BaseAirDashDuration;
                    BaseGroundedChakraDashDuration_field = value.BaseGroundedChakraDashDuration;
                    AwakeMovementSpeed_field = value.AwakeMovementSpeed;
                    AwakeChakraDashSpeed_field = value.AwakeChakraDashSpeed;
                    AwakeNinjaDashSpeed_field = value.AwakeNinjaDashSpeed;
                    AwakeAirDashDuration_field = value.AwakeAirDashDuration;
                    AwakeGroundedChakraDashDuration_field = value.AwakeGroundedChakraDashDuration;
                    EnableChakraDashPriority_field = value.EnableChakraDashPriority;
                    AwakeningDebuffEnabler_field = value.AwakeningDebuffEnabler;
                    ChakraCostAwakening_field = value.ChakraCostAwakening;
                    ChakraBlockRecoverySpeed_field = value.ChakraBlockRecoverySpeed;
                    AwakeningActionChargeSpeed_field = value.AwakeningActionChargeSpeed;
                    Items1_field = value.Items[0];
                    Items2_field = value.Items[1];
                    Items3_field = value.Items[2];
                    Items4_field = value.Items[3];
                    ItemsDuration1_field = value.ItemsDuration[0];
                    ItemsDuration2_field = value.ItemsDuration[1];
                    ItemsDuration3_field = value.ItemsDuration[2];
                    ItemsDuration4_field = value.ItemsDuration[3];
                    AwakeCostumesList = value.AwakeCostumes;
                    BaseCostumesList = value.BaseCostumes;
                }
                
                    OnPropertyChanged("SelectedDPP");
            }
        }
        private int _selectedDPPIndex;
        public int SelectedDPPIndex {
            get { return _selectedDPPIndex; }
            set {
                _selectedDPPIndex = value;
                OnPropertyChanged("SelectedDPPIndex");
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

        public byte[] fileByte;
        public string filePath;
        public DuelPlayerParamEditorViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            DuelPlayerParamList = new ObservableCollection<DuelPlayerParamModel>();
            BaseCostumesList = new ObservableCollection<CostumeModel>();
            AwakeCostumesList = new ObservableCollection<CostumeModel>();
            filePath = "";
        }

        public void Clear() {
            SelectedDPPIndex = -1;
            DuelPlayerParamList.Clear();
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
                ObservableCollection<DuelPlayerParamModel> temp_list = new ObservableCollection<DuelPlayerParamModel>();

                int EntryCount = BinaryReader.b_ReadIntRev(FileBytes, 36) - 1;
                int Index3 = 128;
                for (int x = 0; x < EntryCount; x++) {
                    DuelPlayerParamList.Add(new DuelPlayerParamModel());
                }
                for (int x = 0; x < EntryCount; x++) {
                    string path = BinaryReader.b_ReadString(FileBytes, Index3);
                    DuelPlayerParamList[x].BinPath = path;
                    Index3 = Index3 + path.Length + 1;
                }
                Index3++;
                List<string> binName = new List<string>();
                for (int x = 0; x < EntryCount + 2; x++) {
                    string name = BinaryReader.b_ReadString(FileBytes, Index3);
                    binName.Add(name);
                    Index3 = Index3 + name.Length + 1;
                }
                binName.RemoveAt(1);
                binName.RemoveAt(1);
                for (int x = 0; x < EntryCount; x++) {
                    DuelPlayerParamList[x].BinName = binName[x];
                }

                int StartOfFile = 68 + BinaryReader.b_ReadIntRev(FileBytes, 16);
                for (int x = 0; x < EntryCount; x++) {
                    ObservableCollection<CostumeModel> baseCostumeList = new ObservableCollection<CostumeModel>();
                    ObservableCollection<CostumeModel> awaCostumeList = new ObservableCollection<CostumeModel>();
                    int _ptr = StartOfFile + 760 * x + 48 * x;
                    DuelPlayerParamList[x].MotionCode = BinaryReader.b_ReadString(FileBytes, _ptr);
                    for (int c2 = 0; c2 < 20; c2++) {
                        string cid = BinaryReader.b_ReadString(FileBytes, _ptr + 8 + 8 * c2);
                        CostumeModel entry = new CostumeModel();
                        entry.CostumeName = cid;
                        entry.CostumeIndex = c2;

                        baseCostumeList.Add(entry);
                    }
                    for (int c = 0; c < 20; c++) {
                        string awkcid = BinaryReader.b_ReadString(FileBytes, _ptr + 168 + 8 * c);
                        CostumeModel entry = new CostumeModel();
                        entry.CostumeName = awkcid;
                        entry.CostumeIndex = c;
                        awaCostumeList.Add(entry);
                    }
                    DuelPlayerParamList[x].BaseCostumes = baseCostumeList;
                    DuelPlayerParamList[x].AwakeCostumes = awaCostumeList;
                    DuelPlayerParamList[x].Partner = BinaryReader.b_ReadString(FileBytes, _ptr + 328);
                    DuelPlayerParamList[x].ChakraDashCondition = FileBytes[_ptr + 336];
                    DuelPlayerParamList[x].AwakeningCondition1 = FileBytes[_ptr + 337];
                    DuelPlayerParamList[x].AwakeningCondition2 = FileBytes[_ptr + 338];
                    DuelPlayerParamList[x].AwakeningJutsuController = FileBytes[_ptr + 339];
                    DuelPlayerParamList[x].AwaBodyPriority = (uint)BinaryReader.b_ReadInt(FileBytes, _ptr + 352);
                    DuelPlayerParamList[x].DefaultAwaSkillIndex = BinaryReader.b_ReadInt(FileBytes, _ptr + 356);


                    List<int> FlagList = new List<int>();
                    for (int c = 0; c<15; c++) {
                        FlagList.Add(BinaryReader.b_ReadInt(FileBytes, _ptr + 360 + (4*c)));
                    }
                    DuelPlayerParamList[x].Flag_settings = FlagList;
                    DuelPlayerParamList[x].Support1 = BinaryReader.b_ReadString(FileBytes, _ptr + 420);
                    DuelPlayerParamList[x].Support2 = BinaryReader.b_ReadString(FileBytes, _ptr + 428);
                    DuelPlayerParamList[x].CameraDistance = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 436);
                    DuelPlayerParamList[x].Unk1 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 438);
                    DuelPlayerParamList[x].VictoryCameraAngle = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 440);
                    DuelPlayerParamList[x].Unk2 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 442);
                    DuelPlayerParamList[x].Unk3 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 444);
                    DuelPlayerParamList[x].Unk4 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 446);

                    DuelPlayerParamList[x].BaseMovementSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 448);
                    DuelPlayerParamList[x].BaseChakraDashSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 452);
                    DuelPlayerParamList[x].GuardPressure = BinaryReader.b_ReadFloat(FileBytes, _ptr + 456);
                    DuelPlayerParamList[x].AirDashSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 460);
                    DuelPlayerParamList[x].Attack = BinaryReader.b_ReadFloat(FileBytes, _ptr + 464);
                    DuelPlayerParamList[x].Defense = BinaryReader.b_ReadFloat(FileBytes, _ptr + 468);
                    DuelPlayerParamList[x].AssistDamage = BinaryReader.b_ReadFloat(FileBytes, _ptr + 472);
                    DuelPlayerParamList[x].ItemBuffDuration = BinaryReader.b_ReadFloat(FileBytes, _ptr + 476);
                    DuelPlayerParamList[x].ChakraChargeSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 480);
                    DuelPlayerParamList[x].AwakeningCondition = BinaryReader.b_ReadString(FileBytes, _ptr + 484);
                    DuelPlayerParamList[x].AwakeHPRequirement = BinaryReader.b_ReadFloat(FileBytes, _ptr + 500);
                    DuelPlayerParamList[x].BaseNinjaDashSpeed = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 504);
                    DuelPlayerParamList[x].BaseAirDashDuration = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 506);
                    DuelPlayerParamList[x].BaseGroundedChakraDashDuration = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 508);

                    DuelPlayerParamList[x].Unk5 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 510);
                    DuelPlayerParamList[x].Unk6 = BinaryReader.b_ReadFloat(FileBytes, _ptr + 512);

                    List<string> ItemList = new List<string>();
                    List<int> ItemCountList = new List<int>();
                    for (int i = 0; i < 4; i++) {
                        ItemList.Add(BinaryReader.b_ReadString(FileBytes, _ptr + 516 + 32 * i));
                        ItemCountList.Add(BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 546 + 32 * i));
                    }
                    DuelPlayerParamList[x].Items = ItemList;
                    DuelPlayerParamList[x].ItemsDuration = ItemCountList;
                    DuelPlayerParamList[x].AwakeMovementSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 644);
                    DuelPlayerParamList[x].AwakeChakraDashSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 648);
                    DuelPlayerParamList[x].AwakeNinjaDashSpeed = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 652);
                    DuelPlayerParamList[x].AwakeAirDashDuration = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 654);
                    DuelPlayerParamList[x].AwakeGroundedChakraDashDuration = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 656);
                    DuelPlayerParamList[x].Unk7 = (uint)BinaryReader.b_ReadIntFromTwoBytes(FileBytes, _ptr + 658);

                    List<float> unk8_17List = new List<float>();
                    for (int i = 0; i < 10; i++) {
                        unk8_17List.Add(BinaryReader.b_ReadFloat(FileBytes, _ptr + 660 + (4* i)));
                    }
                    DuelPlayerParamList[x].Unk8_17 = unk8_17List;
                    DuelPlayerParamList[x].Unk18 = BinaryReader.b_ReadInt(FileBytes, _ptr + 700);
                    DuelPlayerParamList[x].EnableChakraDashPriority = Convert.ToBoolean(BinaryReader.b_ReadInt(FileBytes, _ptr + 704));
                    DuelPlayerParamList[x].AwakeningDebuffEnabler = Convert.ToBoolean(BinaryReader.b_ReadInt(FileBytes, _ptr + 708));
                    DuelPlayerParamList[x].ChakraCostAwakening = BinaryReader.b_ReadFloat(FileBytes, _ptr + 712);
                    DuelPlayerParamList[x].Unk19 = BinaryReader.b_ReadFloat(FileBytes, _ptr + 716);
                    DuelPlayerParamList[x].ChakraBlockRecoverySpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 720);
                    DuelPlayerParamList[x].AwakeningActionChargeSpeed = BinaryReader.b_ReadFloat(FileBytes, _ptr + 724);
                    
                }
                Index3++;
            }
        }

        public void RemoveEntry() {
            if (SelectedDPP is not null) {
                DuelPlayerParamList.Remove(SelectedDPP);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public void SaveBaseCostumeEntry() { 
            if (SelectedBaseCostume is not null && SelectedDPP is not null) {
                DuelPlayerParamList[SelectedDPPIndex].BaseCostumes[SelectedBaseCostumeIndex].CostumeName = BaseCostume_field;
            }
        }
        public void SaveAwakeCostumeEntry() {
            if (SelectedAwakeCostume is not null && SelectedDPP is not null) {
                DuelPlayerParamList[SelectedDPPIndex].AwakeCostumes[SelectedAwakeCostumeIndex].CostumeName = AwakeCostume_field;
            }
        }
        public void SaveEntry() {
            if (SelectedDPP is not null) {
                DuelPlayerParamList[SelectedDPPIndex].BinPath = "D:/next5/char_hi/param/player/Converter/bin/" + Characode_field + "prm_bas.bin";
                DuelPlayerParamList[SelectedDPPIndex].BinName = Characode_field + "prm_bas";
                DuelPlayerParamList[SelectedDPPIndex].MotionCode = MotionCode_field;
                DuelPlayerParamList[SelectedDPPIndex].Partner = Partner_field;
                DuelPlayerParamList[SelectedDPPIndex].ChakraDashCondition = ChakraDashCondition_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningCondition1 = AwakeningCondition1_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningCondition2 = AwakeningCondition2_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningJutsuController = AwakeningJutsuController_field;
                DuelPlayerParamList[SelectedDPPIndex].AwaBodyPriority = AwaBodyPriority_field;
                DuelPlayerParamList[SelectedDPPIndex].DefaultAwaSkillIndex = DefaultAwaSkillIndex_field;
                DuelPlayerParamList[SelectedDPPIndex].Support1 = Support1_field;
                DuelPlayerParamList[SelectedDPPIndex].Support2 = Support2_field;
                DuelPlayerParamList[SelectedDPPIndex].CameraDistance = CameraDistance_field;
                DuelPlayerParamList[SelectedDPPIndex].Unk1 = Unk1_field;
                DuelPlayerParamList[SelectedDPPIndex].VictoryCameraAngle = VictoryCameraAngle_field;
                DuelPlayerParamList[SelectedDPPIndex].Unk2 = Unk2_field;
                DuelPlayerParamList[SelectedDPPIndex].Unk3 = Unk3_field;
                DuelPlayerParamList[SelectedDPPIndex].Unk4 = Unk4_field;
                DuelPlayerParamList[SelectedDPPIndex].BaseMovementSpeed = BaseMovementSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].BaseChakraDashSpeed = BaseChakraDashSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].GuardPressure = GuardPressure_field;
                DuelPlayerParamList[SelectedDPPIndex].AirDashSpeed = AirDashSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].Attack = Attack_field;
                DuelPlayerParamList[SelectedDPPIndex].Defense = Defense_field;
                DuelPlayerParamList[SelectedDPPIndex].AssistDamage = AssistDamage_field;
                DuelPlayerParamList[SelectedDPPIndex].ItemBuffDuration = ItemBuffDuration_field;
                DuelPlayerParamList[SelectedDPPIndex].ChakraChargeSpeed = ChakraChargeSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningCondition = AwakeningCondition_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeHPRequirement = AwakeHPRequirement_field;
                DuelPlayerParamList[SelectedDPPIndex].BaseNinjaDashSpeed = BaseNinjaDashSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].BaseAirDashDuration = BaseAirDashDuration_field;
                DuelPlayerParamList[SelectedDPPIndex].BaseGroundedChakraDashDuration = BaseGroundedChakraDashDuration_field;
                DuelPlayerParamList[SelectedDPPIndex].Items[0] = Items1_field;
                DuelPlayerParamList[SelectedDPPIndex].Items[1] = Items2_field;
                DuelPlayerParamList[SelectedDPPIndex].Items[2] = Items3_field;
                DuelPlayerParamList[SelectedDPPIndex].Items[3] = Items4_field;
                DuelPlayerParamList[SelectedDPPIndex].ItemsDuration[0] = ItemsDuration1_field;
                DuelPlayerParamList[SelectedDPPIndex].ItemsDuration[1] = ItemsDuration2_field;
                DuelPlayerParamList[SelectedDPPIndex].ItemsDuration[2] = ItemsDuration3_field;
                DuelPlayerParamList[SelectedDPPIndex].ItemsDuration[3] = ItemsDuration4_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeMovementSpeed = AwakeMovementSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeChakraDashSpeed = AwakeChakraDashSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeNinjaDashSpeed = AwakeNinjaDashSpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeAirDashDuration = AwakeAirDashDuration_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeGroundedChakraDashDuration = AwakeGroundedChakraDashDuration_field;
                DuelPlayerParamList[SelectedDPPIndex].EnableChakraDashPriority = EnableChakraDashPriority_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningDebuffEnabler = AwakeningDebuffEnabler_field;
                DuelPlayerParamList[SelectedDPPIndex].ChakraCostAwakening = ChakraCostAwakening_field;
                DuelPlayerParamList[SelectedDPPIndex].ChakraBlockRecoverySpeed = ChakraBlockRecoverySpeed_field;
                DuelPlayerParamList[SelectedDPPIndex].AwakeningActionChargeSpeed = AwakeningActionChargeSpeed_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public int SearchStringIndex(ObservableCollection<DuelPlayerParamModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].BinName;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchEntry() {
            if (SearchTextBox_field is not null) {
                if (SearchStringIndex(DuelPlayerParamList, SearchTextBox_field, SelectedDPPIndex) != -1) {
                    SelectedDPPIndex = SearchStringIndex(DuelPlayerParamList, SearchTextBox_field, SelectedDPPIndex);
                    CollectionViewSource.GetDefaultView(DuelPlayerParamList).MoveCurrentTo(SelectedDPP);
                } else {
                    if (SearchStringIndex(DuelPlayerParamList, SearchTextBox_field, 0) != -1) {
                        SelectedDPPIndex = SearchStringIndex(DuelPlayerParamList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(DuelPlayerParamList).MoveCurrentTo(SelectedDPP);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }

        public void AddDupEntry() {
            DuelPlayerParamModel DPP_entry = new DuelPlayerParamModel();
            if (SelectedDPP is not null) {
                DPP_entry = (DuelPlayerParamModel)DuelPlayerParamList[SelectedDPPIndex].Clone();
            } else {
                DPP_entry.BinPath = "D:/next5/char_hi/param/player/Converter/bin/" + DuelPlayerParamList.Count.ToString("X2") + "cdprm_bas.bin";
                DPP_entry.BinName = DuelPlayerParamList.Count.ToString("X2") + "cdprm_bas";
                DPP_entry.MotionCode = DuelPlayerParamList.Count.ToString("X2") + "cd";

                ObservableCollection<CostumeModel> new_CostumeList = new ObservableCollection<CostumeModel>();
                for (int i = 0; i<20; i++) {
                    CostumeModel entry = new CostumeModel();
                    entry.CostumeIndex = i;
                    entry.CostumeName = "";
                    new_CostumeList.Add(entry);

                }
                ObservableCollection<CostumeModel> new_awaCostumeList = new ObservableCollection<CostumeModel>();
                for (int i = 0; i < 20; i++) {
                    CostumeModel entry = new CostumeModel();
                    entry.CostumeIndex = i;
                    entry.CostumeName = "";
                    new_awaCostumeList.Add(entry);

                }
                DPP_entry.BaseCostumes = new_CostumeList;
                DPP_entry.AwakeCostumes = new_awaCostumeList;
                DPP_entry.Partner = "";
                DPP_entry.ChakraDashCondition = 0;
                DPP_entry.AwakeningCondition1 = 0;
                DPP_entry.AwakeningCondition2 = 64;
                DPP_entry.AwakeningJutsuController = 0;
                DPP_entry.AwaBodyPriority = 190;
                DPP_entry.DefaultAwaSkillIndex = -1;

                int[] flags = new int[15] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0 };
                DPP_entry.Flag_settings = flags.ToList();
                DPP_entry.Support1 = "2nrt";
                DPP_entry.Support2 = "";
                DPP_entry.CameraDistance = 160;
                DPP_entry.Unk1 = 160;
                DPP_entry.VictoryCameraAngle = 150;
                DPP_entry.Unk2 = 40;
                DPP_entry.Unk3 = 45;
                DPP_entry.Unk4 = 110;
                DPP_entry.BaseMovementSpeed = 32;
                DPP_entry.BaseChakraDashSpeed = 100;
                DPP_entry.GuardPressure = 1;
                DPP_entry.AirDashSpeed = 1;
                DPP_entry.Attack = 1;
                DPP_entry.Defense = 1;
                DPP_entry.AssistDamage = 1;
                DPP_entry.ItemBuffDuration = 1;
                DPP_entry.ChakraChargeSpeed = 1;
                DPP_entry.AwakeningCondition = "AWAKE_1SIK";
                DPP_entry.AwakeHPRequirement = 40;
                DPP_entry.BaseNinjaDashSpeed = 70;
                DPP_entry.BaseAirDashDuration = 14;
                DPP_entry.BaseGroundedChakraDashDuration = 25;
                DPP_entry.Unk5 = 15;
                DPP_entry.Unk6 = (float)0.5;

                string[] items = new string[4]{ "BATTLE_ITEM150", "BATTLE_ITEM126", "BATTLE_ITEM111", "BATTLE_ITEM144" };
                int[] itemsdur = new int[4] { 8, 5, 6, 3 };
                DPP_entry.Items = items.ToList();
                DPP_entry.ItemsDuration = itemsdur.ToList();
                DPP_entry.AwakeMovementSpeed = 32;
                DPP_entry.AwakeChakraDashSpeed = 100;
                DPP_entry.AwakeNinjaDashSpeed = 70;
                DPP_entry.AwakeAirDashDuration = 14;
                DPP_entry.AwakeGroundedChakraDashDuration = 25;
                DPP_entry.Unk7 = 15;

                float[] unk_list = new float[10] { (float)0.5, (float)0.5,(float)0.75, (float)1.8,5,2,1,1,15,2 };

                DPP_entry.Unk8_17 = unk_list.ToList();
                DPP_entry.Unk18 = 0;
                DPP_entry.EnableChakraDashPriority = true;
                DPP_entry.AwakeningDebuffEnabler = true;
                DPP_entry.ChakraCostAwakening = 0;
                DPP_entry.Unk19 = (float)0.1;
                DPP_entry.ChakraBlockRecoverySpeed = (float)0.1;
                DPP_entry.AwakeningActionChargeSpeed = (float)0.3;

            }
            DuelPlayerParamList.Add(DPP_entry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }

        public void SaveFile() {
            string old_path = filePath;
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
            string old_path = filePath;
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
            if (DuelPlayerParamList.Count > 0) {

                File.WriteAllBytes(filePath, ConvertToFile());
                if (basepath == "")
                    ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
            } else {
                ModernWpf.MessageBox.Show("No entries. Failed to save file.", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public byte[] ConvertToFile() {
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x3E, 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            for (int x6 = 0; x6 < DuelPlayerParamList.Count; x6++) {
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, DuelPlayerParamList[x6].BinPath);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            for (int x5 = 0; x5 < 1; x5++) {
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, DuelPlayerParamList[x5].BinName);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "Page0");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "index");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            for (int x4 = 1; x4 < DuelPlayerParamList.Count; x4++) {
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, DuelPlayerParamList[x4].BinName);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

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

            for (int x3 = 1; x3 < DuelPlayerParamList.Count; x3++) {
                int actualEntry = x3 - 1;
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]
                {
                    0,
                    0,
                    0,
                    1
                });
                byte[] xbyte = BitConverter.GetBytes(2 + actualEntry);
                byte[] ybyte = BitConverter.GetBytes(4 + actualEntry);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, xbyte, 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, ybyte, 1);
            }

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
            for (int x2 = 1; x2 < DuelPlayerParamList.Count; x2++) {
                int actualEntry2 = x2 - 1;
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]);
                byte[] xbyte2 = BitConverter.GetBytes(4 + actualEntry2);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, xbyte2, 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]
                {
                    0,
                    0,
                    0,
                    2
                });
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4]
                {
                    0,
                    0,
                    0,
                    3
                });
            }

            totalLength4 = fileBytes36.Length;

            int PathLength = PtrPath - 127;
            int NameLength = PtrName - PtrPath;
            int Section1Length = PtrSection - PtrName - AddedBytes;
            int FullLength = totalLength4 - 68 + 40;
            int ReplaceIndex8 = 16;
            byte[] buffer8 = BitConverter.GetBytes(FullLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 36;
            buffer8 = BitConverter.GetBytes(DuelPlayerParamList.Count + 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 40;
            buffer8 = BitConverter.GetBytes(PathLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 44;
            buffer8 = BitConverter.GetBytes(DuelPlayerParamList.Count + 3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 48;
            buffer8 = BitConverter.GetBytes(NameLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 52;
            buffer8 = BitConverter.GetBytes(DuelPlayerParamList.Count + 3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 56;
            buffer8 = BitConverter.GetBytes(Section1Length);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 60;
            buffer8 = BitConverter.GetBytes(DuelPlayerParamList.Count * 4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            for (int x = 0; x < DuelPlayerParamList.Count; x++) {
                fileBytes36 = ((x != 0) ? BinaryReader.b_AddBytes(fileBytes36, new byte[48]
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
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    99,
                    0,
                    0,
                    0,
                    0,
                    2,
                    252,
                    0,
                    0,
                    0,
                    1,
                    0,
                    99,
                    0,
                    0,
                    0,
                    0,
                    2,
                    248
                }) : BinaryReader.b_AddBytes(fileBytes36, new byte[40]
                {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    121,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    99,
                    0,
                    0,
                    0,
                    0,
                    2,
                    252,
                    0,
                    0,
                    0,
                    1,
                    0,
                    99,
                    0,
                    0,
                    0,
                    0,
                    2,
                    248
                }));
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[760]);
                int _ptr = 68 + FullLength + 48 * x + 760 * x;
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].MotionCode, _ptr, 8);
                for (int i = 0; i < 20; i++) {
                    fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].BaseCostumes[i].CostumeName, _ptr + 8 + 8 * i, 8);
                    fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].AwakeCostumes[i].CostumeName, _ptr + 168 + 8 * i, 8);
                }

                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].Partner, _ptr + 0x148, 8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[1] { DuelPlayerParamList[x].ChakraDashCondition }, _ptr + 0x150);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[1] { DuelPlayerParamList[x].AwakeningCondition1 }, _ptr + 0x151);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[1] { DuelPlayerParamList[x].AwakeningCondition2 }, _ptr + 0x152);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[1] { DuelPlayerParamList[x].AwakeningJutsuController }, _ptr + 0x153);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwaBodyPriority), _ptr + 0x160);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].DefaultAwaSkillIndex), _ptr + 0x164);

                for (int i = 0; i< DuelPlayerParamList[x].Flag_settings.Count; i++) {
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Flag_settings[i]), _ptr + 0x168 + (4*i));
                }
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].Support1, _ptr + 0x1A4, 8);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].Support2, _ptr + 0x1AC, 8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].CameraDistance), _ptr + 0x1B4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk1), _ptr + 0x1B6);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].VictoryCameraAngle), _ptr + 0x1B8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk2), _ptr + 0x1BA);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk3), _ptr + 0x1BC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk4), _ptr + 0x1BE);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].BaseMovementSpeed), _ptr + 0x1C0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].BaseChakraDashSpeed), _ptr + 0x1C4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].GuardPressure), _ptr + 0x1C8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AirDashSpeed), _ptr + 0x1CC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Attack), _ptr + 0x1D0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Defense), _ptr + 0x1D4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AssistDamage), _ptr + 0x1D8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].ItemBuffDuration), _ptr + 0x1DC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].ChakraChargeSpeed), _ptr + 0x1E0);
                fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].AwakeningCondition, _ptr + 0x1E4, 16);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeHPRequirement), _ptr + 0x1F4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].BaseNinjaDashSpeed), _ptr + 0x1F8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].BaseAirDashDuration), _ptr + 0x1FA);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].BaseGroundedChakraDashDuration), _ptr + 0x1FC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk5), _ptr + 0x1FE);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk6), _ptr + 0x200);
                for (int i = 0; i < DuelPlayerParamList[x].Items.Count; i++) {
                    fileBytes36 = BinaryReader.b_ReplaceString(fileBytes36, DuelPlayerParamList[x].Items[i], _ptr + 0x204 + (0x20 * i), 28);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].ItemsDuration[i]), _ptr + 0x222 + (0x20 * i));
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeMovementSpeed), _ptr + 0x284);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeChakraDashSpeed), _ptr + 0x288);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeNinjaDashSpeed), _ptr + 0x28C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeAirDashDuration), _ptr + 0x28E);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeGroundedChakraDashDuration), _ptr + 0x290);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk7), _ptr + 0x292);
                for (int i = 0; i < DuelPlayerParamList[x].Unk8_17.Count; i++) {

                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk8_17[i]), _ptr + 0x294 + (4*i));
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk18), _ptr + 0x2BC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].EnableChakraDashPriority), _ptr + 0x2C0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeningDebuffEnabler), _ptr + 0x2C4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].ChakraCostAwakening), _ptr + 0x2C8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].Unk19), _ptr + 0x2CC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].ChakraBlockRecoverySpeed), _ptr + 0x2D0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(DuelPlayerParamList[x].AwakeningActionChargeSpeed), _ptr + 0x2D4);
            }
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
        private RelayCommand _saveBaseCostumeEntryCommand;
        public RelayCommand SaveBaseCostumeEntryCommand {
            get {
                return _saveBaseCostumeEntryCommand ??
                  (_saveBaseCostumeEntryCommand = new RelayCommand(obj => {
                      SaveBaseCostumeEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveAwakeCostumeEntryCommand;
        public RelayCommand SaveAwakeCostumeEntryCommand {
            get {
                return _saveAwakeCostumeEntryCommand ??
                  (_saveAwakeCostumeEntryCommand = new RelayCommand(obj => {
                      SaveAwakeCostumeEntryAsync();
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

        public async void SaveBaseCostumeEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveBaseCostumeEntry()));

        }
        public async void SaveAwakeCostumeEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveAwakeCostumeEntry()));

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
