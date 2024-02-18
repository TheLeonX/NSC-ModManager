using Microsoft.Win32;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NSC_ModManager.ViewModel
{
    public class PRMEditorViewModel : INotifyPropertyChanged {

        private Visibility _loadingStatePlay;
        public Visibility LoadingStatePlay {
            get { return _loadingStatePlay; }
            set {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }
        public ObservableCollection<string> FUNC_List { get; set; }
        public ObservableCollection<string> COND_List { get; set; }
        public ObservableCollection<string> COND2_List { get; set; }
        public ObservableCollection<string> LINKCOND_List { get; set; }
        public ObservableCollection<string> DMGCOND_List { get; set; }

        private int _selectedVerTypeIndex;
        public int SelectedVerTypeIndex {
            get { return _selectedVerTypeIndex; }
            set {
                _selectedVerTypeIndex = value;
                OnPropertyChanged("SelectedVerTypeIndex");
            }
        }
        private string _characodeBinName_field;
        public string CharacodeBinName_field {
            get { return _characodeBinName_field; }
            set {
                _characodeBinName_field = value;
                OnPropertyChanged("CharacodeBinName_field");
            }
        }
        public ObservableCollection<PRMEditorModel> BinaryList { get; set; }
        public ObservableCollection<PRMVER_Model> VerList { get; set; }
        private PRMVER_Model _selectedVer;
        public PRMVER_Model SelectedVer {
            get { return _selectedVer; }
            set {
                _selectedVer = value;
                if (value is not null) {
                    string cutBineName = value.BinName.Substring(value.BinName.IndexOf("prm_"));
                    switch (cutBineName) {
                        case "prm_mot":
                            SelectedVerTypeIndex = 0;
                            break;
                        case "prm_motcmn":
                            SelectedVerTypeIndex = 3;
                            break;
                        case "prm_awa":
                            SelectedVerTypeIndex = 1;
                            break;
                        case "prm_awa2":
                            SelectedVerTypeIndex = 2;
                            break;
                        case "prm_boss":
                            SelectedVerTypeIndex = 9;
                            break;
                        case "prm_boss01":
                            SelectedVerTypeIndex = 10;
                            break;
                        case "prm_boss02":
                            SelectedVerTypeIndex = 11;
                            break;
                        case "prm_boss03":
                            SelectedVerTypeIndex = 12;
                            break;
                        case "prm_boss04":
                            SelectedVerTypeIndex = 13;
                            break;
                        case "prm_upgrade01":
                            SelectedVerTypeIndex = 6;
                            break;
                        case "prm_upgrade02":
                            SelectedVerTypeIndex = 7;
                            break;
                        case "prm_upgrade03":
                            SelectedVerTypeIndex = 8;
                            break;
                        case "prm_spl":
                            SelectedVerTypeIndex = 5;
                            break;
                        case "prm_skl":
                            SelectedVerTypeIndex = 4;
                            break;
                    }

                    PL_ANM_List = value.PL_ANM_Sections;

                }
                OnPropertyChanged("SelectedVer");
            }
        }
        private ObservableCollection<PRM_PL_ANM_Model> _PL_ANM_List;
        public ObservableCollection<PRM_PL_ANM_Model> PL_ANM_List {
            get { return _PL_ANM_List; }
            set {
                _PL_ANM_List = value;
                OnPropertyChanged("PL_ANM_List");
            }
        }
        private PRM_PL_ANM_Model _selectedPL_ANM;
        public PRM_PL_ANM_Model SelectedPL_ANM {
            get { return _selectedPL_ANM; }
            set {
                _selectedPL_ANM = value;
                if (value is not null) {
                    PL_ANM_current_name_field = value.PL_ANM_current_name;
                    PL_ANM_animation_field = value.PL_ANM_animation;
                    PL_ANM_previous_name_field = value.PL_ANM_previous_name;
                    PL_ANM_next_name_field = value.PL_ANM_next_name;
                    PL_ANM_DMG_name_field = value.PL_ANM_DMG_name;
                    Type_field = value.Type;
                    Direction_field = value.Direction;
                    Interpolation_field = value.Interpolation;
                    Trigger_condition_1_field = value.Trigger_condition_1;
                    Trigger_condition_2_field = value.Trigger_condition_2;
                    Link_condition_field = value.Link_condition + 1;
                    DisableInterpolation_field = value.DisableInterpolation;
                    EnableCubeMan_field = value.EnableCubeMan;
                    EnableFaceAnim_field = value.EnableFaceAnim;
                    EnableAnimPosFix_field = value.EnableAnimPosFix;
                    EnableBackwardFacingFix_field = value.EnableBackwardFacingFix;
                    Press_start_field = value.Press_start;
                    Press_end_field = value.Press_end;
                    FunctionList = value.FunctionList;


                }
                OnPropertyChanged("SelectedPL_ANM");
            }
        }
        private int _selectedPL_ANMIndex;
        public int SelectedPL_ANMIndex {
            get { return _selectedPL_ANMIndex; }
            set {
                _selectedPL_ANMIndex = value;
                OnPropertyChanged("SelectedPL_ANMIndex");
            }
        }
        private int _selectedVerIndex;
        public int SelectedVerIndex {
            get { return _selectedVerIndex; }
            set {
                _selectedVerIndex = value;
                OnPropertyChanged("SelectedVerIndex");
            }
        }

        public ObservableCollection<PRMEtc_Model> MovementList { get; set; }
        private PRMEtc_Model _selectedMovement;
        public PRMEtc_Model SelectedMovement {
            get { return _selectedMovement; }
            set {
                _selectedMovement = value;
                if (value is not null) {

                    FrameActionUnlock_field = value.FrameActionUnlock;
                    ActionLength_field = value.ActionLength;
                    Unk1_field = value.Unk1;
                    CircleVelocity_field = value.CircleVelocity;
                    Unk2_field = value.Unk2;
                    CircleVelocityStrength_field = value.CircleVelocityStrength;
                    MovementFrequency_field = value.MovementFrequency;
                    ForwardVelocity_field = value.ForwardVelocity;

                }
                OnPropertyChanged("SelectedMovement");
            }
        }
        private int _selectedMovementIndex;
        public int SelectedMovementIndex {
            get { return _selectedMovementIndex; }
            set {
                _selectedMovementIndex = value;
                OnPropertyChanged("SelectedMovementIndex");
            }
        }

        private UInt16 _frameActionUnlock_field;
        public UInt16 FrameActionUnlock_field {
            get { return _frameActionUnlock_field; }
            set {
                _frameActionUnlock_field = value;
                OnPropertyChanged("FrameActionUnlock_field");
            }
        }
        private UInt16 _actionLength_field;
        public UInt16 ActionLength_field {
            get { return _actionLength_field; }
            set {
                _actionLength_field = value;
                OnPropertyChanged("ActionLength_field");
            }
        }
        private UInt16 _unk1_field;
        public UInt16 Unk1_field {
            get { return _unk1_field; }
            set {
                _unk1_field = value;
                OnPropertyChanged("Unk1_field");
            }
        }
        private float _circleVelocity_field;
        public float CircleVelocity_field {
            get { return _circleVelocity_field; }
            set {
                _circleVelocity_field = value;
                OnPropertyChanged("CircleVelocity_field");
            }
        }
        private float _unk2_field;
        public float Unk2_field {
            get { return _unk2_field; }
            set {
                _unk2_field = value;
                OnPropertyChanged("Unk2_field");
            }
        }
        private float _circleVelocityStrength_field;
        public float CircleVelocityStrength_field {
            get { return _circleVelocityStrength_field; }
            set {
                _circleVelocityStrength_field = value;
                OnPropertyChanged("CircleVelocityStrength_field");
            }
        }
        private UInt16 _movementFrequency_field;
        public UInt16 MovementFrequency_field {
            get { return _movementFrequency_field; }
            set {
                _movementFrequency_field = value;
                OnPropertyChanged("MovementFrequency_field");
            }
        }
        private float _forwardVelocity_field;
        public float ForwardVelocity_field {
            get { return _forwardVelocity_field; }
            set {
                _forwardVelocity_field = value;
                OnPropertyChanged("ForwardVelocity_field");
            }
        }


        public ObservableCollection<PRMProjectile_Model> ProjectileList { get; set; }
        private PRMProjectile_Model _selectedProjectile;
        public PRMProjectile_Model SelectedProjectile {
            get { return _selectedProjectile; }
            set {
                _selectedProjectile = value;
                if (value is not null) {

                    ProjectileName_field = value.ProjectileName;
                    SkillFileName_field = value.SkillFileName;
                    SkillEntryName_field = value.SkillEntryName;
                    Unk_field = value.Unk;

                }
                OnPropertyChanged("SelectedProjectile");
            }
        }
        private int _selectedProjectileIndex;
        public int SelectedProjectileIndex {
            get { return _selectedProjectileIndex; }
            set {
                _selectedProjectileIndex = value;
                OnPropertyChanged("SelectedProjectileIndex");
            }
        }
        private string _projectileName_field;
        public string ProjectileName_field {
            get { return _projectileName_field; }
            set {
                _projectileName_field = value;
                OnPropertyChanged("ProjectileName_field");
            }
        }
        private string _skillFileName_field;
        public string SkillFileName_field {
            get { return _skillFileName_field; }
            set {
                _skillFileName_field = value;
                OnPropertyChanged("SkillFileName_field");
            }
        }
        private string _skillEntryName_field;
        public string SkillEntryName_field {
            get { return _skillEntryName_field; }
            set {
                _skillEntryName_field = value;
                OnPropertyChanged("SkillEntryName_field");
            }
        }
        private byte _unk_field;
        public byte Unk_field {
            get { return _unk_field; }
            set {
                _unk_field = value;
                OnPropertyChanged("Unk_field");
            }
        }
        public ObservableCollection<PRMCollision_Model> CollisionList { get; set; }
        private PRMCollision_Model _selectedCollision;
        public PRMCollision_Model SelectedCollision {
            get { return _selectedCollision; }
            set {
                _selectedCollision = value;
                if (value is not null) {

                    TypeCollision_field = value.Type;
                    StateCollision_field = value.State;
                    BoneEnablerCollision_field = !value.BoneEnabler;
                    BoneNameCollision_field = value.BoneName;
                    HurtboxRadiusCollision_field = value.HurtboxRadius;
                    Hurtbox_Y_PosCollision_field = value.Hurtbox_Y_Pos;
                    Hurtbox_Z_PosCollision_field = value.Hurtbox_Z_Pos;

                }
                OnPropertyChanged("SelectedCollision");
            }
        }
        private int _selectedCollisionIndex;
        public int SelectedCollisionIndex {
            get { return _selectedCollisionIndex; }
            set {
                _selectedCollisionIndex = value;
                OnPropertyChanged("SelectedCollisionIndex");
            }
        }
        private int _typeCollision_field;
        public int TypeCollision_field {
            get { return _typeCollision_field; }
            set {
                _typeCollision_field = value;
                OnPropertyChanged("TypeCollision_field");
            }
        }
        private int _stateCollision_field;
        public int StateCollision_field {
            get { return _stateCollision_field; }
            set {
                _stateCollision_field = value;
                OnPropertyChanged("StateCollision_field");
            }
        }
        private bool _boneEnablerCollision_field;
        public bool BoneEnablerCollision_field {
            get { return _boneEnablerCollision_field; }
            set {
                _boneEnablerCollision_field = value;
                OnPropertyChanged("BoneEnablerCollision_field");
            }
        }
        private string _boneNameCollision_field;
        public string BoneNameCollision_field {
            get { return _boneNameCollision_field; }
            set {
                _boneNameCollision_field = value;
                OnPropertyChanged("BoneNameCollision_field");
            }
        }
        private UInt16 _hurtboxRadiusCollision_field;
        public UInt16 HurtboxRadiusCollision_field {
            get { return _hurtboxRadiusCollision_field; }
            set {
                _hurtboxRadiusCollision_field = value;
                OnPropertyChanged("HurtboxRadiusCollision_field");
            }
        }
        private Int16 _hurtbox_Y_PosCollision_field;
        public Int16 Hurtbox_Y_PosCollision_field {
            get { return _hurtbox_Y_PosCollision_field; }
            set {
                _hurtbox_Y_PosCollision_field = value;
                OnPropertyChanged("Hurtbox_Y_PosCollision_field");
            }
        }
        private Int16 _hurtbox_Z_PosCollision_field;
        public Int16 Hurtbox_Z_PosCollision_field {
            get { return _hurtbox_Z_PosCollision_field; }
            set {
                _hurtbox_Z_PosCollision_field = value;
                OnPropertyChanged("Hurtbox_Z_PosCollision_field");
            }
        }

        private string _pl_anm_current_name_field;
        public string PL_ANM_current_name_field {
            get { return _pl_anm_current_name_field; }
            set {
                _pl_anm_current_name_field = value;
                OnPropertyChanged("PL_ANM_current_name_field");
            }
        }
        private string _pl_anm_animation_field;
        public string PL_ANM_animation_field {
            get { return _pl_anm_animation_field; }
            set {
                _pl_anm_animation_field = value;
                OnPropertyChanged("PL_ANM_animation_field");
            }
        }

        private string _pl_anm_previous_name_field;
        public string PL_ANM_previous_name_field {
            get { return _pl_anm_previous_name_field; }
            set {
                _pl_anm_previous_name_field = value;
                OnPropertyChanged("PL_ANM_previous_name_field");
            }
        }
        private string _pl_anm_next_name_field;
        public string PL_ANM_next_name_field {
            get { return _pl_anm_next_name_field; }
            set {
                _pl_anm_next_name_field = value;
                OnPropertyChanged("PL_ANM_next_name_field");
            }
        }

        private string _pl_anm_dmg_name_field;
        public string PL_ANM_DMG_name_field {
            get { return _pl_anm_dmg_name_field; }
            set {
                _pl_anm_dmg_name_field = value;
                OnPropertyChanged("PL_ANM_DMG_name_field");
            }
        }

        private int _type_field;
        public int Type_field {
            get { return _type_field; }
            set {
                _type_field = value;
                OnPropertyChanged("Type_field");
            }
        }

        private int _direction_field;
        public int Direction_field {
            get { return _direction_field; }
            set {
                _direction_field = value;
                OnPropertyChanged("Direction_field");
            }
        }

        private int _interpolation_field;
        public int Interpolation_field {
            get { return _interpolation_field; }
            set {
                _interpolation_field = value;
                OnPropertyChanged("Interpolation_field");
            }
        }

        private int _trigger_condition_1_field;
        public int Trigger_condition_1_field {
            get { return _trigger_condition_1_field; }
            set {
                _trigger_condition_1_field = value;
                OnPropertyChanged("Trigger_condition_1_field");
            }
        }
        private int _trigger_condition_2_field;
        public int Trigger_condition_2_field {
            get { return _trigger_condition_2_field; }
            set {
                _trigger_condition_2_field = value;
                OnPropertyChanged("Trigger_condition_2_field");
            }
        }
        private int _link_condition_field;
        public int Link_condition_field {
            get { return _link_condition_field; }
            set {
                _link_condition_field = value;
                OnPropertyChanged("Link_condition_field");
            }
        }
        private bool _enableFaceAnim_field;
        public bool EnableFaceAnim_field {
            get { return _enableFaceAnim_field; }
            set {
                _enableFaceAnim_field = value;
                OnPropertyChanged("EnableFaceAnim_field");
            }
        }
        private bool _disableInterpolation_field;
        public bool DisableInterpolation_field {
            get { return _disableInterpolation_field; }
            set {
                _disableInterpolation_field = value;
                OnPropertyChanged("DisableInterpolation_field");
            }
        }
        private bool _enableCubeMan_field;
        public bool EnableCubeMan_field {
            get { return _enableCubeMan_field; }
            set {
                _enableCubeMan_field = value;
                OnPropertyChanged("EnableCubeMan_field");
            }
        }
        private bool _enableAnimPosFix_field;
        public bool EnableAnimPosFix_field {
            get { return _enableAnimPosFix_field; }
            set {
                _enableAnimPosFix_field = value;
                OnPropertyChanged("EnableAnimPosFix_field");
            }
        }
        private bool _enableBackwardFacingFix_field;
        public bool EnableBackwardFacingFix_field {
            get { return _enableBackwardFacingFix_field; }
            set {
                _enableBackwardFacingFix_field = value;
                OnPropertyChanged("EnableBackwardFacingFix_field");
            }
        }
        private Int16 _press_start_field;
        public Int16 Press_start_field {
            get { return _press_start_field; }
            set {
                _press_start_field = value;
                OnPropertyChanged("Press_start_field");
            }
        }
        private Int16 _press_end_field;
        public Int16 Press_end_field {
            get { return _press_end_field; }
            set {
                _press_end_field = value;
                OnPropertyChanged("Press_end_field");
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
        private PRMFunction_Model _selectedFunction;
        public PRMFunction_Model SelectedFunction {
            get { return _selectedFunction; }
            set {
                _selectedFunction = value;
                if (value is not null) {

                    StringParam_field = value.StringParam;
                    FunctionTiming_field = value.FunctionTiming;
                    FunctionID_field = value.FunctionID;
                    FunctionParam1_field = value.FunctionParam1;
                    FunctionParam2_field = value.FunctionParam2;
                    FunctionParam3_field = value.FunctionParam3;
                    FunctionParam4_field = value.FunctionParam4;
                    DamageCode_field = value.DamageCode;
                    DamageHitEffectID_field = value.DamageHitEffectID;
                    DamageHitSoundID_field = value.DamageHitSoundID;
                    DamageCondition_field = value.DamageCondition;
                    DamageHitFrequency_field = value.DamageHitFrequency;
                    DamageFreezeTime_field = value.DamageFreezeTime;
                    DamageAmount_field = value.DamageAmount;
                    DamageHorizontalPush_field = value.DamageHorizontalPush;
                    DamageVerticalPush_field = value.DamageVerticalPush;
                    DamageHitAmount_field = value.DamageHitAmount;
                    DamageGuard_field = value.DamageGuard;
                }
                OnPropertyChanged("SelectedFunction");
            }
        }

        private int _selectedFunctionIndex;
        public int SelectedFunctionIndex {
            get { return _selectedFunctionIndex; }
            set {
                _selectedFunctionIndex = value;
                OnPropertyChanged("SelectedFunctionIndex");
            }
        }
        private string _stringParam_field;
        public string StringParam_field {
            get { return _stringParam_field; }
            set {
                _stringParam_field = value;
                OnPropertyChanged("StringParam_field");
            }
        }
        private UInt16 _functionTiming_field;
        public UInt16 FunctionTiming_field {
            get { return _functionTiming_field; }
            set {
                _functionTiming_field = value;
                OnPropertyChanged("FunctionTiming_field");
            }
        }
        private Int16 _functionID_field;
        public Int16 FunctionID_field {
            get { return _functionID_field; }
            set {
                _functionID_field = value;
                OnPropertyChanged("FunctionID_field");
            }
        }
        private Int16 _functionParam1_field;
        public Int16 FunctionParam1_field {
            get { return _functionParam1_field; }
            set {
                _functionParam1_field = value;
                OnPropertyChanged("FunctionParam1_field");
            }
        }
        private Int16 _functionParam2_field;
        public Int16 FunctionParam2_field {
            get { return _functionParam2_field; }
            set {
                _functionParam2_field = value;
                OnPropertyChanged("FunctionParam2_field");
            }
        }
        private Int16 _functionParam3_field;
        public Int16 FunctionParam3_field {
            get { return _functionParam3_field; }
            set {
                _functionParam3_field = value;
                OnPropertyChanged("FunctionParam3_field");
            }
        }
        private float _functionParam4_field;
        public float FunctionParam4_field {
            get { return _functionParam4_field; }
            set {
                _functionParam4_field = value;
                OnPropertyChanged("FunctionParam4_field");
            }
        }
        private string _damageCode_field;
        public string DamageCode_field {
            get { return _damageCode_field; }
            set {
                _damageCode_field = value;
                OnPropertyChanged("DamageCode_field");
            }
        }
        private Int16 _damageHitEffectID_field;
        public Int16 DamageHitEffectID_field {
            get { return _damageHitEffectID_field; }
            set {
                _damageHitEffectID_field = value;
                OnPropertyChanged("DamageHitEffectID_field");
            }
        }
        private Int16 _damageHitSoundID_field;
        public Int16 DamageHitSoundID_field {
            get { return _damageHitSoundID_field; }
            set {
                _damageHitSoundID_field = value;
                OnPropertyChanged("DamageHitSoundID_field");
            }
        }
        private Int16 _damageCondition_field;
        public Int16 DamageCondition_field {
            get { return _damageCondition_field; }
            set {
                _damageCondition_field = value;
                OnPropertyChanged("DamageCondition_field");
            }
        }
        private Int16 _damageHitFrequency_field;
        public Int16 DamageHitFrequency_field {
            get { return _damageHitFrequency_field; }
            set {
                _damageHitFrequency_field = value;
                OnPropertyChanged("DamageHitFrequency_field");
            }
        }
        private Int16 _damageFreezeTime_field;
        public Int16 DamageFreezeTime_field {
            get { return _damageFreezeTime_field; }
            set {
                _damageFreezeTime_field = value;
                OnPropertyChanged("DamageFreezeTime_field");
            }
        }
        private float _damageAmount_field;
        public float DamageAmount_field {
            get { return _damageAmount_field; }
            set {
                _damageAmount_field = value;
                OnPropertyChanged("DamageAmount_field");
            }
        }
        private float _damageHorizontalPush_field;
        public float DamageHorizontalPush_field {
            get { return _damageHorizontalPush_field; }
            set {
                _damageHorizontalPush_field = value;
                OnPropertyChanged("DamageHorizontalPush_field");
            }
        }
        private float _damageVerticalPush_field;
        public float DamageVerticalPush_field {
            get { return _damageVerticalPush_field; }
            set {
                _damageVerticalPush_field = value;
                OnPropertyChanged("DamageVerticalPush_field");
            }
        }

        private Int16 _damageHitAmount_field;
        public Int16 DamageHitAmount_field {
            get { return _damageHitAmount_field; }
            set {
                _damageHitAmount_field = value;
                OnPropertyChanged("DamageHitAmount_field");
            }
        }
        private float _damageGuard_field;
        public float DamageGuard_field {
            get { return _damageGuard_field; }
            set {
                _damageGuard_field = value;
                OnPropertyChanged("DamageGuard_field");
            }
        }

        public byte[] fileByte;
        public string filePath;


        public PRMEditorViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            BinaryList = new ObservableCollection<PRMEditorModel>();
            VerList = new ObservableCollection<PRMVER_Model>();
            FUNC_List = new ObservableCollection<string>();
            COND_List = new ObservableCollection<string>();
            COND2_List = new ObservableCollection<string>();
            LINKCOND_List = new ObservableCollection<string>();
            DMGCOND_List = new ObservableCollection<string>();
            PL_ANM_List = new ObservableCollection<PRM_PL_ANM_Model>();
            FunctionList = new ObservableCollection<PRMFunction_Model>();
            ProjectileList = new ObservableCollection<PRMProjectile_Model>();
            CollisionList = new ObservableCollection<PRMCollision_Model>();
            MovementList = new ObservableCollection<PRMEtc_Model>();
            filePath = "";

            // FUNCT
            for (int x = 0; x < Program.ME_NSC_LIST.Length; x++) FUNC_List.Add(x.ToString() + " = " + Program.ME_NSC_LIST[x]);

            // COND
            for (int x = 0; x < Program.COND.Length; x++) COND_List.Add(Program.COND[x]);
            for (int x = Program.COND.Length; x < 256; x++) COND_List.Add(COND_List.Count.ToString() + " = ???");

            //COND2
            for (int x = 0; x < 256; x++) COND2_List.Add(COND2_List.Count.ToString() + " = ???");

            // LINK_COND
            for (int x = 0; x < Program.LINK_COND.Length; x++) LINKCOND_List.Add(Program.LINK_COND[x]);
            for (int x = Program.LINK_COND.Length; x < 256; x++) LINKCOND_List.Add((LINKCOND_List.Count - 1).ToString() + " = ???");

            // DMG COND
            for (int x = 0; x < Program.DMGCOND.Length; x++) DMGCOND_List.Add(Program.DMGCOND[x]);


        }

        public void Clear() {
            BinaryList.Clear();
            FunctionList.Clear();
            PL_ANM_List.Clear();
            ProjectileList.Clear();
            VerList.Clear();
            CollisionList.Clear();
            MovementList.Clear();
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

                int EntryCount = BinaryReader.b_ReadIntRev(FileBytes, 36) - 1;
                int Index3 = 128;
                for (int x = 0; x < EntryCount; x++) {
                    BinaryList.Add(new PRMEditorModel());
                }
                for (int x = 0; x < EntryCount; x++) {
                    string path = BinaryReader.b_ReadString(FileBytes, Index3);
                    BinaryList[x].BinPath = path;
                    Index3 = Index3 + path.Length + 1;
                }
                Index3++;
                List<string> binName = new List<string>();
                for (int x = 0; x < EntryCount + 2; x++) {
                    string name = BinaryReader.b_ReadString(FileBytes, Index3);
                    if (name != "Page0" && name != "index")
                        binName.Add(name);
                    Index3 = Index3 + name.Length + 1;
                }
                for (int x = 0; x < EntryCount; x++) {
                    BinaryList[x].BinName = binName[x];
                }

                int StartOfFile = 0x28 + BinaryReader.b_ReadIntRev(FileBytes, 16) + 0x0C;
                for (int x = 0; x < EntryCount; x++) {
                    int binarySize = BinaryReader.b_ReadIntRev(FileBytes, StartOfFile) + 0x04;
                    BinaryList[x].BinaryData = BinaryReader.b_ReadByteArray(FileBytes, StartOfFile, binarySize);
                    StartOfFile += binarySize + 0x0C;
                }

                for (int x = 0; x < BinaryList.Count; x++) {
                    string cutBineName = BinaryList[x].BinName.Substring(BinaryList[x].BinName.IndexOf("prm_"));
                    switch (cutBineName) {
                        case "prm_mot":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_motcmn":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_awa":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_awa2":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_boss":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_boss01":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_boss02":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_boss03":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_boss04":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_upgrade01":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_upgrade02":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_upgrade03":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_spl":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_skl":
                            GenerateVer(BinaryList[x]);
                            break;
                        case "prm_etc":
                            GenerateMovement(BinaryList[x]);
                            break;
                        case "prm_sklslot":
                            GenerateProjectile(BinaryList[x]);
                            break;
                        case "prm_hit":
                            GenerateCollision(BinaryList[x]);
                            break;
                    }
                }

            }
        }

        public void GenerateVer(PRMEditorModel binary) {
            PRMVER_Model VerEntry = new PRMVER_Model();
            VerEntry.BinName = binary.BinName;
            VerEntry.BinPath = binary.BinPath;
            if (VerList.Count == 0)
                CharacodeBinName_field = binary.BinName.Substring(0, binary.BinName.IndexOf("prm_"));
            ObservableCollection<PRM_PL_ANM_Model> plAnmList = new ObservableCollection<PRM_PL_ANM_Model>();
            int planm_entryCount = BinaryReader.b_ReadInt(binary.BinaryData, 0x34);
            int plAnmStartOffset = 0x44;
            for (int i = 0; i < planm_entryCount; i++) {
                PRM_PL_ANM_Model planm_entry = new PRM_PL_ANM_Model();
                planm_entry.PL_ANM_current_name = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset);
                planm_entry.PL_ANM_animation = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset + 0x20);
                int funcCount = BinaryReader.b_ReadInt(binary.BinaryData, plAnmStartOffset + 0x50);
                planm_entry.Interpolation = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x54);
                planm_entry.Type = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x56);
                planm_entry.EnableCubeMan = (BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x58) == 1);
                planm_entry.EnableFaceAnim = (BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x5A) == 2);
                planm_entry.EnableBackwardFacingFix = (BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x5C) == 1);
                planm_entry.DisableInterpolation = (BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x5E) == 1);
                planm_entry.EnableAnimPosFix = (BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x60) == 1);
                planm_entry.Direction = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x68);
                planm_entry.Link_condition = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x6A);
                planm_entry.Trigger_condition_1 = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x6C);
                planm_entry.Press_start = (Int16)BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x6E);
                planm_entry.Press_end = (Int16)BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x70);
                planm_entry.Trigger_condition_2 = BinaryReader.b_ReadIntFromTwoBytes(binary.BinaryData, plAnmStartOffset + 0x72);
                planm_entry.PL_ANM_previous_name = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset + 0x74);
                planm_entry.PL_ANM_next_name = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset + 0x94);
                planm_entry.PL_ANM_DMG_name = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset + 0xB4);
                plAnmStartOffset += 0xD4;
                planm_entry.FunctionList = new ObservableCollection<PRMFunction_Model>();
                for (int func = 0; func < funcCount; func++) {
                    string check_dmg = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset + 0x40);
                    bool dmg_entry = false;
                    if ((check_dmg.Contains("SKL_ATK") ||
                        check_dmg.Contains("ID") ||
                        check_dmg.Contains("SPSKILL_END") ||
                        check_dmg.Contains("DMG") ||
                        check_dmg.Contains("DAMAGE")) &&
                        !check_dmg.Contains("PL_ANM")) {
                        dmg_entry = true;
                    }

                    PRMFunction_Model funcEntry = new PRMFunction_Model();
                    funcEntry.StringParam = BinaryReader.b_ReadString(binary.BinaryData, plAnmStartOffset);
                    funcEntry.FunctionTiming = BinaryReader.b_ReadUInt16(binary.BinaryData, plAnmStartOffset + 0x20);
                    funcEntry.FunctionID = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x22);
                    funcEntry.FunctionParam1 = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x24);
                    funcEntry.FunctionParam2 = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x26);
                    funcEntry.FunctionParam3 = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x28);
                    funcEntry.FunctionParam4 = BinaryReader.b_ReadFloat(binary.BinaryData, plAnmStartOffset + 0x2C);
                    if (dmg_entry) {
                        funcEntry.DamageCode = check_dmg;
                        funcEntry.DamageHitEffectID = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x82);
                        funcEntry.DamageHitSoundID = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x84);
                        funcEntry.DamageCondition = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x86);
                        funcEntry.DamageAmount = BinaryReader.b_ReadFloat(binary.BinaryData, plAnmStartOffset + 0x88);
                        funcEntry.DamageGuard = BinaryReader.b_ReadFloat(binary.BinaryData, plAnmStartOffset + 0x8C);
                        funcEntry.DamageHorizontalPush = BinaryReader.b_ReadFloat(binary.BinaryData, plAnmStartOffset + 0x90);
                        funcEntry.DamageVerticalPush = BinaryReader.b_ReadFloat(binary.BinaryData, plAnmStartOffset + 0x94);
                        funcEntry.DamageHitAmount = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x98);
                        funcEntry.DamageHitFrequency = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x9A);
                        funcEntry.DamageFreezeTime = BinaryReader.b_ReadInt16(binary.BinaryData, plAnmStartOffset + 0x9C);

                        plAnmStartOffset += 0xA0;
                    } else {
                        funcEntry.DamageCode = "";
                        funcEntry.DamageHitEffectID = 0;
                        funcEntry.DamageHitSoundID = -1;
                        funcEntry.DamageCondition = 0;
                        funcEntry.DamageAmount = 1;
                        funcEntry.DamageGuard = -1;
                        funcEntry.DamageHorizontalPush = 1;
                        funcEntry.DamageVerticalPush = 1;
                        funcEntry.DamageHitAmount = 1;
                        funcEntry.DamageHitFrequency = 0;
                        funcEntry.DamageFreezeTime = 0;
                        plAnmStartOffset += 0x40;

                    }
                    planm_entry.FunctionList.Add(funcEntry);
                }
                if (planm_entry.PL_ANM_current_name != "")
                    plAnmList.Add(planm_entry);
            }
            VerEntry.PL_ANM_Sections = plAnmList;
            VerList.Add(VerEntry);
        }

        public void GenerateProjectile(PRMEditorModel binary) {
            int entryCount = BinaryReader.b_ReadIntRev(binary.BinaryData, 0) / 0x81;
            for (int i = 0; i < entryCount - 1; i++) {
                int _ptr = 0x04 + (i * 0x81);
                PRMProjectile_Model ProjectileEntry = new PRMProjectile_Model();
                ProjectileEntry.ProjectileName = BinaryReader.b_ReadString(binary.BinaryData, _ptr);
                ProjectileEntry.SkillFileName = BinaryReader.b_ReadString(binary.BinaryData, _ptr + 0x40);
                ProjectileEntry.SkillEntryName = BinaryReader.b_ReadString(binary.BinaryData, _ptr + 0x60);
                ProjectileEntry.Unk = binary.BinaryData[_ptr + 0x80];
                ProjectileList.Add(ProjectileEntry);
            }
        }

        public void GenerateCollision(PRMEditorModel binary) {
            int entryCount = BinaryReader.b_ReadInt(binary.BinaryData, 4);
            for (int i = 0; i < entryCount; i++) {
                int _ptr = 0x08 + (i * 0x5C);
                PRMCollision_Model CollisionEntry = new PRMCollision_Model();
                CollisionEntry.Type = BinaryReader.b_ReadInt(binary.BinaryData, _ptr);
                CollisionEntry.State = BinaryReader.b_ReadInt(binary.BinaryData, _ptr + 0x04);
                CollisionEntry.BoneEnabler = Convert.ToBoolean(BinaryReader.b_ReadInt(binary.BinaryData, _ptr + 0x08));
                CollisionEntry.BoneName = BinaryReader.b_ReadString(binary.BinaryData, _ptr + 0x10);
                CollisionEntry.HurtboxRadius = BinaryReader.b_ReadUInt16(binary.BinaryData, _ptr + 0x50);
                CollisionEntry.Hurtbox_Y_Pos = BinaryReader.b_ReadInt16(binary.BinaryData, _ptr + 0x54);
                CollisionEntry.Hurtbox_Z_Pos = BinaryReader.b_ReadInt16(binary.BinaryData, _ptr + 0x58);
                CollisionList.Add(CollisionEntry);
            }
        }
        public void GenerateMovement(PRMEditorModel binary) {
            int entryCount = BinaryReader.b_ReadIntRev(binary.BinaryData, 0) / 0x20;
            for (int i = 0; i < entryCount; i++) {
                int _ptr = 0x04 + (i * 0x20);
                PRMEtc_Model MovementEntry = new PRMEtc_Model();
                MovementEntry.MovementItemIndex = i;
                MovementEntry.FrameActionUnlock = BinaryReader.b_ReadUInt16(binary.BinaryData, _ptr);
                MovementEntry.ActionLength = BinaryReader.b_ReadUInt16(binary.BinaryData, _ptr + 0x02);
                MovementEntry.Unk1 = BinaryReader.b_ReadUInt16(binary.BinaryData, _ptr + 0x06);
                MovementEntry.CircleVelocity = BinaryReader.b_ReadFloat(binary.BinaryData, _ptr + 0x08);
                MovementEntry.Unk2 = BinaryReader.b_ReadFloat(binary.BinaryData, _ptr + 0x0C);
                MovementEntry.CircleVelocityStrength = BinaryReader.b_ReadFloat(binary.BinaryData, _ptr + 0x10);
                MovementEntry.MovementFrequency = BinaryReader.b_ReadUInt16(binary.BinaryData, _ptr + 0x16);
                MovementEntry.ForwardVelocity = BinaryReader.b_ReadFloat(binary.BinaryData, _ptr + 0x18);
                MovementList.Add(MovementEntry);
            }
        }
        public void RemoveMovementEntry() {
            if (SelectedMovement is not null) {
                for (int c = SelectedMovement.MovementItemIndex; c < MovementList.Count; c++) {
                    MovementList[c].MovementItemIndex = c - 1;
                }
                MovementList.Remove(SelectedMovement);
            } else {
                ModernWpf.MessageBox.Show("Select Movement Entry!");
            }
        }
        public void SaveMovementEntry() {
            if (SelectedMovement is not null) {

                MovementList[SelectedMovementIndex].FrameActionUnlock = FrameActionUnlock_field;
                MovementList[SelectedMovementIndex].ActionLength = ActionLength_field;
                MovementList[SelectedMovementIndex].Unk1 = Unk1_field;
                MovementList[SelectedMovementIndex].CircleVelocity = CircleVelocity_field;
                MovementList[SelectedMovementIndex].Unk2 = Unk2_field;
                MovementList[SelectedMovementIndex].CircleVelocityStrength = CircleVelocityStrength_field;
                MovementList[SelectedMovementIndex].MovementFrequency = MovementFrequency_field;
                MovementList[SelectedMovementIndex].ForwardVelocity = ForwardVelocity_field;
                ModernWpf.MessageBox.Show("Movement was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select Movement Entry!");
            }
        }
        public void AddDupMovementEntry() {
            PRMEtc_Model MovementEntry = new PRMEtc_Model();
            if (SelectedMovement is not null) {
                MovementEntry = (PRMEtc_Model)MovementList[SelectedMovementIndex].Clone();

            } else {
                MovementEntry.FrameActionUnlock = 0;
                MovementEntry.ActionLength = 0;
                MovementEntry.Unk1 = 0;
                MovementEntry.CircleVelocity = 0;
                MovementEntry.Unk2 = 0;
                MovementEntry.CircleVelocityStrength = 0;
                MovementEntry.MovementFrequency = 0;
                MovementEntry.ForwardVelocity = 0;
            }
            MovementEntry.MovementItemIndex = MovementList.Count;
            MovementList.Add(MovementEntry);
            ModernWpf.MessageBox.Show("Movement was added!");
        }
        public void RemoveProjectileEntry() {
            if (SelectedProjectile is not null) {
                ProjectileList.Remove(SelectedProjectile);
            } else {
                ModernWpf.MessageBox.Show("Select Projectile entry!");
            }
        }
        public void SaveProjectileEntry() {
            if (SelectedProjectile is not null) {

                ProjectileList[SelectedProjectileIndex].ProjectileName = ProjectileName_field;
                ProjectileList[SelectedProjectileIndex].SkillFileName = SkillFileName_field;
                ProjectileList[SelectedProjectileIndex].SkillEntryName = SkillEntryName_field;
                ProjectileList[SelectedProjectileIndex].Unk = Unk_field;
                ModernWpf.MessageBox.Show("Projectile was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select Projectile entry!");
            }
        }
        public void AddDupProjectileEntry() {
            PRMProjectile_Model ProjectileEntry = new PRMProjectile_Model();
            if (SelectedProjectile is not null) {
                ProjectileEntry = (PRMProjectile_Model)ProjectileList[SelectedProjectileIndex].Clone();

            } else {
                ProjectileEntry.ProjectileName = "COMBO";
                ProjectileEntry.SkillFileName = "";
                ProjectileEntry.SkillEntryName = "";
                ProjectileEntry.Unk = 0;
            }
            ProjectileList.Add(ProjectileEntry);
            ModernWpf.MessageBox.Show("Projectile was added!");
        }
        public void RemoveCollisionEntry() {
            if (SelectedCollision is not null) {
                CollisionList.Remove(SelectedCollision);
            } else {
                ModernWpf.MessageBox.Show("Select Collision entry!");
            }
        }
        public void SaveCollisionEntry() {
            if (SelectedCollision is not null) {
                CollisionList[SelectedCollisionIndex].Type = TypeCollision_field;
                CollisionList[SelectedCollisionIndex].State = StateCollision_field;
                CollisionList[SelectedCollisionIndex].BoneEnabler = !BoneEnablerCollision_field;
                CollisionList[SelectedCollisionIndex].BoneName = BoneNameCollision_field;
                CollisionList[SelectedCollisionIndex].HurtboxRadius = HurtboxRadiusCollision_field;
                CollisionList[SelectedCollisionIndex].Hurtbox_Y_Pos = Hurtbox_Y_PosCollision_field;
                CollisionList[SelectedCollisionIndex].Hurtbox_Z_Pos = Hurtbox_Z_PosCollision_field;
                ModernWpf.MessageBox.Show("Collision was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select Collision entry!");
            }
        }
        public void AddDupCollisionEntry() {
            PRMCollision_Model CollisionEntry = new PRMCollision_Model();
            if (SelectedCollision is not null) {
                CollisionEntry = (PRMCollision_Model)CollisionList[SelectedCollisionIndex].Clone();

            } else {
                CollisionEntry.Type = 0;
                CollisionEntry.State = 2;
                CollisionEntry.BoneEnabler = true;
                CollisionEntry.BoneName = "";
                CollisionEntry.HurtboxRadius = 45;
                CollisionEntry.Hurtbox_Y_Pos = 0;
                CollisionEntry.Hurtbox_Z_Pos = 0;
            }
            CollisionList.Add(CollisionEntry);
            ModernWpf.MessageBox.Show("Collision was added!");
        }

        public void RemoveVerEntry() {
            if (SelectedVer is not null) {
                VerList.Remove(SelectedVer);
            } else {
                ModernWpf.MessageBox.Show("Select Ver Entry!");
            }
        }
        public void SaveVerEntry() {
            if (SelectedVer is not null) {
                if (SelectedVerTypeIndex > -1) {
                    string prm_binName = "";
                    switch (SelectedVerTypeIndex) {
                        case 0:
                            prm_binName = "prm_mot";
                            break;
                        case 3:
                            prm_binName = "prm_motcmn";
                            break;
                        case 1:
                            prm_binName = "prm_awa";
                            break;
                        case 2:
                            prm_binName = "prm_awa2";
                            break;
                        case 9:
                            prm_binName = "prm_boss";
                            break;
                        case 10:
                            prm_binName = "prm_boss01";
                            break;
                        case 11:
                            prm_binName = "prm_boss02";
                            break;
                        case 12:
                            prm_binName = "prm_boss03";
                            break;
                        case 13:
                            prm_binName = "prm_boss04";
                            break;
                        case 6:
                            prm_binName = "prm_upgrade01";
                            break;
                        case 7:
                            prm_binName = "prm_upgrade02";
                            break;
                        case 8:
                            prm_binName = "prm_upgrade03";
                            break;
                        case 5:
                            prm_binName = "prm_spl";
                            break;
                        case 4:
                            prm_binName = "prm_skl";
                            break;
                    }
                    bool exist = false;
                    for (int i = 0; i < VerList.Count; i++) {
                        if (VerList[i].BinName == VerList[i].BinName.Substring(0, VerList[SelectedVerIndex].BinName.IndexOf("prm_")) + prm_binName) {
                            exist = true;
                        }
                    }
                    if (!exist) {
                        VerList[SelectedVerIndex].BinName = VerList[SelectedVerIndex].BinName.Substring(0, VerList[SelectedVerIndex].BinName.IndexOf("prm_")) + prm_binName;
                        VerList[SelectedVerIndex].BinPath = "D:/next5/char_hi/param/player/Converter/bin/" + VerList[SelectedVerIndex].BinName.Substring(0, VerList[SelectedVerIndex].BinName.IndexOf("prm_")) + "/" + VerList[SelectedVerIndex].BinName.Substring(0, VerList[SelectedVerIndex].BinName.IndexOf("prm_")) + prm_binName + ".bin";
                        ModernWpf.MessageBox.Show("Ver Entry was saved!");
                    } else
                        ModernWpf.MessageBox.Show("That Ver Entry already exist!");
                }
            } else {
                ModernWpf.MessageBox.Show("Select Ver Entry!");
            }
        }
        public void AddVerEntry() {
            if (SelectedVerTypeIndex > -1) {
                string prm_binName = "";
                switch (SelectedVerTypeIndex) {
                    case 0:
                        prm_binName = "prm_mot";
                        break;
                    case 3:
                        prm_binName = "prm_motcmn";
                        break;
                    case 1:
                        prm_binName = "prm_awa";
                        break;
                    case 2:
                        prm_binName = "prm_awa2";
                        break;
                    case 9:
                        prm_binName = "prm_boss";
                        break;
                    case 10:
                        prm_binName = "prm_boss01";
                        break;
                    case 11:
                        prm_binName = "prm_boss02";
                        break;
                    case 12:
                        prm_binName = "prm_boss03";
                        break;
                    case 13:
                        prm_binName = "prm_boss04";
                        break;
                    case 6:
                        prm_binName = "prm_upgrade01";
                        break;
                    case 7:
                        prm_binName = "prm_upgrade02";
                        break;
                    case 8:
                        prm_binName = "prm_upgrade03";
                        break;
                    case 5:
                        prm_binName = "prm_spl";
                        break;
                    case 4:
                        prm_binName = "prm_skl";
                        break;
                }
                bool exist = false;
                for (int i = 0; i < VerList.Count; i++) {
                    if (VerList[i].BinName == VerList[i].BinName.Substring(0, VerList[SelectedVerIndex].BinName.IndexOf("prm_")) + prm_binName) {
                        exist = true;
                    }
                }
                if (!exist) {
                    PRMVER_Model VerEntry = new PRMVER_Model();
                    if (VerList.Count > 0) {
                        VerEntry.BinName = VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + prm_binName;
                        VerEntry.BinPath = "D:/next5/char_hi/param/player/Converter/bin/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + prm_binName + ".bin";
                        VerEntry.PL_ANM_Sections = new ObservableCollection<PRM_PL_ANM_Model>();
                    } else {
                        VerEntry.BinName = "1abc" + prm_binName;
                        VerEntry.BinPath = "D:/next5/char_hi/param/player/Converter/bin/1abc/1abc" + prm_binName + ".bin";
                        VerEntry.PL_ANM_Sections = new ObservableCollection<PRM_PL_ANM_Model>();

                    }
                    VerList.Add(VerEntry);
                    ModernWpf.MessageBox.Show("Ver Entry was added!");
                } else {
                    ModernWpf.MessageBox.Show("Ver Entry with that type already exist in file.");

                }
            } else {
                ModernWpf.MessageBox.Show("Select Ver Entry type.");

            }
        }

        public void ChangeCharacode() {
            if (VerList.Count > 0) {
                for (int i = 0; i < VerList.Count; i++) {
                    string prm_bin = VerList[i].BinName.Substring(VerList[i].BinName.IndexOf("prm_"));
                    VerList[i].BinName = CharacodeBinName_field + prm_bin;
                    VerList[i].BinPath = "D:/next5/char_hi/param/player/Converter/bin/" + CharacodeBinName_field + "/" + CharacodeBinName_field + prm_bin + ".bin";
                }
                // Add Projectile, Collision and Movement code
                ModernWpf.MessageBox.Show("Characode was changed successfully!");
            } else {
                ModernWpf.MessageBox.Show("No Ver Entries");
            }
        }

        public void RemovePLANMEntry() {
            if (SelectedPL_ANM is not null) {
                PL_ANM_List.Remove(SelectedPL_ANM);
            } else {
                ModernWpf.MessageBox.Show("Select PL_ANM entry!");
            }
        }
        public void SavePLANMEntry() {
            if (SelectedPL_ANM is not null) {

                PL_ANM_List[SelectedPL_ANMIndex].PL_ANM_current_name = PL_ANM_current_name_field;
                PL_ANM_List[SelectedPL_ANMIndex].PL_ANM_animation = PL_ANM_animation_field;
                PL_ANM_List[SelectedPL_ANMIndex].PL_ANM_previous_name = PL_ANM_previous_name_field;
                PL_ANM_List[SelectedPL_ANMIndex].PL_ANM_next_name = PL_ANM_next_name_field;
                PL_ANM_List[SelectedPL_ANMIndex].PL_ANM_DMG_name = PL_ANM_DMG_name_field;
                PL_ANM_List[SelectedPL_ANMIndex].Type = Type_field;
                PL_ANM_List[SelectedPL_ANMIndex].Direction = Direction_field;
                PL_ANM_List[SelectedPL_ANMIndex].Interpolation = Interpolation_field;
                PL_ANM_List[SelectedPL_ANMIndex].Trigger_condition_1 = Trigger_condition_1_field;
                PL_ANM_List[SelectedPL_ANMIndex].Trigger_condition_2 = Trigger_condition_2_field;
                PL_ANM_List[SelectedPL_ANMIndex].Link_condition = Link_condition_field - 1;
                PL_ANM_List[SelectedPL_ANMIndex].DisableInterpolation = DisableInterpolation_field;
                PL_ANM_List[SelectedPL_ANMIndex].EnableCubeMan = EnableCubeMan_field;
                PL_ANM_List[SelectedPL_ANMIndex].EnableFaceAnim = EnableFaceAnim_field;
                PL_ANM_List[SelectedPL_ANMIndex].EnableAnimPosFix = EnableAnimPosFix_field;
                PL_ANM_List[SelectedPL_ANMIndex].EnableBackwardFacingFix = EnableBackwardFacingFix_field;
                PL_ANM_List[SelectedPL_ANMIndex].Press_start = Press_start_field;
                PL_ANM_List[SelectedPL_ANMIndex].Press_end = Press_end_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void AddDupPLANMEntry() {
            PRM_PL_ANM_Model PlAnmEntry = new PRM_PL_ANM_Model();
            if (SelectedPL_ANM is not null) {
                PlAnmEntry = (PRM_PL_ANM_Model)PL_ANM_List[SelectedPL_ANMIndex].Clone();

            } else {
                PlAnmEntry.PL_ANM_current_name = "PL_ANM_";
                PlAnmEntry.PL_ANM_animation = "empty";
                PlAnmEntry.PL_ANM_previous_name = "PL_ANM_ANY";
                PlAnmEntry.PL_ANM_next_name = "";
                PlAnmEntry.PL_ANM_DMG_name = "";
                PlAnmEntry.Type = 3;
                PlAnmEntry.Direction = 1;
                PlAnmEntry.Interpolation = 0;
                PlAnmEntry.Trigger_condition_1 = 0;
                PlAnmEntry.Trigger_condition_2 = 0;
                PlAnmEntry.Link_condition = 0;
                PlAnmEntry.DisableInterpolation = false;
                PlAnmEntry.EnableCubeMan = false;
                PlAnmEntry.EnableFaceAnim = false;
                PlAnmEntry.EnableAnimPosFix = false;
                PlAnmEntry.EnableBackwardFacingFix = false;
                PlAnmEntry.Press_start = -1;
                PlAnmEntry.Press_end = -1;
                PlAnmEntry.FunctionList = new ObservableCollection<PRMFunction_Model>();
            }
            PL_ANM_List.Add(PlAnmEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void PastePLANMEntry() {
            if (SelectedVer is not null) {
                if (Clipboard.GetText() != "" || Clipboard.GetText().Length < 0xD4) {
                    byte[] binaryData = BinaryReader.b_StringToBytes(Clipboard.GetText());
                    int plAnmStartOffset = 0x00;
                    PRM_PL_ANM_Model planm_entry = new PRM_PL_ANM_Model();
                    planm_entry.PL_ANM_current_name = BinaryReader.b_ReadString(binaryData, plAnmStartOffset);
                    planm_entry.PL_ANM_animation = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0x20);
                    int funcCount = BinaryReader.b_ReadInt(binaryData, plAnmStartOffset + 0x50);
                    planm_entry.Interpolation = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x54);
                    planm_entry.Type = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x56);
                    planm_entry.EnableCubeMan = (BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x58) == 1);
                    planm_entry.EnableFaceAnim = (BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x5A) == 2);
                    planm_entry.EnableBackwardFacingFix = (BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x5C) == 1);
                    planm_entry.DisableInterpolation = (BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x5E) == 1);
                    planm_entry.EnableAnimPosFix = (BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x60) == 1);
                    planm_entry.Direction = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x68);
                    planm_entry.Link_condition = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x6A);
                    planm_entry.Trigger_condition_1 = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x6C);
                    planm_entry.Press_start = (Int16)BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x6E);
                    planm_entry.Press_end = (Int16)BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x70);
                    planm_entry.Trigger_condition_2 = BinaryReader.b_ReadIntFromTwoBytes(binaryData, plAnmStartOffset + 0x72);
                    planm_entry.PL_ANM_previous_name = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0x74);
                    planm_entry.PL_ANM_next_name = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0x94);
                    planm_entry.PL_ANM_DMG_name = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0xB4);
                    plAnmStartOffset += 0xD4;
                    planm_entry.FunctionList = new ObservableCollection<PRMFunction_Model>();
                    for (int func = 0; func < funcCount; func++) {
                        string check_dmg = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0x40);
                        bool dmg_entry = false;
                        if ((check_dmg.Contains("SKL_ATK") ||
                            check_dmg.Contains("ID") ||
                            check_dmg.Contains("SPSKILL_END") ||
                            check_dmg.Contains("DMG") ||
                            check_dmg.Contains("DAMAGE")) &&
                            !check_dmg.Contains("PL_ANM")) {
                            dmg_entry = true;
                        }

                        PRMFunction_Model funcEntry = new PRMFunction_Model();
                        funcEntry.StringParam = BinaryReader.b_ReadString(binaryData, plAnmStartOffset);
                        funcEntry.FunctionTiming = BinaryReader.b_ReadUInt16(binaryData, plAnmStartOffset + 0x20);
                        funcEntry.FunctionID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x22);
                        funcEntry.FunctionParam1 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x24);
                        funcEntry.FunctionParam2 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x26);
                        funcEntry.FunctionParam3 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x28);
                        funcEntry.FunctionParam4 = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x2C);
                        if (dmg_entry) {
                            funcEntry.DamageCode = check_dmg;
                            funcEntry.DamageHitEffectID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x82);
                            funcEntry.DamageHitSoundID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x84);
                            funcEntry.DamageCondition = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x86);
                            funcEntry.DamageAmount = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x88);
                            funcEntry.DamageGuard = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x8C);
                            funcEntry.DamageHorizontalPush = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x90);
                            funcEntry.DamageVerticalPush = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x94);
                            funcEntry.DamageHitAmount = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x98);
                            funcEntry.DamageHitFrequency = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x9A);
                            funcEntry.DamageFreezeTime = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x9C);

                            plAnmStartOffset += 0xA0;
                        } else {
                            funcEntry.DamageCode = "";
                            funcEntry.DamageHitEffectID = 0;
                            funcEntry.DamageHitSoundID = -1;
                            funcEntry.DamageCondition = 0;
                            funcEntry.DamageAmount = 1;
                            funcEntry.DamageGuard = -1;
                            funcEntry.DamageHorizontalPush = 1;
                            funcEntry.DamageVerticalPush = 1;
                            funcEntry.DamageHitAmount = 1;
                            funcEntry.DamageHitFrequency = 0;
                            funcEntry.DamageFreezeTime = 0;
                            plAnmStartOffset += 0x40;

                        }
                        planm_entry.FunctionList.Add(funcEntry);
                    }
                    VerList[SelectedVerIndex].PL_ANM_Sections.Add(planm_entry);
                    ModernWpf.MessageBox.Show("PL_ANM entry was imported successfully!");
                } else {
                    ModernWpf.MessageBox.Show("Empty clipboard or corrupted data!");
                }
            } else {
                ModernWpf.MessageBox.Show("Select Ver Entry!");
            }
        }
        public void CopyPLANMEntry() {
            if (SelectedPL_ANM is not null) {
                byte[] copyBytes = new byte[0xD4];
                int _ptr = 0;
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.PL_ANM_current_name), _ptr);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.PL_ANM_animation), _ptr + 0x20);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList.Count), _ptr + 0x50);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Interpolation), _ptr + 0x54);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Type), _ptr + 0x56);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.EnableCubeMan), _ptr + 0x58);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.EnableFaceAnim == true ? 2 : 0), _ptr + 0x5A);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.EnableBackwardFacingFix), _ptr + 0x5C);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.DisableInterpolation), _ptr + 0x5E);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.EnableAnimPosFix), _ptr + 0x60);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, _ptr + 0x62);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Direction), _ptr + 0x68);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Link_condition), _ptr + 0x6A);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Trigger_condition_1), _ptr + 0x6C);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Press_start), _ptr + 0x6E);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Press_end), _ptr + 0x70);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.Trigger_condition_2), _ptr + 0x72);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.PL_ANM_previous_name), _ptr + 0x74);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.PL_ANM_next_name), _ptr + 0x94);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.PL_ANM_DMG_name), _ptr + 0xB4);
                _ptr += 0xD4;

                for (int func = 0; func < SelectedPL_ANM.FunctionList.Count; func++) {
                    bool dmg_entry = false;
                    copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[0x40]);
                    if (SelectedPL_ANM.FunctionList[func].DamageCode != "") {
                        dmg_entry = true;
                        copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[0x60]);
                    }
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.FunctionList[func].StringParam), _ptr);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionTiming), _ptr + 0x20);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionID), _ptr + 0x22);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionParam1), _ptr + 0x24);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionParam2), _ptr + 0x26);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionParam3), _ptr + 0x28);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].FunctionParam4), _ptr + 0x2C);
                    if (dmg_entry) {
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedPL_ANM.FunctionList[func].DamageCode), _ptr + 0x40);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageHitEffectID), _ptr + 0x82);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageHitSoundID), _ptr + 0x84);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageCondition), _ptr + 0x86);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageAmount), _ptr + 0x88);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageGuard), _ptr + 0x8C);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageHorizontalPush), _ptr + 0x90);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageVerticalPush), _ptr + 0x94);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageHitAmount), _ptr + 0x98);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageHitFrequency), _ptr + 0x9A);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedPL_ANM.FunctionList[func].DamageFreezeTime), _ptr + 0x9C);
                        _ptr += 0xA0;
                    } else
                        _ptr += 0x40;

                }
                string convertedCode = "";
                for (int i = 0; i < copyBytes.Length; i++) {
                    convertedCode = convertedCode + " " + copyBytes[i].ToString("X2");
                }
                Clipboard.SetText(convertedCode.Substring(1, convertedCode.Length - 1));
                ModernWpf.MessageBox.Show("PL_ANM entry was copied successfully!");

            } else {
                ModernWpf.MessageBox.Show("Select PL_ANM entry!");
            }
        }

        public void RemoveFunctionEntry() {
            if (SelectedVer is not null && SelectedPL_ANM is not null && SelectedFunction is not null) {
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList.Remove(SelectedFunction);
            } else {
                ModernWpf.MessageBox.Show("Select function!");
            }
        }
        public void SaveFunctionEntry() {
            if (SelectedVer is not null && SelectedPL_ANM is not null && SelectedFunction is not null) {

                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].StringParam = StringParam_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionTiming = FunctionTiming_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionID = FunctionID_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionParam1 = FunctionParam1_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionParam2 = FunctionParam2_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionParam3 = FunctionParam3_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].FunctionParam4 = FunctionParam4_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageCode = DamageCode_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageHitEffectID = DamageHitEffectID_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageHitSoundID = DamageHitSoundID_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageCondition = DamageCondition_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageHitFrequency = DamageHitFrequency_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageFreezeTime = DamageFreezeTime_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageAmount = DamageAmount_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageHorizontalPush = DamageHorizontalPush_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageVerticalPush = DamageVerticalPush_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageHitAmount = DamageHitAmount_field;
                VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].DamageGuard = DamageGuard_field;
                ModernWpf.MessageBox.Show("Function was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select function!");
            }
        }
        public void AddDupFunctionEntry() {
            PRMFunction_Model FunctionEntry = new PRMFunction_Model();
            if (SelectedVer is not null && SelectedPL_ANM is not null && SelectedFunction is not null) {
                FunctionEntry = (PRMFunction_Model)VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList[SelectedFunctionIndex].Clone();

            } else {
                FunctionEntry.StringParam = "";
                FunctionEntry.FunctionTiming = 0;
                FunctionEntry.FunctionID = 0;
                FunctionEntry.FunctionParam1 = 0;
                FunctionEntry.FunctionParam2 = 0;
                FunctionEntry.FunctionParam3 = 0;
                FunctionEntry.FunctionParam4 = 0;
                FunctionEntry.DamageCode = "";
                FunctionEntry.DamageHitEffectID = 1;
                FunctionEntry.DamageHitSoundID = -1;
                FunctionEntry.DamageCondition = 0;
                FunctionEntry.DamageHitFrequency = 0;
                FunctionEntry.DamageFreezeTime = 0;
                FunctionEntry.DamageAmount = 1;
                FunctionEntry.DamageHorizontalPush = 1;
                FunctionEntry.DamageVerticalPush = 1;
                FunctionEntry.DamageHitAmount = 1;
                FunctionEntry.DamageGuard = -1;

            }
            VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList.Add(FunctionEntry);
            ModernWpf.MessageBox.Show("Function was added!");
        }
        public void PasteFunctionEntry() {
            if (SelectedVer is not null && SelectedPL_ANM is not null) {
                if (Clipboard.GetText() != "" || Clipboard.GetText().Length < 0x40) {
                    byte[] binaryData = BinaryReader.b_StringToBytes(Clipboard.GetText());
                    int plAnmStartOffset = 0x00;
                    string check_dmg = BinaryReader.b_ReadString(binaryData, plAnmStartOffset + 0x40);
                    bool dmg_entry = false;
                    if ((check_dmg.Contains("SKL_ATK") ||
                        check_dmg.Contains("ID") ||
                        check_dmg.Contains("SPSKILL_END") ||
                        check_dmg.Contains("DMG") ||
                        check_dmg.Contains("DAMAGE")) &&
                        !check_dmg.Contains("PL_ANM")) {
                        dmg_entry = true;
                    }

                    PRMFunction_Model funcEntry = new PRMFunction_Model();
                    funcEntry.StringParam = BinaryReader.b_ReadString(binaryData, plAnmStartOffset);
                    funcEntry.FunctionTiming = BinaryReader.b_ReadUInt16(binaryData, plAnmStartOffset + 0x20);
                    funcEntry.FunctionID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x22);
                    funcEntry.FunctionParam1 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x24);
                    funcEntry.FunctionParam2 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x26);
                    funcEntry.FunctionParam3 = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x28);
                    funcEntry.FunctionParam4 = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x2C);
                    if (dmg_entry) {
                        funcEntry.DamageCode = check_dmg;
                        funcEntry.DamageHitEffectID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x82);
                        funcEntry.DamageHitSoundID = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x84);
                        funcEntry.DamageCondition = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x86);
                        funcEntry.DamageAmount = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x88);
                        funcEntry.DamageGuard = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x8C);
                        funcEntry.DamageHorizontalPush = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x90);
                        funcEntry.DamageVerticalPush = BinaryReader.b_ReadFloat(binaryData, plAnmStartOffset + 0x94);
                        funcEntry.DamageHitAmount = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x98);
                        funcEntry.DamageHitFrequency = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x9A);
                        funcEntry.DamageFreezeTime = BinaryReader.b_ReadInt16(binaryData, plAnmStartOffset + 0x9C);
                    } else {
                        funcEntry.DamageCode = "";
                        funcEntry.DamageHitEffectID = 0;
                        funcEntry.DamageHitSoundID = -1;
                        funcEntry.DamageCondition = 0;
                        funcEntry.DamageAmount = 1;
                        funcEntry.DamageGuard = -1;
                        funcEntry.DamageHorizontalPush = 1;
                        funcEntry.DamageVerticalPush = 1;
                        funcEntry.DamageHitAmount = 1;
                        funcEntry.DamageHitFrequency = 0;
                        funcEntry.DamageFreezeTime = 0;

                    }
                    VerList[SelectedVerIndex].PL_ANM_Sections[SelectedPL_ANMIndex].FunctionList.Add(funcEntry);
                    ModernWpf.MessageBox.Show("Function entry was imported successfully!");
                } else {
                    ModernWpf.MessageBox.Show("Empty clipboard or corrupted data!");
                }
            } else {
                ModernWpf.MessageBox.Show("Select PL_ANM Entry!");
            }
        }
        public void CopyFunctionEntry() {
            if (SelectedFunction is not null) {
                byte[] copyBytes = new byte[0x40];
                int _ptr = 0;
                bool dmg_entry = false;
                if (SelectedFunction.DamageCode != "") {
                    dmg_entry = true;
                    copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[0x60]);
                }
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedFunction.StringParam), _ptr);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionTiming), _ptr + 0x20);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionID), _ptr + 0x22);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionParam1), _ptr + 0x24);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionParam2), _ptr + 0x26);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionParam3), _ptr + 0x28);
                copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.FunctionParam4), _ptr + 0x2C);
                if (dmg_entry) {
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, Encoding.ASCII.GetBytes(SelectedFunction.DamageCode), _ptr + 0x40);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageHitEffectID), _ptr + 0x82);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageHitSoundID), _ptr + 0x84);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageCondition), _ptr + 0x86);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageAmount), _ptr + 0x88);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageGuard), _ptr + 0x8C);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageHorizontalPush), _ptr + 0x90);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageVerticalPush), _ptr + 0x94);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageHitAmount), _ptr + 0x98);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageHitFrequency), _ptr + 0x9A);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(SelectedFunction.DamageFreezeTime), _ptr + 0x9C);
                }


                string convertedCode = "";
                for (int i = 0; i < copyBytes.Length; i++) {
                    convertedCode = convertedCode + " " + copyBytes[i].ToString("X2");
                }
                Clipboard.SetText(convertedCode.Substring(1, convertedCode.Length - 1));
                ModernWpf.MessageBox.Show("Function entry was copied successfully!");

            } else {
                ModernWpf.MessageBox.Show("Select Function entry!");
            }
        }

        public void SaveFile() {
            if (VerList.Count > 0) {
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
            } else {
                ModernWpf.MessageBox.Show("Unable to save file. File data is empty!");
            }
        }

        public void SaveFileAs(string basepath = "") {
            if (VerList.Count > 0) {
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
            } else {
                ModernWpf.MessageBox.Show("Unable to save file. File data is empty!");
            }
        }

        public byte[] ConvertToFile() {
            // Build the header
            int totalLength4 = 0;
            int chunkCount = 0;
            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x3E, 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            for (int x6 = 0; x6 < VerList.Count; x6++) {
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, VerList[x6].BinPath);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }
            if (ProjectileList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_sklslot.bin");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }
            if (MovementList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_etc.bin");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }
            if (CollisionList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "D:/next5/char_hi/param/player/Converter/bin/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "/" + VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_hit.bin");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            for (int x4 = 0; x4 < VerList.Count; x4++) {
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, VerList[x4].BinName);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                chunkCount = VerList.Count;
            }
            if (ProjectileList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_sklslot");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                chunkCount++;
            }
            if (MovementList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_etc");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                chunkCount++;
            }
            if (CollisionList.Count > 0) {

                fileBytes36 = BinaryReader.b_AddString(fileBytes36, VerList[0].BinName.Substring(0, VerList[0].BinName.IndexOf("prm_")) + "prm_hit");
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                chunkCount++;
            }
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
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x0C]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            });

            for (int x3 = 0; x3 < chunkCount; x3++) {
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(1), 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(x3 + 1), 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(x3 + 1), 1);
            }

            int PtrSection = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(2), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(0), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(chunkCount + 1), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(3), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(0), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(chunkCount + 2), 1);
            for (int x2 = 0; x2 < chunkCount + 3; x2++) {
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(x2), 1);
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
            buffer8 = BitConverter.GetBytes(chunkCount + 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 40;
            buffer8 = BitConverter.GetBytes(PathLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 44;
            buffer8 = BitConverter.GetBytes(chunkCount + 3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 48;
            buffer8 = BitConverter.GetBytes(NameLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 52;
            buffer8 = BitConverter.GetBytes(chunkCount + 3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 56;
            buffer8 = BitConverter.GetBytes(Section1Length + 0x18);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 60;
            buffer8 = BitConverter.GetBytes(chunkCount + 3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);


            //chunks

            //null chunk
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x0C]
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x63, 0x4B, 0x77
                });

            for (int i = 0; i < VerList.Count; i++) {
                byte[] verChunk = new byte[0];
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes(0x44), 1); //Size 1
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes(i + 1), 1); //ChunkMap Index
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes((UInt16)0x63), 1); // Version
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes((UInt16)0x9276), 1);
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes(0x40), 1); //Size 2
                verChunk = BinaryReader.b_AddBytes(verChunk, new byte[0x30] { 0x76, 0x65, 0x72, 0x30, 0x2E, 0x30, 0x30, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }); //Ver Header
                verChunk = BinaryReader.b_AddBytes(verChunk, BitConverter.GetBytes(VerList[i].PL_ANM_Sections.Count)); //PLANM Count
                verChunk = BinaryReader.b_AddBytes(verChunk, new byte[0x0C]);
                for (int c = 0; c < VerList[i].PL_ANM_Sections.Count; c++) {
                    int _ptr = 0;
                    byte[] copyBytes = new byte[0xD4];
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].PL_ANM_current_name, _ptr);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].PL_ANM_animation, _ptr + 0x20);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList.Count), _ptr + 0x50);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Interpolation), _ptr + 0x54);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Type), _ptr + 0x56);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].EnableCubeMan), _ptr + 0x58);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].EnableFaceAnim == true ? 2 : 0), _ptr + 0x5A);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].EnableBackwardFacingFix), _ptr + 0x5C);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].DisableInterpolation), _ptr + 0x5E);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].EnableAnimPosFix), _ptr + 0x60);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, _ptr + 0x62);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Direction), _ptr + 0x68);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Link_condition), _ptr + 0x6A);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Trigger_condition_1), _ptr + 0x6C);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Press_start), _ptr + 0x6E);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Press_end), _ptr + 0x70);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].Trigger_condition_2), _ptr + 0x72);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].PL_ANM_previous_name, _ptr + 0x74);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].PL_ANM_next_name, _ptr + 0x94);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].PL_ANM_DMG_name, _ptr + 0xB4);
                    _ptr += 0xD4;

                    for (int func = 0; func < VerList[i].PL_ANM_Sections[c].FunctionList.Count; func++) {
                        bool dmg_entry = false;
                        copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[0x40]);
                        if (VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageCode != "") {
                            dmg_entry = true;
                            copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[0x60]);
                        }
                        copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].FunctionList[func].StringParam, _ptr);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionTiming), _ptr + 0x20);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionID), _ptr + 0x22);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionParam1), _ptr + 0x24);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionParam2), _ptr + 0x26);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionParam3), _ptr + 0x28);
                        copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].FunctionParam4), _ptr + 0x2C);
                        if (dmg_entry) {
                            copyBytes = BinaryReader.b_ReplaceString(copyBytes, VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageCode, _ptr + 0x40);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageHitEffectID), _ptr + 0x82);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageHitSoundID), _ptr + 0x84);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageCondition), _ptr + 0x86);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageAmount), _ptr + 0x88);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageGuard), _ptr + 0x8C);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageHorizontalPush), _ptr + 0x90);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageVerticalPush), _ptr + 0x94);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageHitAmount), _ptr + 0x98);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageHitFrequency), _ptr + 0x9A);
                            copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(VerList[i].PL_ANM_Sections[c].FunctionList[func].DamageFreezeTime), _ptr + 0x9C);
                            _ptr += 0xA0;
                        } else
                            _ptr += 0x40;

                    }
                    verChunk = BinaryReader.b_AddBytes(verChunk, copyBytes);
                    verChunk = BinaryReader.b_ReplaceBytes(verChunk, BitConverter.GetBytes(verChunk.Length - 0x0C), 0, 1);
                    verChunk = BinaryReader.b_ReplaceBytes(verChunk, BitConverter.GetBytes(verChunk.Length - 0x10), 0xC, 1);
                }
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, verChunk);
            }
            if (ProjectileList.Count > 0) {

                byte[] projectileChunk = new byte[0x04]; //Size 1
                projectileChunk = BinaryReader.b_AddBytes(projectileChunk, BitConverter.GetBytes(VerList.Count + 1), 1); //ChunkMap Index
                projectileChunk = BinaryReader.b_AddBytes(projectileChunk, BitConverter.GetBytes((UInt16)0x63), 1); // Version
                projectileChunk = BinaryReader.b_AddBytes(projectileChunk, BitConverter.GetBytes((UInt16)0x9276), 1);
                projectileChunk = BinaryReader.b_AddBytes(projectileChunk, new byte[0x04], 1); //Size 2
                for (int i = 0; i < ProjectileList.Count; i++) {
                    byte[] copyBytes = new byte[0x80];
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, ProjectileList[i].ProjectileName, 0x00);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, ProjectileList[i].SkillFileName, 0x40);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, ProjectileList[i].SkillEntryName, 0x60);
                    copyBytes = BinaryReader.b_AddBytes(copyBytes, new byte[1] { ProjectileList[i].Unk });
                    projectileChunk = BinaryReader.b_AddBytes(projectileChunk, copyBytes);
                }
                byte[] lastBytes = new byte[0x80];
                lastBytes = BinaryReader.b_ReplaceString(lastBytes, ProjectileList[ProjectileList.Count - 1].ProjectileName, 0x00);
                lastBytes = BinaryReader.b_ReplaceString(lastBytes, "END", 0x40);
                lastBytes = BinaryReader.b_ReplaceString(lastBytes, ProjectileList[ProjectileList.Count - 1].SkillEntryName, 0x60);
                lastBytes = BinaryReader.b_AddBytes(lastBytes, new byte[1] { 1 });
                projectileChunk = BinaryReader.b_AddBytes(projectileChunk, lastBytes);
                projectileChunk = BinaryReader.b_ReplaceBytes(projectileChunk, BitConverter.GetBytes(projectileChunk.Length - 0x0C), 0, 1);
                projectileChunk = BinaryReader.b_ReplaceBytes(projectileChunk, BitConverter.GetBytes(projectileChunk.Length - 0x10), 0xC, 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, projectileChunk);
            }
            if (MovementList.Count > 0) {
                byte[] movementChunk = new byte[0x04]; //Size 1
                movementChunk = BinaryReader.b_AddBytes(movementChunk, BitConverter.GetBytes(VerList.Count + 2), 1); //ChunkMap Index
                movementChunk = BinaryReader.b_AddBytes(movementChunk, BitConverter.GetBytes((UInt16)0x63), 1); // Version
                movementChunk = BinaryReader.b_AddBytes(movementChunk, BitConverter.GetBytes((UInt16)0x9276), 1);
                movementChunk = BinaryReader.b_AddBytes(movementChunk, new byte[0x04], 1); //Size 2

                for (int i = 0; i < MovementList.Count; i++) {
                    byte[] copyBytes = new byte[0x20];
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].FrameActionUnlock), 0x00);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].ActionLength), 0x02);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].Unk1), 0x06);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].CircleVelocity), 0x08);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].Unk2), 0x0C);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].CircleVelocityStrength), 0x10);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].MovementFrequency), 0x16);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(MovementList[i].ForwardVelocity), 0x18);
                    movementChunk = BinaryReader.b_AddBytes(movementChunk, copyBytes);

                }

                movementChunk = BinaryReader.b_ReplaceBytes(movementChunk, BitConverter.GetBytes(movementChunk.Length - 0x0C), 0, 1);
                movementChunk = BinaryReader.b_ReplaceBytes(movementChunk, BitConverter.GetBytes(movementChunk.Length - 0x10), 0xC, 1);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, movementChunk);
            }
            if (CollisionList.Count > 0) {

                byte[] collisionChunk = new byte[0x04]; //Size 1
                collisionChunk = BinaryReader.b_AddBytes(collisionChunk, BitConverter.GetBytes(VerList.Count + 3), 1); //ChunkMap Index
                collisionChunk = BinaryReader.b_AddBytes(collisionChunk, BitConverter.GetBytes((UInt16)0x63), 1); // Version
                collisionChunk = BinaryReader.b_AddBytes(collisionChunk, BitConverter.GetBytes((UInt16)0x9276), 1);
                collisionChunk = BinaryReader.b_AddBytes(collisionChunk, new byte[0x04], 1); //Size 2
                collisionChunk = BinaryReader.b_AddBytes(collisionChunk, new byte[4]); // count
                for (int i = 0; i < CollisionList.Count; i++) {
                    byte[] copyBytes = new byte[0x5C];
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].Type), 0x00);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].State), 0x04);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].BoneEnabler), 0x08);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(i + 1), 0x0C);
                    copyBytes = BinaryReader.b_ReplaceString(copyBytes, CollisionList[i].BoneName, 0x10);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].HurtboxRadius), 0x50);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].Hurtbox_Y_Pos), 0x54);
                    copyBytes = BinaryReader.b_ReplaceBytes(copyBytes, BitConverter.GetBytes(CollisionList[i].Hurtbox_Z_Pos), 0x58);
                    collisionChunk = BinaryReader.b_AddBytes(collisionChunk, copyBytes);
                }

                collisionChunk = BinaryReader.b_ReplaceBytes(collisionChunk, BitConverter.GetBytes(collisionChunk.Length - 0x0C), 0, 1);
                collisionChunk = BinaryReader.b_ReplaceBytes(collisionChunk, BitConverter.GetBytes(collisionChunk.Length - 0x10), 0xC, 1);
                collisionChunk = BinaryReader.b_ReplaceBytes(collisionChunk, BitConverter.GetBytes(CollisionList.Count), 0x10);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, collisionChunk);
            }

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x08 });
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(chunkCount + 1), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes((UInt16)0x63), 1); // Version
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes((UInt16)0x00), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(chunkCount + 3), 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, BitConverter.GetBytes(0x00), 1);
            return fileBytes36;
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
        private RelayCommand _deleteVerEntryCommand;
        public RelayCommand DeleteVerEntryCommand {
            get {
                return _deleteVerEntryCommand ??
                  (_deleteVerEntryCommand = new RelayCommand(obj => {
                      RemoveVerEntryAsync();
                  }));
            }
        }

        private RelayCommand _addVerEntryCommand;
        public RelayCommand AddVerEntryCommand {
            get {
                return _addVerEntryCommand ??
                  (_addVerEntryCommand = new RelayCommand(obj => {
                      AddVerEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveVerEntryCommand;
        public RelayCommand SaveVerEntryCommand {
            get {
                return _saveVerEntryCommand ??
                  (_saveVerEntryCommand = new RelayCommand(obj => {
                      SaveVerEntryAsync();
                  }));
            }
        }
        private RelayCommand _deletePLANMEntryCommand;
        public RelayCommand DeletePLANMEntryCommand {
            get {
                return _deletePLANMEntryCommand ??
                  (_deletePLANMEntryCommand = new RelayCommand(obj => {
                      RemovePLANMEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupPLANMEntryCommand;
        public RelayCommand AddDupPLANMEntryCommand {
            get {
                return _addDupPLANMEntryCommand ??
                  (_addDupPLANMEntryCommand = new RelayCommand(obj => {
                      AddDupPLANMEntryAsync();
                  }));
            }
        }
        private RelayCommand _savePLANMEntryCommand;
        public RelayCommand SavePLANMEntryCommand {
            get {
                return _savePLANMEntryCommand ??
                  (_savePLANMEntryCommand = new RelayCommand(obj => {
                      SavePLANMEntryAsync();
                  }));
            }
        }
        private RelayCommand _copyPLANMEntryCommand;
        public RelayCommand CopyPLANMEntryCommand {
            get {
                return _copyPLANMEntryCommand ??
                  (_copyPLANMEntryCommand = new RelayCommand(obj => {
                      CopyPLANMEntryAsync();
                  }));
            }
        }
        private RelayCommand _pastePLANMEntryCommand;
        public RelayCommand PastePLANMEntryCommand {
            get {
                return _pastePLANMEntryCommand ??
                  (_pastePLANMEntryCommand = new RelayCommand(obj => {
                      PastePLANMEntryAsync();
                  }));
            }
        }

        private RelayCommand _deleteFunctionEntryCommand;
        public RelayCommand DeleteFunctionEntryCommand {
            get {
                return _deleteFunctionEntryCommand ??
                  (_deleteFunctionEntryCommand = new RelayCommand(obj => {
                      RemoveFunctionEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupFunctionEntryCommand;
        public RelayCommand AddDupFunctionEntryCommand {
            get {
                return _addDupFunctionEntryCommand ??
                  (_addDupFunctionEntryCommand = new RelayCommand(obj => {
                      AddDupFunctionEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveFunctionEntryCommand;
        public RelayCommand SaveFunctionEntryCommand {
            get {
                return _saveFunctionEntryCommand ??
                  (_saveFunctionEntryCommand = new RelayCommand(obj => {
                      SaveFunctionEntryAsync();
                  }));
            }
        }
        private RelayCommand _deleteProjectileEntryCommand;
        public RelayCommand DeleteProjectileEntryCommand {
            get {
                return _deleteProjectileEntryCommand ??
                  (_deleteProjectileEntryCommand = new RelayCommand(obj => {
                      RemoveProjectileEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupProjectileEntryCommand;
        public RelayCommand AddDupProjectileEntryCommand {
            get {
                return _addDupProjectileEntryCommand ??
                  (_addDupProjectileEntryCommand = new RelayCommand(obj => {
                      AddDupProjectileEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveProjectileEntryCommand;
        public RelayCommand SaveProjectileEntryCommand {
            get {
                return _saveProjectileEntryCommand ??
                  (_saveProjectileEntryCommand = new RelayCommand(obj => {
                      SaveProjectileEntryAsync();
                  }));
            }
        }
        private RelayCommand _copyFunctionEntryCommand;
        public RelayCommand CopyFunctionEntryCommand {
            get {
                return _copyFunctionEntryCommand ??
                  (_copyFunctionEntryCommand = new RelayCommand(obj => {
                      CopyFunctionEntryAsync();
                  }));
            }
        }
        private RelayCommand _pasteFunctionEntryCommand;
        public RelayCommand PasteFunctionEntryCommand {
            get {
                return _pasteFunctionEntryCommand ??
                  (_pasteFunctionEntryCommand = new RelayCommand(obj => {
                      PasteFunctionEntryAsync();
                  }));
            }
        }
        private RelayCommand _deleteCollisionEntryCommand;
        public RelayCommand DeleteCollisionEntryCommand {
            get {
                return _deleteCollisionEntryCommand ??
                  (_deleteCollisionEntryCommand = new RelayCommand(obj => {
                      RemoveCollisionEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupCollisionEntryCommand;
        public RelayCommand AddDupCollisionEntryCommand {
            get {
                return _addDupCollisionEntryCommand ??
                  (_addDupCollisionEntryCommand = new RelayCommand(obj => {
                      AddDupCollisionEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveCollisionEntryCommand;
        public RelayCommand SaveCollisionEntryCommand {
            get {
                return _saveCollisionEntryCommand ??
                  (_saveCollisionEntryCommand = new RelayCommand(obj => {
                      SaveCollisionEntryAsync();
                  }));
            }
        }
        private RelayCommand _deleteMovementEntryCommand;
        public RelayCommand DeleteMovementEntryCommand {
            get {
                return _deleteMovementEntryCommand ??
                  (_deleteMovementEntryCommand = new RelayCommand(obj => {
                      RemoveMovementEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupMovementEntryCommand;
        public RelayCommand AddDupMovementEntryCommand {
            get {
                return _addDupMovementEntryCommand ??
                  (_addDupMovementEntryCommand = new RelayCommand(obj => {
                      AddDupMovementEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveMovementEntryCommand;
        public RelayCommand SaveMovementEntryCommand {
            get {
                return _saveMovementEntryCommand ??
                  (_saveMovementEntryCommand = new RelayCommand(obj => {
                      SaveMovementEntryAsync();
                  }));
            }
        }
        private RelayCommand _changeCharacodeCommand;
        public RelayCommand ChangeCharacodeCommand {
            get {
                return _changeCharacodeCommand ??
                  (_changeCharacodeCommand = new RelayCommand(obj => {
                      ChangeCharacodeAsync();
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

        public async void AddVerEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddVerEntry()));

        }
        public async void SaveVerEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveVerEntry()));

        }
        public async void RemoveVerEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveVerEntry()));

        }

        public async void AddDupPLANMEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupPLANMEntry()));

        }
        public async void SavePLANMEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SavePLANMEntry()));

        }
        public async void RemovePLANMEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemovePLANMEntry()));

        }

        public async void CopyPLANMEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => CopyPLANMEntry()));

        }
        public async void PastePLANMEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => PastePLANMEntry()));

        }


        public async void AddDupFunctionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupFunctionEntry()));

        }
        public async void SaveFunctionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFunctionEntry()));

        }
        public async void RemoveFunctionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveFunctionEntry()));

        }

        public async void CopyFunctionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => CopyFunctionEntry()));

        }
        public async void PasteFunctionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => PasteFunctionEntry()));

        }

        public async void AddDupProjectileEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupProjectileEntry()));

        }
        public async void SaveProjectileEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveProjectileEntry()));

        }
        public async void RemoveProjectileEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveProjectileEntry()));

        }

        public async void AddDupCollisionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupCollisionEntry()));

        }
        public async void SaveCollisionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveCollisionEntry()));

        }
        public async void RemoveCollisionEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveCollisionEntry()));

        }
        public async void AddDupMovementEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupMovementEntry()));

        }
        public async void SaveMovementEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveMovementEntry()));

        }
        public async void RemoveMovementEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveMovementEntry()));

        }
        public async void ChangeCharacodeAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => ChangeCharacode()));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
