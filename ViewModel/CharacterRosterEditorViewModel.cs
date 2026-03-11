using DynamicData;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using NSC_Toolbox.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NSC_ModManager.ViewModel
{
    public class CharacterRosterEditorViewModel : INotifyPropertyChanged
    {

        public TitleViewModel TitleVM;

        private ObservableCollection<CharacterSelectParamModel> _characterFullList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterFullList
        {
            get { return _characterFullList; }
            set { _characterFullList = value; OnPropertyChanged("CharacterFullList"); }
        }

        private ObservableCollection<CharacterSelectParamModel> _characterList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterList
        {
            get { return _characterList; }
            set { _characterList = value; OnPropertyChanged("CharacterList"); }
        }

        private ObservableCollection<CharacterSelectParamModel> _characterPlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterPlaceHolderList
        {
            get { return _characterPlaceHolderList; }
            set { _characterPlaceHolderList = value; OnPropertyChanged("CharacterPlaceHolderList"); }
        }

        private ObservableCollection<CharacterSelectParamModel> _costumePlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CostumePlaceHolderList
        {
            get { return _costumePlaceHolderList; }
            set { _costumePlaceHolderList = value; OnPropertyChanged("CostumePlaceHolderList"); }
        }

        // NEW: Costume list for selected placeholder slot
        private ObservableCollection<CharacterSelectParamModel> _placeholderCostumePlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> PlaceholderCostumePlaceHolderList
        {
            get { return _placeholderCostumePlaceHolderList; }
            set { _placeholderCostumePlaceHolderList = value; OnPropertyChanged("PlaceholderCostumePlaceHolderList"); }
        }

        private CharacterSelectParamModel _selectedCostume = new CharacterSelectParamModel();
        public CharacterSelectParamModel SelectedCostume
        {
            get { return _selectedCostume; }
            set { _selectedCostume = value; OnPropertyChanged("SelectedCostume"); }
        }

        private int _selectedCostumeIndex;
        public int SelectedCostumeIndex
        {
            get { return _selectedCostumeIndex; }
            set { _selectedCostumeIndex = value; OnPropertyChanged("SelectedCostumeIndex"); }
        }

        private CharacterSelectParamModel _selectedCharacter = new CharacterSelectParamModel();
        public CharacterSelectParamModel SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value; OnPropertyChanged("SelectedCharacter"); }
        }

        // When SelectedCharacterIndex changes, update CostumePlaceHolderList
        private int _selectedCharacterIndex;
        public int SelectedCharacterIndex
        {
            get { return _selectedCharacterIndex; }
            set
            {
                CostumePlaceHolderList.Clear();

                // Build costume list for selected character slot
                foreach (CharacterSelectParamModel entry in CharacterFullList)
                {
                    if (entry.PageIndex == RosterPage_field && entry.SlotIndex == value + 1 && entry.CostumeIndex != 0)
                    {
                        if (!CostumePlaceHolderList.Contains(entry))
                            CostumePlaceHolderList.Add(entry);
                    }
                }

                _selectedCharacterIndex = value;
                OnPropertyChanged("SelectedCharacterIndex");
            }
        }

        private CharacterSelectParamModel _selectedPlaceholderCharacter = new CharacterSelectParamModel();
        public CharacterSelectParamModel SelectedPlaceholderCharacter
        {
            get { return _selectedPlaceholderCharacter; }
            set { _selectedPlaceholderCharacter = value; OnPropertyChanged("SelectedPlaceholderCharacter"); }
        }

        // When selected placeholder changes, update PlaceholderCostumePlaceHolderList
        private int _selectedPlaceholderCharacterIndex;
        public int SelectedPlaceholderCharacterIndex
        {
            get { return _selectedPlaceholderCharacterIndex; }
            set
            {
                _selectedPlaceholderCharacterIndex = value;

                PlaceholderCostumePlaceHolderList.Clear();

                // Show costumes for the selected placeholder character
                if (value >= 0 && value < CharacterPlaceHolderList.Count)
                {
                    var selectedChar = CharacterPlaceHolderList[value];
                    foreach (CharacterSelectParamModel entry in CharacterFullList)
                    {
                        if (entry.PageIndex == -1 &&
                            entry.SlotIndex == selectedChar.SlotIndex &&
                            entry.CostumeIndex != 0)
                        {
                            if (!PlaceholderCostumePlaceHolderList.Contains(entry))
                                PlaceholderCostumePlaceHolderList.Add(entry);
                        }
                    }
                }

                OnPropertyChanged("SelectedPlaceholderCharacterIndex");
            }
        }

        private int _rosterPage_field;
        public int RosterPage_field
        {
            get { return _rosterPage_field; }
            set
            {
                _rosterPage_field = value;
                CharacterList.Clear();
                CharacterPlaceHolderList.Clear();
                foreach (CharacterSelectParamModel entry in CharacterFullList)
                {
                    if (entry.PageIndex == value && entry.CostumeIndex == 0)
                    {
                        CharacterList.Add(entry);
                    }
                    if (entry.PageIndex < 0 && entry.CostumeIndex == 0)
                    {
                        if (!CharacterPlaceHolderList.Contains(entry))
                            CharacterPlaceHolderList.Add(entry);
                    }
                }
                OnPropertyChanged("RosterPage_field");
            }
        }

        public int LastSlot()
        {
            if (CharacterFullList.Count > 0)
            {
                int maxPage = RosterPage_field;
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterFullList.Count; i++)
                {
                    if (!Slots.Contains(CharacterFullList[i].SlotIndex) && CharacterFullList[i].PageIndex == maxPage)
                        Slots.Add(CharacterFullList[i].SlotIndex);
                }
                return Slots.Max();
            } else
            {
                return 1;
            }
        }

        public int LastSlotOnPage(int page)
        {
            if (CharacterFullList.Count > 0)
            {
                int maxPage = page;
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterFullList.Count; i++)
                {
                    if (CharacterFullList[i].PageIndex == maxPage)
                        Slots.Add(CharacterFullList[i].SlotIndex);
                }
                return Slots.Max();
            } else
            {
                return 1;
            }
        }

        public int FreeSlotOnPage(int page)
        {
            if (CharacterFullList.Count > 0)
            {
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterFullList.Count; i++)
                {
                    if (CharacterFullList[i].PageIndex == page && CharacterFullList[i].CostumeIndex == 0)
                    {
                        Slots.Add(CharacterFullList[i].SlotIndex);
                    }
                }
                for (int i = 1; i <= Slots.Count + 1; i++)
                {
                    if (!Slots.Contains(i))
                    {
                        return i;
                    }
                }
            }
            return 1;
        }

        public void ReplaceSlots(int sourcePage, int sourceSlot, int targetPage, int targetSlot = -1)
        {
            if (CharacterFullList.Count > 0)
            {
                ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();

                // Same page reorder
                if (sourcePage == targetPage && targetSlot != -1)
                {
                    foreach (CharacterSelectParamModel entry in CharacterFullList)
                    {
                        if (entry.PageIndex == targetPage && entry.SlotIndex == targetSlot)
                        {
                            entry.SlotIndex = -2;
                        }
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot)
                        {
                            entry.SlotIndex = -3;
                        }
                    }
                    foreach (CharacterSelectParamModel entry in CharacterFullList)
                    {
                        if (entry.PageIndex == targetPage && entry.SlotIndex == -2)
                        {
                            entry.SlotIndex = sourceSlot;
                        }
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == -3)
                        {
                            entry.SlotIndex = targetSlot;
                        }
                    }
                }
                // Move to different page or convert slot/costume
                else
                {
                    int freeSlot = FreeSlotOnPage(targetPage);

                    foreach (CharacterSelectParamModel entry in CharacterFullList)
                    {
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot)
                        {
                            entry.PageIndex = targetPage;
                            entry.SlotIndex = freeSlot;

                            // Convert to costume if target is a valid character page/slot
                            if (targetPage >= 0 && targetSlot > 0)
                            {
                                // Find the next available costume index for this slot
                                int maxCostume = 0;
                                foreach (CharacterSelectParamModel existingEntry in CharacterFullList)
                                {
                                    if (existingEntry.PageIndex == targetPage && existingEntry.SlotIndex == targetSlot && existingEntry.CostumeIndex > maxCostume)
                                    {
                                        maxCostume = existingEntry.CostumeIndex;
                                    }
                                }
                                entry.SlotIndex = targetSlot;
                                entry.CostumeIndex = maxCostume + 1;
                            }
                            // Convert to standalone slot
                            else
                            {
                                entry.CostumeIndex = 0;
                            }
                        }
                    }

                    // Compact slots on source page
                    for (int i = 0; i < CharacterFullList.Count; i++)
                    {
                        CharacterSelectParamModel entry = CharacterFullList[i];
                        if (entry.PageIndex == sourcePage && sourceSlot < entry.SlotIndex && entry.CostumeIndex == 0)
                        {
                            entry.SlotIndex--;
                        }
                    }
                }

                sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
                CharacterFullList = sortCharacterList;
            }
        }

        // Convert costume to slot
        public void ConvertCostumeToSlot(int page, int slot, int costumeIndex, int targetPage)
        {
            if (CharacterFullList.Count > 0)
            {
                foreach (CharacterSelectParamModel entry in CharacterFullList)
                {
                    if (entry.PageIndex == page && entry.SlotIndex == slot && entry.CostumeIndex == costumeIndex)
                    {
                        entry.PageIndex = targetPage;
                        entry.SlotIndex = FreeSlotOnPage(targetPage);
                        entry.CostumeIndex = 0;
                        break;
                    }
                }

                ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
                sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
                CharacterFullList = sortCharacterList;
            }
        }

        public void MoveSlots(int sourcePage, int sourceSlot, int targetPage)
        {
            if (CharacterFullList.Count > 0)
            {
                ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
                foreach (CharacterSelectParamModel entry in CharacterFullList)
                {
                    if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot)
                    {
                        entry.PageIndex = targetPage;
                        entry.SlotIndex = LastSlotOnPage(targetPage);
                    }
                }

                sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
                CharacterFullList = sortCharacterList;
            }
        }

        public void RestoreRoster()
        {
            MessageBoxResult warning = (MessageBoxResult)ModernWpf.MessageBox.Show("Are you sure that you want to restore all icons? ", "Do you want to restore it?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (warning == MessageBoxResult.Yes)
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\NSC\\characterSelectParam_vanilla.xfbin", AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\NSC\\characterSelectParam.xfbin", true);

                string root_folder = Properties.Settings.Default.RootGameNSCFolder;
                string modmanager_folder = Properties.Settings.Default.ModManagerFolder;
                if (Directory.Exists(modmanager_folder))
                {
                    DirectoryInfo d = new DirectoryInfo(modmanager_folder);
                    FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories);

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
                        if (ModEntry.EnableMod)
                        {
                            DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                            FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                            foreach (FileInfo character_path in CharacterModList)
                            {
                                var CharacterInfo = new IniFile(character_path.FullName);
                                CharacterInfo.Write("EnableRosterChange", "false", "ModManager");
                            }
                            FileInfo[] CostumeModList = mod_dir.GetFiles("model_config.ini", SearchOption.AllDirectories);
                            foreach (FileInfo costume_path in CostumeModList)
                            {
                                var CharacterInfo = new IniFile(costume_path.FullName);
                                CharacterInfo.Write("EnableRosterChange", "false", "ModManager");
                            }
                        }
                    }
                }
                LoadModList();
                RosterPage_field = 0;

                TitleVM?.RefreshModList();
                ModernWpf.MessageBox.Show("Roster was restored!");
            }
        }

        public void SaveRoster()
        {
            string root_folder = Properties.Settings.Default.RootGameNSCFolder;
            string modmanager_folder = Properties.Settings.Default.ModManagerFolder;
            if (Directory.Exists(modmanager_folder))
            {
                DirectoryInfo d = new DirectoryInfo(modmanager_folder);
                FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories);

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
                    if (ModEntry.EnableMod)
                    {
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        FileInfo[] CostumeModList = mod_dir.GetFiles("model_config.ini", SearchOption.AllDirectories);
                        foreach (FileInfo character_path in CharacterModList)
                        {
                            var CharacterInfo = new IniFile(character_path.FullName);

                            string gameVersion = (CharacterInfo.Read("Game", "ModManager") ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try { CharacterInfo.Write("Game", gameVersion, "ModManager"); } catch { }
                            }

                            CharacterModModel CharacterEntry = new CharacterModModel()
                            {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                RootPath = Path.GetDirectoryName(character_path.FullName),
                                GameVersion = gameVersion
                            };

                            string modCharParamPath = Path.Combine(CharacterEntry.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");

                            List<string> CSPCodeList = new List<string>();

                            bool handledSave = false;
                            if (string.Equals(CharacterEntry.GameVersion, "NS4", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!CharacterEntry.Partner && File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamS4ViewModel CharacterSelectParamMod = new CharacterSelectParamS4ViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);
                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            CSPCodeList.Add(rosterEntry.CSP_code);
                                        }
                                        handledSave = true;
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NS4 char param for save: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }

                            if (!handledSave)
                            {
                                if (!CharacterEntry.Partner && File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);
                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            CSPCodeList.Add(rosterEntry.CSP_code);
                                        }
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NSC char param for save: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }

                            if (!CharacterEntry.Partner)
                            {
                                CharacterInfo.Write("EnableRosterChange", "true", "ModManager");
                                foreach (CharacterSelectParamModel rosterEntry in CharacterFullList)
                                {
                                    if (CSPCodeList.Contains(rosterEntry.CSP_code))
                                    {
                                        CharacterInfo.Write("Page", rosterEntry.PageIndex.ToString(), rosterEntry.CSP_code);
                                        CharacterInfo.Write("Slot", rosterEntry.SlotIndex.ToString(), rosterEntry.CSP_code);
                                        CharacterInfo.Write("Costume", rosterEntry.CostumeIndex.ToString(), rosterEntry.CSP_code);
                                    }
                                }
                            }
                        }

                        foreach (FileInfo costume_path in CostumeModList)
                        {
                            Debug.WriteLine(costume_path.FullName);
                            var CostumeInfo = new IniFile(costume_path.FullName);

                            string gameVersion = (CostumeInfo.Read("Game", "ModManager") ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try { CostumeInfo.Write("Game", gameVersion, "ModManager"); } catch { }
                            }

                            CostumeModModel ModelEntry = new CostumeModModel()
                            {
                                Characode = CostumeInfo.Read("Characode", "ModManager"),
                                BaseCostume = CostumeInfo.Read("BaseModel", "ModManager"),
                                AwakeCostume = CostumeInfo.Read("AwakeModel", "ModManager"),
                                GameVersion = gameVersion,
                                RootPath = Path.GetDirectoryName(costume_path.FullName)
                            };

                            string modCharParamPath = Path.Combine(ModelEntry.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");

                            List<string> CSPCodeList = new List<string>();

                            bool handledSave = false;
                            if (string.Equals(ModelEntry.GameVersion, "NS4", StringComparison.OrdinalIgnoreCase))
                            {
                                if (File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamS4ViewModel CharacterSelectParamMod = new CharacterSelectParamS4ViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);
                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            CSPCodeList.Add(rosterEntry.CSP_code);
                                        }
                                        handledSave = true;
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NS4 char param for save: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }

                            if (!handledSave)
                            {
                                if (File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);
                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            CSPCodeList.Add(rosterEntry.CSP_code);
                                        }
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NSC char param for save: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }

                            CostumeInfo.Write("EnableRosterChange", "true", "ModManager");
                            foreach (CharacterSelectParamModel rosterEntry in CharacterFullList)
                            {
                                if (CSPCodeList.Contains(rosterEntry.CSP_code))
                                {
                                    CostumeInfo.Write("Page", rosterEntry.PageIndex.ToString(), "ModManager");
                                    CostumeInfo.Write("Slot", rosterEntry.SlotIndex.ToString(), "ModManager");
                                    CostumeInfo.Write("Costume", rosterEntry.CostumeIndex.ToString(), "ModManager");
                                }
                            }
                        }
                    }
                }
            }

            CharacterSelectParamViewModel CharacterSelectParamBase = new CharacterSelectParamViewModel();
            CharacterSelectParamBase.CharacterSelectParamList = CharacterFullList;
            CharacterSelectParamBase.SaveFileAs(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\NSC\\characterSelectParam.xfbin");

            TitleVM.RefreshModList();
            ModernWpf.MessageBox.Show("Roster was saved!");
        }

        public CharacterRosterEditorViewModel(TitleViewModel VM)
        {
            TitleVM = VM;
            CharacterList = new ObservableCollection<CharacterSelectParamModel>();
            CharacterFullList = new ObservableCollection<CharacterSelectParamModel>();
            CharacterPlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
            CostumePlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
            PlaceholderCostumePlaceHolderList = new ObservableCollection<CharacterSelectParamModel>(); // Initialize new list
            LoadModList();
            RosterPage_field = 0;
        }

        public void LoadModList()
        {
            CharacterList.Clear();
            CharacterFullList.Clear();
            CharacterPlaceHolderList.Clear();
            CostumePlaceHolderList.Clear();
            PlaceholderCostumePlaceHolderList.Clear(); // Clear new list

            string root_folder = Properties.Settings.Default.RootGameNSCFolder;
            string modmanager_folder = Properties.Settings.Default.ModManagerFolder;
            CharacterSelectParamViewModel CharacterSelectParamBase = new CharacterSelectParamViewModel();
            string baseCharParamPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\NSC\\characterSelectParam.xfbin";
            CharacterSelectParamBase.OpenFile(baseCharParamPath);

            if (Directory.Exists(modmanager_folder))
            {
                DirectoryInfo d = new DirectoryInfo(modmanager_folder);
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
                    if (ModEntry.EnableMod)
                    {
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        FileInfo[] CostumeModList = mod_dir.GetFiles("model_config.ini", SearchOption.AllDirectories);
                        foreach (FileInfo character_path in CharacterModList)
                        {
                            var CharacterInfo = new IniFile(character_path.FullName);

                            string gameVersion = (CharacterInfo.Read("Game", "ModManager") ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try { CharacterInfo.Write("Game", gameVersion, "ModManager"); } catch { }
                            }

                            CharacterModModel CharacterEntry = new CharacterModModel()
                            {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                RootPath = Path.GetDirectoryName(character_path.FullName),
                                GameVersion = gameVersion
                            };

                            if (!CharacterEntry.Partner)
                            {
                                string modCharParamPath = Path.Combine(CharacterEntry.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");

                                List<string> CSPCodeList = new List<string>();
                                foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamBase.CharacterSelectParamList)
                                {
                                    CSPCodeList.Add(rosterEntry.CSP_code);
                                }

                                int addedCount = 0;

                                bool handled = false;
                                if (string.Equals(CharacterEntry.GameVersion, "NS4", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (File.Exists(modCharParamPath))
                                    {
                                        try
                                        {
                                            CharacterSelectParamS4ViewModel CharacterSelectParamMod = new CharacterSelectParamS4ViewModel();
                                            CharacterSelectParamMod.OpenFile(modCharParamPath);

                                            int assignedSlot = CharacterSelectParamBase.FreeSlotOnPage(-1);

                                            foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                            {
                                                if (!CSPCodeList.Contains(rosterEntry.CSP_code))
                                                {
                                                    rosterEntry.PageIndex = -1;
                                                    rosterEntry.CostumeIndex -= 1;
                                                    rosterEntry.SlotIndex = assignedSlot;
                                                    rosterEntry.CharacterIconPath = ModEntry.IconPath;
                                                    rosterEntry.SaveInFile = false;
                                                    CharacterSelectParamBase.CharacterSelectParamList.Add(rosterEntry);
                                                    addedCount++;
                                                }
                                            }
                                            handled = true;
                                        } catch (Exception ex)
                                        {
                                            Console.WriteLine($"Failed to open NS4 char param: {modCharParamPath} -> {ex.Message}");
                                        }
                                    }
                                }

                                if (!handled)
                                {
                                    if (File.Exists(modCharParamPath))
                                    {
                                        try
                                        {
                                            CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                                            CharacterSelectParamMod.OpenFile(modCharParamPath);

                                            int assignedSlot = CharacterSelectParamBase.FreeSlotOnPage(-1);

                                            foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                            {
                                                if (!CSPCodeList.Contains(rosterEntry.CSP_code))
                                                {
                                                    rosterEntry.PageIndex = -1;
                                                    rosterEntry.SlotIndex = assignedSlot;
                                                    rosterEntry.CharacterIconPath = ModEntry.IconPath;
                                                    rosterEntry.SaveInFile = false;
                                                    CharacterSelectParamBase.CharacterSelectParamList.Add(rosterEntry);
                                                    addedCount++;
                                                }
                                            }
                                        } catch (Exception ex)
                                        {
                                            Console.WriteLine($"Failed to open NSC char param: {modCharParamPath} -> {ex.Message}");
                                        }
                                    }
                                }
                            }
                        }

                        foreach (FileInfo costume_path in CostumeModList)
                        {
                            var CostumeInfo = new IniFile(costume_path.FullName);

                            string gameVersion = (CostumeInfo.Read("Game", "ModManager") ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(gameVersion))
                            {
                                gameVersion = "NSC";
                                try { CostumeInfo.Write("Game", gameVersion, "ModManager"); } catch { }
                            }

                            CostumeModModel ModelEntry = new CostumeModModel()
                            {
                                Characode = CostumeInfo.Read("Characode", "ModManager"),
                                BaseCostume = CostumeInfo.Read("BaseModel", "ModManager"),
                                AwakeCostume = CostumeInfo.Read("AwakeModel", "ModManager"),
                                GameVersion = gameVersion,
                                RootPath = Path.GetDirectoryName(costume_path.FullName)
                            };

                            string modCharParamPath = Path.Combine(ModelEntry.RootPath, "data", "ui", "max", "select", "characterSelectParam.xfbin");

                            List<string> CSPCodeList = new List<string>();
                            foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamBase.CharacterSelectParamList)
                            {
                                CSPCodeList.Add(rosterEntry.CSP_code);
                            }

                            int addedCount = 0;

                            bool handled = false;
                            if (string.Equals(ModelEntry.GameVersion, "NS4", StringComparison.OrdinalIgnoreCase))
                            {
                                if (File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamS4ViewModel CharacterSelectParamMod = new CharacterSelectParamS4ViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);

                                        int assignedSlot = CharacterSelectParamBase.FreeSlotOnPage(-1);

                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            if (!CSPCodeList.Contains(rosterEntry.CSP_code))
                                            {
                                                rosterEntry.PageIndex = -1;
                                                rosterEntry.CostumeIndex = 0;
                                                rosterEntry.SlotIndex = assignedSlot;
                                                rosterEntry.CharacterIconPath = ModEntry.IconPath;
                                                rosterEntry.SaveInFile = false;
                                                CharacterSelectParamBase.CharacterSelectParamList.Add(rosterEntry);
                                                addedCount++;
                                            }
                                        }
                                        handled = true;
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NS4 char param: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }

                            if (!handled)
                            {
                                if (File.Exists(modCharParamPath))
                                {
                                    try
                                    {
                                        CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                                        CharacterSelectParamMod.OpenFile(modCharParamPath);

                                        int assignedSlot = CharacterSelectParamBase.FreeSlotOnPage(-1);

                                        foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList)
                                        {
                                            if (!CSPCodeList.Contains(rosterEntry.CSP_code))
                                            {
                                                rosterEntry.PageIndex = -1;
                                                rosterEntry.SlotIndex = assignedSlot;
                                                rosterEntry.CostumeIndex = 0;
                                                rosterEntry.CharacterIconPath = ModEntry.IconPath;
                                                rosterEntry.SaveInFile = false;
                                                CharacterSelectParamBase.CharacterSelectParamList.Add(rosterEntry);
                                                addedCount++;
                                            }
                                        }
                                    } catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to open NSC char param: {modCharParamPath} -> {ex.Message}");
                                    }
                                }
                            }
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

            CharacterFullList = CharacterSelectParamBase.CharacterSelectParamList;

            ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
            sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
            CharacterFullList = sortCharacterList;
        }

        private RelayCommand _saveRosterCommand;
        public RelayCommand SaveRosterCommand
        {
            get
            {
                return _saveRosterCommand ??
                  (_saveRosterCommand = new RelayCommand(obj => { SaveRoster(); }));
            }
        }

        private RelayCommand _restoreRosterCommand;
        public RelayCommand RestoreRosterCommand
        {
            get
            {
                return _restoreRosterCommand ??
                  (_restoreRosterCommand = new RelayCommand(obj => { RestoreRoster(); }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
