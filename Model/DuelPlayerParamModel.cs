using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NSC_ModManager.Model {
    public class DuelPlayerParamModel : ICloneable, INotifyPropertyChanged {


        private string _binPath;
        public string BinPath {
            get { return _binPath; }
            set {
                _binPath = value;
                OnPropertyChanged("BinPath");
            }
        }
        private string _binName;
        public string BinName {
            get { return _binName; }
            set {
                _binName = value;
                OnPropertyChanged("BinName");
            }
        }

        private string _motionCode;
        public string MotionCode {
            get { return _motionCode; }
            set {
                _motionCode = value;
                OnPropertyChanged("MotionCode");
            }
        }
        private ObservableCollection<CostumeModel> _baseCostumes;
        public ObservableCollection<CostumeModel> BaseCostumes {
            get { return _baseCostumes; }
            set {
                _baseCostumes = value;
                OnPropertyChanged("BaseCostumes");
            }
        }
        private ObservableCollection<CostumeModel> _awakeCostumes;
        public ObservableCollection<CostumeModel> AwakeCostumes {
            get { return _awakeCostumes; }
            set {
                _awakeCostumes = value;
                OnPropertyChanged("AwakeCostumes");
            }
        }
        private string _partner;
        public string Partner {
            get { return _partner; }
            set {
                _partner = value;
                OnPropertyChanged("Partner");
            }
        }

        private byte _chakraDashCondition;
        public byte ChakraDashCondition {
            get { return _chakraDashCondition; }
            set {
                _chakraDashCondition = value;
                OnPropertyChanged("ChakraDashCondition");
            }
        }
        private byte _awakeningCondition1;
        public byte AwakeningCondition1 {
            get { return _awakeningCondition1; }
            set {
                _awakeningCondition1 = value;
                OnPropertyChanged("AwakeningCondition1");
            }
        }
        private byte _awakeningCondition2;
        public byte AwakeningCondition2 {
            get { return _awakeningCondition2; }
            set {
                _awakeningCondition2 = value;
                OnPropertyChanged("AwakeningCondition2");
            }
        }
        private byte _awakeningJutsuController;
        public byte AwakeningJutsuController {
            get { return _awakeningJutsuController; }
            set {
                _awakeningJutsuController = value;
                OnPropertyChanged("AwakeningJutsuController");
            }
        }
        private uint _awaBodyPriority;
        public uint AwaBodyPriority {
            get { return _awaBodyPriority; }
            set {
                _awaBodyPriority = value;
                OnPropertyChanged("AwaBodyPriority");
            }
        }
        private int _defaultAwaSkillIndex;
        public int DefaultAwaSkillIndex {
            get { return _defaultAwaSkillIndex; }
            set {
                _defaultAwaSkillIndex = value;
                OnPropertyChanged("DefaultAwaSkillIndex");
            }
        }
        private List<int> _flag_settings;
        public List<int> Flag_settings {
            get { return _flag_settings; }
            set {
                _flag_settings = value;
                OnPropertyChanged("Flag_settings");
            }
        }

        private string _support1;
        public string Support1 {
            get { return _support1; }
            set {
                _support1 = value;
                OnPropertyChanged("Support1");
            }
        }
        private string _support2;
        public string Support2 {
            get { return _support2; }
            set {
                _support2 = value;
                OnPropertyChanged("Support2");
            }
        }
        private uint _cameraDistance;
        public uint CameraDistance {
            get { return _cameraDistance; }
            set {
                _cameraDistance = value;
                OnPropertyChanged("CameraDistance");
            }
        }
        private uint _unk1;
        public uint Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private uint _victoryCameraAngle;
        public uint VictoryCameraAngle {
            get { return _victoryCameraAngle; }
            set {
                _victoryCameraAngle = value;
                OnPropertyChanged("VictoryCameraAngle");
            }
        }
        private uint _unk2;
        public uint Unk2 {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private uint _unk3;
        public uint Unk3 {
            get { return _unk3; }
            set {
                _unk3 = value;
                OnPropertyChanged("Unk3");
            }
        }
        private uint _unk4;
        public uint Unk4 {
            get { return _unk4; }
            set {
                _unk4 = value;
                OnPropertyChanged("Unk4");
            }
        }
        private float _baseMovementSpeed;
        public float BaseMovementSpeed {
            get { return _baseMovementSpeed; }
            set {
                _baseMovementSpeed = value;
                OnPropertyChanged("BaseMovementSpeed");
            }
        }
        private float _baseChakraDashSpeed;
        public float BaseChakraDashSpeed {
            get { return _baseChakraDashSpeed; }
            set {
                _baseChakraDashSpeed = value;
                OnPropertyChanged("BaseChakraDashSpeed");
            }
        }
        private float _guardPrssure;
        public float GuardPressure {
            get { return _guardPrssure; }
            set {
                _guardPrssure = value;
                OnPropertyChanged("GuardPressure");
            }
        }
        private float _airDashSpeed;
        public float AirDashSpeed {
            get { return _airDashSpeed; }
            set {
                _airDashSpeed = value;
                OnPropertyChanged("AirDashSpeed");
            }
        }
        private float _attack;
        public float Attack {
            get { return _attack; }
            set {
                _attack = value;
                OnPropertyChanged("Attack");
            }
        }
        private float _defense;
        public float Defense {
            get { return _defense; }
            set {
                _defense = value;
                OnPropertyChanged("Defense");
            }
        }
        private float _assistDamage;
        public float AssistDamage {
            get { return _assistDamage; }
            set {
                _assistDamage = value;
                OnPropertyChanged("AssistDamage");
            }
        }
        private float _itemBuffDuration;
        public float ItemBuffDuration {
            get { return _itemBuffDuration; }
            set {
                _itemBuffDuration = value;
                OnPropertyChanged("ItemBuffDuration");
            }
        }
        private float _chakraChargeSpeed;
        public float ChakraChargeSpeed {
            get { return _chakraChargeSpeed; }
            set {
                _chakraChargeSpeed = value;
                OnPropertyChanged("ChakraChargeSpeed");
            }
        }
        private string _awakeningCondition;
        public string AwakeningCondition {
            get { return _awakeningCondition; }
            set {
                _awakeningCondition = value;
                OnPropertyChanged("AwakeningCondition");
            }
        }
        private float _awakeHPRequirement;
        public float AwakeHPRequirement {
            get { return _awakeHPRequirement; }
            set {
                _awakeHPRequirement = value;
                OnPropertyChanged("AwakeHPRequirement");
            }
        }
        private uint _baseNinjaDashSpeed;
        public uint BaseNinjaDashSpeed {
            get { return _baseNinjaDashSpeed; }
            set {
                _baseNinjaDashSpeed = value;
                OnPropertyChanged("BaseNinjaDashSpeed");
            }
        }
        private uint _baseAirDashDuration;
        public uint BaseAirDashDuration {
            get { return _baseAirDashDuration; }
            set {
                _baseAirDashDuration = value;
                OnPropertyChanged("BaseAirDashDuration");
            }
        }
        private uint _baseGroundedChakraDashDuration;
        public uint BaseGroundedChakraDashDuration {
            get { return _baseGroundedChakraDashDuration; }
            set {
                _baseGroundedChakraDashDuration = value;
                OnPropertyChanged("BaseGroundedChakraDashDuration");
            }
        }
        private uint _unk5;
        public uint Unk5 {
            get { return _unk5; }
            set {
                _unk5 = value;
                OnPropertyChanged("Unk5");
            }
        }
        private float _unk6;
        public float Unk6 {
            get { return _unk6; }
            set {
                _unk6 = value;
                OnPropertyChanged("Unk6");
            }
        }
        private List<string> _items;
        public List<string> Items {
            get { return _items; }
            set {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        private List<int> _itemsDuration;
        public List<int> ItemsDuration {
            get { return _itemsDuration; }
            set {
                _itemsDuration = value;
                OnPropertyChanged("ItemsDuration");
            }
        }
        private float _awakeMovementSpeed;
        public float AwakeMovementSpeed {
            get { return _awakeMovementSpeed; }
            set {
                _awakeMovementSpeed = value;
                OnPropertyChanged("AwakeMovementSpeed");
            }
        }
        private float _awakeChakraDashSpeed;
        public float AwakeChakraDashSpeed {
            get { return _awakeChakraDashSpeed; }
            set {
                _awakeChakraDashSpeed = value;
                OnPropertyChanged("AwakeChakraDashSpeed");
            }
        }
        private uint _awakeNinjaDashSpeed;
        public uint AwakeNinjaDashSpeed {
            get { return _awakeNinjaDashSpeed; }
            set {
                _awakeNinjaDashSpeed = value;
                OnPropertyChanged("AwakeNinjaDashSpeed");
            }
        }
        private uint _awakeAirDashDuration;
        public uint AwakeAirDashDuration {
            get { return _awakeAirDashDuration; }
            set {
                _awakeAirDashDuration = value;
                OnPropertyChanged("AwakeAirDashDuration");
            }
        }
        private uint _awakeGroundedChakraDashDuration;
        public uint AwakeGroundedChakraDashDuration {
            get { return _awakeGroundedChakraDashDuration; }
            set {
                _awakeGroundedChakraDashDuration = value;
                OnPropertyChanged("AwakeGroundedChakraDashDuration");
            }
        }
        private uint _unk7;
        public uint Unk7 {
            get { return _unk7; }
            set {
                _unk7 = value;
                OnPropertyChanged("Unk7");
            }
        }
        private List<float> _unk8_17;
        public List<float> Unk8_17 {
            get { return _unk8_17; }
            set {
                _unk8_17 = value;
                OnPropertyChanged("Unk8_17");
            }
        }
        private int _unk18;
        public int Unk18 {
            get { return _unk18; }
            set {
                _unk18 = value;
                OnPropertyChanged("Unk18");
            }
        }
        private bool _enableAwaChakraDashPriority;
        public bool EnableChakraDashPriority {
            get { return _enableAwaChakraDashPriority; }
            set {
                _enableAwaChakraDashPriority = value;
                OnPropertyChanged("EnableChakraDashPriority");
            }
        }
        private bool _awakeningDebuffEnabler;
        public bool AwakeningDebuffEnabler {
            get { return _awakeningDebuffEnabler; }
            set {
                _awakeningDebuffEnabler = value;
                OnPropertyChanged("AwakeningDebuffEnabler");
            }
        }
        private float _chakraCostAwakening;
        public float ChakraCostAwakening {
            get { return _chakraCostAwakening; }
            set {
                _chakraCostAwakening = value;
                OnPropertyChanged("ChakraCostAwakening");
            }
        }
        private float _unk19;
        public float Unk19 {
            get { return _unk19; }
            set {
                _unk19 = value;
                OnPropertyChanged("Unk19");
            }
        }
        private float _chakraBlockRecoverySpeed;
        public float ChakraBlockRecoverySpeed {
            get { return _chakraBlockRecoverySpeed; }
            set {
                _chakraBlockRecoverySpeed = value;
                OnPropertyChanged("ChakraBlockRecoverySpeed");
            }
        }
        private float _awakeningActionChargeSpeed;
        public float AwakeningActionChargeSpeed {
            get { return _awakeningActionChargeSpeed; }
            set {
                _awakeningActionChargeSpeed = value;
                OnPropertyChanged("AwakeningActionChargeSpeed");
            }
        }
        
        public object Clone() {
            ObservableCollection<CostumeModel> newBaseCostumes = new ObservableCollection<CostumeModel>();
            ObservableCollection<CostumeModel> newAwakeCostumes = new ObservableCollection<CostumeModel>();
            List<int> newFlagSettings = new List<int>();
            List<string> newItems = new List<string>();
            List<int> newItemsDuration = new List<int>();
            List<float> newUnk8_17 = new List<float>();

            for (int i=0; i< BaseCostumes.Count; i++) {
                CostumeModel entry = new CostumeModel();
                entry.CostumeIndex = i;
                entry.CostumeName = BaseCostumes[i].CostumeName;
                newBaseCostumes.Add((CostumeModel)entry.Clone());
            }
            for (int i = 0; i < AwakeCostumes.Count; i++) {
                CostumeModel entry = new CostumeModel();
                entry.CostumeIndex = i;
                entry.CostumeName = AwakeCostumes[i].CostumeName;
                newAwakeCostumes.Add((CostumeModel)entry.Clone());
            }
            for (int i = 0; i < Items.Count; i++) {
                newItems.Add(Items[i]);
            }
            for (int i = 0; i < Flag_settings.Count; i++) {
                newFlagSettings.Add(Flag_settings[i]);
            }
            for (int i = 0; i < ItemsDuration.Count; i++) {
                newItemsDuration.Add(ItemsDuration[i]);
            }
            for (int i = 0; i < Unk8_17.Count; i++) {
                newUnk8_17.Add(Unk8_17[i]);
            }
            return new DuelPlayerParamModel {
                BinPath = this.BinPath,
                BinName = this.BinName,
                MotionCode = this.MotionCode,
                BaseCostumes = newBaseCostumes,
                AwakeCostumes = newAwakeCostumes,
                Partner = this.Partner,
                ChakraDashCondition = this.ChakraDashCondition,
                AwakeningCondition1 = this.AwakeningCondition1,
                AwakeningCondition2 = this.AwakeningCondition2,
                AwakeningJutsuController = this.AwakeningJutsuController,
                AwaBodyPriority = this.AwaBodyPriority,
                DefaultAwaSkillIndex = this.DefaultAwaSkillIndex,
                Flag_settings = newFlagSettings,
                Support1 = this.Support1,
                Support2 = this.Support2,
                CameraDistance = this.CameraDistance,
                Unk1 = this.Unk1,
                VictoryCameraAngle = this.VictoryCameraAngle,
                Unk2 = this.Unk2,
                Unk3 = this.Unk3,
                Unk4 = this.Unk4,
                BaseMovementSpeed = this.BaseMovementSpeed,
                BaseChakraDashSpeed = this.BaseChakraDashSpeed,
                GuardPressure = this.GuardPressure,
                AirDashSpeed = this.AirDashSpeed,
                Attack = this.Attack,
                Defense = this.Defense,
                AssistDamage = this.AssistDamage,
                ItemBuffDuration = this.ItemBuffDuration,
                ChakraChargeSpeed = this.ChakraChargeSpeed,
                AwakeningCondition = this.AwakeningCondition,
                AwakeHPRequirement = this.AwakeHPRequirement,
                BaseNinjaDashSpeed = this.BaseNinjaDashSpeed,
                BaseAirDashDuration = this.BaseAirDashDuration,
                BaseGroundedChakraDashDuration = this.BaseGroundedChakraDashDuration,
                Unk5 = this.Unk5,
                Unk6 = this.Unk6,
                Items = newItems,
                ItemsDuration = newItemsDuration,
                AwakeMovementSpeed = this.AwakeMovementSpeed,
                AwakeChakraDashSpeed = this.AwakeChakraDashSpeed,
                AwakeNinjaDashSpeed = this.AwakeNinjaDashSpeed,
                AwakeAirDashDuration = this.AwakeAirDashDuration,
                AwakeGroundedChakraDashDuration = this.AwakeGroundedChakraDashDuration,
                Unk7 = this.Unk7,
                Unk8_17 = newUnk8_17,
                Unk18 = this.Unk18,
                EnableChakraDashPriority = this.EnableChakraDashPriority,
                AwakeningDebuffEnabler = this.AwakeningDebuffEnabler,
                ChakraCostAwakening = this.ChakraCostAwakening,
                Unk19 = this.Unk19,
                ChakraBlockRecoverySpeed = this.ChakraBlockRecoverySpeed,
                AwakeningActionChargeSpeed = this.AwakeningActionChargeSpeed


            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class CostumeModel : ICloneable, INotifyPropertyChanged {


        private string _costumeName;
        public string CostumeName {
            get { return _costumeName; }
            set {
                _costumeName = value;
                OnPropertyChanged("CostumeName");
            }
        }
        private int _costumeIndex;
        public int CostumeIndex {
            get { return _costumeIndex; }
            set {
                _costumeIndex = value;
                OnPropertyChanged("CostumeIndex");
            }
        }
        public object Clone() {
            return new CostumeModel {
                CostumeName = this.CostumeName,
                CostumeIndex = this.CostumeIndex


            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
