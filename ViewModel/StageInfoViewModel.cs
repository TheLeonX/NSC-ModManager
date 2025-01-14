using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using NSC_ModManager.Properties;
using DynamicData;

namespace NSC_ModManager.ViewModel {
    public class StageInfoViewModel : INotifyPropertyChanged {
        private string _stageTextBoxString;
        public string StageTextBoxString {
            get { return _stageTextBoxString; }
            set {
                _stageTextBoxString = value;
                OnPropertyChanged("StageTextBoxString");
            }
        }
        private ObservableCollection<string> _stagePathComboBoxList;
        public ObservableCollection<string> StagePathComboBoxList {
            get { return _stagePathComboBoxList; }
            set {
                _stagePathComboBoxList = value;
                OnPropertyChanged("StagePathComboBoxList");
            }
        }
        private int _objectPathComboBoxIndex;
        public int ObjectPathComboBoxIndex {
            get { return _objectPathComboBoxIndex; }
            set {
                _objectPathComboBoxIndex = value;
                OnPropertyChanged("ObjectPathComboBoxIndex");
            }
        }
        private int _objectPositionPathComboBoxIndex;
        public int ObjectPositionPathComboBoxIndex {
            get { return _objectPositionPathComboBoxIndex; }
            set {
                _objectPositionPathComboBoxIndex = value;
                OnPropertyChanged("ObjectPositionPathComboBoxIndex");
            }
        }
        private int _breakableObjectPathComboBoxIndex;
        public int BreakableObjectPathComboBoxIndex {
            get { return _breakableObjectPathComboBoxIndex; }
            set {
                _breakableObjectPathComboBoxIndex = value;
                OnPropertyChanged("BreakableObjectPathComboBoxIndex");
            }
        }
        public ObservableCollection<string> ObjectEntryType_List { get; set; }
        public ObservableCollection<StageInfoModel> StageInfoList { get; set; }
        private StageInfoModel _selectedStage;
        public StageInfoModel SelectedStage {
            get { return _selectedStage; }
            set {
                _selectedStage = value;
                if (value is not null) {
                    StageName_field = value.StageName;
                    StageMessageID_field = value.StageMessageID;
                    StageFilter_field = value.StageFilter;

                    ObservableCollection<string> PathListComboBox = new ObservableCollection<string>();
                    PathListComboBox.Add("");
                    for (int i = 0; i < value.FilePaths.Count; i++) {
                        PathListComboBox.Add(value.FilePaths[i].FilePath);
                    }
                    StagePathComboBoxList = PathListComboBox;
                    FilePathList = value.FilePaths;
                    ObjectList = value.Objects;
                    Weather_field = value.Weather;
                    PlayerAmbientColor_field = value.PlayerAmbientColor;
                    RayCutOffShadeColor_field = value.RayCutOffShadeColor;
                    EffectAmbientColor_field = value.EffectAmbientColor;
                    UnknownColor_field = value.UnknownColor;
                    EnableBrightnessAdjustment_field = value.EnableBrightnessAdjustment;
                    Brightness_field = value.Brightness;
                    Contrast_field = value.Contrast;
                    EnableLensFlare_field = value.EnableLensFlare;
                    LensFlare_field = value.LensFlare;
                    LensFlarePositionX_field = value.LensFlarePositionX;
                    LensFlarePositionY_field = value.LensFlarePositionY;
                    LensFlarePositionZ_field = value.LensFlarePositionZ;
                    LensFlareAlpha_field = value.LensFlareAlpha;
                    ParallelAmbientColor_field = value.ParallelAmbientColor;
                    RayCutOffNormalColor_field = value.RayCutOffNormalColor;
                    LightPointDirectionX_field = value.LightPointDirectionX;
                    LightPointDirectionY_field = value.LightPointDirectionY;
                    LightPointDirectionZ_field = value.LightPointDirectionZ;
                    EnableShadowColor_field = value.EnableShadowColor;
                    ShadowColor_field = value.ShadowColor;
                    EnableFog_field = value.EnableFog;
                    FogStartDistance_field = value.FogStartDistance;
                    FogEndDistance_field = value.FogEndDistance;
                    FogStrength_field = value.FogStrength;
                    FogColor_field = value.FogColor;
                    EnableMonoColorFilter_field = value.EnableMonoColorFilter;
                    MonoBlueTone_field = value.MonoBlueTone;
                    MonoRedTone_field = value.MonoRedTone;
                    MonoAlpha_field = value.MonoAlpha;
                    EnableGlareEffect_field = value.EnableGlareEffect;
                    GlareLuminanceThreshold_field = value.GlareLuminanceThreshold;
                    GlareSubtracted_field = value.GlareSubtracted;
                    GlareCompositionStrength_field = value.GlareCompositionStrength;
                    EnableSoftFocus_field = value.EnableSoftFocus;
                    SoftFocusStrength_field = value.SoftFocusStrength;
                    EnableDOFBlur_field = value.EnableDOFBlur;
                    DOFFocalLength_field = value.DOFFocalLength;
                    DOFShortDistance_field = value.DOFShortDistance;
                    DOFLongDistance_field = value.DOFLongDistance;
                    DOFAlpha_field = value.DOFAlpha;
                    EnableDOFEdgeBlur_field = value.EnableDOFEdgeBlur;
                    EnableSunShaft_field = value.EnableSunShaft;
                    SunShaftStartDistance_field = value.SunShaftStartDistance;
                    SunShaftEndDistance_field = value.SunShaftEndDistance;
                    SunShaftAlpha_field = value.SunShaftAlpha;
                    SunShaftColor_field = value.SunShaftColor;
                    SunShaftDirectionX_field = value.SunShaftDirectionX;
                    SunShaftDirectionY_field = value.SunShaftDirectionY;
                    SunShaftDirectionZ_field = value.SunShaftDirectionZ;
                    SunShaftBlurWidth_field = value.SunShaftBlurWidth;
                    SunShaftAttenuationCoefficient_field = value.SunShaftAttenuationCoefficient;
                    RockColor_field = value.RockColor;
                }

                OnPropertyChanged("SelectedStage");
            }
        }
        private int _selectedStageIndex;
        public int SelectedStageIndex {
            get { return _selectedStageIndex; }
            set {
                _selectedStageIndex = value;
                OnPropertyChanged("SelectedStageIndex");
            }
        }

        private string _stageName_field;
        public string StageName_field {
            get{ return _stageName_field; }
            set{
                _stageName_field = value;
                OnPropertyChanged("StageName_field");
            }
        }
        private string _stageMessageID_field;
        public string StageMessageID_field {
            get{ return _stageMessageID_field; }
            set{
                _stageMessageID_field = value;
                OnPropertyChanged("StageMessageID_field");
            }
        }
        private string _stageFilter_field;
        public string StageFilter_field {
            get{ return _stageFilter_field; }
            set{
                _stageFilter_field = value;
                OnPropertyChanged("StageFilter_field");
            }
        }
        private int _weather_field;
        public int Weather_field {
            get{ return _weather_field; }
            set{
                _weather_field = value;
                OnPropertyChanged("Weather_field");
            }
        }
        private Color _playerAmbientColor_field;
        public Color PlayerAmbientColor_field {
            get{ return _playerAmbientColor_field; }
            set{
                _playerAmbientColor_field = value;
                OnPropertyChanged("PlayerAmbientColor_field");
            }
        }
        private Color _rayCutOffShadeColor_field;
        public Color RayCutOffShadeColor_field {
            get{ return _rayCutOffShadeColor_field; }
            set{
                _rayCutOffShadeColor_field = value;
                OnPropertyChanged("RayCutOffShadeColor_field");
            }
        }
        private Color _effectAmbientColor_field;
        public Color EffectAmbientColor_field {
            get{ return _effectAmbientColor_field; }
            set{
                _effectAmbientColor_field = value;
                OnPropertyChanged("EffectAmbientColor_field");
            }
        }
        private Color _unknownColor_field;
        public Color UnknownColor_field {
            get{ return _unknownColor_field; }
            set{
                _unknownColor_field = value;
                OnPropertyChanged("UnknownColor_field");
            }
        }
        private bool _enableBrightnessAdjustment_field;
        public bool EnableBrightnessAdjustment_field {
            get{ return _enableBrightnessAdjustment_field; }
            set{
                _enableBrightnessAdjustment_field = value;
                OnPropertyChanged("EnableBrightnessAdjustment_field");
            }
        }
        private float _brightness_field;
        public float Brightness_field {
            get{ return _brightness_field; }
            set{
                _brightness_field = value;
                OnPropertyChanged("Brightness_field");
            }
        }
        private float _contrast_field;
        public float Contrast_field {
            get{ return _contrast_field; }
            set{
                _contrast_field = value;
                OnPropertyChanged("Contrast_field");
            }
        }
        private bool _enableLensFlare_field;
        public bool EnableLensFlare_field {
            get{ return _enableLensFlare_field; }
            set{
                _enableLensFlare_field = value;
                OnPropertyChanged("EnableLensFlare_field");
            }
        }

        private int _lensFlare_field;
        public int LensFlare_field {
            get{ return _lensFlare_field; }
            set{
                _lensFlare_field = value;
                OnPropertyChanged("LensFlare_field");
            }
        }
        private float _lensFlarePositionX_field;
        public float LensFlarePositionX_field {
            get{ return _lensFlarePositionX_field; }
            set{
                _lensFlarePositionX_field = value;
                OnPropertyChanged("LensFlarePositionX_field");
            }
        }
        private float _lensFlarePositionY_field;
        public float LensFlarePositionY_field {
            get{ return _lensFlarePositionY_field; }
            set{
                _lensFlarePositionY_field = value;
                OnPropertyChanged("LensFlarePositionY_field");
            }
        }
        private float _lensFlarePositionZ_field;
        public float LensFlarePositionZ_field {
            get{ return _lensFlarePositionZ_field; }
            set{
                _lensFlarePositionZ_field = value;
                OnPropertyChanged("LensFlarePositionZ_field");
            }
        }
        private float _lensFlareAlpha_field;
        public float LensFlareAlpha_field {
            get{ return _lensFlareAlpha_field; }
            set{
                _lensFlareAlpha_field = value;
                OnPropertyChanged("LensFlareAlpha_field");
            }
        }
        private Color _parallelAmbientColor_field;
        public Color ParallelAmbientColor_field {
            get{ return _parallelAmbientColor_field; }
            set{
                _parallelAmbientColor_field = value;
                OnPropertyChanged("ParallelAmbientColor_field");
            }
        }
        private Color _rayCutOffNormalColor_field;
        public Color RayCutOffNormalColor_field {
            get{ return _rayCutOffNormalColor_field; }
            set{
                _rayCutOffNormalColor_field = value;
                OnPropertyChanged("RayCutOffNormalColor_field");
            }
        }
        private float _lightPointDirectionX_field;
        public float LightPointDirectionX_field {
            get{ return _lightPointDirectionX_field; }
            set{
                _lightPointDirectionX_field = value;
                OnPropertyChanged("LightPointDirectionX_field");
            }
        }
        private float _lightPointDirectionY_field;
        public float LightPointDirectionY_field {
            get{ return _lightPointDirectionY_field; }
            set{
                _lightPointDirectionY_field = value;
                OnPropertyChanged("LightPointDirectionY_field");
            }
        }
        private float _lightPointDirectionZ_field;
        public float LightPointDirectionZ_field {
            get{ return _lightPointDirectionZ_field; }
            set{
                _lightPointDirectionZ_field = value;
                OnPropertyChanged("LightPointDirectionZ_field");
            }
        }
        private bool _enableShadowColor_field;
        public bool EnableShadowColor_field {
            get{ return _enableShadowColor_field; }
            set{
                _enableShadowColor_field = value;
                OnPropertyChanged("EnableShadowColor_field");
            }
        }
        private Color _shadowColor_field;
        public Color ShadowColor_field {
            get{ return _shadowColor_field; }
            set{
                _shadowColor_field = value;
                OnPropertyChanged("ShadowColor_field");
            }
        }
        private bool _enableFog_field;
        public bool EnableFog_field {
            get{ return _enableFog_field; }
            set{
                _enableFog_field = value;
                OnPropertyChanged("EnableFog_field");
            }
        }
        private float _fogStartDistance_field;
        public float FogStartDistance_field {
            get{ return _fogStartDistance_field; }
            set{
                _fogStartDistance_field = value;
                OnPropertyChanged("FogStartDistance_field");
            }
        }
        private float _fogEndDistance_field;
        public float FogEndDistance_field {
            get{ return _fogEndDistance_field; }
            set{
                _fogEndDistance_field = value;
                OnPropertyChanged("FogEndDistance_field");
            }
        }
        private float _fogStrength_field;
        public float FogStrength_field {
            get { return _fogStrength_field; }
            set {
                _fogStrength_field = value;
                OnPropertyChanged("FogStrength_field");
            }
        }
        private Color _fogColor_field;
        public Color FogColor_field {
            get { return _fogColor_field; }
            set {
                _fogColor_field = value;
                OnPropertyChanged("FogColor_field");
            }
        }
        private bool _enableMonoColorFilter_field;
        public bool EnableMonoColorFilter_field {
            get { return _enableMonoColorFilter_field; }
            set {
                _enableMonoColorFilter_field = value;
                OnPropertyChanged("EnableMonoColorFilter_field");
            }
        }
        private float _monoBlueTone_field;
        public float MonoBlueTone_field {
            get { return _monoBlueTone_field; }
            set {
                _monoBlueTone_field = value;
                OnPropertyChanged("MonoBlueTone_field");
            }
        }
        private float _monoRedTone_field;
        public float MonoRedTone_field {
            get { return _monoRedTone_field; }
            set {
                _monoRedTone_field = value;
                OnPropertyChanged("MonoRedTone_field");
            }
        }
        private float _monoAlpha_field;
        public float MonoAlpha_field {
            get { return _monoAlpha_field; }
            set {
                _monoAlpha_field = value;
                OnPropertyChanged("MonoAlpha_field");
            }
        }
        private bool _enableGlareEffect_field;
        public bool EnableGlareEffect_field {
            get { return _enableGlareEffect_field; }
            set {
                _enableGlareEffect_field = value;
                OnPropertyChanged("EnableGlareEffect_field");
            }
        }
        private float _glareLuminanceThreshold_field;
        public float GlareLuminanceThreshold_field {
            get { return _glareLuminanceThreshold_field; }
            set {
                _glareLuminanceThreshold_field = value;
                OnPropertyChanged("GlareLuminanceThreshold_field");
            }
        }
        private float _glareSubtracted_field;
        public float GlareSubtracted_field {
            get { return _glareSubtracted_field; }
            set {
                _glareSubtracted_field = value;
                OnPropertyChanged("GlareSubtracted_field");
            }
        }
        private float _glareCompositionStrength_field;
        public float GlareCompositionStrength_field {
            get { return _glareCompositionStrength_field; }
            set {
                _glareCompositionStrength_field = value;
                OnPropertyChanged("GlareCompositionStrength_field");
            }
        }
        private bool _enableSoftFocus_field;
        public bool EnableSoftFocus_field {
            get { return _enableSoftFocus_field; }
            set {
                _enableSoftFocus_field = value;
                OnPropertyChanged("EnableSoftFocus_field");
            }
        }
        private float _softFocusStrength_field;
        public float SoftFocusStrength_field {
            get { return _softFocusStrength_field; }
            set {
                _softFocusStrength_field = value;
                OnPropertyChanged("SoftFocusStrength_field");
            }
        }
        private bool _enableDOFBlur_field;
        public bool EnableDOFBlur_field {
            get { return _enableDOFBlur_field; }
            set {
                _enableDOFBlur_field = value;
                OnPropertyChanged("EnableDOFBlur_field");
            }
        }
        private float _DOFFocalLength_field;
        public float DOFFocalLength_field {
            get { return _DOFFocalLength_field; }
            set {
                _DOFFocalLength_field = value;
                OnPropertyChanged("DOFFocalLength_field");
            }
        }
        private float _DOFShortDistance_field;
        public float DOFShortDistance_field {
            get { return _DOFShortDistance_field; }
            set {
                _DOFShortDistance_field = value;
                OnPropertyChanged("DOFShortDistance_field");
            }
        }
        private float _DOFLongDistance_field;
        public float DOFLongDistance_field {
            get { return _DOFLongDistance_field; }
            set {
                _DOFLongDistance_field = value;
                OnPropertyChanged("DOFLongDistance_field");
            }
        }
        private float _DOFAlpha_field;
        public float DOFAlpha_field {
            get { return _DOFAlpha_field; }
            set {
                _DOFAlpha_field = value;
                OnPropertyChanged("DOFAlpha_field");
            }
        }
        private bool _enableDOFEdgeBlur_field;
        public bool EnableDOFEdgeBlur_field {
            get { return _enableDOFEdgeBlur_field; }
            set {
                _enableDOFEdgeBlur_field = value;
                OnPropertyChanged("EnableDOFEdgeBlur_field");
            }
        }
        private bool _enableSunShaft_field;
        public bool EnableSunShaft_field {
            get { return _enableSunShaft_field; }
            set {
                _enableSunShaft_field = value;
                OnPropertyChanged("EnableSunShaft_field");
            }
        }
        private float _sunShaftStartDistance_field;
        public float SunShaftStartDistance_field {
            get { return _sunShaftStartDistance_field; }
            set {
                _sunShaftStartDistance_field = value;
                OnPropertyChanged("SunShaftStartDistance_field");
            }
        }
        private float _sunShaftEndDistance_field;
        public float SunShaftEndDistance_field {
            get { return _sunShaftEndDistance_field; }
            set {
                _sunShaftEndDistance_field = value;
                OnPropertyChanged("SunShaftEndDistance_field");
            }
        }
        private float _sunShaftAlpha_field;
        public float SunShaftAlpha_field {
            get { return _sunShaftAlpha_field; }
            set {
                _sunShaftAlpha_field = value;
                OnPropertyChanged("SunShaftAlpha_field");
            }
        }
        private Color _sunShaftColor_field;
        public Color SunShaftColor_field {
            get { return _sunShaftColor_field; }
            set {
                _sunShaftColor_field = value;
                OnPropertyChanged("SunShaftColor_field");
            }
        }
        private float _sunShaftDirectionX_field;
        public float SunShaftDirectionX_field {
            get { return _sunShaftDirectionX_field; }
            set {
                _sunShaftDirectionX_field = value;
                OnPropertyChanged("SunShaftDirectionX_field");
            }
        }
        private float _sunShaftDirectionY_field;
        public float SunShaftDirectionY_field {
            get { return _sunShaftDirectionY_field; }
            set {
                _sunShaftDirectionY_field = value;
                OnPropertyChanged("SunShaftDirectionY_field");
            }
        }
        private float _sunShaftDirectionZ_field;
        public float SunShaftDirectionZ_field {
            get { return _sunShaftDirectionZ_field; }
            set {
                _sunShaftDirectionZ_field = value;
                OnPropertyChanged("SunShaftDirectionZ_field");
            }
        }
        private float _sunShaftBlurWidth_field;
        public float SunShaftBlurWidth_field {
            get { return _sunShaftBlurWidth_field; }
            set {
                _sunShaftBlurWidth_field = value;
                OnPropertyChanged("SunShaftBlurWidth_field");
            }
        }
        private float _sunShaftAttenuationCoefficient_field;
        public float SunShaftAttenuationCoefficient_field {
            get { return _sunShaftAttenuationCoefficient_field; }
            set {
                _sunShaftAttenuationCoefficient_field = value;
                OnPropertyChanged("SunShaftAttenuationCoefficient_field");
            }
        }
        private Color _rockColor_field;
        public Color RockColor_field {
            get { return _rockColor_field; }
            set {
                _rockColor_field = value;
                OnPropertyChanged("RockColor_field");
            }
        }
        private ObservableCollection<StageInfoFilePathModel> _filePathList;
        public ObservableCollection<StageInfoFilePathModel> FilePathList {
            get { return _filePathList; }
            set {
                _filePathList = value;
                OnPropertyChanged("FilePathList");
            }
        }

        private StageInfoFilePathModel _selectedfilePath;
        public StageInfoFilePathModel SelectedFilePath {
            get { return _selectedfilePath; }
            set {
                _selectedfilePath = value;
                if (value is not null) {
                    FilePathTextBox_field = value.FilePath;
                }
                OnPropertyChanged("SelectedFilePath");
            }
        }
        private int _selectedfilePathIndex;
        public int SelectedFilePathIndex {
            get { return _selectedfilePathIndex; }
            set {
                _selectedfilePathIndex = value;
                OnPropertyChanged("SelectedFilePathIndex");
            }
        }

        private string _filePathTextBox_field;
        public string FilePathTextBox_field {
            get { return _filePathTextBox_field; }
            set {
                _filePathTextBox_field = value;
                OnPropertyChanged("FilePathTextBox_field");
            }
        }

        private ObservableCollection<StageInfoObjectModel> _objectList;
        public ObservableCollection<StageInfoObjectModel> ObjectList {
            get { return _objectList; }
            set {
                _objectList = value;
                OnPropertyChanged("ObjectList");
            }
        }
        private StageInfoObjectModel _selectedObject;
        public StageInfoObjectModel SelectedObject {
            get { return _selectedObject; }
            set {
                _selectedObject = value;
                if (value is not null) {

                    int object_path_index = StagePathComboBoxList.IndexOf(value.ObjectFilePath);
                    int pos_path_index = StagePathComboBoxList.IndexOf(value.PositionFilePath);
                    int breakable_path_index = StagePathComboBoxList.IndexOf(value.BreakableObjectPath);

                    if (object_path_index != -1)
                        ObjectPathComboBoxIndex = object_path_index;
                    else
                        ObjectPathComboBoxIndex = 0;

                    if (pos_path_index != -1)
                        ObjectPositionPathComboBoxIndex = pos_path_index;
                    else
                        ObjectPositionPathComboBoxIndex = 0;
                    if (breakable_path_index != -1)
                        BreakableObjectPathComboBoxIndex = breakable_path_index;
                    else
                        BreakableObjectPathComboBoxIndex = 0;

                    ObjectFilePath_field = value.ObjectFilePath;
                    ObjectName_field = value.ObjectName;
                    PositionFilePath_field = value.PositionFilePath;
                    PositionBoneName_field = value.PositionBoneName;
                    EntryType_field = value.EntryType;
                    EnableCameraHideObject_field = value.EnableCameraHideObject;
                    IsRigidBody_field = value.IsRigidBody;
                    AnimationSpeed_field = value.AnimationSpeed;
                    BreakableWallValue1_field = value.BreakableWallValue1;
                    BreakableWallValue2_field = value.BreakableWallValue2;
                    BreakableWallEffect01_field = value.BreakableWallEffect01;
                    BreakableWallEffect02_field = value.BreakableWallEffect02;
                    BreakableWallEffect03_field = value.BreakableWallEffect03;
                    BreakableWallSound_field = value.BreakableWallSound;
                    BreakableWallVolume_field = value.BreakableWallVolume;
                    BreakableObjectPath_field = value.BreakableObjectPath;
                    BreakableObjectEffect01_field = value.BreakableObjectEffect01;
                    BreakableObjectEffect02_field = value.BreakableObjectEffect02;
                    BreakableObjectEffect03_field = value.BreakableObjectEffect03;
                    BreakableObjectSpeed01_field = value.BreakableObjectSpeed01;
                    BreakableObjectSpeed02_field = value.BreakableObjectSpeed02;
                    BreakableObjectSpeed03_field = value.BreakableObjectSpeed03;
                }
                OnPropertyChanged("SelectedObject");
            }
        }
        private int _selectedObjectIndex;
        public int SelectedObjectIndex {
            get { return _selectedObjectIndex; }
            set {
                _selectedObjectIndex = value;
                OnPropertyChanged("SelectedObjectIndex");
            }
        }

        private string _objectfilePath_field;
        public string ObjectFilePath_field {
            get { return _objectfilePath_field; }
            set {
                _objectfilePath_field = value;
                OnPropertyChanged("ObjectFilePath_field");
            }
        }
        private string _objectName_field;
        public string ObjectName_field {
            get { return _objectName_field; }
            set {
                _objectName_field = value;
                OnPropertyChanged("ObjectName_field");
            }
        }
        private string _positionFilePath_field;
        public string PositionFilePath_field {
            get { return _positionFilePath_field; }
            set {
                _positionFilePath_field = value;
                OnPropertyChanged("PositionFilePath_field");
            }
        }
        private string _positionBoneName_field;
        public string PositionBoneName_field {
            get { return _positionBoneName_field; }
            set {
                _positionBoneName_field = value;
                OnPropertyChanged("PositionBoneName_field");
            }
        }
        private int _entryType_field;
        public int EntryType_field {
            get { return _entryType_field; }
            set {
                _entryType_field = value;
                OnPropertyChanged("EntryType_field");
            }
        }
        private bool _enableCameraHideObject_field;
        public bool EnableCameraHideObject_field {
            get { return _enableCameraHideObject_field; }
            set {
                _enableCameraHideObject_field = value;
                OnPropertyChanged("EnableCameraHideObject_field");
            }
        }
        private bool _isRigidBody_field;
        public bool IsRigidBody_field {
            get { return _isRigidBody_field; }
            set {
                _isRigidBody_field = value;
                OnPropertyChanged("IsRigidBody_field");
            }
        }
        private float _animationSpeed_field;
        public float AnimationSpeed_field {
            get { return _animationSpeed_field; }
            set {
                _animationSpeed_field = value;
                OnPropertyChanged("AnimationSpeed_field");
            }
        }
        private int _breakableWallValue1_field;
        public int BreakableWallValue1_field {
            get { return _breakableWallValue1_field; }
            set {
                _breakableWallValue1_field = value;
                OnPropertyChanged("BreakableWallValue1_field");
            }
        }
        private int _breakableWallValue2_field;
        public int BreakableWallValue2_field {
            get { return _breakableWallValue2_field; }
            set {
                _breakableWallValue2_field = value;
                OnPropertyChanged("BreakableWallValue2_field");
            }
        }
        private string _breakableWallEffect01_field;
        public string BreakableWallEffect01_field {
            get { return _breakableWallEffect01_field; }
            set {
                _breakableWallEffect01_field = value;
                OnPropertyChanged("BreakableWallEffect01_field");
            }
        }
        private string _breakableWallEffect02_field;
        public string BreakableWallEffect02_field {
            get { return _breakableWallEffect02_field; }
            set {
                _breakableWallEffect02_field = value;
                OnPropertyChanged("BreakableWallEffect02_field");
            }
        }
        private string _breakableWallEffect03_field;
        public string BreakableWallEffect03_field {
            get { return _breakableWallEffect03_field; }
            set {
                _breakableWallEffect03_field = value;
                OnPropertyChanged("BreakableWallEffect03_field");
            }
        }
        private string _breakableWallSound_field;
        public string BreakableWallSound_field {
            get { return _breakableWallSound_field; }
            set {
                _breakableWallSound_field = value;
                OnPropertyChanged("BreakableWallSound_field");
            }
        }
        private string _breakableObjectPath_field;
        public string BreakableObjectPath_field {
            get { return _breakableObjectPath_field; }
            set {
                _breakableObjectPath_field = value;
                OnPropertyChanged("BreakableObjectPath_field");
            }
        }
        private string _breakableObjectEffect01_field;
        public string BreakableObjectEffect01_field {
            get { return _breakableObjectEffect01_field; }
            set {
                _breakableObjectEffect01_field = value;
                OnPropertyChanged("BreakableObjectEffect01_field");
            }
        }
        private string _breakableObjectEffect02_field;
        public string BreakableObjectEffect02_field {
            get { return _breakableObjectEffect02_field; }
            set {
                _breakableObjectEffect02_field = value;
                OnPropertyChanged("BreakableObjectEffect02_field");
            }
        }
        private string _breakableObjectEffect03_field;
        public string BreakableObjectEffect03_field {
            get { return _breakableObjectEffect03_field; }
            set {
                _breakableObjectEffect03_field = value;
                OnPropertyChanged("BreakableObjectEffect03_field");
            }
        }
        private float _breakableObjectSpeed01_field;
        public float BreakableObjectSpeed01_field {
            get { return _breakableObjectSpeed01_field; }
            set {
                _breakableObjectSpeed01_field = value;
                OnPropertyChanged("BreakableObjectSpeed01_field");
            }
        }
        private float _breakableObjectSpeed02_field;
        public float BreakableObjectSpeed02_field {
            get { return _breakableObjectSpeed02_field; }
            set {
                _breakableObjectSpeed02_field = value;
                OnPropertyChanged("BreakableObjectSpeed02_field");
            }
        }
        private float _breakableObjectSpeed03_field;
        public float BreakableObjectSpeed03_field {
            get { return _breakableObjectSpeed03_field; }
            set {
                _breakableObjectSpeed03_field = value;
                OnPropertyChanged("BreakableObjectSpeed03_field");
            }
        }
        private float _breakableWallVolume_field;
        public float BreakableWallVolume_field {
            get { return _breakableWallVolume_field; }
            set {
                _breakableWallVolume_field = value;
                OnPropertyChanged("BreakableWallVolume_field");
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
        public string filePath;
        public byte[] fileBytes;
        public string FileBinName;
        public bool finishedConvert;
        public byte[] savedFile;

        public StageInfoModel CopiedStageProperties;
        public StageInfoViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            StageInfoList = new ObservableCollection<StageInfoModel>();
            FilePathList = new ObservableCollection<StageInfoFilePathModel>();
            ObjectList = new ObservableCollection<StageInfoObjectModel>();
            ObjectEntryType_List = new ObservableCollection<string>();
            StagePathComboBoxList = new ObservableCollection<string>();
            CopiedStageProperties = new StageInfoModel();
            for (int x = 0; x < Program.StageEntryTypeList.Length; x++) ObjectEntryType_List.Add(Program.StageEntryTypeList[x]);
            for (int x = Program.StageEntryTypeList.Length; x < 256; x++) ObjectEntryType_List.Add(ObjectEntryType_List.Count.ToString() + " = ???");
            finishedConvert = false;
            savedFile = new byte[0];
            filePath = "";
        }
        public void Clear() {
            StageInfoList.Clear();
            FilePathList.Clear();
            ObjectList.Clear();
            CopiedStageProperties = new StageInfoModel();
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
                fileBytes = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(fileBytes, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++) {
                    string name = BinaryReader.b_ReadString(fileBytes, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(fileBytes, 16);
                if (BinName.Contains("stageInfo") || BinName.Contains("advStageInfo")) {
                    FileBinName = BinName;
                    int entryCount = BinaryReader.b_ReadInt(fileBytes, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int _ptr = StartOfFile + 0x10 + 0x130 * c;
                        StageInfoModel StageInfoEntry = new StageInfoModel();
                        StageInfoEntry.StageName = BinaryReader.b_ReadString(fileBytes, _ptr + BinaryReader.b_ReadInt(fileBytes, _ptr));
                        StageInfoEntry.StageMessageID = BinaryReader.b_ReadString(fileBytes, _ptr + 0x08 + BinaryReader.b_ReadInt(fileBytes, _ptr + 0x08));
                        StageInfoEntry.StageFilter = BinaryReader.b_ReadString(fileBytes, _ptr + 0x10 + BinaryReader.b_ReadInt(fileBytes, _ptr + 0x10));
                        
                        int PathCount = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x18);

                        ObservableCollection<StageInfoFilePathModel> PathList = new ObservableCollection<StageInfoFilePathModel>();
                        for (int path = 0; path < PathCount; path++) {
                            StageInfoFilePathModel path_entry = new StageInfoFilePathModel();

                            int pathListPtr = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x20);
                            int pathPtr = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x20 + pathListPtr + (0x08 * path));
                            int ptr = _ptr + 0x20 + pathListPtr + pathPtr + (0x08 * path);
                            path_entry.FilePath = BinaryReader.b_ReadString(fileBytes, ptr);
                            PathList.Add(path_entry);

                        }
                        StageInfoEntry.FilePaths = PathList;

                        int ObjectCount = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x28);
                        ObservableCollection<StageInfoObjectModel> ObjectList = new ObservableCollection<StageInfoObjectModel>();
                        for (int o = 0; o < ObjectCount; o++) {
                            StageInfoObjectModel objEntry = new StageInfoObjectModel();

                            int objectListPtr = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x30);
                            int object_ptr = _ptr + 0x30 + objectListPtr + (0xB0 * o);

                            int PathString_ptr = object_ptr + BinaryReader.b_ReadInt(fileBytes, object_ptr);
                            objEntry.ObjectFilePath = BinaryReader.b_ReadString(fileBytes, PathString_ptr);

                            int MeshString_ptr = 0x8 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x8 + object_ptr);
                            objEntry.ObjectName = BinaryReader.b_ReadString(fileBytes, MeshString_ptr);

                            int PathDmyString_ptr = 0x10 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x10 + object_ptr);
                            objEntry.PositionFilePath = BinaryReader.b_ReadString(fileBytes, PathDmyString_ptr);

                            int DmyString_ptr = 0x18 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x18 + object_ptr);
                            objEntry.PositionBoneName = BinaryReader.b_ReadString(fileBytes, DmyString_ptr);

                            objEntry.EntryType = BinaryReader.b_ReadInt(fileBytes, object_ptr + 0x20);
                            objEntry.AnimationSpeed = BinaryReader.b_ReadFloat(fileBytes, object_ptr + 0x24);
                            objEntry.EnableCameraHideObject = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, object_ptr + 0x28));
                            objEntry.IsRigidBody = (BinaryReader.b_ReadInt(fileBytes, object_ptr + 0x2C) == 1);

                            int BreakableObject_path_ptr = 0x38 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x38 + object_ptr);
                            objEntry.BreakableObjectPath = BinaryReader.b_ReadString(fileBytes, BreakableObject_path_ptr);

                            int BreakableObject_Effect01_ptr = 0x40 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x40 + object_ptr);
                            objEntry.BreakableObjectEffect01 = BinaryReader.b_ReadString(fileBytes, BreakableObject_Effect01_ptr);
                            objEntry.BreakableObjectSpeed01 = BinaryReader.b_ReadFloat(fileBytes, object_ptr + 0x48);

                            int BreakableObject_Effect02_ptr = 0x50 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x50 + object_ptr);
                            objEntry.BreakableObjectEffect02 = BinaryReader.b_ReadString(fileBytes, BreakableObject_Effect02_ptr);
                            objEntry.BreakableObjectSpeed02 = BinaryReader.b_ReadFloat(fileBytes, object_ptr + 0x58);


                            int BreakableObject_Effect03_ptr = 0x60 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x60 + object_ptr);
                            objEntry.BreakableObjectEffect03 = BinaryReader.b_ReadString(fileBytes, BreakableObject_Effect03_ptr);
                            objEntry.BreakableObjectSpeed03 = BinaryReader.b_ReadFloat(fileBytes, object_ptr + 0x68);

                            int BreakableWall_Effect01_ptr = 0x78 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x78 + object_ptr);
                            objEntry.BreakableWallEffect01 = BinaryReader.b_ReadString(fileBytes, BreakableWall_Effect01_ptr);

                            int BreakableWall_Effect02_ptr = 0x88 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x88 + object_ptr);
                            objEntry.BreakableWallEffect02 = BinaryReader.b_ReadString(fileBytes, BreakableWall_Effect02_ptr);

                            int BreakableWall_Effect03_ptr = 0x90 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0x90 + object_ptr);
                            objEntry.BreakableWallEffect03 = BinaryReader.b_ReadString(fileBytes, BreakableWall_Effect03_ptr);

                            objEntry.BreakableWallValue1 = BinaryReader.b_ReadInt(fileBytes, object_ptr + 0x80);
                            objEntry.BreakableWallValue2 = BinaryReader.b_ReadInt(fileBytes, object_ptr + 0x84);
                            objEntry.BreakableWallVolume = BinaryReader.b_ReadFloat(fileBytes, object_ptr + 0x98);

                            int BreakableWall_Sound_ptr = 0xA0 + object_ptr + BinaryReader.b_ReadInt(fileBytes, 0xA0 + object_ptr);
                            objEntry.BreakableWallSound = BinaryReader.b_ReadString(fileBytes, BreakableWall_Sound_ptr);

                            ObjectList.Add(objEntry);
                        }
                        StageInfoEntry.Objects = ObjectList;
                        StageInfoEntry.Weather = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x38);

                        byte[] playerAmbientColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x3C, 4);
                        StageInfoEntry.PlayerAmbientColor = Color.FromArgb(playerAmbientColor[0], playerAmbientColor[1], playerAmbientColor[2], playerAmbientColor[3]);

                        byte[] rayCutOffShadeColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x40, 4);
                        StageInfoEntry.RayCutOffShadeColor = Color.FromArgb(rayCutOffShadeColor[0], rayCutOffShadeColor[1], rayCutOffShadeColor[2], rayCutOffShadeColor[3]);

                        byte[] EffectAmbientColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x44, 4);
                        StageInfoEntry.EffectAmbientColor = Color.FromArgb(EffectAmbientColor[0], EffectAmbientColor[1], EffectAmbientColor[2], EffectAmbientColor[3]);

                        byte[] UnknownColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x48, 4);
                        StageInfoEntry.UnknownColor = Color.FromArgb(UnknownColor[0], UnknownColor[1], UnknownColor[2], UnknownColor[3]);

                        StageInfoEntry.EnableBrightnessAdjustment = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0x4C));
                        StageInfoEntry.Brightness = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x50);
                        StageInfoEntry.Contrast = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x54);
                        StageInfoEntry.EnableLensFlare = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0x58));
                        StageInfoEntry.LensFlare = BinaryReader.b_ReadInt(fileBytes, _ptr + 0x5C);
                        StageInfoEntry.LensFlarePositionX = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x60);
                        StageInfoEntry.LensFlarePositionY = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x64);
                        StageInfoEntry.LensFlarePositionZ = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x68);
                        StageInfoEntry.LensFlareAlpha = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x6C);

                        byte[] ParallelAmbientColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x70, 4);
                        StageInfoEntry.ParallelAmbientColor = Color.FromArgb(ParallelAmbientColor[0], ParallelAmbientColor[1], ParallelAmbientColor[2], ParallelAmbientColor[3]);

                        byte[] RayCutOffNormalColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x74, 4);
                        StageInfoEntry.RayCutOffNormalColor = Color.FromArgb(RayCutOffNormalColor[0], RayCutOffNormalColor[1], RayCutOffNormalColor[2], RayCutOffNormalColor[3]);

                        StageInfoEntry.LightPointDirectionX = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x78);
                        StageInfoEntry.LightPointDirectionY = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x7C);
                        StageInfoEntry.LightPointDirectionZ = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x80);
                        StageInfoEntry.EnableShadowColor = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0x84));

                        byte[] ShadowColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x88, 4);
                        StageInfoEntry.ShadowColor = Color.FromArgb(ShadowColor[0], ShadowColor[1], ShadowColor[2], ShadowColor[3]);

                        StageInfoEntry.EnableFog = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0x8C));
                        StageInfoEntry.FogStartDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x90);
                        StageInfoEntry.FogEndDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x94);
                        StageInfoEntry.FogStrength = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x98);


                        byte[] FogColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x9C, 4);
                        StageInfoEntry.FogColor = Color.FromArgb(FogColor[0], FogColor[1], FogColor[2], FogColor[3]);

                        StageInfoEntry.EnableMonoColorFilter = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xA0));
                        StageInfoEntry.MonoBlueTone = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xA4);
                        StageInfoEntry.MonoRedTone = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xA8);
                        StageInfoEntry.MonoAlpha = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xAC);
                        StageInfoEntry.EnableGlareEffect = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xB0));
                        StageInfoEntry.GlareLuminanceThreshold = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xB4);
                        StageInfoEntry.GlareSubtracted = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xB8);
                        StageInfoEntry.GlareCompositionStrength = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xBC);
                        StageInfoEntry.EnableSoftFocus = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xC4));
                        StageInfoEntry.SoftFocusStrength = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xC8);
                        StageInfoEntry.EnableDOFBlur = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xCC));
                        StageInfoEntry.DOFFocalLength = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xD0);
                        StageInfoEntry.DOFShortDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xD4);
                        StageInfoEntry.DOFLongDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xD8);
                        StageInfoEntry.DOFAlpha = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xDC);
                        StageInfoEntry.EnableDOFEdgeBlur = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xE0));
                        StageInfoEntry.EnableSunShaft = Convert.ToBoolean(BinaryReader.b_ReadInt(fileBytes, _ptr + 0xE4));
                        StageInfoEntry.SunShaftStartDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xE8);
                        StageInfoEntry.SunShaftEndDistance = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xEC);
                        StageInfoEntry.SunShaftAlpha = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xF0);

                        byte[] SunShaftColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0xF4, 4);
                        StageInfoEntry.SunShaftColor = Color.FromArgb(SunShaftColor[0], SunShaftColor[1], SunShaftColor[2], SunShaftColor[3]);

                        StageInfoEntry.SunShaftDirectionX = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xF8);
                        StageInfoEntry.SunShaftDirectionY = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0xFC);
                        StageInfoEntry.SunShaftDirectionZ = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x100);
                        StageInfoEntry.SunShaftBlurWidth = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x104);
                        StageInfoEntry.SunShaftAttenuationCoefficient = BinaryReader.b_ReadFloat(fileBytes, _ptr + 0x108);


                        byte[] RockColor = BinaryReader.b_ReadByteArray(fileBytes, _ptr + 0x10C, 4);
                        StageInfoEntry.RockColor = Color.FromArgb(RockColor[0], RockColor[1], RockColor[2], RockColor[3]);

                        StageInfoList.Add(StageInfoEntry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedStage is not null) {
                StageInfoList.Remove(SelectedStage);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedStage is not null) {
                SelectedStage.StageName = StageName_field;
                SelectedStage.StageMessageID = StageMessageID_field;
                SelectedStage.StageFilter = StageFilter_field;
                SelectedStage.Weather = Weather_field;
                SelectedStage.PlayerAmbientColor = PlayerAmbientColor_field;
                SelectedStage.RayCutOffShadeColor = RayCutOffShadeColor_field;
                SelectedStage.EffectAmbientColor = EffectAmbientColor_field;
                SelectedStage.UnknownColor = UnknownColor_field;
                SelectedStage.EnableBrightnessAdjustment = EnableBrightnessAdjustment_field;
                SelectedStage.Brightness = Brightness_field;
                SelectedStage.Contrast = Contrast_field;
                SelectedStage.EnableLensFlare = EnableLensFlare_field;
                SelectedStage.LensFlare = LensFlare_field;
                SelectedStage.LensFlarePositionX = LensFlarePositionX_field;
                SelectedStage.LensFlarePositionY = LensFlarePositionY_field;
                SelectedStage.LensFlarePositionZ = LensFlarePositionZ_field;
                SelectedStage.LensFlareAlpha = LensFlareAlpha_field;
                SelectedStage.ParallelAmbientColor = ParallelAmbientColor_field;
                SelectedStage.RayCutOffNormalColor = RayCutOffNormalColor_field;
                SelectedStage.LightPointDirectionX = LightPointDirectionX_field;
                SelectedStage.LightPointDirectionY = LightPointDirectionY_field;
                SelectedStage.LightPointDirectionZ = LightPointDirectionZ_field;
                SelectedStage.EnableShadowColor = EnableShadowColor_field;
                SelectedStage.ShadowColor = ShadowColor_field;
                SelectedStage.EnableFog = EnableFog_field;
                SelectedStage.FogStartDistance = FogStartDistance_field;
                SelectedStage.FogEndDistance = FogEndDistance_field;
                SelectedStage.FogStrength = FogStrength_field;
                SelectedStage.FogColor = FogColor_field;
                SelectedStage.EnableMonoColorFilter = EnableMonoColorFilter_field;
                SelectedStage.MonoBlueTone = MonoBlueTone_field;
                SelectedStage.MonoRedTone = MonoRedTone_field;
                SelectedStage.MonoAlpha = MonoAlpha_field;
                SelectedStage.EnableGlareEffect = EnableGlareEffect_field;
                SelectedStage.GlareLuminanceThreshold = GlareLuminanceThreshold_field;
                SelectedStage.GlareSubtracted = GlareSubtracted_field;
                SelectedStage.GlareCompositionStrength = GlareCompositionStrength_field;
                SelectedStage.EnableSoftFocus = EnableSoftFocus_field;
                SelectedStage.SoftFocusStrength = SoftFocusStrength_field;
                SelectedStage.EnableDOFBlur = EnableDOFBlur_field;
                SelectedStage.DOFFocalLength = DOFFocalLength_field;
                SelectedStage.DOFShortDistance = DOFShortDistance_field;
                SelectedStage.DOFLongDistance = DOFLongDistance_field;
                SelectedStage.DOFAlpha = DOFAlpha_field;
                SelectedStage.EnableDOFEdgeBlur = EnableDOFEdgeBlur_field;
                SelectedStage.EnableSunShaft = EnableSunShaft_field;
                SelectedStage.SunShaftStartDistance = SunShaftStartDistance_field;
                SelectedStage.SunShaftEndDistance = SunShaftEndDistance_field;
                SelectedStage.SunShaftAlpha = SunShaftAlpha_field;
                SelectedStage.SunShaftColor = SunShaftColor_field;
                SelectedStage.SunShaftDirectionX = SunShaftDirectionX_field;
                SelectedStage.SunShaftDirectionY = SunShaftDirectionY_field;
                SelectedStage.SunShaftDirectionZ = SunShaftDirectionZ_field;
                SelectedStage.SunShaftBlurWidth = SunShaftBlurWidth_field;
                SelectedStage.SunShaftAttenuationCoefficient = SunShaftAttenuationCoefficient_field;
                SelectedStage.RockColor = RockColor_field;

                ObservableCollection<StageInfoFilePathModel> newFilePathList = new ObservableCollection<StageInfoFilePathModel>();

                for (int x = 0; x<SelectedStage.FilePaths.Count; x++) {
                    if (!SelectedStage.FilePaths[x].FilePath.Contains("rain")
                        && !SelectedStage.FilePaths[x].FilePath.Contains("snow")
                        && !SelectedStage.FilePaths[x].FilePath.Contains("lensFlare")) {
                        newFilePathList.Add((StageInfoFilePathModel)SelectedStage.FilePaths[x].Clone());
                    }
                }

                if (SelectedStage.Weather != 0) {
                    StageInfoFilePathModel path = new StageInfoFilePathModel();
                    switch (SelectedStage.Weather) {
                        case 1:
                            path.FilePath = "data/stage/sae_snow.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 2:
                            path.FilePath = "data/stage/sae_rain.xfbin";
                            newFilePathList.Add(path);
                            break;
                    }
                }
                if (SelectedStage.EnableLensFlare) {
                    StageInfoFilePathModel path = new StageInfoFilePathModel();
                    switch (SelectedStage.LensFlare) {
                        case 0:
                            path.FilePath = "data/stage/lensFlare/uviolet_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 1:
                            path.FilePath = "data/stage/lensFlare/oprism_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 2:
                            path.FilePath = "data/stage/lensFlare/phalo_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 3:
                            path.FilePath = "data/stage/lensFlare/gpurpose_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 4:
                            path.FilePath = "data/stage/lensFlare/mlight_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 5:
                            path.FilePath = "data/stage/lensFlare/sunset_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                    }
                }
                SelectedStage.FilePaths = newFilePathList;
                FilePathList = SelectedStage.FilePaths;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchStringIndex(ObservableCollection<StageInfoModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].StageName;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x) {
                    return x;
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (StageTextBoxString is not null) {
                if (SearchStringIndex(StageInfoList, StageTextBoxString, SelectedStageIndex) != -1) {
                    SelectedStageIndex = SearchStringIndex(StageInfoList, StageTextBoxString, SelectedStageIndex);
                    CollectionViewSource.GetDefaultView(StageInfoList).MoveCurrentTo(SelectedStage);
                } else {
                    if (SearchStringIndex(StageInfoList, StageTextBoxString, 0) != -1) {
                        SelectedStageIndex = SearchStringIndex(StageInfoList, StageTextBoxString, -1);
                        CollectionViewSource.GetDefaultView(StageInfoList).MoveCurrentTo(SelectedStage);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }

        public void AddDupEntry() {
            StageInfoModel StageEntry = new StageInfoModel();
            if (SelectedStage is not null) {
                StageEntry = (StageInfoModel)SelectedStage.Clone();
            } else {

                StageEntry.StageName = "STAGE_";
                StageEntry.StageMessageID = "";
                StageEntry.StageFilter = "";
                StageEntry.FilePaths = new ObservableCollection<StageInfoFilePathModel>();
                StageEntry.Objects = new ObservableCollection<StageInfoObjectModel>();
                StageEntry.Weather = 0;
                StageEntry.PlayerAmbientColor = Color.FromArgb(255,0,0,0);
                StageEntry.RayCutOffShadeColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.EffectAmbientColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.UnknownColor = Color.FromArgb(255, 255, 255, 255);
                StageEntry.EnableBrightnessAdjustment = false;
                StageEntry.Brightness = 0;
                StageEntry.Contrast = 0;
                StageEntry.EnableLensFlare = false;
                StageEntry.LensFlare = 0;
                StageEntry.LensFlarePositionX = 0;
                StageEntry.LensFlarePositionY = 0;
                StageEntry.LensFlarePositionZ = 0;
                StageEntry.LensFlareAlpha = 1;
                StageEntry.ParallelAmbientColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.RayCutOffNormalColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.LightPointDirectionX = 0;
                StageEntry.LightPointDirectionY = 0;
                StageEntry.LightPointDirectionZ = 0;
                StageEntry.EnableShadowColor = false;
                StageEntry.ShadowColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.EnableFog = false;
                StageEntry.FogStartDistance = 0;
                StageEntry.FogEndDistance = 0;
                StageEntry.FogStrength = 0;
                StageEntry.FogColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.EnableMonoColorFilter = false;
                StageEntry.MonoBlueTone = 0;
                StageEntry.MonoRedTone = 0;
                StageEntry.MonoAlpha = 0.3f;
                StageEntry.EnableGlareEffect = false;
                StageEntry.GlareLuminanceThreshold = 0;
                StageEntry.GlareSubtracted = 0;
                StageEntry.GlareCompositionStrength = 0;
                StageEntry.EnableSoftFocus = false;
                StageEntry.SoftFocusStrength = 0;
                StageEntry.EnableDOFBlur = false;
                StageEntry.DOFFocalLength = 0;
                StageEntry.DOFShortDistance = 0;
                StageEntry.DOFLongDistance = 0;
                StageEntry.DOFAlpha = 0;
                StageEntry.EnableDOFEdgeBlur = false;
                StageEntry.EnableSunShaft = false;
                StageEntry.SunShaftStartDistance = 0;
                StageEntry.SunShaftEndDistance = 0;
                StageEntry.SunShaftAlpha = 0;
                StageEntry.SunShaftColor = Color.FromArgb(255, 0, 0, 0);
                StageEntry.SunShaftDirectionX = 0;
                StageEntry.SunShaftDirectionY = 0;
                StageEntry.SunShaftDirectionZ = 0;
                StageEntry.SunShaftBlurWidth = 0;
                StageEntry.SunShaftAttenuationCoefficient = 0;
                StageEntry.RockColor = Color.FromArgb(255, 0, 0, 0);
            }
            StageInfoList.Add(StageEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void CopyEntry() {
            if (SelectedStage is not null) {
                CopiedStageProperties = (StageInfoModel)SelectedStage.Clone();
                ModernWpf.MessageBox.Show("Stage properties were copied!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void PasteEntry() {
            if (SelectedStage is not null) {
                SelectedStage.Weather = CopiedStageProperties.Weather;
                SelectedStage.PlayerAmbientColor = CopiedStageProperties.PlayerAmbientColor;
                SelectedStage.RayCutOffShadeColor = CopiedStageProperties.RayCutOffShadeColor;
                SelectedStage.EffectAmbientColor = CopiedStageProperties.EffectAmbientColor;
                SelectedStage.UnknownColor = CopiedStageProperties.UnknownColor;
                SelectedStage.EnableBrightnessAdjustment = CopiedStageProperties.EnableBrightnessAdjustment;
                SelectedStage.Brightness = CopiedStageProperties.Brightness;
                SelectedStage.Contrast = CopiedStageProperties.Contrast;
                SelectedStage.EnableLensFlare = CopiedStageProperties.EnableLensFlare;
                SelectedStage.LensFlare = CopiedStageProperties.LensFlare;
                SelectedStage.LensFlarePositionX = CopiedStageProperties.LensFlarePositionX;
                SelectedStage.LensFlarePositionY = CopiedStageProperties.LensFlarePositionY;
                SelectedStage.LensFlarePositionZ = CopiedStageProperties.LensFlarePositionZ;
                SelectedStage.LensFlareAlpha = CopiedStageProperties.LensFlareAlpha;
                SelectedStage.ParallelAmbientColor = CopiedStageProperties.ParallelAmbientColor;
                SelectedStage.RayCutOffNormalColor = CopiedStageProperties.RayCutOffNormalColor;
                SelectedStage.LightPointDirectionX = CopiedStageProperties.LightPointDirectionX;
                SelectedStage.LightPointDirectionY = CopiedStageProperties.LightPointDirectionY;
                SelectedStage.LightPointDirectionZ = CopiedStageProperties.LightPointDirectionZ;
                SelectedStage.EnableShadowColor = CopiedStageProperties.EnableShadowColor;
                SelectedStage.ShadowColor = CopiedStageProperties.ShadowColor;
                SelectedStage.EnableFog = CopiedStageProperties.EnableFog;
                SelectedStage.FogStartDistance = CopiedStageProperties.FogStartDistance;
                SelectedStage.FogEndDistance = CopiedStageProperties.FogEndDistance;
                SelectedStage.FogStrength = CopiedStageProperties.FogStrength;
                SelectedStage.FogColor = CopiedStageProperties.FogColor;
                SelectedStage.EnableMonoColorFilter = CopiedStageProperties.EnableMonoColorFilter;
                SelectedStage.MonoBlueTone = CopiedStageProperties.MonoBlueTone;
                SelectedStage.MonoRedTone = CopiedStageProperties.MonoRedTone;
                SelectedStage.MonoAlpha = CopiedStageProperties.MonoAlpha;
                SelectedStage.EnableGlareEffect = CopiedStageProperties.EnableGlareEffect;
                SelectedStage.GlareLuminanceThreshold = CopiedStageProperties.GlareLuminanceThreshold;
                SelectedStage.GlareSubtracted = CopiedStageProperties.GlareSubtracted;
                SelectedStage.GlareCompositionStrength = CopiedStageProperties.GlareCompositionStrength;
                SelectedStage.EnableSoftFocus = CopiedStageProperties.EnableSoftFocus;
                SelectedStage.SoftFocusStrength = CopiedStageProperties.SoftFocusStrength;
                SelectedStage.EnableDOFBlur = CopiedStageProperties.EnableDOFBlur;
                SelectedStage.DOFFocalLength = CopiedStageProperties.DOFFocalLength;
                SelectedStage.DOFShortDistance = CopiedStageProperties.DOFShortDistance;
                SelectedStage.DOFLongDistance = CopiedStageProperties.DOFLongDistance;
                SelectedStage.DOFAlpha = CopiedStageProperties.DOFAlpha;
                SelectedStage.EnableDOFEdgeBlur = CopiedStageProperties.EnableDOFEdgeBlur;
                SelectedStage.EnableSunShaft = CopiedStageProperties.EnableSunShaft;
                SelectedStage.SunShaftStartDistance = CopiedStageProperties.SunShaftStartDistance;
                SelectedStage.SunShaftEndDistance = CopiedStageProperties.SunShaftEndDistance;
                SelectedStage.SunShaftAlpha = CopiedStageProperties.SunShaftAlpha;
                SelectedStage.SunShaftColor = CopiedStageProperties.SunShaftColor;
                SelectedStage.SunShaftDirectionX = CopiedStageProperties.SunShaftDirectionX;
                SelectedStage.SunShaftDirectionY = CopiedStageProperties.SunShaftDirectionY;
                SelectedStage.SunShaftDirectionZ = CopiedStageProperties.SunShaftDirectionZ;
                SelectedStage.SunShaftBlurWidth = CopiedStageProperties.SunShaftBlurWidth;
                SelectedStage.SunShaftAttenuationCoefficient = CopiedStageProperties.SunShaftAttenuationCoefficient;
                SelectedStage.RockColor = CopiedStageProperties.RockColor;
                ObservableCollection<StageInfoFilePathModel> newFilePathList = new ObservableCollection<StageInfoFilePathModel>();

                for (int x = 0; x < SelectedStage.FilePaths.Count; x++) {
                    if (!SelectedStage.FilePaths[x].FilePath.Contains("rain")
                        && !SelectedStage.FilePaths[x].FilePath.Contains("snow")
                        && !SelectedStage.FilePaths[x].FilePath.Contains("lensFlare")) {
                        newFilePathList.Add((StageInfoFilePathModel)SelectedStage.FilePaths[x].Clone());
                    }
                }

                if (CopiedStageProperties.Weather != 0) {
                    StageInfoFilePathModel path = new StageInfoFilePathModel();
                    switch (CopiedStageProperties.Weather) {
                        case 1:
                            path.FilePath = "data/stage/sae_snow.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 2:
                            path.FilePath = "data/stage/sae_rain.xfbin";
                            newFilePathList.Add(path);
                            break;
                    }
                }
                if (CopiedStageProperties.EnableLensFlare) {
                    StageInfoFilePathModel path = new StageInfoFilePathModel();
                    switch (CopiedStageProperties.LensFlare) {
                        case 0:
                            path.FilePath = "data/stage/lensFlare/uviolet_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 1:
                            path.FilePath = "data/stage/lensFlare/oprism_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 2:
                            path.FilePath = "data/stage/lensFlare/phalo_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 3:
                            path.FilePath = "data/stage/lensFlare/gpurpose_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 4:
                            path.FilePath = "data/stage/lensFlare/mlight_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                        case 5:
                            path.FilePath = "data/stage/lensFlare/sunset_lensFlare.xfbin";
                            newFilePathList.Add(path);
                            break;
                    }
                }
                SelectedStage.FilePaths = newFilePathList;

                StageName_field = SelectedStage.StageName;
                StageMessageID_field = SelectedStage.StageMessageID;
                StageFilter_field = SelectedStage.StageFilter;
                FilePathList = SelectedStage.FilePaths;
                ObjectList = SelectedStage.Objects;
                Weather_field = SelectedStage.Weather;
                PlayerAmbientColor_field = SelectedStage.PlayerAmbientColor;
                RayCutOffShadeColor_field = SelectedStage.RayCutOffShadeColor;
                EffectAmbientColor_field = SelectedStage.EffectAmbientColor;
                UnknownColor_field = SelectedStage.UnknownColor;
                EnableBrightnessAdjustment_field = SelectedStage.EnableBrightnessAdjustment;
                Brightness_field = SelectedStage.Brightness;
                Contrast_field = SelectedStage.Contrast;
                EnableLensFlare_field = SelectedStage.EnableLensFlare;
                LensFlare_field = SelectedStage.LensFlare;
                LensFlarePositionX_field = SelectedStage.LensFlarePositionX;
                LensFlarePositionY_field = SelectedStage.LensFlarePositionY;
                LensFlarePositionZ_field = SelectedStage.LensFlarePositionZ;
                LensFlareAlpha_field = SelectedStage.LensFlareAlpha;
                ParallelAmbientColor_field = SelectedStage.ParallelAmbientColor;
                RayCutOffNormalColor_field = SelectedStage.RayCutOffNormalColor;
                LightPointDirectionX_field = SelectedStage.LightPointDirectionX;
                LightPointDirectionY_field = SelectedStage.LightPointDirectionY;
                LightPointDirectionZ_field = SelectedStage.LightPointDirectionZ;
                EnableShadowColor_field = SelectedStage.EnableShadowColor;
                ShadowColor_field = SelectedStage.ShadowColor;
                EnableFog_field = SelectedStage.EnableFog;
                FogStartDistance_field = SelectedStage.FogStartDistance;
                FogEndDistance_field = SelectedStage.FogEndDistance;
                FogStrength_field = SelectedStage.FogStrength;
                FogColor_field = SelectedStage.FogColor;
                EnableMonoColorFilter_field = SelectedStage.EnableMonoColorFilter;
                MonoBlueTone_field = SelectedStage.MonoBlueTone;
                MonoRedTone_field = SelectedStage.MonoRedTone;
                MonoAlpha_field = SelectedStage.MonoAlpha;
                EnableGlareEffect_field = SelectedStage.EnableGlareEffect;
                GlareLuminanceThreshold_field = SelectedStage.GlareLuminanceThreshold;
                GlareSubtracted_field = SelectedStage.GlareSubtracted;
                GlareCompositionStrength_field = SelectedStage.GlareCompositionStrength;
                EnableSoftFocus_field = SelectedStage.EnableSoftFocus;
                SoftFocusStrength_field = SelectedStage.SoftFocusStrength;
                EnableDOFBlur_field = SelectedStage.EnableDOFBlur;
                DOFFocalLength_field = SelectedStage.DOFFocalLength;
                DOFShortDistance_field = SelectedStage.DOFShortDistance;
                DOFLongDistance_field = SelectedStage.DOFLongDistance;
                DOFAlpha_field = SelectedStage.DOFAlpha;
                EnableDOFEdgeBlur_field = SelectedStage.EnableDOFEdgeBlur;
                EnableSunShaft_field = SelectedStage.EnableSunShaft;
                SunShaftStartDistance_field = SelectedStage.SunShaftStartDistance;
                SunShaftEndDistance_field = SelectedStage.SunShaftEndDistance;
                SunShaftAlpha_field = SelectedStage.SunShaftAlpha;
                SunShaftColor_field = SelectedStage.SunShaftColor;
                SunShaftDirectionX_field = SelectedStage.SunShaftDirectionX;
                SunShaftDirectionY_field = SelectedStage.SunShaftDirectionY;
                SunShaftDirectionZ_field = SelectedStage.SunShaftDirectionZ;
                SunShaftBlurWidth_field = SelectedStage.SunShaftBlurWidth;
                SunShaftAttenuationCoefficient_field = SelectedStage.SunShaftAttenuationCoefficient;
                RockColor_field = SelectedStage.RockColor;
                ModernWpf.MessageBox.Show("Properties were pasted!");

            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }

        public void RemovePathEntry() {
            if (SelectedStage is not null && SelectedFilePath is not null) {
                if (ObjectPathComboBoxIndex == StagePathComboBoxList.IndexOf(SelectedFilePath.FilePath)) {
                    ObjectPathComboBoxIndex = 0;
                }
                if (ObjectPositionPathComboBoxIndex == StagePathComboBoxList.IndexOf(SelectedFilePath.FilePath)) {
                    ObjectPositionPathComboBoxIndex = 0;
                }
                if (BreakableObjectPathComboBoxIndex == StagePathComboBoxList.IndexOf(SelectedFilePath.FilePath)) {
                    BreakableObjectPathComboBoxIndex = 0;
                }

                for (int c = 0; c< SelectedStage.Objects.Count; c++) {
                    if (SelectedStage.Objects[c].ObjectFilePath == SelectedFilePath.FilePath)
                        SelectedStage.Objects[c].ObjectFilePath = "";
                    if (SelectedStage.Objects[c].PositionFilePath == SelectedFilePath.FilePath)
                        SelectedStage.Objects[c].ObjectFilePath = "";
                    if (SelectedStage.Objects[c].BreakableObjectPath == SelectedFilePath.FilePath)
                        SelectedStage.Objects[c].ObjectFilePath = "";
                }
                StagePathComboBoxList.RemoveAt(StagePathComboBoxList.IndexOf(SelectedFilePath.FilePath));
                FilePathList.Remove(SelectedFilePath);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SavePathEntry() {
            if (SelectedStage is not null && SelectedFilePath is not null) {
                SelectedFilePath.FilePath = FilePathTextBox_field;
                StagePathComboBoxList[StagePathComboBoxList.IndexOf(SelectedFilePath.FilePath) + 1] = FilePathTextBox_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void AddPathEntry() {
            if (SelectedStage is not null && FilePathTextBox_field is not null) {
                if (!StagePathComboBoxList.Contains(FilePathTextBox_field) && FilePathTextBox_field.Length > 0) {
                    StageInfoFilePathModel PathEntry = new StageInfoFilePathModel();
                    PathEntry.FilePath = FilePathTextBox_field;
                    FilePathList.Add(PathEntry);
                    StagePathComboBoxList.Add(FilePathTextBox_field);
                    ModernWpf.MessageBox.Show("Entry was added!");

                } else
                    ModernWpf.MessageBox.Show("Path already exist in list!");
            }
        }
        public void RemoveObjectEntry() {
            if (SelectedStage is not null && SelectedObject is not null) {
                ObjectList.Remove(SelectedObject);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveObjectEntry() {
            if (SelectedStage is not null && SelectedObject is not null) {

                SelectedObject.ObjectFilePath = StagePathComboBoxList[ObjectPathComboBoxIndex];
                SelectedObject.ObjectName = ObjectName_field;
                SelectedObject.PositionFilePath = StagePathComboBoxList[ObjectPositionPathComboBoxIndex];
                SelectedObject.PositionBoneName = PositionBoneName_field;
                SelectedObject.EntryType = EntryType_field;
                SelectedObject.EnableCameraHideObject = EnableCameraHideObject_field;
                SelectedObject.IsRigidBody = IsRigidBody_field;
                SelectedObject.AnimationSpeed = AnimationSpeed_field;
                SelectedObject.BreakableWallValue1 = BreakableWallValue1_field;
                SelectedObject.BreakableWallValue2 = BreakableWallValue2_field;
                SelectedObject.BreakableWallEffect01 = BreakableWallEffect01_field;
                SelectedObject.BreakableWallEffect02 = BreakableWallEffect02_field;
                SelectedObject.BreakableWallEffect03 = BreakableWallEffect03_field;
                SelectedObject.BreakableWallSound = BreakableWallSound_field;
                SelectedObject.BreakableWallVolume = BreakableWallVolume_field;
                SelectedObject.BreakableObjectPath = StagePathComboBoxList[BreakableObjectPathComboBoxIndex];
                SelectedObject.BreakableObjectEffect01 = BreakableObjectEffect01_field;
                SelectedObject.BreakableObjectEffect02 = BreakableObjectEffect02_field;
                SelectedObject.BreakableObjectEffect03 = BreakableObjectEffect03_field;
                SelectedObject.BreakableObjectSpeed01 = BreakableObjectSpeed01_field;
                SelectedObject.BreakableObjectSpeed02 = BreakableObjectSpeed02_field;
                SelectedObject.BreakableObjectSpeed03 = BreakableObjectSpeed03_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void AddObjectEntry() {
            //if (SelectedStage is not null && SelectedObject is not null) {
            //    ObjectEntry = (StageInfoObjectModel)SelectedObject.Clone();
            //} else
            if (SelectedStage is not null) {
                StageInfoObjectModel ObjectEntry = new StageInfoObjectModel();
                ObjectEntry.ObjectFilePath = "";
                ObjectEntry.ObjectName = "clump_anim_name";
                ObjectEntry.PositionFilePath = "";
                ObjectEntry.PositionBoneName = "";
                ObjectEntry.EntryType = 0;
                ObjectEntry.EnableCameraHideObject = false;
                ObjectEntry.IsRigidBody = false;
                ObjectEntry.AnimationSpeed = 1;
                ObjectEntry.BreakableWallValue1 = 0;
                ObjectEntry.BreakableWallValue2 = 0;
                ObjectEntry.BreakableWallEffect01 = "";
                ObjectEntry.BreakableWallEffect02 = "";
                ObjectEntry.BreakableWallEffect03 = "";
                ObjectEntry.BreakableWallSound = "";
                ObjectEntry.BreakableWallVolume = 1;
                ObjectEntry.BreakableObjectPath = "";
                ObjectEntry.BreakableObjectEffect01 = "";
                ObjectEntry.BreakableObjectEffect02 = "";
                ObjectEntry.BreakableObjectEffect03 = "";
                ObjectEntry.BreakableObjectSpeed01 = 0;
                ObjectEntry.BreakableObjectSpeed02 = 0;
                ObjectEntry.BreakableObjectSpeed03 = 0;
                ObjectList.Add(ObjectEntry);
                ModernWpf.MessageBox.Show("Entry was added!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
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
            if (FileBinName != "")
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/" + FileBinName + ".bin");
            else
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/stageInfo.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            if (FileBinName != "" && FileBinName is not null)
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, FileBinName);
            else
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, "stageInfo");
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
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length + 0x4;


            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xF2, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[StageInfoList.Count * 0x130]);
            List<int> FilePathEntry_pointer = new List<int>();
            List<int> ObjectEntry_pointer = new List<int>();
            List<int> StageName_pointer = new List<int>();
            List<int> StageMessageID_pointer = new List<int>();
            List<int> StageFilter_pointer = new List<int>();

            for (int x = 0; x < StageInfoList.Count; x++)
            {
                // StageName
                StageName_pointer.Add(fileBytes36.Length);
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].StageName);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                // StageMessageID
                StageMessageID_pointer.Add(fileBytes36.Length);
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].StageMessageID);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                // StageFilter
                StageFilter_pointer.Add(fileBytes36.Length);
                fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].StageFilter);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                // FilePaths
                FilePathEntry_pointer.Add(fileBytes36.Length);
                for (int i = 0; i < StageInfoList[x].FilePaths.Count; i++)
                {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x8]);
                }

                // Objects
                ObjectEntry_pointer.Add(fileBytes36.Length);
                for (int i = 0; i < StageInfoList[x].Objects.Count; i++)
                {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0xB0]);
                }

                byte[] stageEntry = new byte[0x130];
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageName_pointer[x] - startPtr - (0x130 * x)), 0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageMessageID_pointer[x] - startPtr - (0x130 * x) - 0x08), 0x08);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageFilter_pointer[x] - startPtr - (0x130 * x) - 0x10), 0x10);

                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].FilePaths.Count), 0x18);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(FilePathEntry_pointer[x] - startPtr - (0x130 * x) - 0x20), 0x20);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].Objects.Count), 0x28);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(ObjectEntry_pointer[x] - startPtr - (0x130 * x) - 0x30), 0x30);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].Weather), 0x38);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].PlayerAmbientColor.A, StageInfoList[x].PlayerAmbientColor.R, StageInfoList[x].PlayerAmbientColor.G, StageInfoList[x].PlayerAmbientColor.B }, 0x3C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].RayCutOffShadeColor.A, StageInfoList[x].RayCutOffShadeColor.R, StageInfoList[x].RayCutOffShadeColor.G, StageInfoList[x].RayCutOffShadeColor.B }, 0x40);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].EffectAmbientColor.A, StageInfoList[x].EffectAmbientColor.R, StageInfoList[x].EffectAmbientColor.G, StageInfoList[x].EffectAmbientColor.B }, 0x44);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].UnknownColor.A, StageInfoList[x].UnknownColor.R, StageInfoList[x].UnknownColor.G, StageInfoList[x].UnknownColor.B }, 0x48);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableBrightnessAdjustment), 0x4C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].Brightness), 0x50);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].Contrast), 0x54);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableLensFlare), 0x58);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LensFlare), 0x5C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LensFlarePositionX), 0x60);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LensFlarePositionY), 0x64);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LensFlarePositionZ), 0x68);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LensFlareAlpha), 0x6C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].ParallelAmbientColor.A, StageInfoList[x].ParallelAmbientColor.R, StageInfoList[x].ParallelAmbientColor.G, StageInfoList[x].ParallelAmbientColor.B }, 0x70);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].RayCutOffNormalColor.A, StageInfoList[x].RayCutOffNormalColor.R, StageInfoList[x].RayCutOffNormalColor.G, StageInfoList[x].RayCutOffNormalColor.B }, 0x74);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LightPointDirectionX), 0x78);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LightPointDirectionY), 0x7C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].LightPointDirectionZ), 0x80);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableShadowColor), 0x84);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].ShadowColor.A, StageInfoList[x].ShadowColor.R, StageInfoList[x].ShadowColor.G, StageInfoList[x].ShadowColor.B }, 0x88);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableFog), 0x8C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].FogStartDistance), 0x90);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].FogEndDistance), 0x94);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].FogStrength), 0x98);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].FogColor.A, StageInfoList[x].FogColor.R, StageInfoList[x].FogColor.G, StageInfoList[x].FogColor.B }, 0x9C);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableMonoColorFilter), 0xA0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].MonoBlueTone), 0xA4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].MonoRedTone), 0xA8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].MonoAlpha), 0xAC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableGlareEffect), 0xB0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].GlareLuminanceThreshold), 0xB4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].GlareSubtracted), 0xB8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].GlareCompositionStrength), 0xBC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableSoftFocus), 0xC4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SoftFocusStrength), 0xC8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableDOFBlur), 0xCC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].DOFFocalLength), 0xD0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].DOFShortDistance), 0xD4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].DOFLongDistance), 0xD8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].DOFAlpha), 0xDC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableDOFEdgeBlur), 0xE0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].EnableSunShaft), 0xE4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftStartDistance), 0xE8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftEndDistance), 0xEC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftAlpha), 0xF0);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].SunShaftColor.A, StageInfoList[x].SunShaftColor.R, StageInfoList[x].SunShaftColor.G, StageInfoList[x].SunShaftColor.B }, 0xF4);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftDirectionX), 0xF8);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftDirectionY), 0xFC);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftDirectionZ), 0x100);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftBlurWidth), 0x104);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, BitConverter.GetBytes(StageInfoList[x].SunShaftAttenuationCoefficient), 0x108);
                stageEntry = BinaryReader.b_ReplaceBytes(stageEntry, new byte[4] { StageInfoList[x].RockColor.A, StageInfoList[x].RockColor.R, StageInfoList[x].RockColor.G, StageInfoList[x].RockColor.B }, 0x10C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, stageEntry, startPtr + (x * 0x130));

                //Paths
                for (int i = 0; i < StageInfoList[x].FilePaths.Count; i++)
                {
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - FilePathEntry_pointer[x] - (i * 0x08)), FilePathEntry_pointer[x] + (i * 0x08));
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].FilePaths[i].FilePath);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                }

                //Objects
                for (int i = 0; i < StageInfoList[x].Objects.Count; i++)
                {

                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0)), ObjectEntry_pointer[x] + (i * 0xB0));
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].ObjectFilePath);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x08), ObjectEntry_pointer[x] + (i * 0xB0) + 0x08);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].ObjectName);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x10), ObjectEntry_pointer[x] + (i * 0xB0) + 0x10);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].PositionFilePath);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x18), ObjectEntry_pointer[x] + (i * 0xB0) + 0x18);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].PositionBoneName);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].EntryType), ObjectEntry_pointer[x] + (i * 0xB0) + 0x20);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].AnimationSpeed), ObjectEntry_pointer[x] + (i * 0xB0) + 0x24);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].EnableCameraHideObject), ObjectEntry_pointer[x] + (i * 0xB0) + 0x28);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].IsRigidBody), ObjectEntry_pointer[x] + (i * 0xB0) + 0x2C);

                    //breakable object
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x38), ObjectEntry_pointer[x] + (i * 0xB0) + 0x38);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableObjectPath);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x40), ObjectEntry_pointer[x] + (i * 0xB0) + 0x40);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableObjectEffect01);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableObjectSpeed01), ObjectEntry_pointer[x] + (i * 0xB0) + 0x48);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x50), ObjectEntry_pointer[x] + (i * 0xB0) + 0x50);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableObjectEffect02);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableObjectSpeed02), ObjectEntry_pointer[x] + (i * 0xB0) + 0x58);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x60), ObjectEntry_pointer[x] + (i * 0xB0) + 0x60);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableObjectEffect03);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableObjectSpeed03), ObjectEntry_pointer[x] + (i * 0xB0) + 0x68);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0x3C), ObjectEntry_pointer[x] + (i * 0xB0) + 0x70);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0x78), ObjectEntry_pointer[x] + (i * 0xB0) + 0x74);

                    //breakable wall
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x78), ObjectEntry_pointer[x] + (i * 0xB0) + 0x78);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableWallEffect01);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableWallValue1), ObjectEntry_pointer[x] + (i * 0xB0) + 0x80);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableWallValue2), ObjectEntry_pointer[x] + (i * 0xB0) + 0x84);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x88), ObjectEntry_pointer[x] + (i * 0xB0) + 0x88);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableWallEffect02);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0x90), ObjectEntry_pointer[x] + (i * 0xB0) + 0x90);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableWallEffect03);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList[x].Objects[i].BreakableWallVolume), ObjectEntry_pointer[x] + (i * 0xB0) + 0x98);
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - ObjectEntry_pointer[x] - (i * 0xB0) - 0xA0), ObjectEntry_pointer[x] + (i * 0xB0) + 0xA0);
                    fileBytes36 = BinaryReader.b_AddString(fileBytes36, StageInfoList[x].Objects[i].BreakableWallSound);
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                }
            }





            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - startPtr + 0x14), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - startPtr + 0x10), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(StageInfoList.Count), count_index);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[20]
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
                0x79, 0xE9, 0x77,
                0,
                0,
                0,
                4,
                0,
                0,
                0,
                0
            });
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
        private RelayCommand _searchEntryCommand;
        public RelayCommand SearchEntryCommand {
            get {
                return _searchEntryCommand ??
                  (_searchEntryCommand = new RelayCommand(obj => {
                      SearchEntryAsync();
                  }));
            }
        }
        private RelayCommand _addPathEntryCommand;
        public RelayCommand AddPathEntryCommand {
            get {
                return _addPathEntryCommand ??
                  (_addPathEntryCommand = new RelayCommand(obj => {
                      AddPathEntryAsync();
                  }));
            }
        }
        private RelayCommand _savePathEntryCommand;
        public RelayCommand SavePathEntryCommand {
            get {
                return _savePathEntryCommand ??
                  (_savePathEntryCommand = new RelayCommand(obj => {
                      SavePathEntryAsync();
                  }));
            }
        }
        private RelayCommand _removePathEntryCommand;
        public RelayCommand RemovePathEntryCommand {
            get {
                return _removePathEntryCommand ??
                  (_removePathEntryCommand = new RelayCommand(obj => {
                      RemovePathEntryAsync();
                  }));
            }
        }
        private RelayCommand _addObjectEntryCommand;
        public RelayCommand AddObjectEntryCommand {
            get {
                return _addObjectEntryCommand ??
                  (_addObjectEntryCommand = new RelayCommand(obj => {
                      AddObjectEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveObjectEntryCommand;
        public RelayCommand SaveObjectEntryCommand {
            get {
                return _saveObjectEntryCommand ??
                  (_saveObjectEntryCommand = new RelayCommand(obj => {
                      SaveObjectEntryAsync();
                  }));
            }
        }
        private RelayCommand _removeObjectEntryCommand;
        public RelayCommand RemoveObjectEntryCommand {
            get {
                return _removeObjectEntryCommand ??
                  (_removeObjectEntryCommand = new RelayCommand(obj => {
                      RemoveObjectEntryAsync();
                  }));
            }
        }
        private RelayCommand _copyEntryCommand;
        public RelayCommand CopyEntryCommand {
            get {
                return _copyEntryCommand ??
                  (_copyEntryCommand = new RelayCommand(obj => {
                      CopyEntryAsync();
                  }));
            }
        }
        private RelayCommand _pasteEntryCommand;
        public RelayCommand PasteEntryCommand {
            get {
                return _pasteEntryCommand ??
                  (_pasteEntryCommand = new RelayCommand(obj => {
                      PasteEntryAsync();
                  }));
            }
        }
        public async void SaveFileAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFile()));

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
        public async void AddPathEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddPathEntry()));

        }
        public async void SavePathEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SavePathEntry()));

        }
        public async void RemovePathEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemovePathEntry()));

        }
        public async void AddObjectEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddObjectEntry()));

        }
        public async void SaveObjectEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveObjectEntry()));

        }
        public async void RemoveObjectEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveObjectEntry()));

        }
        public async void CopyEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => CopyEntry()));

        }
        public async void PasteEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => PasteEntry()));

        }

        public async Task ConvertFileAsync(bool skip_message) {
            await Task.Run(() => ConvertToFile());
            if (!skip_message)
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
