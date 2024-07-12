using DynamicData;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NSC_ModManager.ViewModel {
    public class CharacterRosterEditorViewModel : INotifyPropertyChanged {


        public TitleViewModel TitleVM;

        private ObservableCollection<CharacterSelectParamModel> _characterFullList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterFullList {
            get {
                return _characterFullList;
            }
            set {
                _characterFullList = value;
                OnPropertyChanged("CharacterFullList");
            }
        }

        private ObservableCollection<CharacterSelectParamModel> _characterList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterList {
            get {
                return _characterList;
            }
            set {
                _characterList = value;
                OnPropertyChanged("CharacterList");
            }
        }
        private ObservableCollection<CharacterSelectParamModel> _characterPlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
        public ObservableCollection<CharacterSelectParamModel> CharacterPlaceHolderList {
            get {
                return _characterPlaceHolderList;
            }
            set {
                _characterPlaceHolderList = value;
                OnPropertyChanged("CharacterPlaceHolderList");
            }
        }
        private CharacterSelectParamModel _selectedCharacter = new CharacterSelectParamModel();
        public CharacterSelectParamModel SelectedCharacter {
            get {
                return _selectedCharacter;
            }
            set {
                _selectedCharacter = value;
                OnPropertyChanged("SelectedCharacter");
            }
        }
        private int _selectedCharacterIndex;
        public int SelectedCharacterIndex {
            get {
                return _selectedCharacterIndex;
            }
            set {
                _selectedCharacterIndex = value;
                OnPropertyChanged("SelectedCharacterIndex");
            }
        }
        private CharacterSelectParamModel _selectedPlaceholderCharacter = new CharacterSelectParamModel();
        public CharacterSelectParamModel SelectedPlaceholderCharacter {
            get {
                return _selectedPlaceholderCharacter;
            }
            set {
                _selectedPlaceholderCharacter = value;
                OnPropertyChanged("SelectedPlaceholderCharacter");
            }
        }
        private int _selectedPlaceholderCharacterIndex;
        public int SelectedPlaceholderCharacterIndex {
            get {
                return _selectedPlaceholderCharacterIndex;
            }
            set {
                _selectedPlaceholderCharacterIndex = value;
                OnPropertyChanged("SelectedPlaceholderCharacterIndex");
            }
        }
        private int _rosterPage_field;
        public int RosterPage_field {
            get {
                return _rosterPage_field;
            }
            set {
                _rosterPage_field = value;
                CharacterList.Clear();
                CharacterPlaceHolderList.Clear();
                foreach (CharacterSelectParamModel entry in CharacterFullList) {
                    if (entry.PageIndex == value && entry.CostumeIndex == 0) {
                        CharacterList.Add(entry);
                    }
                    if (entry.PageIndex < 0 && entry.CostumeIndex == 0) {
                        if (!CharacterPlaceHolderList.Contains(entry))
                            CharacterPlaceHolderList.Add(entry);
                    }
                }

                OnPropertyChanged("RosterPage_field");
            }
        }

        public int LastSlot() {
            if (CharacterFullList.Count > 0) {
                int maxPage = RosterPage_field;
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterFullList.Count; i++) {
                    if (!Slots.Contains(CharacterFullList[i].SlotIndex) && CharacterFullList[i].PageIndex == maxPage)
                        Slots.Add(CharacterFullList[i].SlotIndex);
                }
                return Slots.Max();
            } else {
                return 1;
            }
        }
        public int LastSlotOnPage(int page) {
            if (CharacterFullList.Count > 0) {
                int maxPage = page;
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterFullList.Count; i++) {
                    if (CharacterFullList[i].PageIndex == maxPage)
                        Slots.Add(CharacterFullList[i].SlotIndex);
                }
                return Slots.Max();
            } else {
                return 1;
            }
        }
        public int FreeSlotOnPage(int page) {
            if (CharacterFullList.Count > 0) {
                List<int> Slots = new List<int>();
                
                for (int i = 0; i< CharacterFullList.Count; i++) {
                    if (CharacterFullList[i].PageIndex == page) {
                        Slots.Add(CharacterFullList[i].SlotIndex);
                    }
                }
                for (int i = 1; i<= Slots.Count+1; i++) {
                    if (!Slots.Contains(i)) {
                        return i;
                    }
                }
            }
            return 1;
        }
        public void ReplaceSlots(int sourcePage, int sourceSlot, int targetPage, int targetSlot = -1) {
            if (CharacterFullList.Count > 0) {
                ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();

                if (sourcePage == targetPage) {
                    foreach (CharacterSelectParamModel entry in CharacterFullList) {
                        if (entry.PageIndex == targetPage && entry.SlotIndex == targetSlot) {
                            entry.SlotIndex = -2;
                        }
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot) {
                            entry.SlotIndex = -3;
                        }
                    }
                    foreach (CharacterSelectParamModel entry in CharacterFullList) {
                        if (entry.PageIndex == targetPage && entry.SlotIndex == -2) {
                            entry.SlotIndex = sourceSlot;
                        }
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == -3) {
                            entry.SlotIndex = targetSlot;
                        }
                    }
                } else {

                    int freeSlot = FreeSlotOnPage(targetPage);
                    foreach (CharacterSelectParamModel entry in CharacterFullList) {
                        if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot) {
                            entry.SlotIndex = freeSlot;
                            entry.PageIndex = targetPage;
                        }
                    }

                    for (int i = 0; i < CharacterFullList.Count; i++) {
                        CharacterSelectParamModel entry = CharacterFullList[i];
                        if (entry.PageIndex == sourcePage && sourceSlot < entry.SlotIndex) {
                            entry.SlotIndex--;
                        }
                    }

                }
                sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
                CharacterFullList = sortCharacterList;
            }
        }
        public void MoveSlots(int sourcePage, int sourceSlot, int targetPage) {
            if (CharacterFullList.Count > 0) {
                ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
                foreach (CharacterSelectParamModel entry in CharacterFullList) {

                    if (entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot) {
                        entry.PageIndex = targetPage;
                        entry.SlotIndex = LastSlotOnPage(targetPage);
                    }
                }

                sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
                CharacterFullList = sortCharacterList;
            }
        }

        public void RestoreRoster() {
            MessageBoxResult warning = (MessageBoxResult)ModernWpf.MessageBox.Show("Are you sure that you want to restore all icons? ", "Do you want to restore it?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (warning == MessageBoxResult.Yes) {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\characterSelectParam_vanilla.xfbin", AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\characterSelectParam.xfbin", true);
                
                string root_folder = Properties.Settings.Default.RootGameFolder;
                string modmanager_folder = "\\\\?\\" + root_folder + "\\modmanager\\";
                if (Directory.Exists(modmanager_folder)) {

                    DirectoryInfo d = new DirectoryInfo(modmanager_folder); //This function getting info about all files in a path
                    FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories); //Getting all files with "Icon.png" name

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
                        if (ModEntry.EnableMod) {
                            //character mod
                            DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                            FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                            foreach (FileInfo character_path in CharacterModList) {
                                var CharacterInfo = new IniFile(character_path.FullName);
                                CharacterInfo.Write("Page", "-1", "ModManager");
                                CharacterInfo.Write("Slot", "-1", "ModManager");

                            }

                        }


                    }
                }
                LoadModList();
                RosterPage_field = 0;

                TitleVM.RefreshModList();
                ModernWpf.MessageBox.Show("Roster was restored!");
            }
            
        }

        public void SaveRoster() {


            string root_folder = Properties.Settings.Default.RootGameFolder;
            string modmanager_folder = "\\\\?\\" + root_folder + "\\modmanager\\";
            if (Directory.Exists(modmanager_folder)) {

                DirectoryInfo d = new DirectoryInfo(modmanager_folder); //This function getting info about all files in a path
                FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories); //Getting all files with "Icon.png" name

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
                    if (ModEntry.EnableMod) {
                        //character mod
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        foreach (FileInfo character_path in CharacterModList) {
                            var CharacterInfo = new IniFile(character_path.FullName);
                            CharacterModModel CharacterEntry = new CharacterModModel() {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                RootPath = Path.GetDirectoryName(character_path.FullName)
                            };

                            CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                            string modCharParamPath = CharacterEntry.RootPath + "\\data\\ui\\max\\select\\characterSelectParam.xfbin";
                            CharacterSelectParamMod.OpenFile(modCharParamPath);

                            List<string> CSPCodeList = new List<string>();
                            foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList) {
                                CSPCodeList.Add(rosterEntry.CSP_code);
                            }

                            if (!CharacterEntry.Partner) {

                                foreach (CharacterSelectParamModel rosterEntry in CharacterFullList) {
                                    if (CSPCodeList.Contains(rosterEntry.CSP_code)) {
                                        CharacterInfo.Write("Page", rosterEntry.PageIndex.ToString(), "ModManager");
                                        CharacterInfo.Write("Slot", rosterEntry.SlotIndex.ToString(), "ModManager");
                                    }
                                }
                            }

                        }

                    }


                }
            }

            CharacterSelectParamViewModel CharacterSelectParamBase = new CharacterSelectParamViewModel();
            CharacterSelectParamBase.CharacterSelectParamList = CharacterFullList;
            CharacterSelectParamBase.SaveFileAs(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\characterSelectParam.xfbin");



            TitleVM.RefreshModList();
            ModernWpf.MessageBox.Show("Roster was saved!");
        }
        public CharacterRosterEditorViewModel(TitleViewModel VM) {

            TitleVM = VM;

            CharacterList = new ObservableCollection<CharacterSelectParamModel>();
            CharacterFullList = new ObservableCollection<CharacterSelectParamModel>();
            CharacterPlaceHolderList = new ObservableCollection<CharacterSelectParamModel>();
            LoadModList();
            RosterPage_field = 0;
        }
        public void LoadModList() {
            CharacterList.Clear();
            CharacterFullList.Clear();
            CharacterPlaceHolderList.Clear();
            string root_folder = Properties.Settings.Default.RootGameFolder;
            string modmanager_folder = "\\\\?\\" + root_folder + "\\modmanager\\";

            CharacterSelectParamViewModel CharacterSelectParamBase = new CharacterSelectParamViewModel();
            string baseCharParamPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ParamFiles\\characterSelectParam.xfbin";
            CharacterSelectParamBase.OpenFile(baseCharParamPath);

            if (Directory.Exists(modmanager_folder)) {

                DirectoryInfo d = new DirectoryInfo(modmanager_folder); //This function getting info about all files in a path
                FileInfo[] ModConfigList = d.GetFiles("mod_config.ini", SearchOption.AllDirectories); //Getting all files with "Icon.png" name
                Array.Sort(ModConfigList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(Path.GetFileName(x.DirectoryName), Path.GetFileName(y.DirectoryName)));

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
                    if (ModEntry.EnableMod) {
                        //character mod
                        DirectoryInfo mod_dir = new DirectoryInfo(Path.GetDirectoryName(mod_path.FullName));
                        FileInfo[] CharacterModList = mod_dir.GetFiles("character_config.ini", SearchOption.AllDirectories);
                        foreach (FileInfo character_path in CharacterModList) {
                            var CharacterInfo = new IniFile(character_path.FullName);
                            CharacterModModel CharacterEntry = new CharacterModModel() {
                                Characode = Path.GetFileName(Path.GetDirectoryName(character_path.FullName)),
                                Partner = Convert.ToBoolean(CharacterInfo.Read("Partner", "ModManager")),
                                Page = Convert.ToInt32(CharacterInfo.Read("Page", "ModManager")),
                                Slot = Convert.ToInt32(CharacterInfo.Read("Slot", "ModManager")),
                                RootPath = Path.GetDirectoryName(character_path.FullName)
                            };
                            if (!CharacterEntry.Partner) {
                                CharacterSelectParamViewModel CharacterSelectParamMod = new CharacterSelectParamViewModel();
                                string modCharParamPath = CharacterEntry.RootPath + "\\data\\ui\\max\\select\\characterSelectParam.xfbin";
                                CharacterSelectParamMod.OpenFile(modCharParamPath);

                                List<string> CSPCodeList = new List<string>();
                                foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamBase.CharacterSelectParamList) {
                                    CSPCodeList.Add(rosterEntry.CSP_code);
                                }

                                int freeSlot = CharacterSelectParamBase.FreeSlotOnPage(-1);
                                foreach (CharacterSelectParamModel rosterEntry in CharacterSelectParamMod.CharacterSelectParamList) {
                                    if (!CSPCodeList.Contains(rosterEntry.CSP_code)) {
                                        rosterEntry.PageIndex = -1;
                                        rosterEntry.SlotIndex = freeSlot;
                                        rosterEntry.CharacterIconPath = ModEntry.IconPath;
                                        rosterEntry.SaveInFile = false;
                                        CharacterSelectParamBase.CharacterSelectParamList.Add(rosterEntry);
                                    }
                                }
                            }

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
            CharacterFullList = CharacterSelectParamBase.CharacterSelectParamList;
            ObservableCollection<CharacterSelectParamModel> sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
            sortCharacterList.AddRange(CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
            CharacterFullList = sortCharacterList;
        }

        private RelayCommand _saveRosterCommand;
        public RelayCommand SaveRosterCommand {
            get {
                return _saveRosterCommand ??
                  (_saveRosterCommand = new RelayCommand(obj => {
                      SaveRoster();
                  }));
            }
        }
        private RelayCommand _restoreRosterCommand;
        public RelayCommand RestoreRosterCommand {
            get {
                return _restoreRosterCommand ??
                  (_restoreRosterCommand = new RelayCommand(obj => {
                      RestoreRoster();
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
