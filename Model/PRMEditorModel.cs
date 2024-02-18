using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NSC_ModManager.Model {
    public class PRMEditorModel : INotifyPropertyChanged {

        private string _binName;
        public string BinName {
            get { return _binName; }
            set {
                _binName = value;
                OnPropertyChanged("BinName");
            }
        }
        private string _binPath;
        public string BinPath {
            get { return _binPath; }
            set {
                _binPath = value;
                OnPropertyChanged("BinPath");
            }
        }

        private byte[] _binaryData;
        public byte[] BinaryData {
            get { return _binaryData; }
            set {
                _binaryData = value;
                OnPropertyChanged("BinaryData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class PRMProjectile_Model : ICloneable, INotifyPropertyChanged {

        private string _projectileName;
        public string ProjectileName {
            get { return _projectileName; }
            set {
                _projectileName = value;
                OnPropertyChanged("ProjectileName");
            }
        }
        private string _skillFileName;
        public string SkillFileName {
            get { return _skillFileName; }
            set {
                _skillFileName = value;
                OnPropertyChanged("SkillFileName");
            }
        }
        private string _skillEntryName;
        public string SkillEntryName {
            get { return _skillEntryName; }
            set {
                _skillEntryName = value;
                OnPropertyChanged("SkillEntryName");
            }
        }
        private byte _unk;
        public byte Unk {
            get { return _unk; }
            set {
                _unk = value;
                OnPropertyChanged("Unk");
            }
        }
        public object Clone() {
            return new PRMProjectile_Model {
                ProjectileName = this.ProjectileName,
                SkillFileName = this.SkillFileName,
                SkillEntryName = this.SkillEntryName,
                Unk = this.Unk
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class PRMCollision_Model : ICloneable, INotifyPropertyChanged {


        private int _type;
        public int Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private int _state;
        public int State {
            get { return _state; }
            set {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        private bool _boneEnabler;
        public bool BoneEnabler {
            get { return _boneEnabler; }
            set {
                _boneEnabler = value;
                OnPropertyChanged("BoneEnabler");
            }
        }
        private string _boneName;
        public string BoneName {
            get { return _boneName; }
            set {
                _boneName = value;
                OnPropertyChanged("BoneName");
            }
        }
        private UInt16 _hurtboxRadius;
        public UInt16 HurtboxRadius {
            get { return _hurtboxRadius; }
            set {
                _hurtboxRadius = value;
                OnPropertyChanged("HurtboxRadius");
            }
        }
        private Int16 _hurtbox_Y_Pos;
        public Int16 Hurtbox_Y_Pos {
            get { return _hurtbox_Y_Pos; }
            set {
                _hurtbox_Y_Pos = value;
                OnPropertyChanged("Hurtbox_Y_Pos");
            }
        }
        private Int16 _hurtbox_Z_Pos;
        public Int16 Hurtbox_Z_Pos {
            get { return _hurtbox_Z_Pos; }
            set {
                _hurtbox_Z_Pos = value;
                OnPropertyChanged("Hurtbox_Z_Pos");
            }
        }
        public object Clone() {
            return new PRMCollision_Model {
                Type = this.Type,
                State = this.State,
                BoneEnabler = this.BoneEnabler,
                BoneName = this.BoneName,
                HurtboxRadius = this.HurtboxRadius,
                Hurtbox_Y_Pos = this.Hurtbox_Y_Pos,
                Hurtbox_Z_Pos = this.Hurtbox_Z_Pos

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class PRMEtc_Model : ICloneable, INotifyPropertyChanged {
        private int _movementItemIndex;
        public int MovementItemIndex {
            get { return _movementItemIndex; }
            set {
                _movementItemIndex = value;
                OnPropertyChanged("MovementItemIndex");
            }
        }

        private UInt16 _frameActionUnlock;
        public UInt16 FrameActionUnlock {
            get { return _frameActionUnlock; }
            set {
                _frameActionUnlock = value;
                OnPropertyChanged("FrameActionUnlock");
            }
        }
        private UInt16 _actionLength;
        public UInt16 ActionLength {
            get { return _actionLength; }
            set {
                _actionLength = value;
                OnPropertyChanged("ActionLength");
            }
        }
        private UInt16 _unk1;
        public UInt16 Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private float _circleVelocity;
        public float CircleVelocity {
            get { return _circleVelocity; }
            set {
                _circleVelocity = value;
                OnPropertyChanged("CircleVelocity");
            }
        }
        private float _unk2;
        public float Unk2 {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private float _circleVelocityStrength;
        public float CircleVelocityStrength {
            get { return _circleVelocityStrength; }
            set {
                _circleVelocityStrength = value;
                OnPropertyChanged("CircleVelocityStrength");
            }
        }
        private UInt16 _movementFrequency;
        public UInt16 MovementFrequency {
            get { return _movementFrequency; }
            set {
                _movementFrequency = value;
                OnPropertyChanged("MovementFrequency");
            }
        }
        private float _forwardVelocity;
        public float ForwardVelocity {
            get { return _forwardVelocity; }
            set {
                _forwardVelocity = value;
                OnPropertyChanged("ForwardVelocity");
            }
        }
        public object Clone() {
            return new PRMEtc_Model {
                FrameActionUnlock = this.FrameActionUnlock,
                ActionLength = this.ActionLength,
                Unk1 = this.Unk1,
                CircleVelocity = this.CircleVelocity,
                Unk2 = this.Unk2,
                CircleVelocityStrength = this.CircleVelocityStrength,
                MovementFrequency = this.MovementFrequency,
                ForwardVelocity = this.ForwardVelocity

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class PRMVER_Model : ICloneable, INotifyPropertyChanged {

        private string _binName;
        public string BinName {
            get { return _binName; }
            set {
                _binName = value;
                OnPropertyChanged("BinName");
            }
        }
        private string _binPath;
        public string BinPath {
            get { return _binPath; }
            set {
                _binPath = value;
                OnPropertyChanged("BinPath");
            }
        }

        private ObservableCollection<PRM_PL_ANM_Model> _pl_anm_sections;
        public ObservableCollection<PRM_PL_ANM_Model> PL_ANM_Sections {
            get { return _pl_anm_sections; }
            set {
                _pl_anm_sections = value;
                OnPropertyChanged("PL_ANM_Sections");
            }
        }
        public object Clone() {
            ObservableCollection<PRM_PL_ANM_Model> newPlAnmList = new ObservableCollection<PRM_PL_ANM_Model>();
            for (int i = 0; i<this.PL_ANM_Sections.Count; i++) {
                newPlAnmList.Add((PRM_PL_ANM_Model)this.PL_ANM_Sections[i].Clone());
            }
            return new PRMVER_Model {
                PL_ANM_Sections = newPlAnmList,
                BinName = this.BinName,
                BinPath = this.BinPath
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class PRM_PL_ANM_Model : ICloneable, INotifyPropertyChanged {

        private string _pl_anm_current_name;
        public string PL_ANM_current_name {
            get { return _pl_anm_current_name; }
            set {
                _pl_anm_current_name = value;
                OnPropertyChanged("PL_ANM_current_name");
            }
        }
        private string _pl_anm_animation;
        public string PL_ANM_animation {
            get { return _pl_anm_animation; }
            set {
                _pl_anm_animation = value;
                OnPropertyChanged("PL_ANM_animation");
            }
        }

        private string _pl_anm_previous_name;
        public string PL_ANM_previous_name {
            get { return _pl_anm_previous_name; }
            set {
                _pl_anm_previous_name = value;
                OnPropertyChanged("PL_ANM_previous_name");
            }
        }
        private string _pl_anm_next_name;
        public string PL_ANM_next_name {
            get { return _pl_anm_next_name; }
            set {
                _pl_anm_next_name = value;
                OnPropertyChanged("PL_ANM_next_name");
            }
        }

        private string _pl_anm_dmg_name;
        public string PL_ANM_DMG_name {
            get { return _pl_anm_dmg_name; }
            set {
                _pl_anm_dmg_name = value;
                OnPropertyChanged("PL_ANM_DMG_name");
            }
        }

        private int _type;
        public int Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        private int _direction;
        public int Direction {
            get { return _direction; }
            set {
                _direction = value;
                OnPropertyChanged("Direction");
            }
        }

        private int _interpolation;
        public int Interpolation {
            get { return _interpolation; }
            set {
                _interpolation = value;
                OnPropertyChanged("Interpolation");
            }
        }

        private int _trigger_condition_1;
        public int Trigger_condition_1 {
            get { return _trigger_condition_1; }
            set {
                _trigger_condition_1 = value;
                OnPropertyChanged("Trigger_condition_1");
            }
        }
        private int _trigger_condition_2;
        public int Trigger_condition_2 {
            get { return _trigger_condition_2; }
            set {
                _trigger_condition_2 = value;
                OnPropertyChanged("Trigger_condition_2");
            }
        }
        private int _link_condition;
        public int Link_condition {
            get { return _link_condition; }
            set {
                _link_condition = value;
                OnPropertyChanged("Link_condition");
            }
        }
        private bool _enableFaceAnim;
        public bool EnableFaceAnim {
            get { return _enableFaceAnim; }
            set {
                _enableFaceAnim = value;
                OnPropertyChanged("EnableFaceAnim");
            }
        }
        private bool _disableInterpolation;
        public bool DisableInterpolation {
            get { return _disableInterpolation; }
            set {
                _disableInterpolation = value;
                OnPropertyChanged("DisableInterpolation");
            }
        }
        private bool _enableCubeMan;
        public bool EnableCubeMan {
            get { return _enableCubeMan; }
            set {
                _enableCubeMan = value;
                OnPropertyChanged("EnableCubeMan");
            }
        }
        private bool _enableAnimPosFix;
        public bool EnableAnimPosFix {
            get { return _enableAnimPosFix; }
            set {
                _enableAnimPosFix = value;
                OnPropertyChanged("EnableAnimPosFix");
            }
        }
        private bool _enableBackwardFacingFix;
        public bool EnableBackwardFacingFix {
            get { return _enableBackwardFacingFix; }
            set {
                _enableBackwardFacingFix = value;
                OnPropertyChanged("EnableBackwardFacingFix");
            }
        }
        private Int16 _press_start;
        public Int16 Press_start {
            get { return _press_start; }
            set {
                _press_start = value;
                OnPropertyChanged("Press_start");
            }
        }
        private Int16 _press_end;
        public Int16 Press_end {
            get { return _press_end; }
            set {
                _press_end = value;
                OnPropertyChanged("Press_end");
            }
        }
        private ObservableCollection<PRMFunction_Model> _functionList;
        public ObservableCollection<PRMFunction_Model> FunctionList {
            get { return _functionList; }
            set {
                _functionList = value;
                OnPropertyChanged("FunctionList");
            }
        }
        public object Clone() {

            ObservableCollection<PRMFunction_Model> newFuncList = new ObservableCollection<PRMFunction_Model>();
            for (int i = 0; i<this.FunctionList.Count; i++) {

                newFuncList.Add((PRMFunction_Model)FunctionList[i].Clone());
            }

            return new PRM_PL_ANM_Model {
                PL_ANM_current_name = this.PL_ANM_current_name,
                PL_ANM_animation = this.PL_ANM_animation,
                PL_ANM_previous_name = this.PL_ANM_previous_name,
                PL_ANM_next_name = this.PL_ANM_next_name,
                PL_ANM_DMG_name = this.PL_ANM_DMG_name,
                Type = this.Type,
                Direction = this.Direction,
                Interpolation = this.Interpolation,
                Trigger_condition_1 = this.Trigger_condition_1,
                Trigger_condition_2 = this.Trigger_condition_2,
                Link_condition = this.Link_condition,
                EnableFaceAnim = this.EnableFaceAnim,
                DisableInterpolation = this.DisableInterpolation,
                EnableCubeMan = this.EnableCubeMan,
                EnableAnimPosFix = this.EnableAnimPosFix,
                EnableBackwardFacingFix = this.EnableBackwardFacingFix,
                Press_start = this.Press_start,
                Press_end = this.Press_end,
                FunctionList = newFuncList
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class PRMFunction_Model : ICloneable, INotifyPropertyChanged {
        private string _stringParam;
        public string StringParam {
            get { return _stringParam; }
            set {
                _stringParam = value;
                OnPropertyChanged("StringParam");
            }
        }
        private UInt16 _functionTiming;
        public UInt16 FunctionTiming {
            get { return _functionTiming; }
            set {
                _functionTiming = value;
                OnPropertyChanged("FunctionTiming");
            }
        }
        private Int16 _functionID;
        public Int16 FunctionID {
            get { return _functionID; }
            set {
                _functionID = value;
                OnPropertyChanged("FunctionID");
            }
        }
        private Int16 _functionParam1;
        public Int16 FunctionParam1 {
            get { return _functionParam1; }
            set {
                _functionParam1 = value;
                OnPropertyChanged("FunctionParam1");
            }
        }
        private Int16 _functionParam2;
        public Int16 FunctionParam2 {
            get { return _functionParam2; }
            set {
                _functionParam2 = value;
                OnPropertyChanged("FunctionParam2");
            }
        }
        private Int16 _functionParam3;
        public Int16 FunctionParam3 {
            get { return _functionParam3; }
            set {
                _functionParam3 = value;
                OnPropertyChanged("FunctionParam3");
            }
        }
        private float _functionParam4;
        public float FunctionParam4 {
            get { return _functionParam4; }
            set {
                _functionParam4 = value;
                OnPropertyChanged("FunctionParam4");
            }
        }
        private string _damageCode;
        public string DamageCode {
            get { return _damageCode; }
            set {
                _damageCode = value;
                OnPropertyChanged("DamageCode");
            }
        }
        private Int16 _damageHitEffectID;
        public Int16 DamageHitEffectID {
            get { return _damageHitEffectID; }
            set {
                _damageHitEffectID = value;
                OnPropertyChanged("DamageHitEffectID");
            }
        }
        private Int16 _damageHitSoundID;
        public Int16 DamageHitSoundID {
            get { return _damageHitSoundID; }
            set {
                _damageHitSoundID = value;
                OnPropertyChanged("DamageHitSoundID");
            }
        }
        private Int16 _damageCondition;
        public Int16 DamageCondition {
            get { return _damageCondition; }
            set {
                _damageCondition = value;
                OnPropertyChanged("DamageCondition");
            }
        }
        private Int16 _damageHitFrequency;
        public Int16 DamageHitFrequency {
            get { return _damageHitFrequency; }
            set {
                _damageHitFrequency = value;
                OnPropertyChanged("DamageHitFrequency");
            }
        }
        private Int16 _damageFreezeTime;
        public Int16 DamageFreezeTime {
            get { return _damageFreezeTime; }
            set {
                _damageFreezeTime = value;
                OnPropertyChanged("DamageFreezeTime");
            }
        }
        private float _damageAmount;
        public float DamageAmount {
            get { return _damageAmount; }
            set {
                _damageAmount = value;
                OnPropertyChanged("DamageAmount");
            }
        }
        private float _damageHorizontalPush;
        public float DamageHorizontalPush {
            get { return _damageHorizontalPush; }
            set {
                _damageHorizontalPush = value;
                OnPropertyChanged("DamageHorizontalPush");
            }
        }
        private float _damageVerticalPush;
        public float DamageVerticalPush {
            get { return _damageVerticalPush; }
            set {
                _damageVerticalPush = value;
                OnPropertyChanged("DamageVerticalPush");
            }
        }

        private Int16 _damageHitAmount;
        public Int16 DamageHitAmount {
            get { return _damageHitAmount; }
            set {
                _damageHitAmount = value;
                OnPropertyChanged("DamageHitAmount");
            }
        }
        private float _damageGuard;
        public float DamageGuard {
            get { return _damageGuard; }
            set {
                _damageGuard = value;
                OnPropertyChanged("DamageGuard");
            }
        }
        public object Clone() {
            return new PRMFunction_Model {
                StringParam = this.StringParam,
                FunctionTiming = this.FunctionTiming,
                FunctionID = this.FunctionID,
                FunctionParam1 = this.FunctionParam1,
                FunctionParam2 = this.FunctionParam2,
                FunctionParam3 = this.FunctionParam3,
                FunctionParam4 = this.FunctionParam4,
                DamageCode = this.DamageCode,
                DamageHitEffectID = this.DamageHitEffectID,
                DamageHitSoundID = this.DamageHitSoundID,
                DamageCondition = this.DamageCondition,
                DamageHitFrequency = this.DamageHitFrequency,
                DamageFreezeTime = this.DamageFreezeTime,
                DamageAmount = this.DamageAmount,
                DamageHorizontalPush = this.DamageHorizontalPush,
                DamageVerticalPush = this.DamageVerticalPush,
                DamageHitAmount = this.DamageHitAmount,
                DamageGuard = this.DamageGuard
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}

