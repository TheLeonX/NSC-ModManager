﻿using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using NSC_ModManager.View;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;

namespace NSC_ModManager.ViewModel
{
    public class RepackHelper
    {
        public static int RunRepackProcess(string inputFolder, string outputCpk)
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YACpkTool.exe");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{inputFolder}\"",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                CreateNoWindow = true,  // Отключает создание нового окна
                UseShellExecute = true, // Включает выполнение через оболочку (важно для работы упаковщика)
                WindowStyle = ProcessWindowStyle.Hidden // Скрывает процесс
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                process.WaitForExit();

                // Проверяем, создался ли CPK-файл перед перемещением
                string generatedCpk = inputFolder + ".cpk";
                if (File.Exists(generatedCpk))
                {
                    File.Move(generatedCpk, outputCpk, true);
                }

                return process.ExitCode;
            }
        }

        public static int RunExtractProcess(string inputCpk, string outputFolder = "")
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YACpkTool.exe");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{inputCpk}\"",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                process.WaitForExit();

                // Убираем ".cpk" для получения папки с извлечёнными данными
                /*string extractedFolder = inputCpk;
                if (extractedFolder.EndsWith(".cpk", StringComparison.OrdinalIgnoreCase))
                {
                    extractedFolder = extractedFolder.Substring(0, extractedFolder.Length - 4);
                }

                if (Directory.Exists(extractedFolder))
                {
                    // Перемещаем содержимое папки в outputFolder
                    foreach (string file in Directory.GetFiles(extractedFolder))
                    {
                        string destFile = Path.Combine(outputFolder, Path.GetFileName(file));
                        File.Move(file, destFile);
                    }
                    foreach (string dir in Directory.GetDirectories(extractedFolder))
                    {
                        string destDir = Path.Combine(outputFolder, Path.GetFileName(dir));
                        Directory.Move(dir, destDir);
                    }
                    // Удаляем пустую папку
                    Directory.Delete(extractedFolder);
                }*/

                return process.ExitCode;
            }
        }

    }


    public class TitleViewModel : INotifyPropertyChanged
    {


        private Visibility _modManagerVisibility;
        public Visibility ModManagerVisibility
        {
            get { return _modManagerVisibility; }
            set
            {
                _modManagerVisibility = value;
                OnPropertyChanged("ModManagerVisibility");
            }
        }
        private Visibility _optionsVisibility;
        public Visibility OptionsVisibility
        {
            get { return _optionsVisibility; }
            set
            {
                _optionsVisibility = value;
                OnPropertyChanged("OptionsVisibility");
            }
        }
        private Visibility _mainWindowVisibility;
        public Visibility MainWindowVisibility
        {
            get { return _mainWindowVisibility; }
            set
            {
                _mainWindowVisibility = value;
                OnPropertyChanged("MainWindowVisibility");
            }
        }
        private Visibility _credits2024Visibility;
        public Visibility Credits2024Visibility
        {
            get { return _credits2024Visibility; }
            set
            {
                _credits2024Visibility = value;
                OnPropertyChanged("Credits2024Visibility");
            }
        }
        private int _toolTabState;
        public int ToolTabState
        {
            get { return _toolTabState; }
            set
            {
                _toolTabState = value;
                if (value > -1)
                {
                    switch (value)
                    {
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
        public int MainTabState
        {
            get { return _mainTabState; }
            set
            {
                _mainTabState = value;
                if (value > -1)
                {
                    switch (value)
                    {
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
        public Visibility LoadingStatePlay
        {
            get { return _loadingStatePlay; }
            set
            {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }

        private int _meouchCounter;
        public int MeouchCounter
        {
            get { return _meouchCounter; }
            set
            {
                _meouchCounter = value;
                OnPropertyChanged("MeouchCounter");
            }
        }
        private int _stretchMode_field;
        public int StretchMode_field
        {
            get { return _stretchMode_field; }
            set
            {
                _stretchMode_field = value;
                OnPropertyChanged("StretchMode_field");
            }
        }

        private string _kuramaDialog;
        public string KuramaDialog
        {
            get { return _kuramaDialog; }
            set
            {
                _kuramaDialog = value;
                OnPropertyChanged("KuramaDialog");
            }
        }
        private string _kuramaName;
        public string KuramaName
        {
            get { return _kuramaName; }
            set
            {
                _kuramaName = value;
                OnPropertyChanged("KuramaName");
            }
        }

        private Visibility _meouchVisibility;
        public Visibility MeouchVisibility
        {
            get { return _meouchVisibility; }
            set
            {
                _meouchVisibility = value;
                OnPropertyChanged("MeouchVisibility");
            }
        }
        private Visibility _kyurutoVisibility;
        public Visibility KyurutoVisibility
        {
            get { return _kyurutoVisibility; }
            set
            {
                _kyurutoVisibility = value;
                OnPropertyChanged("KyurutoVisibility");
            }
        }
        private bool _meouchEffectAutoPlay;
        public bool MeouchEffectAutoPlay
        {
            get { return _meouchEffectAutoPlay; }
            set
            {
                _meouchEffectAutoPlay = value;
                OnPropertyChanged("MeouchEffectAutoPlay");
            }
        }

        private RepeatBehavior _meouchEffectRepeat;
        public RepeatBehavior MeouchEffectRepeat
        {
            get { return _meouchEffectRepeat; }
            set
            {
                _meouchEffectRepeat = value;
                OnPropertyChanged("MeouchEffectRepeat");
            }
        }
        private string _backgroundColor_field;
        public string BackgroundColor_field
        {
            get { return _backgroundColor_field; }
            set
            {
                _backgroundColor_field = value;
                OnPropertyChanged("BackgroundColor_field");
            }
        }
        private string _buttonColor_field;
        public string ButtonColor_field
        {
            get { return _buttonColor_field; }
            set
            {
                _buttonColor_field = value;
                OnPropertyChanged("ButtonColor_field");
            }
        }
        private string _textColor_field;
        public string TextColor_field
        {
            get { return _textColor_field; }
            set
            {
                _textColor_field = value;
                OnPropertyChanged("TextColor_field");
            }
        }
        private string _backgroundImagePath_field;
        public string BackgroundImagePath_field
        {
            get { return _backgroundImagePath_field; }
            set
            {
                _backgroundImagePath_field = value;
                OnPropertyChanged("BackgroundImagePath_field");
            }
        }
        private string _rootFolderPath_field;
        public string RootFolderPath_field
        {
            get { return _rootFolderPath_field; }
            set
            {
                _rootFolderPath_field = value;
                OnPropertyChanged("RootFolderPath_field");
            }
        }
        private string _modName_field;
        public string ModName_field
        {
            get { return _modName_field; }
            set
            {
                _modName_field = value;
                OnPropertyChanged("ModName_field");
            }
        }
        private string _modDescription_field;
        public string ModDescription_field
        {
            get { return _modDescription_field; }
            set
            {
                _modDescription_field = value;
                OnPropertyChanged("ModDescription_field");
            }
        }
        private string _modAuthor_field;
        public string ModAuthor_field
        {
            get { return _modAuthor_field; }
            set
            {
                _modAuthor_field = value;
                OnPropertyChanged("ModAuthor_field");
            }
        }
        private string _modVersion_field;
        public string ModVersion_field
        {
            get { return _modVersion_field; }
            set
            {
                _modVersion_field = value;
                OnPropertyChanged("ModVersion_field");
            }
        }
        private string _modLastUpdate_field;
        public string ModLastUpdate_field
        {
            get { return _modLastUpdate_field; }
            set
            {
                _modLastUpdate_field = value;
                OnPropertyChanged("ModLastUpdate_field");
            }
        }
        private bool _enableMotionBlur_field;
        public bool EnableMotionBlur_field
        {
            get { return _enableMotionBlur_field; }
            set
            {
                _enableMotionBlur_field = value;
                OnPropertyChanged("EnableMotionBlur_field");
            }
        }

        private string _modIconPath;
        public string ModIconPath
        {
            get
            {
                return _modIconPath;
            }
            set
            {
                _modIconPath = value;

                MemoryStream memoryStream = new MemoryStream();


                byte[] fileBytes = new byte[0];

                if (File.Exists(value))
                {
                    fileBytes = File.ReadAllBytes(value);
                } else
                {
                    fileBytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "Resources", "TemplateImages", "template_icon.png"));
                }
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;
                ModIconPreview = BitmapFrame.Create(memoryStream);
                OnPropertyChanged("ModIconPath");
            }
        }
        private BitmapSource _modIconPreview;
        public BitmapSource ModIconPreview
        {
            get { return _modIconPreview; }
            set
            {
                _modIconPreview = value;
                OnPropertyChanged("ModIconPreview");
            }
        }

        private bool _enableModSelected;
        public bool EnableModSelected
        {
            get { return _enableModSelected; }
            set
            {
                _enableModSelected = value;
                OnPropertyChanged("EnableModSelected");
            }
        }

        private ObservableCollection<ModManagerModel> _modManagerList = new ObservableCollection<ModManagerModel>();
        public ObservableCollection<ModManagerModel> ModManagerList
        {
            get
            {
                return _modManagerList;
            }
            set
            {
                _modManagerList = value;
                OnPropertyChanged("ModManagerList");
            }
        }

        private ModManagerModel _selectedMod;
        public ModManagerModel SelectedMod
        {
            get
            {
                return _selectedMod;
            }
            set
            {
                _selectedMod = value;
                if (value != null)
                {
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
        public ObservableCollection<CharacterModModel> CharacterList
        {
            get
            {
                return _characterList;
            }
            set
            {
                _characterList = value;
                OnPropertyChanged("CharacterList");
            }
        }
        private ObservableCollection<StageModModel> _stageList = new ObservableCollection<StageModModel>();
        public ObservableCollection<StageModModel> StageList
        {
            get
            {
                return _stageList;
            }
            set
            {
                _stageList = value;
                OnPropertyChanged("StageList");
            }
        }
        private ObservableCollection<CostumeModModel> _costumeList = new ObservableCollection<CostumeModModel>();
        public ObservableCollection<CostumeModModel> CostumeList
        {
            get
            {
                return _costumeList;
            }
            set
            {
                _costumeList = value;
                OnPropertyChanged("CostumeList");
            }
        }

        private ObservableCollection<TeamUltimateJutsuModModel> _TUJList = new ObservableCollection<TeamUltimateJutsuModModel>();
        public ObservableCollection<TeamUltimateJutsuModModel> TUJList
        {
            get
            {
                return _TUJList;
            }
            set
            {
                _TUJList = value;
                OnPropertyChanged("TUJList");
            }
        }
        private ObservableCollection<SpecialInteractionModModel> _specialInteractionList = new ObservableCollection<SpecialInteractionModModel>();
        public ObservableCollection<SpecialInteractionModModel> SpecialInteractionList
        {
            get
            {
                return _specialInteractionList;
            }
            set
            {
                _specialInteractionList = value;
                OnPropertyChanged("SpecialInteractionList");
            }
        }

        private ObservableCollection<string> _CPKList = new ObservableCollection<string>();
        public ObservableCollection<string> CPKList
        {
            get
            {
                return _CPKList;
            }
            set
            {
                _CPKList = value;
                OnPropertyChanged("CPKList");
            }
        }
        private ObservableCollection<string> _shaderList = new ObservableCollection<string>();
        public ObservableCollection<string> ShaderList
        {
            get
            {
                return _shaderList;
            }
            set
            {
                _shaderList = value;
                OnPropertyChanged("ShaderList");
            }
        }

        public TitleViewModel()
        {
            // Upgrade settings if necessary.
            if (Properties.Settings.Default.MustUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.MustUpgrade = false;
                Properties.Settings.Default.Save();
            }

            bw.DoWork += bw_DoWork_CompileModProcess;
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

            switch (Properties.Settings.Default.StretchMode)
            {
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
            else
            {
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

        private async System.Threading.Tasks.Task CheckGitHubNewerVersion()
        {
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
            if (versionComparison < 0)
            {
                SystemSounds.Beep.Play();
                ModernWpf.MessageBox.Show("There is new version of Mod Manager on GitHub page.");
            }

        }
        public void RefreshModList()
        {
            ModManagerList.Clear();
            CharacterList.Clear();
            StageList.Clear();
            CostumeList.Clear();
            TUJList.Clear();
            CPKList.Clear();
            ShaderList.Clear();
            SpecialInteractionList.Clear();
            string root_folder = Properties.Settings.Default.RootGameFolder;
            string modmanager_folder = Path.Combine(root_folder, "modmanager"); // "\\\\?\\" was used for fixing long paths. Crashes sometimes
            if (Directory.Exists(modmanager_folder))
            {

                DirectoryInfo d = new DirectoryInfo(modmanager_folder); //This function getting info about all files in a path
                FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories);
                Array.Sort(ModConfigList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));


                foreach (FileInfo mod_path in ModConfigList)
                {
                    var ModInfo = new IniFile(mod_path.FullName);

                    ModManagerModel ModEntry = new ModManagerModel()
                    {
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

                    if (ModEntry.EnableMod)
                    {
                        //character mod
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        Array.Sort(CharacterModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo character_path in CharacterModList)
                        {
                            var CharacterInfo = new IniFile(character_path.FullName);
                            CharacterModModel CharacterEntry = new CharacterModModel()
                            {
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
                        Array.Sort(StageModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo stage_path in StageModList)
                        {
                            var StageInfo = new IniFile(stage_path.FullName);
                            StageModModel StageEntry = new StageModModel()
                            {
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
                        Array.Sort(CostumeModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo costume_path in CostumeModList)
                        {
                            var CostumeInfo = new IniFile(costume_path.FullName);
                            CostumeModModel CostumeEntry = new CostumeModModel()
                            {
                                Characode = CostumeInfo.Read("Characode", "ModManager"),
                                BaseCostume = CostumeInfo.Read("BaseModel", "ModManager"),
                                AwakeCostume = CostumeInfo.Read("AwakeModel", "ModManager"),
                                RootPath = Path.GetDirectoryName(costume_path.FullName)
                            };
                            CostumeList.Add(CostumeEntry);
                        }

                        //Team Ultimate Jutsu Mod

                        FileInfo[] TUJModList = mod_dir.GetFiles("TUJ_config.ini", SearchOption.AllDirectories);
                        Array.Sort(TUJModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo tuj_path in TUJModList)
                        {
                            var TUJInfo = new IniFile(tuj_path.FullName);
                            string characodesString = TUJInfo.Read("Characodes", "ModManager");
                            // Convert the split list of strings into ObservableCollection
                            ObservableCollection<string> characodeList = new ObservableCollection<string>(
                                characodesString.Split(',')
                                                .Select(code => code.Trim())
                            );

                            TeamUltimateJutsuModModel TUJEntry = new TeamUltimateJutsuModModel()
                            {
                                Label = TUJInfo.Read("Label", "ModManager"),
                                Name = TUJInfo.Read("Name", "ModManager"),
                                Flag1 = Convert.ToBoolean(TUJInfo.Read("Flag1", "ModManager")),
                                Flag2 = Convert.ToBoolean(TUJInfo.Read("Flag2", "ModManager")),
                                MemberCount = Convert.ToInt32(TUJInfo.Read("MemberCount", "ModManager")),
                                CharacodeList = characodeList,
                                RootPath = Path.GetDirectoryName(tuj_path.FullName)
                            };
                            TUJList.Add(TUJEntry);
                        }

                        //special interaction
                        FileInfo[] SpecialInteractionModList = mod_dir.GetFiles("specialInteraction_config.ini", SearchOption.AllDirectories);
                        Array.Sort(SpecialInteractionModList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo specialInteraction_path in SpecialInteractionModList)
                        {
                            var SpecialInteractionInfo = new IniFile(specialInteraction_path.FullName);
                            string characodesString = SpecialInteractionInfo.Read("TriggerList", "ModManager");
                            // Convert the split list of strings into ObservableCollection
                            ObservableCollection<string> characodeList = new ObservableCollection<string>(
                                characodesString.Split(',')
                                                .Select(code => code.Trim())
                            );

                            SpecialInteractionModModel SpecialInteractionEntry = new SpecialInteractionModModel()
                            {
                                MainCharacode = Path.GetFileName(Path.GetDirectoryName(specialInteraction_path.FullName)),
                                TriggerList = characodeList,
                                RootPath = Path.GetDirectoryName(specialInteraction_path.FullName)
                            };
                            SpecialInteractionList.Add(SpecialInteractionEntry);
                        }

                        //CPKs
                        FileInfo[] CpkListInfo = mod_dir.GetFiles("*.cpk", SearchOption.AllDirectories);
                        Array.Sort(CpkListInfo, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo cpk_path in CpkListInfo)
                        {
                            CPKList.Add(cpk_path.FullName);
                        }
                        //Shaders
                        FileInfo[] ShaderListInfo = mod_dir.GetFiles("*.hlsl", SearchOption.AllDirectories);
                        Array.Sort(ShaderListInfo, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo shader_path in ShaderListInfo)
                        {
                            ShaderList.Add(shader_path.FullName);
                        }
                    }


                }
            } else
            {
                if (!Directory.Exists(root_folder))
                {
                    ModernWpf.MessageBox.Show("Select Root Folder of game");
                } else
                {
                    Directory.CreateDirectory(modmanager_folder);
                    ModernWpf.MessageBox.Show("No mods found.");
                }
            }
        }
        static BackgroundWorker bw = new BackgroundWorker();
        void CompileModAsyncProcess(string root_folder)
        {

            try
            {
                //MessageBox.Show(CharacterList[-1].Characode);
                LoadingStatePlay = Visibility.Visible;
                bw.WorkerReportsProgress = true;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted; // Add completion handler
                if (!bw.IsBusy)
                    bw.RunWorkerAsync(root_folder);
                else
                {
                    MessageBox.Show("Wait till compiling process is finished!");
                }

            } catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingStatePlay = Visibility.Hidden;
            if (e.Error != null)
            {
                HandleError(e.Error);
            }
        }

        void bw_DoWork_CompileModProcess(object sender, DoWorkEventArgs e)
        {
            try
            {
                string root_folder = Convert.ToString(e.Argument);
                CleanGameAssets(false);
                Debug.WriteLine("Starting mod compilation...");
                KyurutoDialogTextLoader("Preparing all files!",
                    20);


                //vanilla files
                string characodePath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "characode.bin.xfbin");
                string duelPlayerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "duelPlayerParam.xfbin");
                string playerSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "playerSettingParam.bin.xfbin");
                string skillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "skillCustomizeParam.xfbin");
                string spSkillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "spSkillCustomizeParam.xfbin");
                string skillIndexSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "skillIndexSettingParam.xfbin");
                string supportSkillRecoverySpeedParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "supportSkillRecoverySpeedParam.xfbin");
                string privateCameraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "privateCamera.bin.xfbin");
                string characterSelectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "characterSelectParam.xfbin");

                string costumeBreakColorParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "costumeBreakColorParam.xfbin");

                string costumeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "costumeParam.bin.xfbin");
                string playerIconPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "player_icon.xfbin");
                string cmnparamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "cmnparam.xfbin");
                string supportActionParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "supportActionParam.xfbin");
                string awakeAuraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "awakeAura.xfbin");
                string appearanceAnmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "appearanceAnm.xfbin");
                string afterAttachObjectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "afterAttachObject.xfbin");
                string playerDoubleEffectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "playerDoubleEffectParam.xfbin");
                string spTypeSupportParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "spTypeSupportParam.xfbin");
                string costumeBreakParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "costumeBreakParam.xfbin");
                string messageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "message");
                string damageeffPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "damageeff.bin.xfbin");
                string effectprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "effectprm.bin.xfbin");
                string damageprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "damageprm.bin.xfbin");
                string stageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "StageInfo.bin.xfbin");
                string nuccMaterialDx11Path = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "nuccMaterial_dx11.nsh");
                string stageselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "stagesel.gfx");
                string stageselImageGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "stagesel_image.gfx");
                string charselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "charsel.gfx");
                string chariconGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "charicon_s.gfx");
                string stage_selectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "select_stage.xfbin");
                string conditionprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "conditionprm.bin.xfbin");

                string specialCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "specialCondParam.xfbin");
                string partnerSlotParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "partnerSlotParam.xfbin");
                string susanooCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "susanooCondParam.xfbin");
                string specialInteractionPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "specialInteractionManager.xfbin");
                string conditionprmManagerPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "conditionprmManager.xfbin");
                string bgmManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "bgmManagerParam.xfbin");

                //TUJ Only
                string pairSpSkillCombinationParam = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "pairSpSkillCombinationParam.xfbin");
                string pairSpSkillManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "mods", "base_game", "pairSpSkillManagerParam.xfbin");

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


                ConditionPrmViewModel conditionprm_vanilla = new ConditionPrmViewModel();
                conditionprm_vanilla.OpenFile(conditionprmPath);

                ConditionManagerViewModel conditionprmManager_vanilla = new ConditionManagerViewModel();
                conditionprmManager_vanilla.OpenFile(conditionprmManagerPath);


                //TUJ Vanilla Files
                PairSpSkillCombinationParamViewModel pairSpSkillComb_vanilla = new PairSpSkillCombinationParamViewModel();
                pairSpSkillComb_vanilla.OpenFile(pairSpSkillCombinationParam);
                byte[] pairManagerParam_vanilla = File.ReadAllBytes(pairSpSkillManagerParamPath);

                SpecialInteractionManagerViewModel specialInteraction_vanilla = new SpecialInteractionManagerViewModel();
                specialInteraction_vanilla.OpenFile(specialInteractionPath);

                if (!File.Exists(specialCondParamPath))
                {
                    throw new FileNotFoundException($"File not found: {specialCondParamPath}");
                }
                if (!File.Exists(partnerSlotParamPath))
                {
                    throw new FileNotFoundException($"File not found: {partnerSlotParamPath}");
                }
                if (!File.Exists(susanooCondParamPath))
                {
                    throw new FileNotFoundException($"File not found: {susanooCondParamPath}");
                }
                if (!File.Exists(specialInteractionPath))
                {
                    throw new FileNotFoundException($"File not found: {specialInteractionPath}");
                }
                byte[] specialCondParam_vanilla = File.ReadAllBytes(specialCondParamPath);
                byte[] partnerSlotParam_vanilla = File.ReadAllBytes(partnerSlotParamPath);
                byte[] susanooCondParam_vanilla = File.ReadAllBytes(susanooCondParamPath);

                int characode_count = characode_vanilla.CharacodeList.Count;
                bool messageInfoModified = false;
                bool stageInfoModified = false;
                List<StageModModel> StagesToAdd = new List<StageModModel>();
                List<string> CharselIconNamesList = new List<string>();
                List<string> CharselLoadedIconsList = new List<string>();
                for (int i = 0; i < playerIcon_vanilla.playerIconList.Count; i++)
                {
                    if (!CharselLoadedIconsList.Contains(playerIcon_vanilla.playerIconList[i].BaseIcon))
                    {
                        CharselLoadedIconsList.Add(playerIcon_vanilla.playerIconList[i].BaseIcon);
                    }
                }

                //Compile Character mods
                foreach (CharacterModModel character_mod in CharacterList)
                {
                    string mod_characode = character_mod.Characode;
                    int mod_characodeID = -1;
                    bool replace_character = false;

                    //Read Characode file and add/find entry
                    foreach (CharacodeEditorModel entry in characode_vanilla.CharacodeList)
                    {
                        if (entry.CharacodeName == mod_characode)
                        {
                            mod_characodeID = entry.CharacodeIndex;
                            replace_character = true;
                            break;
                        }
                    }


                    // Required for adding
                    string duelPlayerParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "duelPlayerParam.xfbin");
                    string conditionprmModPath = Path.Combine(character_mod.RootPath, "data", "spc", "conditionprm.bin.xfbin");
                    string playerSettingParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "playerSettingParam.bin.xfbin");
                    string skillCustomizeParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "skillCustomizeParam.xfbin");
                    string spSkillCustomizeParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "spSkillCustomizeParam.xfbin");
                    string skillIndexSettingParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "skillIndexSettingParam.xfbin");
                    string supportSkillRecoverySpeedParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "supportSkillRecoverySpeedParam.xfbin");
                    string privateCameraModPath = Path.Combine(character_mod.RootPath, "data", "spc", "privateCamera.bin.xfbin");

                    string costumeParamModPath = Path.Combine(character_mod.RootPath, "data", "rpg", "param", "costumeParam.bin.xfbin");
                    string playerIconModPath = Path.Combine(character_mod.RootPath, "data", "spc", "player_icon.xfbin");
                    string cmnparamModPath = Path.Combine(character_mod.RootPath, "data", "sound", "cmnparam.xfbin");
                    string characterSelectParamModPath = Path.Combine(character_mod.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");
                    string supportActionParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "supportActionParam.xfbin");


                    //Not required for adding

                    string costumeBreakColorParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "costumeBreakColorParam.xfbin");
                    string awakeAuraModPath = Path.Combine(character_mod.RootPath, "data", "spc", "awakeAura.xfbin");
                    string appearanceAnmModPath = Path.Combine(character_mod.RootPath, "data", "spc", "appearanceAnm.xfbin");
                    string afterAttachObjectModPath = Path.Combine(character_mod.RootPath, "data", "spc", "afterAttachObject.xfbin");
                    string playerDoubleEffectParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "playerDoubleEffectParam.xfbin");
                    string spTypeSupportParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "spTypeSupportParam.xfbin");
                    string costumeBreakParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "costumeBreakParam.xfbin");
                    string messageInfoModPath = Path.Combine(character_mod.RootPath, "data", "message");
                    string damageeffModPath = Path.Combine(character_mod.RootPath, "data", "spc", "damageeff.bin.xfbin");
                    string effectprmModPath = Path.Combine(character_mod.RootPath, "data", "spc", "effectprm.bin.xfbin");
                    string damageprmModPath = Path.Combine(character_mod.RootPath, "data", "spc", "damageprm.bin.xfbin");

                    string specialCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "specialCondParam.xfbin");
                    string partnerSlotParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "partnerSlotParam.xfbin");
                    string susanooCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "susanooCondParam.xfbin");
                    string conditionprmManagerModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "conditionprmManager.xfbin");

                    //characode file
                    if (!replace_character)
                    {
                        //Check if files exists for partners/leaders in case if we add them instead of replacing
                        if (character_mod.Partner == false)
                        {
                            if (!File.Exists(duelPlayerParamModPath) ||
                            !File.Exists(playerSettingParamModPath) ||
                            !File.Exists(skillCustomizeParamModPath) ||
                            !File.Exists(spSkillCustomizeParamModPath) ||
                            !File.Exists(skillIndexSettingParamModPath) ||
                            !File.Exists(supportSkillRecoverySpeedParamModPath) ||
                            !File.Exists(privateCameraModPath) ||
                            !File.Exists(costumeParamModPath) ||
                            !File.Exists(playerIconModPath) ||
                            !File.Exists(cmnparamModPath) ||
                            !File.Exists(characterSelectParamModPath))
                            {
                                ModernWpf.MessageBox.Show("Error 1");
                                continue;
                            }
                        } else
                        {
                            if (!File.Exists(duelPlayerParamModPath))
                            {
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
                    if (File.Exists(duelPlayerParamModPath))
                    {
                        duelPlayerParam_mod.OpenFile(duelPlayerParamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < duelPlayerParam_vanilla.DuelPlayerParamList.Count; i++)
                            {
                                if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BinName.Contains(mod_characode))
                                {
                                    duelPlayerParam_vanilla.DuelPlayerParamList[i] = (DuelPlayerParamModel)duelPlayerParam_mod.DuelPlayerParamList[0].Clone();
                                    break;
                                }
                            }
                        } else
                        {
                            duelPlayerParam_vanilla.DuelPlayerParamList.Add((DuelPlayerParamModel)duelPlayerParam_mod.DuelPlayerParamList[0].Clone());
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            if (!baseModel.Contains(duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName) && duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName != "")
                                baseModel.Add(duelPlayerParam_vanilla.DuelPlayerParamList[0].BaseCostumes[i].CostumeName);
                            if (!awakeModel.Contains(duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName) && duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName != "")
                                awakeModel.Add(duelPlayerParam_vanilla.DuelPlayerParamList[0].AwakeCostumes[i].CostumeName);
                        }

                    }

                    ConditionPrmViewModel conditionprm_mod = new ConditionPrmViewModel();
                    ConditionManagerViewModel conditionprmManager_mod = new ConditionManagerViewModel();
                    //conditionprm and conditionprmManager
                    if (File.Exists(conditionprmPath) && File.Exists(conditionprmManagerPath))
                    {
                        conditionprm_mod.OpenFile(conditionprmModPath);
                        conditionprmManager_mod.OpenFile(conditionprmManagerModPath);


                        // Loop through all entries in the mod's ConditionList
                        foreach (ConditionPrmModel condition in conditionprm_mod.ConditionList)
                        {
                            // Check if the condition already exists in the vanilla list
                            var existingCondition = conditionprm_vanilla.ConditionList
                                .FirstOrDefault(c => c.ConditionName == condition.ConditionName);

                            if (existingCondition != null)
                            {
                                // Replace the existing condition with the new one
                                int index = conditionprm_vanilla.ConditionList.IndexOf(existingCondition);
                                conditionprm_vanilla.ConditionList[index] = (ConditionPrmModel)condition.Clone();
                            } else
                            {
                                // Add the condition if it does not exist
                                conditionprm_vanilla.ConditionList.Add((ConditionPrmModel)condition.Clone());
                            }
                        }

                        // Loop through all entries in the mod's ConditionManagerList
                        foreach (ConditionManagerModel conditionManager in conditionprmManager_mod.ConditionList)
                        {
                            // Check if the condition manager already exists in the vanilla list
                            var existingConditionManager = conditionprmManager_vanilla.ConditionList
                                .FirstOrDefault(c => c.ConditionName == conditionManager.ConditionName);

                            if (existingConditionManager != null)
                            {
                                // Replace the existing condition manager with the new one
                                int index = conditionprmManager_vanilla.ConditionList.IndexOf(existingConditionManager);
                                conditionprmManager_vanilla.ConditionList[index] = (ConditionManagerModel)conditionManager.Clone();
                            } else
                            {
                                // Add the condition manager if it does not exist
                                conditionprmManager_vanilla.ConditionList.Add((ConditionManagerModel)conditionManager.Clone());
                            }
                        }
                    }


                    //playerSettingParam file
                    List<int> RemovedPresetIds = new List<int>();
                    List<int> AddedPresetIds = new List<int>();
                    List<string> RemovedCSPCodes = new List<string>();
                    bool IsPspModified = false;
                    string charMessageID = "";
                    PlayerSettingParamViewModel playerSettingParam_mod = new PlayerSettingParamViewModel();
                    if (File.Exists(playerSettingParamModPath) && !character_mod.Partner)
                    {
                        playerSettingParam_mod.OpenFile(playerSettingParamModPath);

                        foreach (PlayerSettingParamModel psp_entry in playerSettingParam_mod.PlayerSettingParamList)
                        {
                            string costume_csp_code = psp_entry.PSP_code;
                            int csp_code_index = 0;
                            do
                            {
                                csp_code_index++;
                                costume_csp_code = psp_entry.PSP_code + "_" + csp_code_index.ToString("D6");
                            }
                            while (playerSettingParam_vanilla.PSPCodeExists(costume_csp_code));

                            csp_code_replace.Add(psp_entry.PSP_code, costume_csp_code);
                            psp_entry.PSP_code = costume_csp_code;
                        }








                        if (replace_character)
                        {
                            if (playerSettingParam_mod.PlayerSettingParamList.Count > 0 && File.Exists(characterSelectParamModPath))
                            {
                                //Remove old entries
                                for (int i = 0; i < playerSettingParam_vanilla.PlayerSettingParamList.Count; i++)
                                {
                                    if (playerSettingParam_vanilla.PlayerSettingParamList[i].CharacodeID == mod_characodeID)
                                    {
                                        RemovedPresetIds.Add(playerSettingParam_vanilla.PlayerSettingParamList[i].PSP_ID);
                                        RemovedCSPCodes.Add(playerSettingParam_vanilla.PlayerSettingParamList[i].PSP_code);
                                        playerSettingParam_vanilla.PlayerSettingParamList.RemoveAt(i);
                                        i--;
                                    }
                                }
                                //Add new entries
                                for (int i = 0; i < playerSettingParam_mod.PlayerSettingParamList.Count; i++)
                                {
                                    PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[i].Clone();
                                    if (i == 0)
                                        charMessageID = psp_entry.CharacterNameMessageID;
                                    psp_entry.CharacodeID = mod_characodeID;
                                    psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot() + i + 1;
                                    AddedPresetIds.Add(psp_entry.PSP_ID);
                                    if (psp_entry.ReferenceCharacodeID > characode_count)
                                    {
                                        psp_entry.ReferenceCharacodeID = mod_characodeID;
                                        psp_entry.Unk = 1;
                                    }
                                    if (psp_entry.MainPSP_ID != -1)
                                    {
                                        psp_entry.MainPSP_ID = AddedPresetIds[0];
                                    }
                                    playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                                }
                                IsPspModified = true;
                            }
                        } else
                        {
                            for (int i = 0; i < playerSettingParam_mod.PlayerSettingParamList.Count; i++)
                            {
                                PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[i].Clone();
                                psp_entry.CharacodeID = mod_characodeID;
                                psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot() + i + 1;
                                if (i == 0)
                                    charMessageID = psp_entry.CharacterNameMessageID;
                                AddedPresetIds.Add(playerSettingParam_vanilla.MaxSlot() + i + 1);
                                if (psp_entry.ReferenceCharacodeID > characode_count)
                                {
                                    psp_entry.ReferenceCharacodeID = mod_characodeID;
                                    psp_entry.Unk = 1;
                                }
                                if (psp_entry.MainPSP_ID != -1)
                                {
                                    psp_entry.MainPSP_ID = AddedPresetIds[0];
                                }
                                playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                            }
                        }
                    }

                    //costumeColorBreakParam file
                    CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                    if (File.Exists(costumeBreakColorParamModPath) && !character_mod.Partner)
                    {
                        costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                        if (replace_character)
                        {
                            if (costumeBreakColorParam_mod.CostumeBreakColorParamList.Count > 0)
                            {
                                //Remove old entries
                                for (int i = 0; i < costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Count; i++)
                                {
                                    if (RemovedPresetIds.Contains(costumeBreakColorParam_vanilla.CostumeBreakColorParamList[i].PlayerSettingParamID))
                                    {
                                        costumeBreakColorParam_vanilla.CostumeBreakColorParamList.RemoveAt(i);
                                        i--;
                                    }
                                }
                                //Add new entries
                                for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++)
                                {
                                    CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                                    costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                }
                            }
                        } else
                        {
                            //Add new entries
                            for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++)
                            {
                                CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                                costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                            }
                        }
                    }
                    //costumeParam file
                    CostumeParamViewModel costumeParam_mod = new CostumeParamViewModel();
                    if (File.Exists(costumeParamModPath) && !character_mod.Partner)
                    {
                        costumeParam_mod.OpenFile(costumeParamModPath);
                        if (replace_character)
                        {
                            if (costumeParam_mod.CostumeParamList.Count > 0 && File.Exists(characterSelectParamModPath))
                            {
                                //Remove old entries
                                for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++)
                                {
                                    if (RemovedPresetIds.Contains(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID))
                                    {
                                        costumeParam_vanilla.CostumeParamList.RemoveAt(i);
                                        i--;
                                    }
                                }
                                //Add new entries
                                int old_preset_id = 0;
                                int presetIdIndex = -1;
                                for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++)
                                {
                                    CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                                    if (costume_entry.PlayerSettingParamID != old_preset_id)
                                    {
                                        presetIdIndex++;
                                        old_preset_id = costume_entry.PlayerSettingParamID;
                                    }
                                    costume_entry.PlayerSettingParamID = AddedPresetIds[presetIdIndex];
                                    costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                                    //costume_entry.EntryIndex = costumeParam_vanilla.LastEntry();
                                    costume_entry.EntryIndex = 0; //used for unlocking
                                    costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                                }

                            } else
                            {
                                if (RemovedPresetIds.Count == AddedPresetIds.Count && IsPspModified)
                                {
                                    for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++)
                                    {
                                        if (RemovedPresetIds.Contains(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID))
                                        {
                                            int index = RemovedPresetIds.IndexOf(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID);
                                            costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID = AddedPresetIds[index];
                                        }
                                    }
                                } else
                                {
                                    //Remove old entries
                                    for (int i = 0; i < costumeParam_vanilla.CostumeParamList.Count; i++)
                                    {
                                        if (RemovedPresetIds.Contains(costumeParam_vanilla.CostumeParamList[i].PlayerSettingParamID))
                                        {
                                            costumeParam_vanilla.CostumeParamList.RemoveAt(i);
                                            i--;
                                        }
                                    }
                                    for (int i = 0; i < AddedPresetIds.Count; i++)
                                    {
                                        for (int c = 0; c < 2; c++)
                                        {
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
                        } else
                        {
                            //Add new entries
                            int old_preset_id = 0;
                            int presetIdIndex = -1;
                            for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++)
                            {
                                CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                                if (costume_entry.PlayerSettingParamID != old_preset_id)
                                {
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
                    if (File.Exists(skillCustomizeParamModPath) && !character_mod.Partner)
                    {
                        skillCustomizeParam_mod.OpenFile(skillCustomizeParamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < skillCustomizeParam_vanilla.SkillCustomizeParamList.Count; i++)
                            {
                                if (skillCustomizeParam_vanilla.SkillCustomizeParamList[i].CharacodeID == mod_characodeID)
                                {
                                    skillCustomizeParam_vanilla.SkillCustomizeParamList[i] = skillCustomizeParam_mod.SkillCustomizeParamList[0];
                                    break;
                                }
                            }
                        } else
                        {
                            SkillCustomizeParamModel skillEntry = (SkillCustomizeParamModel)skillCustomizeParam_mod.SkillCustomizeParamList[0].Clone();
                            skillEntry.CharacodeID = mod_characodeID;
                            skillCustomizeParam_vanilla.SkillCustomizeParamList.Add(skillEntry);
                        }
                    }

                    //spSkillCustomizeParam file
                    SpSkillCustomizeParamViewModel spSkillCustomizeParam_mod = new SpSkillCustomizeParamViewModel();
                    if (File.Exists(spSkillCustomizeParamModPath) && !character_mod.Partner)
                    {
                        spSkillCustomizeParam_mod.OpenFile(spSkillCustomizeParamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList.Count; i++)
                            {
                                if (spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList[i].CharacodeID == mod_characodeID)
                                {
                                    spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList[i] = spSkillCustomizeParam_mod.SpSkillCustomizeParamList[0];
                                    break;
                                }
                            }
                        } else
                        {
                            SpSkillCustomizeParamModel spSkillEntry = (SpSkillCustomizeParamModel)spSkillCustomizeParam_mod.SpSkillCustomizeParamList[0].Clone();
                            spSkillEntry.CharacodeID = mod_characodeID;
                            spSkillCustomizeParam_vanilla.SpSkillCustomizeParamList.Add(spSkillEntry);
                        }
                    }

                    //skillIndexSettingParam file
                    SkillIndexSettingParamViewModel skillIndexSettingParam_mod = new SkillIndexSettingParamViewModel();
                    if (File.Exists(skillIndexSettingParamModPath) && !character_mod.Partner)
                    {
                        skillIndexSettingParam_mod.OpenFile(skillIndexSettingParamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < skillIndexSettingParam_vanilla.SkillIndexSettingParamList.Count; i++)
                            {
                                if (skillIndexSettingParam_vanilla.SkillIndexSettingParamList[i].CharacodeID == mod_characodeID)
                                {
                                    skillIndexSettingParam_vanilla.SkillIndexSettingParamList[i] = skillIndexSettingParam_mod.SkillIndexSettingParamList[0];
                                    break;
                                }
                            }
                        } else
                        {
                            SkillIndexSettingParamModel skillIndexEntry = (SkillIndexSettingParamModel)skillIndexSettingParam_mod.SkillIndexSettingParamList[0].Clone();
                            skillIndexEntry.CharacodeID = mod_characodeID;
                            skillIndexSettingParam_vanilla.SkillIndexSettingParamList.Add(skillIndexEntry);
                        }
                    }

                    //supportSkillRecoverySpeedParam file
                    SupportSkillRecoverySpeedParamViewModel SupportSkillRecoverySpeedParam_mod = new SupportSkillRecoverySpeedParamViewModel();
                    if (File.Exists(supportSkillRecoverySpeedParamModPath) && !character_mod.Partner)
                    {
                        SupportSkillRecoverySpeedParam_mod.OpenFile(supportSkillRecoverySpeedParamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList.Count; i++)
                            {
                                if (supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList[i].CharacodeID == mod_characodeID)
                                {
                                    supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList[i] = SupportSkillRecoverySpeedParam_mod.SupportSkillRecoverySpeedParamList[0];
                                    break;
                                }
                            }
                        } else
                        {
                            SupportSkillRecoverySpeedParamModel supportSkillRecoverySpeedParamEntry = (SupportSkillRecoverySpeedParamModel)SupportSkillRecoverySpeedParam_mod.SupportSkillRecoverySpeedParamList[0].Clone();
                            supportSkillRecoverySpeedParamEntry.CharacodeID = mod_characodeID;
                            supportSkillRecoverySpeedParam_vanilla.SupportSkillRecoverySpeedParamList.Add(supportSkillRecoverySpeedParamEntry);
                        }
                    }

                    //privateCamera file
                    PrivateCameraViewModel privateCamera_mod = new PrivateCameraViewModel();
                    if (File.Exists(privateCameraModPath))
                    {
                        privateCamera_mod.OpenFile(privateCameraModPath);
                        if (!character_mod.Partner)
                        {
                            if (replace_character)
                            {
                                for (int i = 0; i < privateCamera_vanilla.PrivateCameraList.Count; i++)
                                {
                                    if (privateCamera_vanilla.PrivateCameraList[i].CharacodeIndex == mod_characodeID)
                                    {
                                        privateCamera_vanilla.PrivateCameraList[i] = privateCamera_mod.PrivateCameraList[0];
                                        break;
                                    }
                                }
                            } else
                            {
                                PrivateCameraModel privateCameraEntry = (PrivateCameraModel)privateCamera_mod.PrivateCameraList[0].Clone();
                                privateCameraEntry.CharacodeIndex = mod_characodeID;
                                privateCamera_vanilla.PrivateCameraList.Add(privateCameraEntry);
                            }
                        } else
                        {
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
                    if (File.Exists(playerIconModPath) && !character_mod.Partner)
                    {
                        playerIcon_mod.OpenFile(playerIconModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < playerIcon_vanilla.playerIconList.Count; i++)
                            {
                                if (playerIcon_vanilla.playerIconList[i].CharacodeID == mod_characodeID)
                                {
                                    playerIcon_vanilla.playerIconList.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                        for (int i = 0; i < playerIcon_mod.playerIconList.Count; i++)
                        {
                            PlayerIconModel playerIconEntry = (PlayerIconModel)playerIcon_mod.playerIconList[i].Clone();
                            playerIconEntry.CharacodeID = mod_characodeID;
                            if (!CharselLoadedIconsList.Contains(playerIconEntry.BaseIcon) && !CharselIconNamesList.Contains(playerIconEntry.BaseIcon))
                            {
                                CharselIconNamesList.Add(playerIconEntry.BaseIcon);
                            }
                            playerIcon_vanilla.playerIconList.Add(playerIconEntry);
                        }
                    }

                    //cmnparam file
                    cmnparamViewModel cmnparam_mod = new cmnparamViewModel();
                    if (File.Exists(cmnparamModPath))
                    {
                        cmnparam_mod.OpenFile(cmnparamModPath);
                        if (replace_character)
                        {
                            for (int i = 0; i < cmnparam_vanilla.PlayerSndList.Count; i++)
                            {
                                if (cmnparam_vanilla.PlayerSndList[i].PlayerCharacode == mod_characode)
                                {
                                    cmnparam_vanilla.PlayerSndList[i] = cmnparam_mod.PlayerSndList[0];
                                    break;
                                }
                            }
                        } else
                        {
                            player_sndModel playerSndEntry = (player_sndModel)cmnparam_mod.PlayerSndList[0].Clone();
                            cmnparam_vanilla.PlayerSndList.Add(playerSndEntry);
                        }
                    }

                    //characterSelectParam file
                    CharacterSelectParamViewModel characterSelectParam_mod = new CharacterSelectParamViewModel();
                    if (File.Exists(characterSelectParamModPath) && !character_mod.Partner)
                    {
                        characterSelectParam_mod.OpenFile(characterSelectParamModPath);

                        foreach (CharacterSelectParamModel csp_entry in characterSelectParam_mod.CharacterSelectParamList)
                        {

                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                            {
                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                            }
                        }




                        int page = -1;
                        int slot = -1;
                        if (replace_character)
                        {
                            for (int i = 0; i < characterSelectParam_vanilla.CharacterSelectParamList.Count; i++)
                            {
                                if (RemovedCSPCodes.Contains(characterSelectParam_vanilla.CharacterSelectParamList[i].CSP_code))
                                {
                                    if (page == -1)
                                    {
                                        page = characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex;
                                        slot = characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex;
                                    }
                                }
                            }
                            for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                            {
                                CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();
                                csp_entry.PageIndex = page;
                                csp_entry.SlotIndex = slot;
                                csp_entry.CostumeIndex = i;
                                characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                            }
                        } else
                        {
                            page = character_mod.Page;
                            slot = character_mod.Slot;
                            if (character_mod.Page == -1)
                            {
                                page = characterSelectParam_vanilla.MaxPage();
                                slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                if (slot == 25)
                                {
                                    page++;
                                    slot = 1;
                                }
                            }
                            for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                            {
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
                    if (File.Exists(supportActionParamModPath) && !character_mod.Partner)
                    {
                        supportActionParam_mod.OpenFile(supportActionParamModPath);
                        for (int i = 0; i < supportActionParam_vanilla.SupportActionParamList.Count; i++)
                        {
                            if (supportActionParam_vanilla.SupportActionParamList[i].CharacodeID == mod_characodeID)
                            {
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
                    if (File.Exists(costumeBreakParamModPath) && !character_mod.Partner)
                    {
                        costumeBreakParam_mod.OpenFile(costumeBreakParamModPath);
                        //Remove old entries
                        for (int i = 0; i < costumeBreakParam_vanilla.CostumeBreakParamList.Count; i++)
                        {
                            if (costumeBreakParam_vanilla.CostumeBreakParamList[i].CharacodeID == mod_characodeID)
                            {
                                costumeBreakParam_vanilla.CostumeBreakParamList.RemoveAt(i);
                                i--;
                            }
                        }
                        //Add new entries
                        for (int i = 0; i < costumeBreakParam_mod.CostumeBreakParamList.Count; i++)
                        {
                            CostumeBreakParamModel costumeColor_entry = (CostumeBreakParamModel)costumeBreakParam_mod.CostumeBreakParamList[i].Clone();
                            costumeColor_entry.CharacodeID = mod_characodeID;
                            costumeBreakParam_vanilla.CostumeBreakParamList.Add(costumeColor_entry);
                        }
                    }

                    //AwakeAura file
                    AwakeAuraViewModel awakeeAura_mod = new AwakeAuraViewModel();
                    if (File.Exists(awakeAuraModPath) && !character_mod.Partner)
                    {
                        awakeeAura_mod.OpenFile(awakeAuraModPath);
                        for (int i = 0; i < awakeAura_vanilla.AwakeAuraList.Count; i++)
                        {
                            if (awakeAura_vanilla.AwakeAuraList[i].Characode == mod_characode)
                            {
                                awakeAura_vanilla.AwakeAuraList.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < awakeeAura_mod.AwakeAuraList.Count; i++)
                        {
                            awakeAura_vanilla.AwakeAuraList.Add((AwakeAuraModel)awakeeAura_mod.AwakeAuraList[i].Clone());
                        }
                    }
                    //AppearanceAnm file
                    AppearanceAnmViewModel appearanceAnm_mod = new AppearanceAnmViewModel();
                    if (File.Exists(appearanceAnmModPath) && !character_mod.Partner)
                    {
                        appearanceAnm_mod.OpenFile(appearanceAnmModPath);
                        for (int i = 0; i < appearanceAnm_vanilla.AppearanceAnmList.Count; i++)
                        {
                            if (appearanceAnm_vanilla.AppearanceAnmList[i].CharacodeID == mod_characodeID)
                            {
                                appearanceAnm_vanilla.AppearanceAnmList.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < appearanceAnm_mod.AppearanceAnmList.Count; i++)
                        {
                            AppearanceAnmModel appearanceAnmEntry = (AppearanceAnmModel)appearanceAnm_mod.AppearanceAnmList[i].Clone();
                            appearanceAnmEntry.CharacodeID = mod_characodeID;
                            appearanceAnm_vanilla.AppearanceAnmList.Add(appearanceAnmEntry);
                        }
                    }
                    //afterAttachObject file
                    AfterAttachObjectViewModel afterAttachObject_mod = new AfterAttachObjectViewModel();
                    if (File.Exists(afterAttachObjectModPath) && !character_mod.Partner)
                    {
                        afterAttachObject_mod.OpenFile(afterAttachObjectModPath);
                        for (int i = 0; i < afterAttachObject_vanilla.AfterAttachObjectList.Count; i++)
                        {
                            if (baseModel.Contains(afterAttachObject_vanilla.AfterAttachObjectList[i].Characode)
                                || awakeModel.Contains(afterAttachObject_vanilla.AfterAttachObjectList[i].Characode)
                                || afterAttachObject_vanilla.AfterAttachObjectList[i].Costume == mod_characode)
                            {
                                afterAttachObject_vanilla.AfterAttachObjectList.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < afterAttachObject_mod.AfterAttachObjectList.Count; i++)
                        {
                            afterAttachObject_vanilla.AfterAttachObjectList.Add((AfterAttachObjectModel)afterAttachObject_mod.AfterAttachObjectList[i].Clone());
                        }
                    }
                    //playerDoubleEffectParam file
                    PlayerDoubleEffectParamViewModel playerDoubleEffectParam_mod = new PlayerDoubleEffectParamViewModel();
                    if (File.Exists(playerDoubleEffectParamModPath) && !character_mod.Partner)
                    {
                        playerDoubleEffectParam_mod.OpenFile(playerDoubleEffectParamModPath);
                        for (int i = 0; i < playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.Count; i++)
                        {
                            if (playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList[i].CharacodeID == mod_characodeID)
                            {
                                playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < playerDoubleEffectParam_mod.PlayerDoubleEffectParamList.Count; i++)
                        {
                            PlayerDoubleEffectParamModel playerDoubleEffectEntry = (PlayerDoubleEffectParamModel)playerDoubleEffectParam_mod.PlayerDoubleEffectParamList[i].Clone();
                            playerDoubleEffectEntry.CharacodeID = mod_characodeID;
                            playerDoubleEffectParam_vanilla.PlayerDoubleEffectParamList.Add(playerDoubleEffectEntry);
                        }
                    }
                    //spTypeSupportParam file
                    SpTypeSupportParamViewModel spTypeSupportParam_mod = new SpTypeSupportParamViewModel();
                    if (File.Exists(spTypeSupportParamModPath) && !character_mod.Partner)
                    {
                        spTypeSupportParam_mod.OpenFile(spTypeSupportParamModPath);
                        for (int i = 0; i < spTypeSupportParam_vanilla.SpTypeSupportParamList.Count; i++)
                        {
                            if (spTypeSupportParam_vanilla.SpTypeSupportParamList[i].CharacodeID == mod_characodeID)
                            {
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
                    if (File.Exists(specialCondParamModPath) && !character_mod.Partner)
                    {
                        specialCondParam_mod = File.ReadAllBytes(specialCondParamModPath);
                        specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                        specialCondParam_vanilla = BinaryReader.b_AddBytes(specialCondParam_vanilla, specialCondParam_mod);
                    }

                    //specialCondParam file
                    byte[] partnerSlotParam_mod = new byte[0];
                    if (File.Exists(partnerSlotParamModPath) && character_mod.Partner)
                    {
                        partnerSlotParam_mod = File.ReadAllBytes(partnerSlotParamModPath);
                        partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                        partnerSlotParam_vanilla = BinaryReader.b_AddBytes(partnerSlotParam_vanilla, partnerSlotParam_mod);
                    }

                    //susanooCondParam file
                    byte[] susanooCondParam_mod = new byte[0];
                    if (File.Exists(susanooCondParamModPath) && !character_mod.Partner)
                    {
                        susanooCondParam_mod = File.ReadAllBytes(susanooCondParamModPath);
                        susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x17);
                        susanooCondParam_vanilla = BinaryReader.b_AddBytes(susanooCondParam_vanilla, susanooCondParam_mod);
                    }

                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    if (Directory.Exists(messageInfoModPath) && !character_mod.Partner)
                    {
                        messageInfo_mod.OpenFiles(messageInfoModPath);
                        for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                        {
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
                            for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                            {
                                messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                            }
                        }
                        messageInfoModified = true;

                    }
                    //damageprm file
                    DamagePrmViewModel damageprm_mod = new DamagePrmViewModel();
                    if (File.Exists(damageprmModPath))
                    {
                        damageprm_mod.OpenFile(damageprmModPath);
                        for (int i = 0; i < damageprm_mod.DamagePrmList.Count; i++)
                        {
                            damageprm_vanilla.DamagePrmList.Add((DamagePrmModel)damageprm_mod.DamagePrmList[i].Clone());
                        }
                    }

                    //prm
                    PRMEditorViewModel prm_mod = new PRMEditorViewModel();

                    DirectoryInfo mod_d = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(character_mod.RootPath)));
                    FileInfo[] characterPrmList = mod_d.GetFiles(mod_characode + "prm.bin.xfbin", SearchOption.AllDirectories);
                    Array.Sort(characterPrmList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));

                    string prm_path = "";
                    foreach (FileInfo prm_file in characterPrmList)
                    {
                        prm_path = prm_file.FullName;
                    }
                    if (File.Exists(prm_path) && (prm_path ?? "") != "")
                    {
                        string new_prm_path = root_folder + "\\param_files\\" + prm_path.Remove(0, prm_path.IndexOf("data\\"));
                        //damageeff
                        DamageEffViewModel damageeff_mod = new DamageEffViewModel();
                        //effectprm
                        EffectPrmViewModel effectprm_mod = new EffectPrmViewModel();
                        if (File.Exists(prm_path) && File.Exists(damageeffModPath))
                        {
                            //This function merges damageEff and effectprm files, and fixing prm files with new damageEff ids
                            damageeff_mod.OpenFile(damageeffModPath);
                            if (damageeff_mod.DamageEffList.Count > 0)
                            {
                                List<int> NewEffectIds = new List<int>();

                                if (File.Exists(effectprmModPath))
                                {
                                    effectprm_mod.OpenFile(effectprmModPath);
                                    //This code adds effectprm entries to vanilla/edited files and saves new and olds effectprm ids
                                    for (int j = 0; j < effectprm_mod.EffectPrmList.Count; j++)
                                    {
                                        NewEffectIds.Add(effectprm_vanilla.MaxEffectID() + 1);
                                        effectprm_mod.EffectPrmList[j].EffectPrmID = effectprm_vanilla.MaxEffectID() + 1;
                                        effectprm_vanilla.EffectPrmList.Add((EffectPrmModel)effectprm_mod.EffectPrmList[j].Clone());
                                    }
                                }

                                List<int> OldHitIds = new List<int>();
                                List<int> NewHitIds = new List<int>();
                                //This code changes all effectprm ids in modded damageEff file
                                for (int c = 0; c < damageeff_mod.DamageEffList.Count; c++)
                                {
                                    damageeff_mod.DamageEffList[c].EffectPrmID = NewEffectIds[c];
                                    damageeff_mod.DamageEffList[c].ExtraEffectPrmID = 0;

                                }
                                //This code adding new entries to vanilla/edited damageEff file and changes damageEff ids
                                for (int c = 0; c < damageeff_mod.DamageEffList.Count; c++)
                                {
                                    int maxValue = damageeff_vanilla.MaxDamageID();
                                    OldHitIds.Add(damageeff_mod.DamageEffList[c].DamageEffID);
                                    NewHitIds.Add(maxValue + 1);

                                    DamageEffModel damageeff_entry = (DamageEffModel)damageeff_mod.DamageEffList[c].Clone();
                                    damageeff_entry.DamageEffID = maxValue + 1;
                                    if (OldHitIds.Contains(damageeff_entry.ExtraDamageEffID))
                                    {
                                        damageeff_entry.ExtraDamageEffID = NewHitIds[OldHitIds.IndexOf(damageeff_entry.ExtraDamageEffID)];
                                    }

                                    damageeff_vanilla.DamageEffList.Add(damageeff_entry);
                                }
                                //This code opening prm file of character mod
                                prm_mod.OpenFile(prm_path);
                                //This function checking each movement section 
                                for (int ver = 0; ver < prm_mod.VerList.Count; ver++)
                                {
                                    for (int pl_anm = 0; pl_anm < prm_mod.VerList[ver].PL_ANM_Sections.Count; pl_anm++)
                                    {
                                        for (int function = 0; function < prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList.Count; function++)
                                        {
                                            int selectedhit = prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList[function].DamageHitEffectID;
                                            if (selectedhit != 0)
                                            {
                                                for (int g = 0; g < OldHitIds.Count; g++)
                                                {
                                                    //This code checking for old damageEff Ids and changing them on new ids
                                                    if (OldHitIds[g] == selectedhit)
                                                    {
                                                        prm_mod.VerList[ver].PL_ANM_Sections[pl_anm].FunctionList[function].DamageHitEffectID = (Int16)NewHitIds[g];
                                                    }

                                                }

                                            }
                                        }
                                    }
                                }

                                //Creates directory
                                if (!Directory.Exists(Path.GetDirectoryName(new_prm_path)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(new_prm_path));
                                }
                                //Saves edited prm file
                                prm_mod.SaveFileAs(new_prm_path);
                            }

                        }
                    }



                }
                //Compile Stage Mods

                foreach (StageModModel stage_mod in StageList)
                {

                    string messageInfoModPath = Path.Combine(stage_mod.RootPath, "data", "message");
                    string stageInfoModPath = Path.Combine(stage_mod.RootPath, "data", "stage", "StageInfo.bin.xfbin");

                    string mod_stagename = stage_mod.StageName;
                    int mod_stageID = -1;
                    int BGM_ID = Convert.ToInt32(stage_mod.BgmID);
                    bool replace_stage = false;

                    //Read StageInfo file and find entry
                    for (int i = 0; i < stageInfo_vanilla.StageInfoList.Count; i++)
                    {
                        if (stageInfo_vanilla.StageInfoList[i].StageName == mod_stagename)
                        {
                            mod_stageID = i;
                            replace_stage = true;
                            break;
                        }
                    }
                    StageInfoViewModel stageInfo_mod = new StageInfoViewModel();
                    if (File.Exists(stageInfoModPath))
                    {
                        stageInfo_mod.OpenFile(stageInfoModPath);
                        stageInfoModified = true;
                        // Assume xmlStageIDs is an ObservableCollection<string> containing stageIDs from the XML.
                            var xmlStageIDs = new ObservableCollection<string>
                                {
                            "STAGE_SI00A", "STAGE_SD30A", "STAGE_SD14A", "STAGE_SD01D", "STAGE_SD01B",
                            "STAGE_SD03B", "STAGE_SD03E", "STAGE_SD03A", "STAGE_SD03D", "STAGE_SD18A",
                            "STAGE_SD04B", "STAGE_SD04C", "STAGE_SD05C", "STAGE_SD05A", "STAGE_SD05D",
                            "STAGE_SD05B", "STAGE_SD31A", "STAGE_SI43A", "STAGE_SD00B", "STAGE_SI01A",
                            "STAGE_SD08A", "STAGE_SD06A", "STAGE_SI02A", "STAGE_SD07A", "STAGE_SD07B",
                            "STAGE_SI06A", "STAGE_SD33A", "STAGE_SD10A", "STAGE_SI09A_NR", "STAGE_SI08A",
                            "STAGE_SD32A", "STAGE_SD16A", "STAGE_SD11A", "STAGE_SD12A", "STAGE_SI10A",
                            "STAGE_SI10B", "STAGE_SD24A", "STAGE_SD13A", "STAGE_SD15A_NOSNOW", "STAGE_SD17A",
                            "STAGE_SD17B", "STAGE_SD22A", "STAGE_SD22B", "STAGE_SD25A", "STAGE_SD19A",
                            "STAGE_SD23A", "STAGE_SD21A", "STAGE_SD26A", "STAGE_SI33A", "STAGE_SI35A",
                            "STAGE_SI42B", "STAGE_SI42A", "STAGE_SI44A", "STAGE_SD60A", "STAGE_SI45A",
                            "STAGE_SI50E", "STAGE_SI51C", "STAGE_SD62B", "STAGE_SD62A", "STAGE_SD70B",
                            "STAGE_SI70A", "STAGE_SI71A", "STAGE_SI80A", "STAGE_SI81A", "STAGE_SI81B",
                            "STAGE_SD51A"
                                                    };

                        if (replace_stage)
                        {
                            // Check if the stage already exists in the XML list.
                            string stageName = stageInfo_mod.StageInfoList[0].StageName;
                            if (!xmlStageIDs.Contains(stageName))
                            {
                                StagesToAdd.Add(stage_mod);
                            }
                            stageInfo_vanilla.StageInfoList[mod_stageID] = (StageInfoModel)stageInfo_mod.StageInfoList[0].Clone();
                        } else
                        {
                            StagesToAdd.Add(stage_mod);
                            stageInfo_vanilla.StageInfoList.Add((StageInfoModel)stageInfo_mod.StageInfoList[0].Clone());
                        }
                    }
                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        messageInfo_mod.OpenFiles(messageInfoModPath);
                        for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                        {
                            for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                            {
                                messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                            }
                        }
                        messageInfoModified = true;

                    }


                }
                //Compile Model mods
                foreach (CostumeModModel costume_mod in CostumeList)
                {
                    string mod_characode = costume_mod.Characode;
                    int mod_characodeID = -1;
                    bool replace_character = false;
                    string main_psp_code = "";
                    int main_psp_id = -1;
                    int costume_index = -1;
                    //Read Characode file and add/find entry
                    foreach (CharacodeEditorModel entry in characode_vanilla.CharacodeList)
                    {
                        if (entry.CharacodeName == mod_characode)
                        {
                            mod_characodeID = entry.CharacodeIndex;
                            replace_character = true;
                            break;
                        }
                    }
                    if (!replace_character)
                        continue;


                    string playerSettingParamModPath = Path.Combine(costume_mod.RootPath, "data", "spc", "playerSettingParam.bin.xfbin");
                    string characterSelectParamModPath = Path.Combine(costume_mod.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");
                    string costumeBreakColorParamModPath = Path.Combine(costume_mod.RootPath, "data", "spc", "costumeBreakColorParam.xfbin");
                    string costumeParamModPath = Path.Combine(costume_mod.RootPath, "data", "rpg", "param", "costumeParam.bin.xfbin");
                    string costumeBreakParamModPath = Path.Combine(costume_mod.RootPath, "data", "spc", "costumeBreakParam.xfbin");
                    string afterAttachObjectModPath = Path.Combine(costume_mod.RootPath, "data", "spc", "afterAttachObject.xfbin");
                    string playerIconModPath = Path.Combine(costume_mod.RootPath, "data", "spc", "player_icon.xfbin");
                    string messageInfoModPath = Path.Combine(costume_mod.RootPath, "data", "message");


                    //check if any costume exist for character
                    foreach (PlayerSettingParamModel psp_entry in playerSettingParam_vanilla.PlayerSettingParamList)
                    {
                        if (psp_entry.CharacodeID == mod_characodeID)
                        {
                            main_psp_code = psp_entry.PSP_code;
                            main_psp_id = psp_entry.PSP_ID;
                            break;
                        }
                    }
                    if (main_psp_code == "")
                        continue;



                    //check if there is free slot for character
                    for (int i = 0; i < duelPlayerParam_vanilla.DuelPlayerParamList.Count; i++)
                    {
                        if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BinName.Contains(mod_characode))
                        {
                            for (int c = 0; c < 20; c++)
                            {
                                if (duelPlayerParam_vanilla.DuelPlayerParamList[i].BaseCostumes[c].CostumeName == "")
                                {
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
                    if (File.Exists(playerSettingParamModPath))
                    {
                        playerSettingParam_mod.OpenFile(playerSettingParamModPath);
                        PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[0].Clone();
                        costume_csp_code = psp_entry.PSP_code;
                        int csp_code_index = 0;
                        do
                        {
                            csp_code_index++;
                            costume_csp_code = psp_entry.PSP_code + "_" + csp_code_index.ToString("D6");
                        }
                        while (playerSettingParam_vanilla.PSPCodeExists(costume_csp_code));
                        psp_entry.PSP_code = costume_csp_code;
                        psp_entry.CharacodeID = mod_characodeID;
                        psp_entry.PSP_ID = playerSettingParam_vanilla.MaxSlot() + 1;
                        charMessageID = psp_entry.CharacterNameMessageID;
                        new_preset_id = playerSettingParam_vanilla.MaxSlot() + 1;
                        psp_entry.MainPSP_ID = main_psp_id;
                        psp_entry.CostumeID = costume_index;
                        playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                    }

                    //costumeParam file
                    CostumeParamViewModel costumeParam_mod = new CostumeParamViewModel();
                    if (File.Exists(costumeParamModPath))
                    {
                        costumeParam_mod.OpenFile(costumeParamModPath);
                        //Add new entries
                        for (int i = 0; i < costumeParam_mod.CostumeParamList.Count; i++)
                        {
                            CostumeParamModel costume_entry = (CostumeParamModel)costumeParam_mod.CostumeParamList[i].Clone();
                            costume_entry.PlayerSettingParamID = new_preset_id;
                            costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                            costume_entry.EntryIndex = 0; //used for unlocking
                            costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                        }
                    }

                    //afterAttachObject file
                    AfterAttachObjectViewModel afterAttachObject_mod = new AfterAttachObjectViewModel();
                    if (File.Exists(afterAttachObjectModPath))
                    {
                        afterAttachObject_mod.OpenFile(afterAttachObjectModPath);
                        for (int i = 0; i < afterAttachObject_vanilla.AfterAttachObjectList.Count; i++)
                        {
                            if (costume_mod.BaseCostume == afterAttachObject_vanilla.AfterAttachObjectList[i].Characode
                                || costume_mod.AwakeCostume == afterAttachObject_vanilla.AfterAttachObjectList[i].Characode
                                || afterAttachObject_vanilla.AfterAttachObjectList[i].Costume == mod_characode)
                            {
                                afterAttachObject_vanilla.AfterAttachObjectList.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < afterAttachObject_mod.AfterAttachObjectList.Count; i++)
                        {
                            afterAttachObject_vanilla.AfterAttachObjectList.Add((AfterAttachObjectModel)afterAttachObject_mod.AfterAttachObjectList[i].Clone());
                        }
                    }
                    //costumeBreakParam file
                    CostumeBreakParamViewModel costumeBreakParam_mod = new CostumeBreakParamViewModel();
                    if (File.Exists(costumeBreakParamModPath))
                    {
                        costumeBreakParam_mod.OpenFile(costumeBreakParamModPath);
                        //Add new entries
                        for (int i = 0; i < costumeBreakParam_mod.CostumeBreakParamList.Count; i++)
                        {
                            CostumeBreakParamModel costumeColor_entry = (CostumeBreakParamModel)costumeBreakParam_mod.CostumeBreakParamList[i].Clone();
                            costumeColor_entry.CharacodeID = mod_characodeID;
                            costumeColor_entry.CostumeID = costume_index;
                            costumeBreakParam_vanilla.CostumeBreakParamList.Add(costumeColor_entry);
                        }
                    }
                    //costumeColorBreakParam file
                    CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                    if (File.Exists(costumeBreakColorParamModPath))
                    {
                        costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                        //Add new entries
                        for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++)
                        {
                            CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                            costumeColor_entry.PlayerSettingParamID = new_preset_id;
                            costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                        }
                    }

                    //playerIcon file
                    PlayerIconViewModel playerIcon_mod = new PlayerIconViewModel();
                    if (File.Exists(playerIconModPath))
                    {
                        playerIcon_mod.OpenFile(playerIconModPath);
                        PlayerIconModel playerIconEntry = (PlayerIconModel)playerIcon_mod.playerIconList[0].Clone();
                        playerIconEntry.CharacodeID = mod_characodeID;
                        playerIconEntry.CostumeID = costume_index;
                        if (!CharselLoadedIconsList.Contains(playerIconEntry.BaseIcon) && !CharselIconNamesList.Contains(playerIconEntry.BaseIcon))
                        {
                            CharselIconNamesList.Add(playerIconEntry.BaseIcon);
                        }
                        playerIcon_vanilla.playerIconList.Add(playerIconEntry);
                    }

                    //characterSelectParam
                    CharacterSelectParamViewModel characterSelectParam_mod = new CharacterSelectParamViewModel();
                    if (File.Exists(characterSelectParamModPath))
                    {
                        characterSelectParam_mod.OpenFile(characterSelectParamModPath);

                        int page = 0;
                        int slot = 1;
                        int costume = 0;

                        for (int i = 0; i < characterSelectParam_vanilla.CharacterSelectParamList.Count; i++)
                        {
                            if (characterSelectParam_vanilla.CharacterSelectParamList[i].CSP_code == main_psp_code)
                            {
                                page = characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex;
                                slot = characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex;
                                break;
                            }
                        }
                        for (int i = 0; i < characterSelectParam_vanilla.CharacterSelectParamList.Count; i++)
                        {
                            if (characterSelectParam_vanilla.CharacterSelectParamList[i].PageIndex == page && characterSelectParam_vanilla.CharacterSelectParamList[i].SlotIndex == slot)
                            {
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
                    if (Directory.Exists(messageInfoModPath))
                    {
                        messageInfo_mod.OpenFiles(messageInfoModPath);
                        for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                        {
                            for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                            {
                                messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                            }
                        }

                        messageInfoModified = true;
                    }

                }
                List<string> skippedLabels = new List<string>();
                //Compile Team Ultimate Jutsu Mods
                foreach (TeamUltimateJutsuModModel tuj_mod in TUJList)
                {
                    string cmnparamModPath = Path.Combine(tuj_mod.RootPath, "data", "sound", "cmnparam.xfbin");
                    string messageInfoModPath = Path.Combine(tuj_mod.RootPath, "data", "message");




                    string mod_tuj = tuj_mod.Label;
                    int mod_tuj_id = -1;
                    bool replace_tuj = false;

                    // ---------------------------------------- Pair Sp Skill Manager Param ---------------------------------------------------------------
                    // Read all bytes from the file.
                    int entryLength = 0x18; // Each entry is 24 bytes.

                    // Check if any entry already has TUJ_label_field as its name.
                    int tuj_entryCount = pairManagerParam_vanilla.Length / entryLength;
                    for (int i = 0; i < tuj_entryCount; i++)
                    {
                        int offset = i * entryLength;
                        byte[] nameBytes = new byte[0x10]; // 16 bytes for the name.
                        Array.Copy(pairManagerParam_vanilla, offset, nameBytes, 0, 0x10);

                        // Convert the 16-byte string (assumed ASCII) and trim null terminators.
                        string entryName = Encoding.ASCII.GetString(nameBytes).TrimEnd('\0');

                        if (entryName.Equals(mod_tuj, StringComparison.Ordinal))
                        {
                            replace_tuj = true;
                            mod_tuj_id = i;
                            break;
                        }
                    }
                    // Create a new collection to hold the matching characode indices.
                    ObservableCollection<int> characodeIndices = new ObservableCollection<int>();
                    bool allFound = true;

                    // Iterate through each characode (string) in the TUJ mod.
                    foreach (string code in tuj_mod.CharacodeList)
                    {
                        // Look up the characode entry in characode_vanilla (which contains CharacodeName and CharacodeIndex).
                        var match = characode_vanilla.CharacodeList
                                        .FirstOrDefault(x => x.CharacodeName.Equals(code, StringComparison.OrdinalIgnoreCase));
                        if (match != null)
                        {
                            // Add the found CharacodeIndex.
                            characodeIndices.Add(match.CharacodeIndex);
                        } else
                        {
                            // If any code isn't found, mark as incomplete and exit the loop.
                            allFound = false;
                            break;
                        }
                    }
                    if (!allFound)
                    {
                        skippedLabels.Add(tuj_mod.Label);
                        continue;
                    }

                    List<int> SkipEntriesList = new List<int> { 55, 56, 58 };
                    if (!replace_tuj)
                    {
                        mod_tuj_id = tuj_entryCount;
                        // Add placeholder entries only if the current count is in SkipEntriesList.
                        byte[] newPairManagerEntry = new byte[entryLength];
                        while (SkipEntriesList.Contains(tuj_entryCount))
                        {
                            newPairManagerEntry = new byte[entryLength];
                            // Replace name with "placeholder"
                            newPairManagerEntry = BinaryReader.b_ReplaceString(newPairManagerEntry, "placeholder", 0x00);
                            // Replace Unlock Value with -1
                            newPairManagerEntry = BinaryReader.b_ReplaceBytes(newPairManagerEntry, BitConverter.GetBytes(-1), 0x10);
                            // Append the placeholder entry
                            pairManagerParam_vanilla = BinaryReader.b_AddBytes(pairManagerParam_vanilla, newPairManagerEntry);

                            // Update the count after appending the entry.
                            tuj_entryCount = pairManagerParam_vanilla.Length / entryLength;
                        }
                        // Now add the new entry with tuj_mod.Label as its name.
                        {
                            newPairManagerEntry = new byte[entryLength];
                            newPairManagerEntry = BinaryReader.b_ReplaceString(newPairManagerEntry, tuj_mod.Label, 0x00);
                            newPairManagerEntry = BinaryReader.b_ReplaceBytes(newPairManagerEntry, BitConverter.GetBytes(-1), 0x10);
                            pairManagerParam_vanilla = BinaryReader.b_AddBytes(pairManagerParam_vanilla, newPairManagerEntry);
                        }
                        // ---------------------------------------- Pair Sp Skill Combination Param ---------------------------------------------------------------

                        int entryPairComb = pairSpSkillComb_vanilla.pairSpSkillList.Count;
                        PairSpSkillCombinationParamModel pairSpSkillCombEntry = new PairSpSkillCombinationParamModel();
                        while (SkipEntriesList.Contains(entryPairComb))
                        {
                            pairSpSkillCombEntry.TUJ_ID = entryPairComb;
                            pairSpSkillCombEntry.CharacodeList = new ObservableCollection<int> { 0 };
                            pairSpSkillCombEntry.Unk1 = 30;
                            pairSpSkillCombEntry.Unk2 = 30;
                            pairSpSkillCombEntry.TUJ_Name = "c_union_000";
                            pairSpSkillCombEntry.Condition1 = true;
                            pairSpSkillCombEntry.Condition2 = false;
                            pairSpSkillComb_vanilla.pairSpSkillList.Add(pairSpSkillCombEntry);
                            entryPairComb = pairSpSkillComb_vanilla.pairSpSkillList.Count;
                            pairSpSkillCombEntry = new PairSpSkillCombinationParamModel();

                        }
                        {
                            pairSpSkillCombEntry.TUJ_ID = entryPairComb;
                            pairSpSkillCombEntry.CharacodeList = new ObservableCollection<int>(characodeIndices);
                            pairSpSkillCombEntry.Unk1 = 30;
                            pairSpSkillCombEntry.Unk2 = 30;
                            pairSpSkillCombEntry.TUJ_Name = tuj_mod.Name;
                            pairSpSkillCombEntry.MemberCount = tuj_mod.MemberCount;
                            pairSpSkillCombEntry.Condition1 = tuj_mod.Flag1;
                            pairSpSkillCombEntry.Condition2 = tuj_mod.Flag2;
                            pairSpSkillComb_vanilla.pairSpSkillList.Add(pairSpSkillCombEntry);
                        }


                        //---------------------------------- Cmn Param ---------------------------------------------------------------------------------------------

                        int entrycmnParam = cmnparam_vanilla.PairSplList.Count;
                        pair_spl_sndModel tuj_cmnparam_entry = new pair_spl_sndModel();
                        while (SkipEntriesList.Contains(entrycmnParam))
                        {
                            tuj_cmnparam_entry.PairSplID = entrycmnParam;
                            tuj_cmnparam_entry.PairSplName1 = "placeholder";
                            cmnparam_vanilla.PairSplList.Add(tuj_cmnparam_entry);
                            tuj_cmnparam_entry = new pair_spl_sndModel();
                            entrycmnParam = cmnparam_vanilla.PairSplList.Count;

                        }
                        {
                            cmnparamViewModel cmnparam_mod = new cmnparamViewModel();
                            cmnparam_mod.OpenFile(cmnparamModPath);
                            pair_spl_sndModel pairSndEntry = (pair_spl_sndModel)cmnparam_mod.PairSplList[0].Clone();
                            pairSndEntry.PairSplID = entrycmnParam;
                            cmnparam_vanilla.PairSplList.Add(pairSndEntry);
                        }



                    } else
                    {
                        // ---------------------------------------- Pair Sp Skill Combination Param ---------------------------------------------------------------

                        PairSpSkillCombinationParamModel pairSpSkillCombEntry = new PairSpSkillCombinationParamModel();
                        pairSpSkillCombEntry.TUJ_ID = mod_tuj_id;
                        pairSpSkillCombEntry.CharacodeList = new ObservableCollection<int>(characodeIndices);
                        pairSpSkillCombEntry.Unk1 = 30;
                        pairSpSkillCombEntry.Unk2 = 30;
                        pairSpSkillCombEntry.TUJ_Name = tuj_mod.Name;
                        pairSpSkillCombEntry.MemberCount = tuj_mod.MemberCount;
                        pairSpSkillCombEntry.Condition1 = tuj_mod.Flag1;
                        pairSpSkillCombEntry.Condition2 = tuj_mod.Flag2;

                        // Find the existing entry with matching TUJ_ID
                        PairSpSkillCombinationParamModel existingPairSpCombEntry = pairSpSkillComb_vanilla.pairSpSkillList.FirstOrDefault(entry => entry.TUJ_ID == mod_tuj_id);
                        int tuj_index = pairSpSkillComb_vanilla.pairSpSkillList.IndexOf(existingPairSpCombEntry);
                        pairSpSkillComb_vanilla.pairSpSkillList[tuj_index] = pairSpSkillCombEntry;

                        //---------------------------------- Cmn Param ---------------------------------------------------------------------------------------------
                        if (File.Exists(cmnparamModPath))
                        {
                            cmnparamViewModel cmnparam_mod = new cmnparamViewModel();
                            cmnparam_mod.OpenFile(cmnparamModPath);
                            pair_spl_sndModel pairSndEntry = (pair_spl_sndModel)cmnparam_mod.PairSplList[0].Clone();
                            pairSndEntry.PairSplID = mod_tuj_id;

                            pair_spl_sndModel existingCmnParmaEntry = cmnparam_vanilla.PairSplList.FirstOrDefault(entry => entry.PairSplID == mod_tuj_id);
                            int tuj_cmnparam_index = cmnparam_vanilla.PairSplList.IndexOf(existingCmnParmaEntry);
                            cmnparam_vanilla.PairSplList[tuj_cmnparam_index] = pairSndEntry;
                        }

                    }
                    //---------------------------------- MessageInfo Files ---------------------------------------------------------------------------------------------
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        messageInfo_mod.OpenFiles(messageInfoModPath);
                        for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                        {
                            for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                            {
                                messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                            }
                        }

                        messageInfoModified = true;
                    }

                }

                //Compile Special Interaction
                foreach (SpecialInteractionModModel specialInteractionEntry in SpecialInteractionList)
                {
                    // Find the main character entry in characode_vanilla (case-insensitive comparison).
                    CharacodeEditorModel mainEntry = characode_vanilla.CharacodeList.FirstOrDefault(c =>
                        c.CharacodeName.Equals(specialInteractionEntry.MainCharacode, StringComparison.OrdinalIgnoreCase));
                    if (mainEntry == null)
                    {
                        MessageBox.Show($"Main character '{specialInteractionEntry.MainCharacode}' not found in characode_vanilla.");
                        continue;
                    }
                    int mainCharIndex = mainEntry.CharacodeIndex;

                    // Create a new vanilla entry.
                    SpecialInteractionManagerModel newEntry = new SpecialInteractionManagerModel();
                    newEntry.MainCharacodeID = mainCharIndex;
                    newEntry.TriggerList = new ObservableCollection<int>();

                    // Convert each trigger name to its corresponding index.
                    foreach (string triggerName in specialInteractionEntry.TriggerList)
                    {
                        var triggerEntry = characode_vanilla.CharacodeList.FirstOrDefault(c =>
                            c.CharacodeName.Equals(triggerName, StringComparison.OrdinalIgnoreCase));
                        if (triggerEntry == null)
                        {
                            MessageBox.Show($"Trigger character '{triggerName}' not found in characode_vanilla.");
                            continue;
                        }
                        newEntry.TriggerList.Add(triggerEntry.CharacodeIndex);
                    }

                    // Check if an entry for this main character already exists.
                    var existingEntry = specialInteraction_vanilla.SpecialInteractionList
                                          .FirstOrDefault(e => e.MainCharacodeID == mainCharIndex);
                    if (existingEntry != null)
                    {
                        // Remove the existing vanilla entry.
                        specialInteraction_vanilla.SpecialInteractionList.Remove(existingEntry);
                    }
                    if (newEntry.TriggerList.Count > 0)
                        // Add the new entry to the vanilla list.
                        specialInteraction_vanilla.SpecialInteractionList.Add(newEntry);
                }

                string param_modmanager_path = Path.Combine(root_folder, "param_files") + Path.DirectorySeparatorChar;
                byte[] nuccMaterialFile = File.ReadAllBytes(nuccMaterialDx11Path); // nuccMaterialDx11Path should already be set with Path.Combine

                string chariconDirectory = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "charicon_s");
                if (!Directory.Exists(chariconDirectory))
                    Directory.CreateDirectory(chariconDirectory);
                foreach (ModManagerModel mod in ModManagerList)
                {

                    if (mod.EnableMod)
                    {
                        DirectoryInfo mod_d = new DirectoryInfo(mod.ModFolder);
                        //save shaders
                        FileInfo[] shaderList = mod_d.GetFiles("*.hlsl", SearchOption.AllDirectories);
                        Array.Sort(shaderList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));
                        int ShaderCount = BinaryReader.b_ReadInt16(nuccMaterialFile, 0x0E);
                        List<string> UsedShaders = new List<string>();
                        foreach (FileInfo shader in shaderList)
                        {
                            byte[] shader_data = File.ReadAllBytes(shader.FullName);
                            string shader_name = BitConverter.ToString(BinaryReader.b_ReadByteArray(shader_data, 0, 4));
                            if (!UsedShaders.Contains(shader_name))
                            {
                                nuccMaterialFile = BinaryReader.b_AddBytes(nuccMaterialFile, shader_data);
                                ShaderCount++;
                                UsedShaders.Add(shader_name); //Adding name of shader in list of used shaders
                            }
                        }
                        nuccMaterialFile = BinaryReader.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes((short)ShaderCount), 0x0E, 0); //Replacing byte of shader's count
                        nuccMaterialFile = BinaryReader.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes(nuccMaterialFile.Length), 0x04, 0); //Replacing size bytes of nuccMaterial_dx11 file

                        FileInfo[] cpkList = mod_d.GetFiles("*.cpk", SearchOption.AllDirectories);

                        Array.Sort(cpkList, (x, y) =>
                            StringComparer.OrdinalIgnoreCase.Compare(
                            Path.GetFileName(x.DirectoryName),
                            Path.GetFileName(y.DirectoryName)));
                        foreach (FileInfo cpk in cpkList)
                        {


                            RepackHelper.RunExtractProcess(Path.GetFullPath(cpk.FullName));
                            string fileName = Path.GetFileNameWithoutExtension(cpk.FullName);
                            string sourcePath = Path.Combine(Path.GetDirectoryName(cpk.FullName), fileName);
                            string destinationPath = Path.Combine(root_folder, "cpk_assets");

                            Program.CopyFilesRecursively(sourcePath, destinationPath);

                            if (Directory.Exists(sourcePath))
                                Directory.Delete(sourcePath, true);
                        }

                        

                        // Copy data_win32 files
                        string dataWin32ModManagerPath = Path.Combine(root_folder, "data_win32_modmanager");
                        if (!Directory.Exists(dataWin32ModManagerPath))
                        {
                            Directory.CreateDirectory(dataWin32ModManagerPath);
                        }

                        string modResourcesPath = Path.Combine(mod.ModFolder, "Resources", "Files");
                        if (Directory.Exists(modResourcesPath))
                            Program.CopyFilesRecursivelyModManager(modResourcesPath, dataWin32ModManagerPath);


                    }
                }
                // Write nuccMaterial file
                string nuccMaterialPath = Path.Combine(root_folder, "data", "system", "nuccMaterial_dx11.nsh");
                File.WriteAllBytes(nuccMaterialPath, nuccMaterialFile);

                // Update charsel.gfx
                byte[] charsel_gfx = File.ReadAllBytes(charselGfxPath);
                int charsel_offset_2 = 0x419EF; // 1 + count of pages
                charsel_gfx[charsel_offset_2] = (byte)(1 + characterSelectParam_vanilla.MaxPage());
                string charsel_updated_path = Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "ui", "flash", "OTHER", "charsel", "charsel.gfx");
                File.WriteAllBytes(charsel_updated_path, charsel_gfx);

                // Process Default Icons
                DirectoryInfo default_icons = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "DefaultIcons"));
                FileInfo[] DefaultIconList = default_icons.GetFiles("*.xfbin", SearchOption.AllDirectories);
                Array.Sort(DefaultIconList, (x, y) =>
                    StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));

                foreach (FileInfo icon in DefaultIconList)
                {
                    string destIconPath = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "charicon_s",
                                            Path.GetFileNameWithoutExtension(icon.FullName) + ".xfbin");
                    File.Copy(icon.FullName, destIconPath, true);
                    CharselIconNamesList.Add(Path.GetFileNameWithoutExtension(icon.FullName).Replace("_charicon_s", ""));
                }

                // Validate icon file existence and remove missing ones from the list
                for (int i = 0; i < CharselIconNamesList.Count; i++)
                {
                    string iconName = CharselIconNamesList[i] + "_charicon_s.xfbin";
                    string path1 = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "charicon_s", iconName);
                    string path2 = Path.Combine(root_folder, "data_win32_modmanager", "data", "ui", "flash", "OTHER", "charicon_s", iconName);
                    string path3 = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "DefaultIcons", iconName);

                    if (!File.Exists(path1) && !File.Exists(path2) && !File.Exists(path3))
                    {
                        CharselIconNamesList.RemoveAt(i);
                        i--;
                    }
                }

                // Update charicon_s.gfx
                byte[] charicon_s_filebytes = File.ReadAllBytes(chariconGfxPath);
                string charicon_s_updated_path = Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "ui", "flash", "OTHER", "charicon_s", "charicon_s.gfx");

                byte[] charicon_s_header = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0, 0xCB);
                byte[] charicon_s_body1 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0xCB, 0x4580);
                byte[] charicon_s_body2 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x464B, 0x120A);
                byte[] charicon_s_end = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x5855, 0x1502B); //0x08,0x15,0x7D,0x14F7D - change counts!
                byte[] charicon_s_newFile = new byte[0];
                int icon_count = 0x1CE;
                int icon_count2 = 0xE3;
                int external_image_count = 5;
                for (int i = 0; i < CharselIconNamesList.Count; i++)
                {
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


                // Create directories using Path.Combine
                Directory.CreateDirectory(param_modmanager_path);
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "spc"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "rpg", "param"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "ui", "max", "select"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "stage"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "sound"));

                string bgmManagerParamModPath = Path.Combine(root_folder, "moddingapi", "mods", "base_game", "bgmManagerParam.xfbin");
                if (stageInfoModified)
                {
                    // select_stage
                    int stage_count = 67;
                    byte[] stageSel_file = File.ReadAllBytes(stage_selectPath);
                    byte[] stagesel_header = BinaryReader.b_ReadByteArray(stageSel_file, 0, 0x13C);
                    byte[] stagesel_body = BinaryReader.b_ReadByteArray(stageSel_file, 0x13C, 0x1298);
                    byte[] stagesel_end = BinaryReader.b_ReadByteArray(stageSel_file, 0x13DE, 0x14);
                    byte[] stagesel_xml_add = new byte[0];
                    byte[] stagesel_new_file = new byte[0];
                    // BGMs
                    byte[] BGM_vanilla = new byte[0];
                    if (File.Exists(bgmManagerParamPath))
                    {
                        BGM_vanilla = File.ReadAllBytes(bgmManagerParamPath);
                    }
                    for (int st = 0; st < StagesToAdd.Count; st++)
                    {
                        byte[] BGM_entry = new byte[0x68];
                        BGM_entry = BinaryReader.b_ReplaceString(BGM_entry, StagesToAdd[st].StageName, 0);
                        BGM_entry = BinaryReader.b_ReplaceBytes(BGM_entry, BitConverter.GetBytes(StagesToAdd[st].BgmID), 0x60);
                        BGM_entry = BinaryReader.b_ReplaceBytes(BGM_entry, BitConverter.GetBytes(-1), 0x64);
                        BGM_vanilla = BinaryReader.b_AddBytes(BGM_vanilla, BGM_entry);


                        //if (st < Program.StageBGMSlots.Length)
                        //{
                        //    byte[] stageBGM_slot = new byte[0];
                        //    stageBGM_slot = BinaryReader.b_AddBytes(stageBGM_slot, BinaryReader.crc32(StagesToAdd[st].StageName));
                        //    stageBGM_slot = BinaryReader.b_AddBytes(stageBGM_slot, BitConverter.GetBytes(StagesToAdd[st].BgmID));
                        //    string bgmFile = Path.Combine(root_folder, "moddingapi", "mods", "base_game",
                        //        Program.StageBGMSlots[st].ToString("X") + ".ns4p");
                        //    File.WriteAllBytes(bgmFile, stageBGM_slot);



                        //}

                        byte[] xml_line = new byte[0x0E]
                        {
            0x0D, 0x0A, 0x09, 0x3C, 0x73, 0x74, 0x61, 0x67, 0x65, 0x20, 0x69, 0x64, 0x3D, 0x22
                        };
                        xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes((stage_count + st).ToString()));
                        xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x0A]
                            { 0x22, 0x20, 0x6E, 0x61, 0x6D, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                        xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(StagesToAdd[st].MessageID));
                        xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x0B]
                            { 0x22, 0x20, 0x73, 0x74, 0x61, 0x67, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                        xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(StagesToAdd[st].StageName));
                        int hellID = StagesToAdd[st].Hell ? 1 : 0;
                        xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x08]
                            { 0x22, 0x20, 0x68, 0x65, 0x6C, 0x6C, 0x3D, 0x22 });
                        xml_line = BinaryReader.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(hellID.ToString()));
                        xml_line = BinaryReader.b_AddBytes(xml_line, new byte[0x03] { 0x22, 0x2F, 0x3E });
                        stagesel_xml_add = BinaryReader.b_AddBytes(stagesel_xml_add, xml_line);

                        // Stage preview image
                        byte[] st_img_body = new byte[0];
                        string stagePreviewPath = Path.Combine(StagesToAdd[st].RootPath, "stage_preview.png");
                        if (File.Exists(stagePreviewPath))
                        {
                            st_img_body = File.ReadAllBytes(stagePreviewPath);
                        } else
                        {
                            string defaultStageTex = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_tex.png");
                            st_img_body = File.ReadAllBytes(defaultStageTex);
                        }
                        string xfbinStageImagePath = Path.Combine("Z:/char/x/stagesel/tex/tex_l",
                            "st_img_l_" + (stage_count - 1 + st).ToString() + ".png");
                        byte[] st_img_new_file = BinaryReader.MakeXfbinBinary(xfbinStageImagePath,
                            "st_img_l_" + (stage_count - 1 + st).ToString(), st_img_body);

                        string stageselTexLDir = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "stagesel", "tex_l");
                        if (!Directory.Exists(stageselTexLDir))
                        {
                            Directory.CreateDirectory(stageselTexLDir);
                        }
                        string outputStageImagePath = Path.Combine(stageselTexLDir, "st_img_l_" + (stage_count - 1 + st).ToString() + ".xfbin");
                        File.WriteAllBytes(outputStageImagePath, st_img_new_file);

                        // Stage icon image
                        string stageIconPath = Path.Combine(StagesToAdd[st].RootPath, "stage_icon.dds");
                        if (File.Exists(stageIconPath))
                        {
                            st_img_body = File.ReadAllBytes(stageIconPath);
                        } else
                        {
                            string defaultStageIcon = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_icon.dds");
                            st_img_body = File.ReadAllBytes(defaultStageIcon);
                        }
                        string xfbinStageIconPath = Path.Combine("D:/usr/flash/char/x/stagesel", StagesToAdd[st].StageName + ".dds");
                        st_img_new_file = BinaryReader.MakeXfbinBinary(xfbinStageIconPath,
                            "stagesel_image_" + StagesToAdd[st].StageName, st_img_body);

                        string stageselDir = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "stagesel");
                        if (!Directory.Exists(stageselDir))
                        {
                            Directory.CreateDirectory(stageselDir);
                        }
                        string outputStageIconPath = Path.Combine(stageselDir, "stagesel_image_" + StagesToAdd[st].StageName + ".xfbin");
                        File.WriteAllBytes(outputStageIconPath, st_img_new_file);
                    }

                    stagesel_xml_add = BinaryReader.b_AddBytes(stagesel_xml_add, new byte[0x0A]
                        { 0x0D, 0x0A, 0x3C, 0x2F, 0x5F, 0x72, 0x6F, 0x6F, 0x74, 0x3E });
                    stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_header);
                    stagesel_new_file = BinaryReader.b_ReplaceBytes(stagesel_new_file,
                        BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length), 0x138, 1);
                    stagesel_new_file = BinaryReader.b_ReplaceBytes(stagesel_new_file,
                        BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length + 4), 0x12C, 1);
                    stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_body);
                    stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_xml_add);
                    stagesel_new_file = BinaryReader.b_AddBytes(stagesel_new_file, stagesel_end);

                    string selectStageDir = Path.Combine(root_folder, "param_files", "data", "ui", "max", "select");
                    if (!Directory.Exists(selectStageDir))
                        Directory.CreateDirectory(selectStageDir);
                    string selectStagePath = Path.Combine(selectStageDir, "select_stage.xfbin");
                    File.WriteAllBytes(selectStagePath, stagesel_new_file);

                    // stagesel_image.gfx
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
                    for (int st = 0; st < StagesToAdd.Count; st++)
                    {
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[2]
                            { (byte)(0x4C + ("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length), 0xFC });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add,
                            BitConverter.GetBytes(st + image_count), 0, 0, 2);
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add,
                            new byte[] { 0x09, 0x00, 0x0E, 0x00, 0xB8, 0x00, 0x68, 0x00, 0x00 });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add,
                            new byte[1] { (byte)("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add,
                            Encoding.ASCII.GetBytes("stagesel_image_" + StagesToAdd[st].StageName + ".dds"));
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[2] { 0x0C, 0xFC });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add,
                            BitConverter.GetBytes(image_count_1 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add,
                            BitConverter.GetBytes(st + image_count), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x0E]
                            { 0x00, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x68, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00 });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add,
                            BitConverter.GetBytes((image_count_1 + 1) + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x13]
                            { 0x64, 0x54, 0x3A, 0xC5, 0xF8, 0x20, 0x80, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41 });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add,
                            BitConverter.GetBytes(image_count_1 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x1C]
                            { 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0x8A, 0x8B, 0xF0, 0x00, 0x20, 0x15, 0x91, 0x51, 0x7E, 0x17, 0x63, 0xAC, 0x3B, 0x50, 0x41, 0xD9, 0x15, 0x0E, 0xDB, 0xF0, 0x00 });
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0C]
                            { 0xFF, 0x0A, (byte)(("img_s_" + (stage_count - 1 + st).ToString()).Length + 1), 0x00, 0x00, 0x00, 0x69, 0x6D, 0x67, 0x5F, 0x73, 0x5F });
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add,
                            Encoding.ASCII.GetBytes((stage_count - 1 + st).ToString()));
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0A]
                            { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00, (byte)(image_count_1 + 1 + ((st + 1) * 2)), 0x00, 0x40, 0x00 });
                    }
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                        BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x15, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                        BitConverter.GetBytes(stage_count - 1 + StagesToAdd.Count), 0x59, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                        BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0x64, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                        BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xC63, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x06, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x82, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x8B, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(0x694 + stagesel_image_body2_add.Length), 0xB7, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xBB, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(stage_count - 1 + StagesToAdd.Count), 0xBD, 0, 2);

                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_end);
                    stagesel_image_new_file = BinaryReader.b_ReplaceBytes(stagesel_image_new_file,
                        BitConverter.GetBytes(stagesel_image_new_file.Length), 0x04);
                    string stageselImageOutputPath = Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "stagesel", "stagesel_image.gfx");
                    File.WriteAllBytes(stageselImageOutputPath, stagesel_image_new_file);

                    // stagesel.gfx
                    int pageCount = (stage_count - 2 + StagesToAdd.Count) / 36;
                    byte[] stagesel_gfx_original = File.ReadAllBytes(stageselGfxPath);
                    if (36 * pageCount != stage_count + StagesToAdd.Count)
                        pageCount++;
                    stagesel_gfx_original[0x291C7] = (byte)pageCount;

                    stagesel_gfx_original[0x291CD] = (stage_count - 2 + StagesToAdd.Count) < 255
                        ? (byte)(stage_count - 2 + StagesToAdd.Count)
                        : (byte)255;
                    string stageselGfxOutputPath = Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "stagesel", "stagesel.gfx");
                    File.WriteAllBytes(stageselGfxOutputPath, stagesel_gfx_original);
                    File.WriteAllBytes(bgmManagerParamModPath, BGM_vanilla);


                }


                KyurutoDialogTextLoader("Saving your character and costume mods!",
                    20);
                //save all param files
                // Save vanilla parameter files to the modmanager folder
                characode_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "characode.bin.xfbin"));
                duelPlayerParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "duelPlayerParam.xfbin"));
                playerSettingParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "playerSettingParam.bin.xfbin"));
                skillCustomizeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "skillCustomizeParam.xfbin"));
                spSkillCustomizeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "spSkillCustomizeParam.xfbin"));
                skillIndexSettingParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "skillIndexSettingParam.xfbin"));
                supportSkillRecoverySpeedParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "supportSkillRecoverySpeedParam.xfbin"));
                privateCamera_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "privateCamera.bin.xfbin"));
                characterSelectParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "ui", "max", "select", "characterSelectParam.xfbin"));
                costumeBreakParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "costumeBreakParam.xfbin"));
                costumeBreakColorParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "costumeBreakColorParam.xfbin"));
                costumeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "rpg", "param", "costumeParam.bin.xfbin"));
                playerIcon_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "player_icon.xfbin"));
                cmnparam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "sound", "cmnparam.xfbin"));
                supportActionParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "supportActionParam.xfbin"));
                awakeAura_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "awakeAura.xfbin"));
                appearanceAnm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "appearanceAnm.xfbin"));
                afterAttachObject_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "afterAttachObject.xfbin"));
                playerDoubleEffectParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "playerDoubleEffectParam.xfbin"));
                spTypeSupportParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "spTypeSupportParam.xfbin"));
                pairSpSkillComb_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "pairSpSkillCombinationParam.xfbin"));
                conditionprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "conditionprm.bin.xfbin"));

                damageeff_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "damageeff.bin.xfbin"));
                effectprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "effectprm.bin.xfbin"));
                damageprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "damageprm.bin.xfbin"));

                if (stageInfoModified)
                {
                    KyurutoDialogTextLoader("Saving your stage mods!", 20);
                    stageInfo_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "stage", "StageInfo.bin.xfbin"));
                }
                if (messageInfoModified)
                {
                    KyurutoDialogTextLoader("Making localization!", 20);
                    messageInfo_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data"));
                }

                // Write additional modding API files
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "specialCondParam.xfbin"), specialCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "partnerSlotParam.xfbin"), partnerSlotParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "susanooCondParam.xfbin"), susanooCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "pairSpSkillManagerParam.xfbin"), pairManagerParam_vanilla);
                specialInteraction_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "specialInteractionManager.xfbin"));
                conditionprmManager_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "conditionprmManager.xfbin"));

                // Ensure the destination directory for 5kgyprm exists, then write the file
                string spcDir = Path.Combine(root_folder, "cpk_assets", "data", "spc");
                if (!Directory.Exists(spcDir))
                    Directory.CreateDirectory(spcDir);
                File.WriteAllBytes(
                    Path.Combine(spcDir, "5kgyprm.bin.xfbin"),
                    File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "5kgyprm.bin.xfbin"))
                );

                // Repack CPK archives
                KyurutoDialogTextLoader("Removing all trash from root folder and packing everything in CPK archives.", 20);
                try
                {
                    // Repack cpk_assets folder
                    string cpkAssetsPath = Path.GetFullPath(Path.Combine(root_folder, "cpk_assets"));
                    if (Directory.Exists(cpkAssetsPath) &&
                        Directory.EnumerateFiles(cpkAssetsPath, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(cpkAssetsPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk"));


                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk.info"),
                            new byte[4] { 0x20, 0, 0, 0 });
                    }

                    // Repack data_win32_modmanager folder
                    string dataWin32Path = Path.GetFullPath(Path.Combine(root_folder, "data_win32_modmanager"));
                    if (Directory.Exists(dataWin32Path) &&
                        Directory.EnumerateFiles(dataWin32Path, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(dataWin32Path, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk"));

                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk.info"),
                            new byte[4] { 0x21, 0, 0, 0 });
                    }

                    // Repack param_modmanager_path folder
                    string paramModmanagerFullPath = Path.GetFullPath(Path.Combine(root_folder, "param_files"));
                    if (Directory.Exists(paramModmanagerFullPath) &&
                        Directory.EnumerateFiles(paramModmanagerFullPath, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(paramModmanagerFullPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk"));
                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk.info"),
                            new byte[4] { 0x22, 0, 0, 0 });
                    }
                } catch (Exception ex)
                {
                    throw new Exception("Failed to repack CPK: " + ex.Message, ex);
                }

                // Clean up temporary directories
                if (Directory.Exists(Path.Combine(root_folder, "cpk_assets")))
                    Directory.Delete(Path.Combine(root_folder, "cpk_assets"), true);
                if (Directory.Exists(Path.Combine(root_folder, "data_win32_modmanager")))
                    Directory.Delete(Path.Combine(root_folder, "data_win32_modmanager"), true);
                if (Directory.Exists(Path.Combine(root_folder, "param_files")))
                    Directory.Delete(Path.Combine(root_folder, "param_files"), true);

                File.Copy(Directory.GetCurrentDirectory() + "\\ParamFiles\\freebtl_set.gfx", Properties.Settings.Default.RootGameFolder + "\\data\\ui\\flash\\OTHER\\freebtl_set\\freebtl_set.gfx", true);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    KyurutoDialogTextLoader("Your mods are ready!", 20);
                });
                SystemSounds.Beep.Play();

                //ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.FileName = "steam://rungameid/1020790";
                //startInfo.UseShellExecute = true;
                //startInfo.CreateNoWindow = true;
                //Process process = new Process();
                //process.StartInfo = startInfo;
                //process.Start();

                string exePath = Path.Combine(root_folder, "NSUNSC.exe");
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                Process process = new Process { StartInfo = startInfo };
                process.Start();


                LoadingStatePlay = Visibility.Hidden;
                Debug.WriteLine("Completed processing.");
                if (skippedLabels.Any())
                {
                    string message = "Unable to find characodes for Team Ultimate Jutsus. These Team Ultimate Jutsus were skipped:\n" +
                                     string.Join("\n", skippedLabels);
                    MessageBox.Show(message);
                }
            } catch (Exception ex)
            {
                e.Result = ex;
                Debug.WriteLine($"Error: {ex}");
                throw; // Ensure the error is propagated to RunWorkerCompleted
            }
        }

        public void CompileMods()
        {

            try
            {
                string gameDir = Properties.Settings.Default.RootGameFolder;
                var dirInfo = new DirectoryInfo(gameDir);
                if (!dirInfo.Exists || (dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    ModernWpf.MessageBox.Show("Game directory is read-only or inaccessible. Run as Administrator.");
                    return;
                }

                if (Directory.Exists(Properties.Settings.Default.RootGameFolder))
                {
                    LoadingStatePlay = Visibility.Visible;

                    CompileModAsyncProcess(Properties.Settings.Default.RootGameFolder);

                } else
                    ModernWpf.MessageBox.Show("Set root folder of game!");
            } catch (Exception ex)
            {
                HandleError(ex);
            }

        }


        public void InstallMod(string mod_path = "")
        {
            try
            {
                if (string.IsNullOrEmpty(mod_path))
                {
                    OpenFileDialog myDialog = new OpenFileDialog();
                    myDialog.Filter = "Naruto Storm Connection Mod (*.nsc)|*.nsc";
                    myDialog.CheckFileExists = true;
                    myDialog.Multiselect = false;
                    if (myDialog.ShowDialog() == true)
                    {
                        mod_path = myDialog.FileName;
                    } else
                    {
                        return;
                    }
                }

                string root_folder = Properties.Settings.Default.RootGameFolder;
                string modmanager_folder = Path.Combine(root_folder, "modmanager");
                if (Directory.Exists(root_folder))
                {
                    if (!Directory.Exists(modmanager_folder))
                    {
                        Directory.CreateDirectory(modmanager_folder);
                    }
                    // Use Path.Combine to ensure proper path building.
                    string InstallMod_folder = Path.Combine(modmanager_folder, Path.GetFileNameWithoutExtension(mod_path));
                    if (Directory.Exists(InstallMod_folder))
                    {
                        Directory.Delete(InstallMod_folder, true);
                    }
                    Directory.CreateDirectory(InstallMod_folder);
                    System.IO.Compression.ZipFile.ExtractToDirectory(mod_path, InstallMod_folder);
                    RefreshModList();
                } else
                {
                    ModernWpf.MessageBox.Show("Select Root folder for game.");
                }
            } catch (Exception ex)
            {

                HandleError(ex);
            }
        }


        public void DeleteMod()
        {
            if (SelectedMod is not null)
            {
                string mod_path = SelectedMod.ModFolder;
                if (Directory.Exists(mod_path))
                {
                    Directory.Delete(mod_path, true);
                    ModManagerList.Remove(SelectedMod);
                    RefreshModList();
                }
            }
        }

        public void EnableModIsChecked()
        {
            if (SelectedMod is not null)
            {
                string mod_path = SelectedMod.ModFolder;

                var ModInfo = new IniFile(mod_path + "\\mod_config.ini");
                ModInfo.Write("EnableMod", SelectedMod.EnableMod.ToString().ToLower(), "ModManager");

                if (SelectedMod.EnableMod)
                {
                    KyurutoDialogTextLoader(SelectedMod.ModName + " was enabled!",
                20);
                } else
                {
                    KyurutoDialogTextLoader(SelectedMod.ModName + " was disabled!",
                20);
                }

                RefreshModList();
            }
        }

        public void CleanGameAssets(bool OpenMessage = true)
        {
            // Ensure we're on the UI thread.
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => CleanGameAssets(OpenMessage));
                return;
            }

            try
            {
                if (Directory.Exists(Properties.Settings.Default.RootGameFolder))
                {
                    MessageBoxResult result = MessageBoxResult.No;
                    if (OpenMessage)
                    {
                        result = (MessageBoxResult)ModernWpf.MessageBox.Show(
                            "Are you sure you want to clean your game from mods?",
                            "",
                            MessageBoxButton.YesNo);
                    }

                    if (result == MessageBoxResult.Yes || !OpenMessage)
                    {
                        string appFolder = Directory.GetCurrentDirectory();
                        // Build the target folder using Path.Combine.
                        string rootFolder = Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "ui", "flash", "OTHER");

                        // Delete moddingapi folder if it exists.
                        string moddingApiPath = Path.Combine(Properties.Settings.Default.RootGameFolder, "moddingapi");
                        if (Directory.Exists(moddingApiPath))
                            Directory.Delete(moddingApiPath, true);

                        InstallModdingAPI(false);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "stagesel.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "stagesel_image.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel_image.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "charsel.gfx"),
                            Path.Combine(rootFolder, "charsel", "charsel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "charicon_s.gfx"),
                            Path.Combine(rootFolder, "charicon_s", "charicon_s.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "nuccMaterial_dx11.nsh"),
                            Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "system", "nuccMaterial_dx11.nsh"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "freebtl_set_vanilla.gfx"),
                            Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "ui", "flash", "OTHER", "freebtl_set", "freebtl_set.gfx"),
                            true);

                        if (Properties.Settings.Default.EnableMotionBlur)
                        {
                            File.Copy(
                                Path.Combine(appFolder, "ParamFiles", "nuccPostEffect_dx11_S2.nsh"),
                                Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "system", "nuccPostEffect_dx11.nsh"),
                                true);
                        } else
                        {
                            File.Copy(
                                Path.Combine(appFolder, "ParamFiles", "nuccPostEffect_dx11.nsh"),
                                Path.Combine(Properties.Settings.Default.RootGameFolder, "data", "system", "nuccPostEffect_dx11.nsh"),
                                true);
                        }

                        if (OpenMessage)
                            ModernWpf.MessageBox.Show("Game was cleaned!");
                    }
                } else
                {
                    ModernWpf.MessageBox.Show("Select root folder of game");
                }
            } catch (Exception ex)
            {
                LoadingStatePlay = Visibility.Hidden;

                HandleError(ex);
            }
        }

        public void SaveSettings()
        {
            bool restart = false;
            switch (StretchMode_field)
            {
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

            if (File.Exists(BackgroundImagePath_field))
            {
                Properties.Settings.Default.BackgroundImagePath = BackgroundImagePath_field;
                restart = true;
            }
            Properties.Settings.Default.Save();
            KyurutoDialogTextLoader("Love it!",
                20);
            if (restart)
                ModernWpf.MessageBox.Show("Some changes requires to restart toolbox!");
        }
        public void ResetSettings()
        {
            bool restart = false;
            Properties.Settings.Default.StretchMode = "Uniform";
            Properties.Settings.Default.BackgroundColor1 = "#9C000000";
            Properties.Settings.Default.ButtonColor1 = "#9C000000";
            Properties.Settings.Default.TextColor1 = "White";
            if (Properties.Settings.Default.BackgroundImagePath != "UI/background/bg_toolbox_main.png")
            {
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
        public void SelectImageBackground()
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "PNG Image (*.png)|*.png|JPG Image (*.jpg)|*.jpg|JPEG Image (*.jpeg)|*.jpeg";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                BackgroundImagePath_field = myDialog.FileName;
            } else
            {
                return;
            }
        }

        public void VisitModdingGroup()
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = Settings.Default.ModdingGroupLink,
                UseShellExecute = true
            });

        }
        public void VisitBoosty()
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://boosty.to/theleonx/single-payment/donation/383406?share=target_link",
                UseShellExecute = true
            });

        }

        public void VisitGitHubPage()
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/TheLeonX/NSC-ModManager/releases",
                UseShellExecute = true
            });

        }
        public void SelectRootFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select root folder of game";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.RootGameFolder = dialog.FileName;
                RootFolderPath_field = dialog.FileName;
                Properties.Settings.Default.Save();
            } else
            {
                return;
            }
        }

        public void InstallModdingAPI(bool showMessage = true, string root_path = "")
        {
            try
            {
                if (string.IsNullOrEmpty(root_path))
                {
                    if (string.IsNullOrEmpty(Properties.Settings.Default.RootGameFolder))
                    {
                        var dialog = new CommonOpenFileDialog
                        {
                            IsFolderPicker = true,
                            Title = "Select root folder of game"
                        };
                        CommonFileDialogResult result = dialog.ShowDialog();
                        root_path = dialog.FileName;
                        Properties.Settings.Default.RootGameFolder = dialog.FileName;
                        Properties.Settings.Default.Save();
                    } else
                    {
                        root_path = Properties.Settings.Default.RootGameFolder;
                    }
                }

                if (Directory.Exists(root_path))
                {
                    string moddingAPIFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "ModdingAPIFiles");
                    Program.CopyFilesRecursively(moddingAPIFilesPath, root_path);
                    if (showMessage)
                    {
                        ModernWpf.MessageBox.Show("ModdingAPI was installed!");
                    }
                } else
                {
                    return;
                }
            } catch (Exception ex)
            {

                HandleError(ex);
            }
        }

        public void DeleteModdingAPI()
        {
            try
            {
                string rootFolder = Properties.Settings.Default.RootGameFolder;
                string moddingAPIPath = Path.Combine(rootFolder, "moddingapi");

                if (Directory.Exists(moddingAPIPath))
                {
                    MessageBoxResult warning = (MessageBoxResult)ModernWpf.MessageBox.Show(
                        "Are you sure that you want to delete ModdingAPI? All mods inside of it will be deleted too.",
                        "Do you want to delete it?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (warning == MessageBoxResult.Yes)
                    {
                        Directory.Delete(moddingAPIPath, true);

                        string xinputPath = Path.Combine(rootFolder, "xinput9_1_0.dll");
                        if (File.Exists(xinputPath))
                            File.Delete(xinputPath);

                        string xinputAlternatePath = Path.Combine(rootFolder, "xinput9_1_0_o.dll");
                        if (File.Exists(xinputAlternatePath))
                            File.Delete(xinputAlternatePath);

                        ModernWpf.MessageBox.Show("ModdingAPI was deleted!");
                        KyurutoDialogTextLoader("Mod manager will install ModdingAPI anyway.", 20);
                    }
                } else
                {
                    return;
                }
            } catch (Exception ex)
            {

                HandleError(ex);
            }
        }

        // команда добавления нового объекта
        private RelayCommand addMeouch;
        public RelayCommand AddMeouch
        {
            get
            {
                return addMeouch ??
                  (addMeouch = new RelayCommand(obj =>
                  {
                      if (MeouchCounter == 10)
                      {
                          MeouchVisibility = Visibility.Visible;
                          KyurutoVisibility = Visibility.Hidden;
                          MeouchEffectAutoPlay = true;
                          KuramaName = "Meouch";
                          MeouchEffectRepeat = new RepeatBehavior(1.0);
                          KuramaDialog = "";
                          KyurutoDialogTextLoader("Meow! You can call me " + KuramaName + ".", 50);
                          MeouchCounter++;

                      } else
                      {
                          MeouchCounter++;
                      }

                  }));
            }
        }
        private RelayCommand _characterManagementCommand;
        public RelayCommand CharacterManagementCommand
        {
            get
            {
                return _characterManagementCommand ??
                  (_characterManagementCommand = new RelayCommand(obj =>
                  {
                      ToolTabState = 1;

                  }));
            }
        }
        private RelayCommand _optionsCommand;
        public RelayCommand OptionsCommand
        {
            get
            {
                return _optionsCommand ??
                  (_optionsCommand = new RelayCommand(obj =>
                  {
                      ToolTabState = 2;

                  }));
            }
        }
        private RelayCommand _creditsCommand;
        public RelayCommand CreditsCommand
        {
            get
            {
                return _creditsCommand ??
                  (_creditsCommand = new RelayCommand(obj =>
                  {
                      MainTabState = 2;
                  }));
            }
        }
        private RelayCommand _mainMenuCommand;
        public RelayCommand MainMenuCommand
        {
            get
            {
                return _mainMenuCommand ??
                  (_mainMenuCommand = new RelayCommand(obj =>
                  {
                      MainTabState = 1;
                  }));
            }
        }

        private RelayCommand _saveSettingsCommand;
        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ??
                  (_saveSettingsCommand = new RelayCommand(obj =>
                  {
                      SaveSettings();

                  }));
            }
        }
        private RelayCommand _resetSettingsCommand;
        public RelayCommand ResetSettingsCommand
        {
            get
            {
                return _resetSettingsCommand ??
                  (_resetSettingsCommand = new RelayCommand(obj =>
                  {
                      ResetSettings();

                  }));
            }
        }
        private RelayCommand _selectImageBackgroundCommand;
        public RelayCommand SelectImageBackgroundCommand
        {
            get
            {
                return _selectImageBackgroundCommand ??
                  (_selectImageBackgroundCommand = new RelayCommand(obj =>
                  {
                      SelectImageBackground();

                  }));
            }
        }
        private RelayCommand _selectRootFolderCommand;
        public RelayCommand SelectRootFolderCommand
        {
            get
            {
                return _selectRootFolderCommand ??
                  (_selectRootFolderCommand = new RelayCommand(obj =>
                  {
                      SelectRootFolder();

                  }));
            }
        }

        private RelayCommand _installModdingAPICommand;
        public RelayCommand InstallModdingAPICommand
        {
            get
            {
                return _installModdingAPICommand ??
                  (_installModdingAPICommand = new RelayCommand(obj =>
                  {
                      InstallModdingAPI();

                  }));
            }
        }
        private RelayCommand _deleteModdingAPICommand;
        public RelayCommand DeleteModdingAPICommand
        {
            get
            {
                return _deleteModdingAPICommand ??
                  (_deleteModdingAPICommand = new RelayCommand(obj =>
                  {
                      DeleteModdingAPI();

                  }));
            }
        }
        private RelayCommand _visitModdingGroupCommand;
        public RelayCommand VisitModdingGroupCommand
        {
            get
            {
                return _visitModdingGroupCommand ??
                  (_visitModdingGroupCommand = new RelayCommand(obj =>
                  {
                      VisitModdingGroup();

                  }));
            }
        }
        private RelayCommand _boostyCommand;
        public RelayCommand BoostyCommand
        {
            get
            {
                return _boostyCommand ??
                  (_boostyCommand = new RelayCommand(obj =>
                  {
                      VisitBoosty();

                  }));
            }
        }
        private RelayCommand _installModCommand;
        public RelayCommand InstallModCommand
        {
            get
            {
                return _installModCommand ??
                  (_installModCommand = new RelayCommand(obj =>
                  {
                      InstallMod();

                  }));
            }
        }
        private RelayCommand _deleteModCommand;
        public RelayCommand DeleteModCommand
        {
            get
            {
                return _deleteModCommand ??
                  (_deleteModCommand = new RelayCommand(obj =>
                  {
                      DeleteMod();

                  }));
            }
        }
        private RelayCommand _refreshModListCommand;
        public RelayCommand RefreshModListCommand
        {
            get
            {
                return _refreshModListCommand ??
                  (_refreshModListCommand = new RelayCommand(obj =>
                  {
                      RefreshModList();

                  }));
            }
        }

        private RelayCommand _cleanGameRootCommand;
        public RelayCommand CleanGameRootCommand
        {
            get
            {
                return _cleanGameRootCommand ??
                  (_cleanGameRootCommand = new RelayCommand(obj =>
                  {
                      CleanGameAssets(true);

                  }));
            }
        }

        private RelayCommand _compileModsCommand;
        public RelayCommand CompileModsCommand
        {
            get
            {
                return _compileModsCommand ??
                  (_compileModsCommand = new RelayCommand(obj =>
                  {
                      LoadingStatePlay = Visibility.Visible;
                      CompileMods();
                  }));
            }
        }

        private RelayCommand _enableModIsCheckedCommand;
        public RelayCommand EnableModIsCheckedCommand
        {
            get
            {
                return _enableModIsCheckedCommand ??
                  (_enableModIsCheckedCommand = new RelayCommand(obj =>
                  {
                      EnableModIsChecked();

                  }));
            }
        }
        private RelayCommand _rosterEditorCommand;
        public RelayCommand RosterEditorCommand
        {
            get
            {
                return _rosterEditorCommand ??
                  (_rosterEditorCommand = new RelayCommand(obj =>
                  {
                      TitleViewModel VM = ((TitleViewModel)this);
                      CharacterRosterEditorView s = new CharacterRosterEditorView(VM);
                      s.Show();

                  }));
            }
        }
        private RelayCommand _visitGitHubPage;
        public RelayCommand VisitGitHubPageCommand
        {
            get
            {
                return _visitGitHubPage ??
                  (_visitGitHubPage = new RelayCommand(obj =>
                  {
                      VisitGitHubPage();

                  }));
            }
        }
        public async void KyurutoDialogTextLoader(string kuramaDialogUpdate, int timer)
        {
            try
            {
                await Task.Run(() => KyurutoDialogTextWork(kuramaDialogUpdate, timer));
            } catch (Exception)
            {
                //...
            }
        }

        void KyurutoDialogTextWork(string dialog, int timer)
        {
            KuramaDialog = "";
            for (int i = 0; i < dialog.Length; System.Threading.Thread.Sleep(timer))
            {
                if (KuramaDialog.Length != i || (i == 0 && KuramaDialog.Length > 0))
                {
                    break;
                }
                KuramaDialog += dialog[i];
                i++;

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void HandleError(Exception ex)
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string logFilePath = Path.Combine(appFolder, "error.log");
            string errorContent = $"Error: {ex.Message}\n\nStackTrace: {ex.StackTrace}\n\nTime: {DateTime.Now}";

            try
            {
                File.WriteAllText(logFilePath, errorContent);
            } catch (Exception logEx)
            {
                // Optional: handle log write errors if needed
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                SystemSounds.Exclamation.Play();
                ModernWpf.MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error: {ex.Message}\n\n{ex.StackTrace}", "Error");
                KyurutoDialogTextLoader("An error occurred during compilation. Check error details.", 20);
                LoadingStatePlay = Visibility.Hidden;
            });
        }
        private static void WaitForFile(string filePath, int timeoutSeconds = 300)
        {
            Console.WriteLine($"Waiting for file: {filePath}");
            Stopwatch sw = Stopwatch.StartNew();

            while (!File.Exists(filePath))
            {
                if (sw.Elapsed.TotalSeconds > timeoutSeconds)
                {
                    throw new TimeoutException($"Timeout: File {filePath} was not created in {timeoutSeconds} seconds.");
                }

                Thread.Sleep(500); // Ждём 500 мс перед повторной проверкой
            }

            Console.WriteLine($"File detected: {filePath}");
        }
    }
    

}
