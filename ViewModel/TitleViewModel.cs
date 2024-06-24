using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using NSC_ModManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Octokit;

namespace NSC_ModManager.ViewModel {
    public class TitleViewModel : INotifyPropertyChanged {


        private Visibility _modManagerVisibility;
        public Visibility ModManagerVisibility {
            get { return _modManagerVisibility; }
            set {
                _modManagerVisibility = value;
                OnPropertyChanged("ModManagerVisibility");
            }
        }
        private Visibility _optionsVisibility;
        public Visibility OptionsVisibility {
            get { return _optionsVisibility; }
            set {
                _optionsVisibility = value;
                OnPropertyChanged("OptionsVisibility");
            }
        }
        private Visibility _mainWindowVisibility;
        public Visibility MainWindowVisibility {
            get { return _mainWindowVisibility; }
            set {
                _mainWindowVisibility = value;
                OnPropertyChanged("MainWindowVisibility");
            }
        }
        private Visibility _credits2024Visibility;
        public Visibility Credits2024Visibility {
            get { return _credits2024Visibility; }
            set {
                _credits2024Visibility = value;
                OnPropertyChanged("Credits2024Visibility");
            }
        }
        private int _toolTabState;
        public int ToolTabState {
            get { return _toolTabState; }
            set {
                _toolTabState = value;
                if (value > -1) {
                    switch (value) {
                        case 1:
                            ModManagerVisibility = Visibility.Visible;
                            OptionsVisibility = Visibility.Hidden;
                            break;
                        case 2:
                            ModManagerVisibility = Visibility.Hidden;
                            OptionsVisibility = Visibility.Visible;
                            break;
                    }
                }
                OnPropertyChanged("ToolTabState");
            }
        }
        private int _mainTabState;
        public int MainTabState {
            get { return _mainTabState; }
            set {
                _mainTabState = value;
                if (value > -1) {
                    switch (value) {
                        case 1:
                            MainWindowVisibility = Visibility.Visible;
                            Credits2024Visibility = Visibility.Hidden;
                            break;
                        case 2:
                            MainWindowVisibility = Visibility.Hidden;
                            Credits2024Visibility = Visibility.Visible;
                            break;
                    }
                }
                OnPropertyChanged("MainTabState");
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

        private int _meouchCounter;
        public int MeouchCounter {
            get { return _meouchCounter; }
            set {
                _meouchCounter = value;
                OnPropertyChanged("MeouchCounter");
            }
        }
        private int _stretchMode_field;
        public int StretchMode_field {
            get { return _stretchMode_field; }
            set {
                _stretchMode_field = value;
                OnPropertyChanged("StretchMode_field");
            }
        }

        private string _kuramaDialog;
        public string KuramaDialog {
            get { return _kuramaDialog; }
            set {
                _kuramaDialog = value;
                OnPropertyChanged("KuramaDialog");
            }
        }
        private string _kuramaName;
        public string KuramaName {
            get { return _kuramaName; }
            set {
                _kuramaName = value;
                OnPropertyChanged("KuramaName");
            }
        }

        private Visibility _meouchVisibility;
        public Visibility MeouchVisibility {
            get { return _meouchVisibility; }
            set {
                _meouchVisibility = value;
                OnPropertyChanged("MeouchVisibility");
            }
        }
        private Visibility _kyurutoVisibility;
        public Visibility KyurutoVisibility {
            get { return _kyurutoVisibility; }
            set {
                _kyurutoVisibility = value;
                OnPropertyChanged("KyurutoVisibility");
            }
        }
        private bool _meouchEffectAutoPlay;
        public bool MeouchEffectAutoPlay {
            get { return _meouchEffectAutoPlay; }
            set {
                _meouchEffectAutoPlay = value;
                OnPropertyChanged("MeouchEffectAutoPlay");
            }
        }

        private RepeatBehavior _meouchEffectRepeat;
        public RepeatBehavior MeouchEffectRepeat {
            get { return _meouchEffectRepeat; }
            set {
                _meouchEffectRepeat = value;
                OnPropertyChanged("MeouchEffectRepeat");
            }
        }
        private string _backgroundColor_field;
        public string BackgroundColor_field {
            get { return _backgroundColor_field; }
            set {
                _backgroundColor_field = value;
                OnPropertyChanged("BackgroundColor_field");
            }
        }
        private string _buttonColor_field;
        public string ButtonColor_field {
            get { return _buttonColor_field; }
            set {
                _buttonColor_field = value;
                OnPropertyChanged("ButtonColor_field");
            }
        }
        private string _textColor_field;
        public string TextColor_field {
            get { return _textColor_field; }
            set {
                _textColor_field = value;
                OnPropertyChanged("TextColor_field");
            }
        }
        private string _backgroundImagePath_field;
        public string BackgroundImagePath_field {
            get { return _backgroundImagePath_field; }
            set {
                _backgroundImagePath_field = value;
                OnPropertyChanged("BackgroundImagePath_field");
            }
        }
        private string _rootFolderPath_field;
        public string RootFolderPath_field {
            get { return _rootFolderPath_field; }
            set {
                _rootFolderPath_field = value;
                OnPropertyChanged("RootFolderPath_field");
            }
        }
        private string _modName_field;
        public string ModName_field {
            get { return _modName_field; }
            set {
                _modName_field = value;
                OnPropertyChanged("ModName_field");
            }
        }
        private string _modDescription_field;
        public string ModDescription_field {
            get { return _modDescription_field; }
            set {
                _modDescription_field = value;
                OnPropertyChanged("ModDescription_field");
            }
        }
        private string _modAuthor_field;
        public string ModAuthor_field {
            get { return _modAuthor_field; }
            set {
                _modAuthor_field = value;
                OnPropertyChanged("ModAuthor_field");
            }
        }
        private string _modVersion_field;
        public string ModVersion_field {
            get { return _modVersion_field; }
            set {
                _modVersion_field = value;
                OnPropertyChanged("ModVersion_field");
            }
        }
        private string _modLastUpdate_field;
        public string ModLastUpdate_field {
            get { return _modLastUpdate_field; }
            set {
                _modLastUpdate_field = value;
                OnPropertyChanged("ModLastUpdate_field");
            }
        }
        private bool _enableMotionBlur_field;
        public bool EnableMotionBlur_field {
            get { return _enableMotionBlur_field; }
            set {
                _enableMotionBlur_field = value;
                OnPropertyChanged("EnableMotionBlur_field");
            }
        }

        private string _modIconPath;
        public string ModIconPath {
            get {
                return _modIconPath;
            }
            set {
                _modIconPath = value;

                MemoryStream memoryStream = new MemoryStream();


                byte[] fileBytes = new byte[0];

                if (File.Exists(value)) {
                    fileBytes = File.ReadAllBytes(value);
                } else {
                    fileBytes = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Resources\\TemplateImages\\template_icon.png");
                }
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;
                ModIconPreview = BitmapFrame.Create(memoryStream);
                OnPropertyChanged("ModIconPath");
            }
        }
        private BitmapSource _modIconPreview;
        public BitmapSource ModIconPreview {
            get { return _modIconPreview; }
            set {
                _modIconPreview = value;
                OnPropertyChanged("ModIconPreview");
            }
        }

        private bool _enableModSelected;
        public bool EnableModSelected {
            get { return _enableModSelected; }
            set {
                _enableModSelected = value;
                OnPropertyChanged("EnableModSelected");
            }
        }

        private ObservableCollection<ModManagerModel> _modManagerList = new ObservableCollection<ModManagerModel>();
        public ObservableCollection<ModManagerModel> ModManagerList {
            get {
                return _modManagerList;
            }
            set {
                _modManagerList = value;
                OnPropertyChanged("ModManagerList");
            }
        }

        private ModManagerModel _selectedMod;
        public ModManagerModel SelectedMod {
            get {
                return _selectedMod;
            }
            set {
                _selectedMod = value;
                if (value != null) {
                    ModName_field = value.ModName;
                    ModDescription_field = value.Description;
                    ModAuthor_field = value.Author;
                    ModVersion_field = value.Version;
                    ModLastUpdate_field = value.LastUpdate;
                    ModIconPath = value.IconPath;
                }
                OnPropertyChanged("SelectedMod");
            }
        }

        private ObservableCollection<CharacterModModel> _characterList = new ObservableCollection<CharacterModModel>();
        public ObservableCollection<CharacterModModel> CharacterList {
            get {
                return _characterList;
            }
            set {
                _characterList = value;
                OnPropertyChanged("CharacterList");
            }
        }
        private ObservableCollection<StageModModel> _stageList = new ObservableCollection<StageModModel>();
        public ObservableCollection<StageModModel> StageList {
            get {
                return _stageList;
            }
            set {
                _stageList = value;
                OnPropertyChanged("StageList");
            }
        }
        private ObservableCollection<CostumeModModel> _costumeList = new ObservableCollection<CostumeModModel>();
        public ObservableCollection<CostumeModModel> CostumeList {
            get {
                return _costumeList;
            }
            set {
                _costumeList = value;
                OnPropertyChanged("CostumeList");
            }
        }

        private ObservableCollection<string> _CPKList = new ObservableCollection<string>();
        public ObservableCollection<string> CPKList {
            get {
                return _CPKList;
            }
            set {
                _CPKList = value;
                OnPropertyChanged("CPKList");
            }
        }
        private ObservableCollection<string> _shaderList = new ObservableCollection<string>();
        public ObservableCollection<string> ShaderList {
            get {
                return _shaderList;
            }
            set {
                _shaderList = value;
                OnPropertyChanged("ShaderList");
            }
        }

        public TitleViewModel() {
            ToolTabState = 1;
            KuramaName = "Kyuruto";
            MeouchCounter = 0;
            MeouchVisibility = Visibility.Hidden;
            KyurutoVisibility = Visibility.Visible;
            LoadingStatePlay = Visibility.Hidden;
            MainWindowVisibility = Visibility.Visible;
            Credits2024Visibility = Visibility.Hidden;
            MeouchEffectAutoPlay = false;
            MeouchEffectRepeat = RepeatBehavior.Forever;
            KyurutoDialogTextLoader("Hello! You can call me " + KuramaName + ".", 50);

            switch (Properties.Settings.Default.StretchMode) {
                case "Fill":
                    StretchMode_field = 0;
                    break;
                case "Uniform":
                    StretchMode_field = 1;
                    break;
                case "None":
                    StretchMode_field = 2;
                    break;
            }
            BackgroundColor_field = Properties.Settings.Default.BackgroundColor1;
            ButtonColor_field = Properties.Settings.Default.ButtonColor1;
            TextColor_field = Properties.Settings.Default.TextColor1;
            RootFolderPath_field = Properties.Settings.Default.RootGameFolder; // use "\\\\?\\" for fixing issue with loading files in long paths
            EnableMotionBlur_field = Properties.Settings.Default.EnableMotionBlur;
            if (File.Exists(Properties.Settings.Default.BackgroundImagePath))
                BackgroundImagePath_field = Properties.Settings.Default.BackgroundImagePath;
            else {
                Properties.Settings.Default.BackgroundImagePath = "UI/background/bg_toolbox_main.png";
                Properties.Settings.Default.Save();
                BackgroundImagePath_field = Properties.Settings.Default.BackgroundImagePath;
            }
            ModManagerList = new ObservableCollection<ModManagerModel>();
            CharacterList = new ObservableCollection<CharacterModModel>();
            StageList = new ObservableCollection<StageModModel>();
            CostumeList = new ObservableCollection<CostumeModModel>();
            CPKList = new ObservableCollection<string>();
            ShaderList = new ObservableCollection<string>();
            RefreshModList();
            CheckGitHubNewerVersion();
        }
        private async System.Threading.Tasks.Task CheckGitHubNewerVersion() {
            //Get all releases from GitHub
            //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/
            GitHubClient client = new GitHubClient(new Octokit.ProductHeaderValue("NSC-ModManager"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("TheLeonX", "NSC-ModManager");

            //Setup the versions
            //Source: https://learn.microsoft.com/en-us/archive/msdn-technet-forums/7fe34424-0a53-46cb-b4b3-ab63b0823d01
            Version latestGitHubVersion = new Version(releases[0].TagName);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version localVersion = assembly.GetName().Version;

            //Compare the Versions
            //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
            int versionComparison = localVersion.CompareTo(latestGitHubVersion);
            if (versionComparison < 0) {
                SystemSounds.Beep.Play();
                ModernWpf.MessageBox.Show("There is new version of Mod Manager on GitHub page.");
            }

        }
        public void RefreshModList() {
            ModManagerList.Clear();
            CharacterList.Clear();
            StageList.Clear();
            CostumeList.Clear();
            string root_folder = Properties.Settings.Default.RootGameFolder;
            string modmanager_folder = root_folder + "\\modmanager\\"; // "\\\\?\\" was used for fixing long paths. Crashes sometimes
            if (Directory.Exists(modmanager_folder)) {

                DirectoryInfo d = new DirectoryInfo(modmanager_folder); //This function getting info about all files in a path
                FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories); //Getting all files with "Icon.png" name
                Array.Sort(ModConfigList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));


                foreach (FileInfo mod_path in ModConfigList) {
                    var ModInfo = new IniFile(mod_path.FullName);

                    ModManagerModel ModEntry = new ModManagerModel() {
                        ModName = ModInfo.Read("ModName", "ModManager"),
                        Description = ModInfo.Read("Description", "ModManager"),
                        Author = ModInfo.Read("Author", "ModManager"),
                        LastUpdate = ModInfo.Read("LastUpdate", "ModManager"),
                        Version = ModInfo.Read("Version", "ModManager"),
                        EnableMod = Convert.ToBoolean(ModInfo.Read("EnableMod", "ModManager")),
                        IconPath = Path.GetDirectoryName(mod_path.FullName) + "\\mod_icon.png",
                        ModFolder = Path.GetDirectoryName(mod_path.FullName)
                    };
                    ModManagerList.Add(ModEntry);

                    if (ModEntry.EnableMod) {
                        //character mod
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        Array.Sort(CharacterModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                        foreach (FileInfo character_path in CharacterModList) {
                            var CharacterInfo = new IniFile(character_path.FullName);
                            CharacterModModel CharacterEntry = new CharacterModModel() {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                RootPath = Path.GetDirectoryName(character_path.FullName)
                            };
                            CharacterList.Add(CharacterEntry);
                        }

                        //stage mod
                        FileInfo[] StageModList = mod_dir.GetFiles("stage_config.ini", SearchOption.AllDirectories);
                        Array.Sort(StageModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                        foreach (FileInfo stage_path in StageModList) {
                            var StageInfo = new IniFile(stage_path.FullName);
                            StageModModel StageEntry = new StageModModel() {
                                StageName = Path.GetFileName(Path.GetDirectoryName(stage_path.FullName)),
                                BgmID = Convert.ToInt32(StageInfo.Read("BGM_ID", "ModManager")),
                                MessageID = StageInfo.Read("MessageID", "ModManager"),
                                Hell = Convert.ToBoolean(StageInfo.Read("Hell", "ModManager")),
                                RootPath = Path.GetDirectoryName(stage_path.FullName)
                            };
                            StageList.Add(StageEntry);
                        }

                        //costume mod
                        FileInfo[] CostumeModList = mod_dir.GetFiles("model_config.ini", SearchOption.AllDirectories);
                        Array.Sort(CostumeModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                        foreach (FileInfo costume_path in CostumeModList) {
                            var CostumeInfo = new IniFile(costume_path.FullName);
                            CostumeModModel CostumeEntry = new CostumeModModel() {
                                Characode = CostumeInfo.Read("Characode", "ModManager"),
                                BaseCostume = CostumeInfo.Read("BaseModel", "ModManager"),
                                AwakeCostume = CostumeInfo.Read("AwakeModel", "ModManager"),
                                RootPath = Path.GetDirectoryName(costume_path.FullName)
                            };
                            CostumeList.Add(CostumeEntry);
                        }
                        //CPKs
                        FileInfo[] CpkListInfo = mod_dir.GetFiles("*.cpk", SearchOption.AllDirectories);
                        Array.Sort(CpkListInfo, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                        foreach (FileInfo cpk_path in CpkListInfo) {
                            CPKList.Add(cpk_path.FullName);
                        }
                        //Shaders
                        FileInfo[] ShaderListInfo = mod_dir.GetFiles("*.hlsl", SearchOption.AllDirectories);
                        Array.Sort(ShaderListInfo, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                        foreach (FileInfo shader_path in ShaderListInfo) {
                            ShaderList.Add(shader_path.FullName);
                        }
                    }


                }
            } else {
                if (!Directory.Exists(root_folder)) {
                    ModernWpf.MessageBox.Show("Select Root Folder of game");
                } else {
                    Directory.CreateDirectory(modmanager_folder);
                    ModernWpf.MessageBox.Show("No mods found.");
                }
            }
        }

        async void CompileModAsyncProcess(string root_folder) {

            try {
                //MessageBox.Show(CharacterList[-1].Characode);
                await Task.Run(() => CompileModProcess(root_folder));
                LoadingStatePlay = Visibility.Hidden;
                KyurutoDialogTextLoader("Your mods are ready to play! Your welcome!",
            20);
                SystemSounds.Beep.Play();

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "steam://rungameid/1020790";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

            //ModernWpf.MessageBox.Show("Mods were successfully compiled!");
        } catch (Exception ex) {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show("Something went wrong.. Report issue on GitHub \n\n" + ex.StackTrace + " \n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                KyurutoDialogTextLoader("Something went wrong.. Make sure game is closed and you don't have anywhere opened file which mod manager might use during compile process.", 20);
        LoadingStatePlay = Visibility.Hidden;
            }
}

        public void CompileModProcess(string root_folder) {
            CleanGameAssets(false);
            KyurutoDialogTextLoader("Preparing all files!",
                20);
            //vanilla files
            string characodePath = Directory.GetCurrentDirectory() + "\\ParamFiles\\characode.bin.xfbin";
            string duelPlayerParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\duelPlayerParam.xfbin";
            string playerSettingParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\playerSettingParam.bin.xfbin";
            string skillCustomizeParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\skillCustomizeParam.xfbin";
            string spSkillCustomizeParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\spSkillCustomizeParam.xfbin";
            string skillIndexSettingParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\skillIndexSettingParam.xfbin";
            string supportSkillRecoverySpeedParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\supportSkillRecoverySpeedParam.xfbin";
            string privateCameraPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\privateCamera.bin.xfbin";
            string characterSelectParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\characterSelectParam.xfbin";
            string costumeBreakColorParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\costumeBreakColorParam.xfbin";
            string costumeParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\costumeParam.bin.xfbin";
            string playerIconPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\player_icon.xfbin";
            string cmnparamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\cmnparam.xfbin";
            string supportActionParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\supportActionParam.xfbin";
            string awakeAuraPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\awakeAura.xfbin";
            string appearanceAnmPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\appearanceAnm.xfbin";
            string afterAttachObjectPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\afterAttachObject.xfbin";
            string playerDoubleEffectParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\playerDoubleEffectParam.xfbin";
            string spTypeSupportParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\spTypeSupportParam.xfbin";
            string costumeBreakParamPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\costumeBreakParam.xfbin";
            string messageInfoPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\message";
            string damageeffPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\damageeff.bin.xfbin";
            string effectprmPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\effectprm.bin.xfbin";
            string damageprmPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\damageprm.bin.xfbin";
            string stageInfoPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\StageInfo.bin.xfbin";
            string nuccMaterialDx11Path = Directory.GetCurrentDirectory() + "\\ParamFiles\\nuccMaterial_dx11.nsh";
            string stageselGfxPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\stagesel.gfx";
            string stageselImageGfxPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\stagesel_image.gfx";
            string charselGfxPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\charsel.gfx";
            string chariconGfxPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\charicon_s.gfx";
            string stage_selectPath = Directory.GetCurrentDirectory() + "\\ParamFiles\\select_stage.xfbin";
            string specialCondParamPath = Directory.GetCurrentDirectory() + "\\ModdingAPIFiles\\moddingapi\\mods\\base_game\\specialCondParam.xfbin";
            string partnerSlotParamPath = Directory.GetCurrentDirectory() + "\\ModdingAPIFiles\\moddingapi\\mods\\base_game\\partnerSlotParam.xfbin";
            string susanooCondParamPath = Directory.GetCurrentDirectory() + "\\ModdingAPIFiles\\moddingapi\\mods\\base_game\\susanooCondParam.xfbin";

            //vanilla file editors
            CharacodeEditorViewModel characode_vanilla = new CharacodeEditorViewModel();
            characode_vanilla.OpenFile(characodePath);
            DuelPlayerParamEditorViewModel duelPlayerParam_vanilla = new DuelPlayerParamEditorViewModel();
            duelPlayerParam_vanilla.OpenFile(duelPlayerParamPath);
            PlayerSettingParamViewModel playerSettingParam_vanilla = new PlayerSettingParamViewModel();
            playerSettingParam_vanilla.OpenFile(playerSettingParamPath);
            SkillCustomizeParamViewModel skillCustomizeParam_vanilla = new SkillCustomizeParamViewModel();
            skillCustomizeParam_vanilla.OpenFile(skillCustomizeParamPath);
            SpSkillCustomizeParamViewModel spSkillCustomizeParam_vanilla = new SpSkillCustomizeParamViewModel();
            spSkillCustomizeParam_vanilla.OpenFile(spSkillCustomizeParamPath);
            SkillIndexSettingParamViewModel skillIndexSettingParam_vanilla = new SkillIndexSettingParamViewModel();
            skillIndexSettingParam_vanilla.OpenFile(skillIndexSettingParamPath);
            SupportSkillRecoverySpeedParamViewModel supportSkillRecoverySpeedParam_vanilla = new SupportSkillRecoverySpeedParamViewModel();
            supportSkillRecoverySpeedParam_vanilla.OpenFile(supportSkillRecoverySpeedParamPath);
            PrivateCameraViewModel privateCamera_vanilla = new PrivateCameraViewModel();
            privateCamera_vanilla.OpenFile(privateCameraPath);
            CharacterSelectParamViewModel characterSelectParam_vanilla = new CharacterSelectParamViewModel();
            characterSelectParam_vanilla.OpenFile(characterSelectParamPath);
            CostumeBreakColorParamViewModel costumeBreakColorParam_vanilla = new CostumeBreakColorParamViewModel();
            costumeBreakColorParam_vanilla.OpenFile(costumeBreakColorParamPath);
            CostumeParamViewModel costumeParam_vanilla = new CostumeParamViewModel();
            costumeParam_vanilla.OpenFile(costumeParamPath);
            PlayerIconViewModel playerIcon_vanilla = new PlayerIconViewModel();
            playerIcon_vanilla.OpenFile(playerIconPath);
            cmnparamViewModel cmnparam_vanilla = new cmnparamViewModel();
            cmnparam_vanilla.OpenFile(cmnparamPath);
            SupportActionParamViewModel supportActionParam_vanilla = new SupportActionParamViewModel();
            supportActionParam_vanilla.OpenFile(supportActionParamPath);
            AwakeAuraViewModel awakeAura_vanilla = new AwakeAuraViewModel();
            awakeAura_vanilla.OpenFile(awakeAuraPath);
            AppearanceAnmViewModel appearanceAnm_vanilla = new AppearanceAnmViewModel();
            appearanceAnm_vanilla.OpenFile(appearanceAnmPath);
            AfterAttachObjectViewModel afterAttachObject_vanilla = new AfterAttachObjectViewModel();
            afterAttachObject_vanilla.OpenFile(afterAttachObjectPath);
            PlayerDoubleEffectParamViewModel playerDoubleEffectParam_vanilla = new PlayerDoubleEffectParamViewModel();
            playerDoubleEffectParam_vanilla.OpenFile(playerDoubleEffectParamPath);
            SpTypeSupportParamViewModel spTypeSupportParam_vanilla = new SpTypeSupportParamViewModel();
            spTypeSupportParam_vanilla.OpenFile(spTypeSupportParamPath);
            CostumeBreakParamViewModel costumeBreakParam_vanilla = new CostumeBreakParamViewModel();
            costumeBreakParam_vanilla.OpenFile(costumeBreakParamPath);
            MessageInfoViewModel messageInfo_vanilla = new MessageInfoViewModel();
            messageInfo_vanilla.OpenFiles(messageInfoPath);
            DamageEffViewModel damageeff_vanilla = new DamageEffViewModel();
            damageeff_vanilla.OpenFile(damageeffPath);
            EffectPrmViewModel effectprm_vanilla = new EffectPrmViewModel();
            effectprm_vanilla.OpenFile(effectprmPath);
            DamagePrmViewModel damageprm_vanilla = new DamagePrmViewModel();
            damageprm_vanilla.OpenFile(damageprmPath);

            StageInfoViewModel stageInfo_vanilla = new StageInfoViewModel();
            stageInfo_vanilla.OpenFile(stageInfoPath);

            byte[] specialCondParam_vanilla = File.ReadAllBytes(specialCondParamPath);
            byte[] partnerSlotParam_vanilla = File.ReadAllBytes(partnerSlotParamPath);
            byte[] susanooCondParam_vanilla = File.ReadAllBytes(susanooCondParamPath);

            int characode_count = characode_vanilla.CharacodeList.Count;
            bool messageInfoModified = false;
            bool stageInfoModified = false;
            List<StageModModel> StagesToAdd = new List<StageModModel>();
            List<string> CharselIconNamesList = new List<string>();
            List<string> CharselLoadedIconsList = new List<string>();
            for (int i = 0; i < playerIcon_vanilla.playerIconList.Count; i++) {
                if (!CharselLoadedIconsList.Contains(playerIcon_vanilla.playerIconList[i].BaseIcon)) {
                    CharselLoadedIconsList.Add(playerIcon_vanilla.playerIconList[i].BaseIcon);
                }
            }

            //Compile Character mods
            foreach (CharacterModModel character_mod in CharacterList) {
                string mod_characode = character_mod.Characode;
                int mod_characodeID = -1;
                bool replace_character = false;

                //Read Characode file and add/find entry
                foreach (CharacodeEditorModel entry in characode_vanilla.CharacodeList) {
                    if (entry.CharacodeName == mod_characode) {
                        mod_characodeID = entry.CharacodeIndex;
                        replace_character = true;
                        break;
                    }
                }


                // Required for adding
                string duelPlayerParamModPath = character_mod.RootPath + "\\data\\spc\\duelPlayerParam.xfbin";
                string playerSettingParamModPath = character_mod.RootPath + "\\data\\spc\\playerSettingParam.bin.xfbin";
                string skillCustomizeParamModPath = character_mod.RootPath + "\\data\\spc\\skillCustomizeParam.xfbin";
                string spSkillCustomizeParamModPath = character_mod.RootPath + "\\data\\spc\\spSkillCustomizeParam.xfbin";
                string skillIndexSettingParamModPath = character_mod.RootPath + "\\data\\spc\\skillIndexSettingParam.xfbin";
                string supportSkillRecoverySpeedParamModPath = character_mod.RootPath + "\\data\\spc\\supportSkillRecoverySpeedParam.xfbin";
                string privateCameraModPath = character_mod.RootPath + "\\data\\spc\\privateCamera.bin.xfbin";
                string costumeBreakColorParamModPath = character_mod.RootPath + "\\data\\spc\\costumeBreakColorParam.xfbin";
                string costumeParamModPath = character_mod.RootPath + "\\data\\rpg\\param\\costumeParam.bin.xfbin";
                string playerIconModPath = character_mod.RootPath + "\\data\\spc\\player_icon.xfbin";
                string cmnparamModPath = character_mod.RootPath + "\\data\\sound\\cmnparam.xfbin";
                string characterSelectParamModPath = character_mod.RootPath + "\\data\\ui\\max\\select\\characterSelectParam.xfbin";
                string supportActionParamModPath = character_mod.RootPath + "\\data\\spc\\supportActionParam.xfbin";
                //Not required for adding
                string awakeAuraModPath = character_mod.RootPath + "\\data\\spc\\awakeAura.xfbin";
                string appearanceAnmModPath = character_mod.RootPath + "\\data\\spc\\appearanceAnm.xfbin";
                string afterAttachObjectModPath = character_mod.RootPath + "\\data\\spc\\afterAttachObject.xfbin";
                string playerDoubleEffectParamModPath = character_mod.RootPath + "\\data\\spc\\playerDoubleEffectParam.xfbin";
                string spTypeSupportParamModPath = character_mod.RootPath + "\\data\\spc\\spTypeSupportParam.xfbin";
                string costumeBreakParamModPath = character_mod.RootPath + "\\data\\spc\\costumeBreakParam.xfbin";
                string messageInfoModPath = character_mod.RootPath + "\\data\\message";
                string damageeffModPath = character_mod.RootPath + "\\data\\spc\\damageeff.bin.xfbin";
                string effectprmModPath = character_mod.RootPath + "\\data\\spc\\effectprm.bin.xfbin";
                string damageprmModPath = character_mod.RootPath + "\\data\\spc\\damageprm.bin.xfbin";

                string specialCondParamModPath = character_mod.RootPath + "\\moddingapi\\mods\\base_game\\specialCondParam.xfbin";
                string partnerSlotParamModPath = character_mod.RootPath + "\\moddingapi\\mods\\base_game\\partnerSlotParam.xfbin";
                string susanooCondParamModPath = character_mod.RootPath + "\\moddingapi\\mods\\base_game\\susanooCondParam.xfbin";

                //characode file
                if (!replace_character) {
                    //Check if files exists for partners/leaders in case if we add them instead of replacing
                    if (character_mod.Partner == false) {
                        if (!File.Exists(duelPlayerParamModPath) ||
                        !File.Exists(playerSettingParamModPath) ||
                        !File.Exists(skillCustomizeParamModPath) ||
                        !File.Exists(spSkillCustomizeParamModPath) ||
                        !File.Exists(skillIndexSettingParamModPath) ||
                        !File.Exists(supportSkillRecoverySpeedParamModPath) ||
                        !File.Exists(privateCameraModPath) ||
                        !File.Exists(costumeBreakColorParamModPath) ||
                        !File.Exists(costumeParamModPath) ||
                        !File.Exists(playerIconModPath) ||
                        !File.Exists(cmnparamModPath) ||
                        !File.Exists(characterSelectParamModPath)) {
                            ModernWpf.MessageBox.Show("Error 1");
                            continue;
                        }
                    } else {
                        if (!File.Exists(duelPlayerParamModPath)) {
                            ModernWpf.MessageBox.Show("Error 2");
                            continue;
                        }
                    }

                    //Add new code of character (leader/partner) into characode file
                    CharacodeEditorModel characode_entry = new CharacodeEditorModel();
                    characode_entry.CharacodeName = mod_characode;
                    mod_characodeID = characode_vanilla.CharacodeList.Count + 1;
                    characode_entry.CharacodeIndex = mod_characodeID;
                    characode_vanilla.CharacodeList.Add(characode_entry);

                    

                }

                Dictionary<string, string> csp_code_replace = new Dictionary<string, string>();

                /*---------------------------------------REQUIRED FILES-------------------------------------------*/
                //duelPlayerParam file
                List<string> baseModel = new List<string>();
                List<string> awakeModel = new List<string>();
                DuelPlayerParamEditorViewModel duelPlayerParam_mod = new DuelPlayerParamEditorViewModel();
                if (File.Exists(duelPlayerParamModPath)) {
                    duelPlayerParam_mod.OpenFile(duelPlayerParamModPath);
                    if (replace_character) {
                        for (int i = 0; i < duelPlayerParam_vanilla.DuelPlayerParamList.Count; i++) {
                            if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BinName.Contains(mod_characode)) {
                                duelPlayerParam_vanilla.DuelPlayerParamList[i] = (DuelPlayerParamModel)duelPlayerParam_mod.DuelPlayerParamList[0].Clone();
                                break;
                            }
                        }
                    } else {
                        duelPlayerParam_vanilla.DuelPlayerParamList.Add((DuelPlayerParamModel)duelPlayerParam_mod.DuelPlayerParamList[0].Clone());
                    }
                    for (int i = 0; i < 20; i++) {
                        if (!baseModel.Contains(duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName) && duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName != "")
                            baseModel.Add(duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName);
                        if (!awakeModel.Contains(duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName) && duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName != "")
                            awakeModel.Add(duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName);
                    }

                }

                //playerSettingParam file
                List<int> RemovedPresetIds = new List<int>();
                List<int> AddedPresetIds = new List<int>();
                List<string> RemovedCSPCodes = new List<string>();
                bool IsPspModified = false;
                string charMessageID = "";
                PlayerSettingParamViewModel playerSettingParam_mod = new PlayerSettingParamViewModel();
                if (File.Exists(playerSettingParamModPath) && !character_mod.Partner) {
                    playerSettingParam_mod.OpenFile(playerSettingParamModPath);

                    foreach (PlayerSettingParamModel psp_entry in playerSettingParam_mod.PlayerSettingParamList) {
                        string costume_csp_code = psp_entry.PSP_code;
                        int csp_code_index = 0;
                        do {
                            csp_code_index++;
                            costume_csp_code = psp_entry.PSP_code + "_" + csp_code_index.ToString("D6");
                        }
                        while (playerSettingParam_vanilla.PSPCodeExists(costume_csp_code));

                        csp_code_replace.Add(psp_entry.PSP_code, costume_csp_code);
                        psp_entry.PSP_code = costume_csp_code;
                    }

                    






                    if (replace_character) {
                        if (playerSettingParam_mod.PlayerSettingParamList.Count > 0) {
                            //Remove old entries
                            for (int i = 0; i < playerSettingParam_vanilla.PlayerSettingParamList.Count; i++) {
                                if (playerSettingParam_vanilla.PlayerSettingParamList[i].CharacodeID == mod_characodeID) {
                                    RemovedPresetIds.Add(playerSettingParam_vanilla.PlayerSettingParamList[i].PSP_ID);
                                    RemovedCSPCodes.Add(playerSettingParam_vanilla.PlayerSettingParamList[i].PSP_code);
                                    playerSettingParam_vanilla.PlayerSettingParamList.RemoveAt(i);
                                    i--;
                                }
                            }
                            //Add new entries
                            for (int i = 0; i < playerSettingParam_mod.PlayerSettingParamList.Count; i++) {
                                PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[i].Clone();
                                if (i == 0)
                                    charMessageID = psp_entry.CharacterNameMessageID;
                                psp_entry.CharacodeID = mod_characodeID;
                                psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot() + i + 1;
                                AddedPresetIds.Add(psp_entry.PSP_ID);
                                if (psp_entry.ReferenceCharacodeID > characode_count) {
                                    psp_entry.ReferenceCharacodeID = mod_characodeID;
                                    psp_entry.Unk = 1;
                                }
                                if (psp_entry.MainPSP_ID != -1) {
                                    psp_entry.MainPSP_ID = AddedPresetIds[0];
                                }
                                playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                            }
                            IsPspModified = true;
                        }
                    } else {
                        for (int i = 0; i < playerSettingParam_mod.PlayerSettingParamList.Count; i++) {
                            PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[i].Clone();
                            psp_entry.CharacodeID = mod_characodeID;
                            psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot() + i+1;
                            if (i == 0)
                                charMessageID = psp_entry.CharacterNameMessageID;
                            AddedPresetIds.Add(playerSettingParam_vanilla.MaxSlot() + i + 1);
                            if (psp_entry.ReferenceCharacodeID > characode_count) {
                                psp_entry.ReferenceCharacodeID = mod_characodeID;
                                psp_entry.Unk = 1;
                            }
                            if (psp_entry.MainPSP_ID != -1) {
                                psp_entry.MainPSP_ID = AddedPresetIds[0];
                            }
                            playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                        }
                    }
                }

                //costumeColorBreakParam file
                CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                if (File.Exists(costumeBreakColorParamModPath) && !character_mod.Partner) {
                    costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                    if (replace_character) {
                        if (costumeBreakColorParam_mod.CostumeBreakColorParamList.Count > 0) {
                            //Remove old entries
                            for (int i = 0; i < costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Count; i++) {
                                if (RemovedPresetIds.Contains(costumeBreakColorParam_vanilla.CostumeBreakColorParamList[i].PlayerSettingParamID)) {
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.RemoveAt(i);
                                    i--;
                                }
                            }
                            //Add new entries
                            for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++) {
                                CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                                costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                            }
                        }
                    } else {
                        //Add new entries
                        for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++) {
                            CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                            costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                            costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                        }
                    }
                }
                //costumeParam file
                CostumeParamViewModel costumeParam_mod = new CostumeParamViewModel();
                if (File.Exists(costumeParamModPath) && !character_mod.Partner) {
                    costumeParam_mod.OpenFile(costumeParamModPath);
                    if (replace_character) {
                        if (costumeParam_mod.CostumeParamList.Count > 0) {
                            //Remove old entries
                            for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++) {
                                if (RemovedPresetIds.Contains(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID)) {
                                    costumeParam_vanilla.CostumeParamList.RemoveAt(i);
                                    i--;
                                }
                            }
                            //Add new entries
                            int old_preset_id = 0;
                            int presetIdIndex = -1;
                            for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++) {
                                CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                                if (costume_entry.PlayerSettingParamID != old_preset_id) {
                                    presetIdIndex++;
                                    old_preset_id = costume_entry.PlayerSettingParamID;
                                }
                                costume_entry.PlayerSettingParamID = AddedPresetIds[presetIdIndex];
                                costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                                //costume_entry.EntryIndex = costumeParam_vanilla.LastEntry();
                                costume_entry.EntryIndex = 0; //used for unlocking
                                costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                            }

                        } else {
                            if (RemovedPresetIds.Count == AddedPresetIds.Count && IsPspModified) {
                                for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++) {
                                    if (RemovedPresetIds.Contains(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID)) {
                                        int index = RemovedPresetIds.IndexOf(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID);
                                        costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID = AddedPresetIds[index];
                                    }
                                }
                            } else {
                                //Remove old entries
                                for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++) {
                                    if (RemovedPresetIds.Contains(costumeParam_mod.CostumeParamList[i].PlayerSettingParamID)) {
                                        costumeParam_vanilla.CostumeParamList.RemoveAt(i);
                                        i--;
                                    }
                                }
                                for (int i = 0; i < AddedPresetIds.Count; i++) {
                                    for (int c = 0; c < 2; c++) {
                                        CostumeParamModel costume_entry = new CostumeParamModel();
                                        costume_entry.PlayerSettingParamID = AddedPresetIds[i];
                                        costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                                        //costume_entry.EntryIndex = costumeParam_vanilla.LastEntry();
                                        costume_entry.EntryIndex = 0; //used for unlocking
                                        costume_entry.CharacterName = charMessageID;
                                        costume_entry.UnlockCost = 0;
                                        costume_entry.UnlockCondition = 1;
                                        costume_entry.EntryType = c;
                                        costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                                    }
                                }
                            }
                        }
                    } else {
                        //Add new entries
                        int old_preset_id = 0;
                        int presetIdIndex = -1;
                        for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++) {
                            CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                            if (costume_entry.PlayerSettingParamID != old_preset_id) {
                                presetIdIndex++;
                                old_preset_id = costume_entry.PlayerSettingParamID;
                            }
                            costume_entry.PlayerSettingParamID = AddedPresetIds[presetIdIndex];
                            costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                            //costume_entry.EntryIndex = costumeParam_vanilla.LastEntry();
                            costume_entry.EntryIndex = 0; //used for unlocking
                            costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                        }
                    }
                }

                //skillCustomizeParam file
                SkillCustomizeParamViewModel skillCustomizeParam_mod = new SkillCustomizeParamViewModel();
                if (File.Exists(skillCustomizeParamModPath) && !character_mod.Partner) {
                    skillCustomizeParam_mod.OpenFile(skillCustomizeParamModPath);
                    if (replace_character) {
                        for (int i = 0; i < skillCustomizeParam_vanilla.SkillCustomizeParamList.Count; i++) {
                            if (skillCustomizeParam_vanilla.SkillCustomizeParamList[i].CharacodeID == mod_characodeID) {
                                skillCustomizeParam_vanilla.SkillCustomizeParamList[i] = skillCustomizeParam_mod.SkillCustomizeParamList[0];
                                break;
                            }
                        }
                    } else {
                        SkillCustomizeParamModel skillEntry = (SkillCustomizeParamModel)skillCustomizeParam_mod.SkillCustomizeParamList[0].Clone();
                        skillEntry.CharacodeID = mod_characodeID;
                        skillCustomizeParam_vanilla.SkillCustomizeParamList.Add(skillEntry);
                    }
                }

                //spSkillCustomizeParam file
                SpSkillCustomizeParamViewModel spSkillCustomizeParam_mod = new SpSkillCustomizeParamViewModel();
                if (File.Exists(spSkillCustomizeParamModPath) && !character_mod.Partner) {
                    spSkillCustomizeParam_mod.OpenFile(spSkillCustomizeParamModPath);
                    if (replace_character) {
                        for (int i = 0; i < spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList.Count; i++) {
                            if (spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList[i].CharacodeID == mod_characodeID) {
                                spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList[i] = spSkillCustomizeParam_mod.SpSkillCustomizeParamList[0];
                                break;
                            }
                        }
                    } else {
                        SpSkillCustomizeParamModel spSkillEntry = (SpSkillCustomizeParamModel)spSkillCustomizeParam_mod.SpSkillCustomizeParamList[0].Clone();
                        spSkillEntry.CharacodeID = mod_characodeID;
                        spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList.Add(spSkillEntry);
                    }
                }

                //skillIndexSettingParam file
                SkillIndexSettingParamViewModel skillIndexSettingParam_mod = new SkillIndexSettingParamViewModel();
                if (File.Exists(skillIndexSettingParamModPath) && !character_mod.Partner) {
                    skillIndexSettingParam_mod.OpenFile(skillIndexSettingParamModPath);
                    if (replace_character) {
                        for (int i = 0; i < skillIndexSettingParam_vanilla.SkillIndexSettingParamList.Count; i++) {
                            if (skillIndexSettingParam_vanilla.SkillIndexSettingParamList[i].CharacodeID == mod_characodeID) {
                                skillIndexSettingParam_vanilla.SkillIndexSettingParamList[i] = skillIndexSettingParam_mod.SkillIndexSettingParamList[0];
                                break;
                            }
                        }
                    } else {
                        SkillIndexSettingParamModel skillIndexEntry = (SkillIndexSettingParamModel)skillIndexSettingParam_mod.SkillIndexSettingParamList[0].Clone();
                        skillIndexEntry.CharacodeID = mod_characodeID;
                        skillIndexSettingParam_vanilla.SkillIndexSettingParamList.Add(skillIndexEntry);
                    }
                }

                //supportSkillRecoverySpeedParam file
                SupportSkillRecoverySpeedParamViewModel SupportSkillRecoverySpeedParam_mod = new SupportSkillRecoverySpeedParamViewModel();
                if (File.Exists(supportSkillRecoverySpeedParamModPath) && !character_mod.Partner) {
                    SupportSkillRecoverySpeedParam_mod.OpenFile(supportSkillRecoverySpeedParamModPath);
                    if (replace_character) {
                        for (int i = 0; i < supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList.Count; i++) {
                            if (supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList[i].CharacodeID == mod_characodeID) {
                                supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList[i] = SupportSkillRecoverySpeedParam_mod.SupportSkillRecoverySpeedParamList[0];
                                break;
                            }
                        }
                    } else {
                        SupportSkillRecoverySpeedParamModel supportSkillRecoverySpeedParamEntry = (SupportSkillRecoverySpeedParamModel)SupportSkillRecoverySpeedParam_mod.SupportSkillRecoverySpeedParamList[0].Clone();
                        supportSkillRecoverySpeedParamEntry.CharacodeID = mod_characodeID;
                        supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList.Add(supportSkillRecoverySpeedParamEntry);
                    }
                }

                //privateCamera file
                PrivateCameraViewModel privateCamera_mod = new PrivateCameraViewModel();
                if (File.Exists(privateCameraModPath)) {
                    privateCamera_mod.OpenFile(privateCameraModPath);
                    if (!character_mod.Partner) {
                        if (replace_character) {
                            for (int i = 0; i < privateCamera_vanilla.PrivateCameraList.Count; i++) {
                                if (privateCamera_vanilla.PrivateCameraList[i].CharacodeIndex == mod_characodeID) {
                                    privateCamera_vanilla.PrivateCameraList[i] = privateCamera_mod.PrivateCameraList[0];
                                    break;
                                }
                            }
                        } else {
                            PrivateCameraModel privateCameraEntry = (PrivateCameraModel)privateCamera_mod.PrivateCameraList[0].Clone();
                            privateCameraEntry.CharacodeIndex = mod_characodeID;
                            privateCamera_vanilla.PrivateCameraList.Add(privateCameraEntry);
                        }
                    } else {
                        PrivateCameraModel privateCameraEntry = new PrivateCameraModel();
                        privateCameraEntry.CharacodeIndex = mod_characodeID;
                        privateCameraEntry.Unk1 = -1;
                        privateCameraEntry.Unk2 = -1;
                        privateCameraEntry.FOV = -1;
                        privateCameraEntry.FOV2 = -1;
                        privateCameraEntry.CameraHeight = -1;
                        privateCameraEntry.CameraHeight2 = -1;
                        privateCameraEntry.CameraAngle = -1;
                        privateCameraEntry.CameraDistance = -1;
                        privateCameraEntry.CameraDistance2 = -1;
                        privateCameraEntry.CameraMovement = -1;
                        privateCameraEntry.CameraSpeed = -1;
                        privateCamera_vanilla.PrivateCameraList.Add(privateCameraEntry);
                    }
                }

                //playerIcon file
                PlayerIconViewModel playerIcon_mod = new PlayerIconViewModel();
                if (File.Exists(playerIconModPath) && !character_mod.Partner) {
                    playerIcon_mod.OpenFile(playerIconModPath);
                    if (replace_character) {
                        for (int i = 0; i < playerIcon_vanilla.playerIconList.Count; i++) {
                            if (playerIcon_vanilla.playerIconList[i].CharacodeID == mod_characodeID) {
                                playerIcon_vanilla.playerIconList.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    for (int i = 0; i < playerIcon_mod.playerIconList.Count; i++) {
                        PlayerIconModel playerIconEntry = (PlayerIconModel)playerIcon_mod.playerIconList[i].Clone();
                        playerIconEntry.CharacodeID = mod_characodeID;
                        if (!CharselLoadedIconsList.Contains(playerIconEntry.BaseIcon) && !CharselIconNamesList.Contains(playerIconEntry.BaseIcon)) {
                            CharselIconNamesList.Add(playerIconEntry.BaseIcon);
                        }
                        playerIcon_vanilla.playerIconList.Add(playerIconEntry);
                    }
                }

                //cmnparam file
                cmnparamViewModel cmnparam_mod = new cmnparamViewModel();
                if (File.Exists(cmnparamModPath)) {
                    cmnparam_mod.OpenFile(cmnparamModPath);
                    if (replace_character) {
                        for (int i = 0; i < cmnparam_vanilla.PlayerSndList.Count; i++) {
                            if (cmnparam_vanilla.PlayerSndList[i].PlayerCharacode == mod_characode) {
                                cmnparam_vanilla.PlayerSndList[i] = cmnparam_mod.PlayerSndList[0];
                                break;
                            }
                        }
                    } else {
                        player_sndModel playerSndEntry = (player_sndModel)cmnparam_mod.PlayerSndList[0].Clone();
                        cmnparam_vanilla.PlayerSndList.Add(playerSndEntry);
                    }
                }

                //characterSelectParam file
                CharacterSelectParamViewModel characterSelectParam_mod = new CharacterSelectParamViewModel();
                if (File.Exists(characterSelectParamModPath) && !character_mod.Partner) {
                    characterSelectParam_mod.OpenFile(characterSelectParamModPath);

                    foreach (CharacterSelectParamModel csp_entry in characterSelectParam_mod.CharacterSelectParamList) {

                        if (csp_code_replace.ContainsKey(csp_entry.CSP_code)) {
                            csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                        }
                    }




                    int page = -1;
                    int slot = -1;
                    if (replace_character) {
                        for (int i = 0; i < characterSelectParam_vanilla.CharacterSelectParamList.Count; i++) {
                            if (RemovedCSPCodes.Contains(characterSelectParam_vanilla.CharacterSelectParamList[i].CSP_code)) {
                                if (page == -1) {
                                    page = characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex;
                                    slot = characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex;
                                }
                            }
                        }
                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++) {
                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();
                            csp_entry.PageIndex = page;
                            csp_entry.SlotIndex = slot;
                            csp_entry.CostumeIndex = i;
                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                        }
                    } else {
                        page = character_mod.Page;
                        slot = character_mod.Slot;
                        if (character_mod.Page == -1) {
                            page = characterSelectParam_vanilla.MaxPage();
                            slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                            if (slot == 25) {
                                page++;
                                slot = 1;
                            }
                        }
                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++) {
                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();
                            csp_entry.PageIndex = page;
                            csp_entry.SlotIndex = slot;
                            csp_entry.CostumeIndex = i;
                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                        }
                    }
                }


                //supportActionParam file
                SupportActionParamViewModel supportActionParam_mod = new SupportActionParamViewModel();
                if (File.Exists(supportActionParamModPath) && !character_mod.Partner) {
                    supportActionParam_mod.OpenFile(supportActionParamModPath);
                    for (int i = 0; i < supportActionParam_vanilla.SupportActionParamList.Count; i++) {
                        if (supportActionParam_vanilla.SupportActionParamList[i].CharacodeID == mod_characodeID) {
                            supportActionParam_vanilla.SupportActionParamList[i] = supportActionParam_mod.SupportActionParamList[0];
                            break;
                        }
                    }
                    SupportActionParamModel supportActionParamEntry = (SupportActionParamModel)supportActionParam_mod.SupportActionParamList[0].Clone();
                    supportActionParamEntry.CharacodeID = mod_characodeID;
                    supportActionParam_vanilla.SupportActionParamList.Add(supportActionParamEntry);
                }

                /*---------------------------------------NOT REQUIRED FILES-------------------------------------------*/
                //costumeBreakParam file
                CostumeBreakParamViewModel costumeBreakParam_mod = new CostumeBreakParamViewModel();
                if (File.Exists(costumeBreakParamModPath) && !character_mod.Partner) {
                    costumeBreakParam_mod.OpenFile(costumeBreakParamModPath);
                    //Remove old entries
                    for (int i = 0; i < costumeBreakParam_vanilla.CostumeBreakParamList.Count; i++) {
                        if (costumeBreakParam_vanilla.CostumeBreakParamList[i].CharacodeID == mod_characodeID) {
                            costumeBreakParam_vanilla.CostumeBreakParamList.RemoveAt(i);
                            i--;
                        }
                    }
                    //Add new entries
                    for (int i = 0; i < costumeBreakParam_mod.CostumeBreakParamList.Count; i++) {
                        CostumeBreakParamModel costumeColor_entry = (CostumeBreakParamModel)costumeBreakParam_mod.CostumeBreakParamList[i].Clone();
                        costumeColor_entry.CharacodeID = mod_characodeID;
                        costumeBreakParam_vanilla.CostumeBreakParamList.Add(costumeColor_entry);
                    }
                }

                //AwakeAura file
                AwakeAuraViewModel awakeeAura_mod = new AwakeAuraViewModel();
                if (File.Exists(awakeAuraModPath) && !character_mod.Partner) {
                    awakeeAura_mod.OpenFile(awakeAuraModPath);
                    for (int i = 0; i < awakeAura_vanilla.AwakeAuraList.Count; i++) {
                        if (awakeAura_vanilla.AwakeAuraList[i].Characode == mod_characode) {
                            awakeAura_vanilla.AwakeAuraList.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < awakeeAura_mod.AwakeAuraList.Count; i++) {
                        awakeAura_vanilla.AwakeAuraList.Add((AwakeAuraModel)awakeeAura_mod.AwakeAuraList[i].Clone());
                    }
                }
                //AppearanceAnm file
                AppearanceAnmViewModel appearanceAnm_mod = new AppearanceAnmViewModel();
                if (File.Exists(appearanceAnmModPath) && !character_mod.Partner) {
                    appearanceAnm_mod.OpenFile(appearanceAnmModPath);
                    for (int i = 0; i < appearanceAnm_vanilla.AppearanceAnmList.Count; i++) {
                        if (appearanceAnm_vanilla.AppearanceAnmList[i].CharacodeID == mod_characodeID) {
                            appearanceAnm_vanilla.AppearanceAnmList.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < appearanceAnm_mod.AppearanceAnmList.Count; i++) {
                        AppearanceAnmModel appearanceAnmEntry = (AppearanceAnmModel)appearanceAnm_mod.AppearanceAnmList[i].Clone();
                        appearanceAnmEntry.CharacodeID = mod_characodeID;
                        appearanceAnm_vanilla.AppearanceAnmList.Add(appearanceAnmEntry);
                    }
                }
                //afterAttachObject file
                AfterAttachObjectViewModel afterAttachObject_mod = new AfterAttachObjectViewModel();
                if (File.Exists(afterAttachObjectModPath) && !character_mod.Partner) {
                    afterAttachObject_mod.OpenFile(afterAttachObjectModPath);
                    for (int i = 0; i < afterAttachObject_vanilla.AfterAttachObjectList.Count; i++) {
                        if (baseModel.Contains(afterAttachObject_vanilla.AfterAttachObjectList[i].Characode)
                            || awakeModel.Contains(afterAttachObject_vanilla.AfterAttachObjectList[i].Characode)
                            || afterAttachObject_vanilla.AfterAttachObjectList[i].Costume == mod_characode) {
                            afterAttachObject_vanilla.AfterAttachObjectList.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < afterAttachObject_mod.AfterAttachObjectList.Count; i++) {
                        afterAttachObject_vanilla.AfterAttachObjectList.Add((AfterAttachObjectModel)afterAttachObject_mod.AfterAttachObjectList[i].Clone());
                    }
                }
                //playerDoubleEffectParam file
                PlayerDoubleEffectParamViewModel playerDoubleEffectParam_mod = new PlayerDoubleEffectParamViewModel();
                if (File.Exists(playerDoubleEffectParamModPath) && !character_mod.Partner) {
                    playerDoubleEffectParam_mod.OpenFile(playerDoubleEffectParamModPath);
                    for (int i = 0; i < playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.Count; i++) {
                        if (playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList[i].CharacodeID == mod_characodeID) {
                            playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < playerDoubleEffectParam_mod.PlayerDoubleEffectParamList.Count; i++) {
                        PlayerDoubleEffectParamModel playerDoubleEffectEntry = (PlayerDoubleEffectParamModel)playerDoubleEffectParam_mod.PlayerDoubleEffectParamList[i].Clone();
                        playerDoubleEffectEntry.CharacodeID = mod_characodeID;
                        playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.Add(playerDoubleEffectEntry);
                    }
                }
                //spTypeSupportParam file
                SpTypeSupportParamViewModel spTypeSupportParam_mod = new SpTypeSupportParamViewModel();
                if (File.Exists(spTypeSupportParamModPath) && !character_mod.Partner) {
                    spTypeSupportParam_mod.OpenFile(spTypeSupportParamModPath);
                    for (int i = 0; i < spTypeSupportParam_vanilla.SpTypeSupportParamList.Count; i++) {
                        if (spTypeSupportParam_vanilla.SpTypeSupportParamList[i].CharacodeID == mod_characodeID) {
                            spTypeSupportParam_vanilla.SpTypeSupportParamList.RemoveAt(i);
                            break;
                        }
                    }
                    SpTypeSupportParamModel spTypeSupportParamEntry = (SpTypeSupportParamModel)spTypeSupportParam_mod.SpTypeSupportParamList[0].Clone();
                    spTypeSupportParamEntry.CharacodeID = mod_characodeID;
                    spTypeSupportParam_vanilla.SpTypeSupportParamList.Add(spTypeSupportParamEntry);
                }

                //specialCondParam file
                byte[] specialCondParam_mod = new byte[0];
                if (File.Exists(specialCondParamModPath) && !character_mod.Partner) {
                    specialCondParam_mod = File.ReadAllBytes(specialCondParamModPath);
                    specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                    specialCondParam_vanilla = BinaryReader.b_AddBytes(specialCondParam_vanilla, specialCondParam_mod);
                }

                //specialCondParam file
                byte[] partnerSlotParam_mod = new byte[0];
                if (File.Exists(partnerSlotParamModPath) && character_mod.Partner) {
                    partnerSlotParam_mod = File.ReadAllBytes(partnerSlotParamModPath);
                    partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                    partnerSlotParam_vanilla = BinaryReader.b_AddBytes(partnerSlotParam_vanilla, partnerSlotParam_mod);
                }

                //susanooCondParam file
                byte[] susanooCondParam_mod = new byte[0];
                if (File.Exists(susanooCondParamModPath) && !character_mod.Partner) {
                    susanooCondParam_mod = File.ReadAllBytes(susanooCondParamModPath);
                    susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                    susanooCondParam_vanilla = BinaryReader.b_AddBytes(susanooCondParam_vanilla, susanooCondParam_mod);
                }

                //messageInfo files
                MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                if (Directory.Exists(messageInfoModPath) && !character_mod.Partner) {
                    messageInfo_mod.OpenFiles(messageInfoModPath);
                    for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++) {
                        // might increase compiling time
                        /*for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++) {
                            string mod_crc32 = BitConverter.ToString(messageInfo_vanilla.MessageInfo_List[l][i].CRC32Code);
                            for (int c = 0; c < messageInfo_vanilla.MessageInfo_List[l].Count; c++) {
                                string vanilla_crc32 = BitConverter.ToString(messageInfo_vanilla.MessageInfo_List[l][c].CRC32Code);
                                if (vanilla_crc32 == mod_crc32) {
                                    messageInfo_vanilla.MessageInfo_List[c].RemoveAt(i);
                                    break;
                                }
                                
                            }
                        }*/
                        for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++) {
                            messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                        }
                    }
                    messageInfoModified = true;

                }
                //damageprm file
                DamagePrmViewModel damageprm_mod = new DamagePrmViewModel();
                if (File.Exists(damageprmModPath)) {
                    damageprm_mod.OpenFile(damageprmModPath);
                    for (int i = 0; i < damageprm_mod.DamagePrmList.Count; i++) {
                        damageprm_vanilla.DamagePrmList.Add((DamagePrmModel)damageprm_mod.DamagePrmList[i].Clone());
                    }
                }

                //prm
                PRMEditorViewModel prm_mod = new PRMEditorViewModel();

                DirectoryInfo mod_d = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(character_mod.RootPath)));
                FileInfo[] characterPrmList = mod_d.GetFiles(mod_characode + "prm.bin.xfbin", SearchOption.AllDirectories);
                Array.Sort(characterPrmList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));

                string prm_path = "";
                foreach (FileInfo prm_file in characterPrmList) {
                    prm_path = prm_file.FullName;
                }
                if (File.Exists(prm_path) && (prm_path ?? "") != "") {
                    string new_prm_path = root_folder + "\\param_files\\" + prm_path.Remove(0, prm_path.IndexOf("data\\"));
                    //damageeff
                    DamageEffViewModel damageeff_mod = new DamageEffViewModel();
                    //effectprm
                    EffectPrmViewModel effectprm_mod = new EffectPrmViewModel();
                    if (File.Exists(prm_path) && File.Exists(damageeffModPath)) {
                        //This function merges damageEff and effectprm files, and fixing prm files with new damageEff ids
                        damageeff_mod.OpenFile(damageeffModPath);
                        if (damageeff_mod.DamageEffList.Count > 0) {
                            List<int> NewEffectIds = new List<int>();

                            if (File.Exists(effectprmModPath)) {
                                effectprm_mod.OpenFile(effectprmModPath);
                                //This code adds effectprm entries to vanilla/edited files and saves new and olds effectprm ids
                                for (int j = 0; j < effectprm_mod.EffectPrmList.Count; j++) {
                                    NewEffectIds.Add(effectprm_vanilla.MaxEffectID() + 1);
                                    effectprm_mod.EffectPrmList[j].EffectPrmID = effectprm_vanilla.MaxEffectID() + 1;
                                    effectprm_vanilla.EffectPrmList.Add((EffectPrmModel)effectprm_mod.EffectPrmList[j].Clone());
                                }
                            }

                            List<int> OldHitIds = new List<int>();
                            List<int> NewHitIds = new List<int>();
                            //This code changes all effectprm ids in modded damageEff file
                            for (int c = 0; c < damageeff_mod.DamageEffList.Count; c++) {
                                damageeff_mod.DamageEffList[c].EffectPrmID = NewEffectIds[c];
                                damageeff_mod.DamageEffList[c].ExtraEffectPrmID = 0;

                            }
                            //This code adding new entries to vanilla/edited damageEff file and changes damageEff ids
                            for (int c = 0; c < damageeff_mod.DamageEffList.Count; c++) {
                                int maxValue = damageeff_vanilla.MaxDamageID();
                                OldHitIds.Add(damageeff_mod.DamageEffList[c].DamageEffID);
                                NewHitIds.Add(maxValue + 1);

                                DamageEffModel damageeff_entry = (DamageEffModel)damageeff_mod.DamageEffList[c].Clone();
                                damageeff_entry.DamageEffID = maxValue + 1;
                                if (OldHitIds.Contains(damageeff_entry.ExtraDamageEffID)) {
                                    damageeff_entry.ExtraDamageEffID = NewHitIds[OldHitIds.IndexOf(damageeff_entry.ExtraDamageEffID)];
                                }

                                damageeff_vanilla.DamageEffList.Add(damageeff_entry);
                            }
                            //This code opening prm file of character mod
                            prm_mod.OpenFile(prm_path);
                            //This function checking each movement section 
                            for (int ver = 0; ver < prm_mod.VerList.Count; ver++) {
                                for (int pl_anm = 0; pl_anm < prm_mod.VerList[ver].PL_ANM_Sections.Count; pl_anm++) {
                                    for (int function = 0; function < prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList.Count; function++) {
                                        int selectedhit = prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList[function].DamageHitEffectID;
                                        if (selectedhit != 0) {
                                            for (int g = 0; g < OldHitIds.Count; g++) {
                                                //This code checking for old damageEff Ids and changing them on new ids
                                                if (OldHitIds[g] == selectedhit) {
                                                    prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList[function].DamageHitEffectID = (Int16)NewHitIds[g];
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            //Creates directory
                            if (!Directory.Exists(Path.GetDirectoryName(new_prm_path))) {
                                Directory.CreateDirectory(Path.GetDirectoryName(new_prm_path));
                            }
                            //Saves edited prm file
                            prm_mod.SaveFileAs(new_prm_path);
                        }

                    }
                }
                


            }
            //Compile Stage Mods
            foreach (StageModModel stage_mod in StageList) {

                string messageInfoModPath = stage_mod.RootPath + "\\data\\message";
                string stageInfoModPath = stage_mod.RootPath + "\\data\\stage\\StageInfo.bin.xfbin";

                string mod_stagename = stage_mod.StageName;
                int mod_stageID = -1;
                int BGM_ID = Convert.ToInt32(stage_mod.BgmID);
                bool replace_stage = false;

                //Read StageInfo file and find entry
                for (int i = 0; i< stageInfo_vanilla.StageInfoList.Count; i++) {
                    if (stageInfo_vanilla.StageInfoList[i].StageName == mod_stagename) {
                        mod_stageID = i;
                        replace_stage = true;
                        break;
                    }
                }
                StageInfoViewModel stageInfo_mod = new StageInfoViewModel();
                if (File.Exists(stageInfoModPath)) {
                    stageInfo_mod.OpenFile(stageInfoModPath);
                    stageInfoModified = true;
                    if (replace_stage) {
                        stageInfo_vanilla.StageInfoList[mod_stageID] = (StageInfoModel)stageInfo_mod.StageInfoList[0].Clone();
                    } else {
                        StagesToAdd.Add(stage_mod);
                        stageInfo_vanilla.StageInfoList.Add((StageInfoModel)stageInfo_mod.StageInfoList[0].Clone());
                    }
                }
                //messageInfo files
                MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                if (Directory.Exists(messageInfoModPath)) {
                    messageInfo_mod.OpenFiles(messageInfoModPath);
                    for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++) {
                        for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++) {
                            messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                        }
                    }
                    messageInfoModified = true;

                }


            }
            //Compile Model mods
            foreach (CostumeModModel costume_mod in CostumeList) {
                string mod_characode = costume_mod.Characode;
                int mod_characodeID = -1;
                bool replace_character = false;
                string main_psp_code = "";
                int main_psp_id = -1;
                int costume_index = -1;
                //Read Characode file and add/find entry
                foreach (CharacodeEditorModel entry in characode_vanilla.CharacodeList) {
                    if (entry.CharacodeName == mod_characode) {
                        mod_characodeID = entry.CharacodeIndex;
                        replace_character = true;
                        break;
                    }
                }
                if (!replace_character)
                    continue;


                string playerSettingParamModPath = costume_mod.RootPath + "\\data\\spc\\playerSettingParam.bin.xfbin";
                string characterSelectParamModPath = costume_mod.RootPath + "\\data\\ui\\max\\select\\characterSelectParam.xfbin";
                string costumeBreakColorParamModPath = costume_mod.RootPath + "\\data\\spc\\costumeBreakColorParam.xfbin";
                string costumeParamModPath = costume_mod.RootPath + "\\data\\rpg\\param\\costumeParam.bin.xfbin";
                string costumeBreakParamModPath = costume_mod.RootPath + "\\data\\spc\\costumeBreakParam.xfbin";
                string afterAttachObjectModPath = costume_mod.RootPath + "\\data\\spc\\afterAttachObject.xfbin";
                string playerIconModPath = costume_mod.RootPath + "\\data\\spc\\player_icon.xfbin";
                string messageInfoModPath = costume_mod.RootPath + "\\data\\message";

                //check if any costume exist for character
                foreach (PlayerSettingParamModel psp_entry in playerSettingParam_vanilla.PlayerSettingParamList) {
                    if (psp_entry.CharacodeID == mod_characodeID) {
                        main_psp_code = psp_entry.PSP_code;
                        main_psp_id = psp_entry.PSP_ID;
                        break;
                    }
                }
                if (main_psp_code == "")
                    continue;



                //check if there is free slot for character
                for (int i = 0; i<duelPlayerParam_vanilla.DuelPlayerParamList.Count; i++) {
                    if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BinName.Contains(mod_characode)) {
                        for (int c = 0; c<20; c++) {
                            if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BaseCostumes[c].CostumeName == "") {
                                duelPlayerParam_vanilla.DuelPlayerParamList[i].BaseCostumes[c].CostumeName = costume_mod.BaseCostume;
                                duelPlayerParam_vanilla.DuelPlayerParamList[i].AwakeCostumes[c].CostumeName = costume_mod.AwakeCostume;
                                costume_index = c;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (costume_index == -1)
                    continue;


                //playerSettingParam file
                int new_preset_id = 0;
                string charMessageID = "";
                string costume_csp_code = "";
                PlayerSettingParamViewModel playerSettingParam_mod = new PlayerSettingParamViewModel();
                if (File.Exists(playerSettingParamModPath)) {
                    playerSettingParam_mod.OpenFile(playerSettingParamModPath);
                    PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[0].Clone();
                    costume_csp_code = psp_entry.PSP_code;
                    int csp_code_index = 0;
                    do {
                        csp_code_index++;
                        costume_csp_code = psp_entry.PSP_code + "_" + csp_code_index.ToString("D6");
                    }
                    while (playerSettingParam_vanilla.PSPCodeExists(costume_csp_code));
                    psp_entry.PSP_code = costume_csp_code;
                    psp_entry.CharacodeID = mod_characodeID;
                    psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot()+1;
                    charMessageID = psp_entry.CharacterNameMessageID;
                    new_preset_id = playerSettingParam_vanilla.MaxSlot() + 1;
                    psp_entry.MainPSP_ID = main_psp_id;
                    psp_entry.CostumeID = costume_index;
                    playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                }

                //costumeParam file
                CostumeParamViewModel costumeParam_mod = new CostumeParamViewModel();
                if (File.Exists(costumeParamModPath)) {
                    costumeParam_mod.OpenFile(costumeParamModPath);
                    //Add new entries
                    for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++) {
                        CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                        costume_entry.PlayerSettingParamID = new_preset_id;
                        costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                        costume_entry.EntryIndex = 0; //used for unlocking
                        costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                    }
                }

                //afterAttachObject file
                AfterAttachObjectViewModel afterAttachObject_mod = new AfterAttachObjectViewModel();
                if (File.Exists(afterAttachObjectModPath)) {
                    afterAttachObject_mod.OpenFile(afterAttachObjectModPath);
                    for (int i = 0; i < afterAttachObject_vanilla.AfterAttachObjectList.Count; i++) {
                        if (costume_mod.BaseCostume == afterAttachObject_vanilla.AfterAttachObjectList[i].Characode
                            || costume_mod.AwakeCostume == afterAttachObject_vanilla.AfterAttachObjectList[i].Characode
                            || afterAttachObject_vanilla.AfterAttachObjectList[i].Costume == mod_characode) {
                            afterAttachObject_vanilla.AfterAttachObjectList.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < afterAttachObject_mod.AfterAttachObjectList.Count; i++) {
                        afterAttachObject_vanilla.AfterAttachObjectList.Add((AfterAttachObjectModel)afterAttachObject_mod.AfterAttachObjectList[i].Clone());
                    }
                }
                //costumeBreakParam file
                CostumeBreakParamViewModel costumeBreakParam_mod = new CostumeBreakParamViewModel();
                if (File.Exists(costumeBreakParamModPath)) {
                    costumeBreakParam_mod.OpenFile(costumeBreakParamModPath);
                    //Add new entries
                    for (int i = 0; i < costumeBreakParam_mod.CostumeBreakParamList.Count; i++) {
                        CostumeBreakParamModel costumeColor_entry = (CostumeBreakParamModel)costumeBreakParam_mod.CostumeBreakParamList[i].Clone();
                        costumeColor_entry.CharacodeID = mod_characodeID;
                        costumeColor_entry.CostumeID = costume_index;
                        costumeBreakParam_vanilla.CostumeBreakParamList.Add(costumeColor_entry);
                    }
                }
                //costumeColorBreakParam file
                CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                if (File.Exists(costumeBreakColorParamModPath)) {
                    costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                    //Add new entries
                    for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++) {
                        CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                        costumeColor_entry.PlayerSettingParamID = new_preset_id;
                        costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                    }
                }

                //playerIcon file
                PlayerIconViewModel playerIcon_mod = new PlayerIconViewModel();
                if (File.Exists(playerIconModPath)) {
                    playerIcon_mod.OpenFile(playerIconModPath);
                    PlayerIconModel playerIconEntry = (PlayerIconModel)playerIcon_mod.playerIconList[0].Clone();
                    playerIconEntry.CharacodeID = mod_characodeID;
                    playerIconEntry.CostumeID = costume_index;
                    if (!CharselLoadedIconsList.Contains(playerIconEntry.BaseIcon) && !CharselIconNamesList.Contains(playerIconEntry.BaseIcon)) {
                        CharselIconNamesList.Add(playerIconEntry.BaseIcon);
                    }
                    playerIcon_vanilla.playerIconList.Add(playerIconEntry);
                }

                //characterSelectParam
                CharacterSelectParamViewModel characterSelectParam_mod = new CharacterSelectParamViewModel();
                if (File.Exists(characterSelectParamModPath)) {
                    characterSelectParam_mod.OpenFile(characterSelectParamModPath);

                    int page = 0;
                    int slot = 1;
                    int costume = 0;

                    for (int i = 0; i<characterSelectParam_vanilla.CharacterSelectParamList.Count; i++) {
                        if (characterSelectParam_vanilla.CharacterSelectParamList[i].CSP_code == main_psp_code) {
                            page = characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex;
                            slot = characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex;
                            break;
                        }
                    }
                    for (int i = 0; i < characterSelectParam_vanilla.CharacterSelectParamList.Count; i++) {
                        if (characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex == page && characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex == slot) {
                            costume++;
                        }
                    }

                    CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[0].Clone();
                    csp_entry.CSP_code = costume_csp_code;
                    csp_entry.PageIndex = page;
                    csp_entry.SlotIndex = slot;
                    csp_entry.CostumeIndex = costume;
                    characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                }

                //messageInfo files
                MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                if (Directory.Exists(messageInfoModPath)) {
                    messageInfo_mod.OpenFiles(messageInfoModPath);
                    for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++) {
                        for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++) {
                            messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                        }
                    }

                    messageInfoModified = true;
                }

            }

            string param_modmanager_path = root_folder + "\\param_files\\";
            byte[] nuccMaterialFile = File.ReadAllBytes(nuccMaterialDx11Path); // This function reading all bytes from nuccMaterial_dx11 file
                                                                               //unpack CPKs
            if (!Directory.Exists(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\charicon_s\\"))
                Directory.CreateDirectory(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\charicon_s\\");
            foreach (ModManagerModel mod in ModManagerList) {

                if (mod.EnableMod) {
                    DirectoryInfo mod_d = new DirectoryInfo(mod.ModFolder);
                    //save shaders
                    FileInfo[] shaderList = mod_d.GetFiles("*.hlsl", SearchOption.AllDirectories);
                    Array.Sort(shaderList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                    int ShaderCount = BinaryReader.b_ReadInt16(nuccMaterialFile, 0x0E);
                    List<string> UsedShaders = new List<string>();
                    foreach (FileInfo shader in shaderList) {
                        byte[] shader_data = File.ReadAllBytes(shader.FullName);
                        string shader_name = BitConverter.ToString(BinaryReader.b_ReadByteArray(shader_data,0,4));
                        if (!UsedShaders.Contains(shader_name)) {
                            nuccMaterialFile = BinaryReader.b_AddBytes(nuccMaterialFile, shader_data);
                            ShaderCount++;
                            UsedShaders.Add(shader_name); //Adding name of shader in list of used shaders
                        }
                    }
                    nuccMaterialFile = BinaryReader.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes((short)ShaderCount), 0x0E, 0); //Replacing byte of shader's count
                    nuccMaterialFile = BinaryReader.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes(nuccMaterialFile.Length), 0x04, 0); //Replacing size bytes of nuccMaterial_dx11 file
                    
                    FileInfo[] cpkList = mod_d.GetFiles("*.cpk", SearchOption.AllDirectories);

                    Array.Sort(cpkList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
                    foreach (FileInfo cpk in cpkList) {
                        YaCpkTool.CPK_extract(@Path.GetFullPath(cpk.FullName));
                        string file_name = Path.GetFileNameWithoutExtension(cpk.FullName);

                        Program.CopyFilesRecursively(Path.GetDirectoryName(cpk.FullName) + "\\" + file_name, root_folder + "\\cpk_assets");
                        if (Directory.Exists(Path.GetDirectoryName(cpk.FullName) + "\\" + file_name))
                            Directory.Delete(Path.GetDirectoryName(cpk.FullName) + "\\" + file_name, true);
                    }

                    //copy data_win32 files
                    if (!Directory.Exists(root_folder + "\\data_win32_modmanager")) {
                        Directory.CreateDirectory(root_folder + "\\data_win32_modmanager");
                    }
                    if (Directory.Exists(mod.ModFolder + "\\Resources\\Files\\"))
                        Program.CopyFilesRecursivelyModManager(mod.ModFolder + "\\Resources\\Files\\", root_folder + "\\data_win32_modmanager\\");

                }
            }
            File.WriteAllBytes(root_folder + "\\data\\system\\nuccMaterial_dx11.nsh", nuccMaterialFile);

            //charsel.gfx - dont really need to be changed with updates
            byte[] charsel_gfx = File.ReadAllBytes(charselGfxPath);
            //int charsel_offset_1 = 0x21672; // 8 + 1 + count of pages
            int charsel_offset_2 = 0x419EF; // 1 + count of pages
            //charsel_gfx[charsel_offset_1] = (byte)(8 + 1 + characterSelectParam_vanilla.MaxPage());
            charsel_gfx[charsel_offset_2] = (byte)(1 + characterSelectParam_vanilla.MaxPage());
            string charsel_updated_path = Properties.Settings.Default.RootGameFolder + "\\data\\ui\\flash\\OTHER\\charsel\\charsel.gfx";
            File.WriteAllBytes(charsel_updated_path, charsel_gfx);

            DirectoryInfo default_icons = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\ParamFiles\\DefaultIcons\\");
            FileInfo[] DefaultIconList = default_icons.GetFiles("*.xfbin", SearchOption.AllDirectories);
            Array.Sort(DefaultIconList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
            foreach (FileInfo icon in DefaultIconList) {
                File.Copy(icon.FullName, root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\charicon_s\\" + Path.GetFileNameWithoutExtension(icon.FullName) + ".xfbin", true);
                CharselIconNamesList.Add(Path.GetFileNameWithoutExtension(icon.FullName).Replace("_charicon_s", ""));
            }

            for (int i =0; i< CharselIconNamesList.Count; i++) {
                if (!File.Exists(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\charicon_s\\" + CharselIconNamesList[i] + "_charicon_s.xfbin") &&
                    !File.Exists(root_folder + "\\data_win32_modmanager\\data\\ui\\flash\\OTHER\\charicon_s\\" + CharselIconNamesList[i] + "_charicon_s.xfbin") &&
                    !File.Exists(Directory.GetCurrentDirectory() + "\\ParamFiles\\DefaultIcons\\" + CharselIconNamesList[i] + "_charicon_s.xfbin")) {
                    CharselIconNamesList.RemoveAt(i);
                    i--;
                }
            }


            //charicon_s.gfx
            byte[] charicon_s_filebytes = File.ReadAllBytes(chariconGfxPath);
            string charicon_s_updated_path = Properties.Settings.Default.RootGameFolder + "\\data\\ui\\flash\\OTHER\\charicon_s\\charicon_s.gfx";
            byte[] charicon_s_header = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0, 0xCB);
            byte[] charicon_s_body1 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0xCB, 0x4580);
            byte[] charicon_s_body2 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x464B, 0x120A);
            byte[] charicon_s_end = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x5855, 0x1502B); //0x08,0x15,0x7D,0x14F7D - change counts!
            byte[] charicon_s_newFile = new byte[0];
            int icon_count = 0x1CE;
            int icon_count2 = 0xE3;
            int external_image_count = 5;
            for (int i = 0; i < CharselIconNamesList.Count; i++) {
                string IconName = CharselIconNamesList[i];
                byte[] charicon_s_extra_files = new byte[0];
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes((0x4C + (IconName + "_charicon_s.dds").Length)), 0, 0, 1);
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, new byte[0x1] { 0xFC });
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes(external_image_count + i), 0, 0, 2);
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, new byte[0x9] { 0x09, 0x00, 0x0E, 0x00, 0x80, 0x00, 0x80, 0x00, 0x00 });
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes((IconName + "_charicon_s.dds").Length), 0, 0, 1);
                charicon_s_extra_files = BinaryReader.b_AddBytes(charicon_s_extra_files, Encoding.ASCII.GetBytes(IconName + "_charicon_s.dds"));
                charicon_s_header = BinaryReader.b_AddBytes(charicon_s_header, charicon_s_extra_files);
                byte[] charicon_s_section_temp = new byte[0x47] { 0x0C, 0xFC, 0x85, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x80, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00, 0x86, 0x01, 0x65, 0x80, 0x28, 0x05, 0x80, 0x28, 0x00, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41, 0x85, 0x01, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0xB0, 0x0B, 0x00, 0x00, 0x20, 0x15, 0x96, 0x01, 0x60, 0x17, 0x62, 0x80, 0x3B, 0x54, 0x01, 0xD9, 0x60, 0x0E, 0xDB, 0x00, 0x00 }; //0x02,0x14,0x29 - counts, 0x04 - DDS ID, 0x06 - x1, 0x08 - y1, 0x0A - x2, 0x0C - y2
                charicon_s_section_temp = BinaryReader.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes((Int16)(icon_count + (i * 2))), 0x2, 0, 2);
                charicon_s_section_temp = BinaryReader.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes((Int16)((icon_count + 1) + (i * 2))), 0x14, 0, 2);
                charicon_s_section_temp = BinaryReader.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes((Int16)(icon_count + (i * 2))), 0x29, 0, 2);
                charicon_s_section_temp = BinaryReader.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes((Int16)(external_image_count + i)), 0x4, 0, 2);
                charicon_s_body1 = BinaryReader.b_AddBytes(charicon_s_body1, charicon_s_section_temp);
                byte[] charicon_s_name = new byte[0];
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, new byte[2] { 0xFF, 0x0A });
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, BitConverter.GetBytes(IconName.Length + 1));
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, Encoding.ASCII.GetBytes(IconName));
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, new byte[6] { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00 });
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, BitConverter.GetBytes((Int16)((icon_count + 1) + (i * 2))), 0, 0);
                charicon_s_name = BinaryReader.b_AddBytes(charicon_s_name, new byte[2] { 0x40, 0x00 });
                charicon_s_body2 = BinaryReader.b_AddBytes(charicon_s_body2, charicon_s_name);
            }
            charicon_s_body2 = BinaryReader.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(charicon_s_body2.Length - 4), 0x02, 0);
            charicon_s_body2 = BinaryReader.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(icon_count + (CharselIconNamesList.Count * 2)), 0x06, 0, 2);
            charicon_s_body2 = BinaryReader.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(icon_count2 + CharselIconNamesList.Count), 0x08, 0, 2);
            charicon_s_end = BinaryReader.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(icon_count + 1 + (CharselIconNamesList.Count * 2)), 0x08, 0, 2);
            charicon_s_end = BinaryReader.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(icon_count + (CharselIconNamesList.Count * 2)), 0x15, 0, 2);
            charicon_s_end = BinaryReader.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(icon_count + 1 + (CharselIconNamesList.Count * 2)), 0x7D, 0, 2);
            charicon_s_end = BinaryReader.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(icon_count + (CharselIconNamesList.Count * 2)), 0x14F7D, 0, 2);
            charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_header);
            charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_body1);
            charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_body2);
            charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_end);
            charicon_s_newFile = BinaryReader.b_ReplaceBytes(charicon_s_newFile, BitConverter.GetBytes((int)charicon_s_newFile.Length), 0x04, 0);
            File.WriteAllBytes(charicon_s_updated_path, charicon_s_newFile);


            Directory.CreateDirectory(param_modmanager_path);
            Directory.CreateDirectory(param_modmanager_path + "data\\spc");
            Directory.CreateDirectory(param_modmanager_path + "data\\rpg\\param");
            Directory.CreateDirectory(param_modmanager_path + "data\\ui\\max\\select");
            Directory.CreateDirectory(param_modmanager_path + "data\\stage");
            Directory.CreateDirectory(param_modmanager_path + "data\\sound");

            if (stageInfoModified) {
                //select_stage
                int stage_count = 67;
                byte[] stageSel_file = File.ReadAllBytes(stage_selectPath);
                byte[] stagesel_header = BinaryReader.b_ReadByteArray(stageSel_file, 0, 0x13C);
                byte[] stagesel_body = BinaryReader.b_ReadByteArray(stageSel_file, 0x13C, 0x1298);
                byte[] stagesel_end = BinaryReader.b_ReadByteArray(stageSel_file, 0x13DE, 0x14);
                byte[] stagesel_xml_add = new byte[0];
                byte[] stagesel_new_file = new byte[0];
                for (int st = 0; st < StagesToAdd.Count; st++) {

                    //BGMs
                    if (st < Program.StageBGMSlots.Length) {
                        byte[] stageBGM_slot = new byte[0];
                        stageBGM_slot = BinaryReader.b_AddBytes(stageBGM_slot, BinaryReader.crc32(StagesToAdd[st].StageName));
                        stageBGM_slot = BinaryReader.b_AddBytes(stageBGM_slot, BitConverter.GetBytes(StagesToAdd[st].BgmID));
                        File.WriteAllBytes(root_folder + "//moddingapi//mods//base_game//" + Program.StageBGMSlots[st].ToString("X") + ".ns4p", stageBGM_slot);
                    }


                    byte[] xml_line = new byte[0x0E] { 0x0D, 0x0A, 0x09, 0x3C, 0x73, 0x74, 0x61, 0x67, 0x65, 0x20, 0x69, 0x64, 0x3D, 0x22 };
                    xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes((stage_count + st).ToString()));
                    xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x0A] { 0x22, 0x20, 0x6E, 0x61, 0x6D, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                    xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(StagesToAdd[st].MessageID));
                    xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x0B] { 0x22, 0x20, 0x73, 0x74, 0x61, 0x67, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                    xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(StagesToAdd[st].StageName));
                    int hellID = 0;
                    if (StagesToAdd[st].Hell)
                        hellID = 1;

                    xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x08] { 0x22, 0x20, 0x68, 0x65, 0x6C, 0x6C, 0x3D, 0x22 });
                    xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(hellID.ToString()));
                    xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x03] { 0x22, 0x2F, 0x3E });
                    stagesel_xml_add = BinaryReader.b_AddBytes(stagesel_xml_add, xml_line);
                    byte[] st_img_body = new byte[0];
                    if (File.Exists(StagesToAdd[st].RootPath + "\\stage_preview.png")) {
                        st_img_body = File.ReadAllBytes(StagesToAdd[st].RootPath + "\\stage_preview.png");
                    } else {
                        st_img_body = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Resources\\TemplateImages\\stage_tex.png");
                    }
                    byte[] st_img_new_file = BinaryReader.MakeXfbinBinary("Z:/char/x/stagesel/tex/tex_l/st_img_l_" + (stage_count - 1 + st).ToString() + ".png", "st_img_l_" + (stage_count - 1 + st).ToString(), st_img_body);
                    if (!Directory.Exists(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel\\tex_l")) {
                        Directory.CreateDirectory(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel\\tex_l");
                    }
                    File.WriteAllBytes(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel\\tex_l\\st_img_l_" + (stage_count - 1 + st).ToString() + ".xfbin", st_img_new_file);
                    

                    if (File.Exists(StagesToAdd[st].RootPath + "\\stage_icon.dds")) {
                        st_img_body = File.ReadAllBytes(StagesToAdd[st].RootPath + "\\stage_icon.dds");
                    } else {
                        st_img_body = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Resources\\TemplateImages\\stage_icon.dds");
                    }
                    st_img_new_file = BinaryReader.MakeXfbinBinary("D:/usr/flash/char/x/stagesel/" + StagesToAdd[st].StageName + ".dds", "stagesel_image_" + StagesToAdd[st].StageName, st_img_body);
                    if (!Directory.Exists(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel")) {
                        Directory.CreateDirectory(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel");
                    }
                    File.WriteAllBytes(root_folder + "\\cpk_assets\\data\\ui\\flash\\OTHER\\stagesel\\" + "stagesel_image_" + StagesToAdd[st].StageName + ".xfbin", st_img_new_file);

                }
                stagesel_xml_add = BinaryReader.b_AddBytes(stagesel_xml_add, new byte[0x0A] { 0x0D, 0x0A, 0x3C, 0x2F, 0x5F, 0x72, 0x6F, 0x6F, 0x74, 0x3E });
                stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_header);
                stagesel_new_file = BinaryReader.b_ReplaceBytes(stagesel_new_file, BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length), 0x138, 1);
                stagesel_new_file = BinaryReader.b_ReplaceBytes(stagesel_new_file, BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length + 4), 0x12C, 1);
                stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_body);
                stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_xml_add);
                stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_end);
                if (!Directory.Exists(root_folder + "\\cpk_assets\\data\\ui\\max\\select\\"))
                    Directory.CreateDirectory(root_folder + "\\cpk_assets\\data\\ui\\max\\select\\");
                File.WriteAllBytes(root_folder + "\\cpk_assets\\data\\ui\\max\\select\\select_stage.xfbin", stagesel_new_file);

                //stagesel_image.gfx
                byte[] stagesel_image_original = File.ReadAllBytes(stageselImageGfxPath);
                byte[] stagesel_image_header = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x00, 0x78);
                byte[] stagesel_image_header_add = new byte[0];
                byte[] stagesel_image_body1 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x78, 0x126E);
                byte[] stagesel_image_body1_add = new byte[0];
                byte[] stagesel_image_body2 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x12E6, 0x6F0);
                byte[] stagesel_image_body2_add = new byte[0];
                byte[] stagesel_image_end = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x19D6, 0xC86);
                byte[] stagesel_image_new_file = new byte[0];
                int image_count = 2;
                int image_count_1 = 0x83;
                for (int st = 0; st < StagesToAdd.Count; st++) {
                    stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[2] { (byte)(0x4C + ("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length), 0xFC });
                    stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, BitConverter.GetBytes(st + image_count), 0, 0, 2);
                    stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[] { 0x09, 0x00, 0x0E, 0x00, 0xB8, 0x00, 0x68, 0x00, 0x00 });
                    stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[1] { (byte)("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length });
                    stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, Encoding.ASCII.GetBytes("stagesel_image_" + StagesToAdd[st].StageName + ".dds"));
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[2] { 0x0C, 0xFC });
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(image_count_1 + ((st + 1) * 2)), 0, 0, 2);
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(st + image_count), 0, 0, 2);
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x0E] { 0x00, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x68, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00 });
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes((image_count_1 + 1) + ((st + 1) * 2)), 0, 0, 2);
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x13] { 0x64, 0x54, 0x3A, 0xC5, 0xF8, 0x20, 0x80, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41 });
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(image_count_1 + ((st + 1) * 2)), 0, 0, 2);
                    stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x1C] { 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0x8A, 0x8B, 0xF0, 0x00, 0x20, 0x15, 0x91, 0x51, 0x7E, 0x17, 0x63, 0xAC, 0x3B, 0x50, 0x41, 0xD9, 0x15, 0x0E, 0xDB, 0xF0, 0x00 });
                    stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0C] { 0xFF, 0x0A, (byte)(("img_s_" + (stage_count - 1 + st).ToString()).Length + 1), 0x00, 0x00, 0x00, 0x69, 0x6D, 0x67, 0x5F, 0x73, 0x5F });
                    stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, Encoding.ASCII.GetBytes((stage_count-1 + st).ToString()));
                    stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0A] { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00, (byte)(image_count_1 + 1 + ((st + 1) * 2)), 0x00, 0x40, 0x00 });
                }
                stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x15, 0, 2);
                stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(stage_count - 1 + StagesToAdd.Count), 0x59, 0, 2);
                stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0x64, 0, 2);
                stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xC63, 0, 2);

                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x06, 0, 2);
                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x82, 0, 2);
                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x8B, 0, 2);
                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x694 + stagesel_image_body2_add.Length), 0xB7, 0, 2);
                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xBB, 0, 2);
                stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(stage_count - 1 + StagesToAdd.Count), 0xBD, 0, 2);

                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header_add);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1_add);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2_add);
                stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_end);
                stagesel_image_new_file = BinaryReader.b_ReplaceBytes(stagesel_image_new_file, BitConverter.GetBytes(stagesel_image_new_file.Length), 0x04);
                File.WriteAllBytes(root_folder + "\\data\\ui\\flash\\OTHER\\stagesel\\stagesel_image.gfx", stagesel_image_new_file);

                //stagesel.gfx
                
                int pageCount = (stage_count - 2 + StagesToAdd.Count) / 36;
                byte[] stagesel_gfx_original = File.ReadAllBytes(stageselGfxPath);
                if (36 * pageCount != stage_count + StagesToAdd.Count)
                    pageCount++;
                stagesel_gfx_original[0x28170] = (byte)pageCount;

                if ((stage_count - 2 + StagesToAdd.Count) < 255)
                    stagesel_gfx_original[0x28176] = (byte)(stage_count - 2 + StagesToAdd.Count);
                else
                    stagesel_gfx_original[0x28176] = (byte)255;
                File.WriteAllBytes(root_folder + "\\data\\ui\\flash\\OTHER\\stagesel\\stagesel.gfx", stagesel_gfx_original);

            }

            KyurutoDialogTextLoader("Saving your character and costume mods!",
                20);
            //save all param files
            characode_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\characode.bin.xfbin");
            duelPlayerParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\duelPlayerParam.xfbin");
            playerSettingParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\playerSettingParam.bin.xfbin");
            skillCustomizeParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\skillCustomizeParam.xfbin");
            spSkillCustomizeParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\spSkillCustomizeParam.xfbin");
            skillIndexSettingParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\skillIndexSettingParam.xfbin");
            supportSkillRecoverySpeedParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\supportSkillRecoverySpeedParam.xfbin");
            privateCamera_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\privateCamera.bin.xfbin");
            characterSelectParam_vanilla.SaveFileAs(param_modmanager_path + "data\\ui\\max\\select\\characterSelectParam.xfbin");
            costumeBreakParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\costumeBreakParam.xfbin");
            costumeBreakColorParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\costumeBreakColorParam.xfbin");
            costumeParam_vanilla.SaveFileAs(param_modmanager_path + "data\\rpg\\param\\costumeParam.bin.xfbin");
            playerIcon_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\player_icon.xfbin");
            cmnparam_vanilla.SaveFileAs(param_modmanager_path + "data\\sound\\cmnparam.xfbin");
            supportActionParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\supportActionParam.xfbin");
            awakeAura_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\awakeAura.xfbin");
            appearanceAnm_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\appearanceAnm.xfbin");
            afterAttachObject_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\afterAttachObject.xfbin");
            playerDoubleEffectParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\playerDoubleEffectParam.xfbin");
            spTypeSupportParam_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\spTypeSupportParam.xfbin");
            
            damageeff_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\damageeff.bin.xfbin");
            effectprm_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\effectprm.bin.xfbin");
            damageprm_vanilla.SaveFileAs(param_modmanager_path + "data\\spc\\damageprm.bin.xfbin");
            if (stageInfoModified) {
                KyurutoDialogTextLoader("Saving your stage mods! That process can take some solid time to be finished. Be patient and take some tea while mod manager working hard to please you :P",
                20);
                stageInfo_vanilla.SaveFileAs(param_modmanager_path + "data\\stage\\StageInfo.bin.xfbin");
            }
            if (messageInfoModified) {
                KyurutoDialogTextLoader("Making localization! That can take some time.",
                20);
                messageInfo_vanilla.SaveFileAs(param_modmanager_path + "data\\");
            }
            File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\specialCondParam.xfbin", specialCondParam_vanilla);
            File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\partnerSlotParam.xfbin", partnerSlotParam_vanilla);
            File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\susanooCondParam.xfbin", susanooCondParam_vanilla);


            KyurutoDialogTextLoader("Removing all trash from root folder and packing everything in CPK archives.",
            20);
            //pack all CPKs
            if (Directory.Exists(@Path.GetFullPath(root_folder + "\\cpk_assets"))){
                if (Directory.EnumerateFiles(@Path.GetFullPath(root_folder + "\\cpk_assets"), "*.*", SearchOption.AllDirectories).Any()) {
                    YaCpkTool.CPK_repack(@Path.GetFullPath(root_folder + "\\cpk_assets"));
                    File.Move(root_folder + "\\cpk_assets.cpk", root_folder + "\\moddingapi\\mods\\base_game\\cpk_assets.cpk");
                    File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\cpk_assets.cpk.info", new byte[4] { 0x20, 0, 0, 0 });
                }
            }
            if (Directory.Exists(@Path.GetFullPath(root_folder + "\\data_win32_modmanager"))){
                if (Directory.EnumerateFiles(@Path.GetFullPath(root_folder + "\\data_win32_modmanager"), "*.*", SearchOption.AllDirectories).Any()) {

                    YaCpkTool.CPK_repack(@Path.GetFullPath(root_folder + "\\data_win32_modmanager"));
                    File.Move(root_folder + "\\data_win32_modmanager.cpk", root_folder + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk");
                    File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk.info", new byte[4] { 0x21, 0, 0, 0 });
                }
            }
            if (Directory.Exists(@Path.GetFullPath(param_modmanager_path))){
                if (Directory.EnumerateFiles(@Path.GetFullPath(param_modmanager_path), "*.*", SearchOption.AllDirectories).Any()) {

                    YaCpkTool.CPK_repack(@Path.GetFullPath(param_modmanager_path));
                    File.Move(param_modmanager_path + ".cpk", root_folder + "\\moddingapi\\mods\\base_game\\param_files.cpk");
                    File.WriteAllBytes(root_folder + "\\moddingapi\\mods\\base_game\\param_files.cpk.info", new byte[4] { 0x22, 0, 0, 0 });
                }
            }
            if (Directory.Exists(root_folder + "\\cpk_assets"))
                Directory.Delete(root_folder + "\\cpk_assets", true);
            if (Directory.Exists(root_folder + "\\data_win32_modmanager"))
                Directory.Delete(root_folder + "\\data_win32_modmanager", true);
            if (Directory.Exists(root_folder + "\\param_files"))
                Directory.Delete(root_folder + "\\param_files", true);
        }

        public void CompileMods() {
            if (Directory.Exists(Properties.Settings.Default.RootGameFolder)) {
                LoadingStatePlay = Visibility.Visible;
                CompileModAsyncProcess(Properties.Settings.Default.RootGameFolder);

            } else
                ModernWpf.MessageBox.Show("Set root folder of game!");
        }


        public void InstallMod(string mod_path = "") {
            try {
                if (mod_path == "") {
                    OpenFileDialog myDialog = new OpenFileDialog();
                    myDialog.Filter = "Naruto Storm Connection Mod (*.nsc)|*.nsc";
                    myDialog.CheckFileExists = true;
                    myDialog.Multiselect = false;
                    if (myDialog.ShowDialog() == true) {
                        mod_path = myDialog.FileName;
                    } else {
                        return;
                    }
                }
                string root_folder = Properties.Settings.Default.RootGameFolder;
                string modmanager_folder = root_folder + "\\modmanager\\";
                if (Directory.Exists(root_folder)) {
                    if (!Directory.Exists(modmanager_folder)) {
                        Directory.CreateDirectory(modmanager_folder);
                    }
                    string InstallMod_folder = modmanager_folder + Path.GetFileNameWithoutExtension(mod_path);
                    if (Directory.Exists(InstallMod_folder)) {
                        Directory.Delete(InstallMod_folder, true);
                    }
                    Directory.CreateDirectory(InstallMod_folder);
                    System.IO.Compression.ZipFile.ExtractToDirectory(mod_path, @InstallMod_folder);
                    RefreshModList();

                } else {
                    ModernWpf.MessageBox.Show("Select Root folder for game.");
                }
            }
            catch(Exception ex) {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show("Something went wrong.. Report issue on GitHub \n\n" + ex.StackTrace + " \n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            
        }

        public void DeleteMod() {
            if (SelectedMod is not null) {
                string mod_path = SelectedMod.ModFolder;
                if (Directory.Exists(mod_path)) {
                    Directory.Delete(mod_path, true);
                    ModManagerList.Remove(SelectedMod);
                }
            }
        }

        public void EnableModIsChecked() {
            if (SelectedMod is not null) {
                string mod_path = SelectedMod.ModFolder;

                var ModInfo = new IniFile(mod_path + "\\mod_config.ini");
                ModInfo.Write("EnableMod", SelectedMod.EnableMod.ToString().ToLower(), "ModManager");

                if (SelectedMod.EnableMod) {
                    KyurutoDialogTextLoader(SelectedMod.ModName +" was enabled!",
                20);
                } else {
                    KyurutoDialogTextLoader(SelectedMod.ModName + " was disabled!",
                20);
                }

                RefreshModList();
            }
        }

        public void CleanGameAssets(bool OpenMessage = true) {
            try {
                if (Directory.Exists(Properties.Settings.Default.RootGameFolder)) {
                    //This function was used for cleaning data_win32 and moddingapi folders
                    MessageBoxResult result = MessageBoxResult.No;
                    if (OpenMessage) {
                        result = (MessageBoxResult)ModernWpf.MessageBox.Show("Are you sure you want to clean your game from mods?", "", MessageBoxButton.YesNo);

                    }
                    if (result == MessageBoxResult.Yes || !OpenMessage) {
                        string appFolder = Directory.GetCurrentDirectory();
                        string rootFolder = Properties.Settings.Default.RootGameFolder + "\\data\\ui\\flash\\OTHER";

                        if (Directory.Exists(Properties.Settings.Default.RootGameFolder + "\\moddingapi"))
                            Directory.Delete(Properties.Settings.Default.RootGameFolder + "\\moddingapi", true);
                        InstallModdingAPI(false);

                        File.Copy(appFolder + "\\ParamFiles\\stagesel.gfx", rootFolder + "\\stagesel\\stagesel.gfx", true);
                        File.Copy(appFolder + "\\ParamFiles\\stagesel_image.gfx", rootFolder + "\\stagesel\\stagesel_image.gfx", true);
                        File.Copy(appFolder + "\\ParamFiles\\charsel.gfx", rootFolder + "\\charsel\\charsel.gfx", true);
                        File.Copy(appFolder + "\\ParamFiles\\charicon_s.gfx", rootFolder + "\\charicon_s\\charicon_s.gfx", true);
                        File.Copy(appFolder + "\\ParamFiles\\nuccMaterial_dx11.nsh", Properties.Settings.Default.RootGameFolder + "\\data\\system\\nuccMaterial_dx11.nsh", true);
                        
                        if (Properties.Settings.Default.EnableMotionBlur)
                            File.Copy(appFolder + "\\ParamFiles\\nuccPostEffect_dx11_S2.nsh", Properties.Settings.Default.RootGameFolder + "\\data\\system\\nuccPostEffect_dx11.nsh", true);
                        else
                            File.Copy(appFolder + "\\ParamFiles\\nuccPostEffect_dx11.nsh", Properties.Settings.Default.RootGameFolder + "\\data\\system\\nuccPostEffect_dx11.nsh", true);

                        if (OpenMessage)
                            ModernWpf.MessageBox.Show("Game was cleaned!");
                    }

                } else {
                    System.Windows.MessageBox.Show("Select root folder of game");
                }
            }
            catch (Exception) {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show("Something went wrong.. Make sure game is closed and you don't have anywhere opened file which mod manager might use during compile process.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void SaveSettings() {
            bool restart = false;
            switch (StretchMode_field) {
                case 0:
                    Properties.Settings.Default.StretchMode = "Fill";
                    break;
                case 1:
                    Properties.Settings.Default.StretchMode = "Uniform";
                    break;
                case 2:
                    Properties.Settings.Default.StretchMode = "None";
                    break;
            }
            Properties.Settings.Default.BackgroundColor1 = BackgroundColor_field;
            Properties.Settings.Default.ButtonColor1 = ButtonColor_field;
            Properties.Settings.Default.TextColor1 = TextColor_field;
            Properties.Settings.Default.RootGameFolder = RootFolderPath_field;
            Properties.Settings.Default.EnableMotionBlur = EnableMotionBlur_field;

            if (File.Exists(BackgroundImagePath_field)) {
                Properties.Settings.Default.BackgroundImagePath = BackgroundImagePath_field;
                restart = true;
            }
            Properties.Settings.Default.Save();
            KyurutoDialogTextLoader("Love it!",
                20);
            if (restart)
                ModernWpf.MessageBox.Show("Some changes requires to restart toolbox!");
        }
        public void ResetSettings() {
            bool restart = false;
            Properties.Settings.Default.StretchMode = "Uniform";
            Properties.Settings.Default.BackgroundColor1 = "#9C000000";
            Properties.Settings.Default.ButtonColor1 = "#9C000000";
            Properties.Settings.Default.TextColor1 = "White";
            if (Properties.Settings.Default.BackgroundImagePath != "UI/background/bg_toolbox_main.png") {
                Properties.Settings.Default.BackgroundImagePath = "UI/background/bg_toolbox_main.png";
                restart = true;

            }
            BackgroundColor_field = Properties.Settings.Default.BackgroundColor1;
            ButtonColor_field = Properties.Settings.Default.ButtonColor1;
            TextColor_field = Properties.Settings.Default.TextColor1;
            Properties.Settings.Default.RootGameFolder = "";
            BackgroundImagePath_field = "UI/background/bg_toolbox_main.png";
            Properties.Settings.Default.Save();
            KyurutoDialogTextLoader("Don't forget to restart mod manager!",
                20);
            if (restart)
                ModernWpf.MessageBox.Show("Some changes requires to restart toolbox!");
        }
        public void SelectImageBackground() {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "PNG Image (*.png)|*.png|JPG Image (*.jpg)|*.jpg|JPEG Image (*.jpeg)|*.jpeg";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true) {
                BackgroundImagePath_field = myDialog.FileName;
            } else {
                return;
            }
        }

        public void VisitModdingGroup() {
            System.Diagnostics.Process.Start(new ProcessStartInfo {
                FileName = Settings.Default.ModdingGroupLink,
                UseShellExecute = true
            });

        }
        public void VisitBoosty() {
            System.Diagnostics.Process.Start(new ProcessStartInfo {
                FileName = "https://boosty.to/theleonx/single-payment/donation/383406?share=target_link",
                UseShellExecute = true
            });

        }

        public void VisitGitHubPage() {
            System.Diagnostics.Process.Start(new ProcessStartInfo {
                FileName = "https://github.com/TheLeonX/NSC-ModManager/releases",
                UseShellExecute = true
            });

        }
        public void SelectRootFolder() {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select root folder of game";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok) {
                Properties.Settings.Default.RootGameFolder = dialog.FileName;
                RootFolderPath_field = dialog.FileName;
                Properties.Settings.Default.Save();
            } else {
                return;
            }
        }

        public void InstallModdingAPI(bool Message = true, string root_path = "") {
            try {
                if (root_path == "") {
                    if (Properties.Settings.Default.RootGameFolder == "") {
                        var dialog = new CommonOpenFileDialog();
                        dialog.IsFolderPicker = true;
                        dialog.Title = "Select root folder of game";
                        CommonFileDialogResult result = dialog.ShowDialog();
                        root_path = dialog.FileName;
                        Properties.Settings.Default.RootGameFolder = dialog.FileName;
                        Properties.Settings.Default.Save();
                    } else {
                        root_path = Properties.Settings.Default.RootGameFolder;
                    }
                }
                if (Directory.Exists(root_path)) {
                    Program.CopyFilesRecursively(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ModdingAPIFiles", root_path);
                    if (Message)
                        ModernWpf.MessageBox.Show("ModdingAPI was installed!");
                } else {
                    return;
                }
            }
            catch (Exception) {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show("Something went wrong.. Make sure game is closed and you don't have anywhere opened file which mod manager might use during compile process.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public void DeleteModdingAPI() {
            try {
                if (Directory.Exists(Properties.Settings.Default.RootGameFolder + "\\moddingapi")) {

                    MessageBoxResult warning = (MessageBoxResult)ModernWpf.MessageBox.Show("Are you sure that you want to delete ModdingAPI? All mods inside of it will be deleted too.", "Do you want to delete it?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (warning == MessageBoxResult.Yes) {

                        Directory.Delete(Properties.Settings.Default.RootGameFolder + "\\moddingapi", true);
                        if (File.Exists(Properties.Settings.Default.RootGameFolder + "\\xinput9_1_0.dll"))
                            File.Delete(Properties.Settings.Default.RootGameFolder + "\\xinput9_1_0.dll");
                        if (File.Exists(Properties.Settings.Default.RootGameFolder + "\\xinput9_1_0_o.dll"))
                            File.Delete(Properties.Settings.Default.RootGameFolder + "\\xinput9_1_0_o.dll");
                        ModernWpf.MessageBox.Show("ModdingAPI was deleted!");
                        KyurutoDialogTextLoader("Mod manager will install ModdingAPI anyway.",
                20);
                    }
                } else {
                    return;
                }
            }
            catch (Exception) {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show("Something went wrong.. Make sure game is closed and you don't have anywhere opened file which mod manager might use during compile process.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        // команда добавления нового объекта
        private RelayCommand addMeouch;
        public RelayCommand AddMeouch {
            get {
                return addMeouch ??
                  (addMeouch = new RelayCommand(obj => {
                      if (MeouchCounter == 10) {
                          MeouchVisibility = Visibility.Visible;
                          KyurutoVisibility = Visibility.Hidden;
                          MeouchEffectAutoPlay = true;
                          KuramaName = "Meouch";
                          MeouchEffectRepeat = new RepeatBehavior(1.0);
                          KuramaDialog = "";
                          KyurutoDialogTextLoader("Meow! You can call me " + KuramaName + ".", 50);
                          MeouchCounter++;

                      } else {
                          MeouchCounter++;
                      }

                  }));
            }
        }
        private RelayCommand _characterManagementCommand;
        public RelayCommand CharacterManagementCommand {
            get {
                return _characterManagementCommand ??
                  (_characterManagementCommand = new RelayCommand(obj => {
                      ToolTabState = 1;

                  }));
            }
        }
        private RelayCommand _optionsCommand;
        public RelayCommand OptionsCommand {
            get {
                return _optionsCommand ??
                  (_optionsCommand = new RelayCommand(obj => {
                      ToolTabState = 2;

                  }));
            }
        }
        private RelayCommand _creditsCommand;
        public RelayCommand CreditsCommand {
            get {
                return _creditsCommand ??
                  (_creditsCommand = new RelayCommand(obj => {
                      MainTabState = 2;
                  }));
            }
        }
        private RelayCommand _mainMenuCommand;
        public RelayCommand MainMenuCommand {
            get {
                return _mainMenuCommand ??
                  (_mainMenuCommand = new RelayCommand(obj => {
                      MainTabState = 1;
                  }));
            }
        }

        private RelayCommand _saveSettingsCommand;
        public RelayCommand SaveSettingsCommand {
            get {
                return _saveSettingsCommand ??
                  (_saveSettingsCommand = new RelayCommand(obj => {
                      SaveSettings();

                  }));
            }
        }
        private RelayCommand _resetSettingsCommand;
        public RelayCommand ResetSettingsCommand {
            get {
                return _resetSettingsCommand ??
                  (_resetSettingsCommand = new RelayCommand(obj => {
                      ResetSettings();

                  }));
            }
        }
        private RelayCommand _selectImageBackgroundCommand;
        public RelayCommand SelectImageBackgroundCommand {
            get {
                return _selectImageBackgroundCommand ??
                  (_selectImageBackgroundCommand = new RelayCommand(obj => {
                      SelectImageBackground();

                  }));
            }
        }
        private RelayCommand _selectRootFolderCommand;
        public RelayCommand SelectRootFolderCommand {
            get {
                return _selectRootFolderCommand ??
                  (_selectRootFolderCommand = new RelayCommand(obj => {
                      SelectRootFolder();

                  }));
            }
        }

        private RelayCommand _installModdingAPICommand;
        public RelayCommand InstallModdingAPICommand {
            get {
                return _installModdingAPICommand ??
                  (_installModdingAPICommand = new RelayCommand(obj => {
                      InstallModdingAPI();

                  }));
            }
        }
        private RelayCommand _deleteModdingAPICommand;
        public RelayCommand DeleteModdingAPICommand {
            get {
                return _deleteModdingAPICommand ??
                  (_deleteModdingAPICommand = new RelayCommand(obj => {
                      DeleteModdingAPI();

                  }));
            }
        }
        private RelayCommand _visitModdingGroupCommand;
        public RelayCommand VisitModdingGroupCommand {
            get {
                return _visitModdingGroupCommand ??
                  (_visitModdingGroupCommand = new RelayCommand(obj => {
                      VisitModdingGroup();

                  }));
            }
        }
        private RelayCommand _boostyCommand;
        public RelayCommand BoostyCommand {
            get {
                return _boostyCommand ??
                  (_boostyCommand = new RelayCommand(obj => {
                      VisitBoosty();

                  }));
            }
        }
        private RelayCommand _installModCommand;
        public RelayCommand InstallModCommand {
            get {
                return _installModCommand ??
                  (_installModCommand = new RelayCommand(obj => {
                      InstallMod();

                  }));
            }
        }
        private RelayCommand _deleteModCommand;
        public RelayCommand DeleteModCommand {
            get {
                return _deleteModCommand ??
                  (_deleteModCommand = new RelayCommand(obj => {
                      DeleteMod();

                  }));
            }
        }
        private RelayCommand _refreshModListCommand;
        public RelayCommand RefreshModListCommand {
            get {
                return _refreshModListCommand ??
                  (_refreshModListCommand = new RelayCommand(obj => {
                      RefreshModList();

                  }));
            }
        }

        private RelayCommand _cleanGameRootCommand;
        public RelayCommand CleanGameRootCommand {
            get {
                return _cleanGameRootCommand ??
                  (_cleanGameRootCommand = new RelayCommand(obj => {
                      CleanGameAssets(true);

                  }));
            }
        }

        private RelayCommand _compileModsCommand;
        public RelayCommand CompileModsCommand {
            get {
                return _compileModsCommand ??
                  (_compileModsCommand = new RelayCommand(obj => {
                      LoadingStatePlay = Visibility.Visible;
                      CompileMods();
                  }));
            }
        }

        private RelayCommand _enableModIsCheckedCommand;
        public RelayCommand EnableModIsCheckedCommand {
            get {
                return _enableModIsCheckedCommand ??
                  (_enableModIsCheckedCommand = new RelayCommand(obj => {
                      EnableModIsChecked();

                  }));
            }
        }
        private RelayCommand _rosterEditorCommand;
        public RelayCommand RosterEditorCommand {
            get {
                return _rosterEditorCommand ??
                  (_rosterEditorCommand = new RelayCommand(obj => {
                      TitleViewModel VM = ((TitleViewModel)this);
                      CharacterRosterEditorView s = new CharacterRosterEditorView(VM);
                      s.Show();

                  }));
            }
        }
        private RelayCommand _visitGitHubPage;
        public RelayCommand VisitGitHubPageCommand {
            get {
                return _visitGitHubPage ??
                  (_visitGitHubPage = new RelayCommand(obj => {
                      VisitGitHubPage();

                  }));
            }
        }
        public async void KyurutoDialogTextLoader(string kuramaDialogUpdate, int timer) {
            try {
                await Task.Run(() => KyurutoDialogTextWork(kuramaDialogUpdate, timer));
            } catch (Exception) {
                //...
            }
        }

        void KyurutoDialogTextWork(string dialog, int timer) {
            KuramaDialog = "";
            for (int i = 0; i < dialog.Length; System.Threading.Thread.Sleep(timer)) {
                if (KuramaDialog.Length != i || (i == 0 && KuramaDialog.Length > 0)) {
                    break;
                }
                KuramaDialog += dialog[i];
                i++;

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


}
