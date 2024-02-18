using DynamicData;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace NSC_ModManager.Model {
    public class StageInfoModel : ICloneable, INotifyPropertyChanged {
        private string _stageName;
        public string StageName {
            get { return _stageName; }
            set {
                _stageName = value;
                OnPropertyChanged("StageName");
            }
        }
        private string _stageMessageID;
        public string StageMessageID {
            get { return _stageMessageID; }
            set {
                _stageMessageID = value;
                OnPropertyChanged("StageMessageID");
            }
        }
        private string _stageFilter;
        public string StageFilter {
            get { return _stageFilter; }
            set {
                _stageFilter = value;
                OnPropertyChanged("StageFilter");
            }
        }
        private int _weather;
        public int Weather {
            get { return _weather; }
            set {
                _weather = value;
                OnPropertyChanged("Weather");
            }
        }
        private Color _playerAmbientColor;
        public Color PlayerAmbientColor {
            get { return _playerAmbientColor; }
            set {
                _playerAmbientColor = value;
                OnPropertyChanged("PlayerAmbientColor");
            }
        }
        private Color _rayCutOffShadeColor;
        public Color RayCutOffShadeColor {
            get { return _rayCutOffShadeColor; }
            set {
                _rayCutOffShadeColor = value;
                OnPropertyChanged("RayCutOffShadeColor");
            }
        }
        private Color _effectAmbientColor;
        public Color EffectAmbientColor {
            get { return _effectAmbientColor; }
            set {
                _effectAmbientColor = value;
                OnPropertyChanged("EffectAmbientColor");
            }
        }
        private Color _unknownColor;
        public Color UnknownColor {
            get { return _unknownColor; }
            set {
                _unknownColor = value;
                OnPropertyChanged("UnknownColor");
            }
        }
        private bool _enableBrightnessAdjustment;
        public bool EnableBrightnessAdjustment {
            get { return _enableBrightnessAdjustment; }
            set {
                _enableBrightnessAdjustment = value;
                OnPropertyChanged("EnableBrightnessAdjustment");
            }
        }
        private float _brightness;
        public float Brightness {
            get { return _brightness; }
            set {
                _brightness = value;
                OnPropertyChanged("Brightness");
            }
        }
        private float _contrast;
        public float Contrast {
            get { return _contrast; }
            set {
                _contrast = value;
                OnPropertyChanged("Contrast");
            }
        }
        private bool _enableLensFlare;
        public bool EnableLensFlare {
            get { return _enableLensFlare; }
            set {
                _enableLensFlare = value;
                OnPropertyChanged("EnableLensFlare");
            }
        }

        private int _lensFlare;
        public int LensFlare {
            get { return _lensFlare; }
            set {
                _lensFlare = value;
                OnPropertyChanged("LensFlare");
            }
        }
        private float _lensFlarePositionX;
        public float LensFlarePositionX {
            get { return _lensFlarePositionX; }
            set {
                _lensFlarePositionX = value;
                OnPropertyChanged("LensFlarePositionX");
            }
        }
        private float _lensFlarePositionY;
        public float LensFlarePositionY {
            get { return _lensFlarePositionY; }
            set {
                _lensFlarePositionY = value;
                OnPropertyChanged("LensFlarePositionY");
            }
        }
        private float _lensFlarePositionZ;
        public float LensFlarePositionZ {
            get { return _lensFlarePositionZ; }
            set {
                _lensFlarePositionZ = value;
                OnPropertyChanged("LensFlarePositionZ");
            }
        }
        private float _lensFlareAlpha;
        public float LensFlareAlpha {
            get { return _lensFlareAlpha; }
            set {
                _lensFlareAlpha = value;
                OnPropertyChanged("LensFlareAlpha");
            }
        }
        private Color _parallelAmbientColor;
        public Color ParallelAmbientColor {
            get { return _parallelAmbientColor; }
            set {
                _parallelAmbientColor = value;
                OnPropertyChanged("ParallelAmbientColor");
            }
        }
        private Color _rayCutOffNormalColor;
        public Color RayCutOffNormalColor {
            get { return _rayCutOffNormalColor; }
            set {
                _rayCutOffNormalColor = value;
                OnPropertyChanged("RayCutOffNormalColor");
            }
        }
        private float _lightPointDirectionX;
        public float LightPointDirectionX {
            get { return _lightPointDirectionX; }
            set {
                _lightPointDirectionX = value;
                OnPropertyChanged("LightPointDirectionX");
            }
        }
        private float _lightPointDirectionY;
        public float LightPointDirectionY {
            get { return _lightPointDirectionY; }
            set {
                _lightPointDirectionY = value;
                OnPropertyChanged("LightPointDirectionY");
            }
        }
        private float _lightPointDirectionZ;
        public float LightPointDirectionZ {
            get { return _lightPointDirectionZ; }
            set {
                _lightPointDirectionZ = value;
                OnPropertyChanged("LightPointDirectionZ");
            }
        }
        private bool _enableShadowColor;
        public bool EnableShadowColor {
            get { return _enableShadowColor; }
            set {
                _enableShadowColor = value;
                OnPropertyChanged("EnableShadowColor");
            }
        }
        private Color _shadowColor;
        public Color ShadowColor {
            get { return _shadowColor; }
            set {
                _shadowColor = value;
                OnPropertyChanged("ShadowColor");
            }
        }
        private bool _enableFog;
        public bool EnableFog {
            get { return _enableFog; }
            set {
                _enableFog = value;
                OnPropertyChanged("EnableFog");
            }
        }
        private float _fogStartDistance;
        public float FogStartDistance {
            get { return _fogStartDistance; }
            set {
                _fogStartDistance = value;
                OnPropertyChanged("FogStartDistance");
            }
        }
        private float _fogEndDistance;
        public float FogEndDistance {
            get { return _fogEndDistance; }
            set {
                _fogEndDistance = value;
                OnPropertyChanged("FogEndDistance");
            }
        }
        private float _fogStrength;
        public float FogStrength {
            get { return _fogStrength; }
            set {
                _fogStrength = value;
                OnPropertyChanged("FogStrength");
            }
        }
        private Color _fogColor;
        public Color FogColor {
            get { return _fogColor; }
            set {
                _fogColor = value;
                OnPropertyChanged("FogColor");
            }
        }
        private bool _enableMonoColorFilter;
        public bool EnableMonoColorFilter {
            get { return _enableMonoColorFilter; }
            set {
                _enableMonoColorFilter = value;
                OnPropertyChanged("EnableMonoColorFilter");
            }
        }
        private float _monoBlueTone;
        public float MonoBlueTone {
            get { return _monoBlueTone; }
            set {
                _monoBlueTone = value;
                OnPropertyChanged("MonoBlueTone");
            }
        }
        private float _monoRedTone;
        public float MonoRedTone {
            get { return _monoRedTone; }
            set {
                _monoRedTone = value;
                OnPropertyChanged("MonoRedTone");
            }
        }
        private float _monoAlpha;
        public float MonoAlpha {
            get { return _monoAlpha; }
            set {
                _monoAlpha = value;
                OnPropertyChanged("MonoAlpha");
            }
        }
        private bool _enableGlareEffect;
        public bool EnableGlareEffect {
            get { return _enableGlareEffect; }
            set {
                _enableGlareEffect = value;
                OnPropertyChanged("EnableGlareEffect");
            }
        }
        private float _glareLuminanceThreshold;
        public float GlareLuminanceThreshold {
            get { return _glareLuminanceThreshold; }
            set {
                _glareLuminanceThreshold = value;
                OnPropertyChanged("GlareLuminanceThreshold");
            }
        }
        private float _glareSubtracted;
        public float GlareSubtracted {
            get { return _glareSubtracted; }
            set {
                _glareSubtracted = value;
                OnPropertyChanged("GlareSubtracted");
            }
        }
        private float _glareCompositionStrength;
        public float GlareCompositionStrength {
            get { return _glareCompositionStrength; }
            set {
                _glareCompositionStrength = value;
                OnPropertyChanged("GlareCompositionStrength");
            }
        }
        private bool _enableSoftFocus;
        public bool EnableSoftFocus {
            get { return _enableSoftFocus; }
            set {
                _enableSoftFocus = value;
                OnPropertyChanged("EnableSoftFocus");
            }
        }
        private float _softFocusStrength;
        public float SoftFocusStrength {
            get { return _softFocusStrength; }
            set {
                _softFocusStrength = value;
                OnPropertyChanged("SoftFocusStrength");
            }
        }
        private bool _enableDOFBlur;
        public bool EnableDOFBlur {
            get { return _enableDOFBlur; }
            set {
                _enableDOFBlur = value;
                OnPropertyChanged("EnableDOFBlur");
            }
        }
        private float _DOFFocalLength;
        public float DOFFocalLength {
            get { return _DOFFocalLength; }
            set {
                _DOFFocalLength = value;
                OnPropertyChanged("DOFFocalLength");
            }
        }
        private float _DOFShortDistance;
        public float DOFShortDistance {
            get { return _DOFShortDistance; }
            set {
                _DOFShortDistance = value;
                OnPropertyChanged("DOFShortDistance");
            }
        }
        private float _DOFLongDistance;
        public float DOFLongDistance {
            get { return _DOFLongDistance; }
            set {
                _DOFLongDistance = value;
                OnPropertyChanged("DOFLongDistance");
            }
        }
        private float _DOFAlpha;
        public float DOFAlpha {
            get { return _DOFAlpha; }
            set {
                _DOFAlpha = value;
                OnPropertyChanged("DOFAlpha");
            }
        }
        private bool _enableDOFEdgeBlur;
        public bool EnableDOFEdgeBlur {
            get { return _enableDOFEdgeBlur; }
            set {
                _enableDOFEdgeBlur = value;
                OnPropertyChanged("EnableDOFEdgeBlur");
            }
        }
        private bool _enableSunShaft;
        public bool EnableSunShaft {
            get { return _enableSunShaft; }
            set {
                _enableSunShaft = value;
                OnPropertyChanged("EnableSunShaft");
            }
        }
        private float _sunShaftStartDistance;
        public float SunShaftStartDistance {
            get { return _sunShaftStartDistance; }
            set {
                _sunShaftStartDistance = value;
                OnPropertyChanged("SunShaftStartDistance");
            }
        }
        private float _sunShaftEndDistance;
        public float SunShaftEndDistance {
            get { return _sunShaftEndDistance; }
            set {
                _sunShaftEndDistance = value;
                OnPropertyChanged("SunShaftEndDistance");
            }
        }
        private float _sunShaftAlpha;
        public float SunShaftAlpha {
            get { return _sunShaftAlpha; }
            set {
                _sunShaftAlpha = value;
                OnPropertyChanged("SunShaftAlpha");
            }
        }
        private Color _sunShaftColor;
        public Color SunShaftColor {
            get { return _sunShaftColor; }
            set {
                _sunShaftColor = value;
                OnPropertyChanged("SunShaftColor");
            }
        }
        private float _sunShaftDirectionX;
        public float SunShaftDirectionX {
            get { return _sunShaftDirectionX; }
            set {
                _sunShaftDirectionX = value;
                OnPropertyChanged("SunShaftDirectionX");
            }
        }
        private float _sunShaftDirectionY;
        public float SunShaftDirectionY {
            get { return _sunShaftDirectionY; }
            set {
                _sunShaftDirectionY = value;
                OnPropertyChanged("SunShaftDirectionY");
            }
        }
        private float _sunShaftDirectionZ;
        public float SunShaftDirectionZ {
            get { return _sunShaftDirectionZ; }
            set {
                _sunShaftDirectionZ = value;
                OnPropertyChanged("SunShaftDirectionZ");
            }
        }
        private float _sunShaftBlurWidth;
        public float SunShaftBlurWidth {
            get { return _sunShaftBlurWidth; }
            set {
                _sunShaftBlurWidth = value;
                OnPropertyChanged("SunShaftBlurWidth");
            }
        }
        private float _sunShaftAttenuationCoefficient;
        public float SunShaftAttenuationCoefficient {
            get { return _sunShaftAttenuationCoefficient; }
            set {
                _sunShaftAttenuationCoefficient = value;
                OnPropertyChanged("SunShaftAttenuationCoefficient");
            }
        }
        private Color _rockColor;
        public Color RockColor {
            get { return _rockColor; }
            set {
                _rockColor = value;
                OnPropertyChanged("RockColor");
            }
        }
        private ObservableCollection<StageInfoFilePathModel> _filePaths;
        public ObservableCollection<StageInfoFilePathModel> FilePaths {
            get { return _filePaths; }
            set {
                _filePaths = value;
                OnPropertyChanged("FilePaths");
            }
        }
        private ObservableCollection<StageInfoObjectModel> _objects;
        public ObservableCollection<StageInfoObjectModel> Objects {
            get { return _objects; }
            set {
                _objects = value;
                OnPropertyChanged("Objects");
            }
        }
        public object Clone() {
            ObservableCollection<StageInfoFilePathModel> newPathList = new ObservableCollection<StageInfoFilePathModel>();
            for (int i = 0; i < this.FilePaths.Count; i++) {
                newPathList.Add((StageInfoFilePathModel)FilePaths[i].Clone());
            }
            ObservableCollection<StageInfoObjectModel> newObjectList = new ObservableCollection<StageInfoObjectModel>();
            for (int i = 0; i < this.Objects.Count; i++) {
                newObjectList.Add((StageInfoObjectModel)Objects[i].Clone());
            }
            return new StageInfoModel {
                StageName = this.StageName,
                StageMessageID = this.StageMessageID,
                StageFilter = this.StageFilter,
                FilePaths = newPathList,
                Objects = newObjectList,
                Weather = this.Weather,
                PlayerAmbientColor = this.PlayerAmbientColor,
                RayCutOffShadeColor = this.RayCutOffShadeColor,
                EffectAmbientColor = this.EffectAmbientColor,
                UnknownColor = this.UnknownColor,
                EnableBrightnessAdjustment = this.EnableBrightnessAdjustment,
                Brightness = this.Brightness,
                Contrast = this.Contrast,
                EnableLensFlare = EnableLensFlare,
                LensFlare = this.LensFlare,
                LensFlarePositionX = this.LensFlarePositionX,
                LensFlarePositionY = this.LensFlarePositionY,
                LensFlarePositionZ = this.LensFlarePositionZ,
                LensFlareAlpha = this.LensFlareAlpha,
                ParallelAmbientColor = this.ParallelAmbientColor,
                RayCutOffNormalColor = this.RayCutOffNormalColor,
                LightPointDirectionX = this.LightPointDirectionX,
                LightPointDirectionY = this.LightPointDirectionY,
                LightPointDirectionZ = this.LightPointDirectionZ,
                EnableShadowColor = this.EnableShadowColor,
                ShadowColor = this.ShadowColor,
                EnableFog = this.EnableFog,
                FogStartDistance = this.FogStartDistance,
                FogEndDistance = this.FogEndDistance,
                FogStrength = this.FogStrength,
                FogColor = this.FogColor,
                EnableMonoColorFilter = this.EnableMonoColorFilter,
                MonoBlueTone = this.MonoBlueTone,
                MonoRedTone = this.MonoRedTone,
                MonoAlpha = this.MonoAlpha,
                EnableGlareEffect = this.EnableGlareEffect,
                GlareLuminanceThreshold = this.GlareLuminanceThreshold,
                GlareSubtracted = this.GlareSubtracted,
                GlareCompositionStrength = this.GlareCompositionStrength,
                EnableSoftFocus = this.EnableSoftFocus,
                SoftFocusStrength = this.SoftFocusStrength,
                EnableDOFBlur = this.EnableDOFBlur,
                DOFFocalLength = this.DOFFocalLength,
                DOFShortDistance = this.DOFShortDistance,
                DOFLongDistance = this.DOFLongDistance,
                DOFAlpha = this.DOFAlpha,
                EnableDOFEdgeBlur = this.EnableDOFEdgeBlur,
                EnableSunShaft = this.EnableSunShaft,
                SunShaftStartDistance = this.SunShaftStartDistance,
                SunShaftEndDistance = this.SunShaftEndDistance,
                SunShaftAlpha = this.SunShaftAlpha,
                SunShaftColor = this.SunShaftColor,
                SunShaftDirectionX = this.SunShaftDirectionX,
                SunShaftDirectionY = this.SunShaftDirectionY,
                SunShaftDirectionZ = this.SunShaftDirectionZ,
                SunShaftBlurWidth = this.SunShaftBlurWidth,
                SunShaftAttenuationCoefficient = this.SunShaftAttenuationCoefficient,
                RockColor = this.RockColor


            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class StageInfoFilePathModel : ICloneable, INotifyPropertyChanged {
        private string _filePath;
        public string FilePath {
            get { return _filePath; }
            set {
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }
        public object Clone() {

            return new StageInfoFilePathModel {
                FilePath = this.FilePath,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class StageInfoObjectModel : ICloneable, INotifyPropertyChanged {
        private string _objectfilePath;
        public string ObjectFilePath {
            get { return _objectfilePath; }
            set {
                _objectfilePath = value;
                OnPropertyChanged("ObjectFilePath");
            }
        }
        private string _objectName;
        public string ObjectName {
            get { return _objectName; }
            set {
                _objectName = value;
                OnPropertyChanged("ObjectName");
            }
        }
        private string _positionFilePath;
        public string PositionFilePath {
            get { return _positionFilePath; }
            set {
                _positionFilePath = value;
                OnPropertyChanged("PositionFilePath");
            }
        }
        private string _positionBoneName;
        public string PositionBoneName {
            get { return _positionBoneName; }
            set {
                _positionBoneName = value;
                OnPropertyChanged("PositionBoneName");
            }
        }
        private int _entryType;
        public int EntryType {
            get { return _entryType; }
            set {
                _entryType = value;
                OnPropertyChanged("EntryType");
            }
        }
        private bool _enableCameraHideObject;
        public bool EnableCameraHideObject {
            get { return _enableCameraHideObject; }
            set {
                _enableCameraHideObject = value;
                OnPropertyChanged("EnableCameraHideObject");
            }
        }
        private bool _isRigidBody;
        public bool IsRigidBody {
            get { return _isRigidBody; }
            set {
                _isRigidBody = value;
                OnPropertyChanged("IsRigidBody");
            }
        }
        private float _animationSpeed;
        public float AnimationSpeed {
            get { return _animationSpeed; }
            set {
                _animationSpeed = value;
                OnPropertyChanged("AnimationSpeed");
            }
        }
        private int _breakableWallValue1;
        public int BreakableWallValue1 {
            get { return _breakableWallValue1; }
            set {
                _breakableWallValue1 = value;
                OnPropertyChanged("BreakableWallValue1");
            }
        }
        private int _breakableWallValue2;
        public int BreakableWallValue2 {
            get { return _breakableWallValue2; }
            set {
                _breakableWallValue2 = value;
                OnPropertyChanged("BreakableWallValue2");
            }
        }
        private string _breakableWallEffect01;
        public string BreakableWallEffect01 {
            get { return _breakableWallEffect01; }
            set {
                _breakableWallEffect01 = value;
                OnPropertyChanged("BreakableWallEffect01");
            }
        }
        private string _breakableWallEffect02;
        public string BreakableWallEffect02 {
            get { return _breakableWallEffect02; }
            set {
                _breakableWallEffect02 = value;
                OnPropertyChanged("BreakableWallEffect02");
            }
        }
        private string _breakableWallEffect03;
        public string BreakableWallEffect03 {
            get { return _breakableWallEffect03; }
            set {
                _breakableWallEffect03 = value;
                OnPropertyChanged("BreakableWallEffect03");
            }
        }
        private string _breakableWallSound;
        public string BreakableWallSound {
            get { return _breakableWallSound; }
            set {
                _breakableWallSound = value;
                OnPropertyChanged("BreakableWallSound");
            }
        }
        private string _breakableObjectPath;
        public string BreakableObjectPath {
            get { return _breakableObjectPath; }
            set {
                _breakableObjectPath = value;
                OnPropertyChanged("BreakableObjectPath");
            }
        }
        private string _breakableObjectEffect01;
        public string BreakableObjectEffect01 {
            get { return _breakableObjectEffect01; }
            set {
                _breakableObjectEffect01 = value;
                OnPropertyChanged("BreakableObjectEffect01");
            }
        }
        private string _breakableObjectEffect02;
        public string BreakableObjectEffect02 {
            get { return _breakableObjectEffect02; }
            set {
                _breakableObjectEffect02 = value;
                OnPropertyChanged("BreakableObjectEffect02");
            }
        }
        private string _breakableObjectEffect03;
        public string BreakableObjectEffect03 {
            get { return _breakableObjectEffect03; }
            set {
                _breakableObjectEffect03 = value;
                OnPropertyChanged("BreakableObjectEffect03");
            }
        }
        private float _breakableObjectSpeed01;
        public float BreakableObjectSpeed01 {
            get { return _breakableObjectSpeed01; }
            set {
                _breakableObjectSpeed01 = value;
                OnPropertyChanged("BreakableObjectSpeed01");
            }
        }
        private float _breakableObjectSpeed02;
        public float BreakableObjectSpeed02 {
            get { return _breakableObjectSpeed02; }
            set {
                _breakableObjectSpeed02 = value;
                OnPropertyChanged("BreakableObjectSpeed02");
            }
        }
        private float _breakableObjectSpeed03;
        public float BreakableObjectSpeed03 {
            get { return _breakableObjectSpeed03; }
            set {
                _breakableObjectSpeed03 = value;
                OnPropertyChanged("BreakableObjectSpeed03");
            }
        }
        private float _breakableWallVolume;
        public float BreakableWallVolume {
            get { return _breakableWallVolume; }
            set {
                _breakableWallVolume = value;
                OnPropertyChanged("BreakableWallVolume");
            }
        }
        public object Clone() {

            return new StageInfoObjectModel {
                ObjectFilePath = this.ObjectFilePath,
                ObjectName = this.ObjectName,
                PositionFilePath = this.PositionFilePath,
                PositionBoneName = this.PositionBoneName,
                EntryType = this.EntryType,
                EnableCameraHideObject = this.EnableCameraHideObject,
                IsRigidBody = this.IsRigidBody,
                AnimationSpeed = this.AnimationSpeed,
                BreakableWallValue1 = this.BreakableWallValue1,
                BreakableWallValue2 = this.BreakableWallValue2,
                BreakableWallEffect01 = this.BreakableWallEffect01,
                BreakableWallEffect02 = this.BreakableWallEffect02,
                BreakableWallEffect03 = this.BreakableWallEffect03,
                BreakableWallSound = this.BreakableWallSound,
                BreakableWallVolume = this.BreakableWallVolume,
                BreakableObjectPath = this.BreakableObjectPath,
                BreakableObjectEffect01 = this.BreakableObjectEffect01,
                BreakableObjectEffect02 = this.BreakableObjectEffect02,
                BreakableObjectEffect03 = this.BreakableObjectEffect03,
                BreakableObjectSpeed01 = this.BreakableObjectSpeed01,
                BreakableObjectSpeed02 = this.BreakableObjectSpeed02,
                BreakableObjectSpeed03 = this.BreakableObjectSpeed03,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
