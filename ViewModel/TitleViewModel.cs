using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using NSC_ModManager.View;
using NSC_Toolbox.ViewModel;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Application = System.Windows.Application;
using Path = System.IO.Path;

namespace NSC_ModManager.ViewModel
{
    //public class RepackHelper
    //{
    //    public static int RunRepackProcess(string inputFolder, string outputCpk)
    //    {
    //        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YACpkTool.exe");
    //        ProcessStartInfo startInfo = new ProcessStartInfo
    //        {
    //            FileName = exePath,
    //            Arguments = $"\"{inputFolder}\"",
    //            WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
    //            CreateNoWindow = true,  // Отключает создание нового окна
    //            UseShellExecute = true, // Включает выполнение через оболочку (важно для работы упаковщика)
    //            WindowStyle = ProcessWindowStyle.Hidden // Скрывает процесс
    //        };

    //        using (Process process = new Process { StartInfo = startInfo })
    //        {
    //            process.Start();
    //            process.WaitForExit();

    //            // Проверяем, создался ли CPK-файл перед перемещением
    //            string generatedCpk = inputFolder + ".cpk";
    //            if (File.Exists(generatedCpk))
    //            {
    //                File.Move(generatedCpk, outputCpk, true);
    //            }

    //            return process.ExitCode;
    //        }
    //    }

    //    public static int RunExtractProcess(string inputCpk, string outputFolder = "")
    //    {
    //        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YACpkTool.exe");
    //        ProcessStartInfo startInfo = new ProcessStartInfo
    //        {
    //            FileName = exePath,
    //            Arguments = $"\"{inputCpk}\"",
    //            WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
    //            CreateNoWindow = true,
    //            UseShellExecute = true,
    //            WindowStyle = ProcessWindowStyle.Hidden
    //        };

    //        using (Process process = new Process { StartInfo = startInfo })
    //        {
    //            process.Start();
    //            process.WaitForExit();


    //            return process.ExitCode;
    //        }
    //    }

    //}
    public static class RepackXfbinFinder
    {
        /// <summary>
        /// Ищет в указанных папках (по умолчанию: resources_modmanager, cpk_assets, data_win32_modmanager, param_files)
        /// подпапку data/sound/PC и возвращает пути всех .xfbin файлов, найденных в каждой подпапке PC/*.
        /// </summary>
        public static Dictionary<string, List<string>> GetXfbinPathsFromRepackFolders(string rootFolder, IEnumerable<string> repackFolderNames = null)
        {
            if (rootFolder == null) throw new ArgumentNullException(nameof(rootFolder));

            IEnumerable<string> defaultNames = new[] { "resources_modmanager", "cpk_assets", "data_win32_modmanager", "param_files" };
            IEnumerable<string> foldersToCheck = repackFolderNames ?? defaultNames;

            var result = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (string folderName in foldersToCheck)
            {
                string fullRepackPath = Path.GetFullPath(Path.Combine(rootFolder, folderName));
                if (!Directory.Exists(fullRepackPath))
                {
                    continue;
                }

                string pcPath = Path.Combine(fullRepackPath, "data", "sound", "PC");
                if (!Directory.Exists(pcPath))
                {
                    continue;
                }

                // Перебираем только непосредственные подпапки внутри data/sound/PC
                string[] subdirectories;
                try
                {
                    subdirectories = Directory.GetDirectories(pcPath, "*", SearchOption.TopDirectoryOnly);
                } catch (Exception)
                {
                    continue;
                }

                foreach (string subdir in subdirectories)
                {
                    List<string> xfbinFiles;
                    try
                    {
                        // Собираем все .xfbin внутри этой подпапки (включая вложенные).
                        xfbinFiles = Directory.EnumerateFiles(subdir, "*.xfbin", SearchOption.AllDirectories).ToList();
                    } catch (Exception)
                    {
                        xfbinFiles = new List<string>();
                    }

                    if (xfbinFiles.Count > 0)
                    {
                        result[subdir] = xfbinFiles;
                    }
                }
            }

            return result;
        }
    }
    public class RepackHelper
    {
        public static void RemoveZoneIdentifier(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            // Попытка удалить ADS напрямую
            try
            {
                File.Delete(path + ":Zone.Identifier");
                return;
            } catch
            {
                // игнорируем и пробуем PowerShell-фоллбек
            }

            // Фоллбек: PowerShell Unblock-File (экран вывода подавлён)
            try
            {
                string psPath = path.Replace("'", "''");
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-NoProfile -Command \"Unblock-File -LiteralPath '{psPath}'\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                using (var p = Process.Start(psi))
                {
                    p.WaitForExit();
                }
            } catch
            {
                // окончательно молча игнорируем ошибки
            }
        }

        public static int RunRepackProcess(string inputFolder, string outputCpk)
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YACpkTool.exe");

            // Разблокируем сам YACpkTool.exe перед запуском
            RemoveZoneIdentifier(exePath);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{inputFolder}\"",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                process.WaitForExit();

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

            // Разблокируем сам YACpkTool.exe перед запуском
            RemoveZoneIdentifier(exePath);

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

                // Очистка меток распакованных файлов (опционально)
                if (!string.IsNullOrEmpty(outputFolder) && Directory.Exists(outputFolder))
                {
                    // при необходимости можно вызвать рекурсивное RemoveZoneIdentifier для файлов в outputFolder
                }

                return process.ExitCode;
            }
        }
    }
    public class TitleViewModel : INotifyPropertyChanged
    {
        public bool IsS4
        {
            get { return GameVersion == 2; }
            set { GameVersion = value ? 2 : 1; }
        }
        private int _gameVersion;
        public int GameVersion
        {
            get { return _gameVersion; }
            set
            {
                if (_gameVersion == value) return;
                _gameVersion = value;
                OnPropertyChanged(nameof(GameVersion));
                OnPropertyChanged(nameof(IsS4));
            }
        }
        private ObservableCollection<BitmapImage> _screenshots;
        private int _currentScreenshotIndex;
        private BitmapImage _currentScreenshot;
        public ObservableCollection<BitmapImage> Screenshots
        {
            get { return _screenshots; }
            set
            {
                _screenshots = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasScreenshots));
                OnPropertyChanged(nameof(TotalScreenshots));
            }
        }

        public BitmapImage CurrentScreenshot
        {
            get { return _currentScreenshot; }
            set
            {
                _currentScreenshot = value;
                OnPropertyChanged();
            }
        }

        public int CurrentScreenshotIndex
        {
            get { return _currentScreenshotIndex + 1; } // Display as 1-based
        }

        public int TotalScreenshots
        {
            get { return Screenshots?.Count ?? 0; }
        }

        public bool HasScreenshots
        {
            get { return Screenshots != null && Screenshots.Count > 0; }
        }
        public BitmapImage GetNextScreenshot()
        {
            _currentScreenshotIndex = (_currentScreenshotIndex + 1) % Screenshots.Count;
            OnPropertyChanged(nameof(CurrentScreenshotIndex));
            return Screenshots[_currentScreenshotIndex];
        }
        // Call this method when SelectedMod changes
        private void LoadScreenshots(ModManagerModel selectedMod)
        {
            if (selectedMod == null || string.IsNullOrEmpty(selectedMod.ScreenshotsPath))
            {
                Screenshots = null;
                CurrentScreenshot = null;
                _currentScreenshotIndex = 0;
                return;
            }

            if (!Directory.Exists(selectedMod.ScreenshotsPath))
            {
                Screenshots = null;
                CurrentScreenshot = null;
                _currentScreenshotIndex = 0;
                return;
            }

            try
            {
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                var imageFiles = Directory.GetFiles(selectedMod.ScreenshotsPath)
                    .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLower()))
                    .OrderBy(f => f)
                    .ToList();

                if (imageFiles.Count == 0)
                {
                    Screenshots = null;
                    CurrentScreenshot = null;
                    _currentScreenshotIndex = 0;
                    return;
                }

                var screenshots = new ObservableCollection<BitmapImage>();
                foreach (var imagePath in imageFiles)
                {
                    try
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        bitmap.Freeze(); // For thread safety
                        screenshots.Add(bitmap);
                    } catch
                    {
                        // Skip invalid images
                    }
                }

                Screenshots = screenshots;
                _currentScreenshotIndex = 0;
                CurrentScreenshot = Screenshots.Count > 0 ? Screenshots[0] : null;
                OnPropertyChanged(nameof(CurrentScreenshotIndex));
            } catch
            {
                Screenshots = null;
                CurrentScreenshot = null;
                _currentScreenshotIndex = 0;
            }
        }

        public void NextScreenshot()
        {
            if (Screenshots == null || Screenshots.Count == 0)
                return;

            _currentScreenshotIndex = (_currentScreenshotIndex + 1) % Screenshots.Count;
            CurrentScreenshot = Screenshots[_currentScreenshotIndex];
            OnPropertyChanged(nameof(CurrentScreenshotIndex));
        }
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
        private Visibility _creditsVisibility;
        private Visibility _credits2024Visibility;
        private Visibility _credits2025Visibility;
        private Visibility _credits2026Visibility;
        private string _selectedYearLabel;

        public Visibility CreditsVisibility
        {
            get { return _creditsVisibility; }
            set { _creditsVisibility = value; OnPropertyChanged(); }
        }
        public Visibility Credits2024Visibility
        {
            get { return _credits2024Visibility; }
            set { _credits2024Visibility = value; OnPropertyChanged(); }
        }

        public Visibility Credits2025Visibility
        {
            get { return _credits2025Visibility; }
            set { _credits2025Visibility = value; OnPropertyChanged(); }
        }

        public Visibility Credits2026Visibility
        {
            get { return _credits2026Visibility; }
            set { _credits2026Visibility = value; OnPropertyChanged(); }
        }

        public string SelectedYearLabel
        {
            get => _selectedYearLabel;
            set { _selectedYearLabel = value; OnPropertyChanged(); }
        }
        public ICommand SetCreditsYearCommand { get; }
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
                            CreditsVisibility = Visibility.Hidden;
                            break;
                        case 2:
                            MainWindowVisibility = Visibility.Hidden;
                            CreditsVisibility = Visibility.Visible;
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
        private string _modManagerFolder_field;
        public string ModManagerFolder_field
        {
            get { return _modManagerFolder_field; }
            set
            {
                _modManagerFolder_field = value;
                OnPropertyChanged("ModManagerFolder_field");
            }
        }
        private string _rootFolderPathNS4_field;
        public string RootFolderPathNS4_field
        {
            get { return _rootFolderPathNS4_field; }
            set
            {
                _rootFolderPathNS4_field = value;
                OnPropertyChanged("RootFolderPathNS4_field");
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
            get => _modIconPath;
            set
            {
                _modIconPath = value;

                byte[] fileBytes;
                string path = value;
                if (!File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "TemplateImages", "template_icon.png");
                }

                fileBytes = File.ReadAllBytes(path);

                using (var ms = new MemoryStream(fileBytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    ModIconPreview = bitmap;
                }

                OnPropertyChanged(nameof(ModIconPath));
            }
        }

        private BitmapSource _modIconPreview;
        public BitmapSource ModIconPreview
        {
            get => _modIconPreview;
            set
            {
                _modIconPreview = value;
                OnPropertyChanged(nameof(ModIconPreview));
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
                    LoadScreenshots(value);
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
        public sealed class WorkerArgs
        {
            public string Path { get; init; }
            public int Index { get; init; }
        }
        private void bw_CompileModProcess(object sender, DoWorkEventArgs e)
        {
            var args = (WorkerArgs)e.Argument;

            switch (args.Index)
            {
                case 1:
                    bw_CompileModProcess_NSC(args.Path);
                    break;
                case 2:
                    bw_CompileModProcess_NS4(args.Path);
                    break;
            }
        }
        private void SetYear(object parameter)
        {
            if (parameter == null) return;
            if (!int.TryParse(parameter.ToString(), out int year)) return;
            ShowOnlyYear(year);
        }

        private void ShowOnlyYear(int year)
        {
            Credits2024Visibility = (year == 2024) ? Visibility.Visible : Visibility.Collapsed;
            Credits2025Visibility = (year == 2025) ? Visibility.Visible : Visibility.Collapsed;
            Credits2026Visibility = (year == 2026) ? Visibility.Visible : Visibility.Collapsed;
            SelectedYearLabel = year.ToString();
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

            if (!Directory.Exists(Properties.Settings.Default.ModManagerFolder))
            {
                Properties.Settings.Default.ModManagerFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "modmanager");
                Properties.Settings.Default.Save();
            }

            SetCreditsYearCommand = new RelayCommand(param => SetYear(param), _ => true);
            ShowOnlyYear(2026); // по умолчанию 2024 видим
            int stored = Properties.Settings.Default.StormVersion;
            if (stored == 1 || stored == 2)
                _gameVersion = stored;
            else
                _gameVersion = 1;
            bw.DoWork += bw_CompileModProcess;
            ToolTabState = 1;
            KuramaName = "Kyuruto";
            MeouchCounter = 0;
            MeouchVisibility = Visibility.Hidden;
            KyurutoVisibility = Visibility.Visible;
            LoadingStatePlay = Visibility.Hidden;
            MainWindowVisibility = Visibility.Visible;
            CreditsVisibility = Visibility.Hidden;
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
            RootFolderPath_field = Properties.Settings.Default.RootGameNSCFolder; // use "\\\\?\\" for fixing issue with loading files in long paths
            RootFolderPathNS4_field = Properties.Settings.Default.RootGameNS4Folder; // use "\\\\?\\" for fixing issue with loading files in long paths
            ModManagerFolder_field = Properties.Settings.Default.ModManagerFolder; // use "\\\\?\\" for fixing issue with loading files in long paths
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
        private bool TryReadCSPConfig(string rootPath, string cspCode, out int page, out int slot, out int costume)
        {
            page = -1; slot = -1; costume = -1;
            string cfgPath = Path.Combine(rootPath, "character_config.ini");
            Debug.WriteLine(cspCode);
            Debug.WriteLine(cfgPath);
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(cspCode))
                return false;

            if (!File.Exists(cfgPath))
                return false;

            var ini = new IniFile(cfgPath);
            bool found = false;

            // пробуем сначала NS4-ключи, затем обычные как fallback
            string val;

            val = ini.Read("Page", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int p))
            {
                page = p;
                found = true;
            }

            val = ini.Read("Slot", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int s))
            {
                slot = s;
                found = true;
            }

            val = ini.Read("Costume", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int c))
            {
                costume = c;
                found = true;
            }

            return found;
        }



        private bool TryReadCSPConfig_NS4(string rootPath, string cspCode, out int page, out int slot, out int costume)
        {
            page = -1; slot = -1; costume = -1;
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(cspCode))
                return false;

            string cfgPath = Path.Combine(rootPath, "character_config.ini");
            if (!File.Exists(cfgPath))
                return false;

            var ini = new IniFile(cfgPath);
            bool found = false;

            // пробуем сначала NS4-ключи, затем обычные как fallback
            string val;

            val = ini.Read("Page_NS4", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int p))
            {
                page = p;
                found = true;
            }

            val = ini.Read("Slot_NS4", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int s))
            {
                slot = s;
                found = true;
            }

            val = ini.Read("Costume_NS4", cspCode);
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int c))
            {
                costume = c;
                found = true;
            }

            return found;
        }




        private bool TryReadCostumeCSPConfig(string rootPath, out int page, out int slot, out int costume)
        {
            page = -1; slot = -1; costume = -1;
            string cfgPath = Path.Combine(rootPath, "model_config.ini");
            if (string.IsNullOrEmpty(rootPath))
                return false;

            if (!File.Exists(cfgPath))
                return false;

            var ini = new IniFile(cfgPath);
            bool found = false;

            // пробуем сначала NS4-ключи, затем обычные как fallback
            string val;

            val = ini.Read("Page", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int p))
            {
                page = p;
                found = true;
            }

            val = ini.Read("Slot", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int s))
            {
                slot = s;
                found = true;
            }

            val = ini.Read("Costume", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int c))
            {
                costume = c;
                found = true;
            }

            return found;
        }



        private bool TryReadCostumeCSPConfig_NS4(string rootPath, out int page, out int slot, out int costume)
        {
            page = -1; slot = -1; costume = -1;
            if (string.IsNullOrEmpty(rootPath))
                return false;

            string cfgPath = Path.Combine(rootPath, "model_config.ini");
            if (!File.Exists(cfgPath))
                return false;

            var ini = new IniFile(cfgPath);
            bool found = false;

            // пробуем сначала NS4-ключи, затем обычные как fallback
            string val;

            val = ini.Read("Page_NS4", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int p))
            {
                page = p;
                found = true;
            }

            val = ini.Read("Slot_NS4", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int s))
            {
                slot = s;
                found = true;
            }

            val = ini.Read("Costume_NS4", "ModManager");
            if (!string.IsNullOrWhiteSpace(val) && int.TryParse(val.Trim(), out int c))
            {
                costume = c;
                found = true;
            }

            return found;
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
            //string root_folder = Properties.Settings.Default.RootGameNSCFolder;
            //string modmanager_folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "modmanager");


            string modmanager_folder = Properties.Settings.Default.ModManagerFolder;

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
                        Game = ModInfo.Read("Game", "ModManager"),
                        EnableMod = Convert.ToBoolean(ModInfo.Read("EnableMod", "ModManager")),
                        IconPath = Path.GetDirectoryName(mod_path.FullName) + "\\mod_icon.png",
                        ScreenshotsPath = Path.Combine(Path.GetDirectoryName(mod_path.FullName), "Screenshots"),
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

                            string gameVersion = CharacterInfo.Read("Game", "ModManager");
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try
                                {
                                    CharacterInfo.Write("Game", gameVersion, "ModManager");
                                } catch { }
                            }

                            // Page_NS4
                            int pageNs4;
                            string pageNs4Str = CharacterInfo.Read("Page_NS4", "ModManager");
                            if (string.IsNullOrWhiteSpace(pageNs4Str) || !int.TryParse(pageNs4Str, out pageNs4))
                            {
                                pageNs4 = -1;
                                try { CharacterInfo.Write("Page_NS4", pageNs4.ToString(), "ModManager"); } catch { }
                            }

                            // Slot_NS4
                            int slotNs4;
                            string slotNs4Str = CharacterInfo.Read("Slot_NS4", "ModManager");
                            if (string.IsNullOrWhiteSpace(slotNs4Str) || !int.TryParse(slotNs4Str, out slotNs4))
                            {
                                slotNs4 = -1;
                                try { CharacterInfo.Write("Slot_NS4", slotNs4.ToString(), "ModManager"); } catch { }
                            }
                            bool EnableRosterChange;
                            string EnableRosterChangeStr = CharacterInfo.Read("EnableRosterChange", "ModManager");
                            if (string.IsNullOrWhiteSpace(EnableRosterChangeStr) || !bool.TryParse(EnableRosterChangeStr, out EnableRosterChange))
                            {
                                EnableRosterChange = false;
                                try { CharacterInfo.Write("EnableRosterChange", EnableRosterChange.ToString(), "ModManager"); } catch { }
                            }
                            bool EnableRosterChangeNS4;
                            string EnableRosterChangeNS4Str = CharacterInfo.Read("EnableRosterChangeNS4", "ModManager");
                            if (string.IsNullOrWhiteSpace(EnableRosterChangeNS4Str) || !bool.TryParse(EnableRosterChangeNS4Str, out EnableRosterChangeNS4))
                            {
                                EnableRosterChangeNS4 = false;
                                try { CharacterInfo.Write("EnableRosterChangeNS4", EnableRosterChangeNS4.ToString(), "ModManager"); } catch { }
                            }

                            CharacterModModel CharacterEntry = new CharacterModModel()
                            {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                Page_NS4 = pageNs4,
                                Slot_NS4 = slotNs4,
                                EnableRosterChange = EnableRosterChange,
                                EnableRosterChangeNS4 = EnableRosterChangeNS4,
                                GameVersion = gameVersion,
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

                            string gameVersion = StageInfo.Read("Game", "ModManager");
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try
                                {
                                    StageInfo.Write("Game", gameVersion, "ModManager");
                                } catch { }
                            }

                            // BGM_ID_NS4
                            int bgmIdNs4;
                            string bgmIdNs4Str = StageInfo.Read("BGM_ID_NS4", "ModManager");
                            if (string.IsNullOrWhiteSpace(bgmIdNs4Str) || !int.TryParse(bgmIdNs4Str, out bgmIdNs4))
                            {
                                bgmIdNs4 = Convert.ToInt32(StageInfo.Read("BGM_ID", "ModManager"));
                                try { StageInfo.Write("BGM_ID_NS4", bgmIdNs4.ToString(), "ModManager"); } catch { }
                            }

                            StageModModel StageEntry = new StageModModel()
                            {
                                StageName = Path.GetFileName(Path.GetDirectoryName(stage_path.FullName)),
                                BgmID = Convert.ToInt32(StageInfo.Read("BGM_ID", "ModManager")),
                                BgmID_NS4 = bgmIdNs4,
                                MessageID = StageInfo.Read("MessageID", "ModManager"),
                                Hell = Convert.ToBoolean(StageInfo.Read("Hell", "ModManager")),
                                GameVersion = gameVersion,
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

                            string gameVersion = CostumeInfo.Read("Game", "ModManager");
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try
                                {
                                    CostumeInfo.Write("Game", gameVersion, "ModManager");
                                } catch
                                {
                                }
                            }
                            bool EnableRosterChange;
                            string EnableRosterChangeStr = CostumeInfo.Read("EnableRosterChange", "ModManager");
                            if (string.IsNullOrWhiteSpace(EnableRosterChangeStr) || !bool.TryParse(EnableRosterChangeStr, out EnableRosterChange))
                            {
                                EnableRosterChange = false;
                                try { CostumeInfo.Write("EnableRosterChange", EnableRosterChange.ToString(), "ModManager"); } catch { }
                            }
                            bool EnableRosterChangeNS4;
                            string EnableRosterChangeNS4Str = CostumeInfo.Read("EnableRosterChangeNS4", "ModManager");
                            if (string.IsNullOrWhiteSpace(EnableRosterChangeNS4Str) || !bool.TryParse(EnableRosterChangeNS4Str, out EnableRosterChangeNS4))
                            {
                                EnableRosterChangeNS4 = false;
                                try { CostumeInfo.Write("EnableRosterChangeNS4", EnableRosterChangeNS4.ToString(), "ModManager"); } catch { }
                            }
                            CostumeModModel CostumeEntry = new CostumeModModel()
                            {
                                Characode = CostumeInfo.Read("Characode", "ModManager"),
                                BaseCostume = CostumeInfo.Read("BaseModel", "ModManager"),
                                AwakeCostume = CostumeInfo.Read("AwakeModel", "ModManager"),
                                EnableRosterChange = EnableRosterChange,
                                EnableRosterChangeNS4 = EnableRosterChangeNS4,
                                GameVersion = gameVersion,
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

                            string gameVersion = TUJInfo.Read("Game", "ModManager");
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try
                                {
                                    TUJInfo.Write("Game", gameVersion, "ModManager");
                                } catch
                                {
                                }
                            }

                            string characodesString = TUJInfo.Read("Characodes", "ModManager");
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
                                GameVersion = gameVersion,
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
                Directory.CreateDirectory(modmanager_folder);
                ModernWpf.MessageBox.Show("No mods found.");
            }
        }
        static BackgroundWorker bw = new BackgroundWorker();
        void CompileModAsyncProcess(string root_folder)
        {

            try
            {
                //MessageBox.Show(CharacterList[-1].Characode);
                LoadingStatePlay = Visibility.Visible;
                RefreshModList();
                bw.WorkerReportsProgress = true;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted; // Add completion handler
                if (!bw.IsBusy)
                {
                    bw.RunWorkerAsync(new WorkerArgs { Path = root_folder, Index = Properties.Settings.Default.StormVersion });

                } else
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

        void bw_CompileModProcess_NSC(string path)
        {
            try
            {
                string root_folder = path;
                CleanGameAssets(false, false);
                InstallModdingAPI(false, Properties.Settings.Default.RootGameNSCFolder);
                Debug.WriteLine("Starting mod compilation...");
                KyurutoDialogTextLoader("Preparing all files!",
                    20);


                //vanilla files
                string characodePath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "characode.bin.xfbin");
                string duelPlayerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "duelPlayerParam.xfbin");
                string playerSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "playerSettingParam.bin.xfbin");
                string skillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "skillCustomizeParam.xfbin");
                string spSkillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "spSkillCustomizeParam.xfbin");
                string skillIndexSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "skillIndexSettingParam.xfbin");
                string supportSkillRecoverySpeedParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "supportSkillRecoverySpeedParam.xfbin");
                string privateCameraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "privateCamera.bin.xfbin");
                string characterSelectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "characterSelectParam.xfbin");

                string costumeBreakColorParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "costumeBreakColorParam.xfbin");

                string costumeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "costumeParam.bin.xfbin");
                string playerIconPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "player_icon.xfbin");
                string cmnparamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "cmnparam.xfbin");
                string supportActionParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "supportActionParam.xfbin");
                string awakeAuraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "awakeAura.xfbin");
                string appearanceAnmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "appearanceAnm.xfbin");
                string afterAttachObjectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "afterAttachObject.xfbin");
                string playerDoubleEffectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "playerDoubleEffectParam.xfbin");
                string spTypeSupportParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "spTypeSupportParam.xfbin");
                string costumeBreakParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "costumeBreakParam.xfbin");
                string messageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "message");
                string damageeffPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "damageeff.bin.xfbin");
                string damageeffS4Path = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "damageeff.bin.xfbin");
                string effectprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "effectprm.bin.xfbin");
                string damageprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "damageprm.bin.xfbin");
                string stageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "StageInfo.bin.xfbin");
                string nuccMaterialDx11Path = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "nuccMaterial_dx11.nsh");
                string stageselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "stagesel.gfx");
                string stageselImageGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "stagesel_image.gfx");
                string charselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "charsel.gfx");
                string chariconGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "charicon_s.gfx");
                string stage_selectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "select_stage.xfbin");
                string conditionprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "conditionprm.bin.xfbin");

                string specialCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "specialCondParam.xfbin");
                string partnerSlotParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "partnerSlotParam.xfbin");
                string susanooCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "susanooCondParam.xfbin");
                string specialInteractionPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "specialInteractionManager.xfbin");
                string conditionprmManagerPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "conditionprmManager.xfbin");
                string bgmManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "bgmManagerParam.xfbin");

                //TUJ Only
                string pairSpSkillCombinationParam = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "pairSpSkillCombinationParam.xfbin");
                string pairSpSkillManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "pairSpSkillManagerParam.xfbin");


                string guardEffectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "guardEffectParam.xfbin");
                string gudoBallParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "gudoBallParam.xfbin");
                string ougiAwakeningParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NSC", "ougiAwakeningParam.xfbin");



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
                DamageEffViewModel damageeffS4_vanilla = new DamageEffViewModel();
                damageeffS4_vanilla.OpenFile(damageeffS4Path);
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



                GuardEffectParamViewModel guardEffectParam_vanilla = new GuardEffectParamViewModel();
                guardEffectParam_vanilla.OpenFile(guardEffectParamPath);

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
                byte[] ougiAwakeningParam_vanilla = File.ReadAllBytes(ougiAwakeningParamPath);
                byte[] gudoBallParam_vanilla = File.ReadAllBytes(gudoBallParamPath);

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

                    string specialCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "specialCondParam.xfbin");
                    string partnerSlotParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "partnerSlotParam.xfbin");
                    string susanooCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "susanooCondParam.xfbin");
                    string conditionprmManagerModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "conditionprmManager.xfbin");
                    if (!File.Exists(specialCondParamModPath))
                        specialCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "specialCondParam.xfbin");
                    if (!File.Exists(partnerSlotParamModPath))
                        partnerSlotParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "partnerSlotParam.xfbin");
                    if (!File.Exists(susanooCondParamModPath))
                        susanooCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "susanooCondParam.xfbin");
                    if (!File.Exists(conditionprmManagerModPath))
                        conditionprmManagerModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "conditionprmManager.xfbin");

                    string guardEffectParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "guardEffectParam.xfbin");
                    string ougiAwakeningParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "ougiAwakeningParam.xfbin");
                    string gudoBallParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param","gudoBallParam.xfbin");

                    string stormVersion = character_mod.GameVersion;
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
                            //!File.Exists(skillIndexSettingParamModPath) ||
                            //!File.Exists(supportSkillRecoverySpeedParamModPath) ||
                            //!File.Exists(privateCameraModPath) ||
                            //!File.Exists(costumeParamModPath) ||
                            !File.Exists(playerIconModPath) ||
                            !File.Exists(cmnparamModPath) ||
                            !File.Exists(characterSelectParamModPath))
                            {
                                MessageBox.Show("Missing Param files");
                                continue;
                            }
                        } else
                        {
                            if (!File.Exists(duelPlayerParamModPath))
                            {
                                MessageBox.Show("Missing DuelPlayerParam file for partner");
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
                    PlayerSettingParamS4ViewModel playerSettingParamS4_mod = new PlayerSettingParamS4ViewModel();
                    if (File.Exists(playerSettingParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                            psp_entry.DLC_ID = -1;
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
                                        psp_entry.DLC_ID = -1;
                                        playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                                    }
                                }
                                break;
                            case "NS4":
                                playerSettingParamS4_mod.OpenFile(playerSettingParamModPath);

                                foreach (PlayerSettingParamModel psp_entry in playerSettingParamS4_mod.PlayerSettingParamList)
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
                                    if (playerSettingParamS4_mod.PlayerSettingParamList.Count > 0 && File.Exists(characterSelectParamModPath))
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
                                        for (int i = 0; i < playerSettingParamS4_mod.PlayerSettingParamList.Count; i++)
                                        {
                                            PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[i].Clone();
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
                                            psp_entry.DLC_ID = -1;
                                            playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);

                                            CostumeParamModel costume_entry = new CostumeParamModel();
                                            costume_entry.PlayerSettingParamID = psp_entry.PSP_ID;
                                            costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                                            costume_entry.EntryIndex = 0; //used for unlocking
                                            costume_entry.EntryType = 0;
                                            costume_entry.UnlockCondition = 1;
                                            costume_entry.UnlockCost = 0;
                                            costumeParam_vanilla.CostumeParamList.Add(costume_entry);
                                        }
                                        IsPspModified = true;
                                    }
                                } else
                                {
                                    for (int i = 0; i < playerSettingParamS4_mod.PlayerSettingParamList.Count; i++)
                                    {
                                        PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[i].Clone();
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
                                        psp_entry.DLC_ID = -1;
                                        playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);

                                        CostumeParamModel costume_entry = new CostumeParamModel();
                                        costume_entry.PlayerSettingParamID = psp_entry.PSP_ID;
                                        costume_entry.EntryString = costumeParam_vanilla.LastCostume();
                                        costume_entry.EntryIndex = 0; //used for unlocking
                                        costume_entry.EntryType = 0;
                                        costume_entry.UnlockCondition = 1;
                                        costume_entry.UnlockCost = 0;
                                        costumeParam_vanilla.CostumeParamList.Add(costume_entry);

                                    }
                                }
                                break;
                        }

                    }

                    //costumeBreakColorParam file
                    CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                    CostumeBreakColorParamS4ViewModel costumeBreakColorParamS4_mod = new CostumeBreakColorParamS4ViewModel();
                    if (File.Exists(costumeBreakColorParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                break;
                            case "NS4":
                                costumeBreakColorParamS4_mod.OpenFile(costumeBreakColorParamModPath);
                                if (replace_character)
                                {
                                    if (costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count > 0)
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
                                        for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                        {
                                            CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                            costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                            costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                        }
                                    }
                                } else
                                {
                                    //Add new entries
                                    for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                    {
                                        CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                        costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                        costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                    }
                                }
                                break;
                        }

                    }
                    //costumeParam file
                    CostumeParamViewModel costumeParam_mod = new CostumeParamViewModel();
                    if (File.Exists(costumeParamModPath))
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
                    SkillCustomizeParamS4ViewModel skillCustomizeParamS4_mod = new SkillCustomizeParamS4ViewModel();
                    if (File.Exists(skillCustomizeParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                break;
                            case "NS4":
                                skillCustomizeParamS4_mod.OpenFile(skillCustomizeParamModPath);
                                if (replace_character)
                                {
                                    for (int i = 0; i < skillCustomizeParam_vanilla.SkillCustomizeParamList.Count; i++)
                                    {
                                        if (skillCustomizeParam_vanilla.SkillCustomizeParamList[i].CharacodeID == mod_characodeID)
                                        {
                                            skillCustomizeParam_vanilla.SkillCustomizeParamList[i] = skillCustomizeParamS4_mod.SkillCustomizeParamList[0];
                                            break;
                                        }
                                    }
                                } else
                                {
                                    SkillCustomizeParamModel skillEntry = (SkillCustomizeParamModel)skillCustomizeParamS4_mod.SkillCustomizeParamList[0].Clone();
                                    skillEntry.CharacodeID = mod_characodeID;
                                    skillCustomizeParam_vanilla.SkillCustomizeParamList.Add(skillEntry);
                                }
                                break;
                        }
                        
                    }

                    //spSkillCustomizeParam file
                    SpSkillCustomizeParamViewModel spSkillCustomizeParam_mod = new SpSkillCustomizeParamViewModel();
                    if (File.Exists(spSkillCustomizeParamModPath))
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
                    if (File.Exists(skillIndexSettingParamModPath))
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
                    if (File.Exists(supportSkillRecoverySpeedParamModPath))
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

                    //playerIcon file
                    PlayerIconViewModel playerIcon_mod = new PlayerIconViewModel();
                    if (File.Exists(playerIconModPath))
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
                    CharacterSelectParamS4ViewModel characterSelectParamS4_mod = new CharacterSelectParamS4ViewModel();
                    if (File.Exists(characterSelectParamModPath))
                    {
                        int page = -1;
                        int slot = -1;
                        switch (stormVersion)
                        {

                            case "NSC":
                                characterSelectParam_mod.OpenFile(characterSelectParamModPath);




                                if (replace_character)
                                {
                                    if (!character_mod.EnableRosterChange)
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
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                page = characterSelectParam_vanilla.MaxPage();
                                                slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                                if (slot == 25)
                                                {
                                                    page++;
                                                    slot = 1;
                                                }
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i;
                                            }

                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                } else
                                {

                                    if (!character_mod.EnableRosterChange)
                                    {
                                        page = characterSelectParam_vanilla.MaxPage();
                                        slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                        if (slot == 25)
                                        {
                                            page++;
                                            slot = 1;
                                        }
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        page = characterSelectParam_vanilla.MaxPage();
                                        slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                        if (slot == 25)
                                        {
                                            page++;
                                            slot = 1;
                                        }
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;

                                            if (TryReadCSPConfig(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {

                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i;
                                            }

                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }




                                }
                                break;
                            case "NS4":
                                characterSelectParamS4_mod.OpenFile(characterSelectParamModPath);

                                if (replace_character)
                                {
                                    if (!character_mod.EnableRosterChange)
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

                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i;
                                            csp_entry.SaveInFile = true;
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;

                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        page = characterSelectParam_vanilla.MaxPage();
                                        slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                        if (slot == 25)
                                        {
                                            page++;
                                            slot = 1;
                                        }
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i;
                                            }

                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume >= 0 ? cfgCostume : i;
                                            csp_entry.SaveInFile = true;
                                            //Debug.WriteLine($"{csp_entry.CSP_code} was replaced S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;

                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                } else
                                {
                                    if (!character_mod.EnableRosterChange)
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
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            Debug.WriteLine($"{csp_entry.CSP_code} was added S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            csp_entry.SaveInFile = true;
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;
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
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i;
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            Debug.WriteLine($"{csp_entry.CSP_code} was added S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            csp_entry.SaveInFile = true;
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                }
                                break;
                        }
                        
                    }


                    //supportActionParam file
                    SupportActionParamViewModel supportActionParam_mod = new SupportActionParamViewModel();
                    if (File.Exists(supportActionParamModPath))
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
                    if (File.Exists(costumeBreakParamModPath))
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
                    if (File.Exists(awakeAuraModPath))
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
                    if (File.Exists(appearanceAnmModPath))
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
                    if (File.Exists(afterAttachObjectModPath))
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
                    if (File.Exists(playerDoubleEffectParamModPath))
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
                    if (File.Exists(spTypeSupportParamModPath))
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
                    if (File.Exists(specialCondParamModPath))
                    {
                        specialCondParam_mod = File.ReadAllBytes(specialCondParamModPath);
                        specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        specialCondParam_vanilla = BinaryReader.b_AddBytes(specialCondParam_vanilla, specialCondParam_mod);
                    }

                    //specialCondParam file
                    byte[] partnerSlotParam_mod = new byte[0];
                    if (File.Exists(partnerSlotParamModPath))
                    {
                        partnerSlotParam_mod = File.ReadAllBytes(partnerSlotParamModPath);
                        partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        partnerSlotParam_vanilla = BinaryReader.b_AddBytes(partnerSlotParam_vanilla, partnerSlotParam_mod);
                    }

                    //susanooCondParam file
                    byte[] susanooCondParam_mod = new byte[0];
                    if (File.Exists(susanooCondParamModPath))
                    {
                        susanooCondParam_mod = File.ReadAllBytes(susanooCondParamModPath);
                        susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        susanooCondParam_vanilla = BinaryReader.b_AddBytes(susanooCondParam_vanilla, susanooCondParam_mod);
                    }

                    //guardEffectParam file
                    GuardEffectParamViewModel guardEffectParam_mod = new GuardEffectParamViewModel();
                    if (File.Exists(guardEffectParamModPath))
                    {
                        guardEffectParam_mod.OpenFile(guardEffectParamModPath);
                        for (int i = 0; i < guardEffectParam_mod.GuardEffectParamList.Count; i++)
                        {

                            GuardEffectParamModel guardEffectParamEntry = (GuardEffectParamModel)guardEffectParam_mod.GuardEffectParamList[i].Clone();
                            guardEffectParamEntry.CharacodeID = mod_characodeID;
                            guardEffectParam_vanilla.GuardEffectParamList.Add(guardEffectParamEntry);
                        }
                    }
                    //ougiAwakeningParam file
                    byte[] ougiAwakeningParam_mod = new byte[0];
                    if (File.Exists(ougiAwakeningParamModPath))
                    {
                        ougiAwakeningParam_mod = File.ReadAllBytes(ougiAwakeningParamModPath);
                        ougiAwakeningParam_mod = BinaryReader.b_ReplaceBytes(ougiAwakeningParam_mod, BitConverter.GetBytes(mod_characodeID), 0, 0, 4);
                        ougiAwakeningParam_vanilla = BinaryReader.b_AddBytes(ougiAwakeningParam_vanilla, ougiAwakeningParam_mod);
                    }

                    byte[] gudoBallParam_mod = new byte[0];
                    if (File.Exists(gudoBallParamModPath))
                    {
                        gudoBallParam_mod = File.ReadAllBytes(gudoBallParamModPath);
                        gudoBallParam_mod = BinaryReader.b_ReplaceBytes(gudoBallParam_mod, BitConverter.GetBytes(mod_characodeID), 0, 0, 4);
                        gudoBallParam_vanilla = BinaryReader.b_AddBytes(gudoBallParam_vanilla, gudoBallParam_mod);
                    }

                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfo_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langList.Length > l ? Program.langList[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langS4List, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langS4List, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langS4List, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfoS4_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfoS4_mod.MessageInfo_List[srcIndex][i].Speaker = new byte[0];
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfoS4_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }
                    //damageprm file
                    DamagePrmViewModel damageprm_mod = new DamagePrmViewModel();
                    if (File.Exists(damageprmModPath))
                    {
                        damageprm_mod.OpenFile(damageprmModPath);
                        for (int i = 0; i < damageprm_mod.DamagePrmList.Count; i++)
                        {
                            var entry = (DamagePrmModel)damageprm_mod.DamagePrmList[i].Clone();

                            // если мод для Storm 4 — читаем int32 по смещению 0x6C, ищем соответствие в списках и заменяем
                            if (stormVersion == "NS4" && entry.Data != null && entry.Data.Length >= 0x6C + 4)
                            {
                                int oldIndex = BitConverter.ToInt32(entry.Data, 0x6C);
                                if (oldIndex >= 0 && Program.CONDITION_NS4_LIST != null && oldIndex < Program.CONDITION_NS4_LIST.Length)
                                {
                                    string cond = Program.CONDITION_NS4_LIST[oldIndex];
                                    if (!string.IsNullOrEmpty(cond) && Program.CONDITION_NSC_LIST != null)
                                    {
                                        int newIndex = Array.IndexOf(Program.CONDITION_NSC_LIST, cond);
                                        if (newIndex >= 0)
                                        {
                                            byte[] newBytes = BitConverter.GetBytes(newIndex);
                                            Array.Copy(newBytes, 0, entry.Data, 0x6C, 4);
                                        }
                                        // если newIndex < 0 — соответствия нет, оставляем старый индекс
                                    }
                                }
                                // если oldIndex вне диапазона — не трогаем
                            }

                            damageprm_vanilla.DamagePrmList.Add(entry);
                        }
                    }

                    //prm
                    PRMEditorViewModel prm_mod = new PRMEditorViewModel();

                    var modDir = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(character_mod.RootPath)));
                    var prmFiles = modDir
                        .GetFiles($"{mod_characode}prm.bin.xfbin", SearchOption.AllDirectories)
                        .OrderBy(f => f.DirectoryName, StringComparer.OrdinalIgnoreCase)
                        .ToArray();

                    if (prmFiles.Length > 0)
                    {
                        string prm_path = prmFiles.Last().FullName;
                        string relative = prm_path.Substring(prm_path.IndexOf("data\\", StringComparison.OrdinalIgnoreCase));
                        string new_prm_path = Path.Combine(root_folder, "param_files", relative);

                        // Только если оба файла существуют, выполняем merge
                        if (File.Exists(prm_path) && File.Exists(damageeffModPath))
                        {
                            // load mod and vanilla view‑models
                            var damageeff_mod = new DamageEffViewModel(); damageeff_mod.OpenFile(damageeffModPath);
                            var effectprm_mod = new EffectPrmViewModel();

                            var effectIdMap = new Dictionary<int, int>();
                            if (File.Exists(effectprmModPath))
                            {
                                effectprm_mod.OpenFile(effectprmModPath);
                                foreach (var modEntry in effectprm_mod.EffectPrmList)
                                {
                                    int newId = effectprm_vanilla.MaxEffectID() + 1;
                                    effectIdMap[modEntry.EffectPrmID] = newId;
                                    Debug.WriteLine($"Effect Entry, old id = {modEntry.EffectPrmID}, new id = {newId}");
                                    modEntry.EffectPrmID = newId;
                                    effectprm_vanilla.EffectPrmList.Add((EffectPrmModel)modEntry.Clone());
                                }
                            }

                            // remap EffectPrmID in damageEff_mod and build hit‑ID map
                            var hitIdMap = new Dictionary<int, int>();
                            foreach (var de in damageeff_mod.DamageEffList)
                            {
                                if (effectIdMap.TryGetValue(de.EffectPrmID, out var mapped))
                                {
                                    de.EffectPrmID = mapped;
                                    de.ExtraEffectPrmID = 0;
                                }
                                int newHit = damageeff_vanilla.MaxDamageID() + 1;
                                hitIdMap[de.DamageEffID] = newHit;

                                var clone = (DamageEffModel)de.Clone();
                                Debug.WriteLine($"Damage Entry, old id = {clone.DamageEffID}, new id = {newHit}");
                                clone.DamageEffID = newHit;
                                if (hitIdMap.TryGetValue(clone.ExtraDamageEffID, out var extra))
                                    clone.ExtraDamageEffID = extra;

                                damageeff_vanilla.DamageEffList.Add(clone);
                            }

                            // open and correct prm
                            prm_mod.OpenFile(prm_path);
                            foreach (var ver in prm_mod.VerList)
                                foreach (var sec in ver.PL_ANM_Sections)
                                    foreach (var fn in sec.FunctionList)
                                    {
                                        if (hitIdMap.TryGetValue(fn.DamageHitEffectID, out var nid))
                                            fn.DamageHitEffectID = (short)nid;

                                    }

                            // save result
                            Directory.CreateDirectory(Path.GetDirectoryName(new_prm_path)!);
                            prm_mod.SaveFileAs(new_prm_path);
                        }
                    }




                }
                //Compile Stage Mods

                foreach (StageModModel stage_mod in StageList)
                {

                    string stormVersion = stage_mod.GameVersion;
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
                            "STAGE_SD51A", "STAGE_0_MAID_IN_HEAVEN"
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
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfo_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langList.Length > l ? Program.langList[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langS4List, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langS4List, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langS4List, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfoS4_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfoS4_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }


                }
                //Compile Model mods
                foreach (CostumeModModel costume_mod in CostumeList)
                {
                    string mod_characode = costume_mod.Characode;
                    string stormVersion = costume_mod.GameVersion;
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
                    int csp_code_index = 0;

                    PlayerSettingParamViewModel playerSettingParam_mod = new PlayerSettingParamViewModel();

                    PlayerSettingParamS4ViewModel playerSettingParamS4_mod = new PlayerSettingParamS4ViewModel();
                    if (File.Exists(playerSettingParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
                                playerSettingParam_mod.OpenFile(playerSettingParamModPath);
                                PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[0].Clone();
                                costume_csp_code = psp_entry.PSP_code;
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
                                break;
                            case "NS4":
                                playerSettingParamS4_mod.OpenFile(playerSettingParamModPath);
                                psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[0].Clone();
                                costume_csp_code = psp_entry.PSP_code;
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
                                break;
                        }
                       
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
                    CostumeBreakColorParamS4ViewModel costumeBreakColorParamS4_mod = new CostumeBreakColorParamS4ViewModel();
                    if (File.Exists(costumeBreakColorParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
                                costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                                //Add new entries
                                for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++)
                                {
                                    CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                                    costumeColor_entry.PlayerSettingParamID = new_preset_id;
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                }
                                break;
                            case "NS4":
                                costumeBreakColorParamS4_mod.OpenFile(costumeBreakColorParamModPath);
                                //Add new entries
                                for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                {
                                    CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                    costumeColor_entry.PlayerSettingParamID = new_preset_id;
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                }
                                break;
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
                    CharacterSelectParamS4ViewModel characterSelectParamS4_mod = new CharacterSelectParamS4ViewModel();
                    if (File.Exists(characterSelectParamModPath))
                    {
                        int page = 0;
                        int slot = 1;
                        int costume = 0;
                        switch (stormVersion)
                        {

                            case "NSC":
                                characterSelectParam_mod.OpenFile(characterSelectParamModPath);
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

                                int new_page = -1;
                                int new_slot = -1;
                                int new_costume = -1;

                                TryReadCostumeCSPConfig(costume_mod.RootPath, out new_page, out new_slot, out new_costume);
                                if (costume_mod.EnableRosterChange && new_page != -1)
                                {

                                    csp_entry.PageIndex = new_page;
                                    csp_entry.SlotIndex = new_slot;
                                    csp_entry.CostumeIndex = new_costume;
                                } else
                                {

                                    csp_entry.PageIndex = page;
                                    csp_entry.SlotIndex = slot;
                                    csp_entry.CostumeIndex = costume;
                                }
                                csp_entry.CSP_code = costume_csp_code;
                                characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                break;
                            case "NS4":
                                characterSelectParamS4_mod.OpenFile(characterSelectParamModPath);
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
                                csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[0].Clone();
                                int new_page_s4 = -1;
                                int new_slot_s4 = -1;
                                int new_costume_s4 = -1;

                                TryReadCostumeCSPConfig(costume_mod.RootPath, out new_page_s4, out new_slot_s4, out new_costume_s4);
                                if (costume_mod.EnableRosterChange && new_page_s4 != -1)
                                {

                                    csp_entry.PageIndex = new_page_s4;
                                    csp_entry.SlotIndex = new_slot_s4;
                                    csp_entry.CostumeIndex = new_costume_s4;
                                } else
                                {

                                    csp_entry.PageIndex = page;
                                    csp_entry.SlotIndex = slot;
                                    csp_entry.CostumeIndex = costume;
                                }
                                csp_entry.CSP_code = costume_csp_code;
                                characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                break;
                        }


                        
                    }

                    //messageInfo files
                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfo_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langList.Length > l ? Program.langList[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langS4List, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langS4List, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langS4List, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfoS4_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfoS4_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }
                }
                List<string> skippedLabels = new List<string>();
                //Compile Team Ultimate Jutsu Mods
                foreach (TeamUltimateJutsuModModel tuj_mod in TUJList)
                {
                    string cmnparamModPath = Path.Combine(tuj_mod.RootPath, "data", "sound", "cmnparam.xfbin");
                    string messageInfoModPath = Path.Combine(tuj_mod.RootPath, "data", "message");

                    string stormVersion = tuj_mod.GameVersion;



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

                    List<int> SkipEntriesList = new List<int> {};
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
                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfo_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfo_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langList.Length > l ? Program.langList[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langS4List, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langS4List, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langS4List, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfoS4_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfoS4_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
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
                int charsel_offset_2 = 0x40950; // 1 + count of pages
                charsel_gfx[charsel_offset_2] = (byte)(1 + characterSelectParam_vanilla.MaxPage());
                string charsel_updated_path = Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "ui", "flash", "OTHER", "charsel", "charsel.gfx");
                File.WriteAllBytes(charsel_updated_path, charsel_gfx);

                // Process Default Icons
                DirectoryInfo default_icons = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "DefaultIcons"));
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



                // Copy resource files
                string resourcesModManagerPath = Path.Combine(root_folder, "resources_modmanager");
                if (!Directory.Exists(resourcesModManagerPath))
                {
                    Directory.CreateDirectory(resourcesModManagerPath);
                }

                string ResourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "Resources");
                if (Directory.Exists(ResourcesPath))
                    Program.CopyFilesRecursivelyModManager(ResourcesPath, resourcesModManagerPath);

                // Validate icon file existence and remove missing ones from the list
                for (int i = 0; i < CharselIconNamesList.Count; i++)
                {
                    string iconName = CharselIconNamesList[i] + "_charicon_s.xfbin";
                    string path1 = Path.Combine(root_folder, "cpk_assets", "data", "ui", "flash", "OTHER", "charicon_s", iconName);
                    string path2 = Path.Combine(root_folder, "data_win32_modmanager", "data", "ui", "flash", "OTHER", "charicon_s", iconName);
                    string path3 = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "DefaultIcons", iconName);

                    if (!File.Exists(path1) && !File.Exists(path2) && !File.Exists(path3))
                    {
                        CharselIconNamesList.RemoveAt(i);
                        i--;
                    }
                }

                // Update charicon_s.gfx
                byte[] charicon_s_filebytes = File.ReadAllBytes(chariconGfxPath);
                string charicon_s_updated_path = Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "ui", "flash", "OTHER", "charicon_s", "charicon_s.gfx");

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

                string bgmManagerParamModPath = Path.Combine(root_folder, "moddingapi", "param", "NSC", "bgmManagerParam.xfbin");
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
                            string stageIconPathSC = Path.Combine(StagesToAdd[st].RootPath, "stage_icon_SC.dds");
                            if (File.Exists(stageIconPathSC))
                            {

                                st_img_body = File.ReadAllBytes(stageIconPathSC);
                            }
                        } else
                        {
                            string defaultStageIcon = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_icon.dds");
                            st_img_body = File.ReadAllBytes(defaultStageIcon);
                            string stageIconPathSC = Path.Combine(StagesToAdd[st].RootPath, "stage_icon_SC.dds");
                            if (File.Exists(stageIconPathSC))
                            {

                                st_img_body = File.ReadAllBytes(stageIconPathSC);
                            }
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

                    //byte[] stagesel_image_original = File.ReadAllBytes(stageselImageGfxPath);
                    //byte[] stagesel_image_header = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x00, 0x78);
                    //byte[] stagesel_image_header_add = new byte[0];
                    //byte[] stagesel_image_body1 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x78, 0x126E);
                    //byte[] stagesel_image_body1_add = new byte[0];
                    //byte[] stagesel_image_body2 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x12E6, 0x74B);
                    //byte[] stagesel_image_body2_add = new byte[0];
                    //byte[] stagesel_image_end = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x1A31, 0xC2F);

                    byte[] stagesel_image_original = File.ReadAllBytes(stageselImageGfxPath);
                    byte[] stagesel_image_header = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x00, 0x78);
                    byte[] stagesel_image_header_add = new byte[0];
                    byte[] stagesel_image_body1 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x78, 0x126E);
                    byte[] stagesel_image_body1_add = new byte[0];
                    byte[] stagesel_image_body2 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x12E6, 0x6F0);
                    byte[] stagesel_image_body2_add = new byte[0];
                    byte[] stagesel_image_end = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x19D6, 0xC8B);
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
                    //stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                    //    BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x15, 0, 2);
                    //stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                    //    BitConverter.GetBytes(stage_count - 1 + StagesToAdd.Count), 0x59, 0, 2);
                    //stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                    //    BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0x64, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end,
                        BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xC64, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x06, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 3 + (StagesToAdd.Count * 2)), 0x82, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 2 + (StagesToAdd.Count * 2)), 0x8B, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(0x695 + stagesel_image_body2_add.Length), 0xB7, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(image_count_1 + 4 + (StagesToAdd.Count * 2)), 0xBB, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2,
                        BitConverter.GetBytes(stage_count + StagesToAdd.Count), 0xBD, 0, 2);

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
                    stagesel_gfx_original[0x29E22] = (byte)pageCount;

                    //stagesel_gfx_original[0x291CD] = (stage_count - 2 + StagesToAdd.Count) < 255
                    //    ? (byte)(stage_count - 2 + StagesToAdd.Count)
                    //    : (byte)255;
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
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "specialCondParam.xfbin"), specialCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "partnerSlotParam.xfbin"), partnerSlotParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "susanooCondParam.xfbin"), susanooCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "pairSpSkillManagerParam.xfbin"), pairManagerParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "gudoBallParam.xfbin"), gudoBallParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NSC", "ougiAwakeningParam.xfbin"), ougiAwakeningParam_vanilla);
                specialInteraction_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NSC", "specialInteractionManager.xfbin"));
                conditionprmManager_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NSC", "conditionprmManager.xfbin"));
                guardEffectParam_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NSC", "guardEffectParam.xfbin"));

                // Ensure the destination directory for 5kgyprm exists, then write the file
                //string spcDir = Path.Combine(root_folder, "cpk_assets", "data", "spc");
                //if (!Directory.Exists(spcDir))
                //    Directory.CreateDirectory(spcDir);
                //File.WriteAllBytes(
                //    Path.Combine(spcDir, "5kgyprm.bin.xfbin"),
                //    File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "5kgyprm.bin.xfbin"))
                //);

                // Repack CPK archives
                KyurutoDialogTextLoader("Removing all trash from root folder and packing everything in CPK archives.", 20);
                try
                { // Repack data_win32_modmanager folder
                    string resourcePath = Path.GetFullPath(Path.Combine(root_folder, "resources_modmanager"));
                    if (Directory.Exists(resourcePath) &&
                        Directory.EnumerateFiles(resourcePath, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(resourcePath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "resources_modmanager.cpk"));


                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "resources_modmanager.cpk.info"),
                            new byte[8] { 0x1F, 0, 0, 0, 1, 0, 0, 0 });
                    }

                    // Repack cpk_assets folder
                    string cpkAssetsPath = Path.GetFullPath(Path.Combine(root_folder, "cpk_assets"));
                    if (Directory.Exists(cpkAssetsPath) &&
                        Directory.EnumerateFiles(cpkAssetsPath, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(cpkAssetsPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk"));


                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk.info"),
                            new byte[8] { 0x20, 0, 0, 0, 1, 0, 0, 0 });
                    }

                    // Repack data_win32_modmanager folder
                    string dataWin32Path = Path.GetFullPath(Path.Combine(root_folder, "data_win32_modmanager"));
                    if (Directory.Exists(dataWin32Path) &&
                        Directory.EnumerateFiles(dataWin32Path, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(dataWin32Path, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk"));

                        //CpkBlockEncrypter.EncryptDataBlocksInCpk(Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk"));


                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk.info"),
                            new byte[8] { 0x21, 0, 0, 0, 1, 0, 0, 0 });
                    }

                    // Repack param_modmanager_path folder
                    string paramModmanagerFullPath = Path.GetFullPath(Path.Combine(root_folder, "param_files"));
                    if (Directory.Exists(paramModmanagerFullPath) &&
                        Directory.EnumerateFiles(paramModmanagerFullPath, "*.*", SearchOption.AllDirectories).Any())
                    {
                        RepackHelper.RunRepackProcess(paramModmanagerFullPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk"));
                        File.WriteAllBytes(
                            Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk.info"),
                            new byte[8] { 0x22, 0, 0, 0, 1, 0, 0, 0 });
                    }
                } catch (Exception ex)
                {
                    throw new Exception("Failed to repack CPK: " + ex.Message, ex);
                }

                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "gametitle.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "gametitle.gfx"),
                    true);


                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "xcmn_win_roll1.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "xcmn_win_roll1.gfx"),
                    true);


                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "celshade.tex.xfbin"),
                    Path.Combine(root_folder, "data", "system", "celshade.tex.xfbin"),
                    true);

                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "patchnotes.txt"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "patchnotes.txt"),
                    true);


                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "gauge_p.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gauge_p", "gauge_p.gfx"),
                    true);

                // Clean up temporary directories
                if (Directory.Exists(Path.Combine(root_folder, "cpk_assets")))
                    Directory.Delete(Path.Combine(root_folder, "cpk_assets"), true);
                if (Directory.Exists(Path.Combine(root_folder, "data_win32_modmanager")))
                    Directory.Delete(Path.Combine(root_folder, "data_win32_modmanager"), true);
                if (Directory.Exists(Path.Combine(root_folder, "resources_modmanager")))
                    Directory.Delete(Path.Combine(root_folder, "resources_modmanager"), true);
                if (Directory.Exists(Path.Combine(root_folder, "param_files")))
                    Directory.Delete(Path.Combine(root_folder, "param_files"), true);

                File.Copy(Directory.GetCurrentDirectory() + "\\ParamFiles\\NSC\\freebtl_set.gfx", Properties.Settings.Default.RootGameNSCFolder + "\\data\\ui\\flash\\OTHER\\freebtl_set\\freebtl_set.gfx", true);
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
                //e.Result = ex;
                Debug.WriteLine($"Error: {ex}");
                throw; // Ensure the error is propagated to RunWorkerCompleted
            }
        }
        void bw_CompileModProcess_NS4(string path)
        {
            try
            {
                string root_folder = path;
                CleanGameAssetsNS4(false, false);
                InstallModdingAPI(false, Properties.Settings.Default.RootGameNS4Folder);
                Debug.WriteLine("Starting mod compilation...");
                KyurutoDialogTextLoader("Preparing all files!",
                    20);


                //vanilla files
                string characodePath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "characode.bin.xfbin");
                string duelPlayerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "duelPlayerParam.xfbin");
                string playerSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "playerSettingParam.bin.xfbin");
                string skillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "skillCustomizeParam.xfbin");
                string spSkillCustomizeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "spSkillCustomizeParam.xfbin");
                //string skillIndexSettingParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "skillIndexSettingParam.xfbin");
                //string supportSkillRecoverySpeedParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "supportSkillRecoverySpeedParam.xfbin");
                string privateCameraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "privateCamera.bin.xfbin");
                string characterSelectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "characterSelectParam.xfbin");

                string costumeBreakColorParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "costumeBreakColorParam.xfbin");

                //string costumeParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "costumeParam.bin.xfbin");
                string playerIconPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "player_icon.xfbin");
                string cmnparamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "cmnparam.xfbin");
                string supportActionParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "supportActionParam.xfbin");
                string awakeAuraPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "awakeAura.xfbin");
                string appearanceAnmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "appearanceAnm.xfbin");
                string afterAttachObjectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "afterAttachObject.xfbin");
                string playerDoubleEffectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "playerDoubleEffectParam.xfbin");
                string spTypeSupportParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "spTypeSupportParam.xfbin");
                string costumeBreakParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "costumeBreakParam.xfbin");
                string messageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "message");
                string damageeffPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "damageeff.bin.xfbin");
                string effectprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "effectprm.bin.xfbin");
                string damageprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "damageprm.bin.xfbin");
                string stageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "StageInfo.bin.xfbin");
                string nuccMaterialDx11Path = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "nuccMaterial_dx11.nsh");
                string stageselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "stagesel.gfx");
                string stageselImageGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "stagesel_image.gfx");
                string charselGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "charsel.gfx");
                string chariconGfxPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "charicon_s.gfx");
                string stage_selectPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "select_stage.xfbin");
                string conditionprmPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "conditionprm.bin.xfbin");

                string specialCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "specialCondParam.xfbin");
                string partnerSlotParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "partnerSlotParam.xfbin");
                string susanooCondParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "susanooCondParam.xfbin");
                string specialInteractionPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "specialInteractionManager.xfbin");
                string conditionprmManagerPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "conditionprmManager.xfbin");
                string bgmManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "bgmManagerParam.xfbin");

                //TUJ Only
                string pairSpSkillCombinationParam = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "pairSpSkillCombinationParam.xfbin");
                string pairSpSkillManagerParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "pairSpSkillManagerParam.xfbin");


                string guardEffectParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "guardEffectParam.xfbin");
                string gudoBallParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "gudoBallParam.xfbin");
                string ougiAwakeningParamPath = Path.Combine(Directory.GetCurrentDirectory(), "ModdingAPIFiles", "moddingapi", "param", "NS4", "ougiAwakeningParam.xfbin");



                //vanilla file editors
                CharacodeEditorViewModel characode_vanilla = new CharacodeEditorViewModel();
                characode_vanilla.OpenFile(characodePath);
                DuelPlayerParamEditorViewModel duelPlayerParam_vanilla = new DuelPlayerParamEditorViewModel();
                duelPlayerParam_vanilla.OpenFile(duelPlayerParamPath);
                PlayerSettingParamS4ViewModel playerSettingParam_vanilla = new PlayerSettingParamS4ViewModel();
                playerSettingParam_vanilla.OpenFile(playerSettingParamPath);
                SkillCustomizeParamS4ViewModel skillCustomizeParam_vanilla = new SkillCustomizeParamS4ViewModel();
                skillCustomizeParam_vanilla.OpenFile(skillCustomizeParamPath);
                SpSkillCustomizeParamViewModel spSkillCustomizeParam_vanilla = new SpSkillCustomizeParamViewModel();
                spSkillCustomizeParam_vanilla.OpenFile(spSkillCustomizeParamPath);
                //SkillIndexSettingParamViewModel skillIndexSettingParam_vanilla = new SkillIndexSettingParamViewModel();
                //skillIndexSettingParam_vanilla.OpenFile(skillIndexSettingParamPath);
                //SupportSkillRecoverySpeedParamViewModel supportSkillRecoverySpeedParam_vanilla = new SupportSkillRecoverySpeedParamViewModel();
                //supportSkillRecoverySpeedParam_vanilla.OpenFile(supportSkillRecoverySpeedParamPath);
                PrivateCameraViewModel privateCamera_vanilla = new PrivateCameraViewModel();
                privateCamera_vanilla.OpenFile(privateCameraPath);
                CharacterSelectParamS4ViewModel characterSelectParam_vanilla = new CharacterSelectParamS4ViewModel();
                characterSelectParam_vanilla.OpenFile(characterSelectParamPath);

                CostumeBreakColorParamS4ViewModel costumeBreakColorParam_vanilla = new CostumeBreakColorParamS4ViewModel();
                costumeBreakColorParam_vanilla.OpenFile(costumeBreakColorParamPath);

                //CostumeParamViewModel costumeParam_vanilla = new CostumeParamViewModel();
                //costumeParam_vanilla.OpenFile(costumeParamPath);
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
                MessageInfoS4ViewModel messageInfo_vanilla = new MessageInfoS4ViewModel();
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



                GuardEffectParamViewModel guardEffectParam_vanilla = new GuardEffectParamViewModel();
                guardEffectParam_vanilla.OpenFile(guardEffectParamPath);

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
                byte[] ougiAwakeningParam_vanilla = File.ReadAllBytes(ougiAwakeningParamPath);
                byte[] gudoBallParam_vanilla = File.ReadAllBytes(gudoBallParamPath);

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
                    //string skillIndexSettingParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "skillIndexSettingParam.xfbin");
                    //string supportSkillRecoverySpeedParamModPath = Path.Combine(character_mod.RootPath, "data", "spc", "supportSkillRecoverySpeedParam.xfbin");
                    string privateCameraModPath = Path.Combine(character_mod.RootPath, "data", "spc", "privateCamera.bin.xfbin");

                    //string costumeParamModPath = Path.Combine(character_mod.RootPath, "data", "rpg", "param", "costumeParam.bin.xfbin");
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

                    string specialCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "specialCondParam.xfbin");
                    string partnerSlotParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "partnerSlotParam.xfbin");
                    string susanooCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "susanooCondParam.xfbin");
                    string conditionprmManagerModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "conditionprmManager.xfbin");
                    if (!File.Exists(specialCondParamModPath))
                        specialCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "specialCondParam.xfbin");
                    if (!File.Exists(partnerSlotParamModPath))
                        partnerSlotParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "partnerSlotParam.xfbin");
                    if (!File.Exists(susanooCondParamModPath))
                        susanooCondParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "susanooCondParam.xfbin");
                    if (!File.Exists(conditionprmManagerModPath))
                        conditionprmManagerModPath = Path.Combine(character_mod.RootPath, "moddingapi", "mods", "base_game", "conditionprmManager.xfbin");

                    string guardEffectParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "NSC", "guardEffectParam.xfbin");
                    string ougiAwakeningParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "NSC", "ougiAwakeningParam.xfbin");
                    string gudoBallParamModPath = Path.Combine(character_mod.RootPath, "moddingapi", "param", "NSC", "gudoBallParam.xfbin");

                    string stormVersion = character_mod.GameVersion;
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
                            //!File.Exists(skillIndexSettingParamModPath) ||
                            //!File.Exists(supportSkillRecoverySpeedParamModPath) ||
                            //!File.Exists(privateCameraModPath) ||
                            //!File.Exists(costumeParamModPath) ||
                            !File.Exists(playerIconModPath) ||
                            !File.Exists(cmnparamModPath) ||
                            !File.Exists(characterSelectParamModPath))
                            {
                                MessageBox.Show("Error 1");
                                continue;
                            }
                        } else
                        {
                            if (!File.Exists(duelPlayerParamModPath))
                            {
                                MessageBox.Show("Error 2");
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
                    PlayerSettingParamS4ViewModel playerSettingParamS4_mod = new PlayerSettingParamS4ViewModel();
                    if (File.Exists(playerSettingParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                            psp_entry.DLC_ID = -1;
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
                                        psp_entry.DLC_ID = -1;
                                        playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                                    }
                                }
                                break;
                            case "NS4":
                                playerSettingParamS4_mod.OpenFile(playerSettingParamModPath);

                                foreach (PlayerSettingParamModel psp_entry in playerSettingParamS4_mod.PlayerSettingParamList)
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
                                    if (playerSettingParamS4_mod.PlayerSettingParamList.Count > 0 && File.Exists(characterSelectParamModPath))
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
                                        for (int i = 0; i < playerSettingParamS4_mod.PlayerSettingParamList.Count; i++)
                                        {
                                            PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[i].Clone();
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
                                            psp_entry.DLC_ID = -1;
                                            playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                                        }
                                        IsPspModified = true;
                                    }
                                } else
                                {
                                    for (int i = 0; i < playerSettingParamS4_mod.PlayerSettingParamList.Count; i++)
                                    {
                                        PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[i].Clone();
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
                                        psp_entry.DLC_ID = -1;
                                        playerSettingParam_vanilla.PlayerSettingParamList.Add(psp_entry);
                                    }
                                }
                                break;
                        }

                    }

                    //costumeBreakColorParam file
                    CostumeBreakColorParamViewModel costumeBreakColorParam_mod = new CostumeBreakColorParamViewModel();
                    CostumeBreakColorParamS4ViewModel costumeBreakColorParamS4_mod = new CostumeBreakColorParamS4ViewModel();
                    if (File.Exists(costumeBreakColorParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                break;
                            case "NS4":
                                costumeBreakColorParamS4_mod.OpenFile(costumeBreakColorParamModPath);
                                if (replace_character)
                                {
                                    if (costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count > 0)
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
                                        for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                        {
                                            CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                            costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                            costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                        }
                                    }
                                } else
                                {
                                    //Add new entries
                                    for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                    {
                                        CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                        costumeColor_entry.PlayerSettingParamID = AddedPresetIds[i];
                                        costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                    }
                                }
                                break;
                        }

                    }
                   

                    //skillCustomizeParam file
                    SkillCustomizeParamViewModel skillCustomizeParam_mod = new SkillCustomizeParamViewModel();
                    SkillCustomizeParamS4ViewModel skillCustomizeParamS4_mod = new SkillCustomizeParamS4ViewModel();
                    if (File.Exists(skillCustomizeParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
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
                                break;
                            case "NS4":
                                skillCustomizeParamS4_mod.OpenFile(skillCustomizeParamModPath);
                                if (replace_character)
                                {
                                    for (int i = 0; i < skillCustomizeParam_vanilla.SkillCustomizeParamList.Count; i++)
                                    {
                                        if (skillCustomizeParam_vanilla.SkillCustomizeParamList[i].CharacodeID == mod_characodeID)
                                        {
                                            skillCustomizeParam_vanilla.SkillCustomizeParamList[i] = skillCustomizeParamS4_mod.SkillCustomizeParamList[0];
                                            break;
                                        }
                                    }
                                } else
                                {
                                    SkillCustomizeParamModel skillEntry = (SkillCustomizeParamModel)skillCustomizeParamS4_mod.SkillCustomizeParamList[0].Clone();
                                    skillEntry.CharacodeID = mod_characodeID;
                                    skillCustomizeParam_vanilla.SkillCustomizeParamList.Add(skillEntry);
                                }
                                break;
                        }

                    }

                    //spSkillCustomizeParam file
                    SpSkillCustomizeParamViewModel spSkillCustomizeParam_mod = new SpSkillCustomizeParamViewModel();
                    if (File.Exists(spSkillCustomizeParamModPath))
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

                    //playerIcon file
                    PlayerIconViewModel playerIcon_mod = new PlayerIconViewModel();
                    if (File.Exists(playerIconModPath))
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
                    CharacterSelectParamS4ViewModel characterSelectParamS4_mod = new CharacterSelectParamS4ViewModel();
                    if (File.Exists(characterSelectParamModPath))
                    {
                        int page = -1;
                        int slot = -1;
                        switch (stormVersion)
                        {

                            case "NSC":
                                characterSelectParam_mod.OpenFile(characterSelectParamModPath);


                                if (replace_character)
                                {
                                    if (!character_mod.EnableRosterChangeNS4)
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
                                            csp_entry.CostumeIndex = i+1;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig_NS4(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                page = characterSelectParam_vanilla.MaxPage();
                                                slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                                if (slot == 28)
                                                {
                                                    page++;
                                                    slot = 1;
                                                }
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i+1;
                                            }

                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                } else
                                {

                                    if (!character_mod.EnableRosterChangeNS4)
                                    {
                                        page = characterSelectParam_vanilla.MaxPage();
                                        slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                        if (slot == 28)
                                        {
                                            page++;
                                            slot = 1;
                                        }
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i + 1;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        for (int i = 0; i < characterSelectParam_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParam_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;

                                            if (TryReadCSPConfig_NS4(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                page = characterSelectParam_vanilla.MaxPage();
                                                slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                                if (slot == 28)
                                                {
                                                    page++;
                                                    slot = 1;
                                                }
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i + 1;
                                            }


                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            csp_entry.SaveInFile = true;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }




                                }
                                break;
                            case "NS4":
                                characterSelectParamS4_mod.OpenFile(characterSelectParamModPath);

                                if (replace_character)
                                {
                                    if (!character_mod.EnableRosterChangeNS4)
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

                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i + 1;
                                            csp_entry.SaveInFile = true;
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;

                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    } else
                                    {
                                        page = characterSelectParam_vanilla.MaxPage();
                                        slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig_NS4(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                if (slot == 28)
                                                {
                                                    page++;
                                                    slot = 1;
                                                }
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i + 1;
                                            }

                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            csp_entry.SaveInFile = true;
                                            //Debug.WriteLine($"{csp_entry.CSP_code} was replaced S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;

                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                } else
                                {
                                    if (!character_mod.EnableRosterChangeNS4)
                                    {
                                        page = character_mod.Page;
                                        slot = character_mod.Slot;
                                        if (character_mod.Page == -1)
                                        {
                                            page = characterSelectParam_vanilla.MaxPage();
                                            slot = characterSelectParam_vanilla.FreeSlotOnPage(page);
                                            if (slot == 28)
                                            {
                                                page++;
                                                slot = 1;
                                            }
                                        }
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();
                                            csp_entry.PageIndex = page;
                                            csp_entry.SlotIndex = slot;
                                            csp_entry.CostumeIndex = i;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            Debug.WriteLine($"{csp_entry.CSP_code} was added S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            csp_entry.SaveInFile = true;
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;
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
                                            if (slot == 28)
                                            {
                                                page++;
                                                slot = 1;
                                            }
                                        }
                                        for (int i = 0; i < characterSelectParamS4_mod.CharacterSelectParamList.Count; i++)
                                        {
                                            CharacterSelectParamModel csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[i].Clone();

                                            int cfgPage = -1;
                                            int cfgSlot = -1;
                                            int cfgCostume = -1;
                                            if (TryReadCSPConfig_NS4(character_mod.RootPath, csp_entry.CSP_code, out int pRead, out int sRead, out int cRead))
                                            {
                                                cfgPage = pRead;
                                                cfgSlot = sRead;
                                                cfgCostume = cRead;
                                            }
                                            if (cfgPage == -1)
                                            {
                                                cfgPage = page;
                                                cfgSlot = slot;
                                                cfgCostume = i+1;
                                            }
                                            csp_entry.PageIndex = cfgPage;
                                            csp_entry.SlotIndex = cfgSlot;
                                            csp_entry.CostumeIndex = cfgCostume;
                                            if (csp_code_replace.ContainsKey(csp_entry.CSP_code))
                                            {
                                                csp_entry.CSP_code = csp_code_replace[csp_entry.CSP_code];
                                            }
                                            Debug.WriteLine($"{csp_entry.CSP_code} was added S4!");
                                            csp_entry.DictionaryCode = "";
                                            csp_entry.DictionaryIndex = -1;
                                            csp_entry.Unk = 1;
                                            csp_entry.CostumeName = "practice_normal";
                                            csp_entry.SaveInFile = true;
                                            csp_entry.CharselValues.P1_customization_pos_x = (float)-76.1235122680664;
                                            csp_entry.CharselValues.P1_customization_pos_y = (float)73.89142608642578;
                                            csp_entry.CharselValues.P1_customization_pos_z = (float)-323.99603271484375;
                                            csp_entry.CharselValues.P1_customization_rot = (float)14.025724411010742;
                                            csp_entry.CharselValues.P1_customization_light_x = (float)18.649999618530273;
                                            csp_entry.CharselValues.P1_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P1_customization_light_z = (float)0.38999998569488525;
                                            csp_entry.CharselValues.P2_customization_pos_x = (float)76.17376708984375;
                                            csp_entry.CharselValues.P2_customization_pos_y = (float)360.3885498046875;
                                            csp_entry.CharselValues.P2_customization_pos_z = (float)-285.6630859375;
                                            csp_entry.CharselValues.P2_customization_rot = (float)345.3846130371094;
                                            csp_entry.CharselValues.P2_customization_light_x = (float)11.158173561096191;
                                            csp_entry.CharselValues.P2_customization_light_y = (float)68.86000061035156;
                                            csp_entry.CharselValues.P2_customization_light_z = (float)-16.35211753845215;
                                            characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                        }
                                    }
                                }
                                break;
                        }
                    }


                    //supportActionParam file
                    SupportActionParamViewModel supportActionParam_mod = new SupportActionParamViewModel();
                    if (File.Exists(supportActionParamModPath))
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
                    if (File.Exists(costumeBreakParamModPath))
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
                    if (File.Exists(awakeAuraModPath))
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
                    if (File.Exists(appearanceAnmModPath))
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
                    if (File.Exists(afterAttachObjectModPath))
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
                    if (File.Exists(playerDoubleEffectParamModPath))
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
                    if (File.Exists(spTypeSupportParamModPath))
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
                    if (File.Exists(specialCondParamModPath))
                    {
                        specialCondParam_mod = File.ReadAllBytes(specialCondParamModPath);
                        specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        specialCondParam_mod = BinaryReader.b_ReplaceBytes(specialCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        specialCondParam_vanilla = BinaryReader.b_AddBytes(specialCondParam_vanilla, specialCondParam_mod);
                    }

                    //specialCondParam file
                    byte[] partnerSlotParam_mod = new byte[0];
                    if (File.Exists(partnerSlotParamModPath))
                    {
                        partnerSlotParam_mod = File.ReadAllBytes(partnerSlotParamModPath);
                        partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        partnerSlotParam_mod = BinaryReader.b_ReplaceBytes(partnerSlotParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        partnerSlotParam_vanilla = BinaryReader.b_AddBytes(partnerSlotParam_vanilla, partnerSlotParam_mod);
                    }

                    //susanooCondParam file
                    byte[] susanooCondParam_mod = new byte[0];
                    if (File.Exists(susanooCondParamModPath))
                    {
                        susanooCondParam_mod = File.ReadAllBytes(susanooCondParamModPath);
                        susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, new byte[4] { 0, 0, 0, 0 }, 0x17);
                        susanooCondParam_mod = BinaryReader.b_ReplaceBytes(susanooCondParam_mod, BitConverter.GetBytes(mod_characodeID), 0x18);
                        susanooCondParam_vanilla = BinaryReader.b_AddBytes(susanooCondParam_vanilla, susanooCondParam_mod);
                    }

                    //guardEffectParam file
                    GuardEffectParamViewModel guardEffectParam_mod = new GuardEffectParamViewModel();
                    if (File.Exists(guardEffectParamModPath))
                    {
                        guardEffectParam_mod.OpenFile(guardEffectParamModPath);
                        for (int i = 0; i < guardEffectParam_mod.GuardEffectParamList.Count; i++)
                        {

                            GuardEffectParamModel guardEffectParamEntry = (GuardEffectParamModel)guardEffectParam_mod.GuardEffectParamList[i].Clone();
                            guardEffectParamEntry.CharacodeID = mod_characodeID;
                            guardEffectParam_vanilla.GuardEffectParamList.Add(guardEffectParamEntry);
                        }
                    }
                    //ougiAwakeningParam file
                    byte[] ougiAwakeningParam_mod = new byte[0];
                    if (File.Exists(ougiAwakeningParamModPath))
                    {
                        ougiAwakeningParam_mod = File.ReadAllBytes(ougiAwakeningParamModPath);
                        ougiAwakeningParam_mod = BinaryReader.b_ReplaceBytes(ougiAwakeningParam_mod, BitConverter.GetBytes(mod_characodeID), 0, 0, 4);
                        ougiAwakeningParam_vanilla = BinaryReader.b_AddBytes(ougiAwakeningParam_vanilla, ougiAwakeningParam_mod);
                    }

                    byte[] gudoBallParam_mod = new byte[0];
                    if (File.Exists(gudoBallParamModPath))
                    {
                        gudoBallParam_mod = File.ReadAllBytes(gudoBallParamModPath);
                        gudoBallParam_mod = BinaryReader.b_ReplaceBytes(gudoBallParam_mod, BitConverter.GetBytes(mod_characodeID), 0, 0, 4);
                        gudoBallParam_vanilla = BinaryReader.b_AddBytes(gudoBallParam_vanilla, gudoBallParam_mod);
                    }

                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfoS4_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfoS4_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langS4List.Length > l ? Program.langS4List[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langList, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langList, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langList, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfo_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfo_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }
                    //damageprm file
                    DamagePrmViewModel damageprm_mod = new DamagePrmViewModel();
                    if (File.Exists(damageprmModPath))
                    {
                        damageprm_mod.OpenFile(damageprmModPath);
                        for (int i = 0; i < damageprm_mod.DamagePrmList.Count; i++)
                        {
                            var entry = (DamagePrmModel)damageprm_mod.DamagePrmList[i].Clone();

                            // если мод для Storm 4 — читаем int32 по смещению 0x6C, ищем соответствие в списках и заменяем
                            if (stormVersion == "NSC" && entry.Data != null && entry.Data.Length >= 0x6C + 4)
                            {
                                int oldIndex = BitConverter.ToInt32(entry.Data, 0x6C);
                                if (oldIndex >= 0 && Program.CONDITION_NSC_LIST != null && oldIndex < Program.CONDITION_NSC_LIST.Length)
                                {
                                    string cond = Program.CONDITION_NSC_LIST[oldIndex];
                                    if (!string.IsNullOrEmpty(cond) && Program.CONDITION_NS4_LIST != null)
                                    {
                                        int newIndex = Array.IndexOf(Program.CONDITION_NS4_LIST, cond);
                                        //Debug.WriteLine($"cond = {cond}, old index = {oldIndex}, new index = {newIndex}");
                                        if (newIndex >= 0)
                                        {
                                            byte[] newBytes = BitConverter.GetBytes(newIndex);
                                            Array.Copy(newBytes, 0, entry.Data, 0x6C, 4);

                                        }
                                        // если newIndex < 0 — соответствия нет, оставляем старый индекс
                                    }
                                }
                                // если oldIndex вне диапазона — не трогаем
                            }

                            damageprm_vanilla.DamagePrmList.Add(entry);
                        }
                    }

                    //prm
                    PRMEditorViewModel prm_mod = new PRMEditorViewModel();

                    var modDir = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(character_mod.RootPath)));
                    var prmFiles = modDir
                        .GetFiles($"{mod_characode}prm.bin.xfbin", SearchOption.AllDirectories)
                        .OrderBy(f => f.DirectoryName, StringComparer.OrdinalIgnoreCase)
                        .ToArray();

                    if (prmFiles.Length > 0)
                    {
                        string prm_path = prmFiles.Last().FullName;
                        string relative = prm_path.Substring(prm_path.IndexOf("data\\", StringComparison.OrdinalIgnoreCase));
                        string new_prm_path = Path.Combine(root_folder, "param_files", relative);

                        // Только если оба файла существуют, выполняем merge
                        if (File.Exists(prm_path) && File.Exists(damageeffModPath))
                        {
                            // load mod and vanilla view‑models
                            var damageeff_mod = new DamageEffViewModel(); damageeff_mod.OpenFile(damageeffModPath);
                            var effectprm_mod = new EffectPrmViewModel();

                            var effectIdMap = new Dictionary<int, int>();
                            if (File.Exists(effectprmModPath))
                            {
                                effectprm_mod.OpenFile(effectprmModPath);
                                foreach (var modEntry in effectprm_mod.EffectPrmList)
                                {
                                    int newId = effectprm_vanilla.MaxEffectID() + 1;
                                    effectIdMap[modEntry.EffectPrmID] = newId;
                                    Debug.WriteLine($"Effect Entry, old id = {modEntry.EffectPrmID}, new id = {newId}");
                                    modEntry.EffectPrmID = newId;
                                    effectprm_vanilla.EffectPrmList.Add((EffectPrmModel)modEntry.Clone());
                                }
                            }

                            // remap EffectPrmID in damageEff_mod and build hit‑ID map
                            var hitIdMap = new Dictionary<int, int>();
                            foreach (var de in damageeff_mod.DamageEffList)
                            {
                                if (effectIdMap.TryGetValue(de.EffectPrmID, out var mapped))
                                {
                                    de.EffectPrmID = mapped;
                                    de.ExtraEffectPrmID = 0;
                                }
                                int newHit = damageeff_vanilla.MaxDamageID() + 1;
                                hitIdMap[de.DamageEffID] = newHit;

                                var clone = (DamageEffModel)de.Clone();
                                Debug.WriteLine($"Damage Entry, old id = {clone.DamageEffID}, new id = {newHit}");
                                clone.DamageEffID = newHit;
                                if (hitIdMap.TryGetValue(clone.ExtraDamageEffID, out var extra))
                                    clone.ExtraDamageEffID = extra;

                                damageeff_vanilla.DamageEffList.Add(clone);
                            }

                            // open and correct prm
                            prm_mod.OpenFile(prm_path);
                            foreach (var ver in prm_mod.VerList)
                                foreach (var sec in ver.PL_ANM_Sections)
                                    foreach (var fn in sec.FunctionList)
                                    {
                                        if (hitIdMap.TryGetValue(fn.DamageHitEffectID, out var nid))
                                            fn.DamageHitEffectID = (short)nid;

                                        if (fn.FunctionID > 0x120)
                                        {
                                            fn.FunctionID = 0x10E;
                                        }

                                    }

                            // save result
                            Directory.CreateDirectory(Path.GetDirectoryName(new_prm_path)!);
                            prm_mod.SaveFileAs(new_prm_path);
                        }
                    }




                }
                //Compile Stage Mods

                foreach (StageModModel stage_mod in StageList)
                {

                    string stormVersion = stage_mod.GameVersion;
                    string messageInfoModPath = Path.Combine(stage_mod.RootPath, "data", "message");
                    string stageInfoModPath = Path.Combine(stage_mod.RootPath, "data", "stage", "StageInfo.bin.xfbin");

                    string mod_stagename = stage_mod.StageName;
                    int mod_stageID = -1;
                    int BGM_ID = Convert.ToInt32(stage_mod.BgmID_NS4);
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
                            "STAGE_SD62B",
                            "STAGE_SD62A",
                            "STAGE_SD01B",
                            "STAGE_SD03B",
                            "STAGE_SD03E",
                            "STAGE_SD12A",
                            "STAGE_SI00A",
                            "STAGE_SD01D",
                            "STAGE_SI06A",
                            "STAGE_SD14A",
                            "STAGE_SI01A",
                            "STAGE_SD06A",
                            "STAGE_SD07A",
                            "STAGE_SD07B",
                            "STAGE_SD10A",
                            "STAGE_SI09A_NR",
                            "STAGE_SI08A",
                            "STAGE_SD13A",
                            "STAGE_SD15A_NOSNOW",
                            "STAGE_SD17A",
                            "STAGE_SD16A",
                            "STAGE_SD22A",
                            "STAGE_SD25A",
                            "STAGE_SD23A",
                            "STAGE_SD21A",
                            "STAGE_SD19A",
                            "STAGE_SD33A",
                            "STAGE_SD05D",
                            "STAGE_SD04B",
                            "STAGE_SI43A",
                            "STAGE_SI35A",
                            "STAGE_SI33A",
                            "STAGE_SI42B",
                            "STAGE_SI42A",
                            "STAGE_SI44A",
                            "STAGE_SI45A",
                            "STAGE_SI50E",
                            "STAGE_SD60A",
                            "STAGE_SD05B",
                            "STAGE_SI51C",
                            "STAGE_SD70B",
                            "STAGE_SI70A",
                            "STAGE_SI71A",
                            "STAGE_0_MAID_IN_HEAVEN",
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
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfoS4_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfoS4_mod.MessageInfo_List[l][i].SecondaryText = messageInfoS4_mod.MessageInfo_List[l][i].MainText;
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfoS4_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langS4List.Length > l ? Program.langS4List[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langList, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langList, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langList, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfo_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_mod.MessageInfo_List[srcIndex][i].SecondaryText = messageInfo_mod.MessageInfo_List[srcIndex][i].MainText;
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfo_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }


                }
                //Compile Model mods
                foreach (CostumeModModel costume_mod in CostumeList)
                {
                    string mod_characode = costume_mod.Characode;
                    string stormVersion = costume_mod.GameVersion;
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
                    int csp_code_index = 0;

                    PlayerSettingParamViewModel playerSettingParam_mod = new PlayerSettingParamViewModel();

                    PlayerSettingParamS4ViewModel playerSettingParamS4_mod = new PlayerSettingParamS4ViewModel();
                    if (File.Exists(playerSettingParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
                                playerSettingParam_mod.OpenFile(playerSettingParamModPath);
                                PlayerSettingParamModel psp_entry = (PlayerSettingParamModel)playerSettingParam_mod.PlayerSettingParamList[0].Clone();
                                costume_csp_code = psp_entry.PSP_code;
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
                                break;
                            case "NS4":
                                playerSettingParamS4_mod.OpenFile(playerSettingParamModPath);
                                psp_entry = (PlayerSettingParamModel)playerSettingParamS4_mod.PlayerSettingParamList[0].Clone();
                                costume_csp_code = psp_entry.PSP_code;
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
                                break;
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
                    CostumeBreakColorParamS4ViewModel costumeBreakColorParamS4_mod = new CostumeBreakColorParamS4ViewModel();
                    if (File.Exists(costumeBreakColorParamModPath))
                    {
                        switch (stormVersion)
                        {

                            case "NSC":
                                costumeBreakColorParam_mod.OpenFile(costumeBreakColorParamModPath);
                                //Add new entries
                                for (int i = 0; i < costumeBreakColorParam_mod.CostumeBreakColorParamList.Count; i++)
                                {
                                    CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParam_mod.CostumeBreakColorParamList[i].Clone();
                                    costumeColor_entry.PlayerSettingParamID = new_preset_id;
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                }
                                break;
                            case "NS4":
                                costumeBreakColorParamS4_mod.OpenFile(costumeBreakColorParamModPath);
                                //Add new entries
                                for (int i = 0; i < costumeBreakColorParamS4_mod.CostumeBreakColorParamList.Count; i++)
                                {
                                    CostumeBreakColorParamModel costumeColor_entry = (CostumeBreakColorParamModel)costumeBreakColorParamS4_mod.CostumeBreakColorParamList[i].Clone();
                                    costumeColor_entry.PlayerSettingParamID = new_preset_id;
                                    costumeBreakColorParam_vanilla.CostumeBreakColorParamList.Add(costumeColor_entry);
                                }
                                break;
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
                    CharacterSelectParamS4ViewModel characterSelectParamS4_mod = new CharacterSelectParamS4ViewModel();
                    if (File.Exists(characterSelectParamModPath))
                    {
                        int page = 1;
                        int slot = 1;
                        int costume = 1;
                        switch (stormVersion)
                        {

                            case "NSC":
                                characterSelectParam_mod.OpenFile(characterSelectParamModPath);
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

                                int new_page = -1;
                                int new_slot = -1;
                                int new_costume = -1;

                                TryReadCostumeCSPConfig_NS4(costume_mod.RootPath, out new_page, out new_slot, out new_costume);
                                if (costume_mod.EnableRosterChangeNS4 && new_page != -1)
                                {

                                    csp_entry.PageIndex = new_page;
                                    csp_entry.SlotIndex = new_slot;
                                    csp_entry.CostumeIndex = new_costume;
                                } else
                                {

                                    csp_entry.PageIndex = page;
                                    csp_entry.SlotIndex = slot;
                                    csp_entry.CostumeIndex = costume;
                                }
                                csp_entry.CSP_code = costume_csp_code;
                                characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                break;
                            case "NS4":
                                characterSelectParamS4_mod.OpenFile(characterSelectParamModPath);
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

                                csp_entry = (CharacterSelectParamModel)characterSelectParamS4_mod.CharacterSelectParamList[0].Clone();

                                int new_page_s4 = -1;
                                int new_slot_s4 = -1;
                                int new_costume_s4 = -1;

                                TryReadCostumeCSPConfig_NS4(costume_mod.RootPath, out new_page_s4, out new_slot_s4, out new_costume_s4);
                                if (costume_mod.EnableRosterChangeNS4 && new_page_s4 != -1)
                                {

                                    csp_entry.PageIndex = new_page_s4;
                                    csp_entry.SlotIndex = new_slot_s4;
                                    csp_entry.CostumeIndex = new_costume_s4;
                                } else
                                {

                                    csp_entry.PageIndex = page;
                                    csp_entry.SlotIndex = slot;
                                    csp_entry.CostumeIndex = costume;
                                }
                                csp_entry.CSP_code = costume_csp_code;
                                characterSelectParam_vanilla.CharacterSelectParamList.Add(csp_entry);
                                break;
                        }



                    }

                    //messageInfo files
                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfoS4_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfoS4_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langS4List.Length > l ? Program.langS4List[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langList, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langList, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langList, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfo_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfo_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
                    }
                }
                List<string> skippedLabels = new List<string>();
                //Compile Team Ultimate Jutsu Mods
                foreach (TeamUltimateJutsuModModel tuj_mod in TUJList)
                {
                    string cmnparamModPath = Path.Combine(tuj_mod.RootPath, "data", "sound", "cmnparam.xfbin");
                    string messageInfoModPath = Path.Combine(tuj_mod.RootPath, "data", "message");

                    string stormVersion = tuj_mod.GameVersion;



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

                    List<int> SkipEntriesList = new List<int> {  };
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
                    //messageInfo files
                    MessageInfoViewModel messageInfo_mod = new MessageInfoViewModel();
                    MessageInfoS4ViewModel messageInfoS4_mod = new MessageInfoS4ViewModel();
                    if (Directory.Exists(messageInfoModPath))
                    {
                        switch (stormVersion)
                        {
                            case "NS4":
                                messageInfoS4_mod.OpenFiles(messageInfoModPath);
                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    if (l >= messageInfoS4_mod.MessageInfo_List.Count) continue; // защита от выхода за пределы
                                    for (int i = 0; i < messageInfoS4_mod.MessageInfo_List[l].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add((MessageInfoModel)messageInfoS4_mod.MessageInfo_List[l][i].Clone());
                                    }
                                }
                                messageInfoModified = true;
                                break;

                            case "NSC":
                                messageInfo_mod.OpenFiles(messageInfoModPath);

                                for (int l = 0; l < messageInfo_vanilla.MessageInfo_List.Count; l++)
                                {
                                    // целевой язык (в порядке langList)
                                    string targetLang = Program.langS4List.Length > l ? Program.langS4List[l] : null;
                                    if (string.IsNullOrEmpty(targetLang)) continue;

                                    // найдем соответствующий индекс в списке NS4 языков
                                    int srcIndex = Array.IndexOf(Program.langList, targetLang);

                                    // если не найден — использовать fallback: для zhcn брать chi, иначе eng
                                    if (srcIndex < 0)
                                    {
                                        if (targetLang == "zhcn")
                                            srcIndex = Array.IndexOf(Program.langList, "chi");
                                        else
                                            srcIndex = Array.IndexOf(Program.langList, "eng");
                                    }

                                    // защита от неверных индексов
                                    if (srcIndex < 0 || srcIndex >= messageInfo_mod.MessageInfo_List.Count) continue;

                                    for (int i = 0; i < messageInfo_mod.MessageInfo_List[srcIndex].Count; i++)
                                    {
                                        messageInfo_vanilla.MessageInfo_List[l].Add(
                                            (MessageInfoModel)messageInfo_mod.MessageInfo_List[srcIndex][i].Clone()
                                        );
                                    }
                                }

                                messageInfoModified = true;
                                break;
                        }
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
                int charsel_offset_2 = 0x33DCB; // 1 + count of pages
                charsel_gfx[charsel_offset_2] = (byte)(characterSelectParam_vanilla.MaxPage());
                string charsel_updated_path = Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "ui", "flash", "OTHER", "charsel", "charsel.gfx");
                File.WriteAllBytes(charsel_updated_path, charsel_gfx);
                // Copy resource files
                string resourcesModManagerPath = Path.Combine(root_folder, "resources_modmanager");
                if (!Directory.Exists(resourcesModManagerPath))
                {
                    Directory.CreateDirectory(resourcesModManagerPath);
                }

                string ResourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "Resources");
                if (Directory.Exists(ResourcesPath))
                    Program.CopyFilesRecursivelyModManager(ResourcesPath, resourcesModManagerPath);
                // Process Default Icons
                DirectoryInfo default_icons = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "DefaultIcons"));
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
                    string path3 = Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "DefaultIcons", iconName);

                    if (!File.Exists(path1) && !File.Exists(path2) && !File.Exists(path3))
                    {
                        CharselIconNamesList.RemoveAt(i);
                        i--;
                    }
                }

                // Update charicon_s.gfx
                byte[] charicon_s_filebytes = File.ReadAllBytes(chariconGfxPath);
                string charicon_s_updated_path = Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "ui", "flash", "OTHER", "charicon_s", "charicon_s.gfx");

                byte[] charicon_s_header = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0, 0xAB);
                byte[] charicon_s_body1 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0xAB, 0x3669);
                byte[] charicon_s_body2 = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x3714, 0xF20);
                byte[] charicon_s_end = BinaryReader.b_ReadByteArray(charicon_s_filebytes, 0x4634, 0xA372); //0x08,0x15,0x7D,0xA2C4 - change counts!
                byte[] charicon_s_newFile = new byte[0];
                int icon_count = 0x187;
                int icon_count2 = 0xC0;
                int external_image_count = 4;
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
                charicon_s_end = BinaryReader.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(icon_count + (CharselIconNamesList.Count * 2)), 0xA2C4, 0, 2);
                charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_header);
                charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_body1);
                charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_body2);
                charicon_s_newFile = BinaryReader.b_AddBytes(charicon_s_newFile, charicon_s_end);
                charicon_s_newFile = BinaryReader.b_ReplaceBytes(charicon_s_newFile, BitConverter.GetBytes((int)charicon_s_newFile.Length), 0x04, 0);
                File.WriteAllBytes(charicon_s_updated_path, charicon_s_newFile);


                // Create directories using Path.Combine
                Directory.CreateDirectory(param_modmanager_path);
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "spc", "WIN64"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "rpg", "param", "WIN64"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "ui", "max", "select", "WIN64"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "stage", "WIN64"));
                Directory.CreateDirectory(Path.Combine(param_modmanager_path, "data", "sound"));

                string bgmManagerParamModPath = Path.Combine(root_folder, "moddingapi", "param", "NS4", "bgmManagerParam.xfbin");
                if (stageInfoModified)
                {
                    // select_stage
                    int stage_count = 44;
                    byte[] stageSel_file = File.ReadAllBytes(stage_selectPath);
                    byte[] stagesel_header = BinaryReader.b_ReadByteArray(stageSel_file, 0, 0x144);
                    byte[] stagesel_body = BinaryReader.b_ReadByteArray(stageSel_file, 0x144, 0xBEB);
                    byte[] stagesel_end = BinaryReader.b_ReadByteArray(stageSel_file, 0xD39, 0x14);
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

                            string stageIconPathS4 = Path.Combine(StagesToAdd[st].RootPath, "stage_icon_S4.dds");
                            if (File.Exists(stageIconPathS4))
                            {

                                st_img_body = File.ReadAllBytes(stageIconPathS4);
                            }
                        } else
                        {
                            string defaultStageIcon = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_icon.dds");
                            st_img_body = File.ReadAllBytes(defaultStageIcon);

                            string stageIconPathS4 = Path.Combine(StagesToAdd[st].RootPath, "stage_icon_S4.dds");
                            if (File.Exists(stageIconPathS4))
                            {

                                st_img_body = File.ReadAllBytes(stageIconPathS4);
                            }
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
                        BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length), 0x140, 1);
                    stagesel_new_file = BinaryReader.b_ReplaceBytes(stagesel_new_file,
                        BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length + 4), 0x134, 1);
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
                    byte[] stagesel_image_header = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x00, 0x55);
                    byte[] stagesel_image_header_add = new byte[0];
                    byte[] stagesel_image_body1 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x55, 0xC0D);
                    byte[] stagesel_image_body1_add = new byte[0];
                    byte[] stagesel_image_body2 = BinaryReader.b_ReadByteArray(stagesel_image_original, 0xC62, 0x45D);
                    byte[] stagesel_image_body2_add = new byte[0];
                    byte[] stagesel_image_end = BinaryReader.b_ReadByteArray(stagesel_image_original, 0x10BF, 0x8DF);
                    byte[] stagesel_image_new_file = new byte[0];
                    for (int st = 0; st < StagesToAdd.Count; st++)
                    {
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[2] { (byte)(0x4C + ("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length), 0xFC });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, BitConverter.GetBytes(st + 1), 0, 0, 2);
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[] { 0x09, 0x00, 0x0E, 0x00, 0xa8, 0x00, 0x4c, 0x00, 0x00 });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, new byte[1] { (byte)("stagesel_image_" + StagesToAdd[st].StageName + ".dds").Length });
                        stagesel_image_header_add = BinaryReader.b_AddBytes(stagesel_image_header_add, Encoding.ASCII.GetBytes("stagesel_image_" + StagesToAdd[st].StageName + ".dds"));
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[2] { 0x0C, 0xFC });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x55 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(st + 1), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x0E] { 0x00, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x4C, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00 });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x56 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x13] { 0x64, 0xC2, 0x33, 0xE6, 0x84, 0x17, 0xC0, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41 });
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x55 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = BinaryReader.b_AddBytes(stagesel_image_body1_add, new byte[0x1C] { 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0x98, 0x4D, 0x08, 0x00, 0x20, 0x15, 0x93, 0x09, 0xA1, 0x17, 0x63, 0x3E, 0x3A, 0x57, 0xC3, 0xB2, 0x61, 0x1D, 0x34, 0x20, 0x00 });
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0C] { 0xFF, 0x0A, (byte)(("img_s_" + (43 + st).ToString()).Length + 1), 0x00, 0x00, 0x00, 0x69, 0x6D, 0x67, 0x5F, 0x73, 0x5F });
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, Encoding.ASCII.GetBytes((43 + st).ToString()));
                        stagesel_image_body2_add = BinaryReader.b_AddBytes(stagesel_image_body2_add, new byte[0x0A] { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00, (byte)(0x56 + ((st + 1) * 2)), 0x00, 0x40, 0x00 });
                    }
                    //stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x57 + (StagesToAdd.Count * 2)), 0x11, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x2B + StagesToAdd.Count), 0x48, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x58 + (StagesToAdd.Count * 2)), 0x53, 0, 2);
                    stagesel_image_end = BinaryReader.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x58 + (StagesToAdd.Count * 2)), 0x8B8, 0, 2);

                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x45B + stagesel_image_body2_add.Length), 0x4C, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x58 + (StagesToAdd.Count * 2)), 0x50, 0, 2);
                    stagesel_image_body2 = BinaryReader.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x2C + StagesToAdd.Count), 0x52, 0, 2);

                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_header_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body1_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_body2_add);
                    stagesel_image_new_file = BinaryReader.b_AddBytes(stagesel_image_new_file, stagesel_image_end);
                    stagesel_image_new_file = BinaryReader.b_ReplaceBytes(stagesel_image_new_file, BitConverter.GetBytes(stagesel_image_new_file.Length), 0x04);
                    string stageselImageOutputPath = Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "stagesel", "stagesel_image.gfx");
                    File.WriteAllBytes(stageselImageOutputPath, stagesel_image_new_file);

                    // stagesel.gfx
                    int pageCount = (40 + StagesToAdd.Count) / 40;
                    byte[] stagesel_gfx_original = File.ReadAllBytes(stageselGfxPath);
                    if (40 * pageCount != 40 + StagesToAdd.Count)
                        pageCount++;
                    stagesel_gfx_original[0x15B74] = (byte)pageCount;

                    //stagesel_gfx_original[0x14EA1] = (40 + StagesToAdd.Count) < 255
                    //    ? (byte)(40 + StagesToAdd.Count)
                    //    : (byte)255;
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
                playerSettingParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "playerSettingParam.bin.xfbin"));
                skillCustomizeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "skillCustomizeParam.xfbin"));
                spSkillCustomizeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "spSkillCustomizeParam.xfbin"));
                //skillIndexSettingParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "skillIndexSettingParam.xfbin"));
                //supportSkillRecoverySpeedParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "supportSkillRecoverySpeedParam.xfbin"));
                privateCamera_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "privateCamera.bin.xfbin"));
                characterSelectParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "ui", "max", "select", "WIN64", "characterSelectParam.xfbin"));
                costumeBreakParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "costumeBreakParam.xfbin"));
                costumeBreakColorParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "costumeBreakColorParam.xfbin"));
                //costumeParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "rpg", "param", "costumeParam.bin.xfbin"));
                playerIcon_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "player_icon.xfbin"));
                cmnparam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "sound", "cmnparam.xfbin"));
                supportActionParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "supportActionParam.xfbin"));
                awakeAura_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "awakeAura.xfbin"));
                appearanceAnm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "appearanceAnm.xfbin"));
                afterAttachObject_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "afterAttachObject.xfbin"));
                playerDoubleEffectParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "playerDoubleEffectParam.xfbin"));
                spTypeSupportParam_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "spTypeSupportParam.xfbin"));
                pairSpSkillComb_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "WIN64", "pairSpSkillCombinationParam.xfbin"));
                conditionprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "conditionprm.bin.xfbin"));

                damageeff_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "damageeff.bin.xfbin"));
                effectprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "effectprm.bin.xfbin"));
                damageprm_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "spc", "damageprm.bin.xfbin"));

                if (stageInfoModified)
                {
                    KyurutoDialogTextLoader("Saving your stage mods!", 20);
                    stageInfo_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data", "stage", "WIN64", "StageInfo.bin.xfbin"));
                }
                if (messageInfoModified)
                {
                    KyurutoDialogTextLoader("Making localization!", 20);
                    messageInfo_vanilla.SaveFileAs(Path.Combine(param_modmanager_path, "data"));
                }

                // Write additional modding API files
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "specialCondParam.xfbin"), specialCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "partnerSlotParam.xfbin"), partnerSlotParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "susanooCondParam.xfbin"), susanooCondParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "pairSpSkillManagerParam.xfbin"), pairManagerParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "gudoBallParam.xfbin"), gudoBallParam_vanilla);
                File.WriteAllBytes(Path.Combine(root_folder, "moddingapi", "param", "NS4", "ougiAwakeningParam.xfbin"), ougiAwakeningParam_vanilla);
                specialInteraction_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NS4", "specialInteractionManager.xfbin"));
                conditionprmManager_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NS4", "conditionprmManager.xfbin"));
                guardEffectParam_vanilla.SaveFileAs(Path.Combine(root_folder, "moddingapi", "param", "NS4", "guardEffectParam.xfbin"));

                // Ensure the destination directory for 5kgyprm exists, then write the file
                //string spcDir = Path.Combine(root_folder, "cpk_assets", "data", "spc");
                //if (!Directory.Exists(spcDir))
                //    Directory.CreateDirectory(spcDir);
                //File.WriteAllBytes(
                //    Path.Combine(spcDir, "5kgyprm.bin.xfbin"),
                //    File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "5kgyprm.bin.xfbin"))
                //);




                // Repack CPK archives
                KyurutoDialogTextLoader("Removing all trash from root folder and packing everything in CPK archives.", 20);
                var xfbinMap = RepackXfbinFinder.GetXfbinPathsFromRepackFolders(root_folder);
                int sound_index = 850;
                // Пример: вывести найденные пути в лог или использовать дальше
                foreach (var kv in xfbinMap)
                {
                    // kv.Key — подпапка (например ...\data\sound\PC\folder1)
                    // kv.Value — список путей к .xfbin в этой подпапке
                    foreach (string sound_path in kv.Value)
                    {
                        NUS3BANKViewModel SoundFileMod = new NUS3BANKViewModel();
                        SoundFileMod.OpenFile(sound_path);
                        SoundFileMod.NUS3BANK_FILE.BINF_DATA.FileID = sound_index;
                        SoundFileMod.SaveFile();
                        sound_index++;

                    }
                }

                // Repack resources_modmanager folder
                string resourcesPath = Path.GetFullPath(Path.Combine(root_folder, "resources_modmanager"));
                if (Directory.Exists(resourcesPath) &&
                    Directory.EnumerateFiles(resourcesPath, "*.*", SearchOption.AllDirectories).Any())
                {
                    RepackHelper.RunRepackProcess(resourcesPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "resources_modmanager.cpk"));


                    File.WriteAllBytes(
                        Path.Combine(root_folder, "moddingapi", "mods", "base_game", "resources_modmanager.cpk.info"),
                        new byte[8] { 0x1F, 0, 0, 0, 1, 0, 0, 0 });
                }
                // Repack cpk_assets folder
                string cpkAssetsPath = Path.GetFullPath(Path.Combine(root_folder, "cpk_assets"));
                if (Directory.Exists(cpkAssetsPath) &&
                    Directory.EnumerateFiles(cpkAssetsPath, "*.*", SearchOption.AllDirectories).Any())
                {
                    RepackHelper.RunRepackProcess(cpkAssetsPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk"));


                    File.WriteAllBytes(
                        Path.Combine(root_folder, "moddingapi", "mods", "base_game", "cpk_assets.cpk.info"),
                        new byte[8] { 0x20, 0, 0, 0, 1, 0, 0, 0 });
                }

                // Repack data_win32_modmanager folder
                string dataWin32Path = Path.GetFullPath(Path.Combine(root_folder, "data_win32_modmanager"));
                if (Directory.Exists(dataWin32Path) &&
                    Directory.EnumerateFiles(dataWin32Path, "*.*", SearchOption.AllDirectories).Any())
                {
                    RepackHelper.RunRepackProcess(dataWin32Path, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk"));


                    File.WriteAllBytes(
                        Path.Combine(root_folder, "moddingapi", "mods", "base_game", "data_win32_modmanager.cpk.info"),
                        new byte[8] { 0x21, 0, 0, 0, 1, 0, 0, 0 });
                }

                // Repack param_modmanager_path folder
                string paramModmanagerFullPath = Path.GetFullPath(Path.Combine(root_folder, "param_files"));
                if (Directory.Exists(paramModmanagerFullPath) &&
                    Directory.EnumerateFiles(paramModmanagerFullPath, "*.*", SearchOption.AllDirectories).Any())
                {
                    RepackHelper.RunRepackProcess(paramModmanagerFullPath, Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk"));
                    File.WriteAllBytes(
                        Path.Combine(root_folder, "moddingapi", "mods", "base_game", "param_files.cpk.info"),
                        new byte[8] { 0x22, 0, 0, 0, 1, 0, 0, 0 });
                }



                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "gametitle.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "gametitle.gfx"),
                    true);

                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "celshade.tex.xfbin"),
                    Path.Combine(root_folder, "data", "system", "celshade.tex.xfbin"),
                    true);


                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "xcmn_win_roll1.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "xcmn_win_roll1.gfx"),
                    true);


                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "patchnotes.txt"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gametitle", "patchnotes.txt"),
                    true);

                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "freebtl_set.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "freebtl_set", "freebtl_set.gfx"),
                    true);

                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "gauge_p.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "gauge_p", "gauge_p.gfx"),
                    true);
                File.Copy(
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NS4", "duel_cmn.gfx"),
                    Path.Combine(root_folder, "data", "ui", "flash", "OTHER", "duel_cmn", "duel_cmn.gfx"),
                    true);
                // Clean up temporary directories
                if (Directory.Exists(Path.Combine(root_folder, "cpk_assets")))
                    Directory.Delete(Path.Combine(root_folder, "cpk_assets"), true);
                if (Directory.Exists(Path.Combine(root_folder, "resources_modmanager")))
                    Directory.Delete(Path.Combine(root_folder, "resources_modmanager"), true);
                if (Directory.Exists(Path.Combine(root_folder, "data_win32_modmanager")))
                    Directory.Delete(Path.Combine(root_folder, "data_win32_modmanager"), true);
                if (Directory.Exists(Path.Combine(root_folder, "param_files")))
                    Directory.Delete(Path.Combine(root_folder, "param_files"), true);

                //File.Copy(Directory.GetCurrentDirectory() + "\\ParamFiles\\NSC\\freebtl_set.gfx", Properties.Settings.Default.RootGameNSCFolder + "\\data\\ui\\flash\\OTHER\\freebtl_set\\freebtl_set.gfx", true);
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

                string exePath = Path.Combine(root_folder, "NSUNS4.exe");
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
                //e.Result = ex;
                Debug.WriteLine($"Error: {ex}");
                throw; // Ensure the error is propagated to RunWorkerCompleted
            }
        }
        public void CompileMods()
        {

            try
            {
                string gameDir = Properties.Settings.Default.RootGameNSCFolder;
                if (!Directory.Exists(gameDir))
                {
                    ModernWpf.MessageBox.Show("Select Game Directory for Naruto Storm Connections before compiling mods!");
                    LoadingStatePlay = Visibility.Hidden;
                    return;
                }
                var dirInfo = new DirectoryInfo(gameDir);
                if ((dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    ModernWpf.MessageBox.Show("Game directory is read-only or inaccessible. Run as Administrator.");
                    LoadingStatePlay = Visibility.Hidden;
                    return;
                }

                if (Directory.Exists(Properties.Settings.Default.RootGameNSCFolder))
                {
                    LoadingStatePlay = Visibility.Visible;

                    CompileModAsyncProcess(Properties.Settings.Default.RootGameNSCFolder);

                } else
                    ModernWpf.MessageBox.Show("Set root folder of game!");
            } catch (Exception ex)
            {
                HandleError(ex);
            }

        }
        public void CompileModsNS4()
        {

            try
            {
                string gameDir = Properties.Settings.Default.RootGameNS4Folder;
                if (!Directory.Exists(gameDir))
                {
                    ModernWpf.MessageBox.Show("Select Game Directory for Naruto Storm 4 before compiling mods!");
                    LoadingStatePlay = Visibility.Hidden;
                    return;
                }
                var dirInfo = new DirectoryInfo(gameDir);
                if ((dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    LoadingStatePlay = Visibility.Hidden;
                    ModernWpf.MessageBox.Show("Game directory is read-only or inaccessible. Run as Administrator.");
                    return;
                }

                if (Directory.Exists(Properties.Settings.Default.RootGameNS4Folder))
                {
                    LoadingStatePlay = Visibility.Visible;

                    CompileModAsyncProcess(Properties.Settings.Default.RootGameNS4Folder);

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

                    myDialog.Filter = "Naruto Storm Mod|*.uns;*.unse;*.nsc;*.ensc;|Old Naruto Storm 4 Mod|*.nus4";
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

                string modmanager_folder = Properties.Settings.Default.ModManagerFolder;
                if (!Directory.Exists(modmanager_folder))
                {
                    ModernWpf.MessageBox.Show("Select Mod folder!");
                    return;
                }

                string InstallMod_folder = Path.Combine(modmanager_folder, Path.GetFileNameWithoutExtension(mod_path));
                if (Directory.Exists(InstallMod_folder))
                {
                    Directory.Delete(InstallMod_folder, true);
                }
                Directory.CreateDirectory(InstallMod_folder);

                string ext = Path.GetExtension(mod_path).ToLowerInvariant();
                if (ext == ".nus4")
                {
                    ExtractNus4(mod_path, InstallMod_folder);
                } else
                {
                    System.IO.Compression.ZipFile.ExtractToDirectory(mod_path, InstallMod_folder);
                }

                RefreshModList();
            } catch (Exception ex)
            {
                HandleError(ex);
            }
        }
        private void ExtractNus4(string nus4Path, string destinationFolder)
        {
            if (!File.Exists(nus4Path))
                throw new FileNotFoundException("NUS4 file not found.", nus4Path);

            // --- prepare temp extraction ---
            byte[] fileData = File.ReadAllBytes(nus4Path);
            byte[] zipSignature = new byte[] { 0x50, 0x4B, 0x03, 0x04 };
            int offset = -1;
            for (int i = 0; i <= fileData.Length - zipSignature.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < zipSignature.Length; j++)
                {
                    if (fileData[i + j] != zipSignature[j]) { match = false; break; }
                }
                if (match) { offset = i; break; }
            }
            if (offset < 0)
                throw new InvalidDataException("Cannot extract .nus4: embedded ZIP archive not found.");

            string tempRoot = Path.Combine(Path.GetTempPath(), "nus4_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempRoot);
            string tempZip = Path.Combine(tempRoot, Path.GetFileNameWithoutExtension(nus4Path) + ".zip");
            string extractedTemp = Path.Combine(tempRoot, "extracted");
            Directory.CreateDirectory(extractedTemp);

            try
            {
                // write embedded ZIP to temp file and extract
                using (var outFs = new FileStream(tempZip, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    outFs.Write(fileData, offset, fileData.Length - offset);
                    outFs.Flush();
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(tempZip, extractedTemp);

                // --- create destination structure (do not extract into destination) ---
                Directory.CreateDirectory(destinationFolder);
                string charsRoot = Path.Combine(destinationFolder, "Characters");
                string stagesRoot = Path.Combine(destinationFolder, "Stages");
                string resourcesRoot = Path.Combine(destinationFolder, "Resources");
                string resourcesFilesData = Path.Combine(resourcesRoot, "Files", "data");
                string resourcesCpk = Path.Combine(resourcesRoot, "CPKs");
                string resourcesShaders = Path.Combine(resourcesRoot, "Shaders");

                Directory.CreateDirectory(charsRoot);
                Directory.CreateDirectory(stagesRoot);
                Directory.CreateDirectory(resourcesFilesData);
                Directory.CreateDirectory(resourcesCpk);
                Directory.CreateDirectory(resourcesShaders);

                // --- Icon, Description, Author handling ---
                string[] iconFiles = Directory.GetFiles(extractedTemp, "Icon.png", SearchOption.AllDirectories);
                if (iconFiles.Length > 0)
                {
                    string srcIcon = iconFiles[0];
                    string destIcon = Path.Combine(destinationFolder, "mod_icon.png");
                    File.Copy(srcIcon, destIcon, true);
                }

                string description = "";
                string[] descFiles = Directory.GetFiles(extractedTemp, "Description.txt", SearchOption.AllDirectories);
                if (descFiles.Length > 0)
                    description = File.ReadAllText(descFiles[0]);

                string author = "";
                string[] authorFiles = Directory.GetFiles(extractedTemp, "Author.txt", SearchOption.AllDirectories);
                if (authorFiles.Length > 0)
                    author = File.ReadAllText(authorFiles[0]);

                // create mod_config.ini at destinationFolder root
                string iniPath = Path.Combine(destinationFolder, "mod_config.ini");
                var myIni = new IniFile(iniPath);
                string modName = Path.GetFileNameWithoutExtension(nus4Path);
                myIni.Write("ModName", modName, "ModManager");
                myIni.Write("Description", description, "ModManager");
                myIni.Write("Author", author, "ModManager");
                myIni.Write("LastUpdate", DateTime.Today.ToString("dd/MM/yyyy"), "ModManager");
                myIni.Write("Version", "1.0", "ModManager");
                myIni.Write("Game", "NS4", "ModManager");
                myIni.Write("EnableMod", "true", "ModManager");

                // --- copy .cpk files from any moddingapi/mods/base_game locations into Resources/CPKs ---
                var cpkFiles = Directory.GetFiles(extractedTemp, "*.cpk", SearchOption.AllDirectories);
                foreach (var cpk in cpkFiles)
                {
                    // optionally ensure it's from moddingapi/mods/base_game, but copy all .cpk to be safe
                    string dest = Path.Combine(resourcesCpk, Path.GetFileName(cpk));
                    File.Copy(cpk, dest, true);
                }

                // --- process characters (folders that contain characode.txt) ---
                var characodeFiles = Directory.GetFiles(extractedTemp, "characode.txt", SearchOption.AllDirectories);
                foreach (var characodePath in characodeFiles)
                {
                    string charFolder = Path.GetDirectoryName(characodePath);
                    if (string.IsNullOrEmpty(charFolder)) continue;
                    string charFolderName = new DirectoryInfo(charFolder).Name;
                    string charDestRoot = Path.Combine(charsRoot, charFolderName);
                    Directory.CreateDirectory(charDestRoot);

                    // 1) Создаём character_config.ini
                    string charIniPath = Path.Combine(charDestRoot, "character_config.ini");
                    var charIni = new IniFile(charIniPath);
                    charIni.Write("Partner", "false", "ModManager");
                    charIni.Write("Page", "-1", "ModManager");
                    charIni.Write("Slot", "-1", "ModManager");
                    charIni.Write("Game", "NS4", "ModManager");
                    charIni.Write("Page_NS4", "-1", "ModManager");
                    charIni.Write("Slot_NS4", "-1", "ModManager");

                    // 2) Определяем Partner=true если найден partnerSlotParam.xfbin в moddingapi/mods/**
                    string moddingApiSrc = Path.Combine(charFolder, "moddingapi");
                    bool partnerFound = false;
                    if (Directory.Exists(moddingApiSrc))
                    {
                        var partnerFiles = Directory.GetFiles(moddingApiSrc, "partnerSlotParam.xfbin", SearchOption.AllDirectories);
                        if (partnerFiles.Length > 0) partnerFound = true;
                    }
                    if (partnerFound) charIni.Write("Partner", "true", "ModManager");
                    // 2.5) Копируем ВСЕ .xfbin из character/moddingapi в Characters\[Char]\moddingapi\mods\base_game
                    string charModdingApiSrc = Path.Combine(charFolder, "moddingapi");
                    if (Directory.Exists(charModdingApiSrc))
                    {
                        string charModdingApiDest = Path.Combine(charDestRoot, "moddingapi", "mods", "base_game");
                        Directory.CreateDirectory(charModdingApiDest);

                        foreach (string xfbin in Directory.GetFiles(charModdingApiSrc, "*.xfbin", SearchOption.AllDirectories))
                        {
                            string fileName = Path.GetFileName(xfbin);
                            string destPath = Path.Combine(charModdingApiDest, fileName);

                            using (var src = new FileStream(xfbin, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var dst = new FileStream(destPath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                src.CopyTo(dst);
                            }
                        }
                    }
                    // 3) Копируем ПАРАМЕТРЫ из data_win32 в Characters\[Char]\data
                    string dataWin32Src = Path.Combine(charFolder, "data_win32");
                    string charDataTarget = Path.Combine(charsRoot, charFolderName, "data");
                    if (Directory.Exists(dataWin32Src))
                    {
                        Program.CopyParamsRecursivelyModManager(dataWin32Src, charDataTarget);
                    }

                    // 3) Handle data_win32: copy non-param .xfbin files into Resources/Files/data using provided helper
                    if (Directory.Exists(dataWin32Src))
                    {
                        // Program.CopyFilesRecursivelyModManager copies only non-param .xfbin files preserving structure
                        Program.CopyFilesRecursivelyModManager(dataWin32Src, resourcesFilesData);

                        



                        

                        // --- NEW: для каждого *bod1.xfbin в папке resourcesFilesData\spc создаём копию шаблона acc ---
                        string spcFolder = Path.Combine(resourcesFilesData, "spc");
                        string appFolder = Directory.GetCurrentDirectory();
                        string accTemplate = Path.Combine(appFolder, "ParamFiles", "NS4", "1cmnbod1acc.bin.xfbin");
                        if (Directory.Exists(spcFolder) && File.Exists(accTemplate))
                        {
                            foreach (var bodFile in Directory.GetFiles(spcFolder, "*bod1.xfbin", SearchOption.TopDirectoryOnly))
                            {
                                string baseName = Path.GetFileNameWithoutExtension(bodFile); // e.g. "2nrtbod1"
                                string newName = baseName + "acc.bin.xfbin"; // e.g. "2nrtbod1acc.bin.xfbin"
                                string destPath = Path.Combine(spcFolder, newName);
                                try
                                {
                                    if (baseName != "1cmnbod1")
                                        File.Copy(accTemplate, destPath, true);
                                } catch
                                {
                                    // ignore individual copy errors
                                }
                            }
                        }


                    }
                    
                    // 4) Copy shaders from charFolder/shaders into Resources/Shaders (preserve structure under shaders)
                    string shadersSrc = Path.Combine(charFolder, "shaders");
                    if (Directory.Exists(shadersSrc))
                    {
                        CopyAllPreserveStructure(shadersSrc, resourcesShaders);
                    }
                }
                IEnumerable<string> FindPrmFiles(string root)
                {
                    var results = new List<string>();
                    try
                    {
                        var options = new EnumerationOptions { RecurseSubdirectories = true, IgnoreInaccessible = true };
                        foreach (var f in Directory.EnumerateFiles(root, "*prm.bin.xfbin", options))
                            results.Add(f);
                    } catch (Exception)
                    {
                        // при ошибке возвращаем текущие найденные
                    }
                    return results;
                }
                foreach (var file in FindPrmFiles(resourcesFilesData))
                {
                    Debug.WriteLine(file);
                    var PRM_mod = new PRMEditorViewModel();
                    PRM_mod.OpenFile(file);

                    foreach (var ver in PRM_mod.VerList)
                        foreach (var sec in ver.PL_ANM_Sections)
                            foreach (var fn in sec.FunctionList)
                            {
                                if (fn.FunctionID != 0xEC) continue;
                                int orig = fn.FunctionParam1;
                                switch (orig)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 9:
                                        fn.FunctionID = 0x10E;
                                        break;
                                    case 5:
                                        fn.FunctionParam1 = 10;
                                        fn.FunctionParam3 = 0;
                                        break;
                                    case 6:
                                        fn.FunctionParam1 = 11;
                                        fn.FunctionParam3 = 0;
                                        break;
                                    case 7:
                                        fn.FunctionParam1 = 10;
                                        fn.FunctionParam3 = 1;
                                        break;
                                    case 8:
                                        fn.FunctionParam1 = 11;
                                        fn.FunctionParam3 = 1;
                                        break;
                                    case 10:
                                        fn.FunctionParam1 = 1;
                                        break;
                                }
                                Debug.WriteLine(orig);
                            }

                    PRM_mod.SaveFile();
                }

                //Stages
                var stageFiles = Directory.GetFiles(extractedTemp, "stageMessage.txt", SearchOption.AllDirectories);
                foreach (var stagePath in stageFiles)
                {
                    string stageFolder = Path.GetDirectoryName(stagePath);
                    if (string.IsNullOrEmpty(stageFolder)) continue;
                    string stageFolderName = new DirectoryInfo(stageFolder).Name;
                    string stageDestRoot = Path.Combine(stagesRoot, stageFolderName);
                    Directory.CreateDirectory(stageDestRoot);

                    string bgm_id = "";
                    string[] bgm_idFiles = Directory.GetFiles(extractedTemp, "BGM_ID.txt", SearchOption.AllDirectories);
                    if (bgm_idFiles.Length > 0)
                        bgm_id = File.ReadAllText(bgm_idFiles[0]);

                    string StageMessageID = stageFolderName + "_stageName";
                    // 1) Создаём character_config.ini
                    string stageIniPath = Path.Combine(stageDestRoot, "stage_config.ini");
                    var stageIni = new IniFile(stageIniPath);
                    stageIni.Write("BGM_ID", bgm_id, "ModManager");
                    stageIni.Write("BGM_ID_NS4", bgm_id, "ModManager");
                    stageIni.Write("MessageID", StageMessageID, "ModManager");
                    stageIni.Write("Hell", "false", "ModManager");
                    stageIni.Write("Game", "NS4", "ModManager");

                    string dataWin32Src = Path.Combine(stageFolder, "data_win32");
                    string stageDataTarget = Path.Combine(stagesRoot, stageFolderName, "data");
                    if (Directory.Exists(dataWin32Src))
                    {
                        Program.CopyParamsRecursivelyModManager(dataWin32Src, stageDataTarget);
                    }
                    // --- create MessageInfo from stageMessage.txt ---
                    string[] stageMsgLines = File.ReadAllLines(stagePath);
                    var langMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var ln in stageMsgLines)
                    {
                        if (string.IsNullOrWhiteSpace(ln)) continue;
                        var parts = ln.Split(new[] { '=' }, 2);
                        if (parts.Length != 2) continue;
                        langMap[parts[0].Trim()] = parts[1].Trim();
                    }

                    // StageMessageID уже определён выше
                    byte[] stageCrc = BinaryReader.crc32(StageMessageID);

                    // Создаём MessageInfoS4 и заполняем 12 языков в порядке Program.langS4List
                    MessageInfoS4ViewModel msgS4 = new MessageInfoS4ViewModel();

                    // Ensure lists initialized (на случай, если конструктор не создаёт их)
                    for (int i = 0; i < Program.langS4List.Length; i++)
                    {
                        if (msgS4.MessageInfo_List.Count <= i)
                            msgS4.MessageInfo_List.Add(new ObservableCollection<MessageInfoModel>());
                    }

                    // Для каждой целевой локали берём строку из stageMessage.txt по ключу (fallback на "eng")
                    for (int langIndex = 0; langIndex < Program.langS4List.Length; langIndex++)
                    {
                        string langKey = Program.langS4List[langIndex]; // ожидаемые коды: arae, chi, eng, ...
                        string text = null;
                        if (langMap.ContainsKey(langKey))
                            text = langMap[langKey];
                        else if (langMap.ContainsKey("eng"))
                            text = langMap["eng"];
                        else
                            text = "";

                        var entry = new MessageInfoModel
                        {
                            CRC32Code = stageCrc,
                            MainText = Encoding.UTF8.GetBytes(text),
                            SecondaryText = Encoding.UTF8.GetBytes(text),
                            Speaker = new byte[1] {0},
                            ACBFileID = 0,
                            CueID = 0,
                            DisableText = false
                        };

                        msgS4.MessageInfo_List[langIndex].Add(entry);
                    }

                    // Сохраняем в папку stageDataTarget
                    // SaveFileAs ожидает путь к директории с data (аналогично вашему примеру)
                    msgS4.SaveFileAs(stageDataTarget);
                    // 3) Handle data_win32: copy non-param .xfbin files into Resources/Files/data using provided helper
                    if (Directory.Exists(dataWin32Src))
                    {
                        // Program.CopyFilesRecursivelyModManager copies only non-param .xfbin files preserving structure
                        Program.CopyFilesRecursivelyModManager(dataWin32Src, resourcesFilesData);

                    }

                    File.Copy(
                            Path.Combine(stageFolder, "stage_tex.png"),
                            Path.Combine(stageDestRoot, "stage_preview.png"),
                            true);

                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_icon.dds"),
                        Path.Combine(stageDestRoot, "stage_icon_S4.dds"),
                        true);
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TemplateImages", "stage_icon.dds"),
                        Path.Combine(stageDestRoot, "stage_icon_SC.dds"),
                        true);

                }
                // --- done ---
            } finally
            {
                // cleanup temp
                try
                {
                    if (Directory.Exists(tempRoot))
                        Directory.Delete(tempRoot, true);
                } catch
                {
                    // ignore cleanup errors
                }
            }

            // helper local functions
            static void CopyOnlyXfbinPreserveStructure(string sourceDir, string targetDir)
            {
                if (!Directory.Exists(sourceDir)) return;
                foreach (string src in Directory.GetFiles(sourceDir, "*.xfbin", SearchOption.AllDirectories))
                {
                    string relative = Path.GetRelativePath(sourceDir, src);
                    string dest = Path.Combine(targetDir, relative);
                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    File.Copy(src, dest, true);
                }
            }

            static void CopyAllPreserveStructure(string sourceDir, string targetDir)
            {
                if (!Directory.Exists(sourceDir)) return;
                foreach (string src in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
                {
                    string relative = Path.GetRelativePath(sourceDir, src);
                    string dest = Path.Combine(targetDir, relative);
                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    File.Copy(src, dest, true);
                }
            }

            static string NormalizePath(string path) => path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar);
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

                //RefreshModList();
            }
        }

        public void CleanGameAssets(bool OpenMessage = true, bool cleanMotionBlur = false)
        {
            // Ensure we're on the UI thread.
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => CleanGameAssets(OpenMessage));
                return;
            }

            try
            {
                if (Directory.Exists(Properties.Settings.Default.RootGameNSCFolder))
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
                        string rootFolder = Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "ui", "flash", "OTHER");


                        //InstallModdingAPI(false, Properties.Settings.Default.RootGameNSCFolder);
                        DeleteModdingAPI(OpenMessage);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "stagesel.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "stagesel_image.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel_image.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "charsel.gfx"),
                            Path.Combine(rootFolder, "charsel", "charsel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "charicon_s.gfx"),
                            Path.Combine(rootFolder, "charicon_s", "charicon_s.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "gametitle_vanilla.gfx"),
                            Path.Combine(rootFolder, "gametitle", "gametitle.gfx"),
                            true);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "gauge_p_vanilla.gfx"),
                            Path.Combine(rootFolder, "gauge_p", "gauge_p.gfx"),
                            true);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "nuccMaterial_dx11.nsh"),
                            Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "system", "nuccMaterial_dx11.nsh"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NSC", "freebtl_set_vanilla.gfx"),
                            Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "ui", "flash", "OTHER", "freebtl_set", "freebtl_set.gfx"),
                            true);

                        File.Copy(
                                Path.Combine(appFolder, "ParamFiles", "NSC", "nuccPostEffect_dx11.nsh"),
                                Path.Combine(Properties.Settings.Default.RootGameNSCFolder, "data", "system", "nuccPostEffect_dx11.nsh"),
                                true);

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
        public void CleanGameAssetsNS4(bool OpenMessage = true, bool cleanMotionBlur = false)
        {
            // Ensure we're on the UI thread.
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => CleanGameAssetsNS4(OpenMessage));
                return;
            }

            try
            {
                if (Directory.Exists(Properties.Settings.Default.RootGameNS4Folder))
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
                        string rootFolder = Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "ui", "flash", "OTHER");

                        //InstallModdingAPI(false, Properties.Settings.Default.RootGameNS4Folder);
                        DeleteModdingAPI_NS4(OpenMessage);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "stagesel.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "stagesel_image.gfx"),
                            Path.Combine(rootFolder, "stagesel", "stagesel_image.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "charsel.gfx"),
                            Path.Combine(rootFolder, "charsel", "charsel.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "gametitle_vanilla.gfx"),
                            Path.Combine(rootFolder, "gametitle", "gametitle.gfx"),
                            true);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "charicon_s.gfx"),
                            Path.Combine(rootFolder, "charicon_s", "charicon_s.gfx"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "gauge_p_vanilla.gfx"),
                            Path.Combine(rootFolder, "gauge_p", "gauge_p.gfx"),
                            true);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "duel_cmn_vanilla.gfx"),
                            Path.Combine(rootFolder, "duel_cmn", "duel_cmn.gfx"),
                            true);
                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "nuccMaterial_dx11.nsh"),
                            Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "system", "nuccMaterial_dx11.nsh"),
                            true);

                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "freebtl_set_vanilla.gfx"),
                            Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "ui", "flash", "OTHER", "freebtl_set", "freebtl_set.gfx"),
                            true);


                        File.Copy(
                            Path.Combine(appFolder, "ParamFiles", "NS4", "nuccPostEffect_dx11.nsh"),
                            Path.Combine(Properties.Settings.Default.RootGameNS4Folder, "data", "system", "nuccPostEffect_dx11.nsh"),
                            true);

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
            Properties.Settings.Default.RootGameNSCFolder = RootFolderPath_field;
            Properties.Settings.Default.RootGameNS4Folder = RootFolderPathNS4_field;
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
            Properties.Settings.Default.RootGameNSCFolder = "";
            Properties.Settings.Default.RootGameNS4Folder = "";
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
                FileName = "https://app.lava.top/theleonx?tabId=donate",
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
        public void SelectModManagerFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select folder where mods will be installed";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.ModManagerFolder = dialog.FileName;
                ModManagerFolder_field = dialog.FileName;
                Properties.Settings.Default.Save();
            } else
            {
                return;
            }
        }
        public void SelectRootFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select root folder of Storm Connections";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                if (!File.Exists(Path.Combine(dialog.FileName, "NSUNSC.exe")))
                {
                    ModernWpf.MessageBox.Show("Executable file doesn't exist. Selected wrong folder for Naruto Storm Connections.");
                    return;
                }

                Properties.Settings.Default.RootGameNSCFolder = dialog.FileName;
                RootFolderPath_field = dialog.FileName;
                Properties.Settings.Default.Save();
            } else
            {
                return;
            }
        }
        public void SelectRootFolderNS4()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select root folder of Storm 4";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                if (!File.Exists(Path.Combine(dialog.FileName, "NSUNS4.exe")))
                {
                    ModernWpf.MessageBox.Show("Executable file doesn't exist. Selected wrong folder for Naruto Storm 4.");
                    return;
                }
                Properties.Settings.Default.RootGameNS4Folder = dialog.FileName;
                RootFolderPathNS4_field = dialog.FileName;
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
                    var dialog = new CommonOpenFileDialog
                    {
                        IsFolderPicker = true,
                        Title = "Select root folder of game"
                    };

                    CommonFileDialogResult result = dialog.ShowDialog();
                    if (result != CommonFileDialogResult.Ok)
                        return;

                    root_path = dialog.FileName;
                }

                if (!Directory.Exists(root_path))
                    return;

                string moddingAPIFilesPath =
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModdingAPIFiles");

                Program.CopyFilesRecursively(moddingAPIFilesPath, root_path);

                string exePath = Path.Combine(root_path, "NSUNSC.exe");


                RepackHelper.RemoveZoneIdentifier(Path.Combine(root_path, "d3dcompiler_47.dll"));
                RepackHelper.RemoveZoneIdentifier(Path.Combine(root_path, "d3dcompiler_47_o.dll"));
                if (File.Exists(exePath))
                {
                    string sourceFile =
                        Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "charsel.gfx");

                    string targetDir =
                        Path.Combine(root_path, "data", "ui", "flash", "OTHER", "charsel");

                    string targetFile =
                        Path.Combine(targetDir, "charsel.gfx");

                    Directory.CreateDirectory(targetDir);

                    File.Copy(sourceFile, targetFile, true);
                }

                if (showMessage)
                {
                    ModernWpf.MessageBox.Show("ModdingAPI was installed!");
                }
            } catch (Exception ex)
            {
                HandleError(new Exception("Error while installing ModdingAPI.", ex));
            }
        }

        public void DeleteModdingAPI(bool openMessage = true)
        {
            try
            {
                string rootFolder = Properties.Settings.Default.RootGameNSCFolder;
                string moddingAPIPath = Path.Combine(rootFolder, "moddingapi");
                bool delete_api = false;

                if (!Directory.Exists(moddingAPIPath))
                    return;

                if (openMessage)
                {
                    MessageBoxResult warning =
                        (MessageBoxResult)ModernWpf.MessageBox.Show(
                            "Are you sure that you want to delete ModdingAPI? All mods inside of it will be deleted too.",
                            "Do you want to delete it?",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                    if (warning == MessageBoxResult.Yes)
                        delete_api = true;
                } else
                {
                    delete_api = true;
                }

                if (!delete_api)
                    return;

                Directory.Delete(moddingAPIPath, true);

                string d3d = Path.Combine(rootFolder, "d3dcompiler_47.dll");
                string d3dBackup = Path.Combine(rootFolder, "d3dcompiler_47_o.dll");

                if (File.Exists(d3d))
                    File.Delete(d3d);

                if (File.Exists(d3dBackup))
                    File.Move(d3dBackup, d3d);



                string xinput_backup = Path.Combine(rootFolder, "xinput9_1_0_o.dll");
                string xinput = Path.Combine(rootFolder, "xinput9_1_0.dll");

                if (File.Exists(xinput))
                {
                    File.Delete(xinput);

                }
                if (File.Exists(xinput_backup))
                {
                    File.Delete(xinput_backup);

                }
                // Restore vanilla charsel.gfx (no NSUNSC.exe check)
                string sourceFile =
                    Path.Combine(Directory.GetCurrentDirectory(), "ParamFiles", "NSC", "charsel_vanilla.gfx");

                string targetDir =
                    Path.Combine(rootFolder, "data", "ui", "flash", "OTHER", "charsel");

                string targetFile =
                    Path.Combine(targetDir, "charsel.gfx");

                Directory.CreateDirectory(targetDir);
                File.Copy(sourceFile, targetFile, true);

                if (openMessage)
                    ModernWpf.MessageBox.Show("ModdingAPI was deleted!");
            } catch (Exception ex)
            {
                HandleError(ex);
            }
        }
        public void DeleteModdingAPI_NS4(bool openMessage = true)
        {
            try
            {
                string appFolder = Directory.GetCurrentDirectory();
                string rootFolder = Properties.Settings.Default.RootGameNS4Folder;
                string moddingAPIPath = Path.Combine(rootFolder, "moddingapi");
                bool delete_api = false;
                if (Directory.Exists(moddingAPIPath))
                {
                    if (openMessage == true)
                    {
                        MessageBoxResult warning = (MessageBoxResult)ModernWpf.MessageBox.Show(
                        "Are you sure that you want to delete ModdingAPI? All mods inside of it will be deleted too.",
                        "Do you want to delete it?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (warning == MessageBoxResult.Yes)
                        {
                            delete_api = true;
                        }
                    } else
                    {
                        delete_api = true;
                    }
                    if (delete_api)
                    {
                        Directory.Delete(moddingAPIPath, true);
                        if (File.Exists(Path.Combine(rootFolder, "d3dcompiler_47.dll")))
                            File.Delete(Path.Combine(rootFolder, "d3dcompiler_47.dll"));
                        if (File.Exists(Path.Combine(rootFolder, "d3dcompiler_47_o.dll")))
                            File.Move(Path.Combine(rootFolder, "d3dcompiler_47_o.dll"), Path.Combine(rootFolder, "d3dcompiler_47.dll"));
                        string xinput_backup = Path.Combine(rootFolder, "xinput9_1_0_o.dll");
                        string xinput = Path.Combine(rootFolder, "xinput9_1_0.dll");

                        if (File.Exists(xinput))
                        {
                            File.Delete(xinput);

                        }
                        if (File.Exists(xinput_backup))
                        {
                            File.Delete(xinput_backup);

                        }
                        //CleanGameAssetsNS4(false, true);
                        if (openMessage)
                            ModernWpf.MessageBox.Show("ModdingAPI was deleted!");
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
        private RelayCommand _selectModManagerFolderCommand;
        public RelayCommand SelectModManagerFolderCommand
        {
            get
            {
                return _selectModManagerFolderCommand ??
                  (_selectModManagerFolderCommand = new RelayCommand(obj =>
                  {
                      SelectModManagerFolder();

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
        private RelayCommand _selectRootFolderNS4Command;
        public RelayCommand SelectRootFolderNS4Command
        {
            get
            {
                return _selectRootFolderNS4Command ??
                  (_selectRootFolderNS4Command = new RelayCommand(obj =>
                  {
                      SelectRootFolderNS4();

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
                      if (Properties.Settings.Default.StormVersion == 1)
                          DeleteModdingAPI();
                      else if (Properties.Settings.Default.StormVersion == 2)
                          DeleteModdingAPI_NS4();

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

                      if (Properties.Settings.Default.StormVersion == 1)
                          CleanGameAssets(true);
                      else if (Properties.Settings.Default.StormVersion == 2)
                          CleanGameAssetsNS4(true);
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
                      if (Properties.Settings.Default.StormVersion == 1)
                          CompileMods();
                      else if (Properties.Settings.Default.StormVersion == 2)
                          CompileModsNS4();
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
                      if (Properties.Settings.Default.StormVersion == 1)
                      {

                          CharacterRosterEditorView s = new CharacterRosterEditorView(VM);
                          s.Show();
                      }
                      else if (Properties.Settings.Default.StormVersion == 2)
                      {

                          CharacterRosterEditorNS4View s = new CharacterRosterEditorNS4View(VM);
                          s.Show();
                      }

                  }));
            }
        }

        private RelayCommand _changeGame;
        public RelayCommand ChangeGame
        {
            get
            {
                return _changeGame ?? (_changeGame = new RelayCommand(obj =>
                {

                    // Сохранение в настройки по изменению
                    Properties.Settings.Default.StormVersion = GameVersion;
                    Debug.Print(GameVersion.ToString());
                    Properties.Settings.Default.Save();
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
                ModernWpf.MessageBox.Show($"Error: {ex.Message}\n\n {ex.TargetSite}\n\n {ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error: {ex.Message}\n\n{ex.StackTrace}", "Error");
                KyurutoDialogTextLoader("An error occurred during compilation. Check error details.", 20);
                LoadingStatePlay = Visibility.Hidden;
            });
        }

    }


}
