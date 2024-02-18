using DynamicData;
using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using Task = System.Threading.Tasks.Task;

namespace NSC_ModManager.ViewModel
{
    public class cmnparamViewModel : INotifyPropertyChanged {
        private string _searchPairSpl_field;
        public string SearchPairSpl_field {
            get { return _searchPairSpl_field; }
            set {
                _searchPairSpl_field = value;
                OnPropertyChanged("SearchPairSpl_field");
            }
        }
        private string _searchPlayerSnd_field;
        public string SearchPlayerSnd_field {
            get { return _searchPlayerSnd_field; }
            set {
                _searchPlayerSnd_field = value;
                OnPropertyChanged("SearchPlayerSnd_field");
            }
        }
        private int _pairSplID_field;
        public int PairSplID_field {
            get { return _pairSplID_field; }
            set {
                _pairSplID_field = value;
                OnPropertyChanged("PairSplID_field");
            }
        }
        private string _soundEvFileName_field;
        public string PairSoundEvFileName_field {
            get { return _soundEvFileName_field; }
            set {
                _soundEvFileName_field = value;
                OnPropertyChanged("PairSoundEvFileName_field");
            }
        }
        private string _cutInChunkName_field;
        public string PairCutInChunkName_field {
            get { return _cutInChunkName_field; }
            set {
                _cutInChunkName_field = value;
                OnPropertyChanged("PairCutInChunkName_field");
            }
        }
        private string _atkChunkName_field;
        public string PairAtkChunkName_field {
            get { return _atkChunkName_field; }
            set {
                _atkChunkName_field = value;
                OnPropertyChanged("PairAtkChunkName_field");
            }
        }
        private string _pairSplName1_field;
        public string PairSplName1_field {
            get { return _pairSplName1_field; }
            set {
                _pairSplName1_field = value;
                OnPropertyChanged("PairSplName1_field");
            }
        }
        private string _pairSplName2_field;
        public string PairSplName2_field {
            get { return _pairSplName2_field; }
            set {
                _pairSplName2_field = value;
                OnPropertyChanged("PairSplName2_field");
            }
        }
        private string _characode_field;
        public string PlayerCharacode_field {
            get { return _characode_field; }
            set {
                _characode_field = value;
                OnPropertyChanged("PlayerCharacode_field");
            }
        }
        private string _sndBaseFileName_field;
        public string PlayerSndBaseFileName_field {
            get { return _sndBaseFileName_field; }
            set {
                _sndBaseFileName_field = value;
                OnPropertyChanged("PlayerSndBaseFileName_field");
            }
        }
        private string _sndAwa1FileName_field;
        public string PlayerSndAwa1FileName_field {
            get { return _sndAwa1FileName_field; }
            set {
                _sndAwa1FileName_field = value;
                OnPropertyChanged("PlayerSndAwa1FileName_field");
            }
        }
        private string _sndAwa2FileName_field;
        public string PlayerSndAwa2FileName_field {
            get { return _sndAwa2FileName_field; }
            set {
                _sndAwa2FileName_field = value;
                OnPropertyChanged("PlayerSndAwa2FileName_field");
            }
        }
        private string _sndEventFileName_field;
        public string PlayerSndEventFileName_field {
            get { return _sndEventFileName_field; }
            set {
                _sndEventFileName_field = value;
                OnPropertyChanged("PlayerSndEventFileName_field");
            }
        }
        private string _sndUJEventFileName_field;
        public string PlayerSndUJEventFileName_field {
            get { return _sndUJEventFileName_field; }
            set {
                _sndUJEventFileName_field = value;
                OnPropertyChanged("PlayerSndUJEventFileName_field");
            }
        }
        private string _sndUJ_1_CutIn_ChunkName_field;
        public string PlayerSndUJ_1_CutIn_ChunkName_field {
            get { return _sndUJ_1_CutIn_ChunkName_field; }
            set {
                _sndUJ_1_CutIn_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_1_CutIn_ChunkName_field");
            }
        }
        private string _sndUJ_1_Atk_ChunkName_field;
        public string PlayerSndUJ_1_Atk_ChunkName_field {
            get { return _sndUJ_1_Atk_ChunkName_field; }
            set {
                _sndUJ_1_Atk_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_1_Atk_ChunkName_field");
            }
        }
        private string _sndUJ_2_CutIn_ChunkName_field;
        public string PlayerSndUJ_2_CutIn_ChunkName_field {
            get { return _sndUJ_2_CutIn_ChunkName_field; }
            set {
                _sndUJ_2_CutIn_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_2_CutIn_ChunkName_field");
            }
        }
        private string _sndUJ_2_Atk_ChunkName_field;
        public string PlayerSndUJ_2_Atk_ChunkName_field {
            get { return _sndUJ_2_Atk_ChunkName_field; }
            set {
                _sndUJ_2_Atk_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_2_Atk_ChunkName_field");
            }
        }
        private string _playerSndUJ_3_CutIn_ChunkName_field;
        public string PlayerSndUJ_3_CutIn_ChunkName_field {
            get { return _playerSndUJ_3_CutIn_ChunkName_field; }
            set {
                _playerSndUJ_3_CutIn_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_3_CutIn_ChunkName_field");
            }
        }
        private string _playerSndUJ_3_Atk_ChunkName_field;
        public string PlayerSndUJ_3_Atk_ChunkName_field {
            get { return _playerSndUJ_3_Atk_ChunkName_field; }
            set {
                _playerSndUJ_3_Atk_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_3_Atk_ChunkName_field");
            }
        }
        private string _playerSndUJ_alt_CutIn_ChunkName_field;
        public string PlayerSndUJ_alt_CutIn_ChunkName_field {
            get { return _playerSndUJ_alt_CutIn_ChunkName_field; }
            set {
                _playerSndUJ_alt_CutIn_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_alt_CutIn_ChunkName_field");
            }
        }
        private string _playerSndUJ_alt_Atk_ChunkName_field;
        public string PlayerSndUJ_alt_Atk_ChunkName_field {
            get { return _playerSndUJ_alt_Atk_ChunkName_field; }
            set {
                _playerSndUJ_alt_Atk_ChunkName_field = value;
                OnPropertyChanged("PlayerSndUJ_alt_Atk_ChunkName_field");
            }
        }
        private string _playerPartnerCharacodeBase_field;
        public string PlayerPartnerCharacodeBase_field {
            get { return _playerPartnerCharacodeBase_field; }
            set {
                _playerPartnerCharacodeBase_field = value;
                OnPropertyChanged("PlayerPartnerCharacodeBase_field");
            }
        }
        private string _playerPartnerCharacodeAwake_field;
        public string PlayerPartnerCharacodeAwake_field {
            get { return _playerPartnerCharacodeAwake_field; }
            set {
                _playerPartnerCharacodeAwake_field = value;
                OnPropertyChanged("PlayerPartnerCharacodeAwake_field");
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

        public ObservableCollection<pair_spl_sndModel> PairSplList { get; set; }
        private pair_spl_sndModel _selectedPairSpl;
        public pair_spl_sndModel SelectedPairSpl {
            get { return _selectedPairSpl; }
            set {
                _selectedPairSpl = value;
                if (value is not null) {
                    PairSplID_field = value.PairSplID;
                    PairSoundEvFileName_field = value.PairSoundEvFileName;
                    PairCutInChunkName_field = value.PairCutInChunkName;
                    PairAtkChunkName_field = value.PairAtkChunkName;
                    PairSplName1_field = value.PairSplName1;
                    PairSplName2_field = value.PairSplName2;
                }

                OnPropertyChanged("SelectedPairSpl");
            }
        }
        private int _selectedPairSplIndex;
        public int SelectedPairSplIndex {
            get { return _selectedPairSplIndex; }
            set {
                _selectedPairSplIndex = value;
                OnPropertyChanged("SelectedPairSplIndex");
            }
        }
        public ObservableCollection<player_sndModel> PlayerSndList { get; set; }
        private player_sndModel _selectedPlayerSnd;
        public player_sndModel SelectedPlayerSnd {
            get { return _selectedPlayerSnd; }
            set {
                _selectedPlayerSnd = value;
                if (value is not null) {
                    PlayerCharacode_field = value.PlayerCharacode;
                    PlayerSndBaseFileName_field = value.PlayerSndBaseFileName;
                    PlayerSndAwa1FileName_field = value.PlayerSndAwa1FileName;
                    PlayerSndAwa2FileName_field = value.PlayerSndAwa2FileName;
                    PlayerSndEventFileName_field = value.PlayerSndEventFileName;
                    PlayerSndUJEventFileName_field = value.PlayerSndUJEventFileName;
                    PlayerSndUJ_1_CutIn_ChunkName_field = value.PlayerSndUJ_1_CutIn_ChunkName;
                    PlayerSndUJ_1_Atk_ChunkName_field = value.PlayerSndUJ_1_Atk_ChunkName;
                    PlayerSndUJ_2_CutIn_ChunkName_field = value.PlayerSndUJ_2_CutIn_ChunkName;
                    PlayerSndUJ_2_Atk_ChunkName_field = value.PlayerSndUJ_2_Atk_ChunkName;
                    PlayerSndUJ_3_CutIn_ChunkName_field = value.PlayerSndUJ_3_CutIn_ChunkName;
                    PlayerSndUJ_3_Atk_ChunkName_field = value.PlayerSndUJ_3_Atk_ChunkName;
                    PlayerSndUJ_alt_CutIn_ChunkName_field = value.PlayerSndUJ_alt_CutIn_ChunkName;
                    PlayerSndUJ_alt_Atk_ChunkName_field = value.PlayerSndUJ_alt_Atk_ChunkName;
                    PlayerPartnerCharacodeBase_field = value.PlayerPartnerCharacodeBase;
                    PlayerPartnerCharacodeAwake_field = value.PlayerPartnerCharacodeAwake;
                }

                OnPropertyChanged("SelectedPlayerSnd");
            }
        }
        private int _selectedPlayerSndIndex;
        public int SelectedPlayerSndIndex {
            get { return _selectedPlayerSndIndex; }
            set {
                _selectedPlayerSndIndex = value;
                OnPropertyChanged("SelectedPlayerSndIndex");
            }
        }
        public byte[] fileByte;
        public string filePath;
        public cmnparamViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            PairSplList = new ObservableCollection<pair_spl_sndModel>();
            PlayerSndList = new ObservableCollection<player_sndModel>();
            filePath = "";
        }
        public void Clear() {
            PairSplList.Clear();
            PlayerSndList.Clear();
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
                int PairSplStart = 68 + BinaryReader.b_ReadIntRev(FileBytes, 16);
                int PairSplCount = BinaryReader.b_ReadInt16(FileBytes, PairSplStart);
                for (int x = 0; x < PairSplCount; x++) {
                    int _ptr = PairSplStart + 0x02 + (0xA4 * x);

                    pair_spl_sndModel PairSplEntry = new pair_spl_sndModel();

                    PairSplEntry.PairSplID = (int)BinaryReader.b_ReadFloat(FileBytes, _ptr);
                    PairSplEntry.PairSoundEvFileName = BinaryReader.b_ReadString(FileBytes, _ptr + 0x04);
                    PairSplEntry.PairCutInChunkName = BinaryReader.b_ReadString(FileBytes, _ptr + 0x24);
                    PairSplEntry.PairAtkChunkName = BinaryReader.b_ReadString(FileBytes, _ptr + 0x44);
                    PairSplEntry.PairSplName1 = BinaryReader.b_ReadString(FileBytes, _ptr + 0x64);
                    PairSplEntry.PairSplName2 = BinaryReader.b_ReadString(FileBytes, _ptr + 0x84);
                    PairSplEntry.PairSoundEvFileName = BinaryReader.b_ReadString(FileBytes, _ptr + 0x04);
                    PairSplList.Add(PairSplEntry);
                }
                int PlayerSndStart = PairSplStart + BinaryReader.b_ReadIntRev(FileBytes, PairSplStart - 4) + 0x30;
                int PlayerSndCount = BinaryReader.b_ReadInt16(FileBytes, PlayerSndStart);
                for (int x = 0; x < PlayerSndCount; x++) {
                    int _ptr = PlayerSndStart + 0x02 + (0x3E4 * x);
                    player_sndModel PlayerSndEntry = new player_sndModel();

                    PlayerSndEntry.PlayerCharacode = BinaryReader.b_ReadString(FileBytes, (int)_ptr);
                    PlayerSndEntry.PlayerSndBaseFileName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x20);
                    PlayerSndEntry.PlayerSndAwa1FileName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x60);
                    PlayerSndEntry.PlayerSndAwa2FileName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x80);
                    PlayerSndEntry.PlayerSndEventFileName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x200);
                    PlayerSndEntry.PlayerSndUJEventFileName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x280);
                    PlayerSndEntry.PlayerSndUJ_1_CutIn_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x2A0);
                    PlayerSndEntry.PlayerSndUJ_1_Atk_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x2C0);
                    PlayerSndEntry.PlayerSndUJ_2_CutIn_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x2E0);
                    PlayerSndEntry.PlayerSndUJ_2_Atk_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x300);
                    PlayerSndEntry.PlayerSndUJ_3_CutIn_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x320);
                    PlayerSndEntry.PlayerSndUJ_3_Atk_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x340);
                    PlayerSndEntry.PlayerSndUJ_alt_CutIn_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x360);
                    PlayerSndEntry.PlayerSndUJ_alt_Atk_ChunkName = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x380);
                    PlayerSndEntry.PlayerPartnerCharacodeBase = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x3A0);
                    PlayerSndEntry.PlayerPartnerCharacodeAwake = BinaryReader.b_ReadString(FileBytes, (int)_ptr + 0x3C0);
                    PlayerSndList.Add(PlayerSndEntry);
                }
            }
        }

        public void RemovePairSplEntry() {
            if (SelectedPairSpl is not null) {
                PairSplList.Remove(SelectedPairSpl);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SavePairSplEntry() {
            if (SelectedPairSpl is not null) {
                SelectedPairSpl.PairSplID = PairSplID_field;
                SelectedPairSpl.PairSoundEvFileName = PairSoundEvFileName_field;
                SelectedPairSpl.PairCutInChunkName = PairCutInChunkName_field;
                SelectedPairSpl.PairAtkChunkName = PairAtkChunkName_field;
                SelectedPairSpl.PairSplName1 = PairSplName1_field;
                SelectedPairSpl.PairSplName2 = PairSplName2_field;
                SelectedPairSpl.PairSoundEvFileName = PairSoundEvFileName_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void AddDupPairSplEntry() {
            pair_spl_sndModel PairSplEntry = new pair_spl_sndModel();
            if (SelectedPairSpl is not null) {
                PairSplEntry = (pair_spl_sndModel)SelectedPairSpl.Clone();
            } else {
                PairSplEntry.PairSplID = 0;
                PairSplEntry.PairSoundEvFileName = "";
                PairSplEntry.PairCutInChunkName = "";
                PairSplEntry.PairAtkChunkName = "";
                PairSplEntry.PairSplName1 = "";
                PairSplEntry.PairSplName2 = "";
                PairSplEntry.PairSoundEvFileName = "";
            }
            PairSplList.Add(PairSplEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void RemovePlayerSndEntry() {
            if (SelectedPlayerSnd is not null) {
                PlayerSndList.Remove(SelectedPlayerSnd);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SavePlayerSndEntry() {
            if (SelectedPlayerSnd is not null) {
                SelectedPlayerSnd.PlayerCharacode = PlayerCharacode_field;
                SelectedPlayerSnd.PlayerSndBaseFileName = PlayerSndBaseFileName_field;
                SelectedPlayerSnd.PlayerSndAwa1FileName = PlayerSndAwa1FileName_field;
                SelectedPlayerSnd.PlayerSndAwa2FileName = PlayerSndAwa2FileName_field;
                SelectedPlayerSnd.PlayerSndEventFileName = PlayerSndEventFileName_field;
                SelectedPlayerSnd.PlayerSndUJEventFileName = PlayerSndUJEventFileName_field;
                SelectedPlayerSnd.PlayerSndUJ_1_CutIn_ChunkName = PlayerSndUJ_1_CutIn_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_1_Atk_ChunkName = PlayerSndUJ_1_Atk_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_2_CutIn_ChunkName = PlayerSndUJ_2_CutIn_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_2_Atk_ChunkName = PlayerSndUJ_2_Atk_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_3_CutIn_ChunkName = PlayerSndUJ_3_CutIn_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_3_Atk_ChunkName = PlayerSndUJ_3_Atk_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_alt_CutIn_ChunkName = PlayerSndUJ_alt_CutIn_ChunkName_field;
                SelectedPlayerSnd.PlayerSndUJ_alt_Atk_ChunkName = PlayerSndUJ_alt_Atk_ChunkName_field;
                SelectedPlayerSnd.PlayerPartnerCharacodeBase = PlayerPartnerCharacodeBase_field;
                SelectedPlayerSnd.PlayerPartnerCharacodeAwake = PlayerPartnerCharacodeAwake_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void AddDupPlayerSndEntry() {
            player_sndModel PlayerSndEntry = new player_sndModel();
            if (SelectedPlayerSnd is not null) {
                PlayerSndEntry = (player_sndModel)SelectedPlayerSnd.Clone();
            } else {
                PlayerSndEntry.PlayerCharacode = "";
                PlayerSndEntry.PlayerSndBaseFileName = "";
                PlayerSndEntry.PlayerSndAwa1FileName = "";
                PlayerSndEntry.PlayerSndAwa2FileName = "";
                PlayerSndEntry.PlayerSndEventFileName = "";
                PlayerSndEntry.PlayerSndUJEventFileName = "";
                PlayerSndEntry.PlayerSndUJ_1_CutIn_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_1_Atk_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_2_CutIn_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_2_Atk_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_3_CutIn_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_3_Atk_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_alt_CutIn_ChunkName = "";
                PlayerSndEntry.PlayerSndUJ_alt_Atk_ChunkName = "";
                PlayerSndEntry.PlayerPartnerCharacodeBase = "";
                PlayerSndEntry.PlayerPartnerCharacodeAwake = "";

            }
            PlayerSndList.Add(PlayerSndEntry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public int SearchPairSplStringIndex(ObservableCollection<pair_spl_sndModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].PairSoundEvFileName;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected <= x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchPairSplEntry() {
            if (SearchPairSpl_field is not null) {
                if (SearchPairSplStringIndex(PairSplList, SearchPairSpl_field, SelectedPairSplIndex) != -1) {
                    SelectedPairSplIndex = SearchPairSplStringIndex(PairSplList, SearchPairSpl_field, SelectedPairSplIndex);
                    CollectionViewSource.GetDefaultView(PairSplList).MoveCurrentTo(SelectedPairSpl);
                } else {
                    if (SearchPairSplStringIndex(PairSplList, SearchPairSpl_field, 0) != -1) {
                        SelectedPairSplIndex = SearchPairSplStringIndex(PairSplList, SearchPairSpl_field, -1);
                        CollectionViewSource.GetDefaultView(PairSplList).MoveCurrentTo(SelectedPairSpl);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }
        public int SearchPlayerSndStringIndex(ObservableCollection<player_sndModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].PlayerCharacode;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected <= x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchPlayerSndEntry() {
            if (SearchPlayerSnd_field is not null) {
                if (SearchPlayerSndStringIndex(PlayerSndList, SearchPlayerSnd_field, SelectedPlayerSndIndex) != -1) {
                    SelectedPlayerSndIndex = SearchPlayerSndStringIndex(PlayerSndList, SearchPlayerSnd_field, SelectedPlayerSndIndex);
                    CollectionViewSource.GetDefaultView(PlayerSndList).MoveCurrentTo(SelectedPlayerSnd);
                } else {
                    if (SearchPlayerSndStringIndex(PlayerSndList, SearchPlayerSnd_field, 0) != -1) {
                        SelectedPlayerSndIndex = SearchPlayerSndStringIndex(PlayerSndList, SearchPlayerSnd_field, -1);
                        CollectionViewSource.GetDefaultView(PlayerSndList).MoveCurrentTo(SelectedPlayerSnd);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }
        public void SaveFile() {
            string old_path = filePath;
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
            string old_path = filePath;
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

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x3E, 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin/pair_spl_snd.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin/player_snd.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "pair_spl_snd");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "Page0");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "index");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "player_snd");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrName = fileBytes36.Length;
            totalLength4 = PtrName;
            int AddedBytes = 0;

            while (fileBytes36.Length % 4 != 0) {
                AddedBytes++;
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

            totalLength4 = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[60]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x04
            });


            int PtrSection = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[32]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03
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
            buffer8 = BitConverter.GetBytes(3);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 40;
            buffer8 = BitConverter.GetBytes(PathLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 44;
            buffer8 = BitConverter.GetBytes(5);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 48;
            buffer8 = BitConverter.GetBytes(NameLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 52;
            buffer8 = BitConverter.GetBytes(5);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 56;
            buffer8 = BitConverter.GetBytes(Section1Length);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 60;
            buffer8 = BitConverter.GetBytes(8);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[42]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x01,0x75,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0xC7,0x77,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0xC7,0x77,0x00,0x00,0x00,0x00,0x00,0x00
            });
            int startPtr = fileBytes36.Length;
            for (int x = 0; x < PairSplList.Count; x++) {
                byte[] pairSplEntry = new byte[0xA4];
                if (PairSplList[x].PairSoundEvFileName is null) {
                    PairSplList[x].PairSoundEvFileName = "";
                }
                if (PairSplList[x].PairCutInChunkName is null) {
                    PairSplList[x].PairCutInChunkName = "";
                }
                if (PairSplList[x].PairCutInChunkName is null) {
                    PairSplList[x].PairCutInChunkName = "";
                }
                if (PairSplList[x].PairAtkChunkName is null) {
                    PairSplList[x].PairAtkChunkName = "";
                }
                if (PairSplList[x].PairSplName1 is null) {
                    PairSplList[x].PairSplName1 = "";
                }
                if (PairSplList[x].PairSplName2 is null) {
                    PairSplList[x].PairSplName2 = "";
                }
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, BitConverter.GetBytes((float)PairSplList[x].PairSplID), 0x00);
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, Encoding.ASCII.GetBytes(PairSplList[x].PairSoundEvFileName), 0x04);
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, Encoding.ASCII.GetBytes(PairSplList[x].PairCutInChunkName), 0x24);
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, Encoding.ASCII.GetBytes(PairSplList[x].PairAtkChunkName), 0x44);
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, Encoding.ASCII.GetBytes(PairSplList[x].PairSplName1), 0x64);
                pairSplEntry = BinaryReader.b_ReplaceBytes(pairSplEntry, Encoding.ASCII.GetBytes(PairSplList[x].PairSplName2), 0x84);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, pairSplEntry);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((Int16)PairSplList.Count), startPtr - 0x02);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PairSplList.Count * 0xA4) + 2), startPtr - 0x06, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PairSplList.Count * 0xA4) + 6), startPtr - 0x12, 1);
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[50]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0xB9,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0xC7,0x77,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0xC7,0x77,0x00,0x00,0x00,0x00,0x00,0x00
            });
            startPtr = fileBytes36.Length;
            for (int x = 0; x < PlayerSndList.Count; x++) {
                byte[] playerSndEntry = new byte[0x3E4];
                if (PlayerSndList[x].PlayerCharacode == "" || PlayerSndList[x].PlayerCharacode is null) {
                    PlayerSndList[x].PlayerCharacode = "NULL";
                }
                if (PlayerSndList[x].PlayerSndBaseFileName == "" || PlayerSndList[x].PlayerSndBaseFileName is null) {
                    PlayerSndList[x].PlayerSndBaseFileName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndAwa1FileName == "" || PlayerSndList[x].PlayerSndAwa1FileName is null) {
                    PlayerSndList[x].PlayerSndAwa1FileName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndAwa2FileName == "" || PlayerSndList[x].PlayerSndAwa2FileName is null) {
                    PlayerSndList[x].PlayerSndAwa2FileName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndEventFileName == "" || PlayerSndList[x].PlayerSndEventFileName is null) {
                    PlayerSndList[x].PlayerSndEventFileName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJEventFileName == "" || PlayerSndList[x].PlayerSndUJEventFileName is null) {
                    PlayerSndList[x].PlayerSndUJEventFileName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_1_CutIn_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_1_CutIn_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_1_CutIn_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_1_Atk_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_1_Atk_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_1_Atk_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_2_CutIn_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_2_CutIn_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_2_CutIn_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_2_Atk_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_2_Atk_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_2_Atk_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_3_CutIn_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_3_CutIn_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_3_CutIn_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_3_Atk_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_3_Atk_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_3_Atk_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_alt_CutIn_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_alt_CutIn_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_alt_CutIn_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerSndUJ_alt_Atk_ChunkName == "" || PlayerSndList[x].PlayerSndUJ_alt_Atk_ChunkName is null) {
                    PlayerSndList[x].PlayerSndUJ_alt_Atk_ChunkName = "NULL";
                }
                if (PlayerSndList[x].PlayerPartnerCharacodeBase == "" || PlayerSndList[x].PlayerPartnerCharacodeBase is null) {
                    PlayerSndList[x].PlayerPartnerCharacodeBase = "NULL";
                }
                if (PlayerSndList[x].PlayerPartnerCharacodeAwake == "" || PlayerSndList[x].PlayerPartnerCharacodeAwake is null) {
                    PlayerSndList[x].PlayerPartnerCharacodeAwake = "NULL";
                }
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerCharacode), 0x00);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndBaseFileName), 0x20);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndBaseFileName), 0x40);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndAwa1FileName), 0x60);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndAwa2FileName), 0x80);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0xA0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0xC0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0xE0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x100);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x120);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x140);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x160);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x180);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x1A0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x1C0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x1E0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndEventFileName), 0x200);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndEventFileName), 0x220);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x240);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes("NULL"), 0x260);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJEventFileName), 0x280);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_1_CutIn_ChunkName), 0x2A0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_1_Atk_ChunkName), 0x2C0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_2_CutIn_ChunkName), 0x2E0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_2_Atk_ChunkName), 0x300);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_3_CutIn_ChunkName), 0x320);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_3_Atk_ChunkName), 0x340);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_alt_CutIn_ChunkName), 0x360);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerSndUJ_alt_Atk_ChunkName), 0x380);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerPartnerCharacodeBase), 0x3A0);
                playerSndEntry = BinaryReader.b_ReplaceBytes(playerSndEntry, Encoding.ASCII.GetBytes(PlayerSndList[x].PlayerPartnerCharacodeAwake), 0x3C0);
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, playerSndEntry);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((Int16)PlayerSndList.Count), startPtr - 0x02);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerSndList.Count * 0x3E4) + 2), startPtr - 0x06, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((PlayerSndList.Count * 0x3E4) + 6), startPtr - 0x12, 1);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
               0x000,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0xB9,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            });
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
        private RelayCommand _deletePairSplEntryCommand;
        public RelayCommand DeletePairSplEntryCommand {
            get {
                return _deletePairSplEntryCommand ??
                  (_deletePairSplEntryCommand = new RelayCommand(obj => {
                      RemovePairSplEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupPairSplEntryCommand;
        public RelayCommand AddDupPairSplEntryCommand {
            get {
                return _addDupPairSplEntryCommand ??
                  (_addDupPairSplEntryCommand = new RelayCommand(obj => {
                      AddDupPairSplEntryAsync();
                  }));
            }
        }
        private RelayCommand _savePairSplEntryCommand;
        public RelayCommand SavePairSplEntryCommand {
            get {
                return _savePairSplEntryCommand ??
                  (_savePairSplEntryCommand = new RelayCommand(obj => {
                      SavePairSplEntryAsync();
                  }));
            }
        }
        private RelayCommand _deletePlayerSndEntryCommand;
        public RelayCommand DeletePlayerSndEntryCommand {
            get {
                return _deletePlayerSndEntryCommand ??
                  (_deletePlayerSndEntryCommand = new RelayCommand(obj => {
                      RemovePlayerSndEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupPlayerSndEntryCommand;
        public RelayCommand AddDupPlayerSndEntryCommand {
            get {
                return _addDupPlayerSndEntryCommand ??
                  (_addDupPlayerSndEntryCommand = new RelayCommand(obj => {
                      AddDupPlayerSndEntryAsync();
                  }));
            }
        }
        private RelayCommand _savePlayerSndEntryCommand;
        public RelayCommand SavePlayerSndEntryCommand {
            get {
                return _savePlayerSndEntryCommand ??
                  (_savePlayerSndEntryCommand = new RelayCommand(obj => {
                      SavePlayerSndEntryAsync();
                  }));
            }
        }
        private RelayCommand _searchPlayerSndEntryCommand;
        public RelayCommand SearchPlayerSndEntryCommand {
            get {
                return _searchPlayerSndEntryCommand ??
                  (_searchPlayerSndEntryCommand = new RelayCommand(obj => {
                      SearchPlayerSndEntryAsync();
                  }));
            }
        }
        private RelayCommand _searchPairSplEntryCommand;
        public RelayCommand SearchPairSplEntryCommand {
            get {
                return _searchPairSplEntryCommand ??
                  (_searchPairSplEntryCommand = new RelayCommand(obj => {
                      SearchPairSplEntryAsync();
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
        public async void AddDupPairSplEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupPairSplEntry()));

        }
        public async void SavePairSplEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SavePairSplEntry()));

        }
        public async void RemovePairSplEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemovePairSplEntry()));

        }
        public async void AddDupPlayerSndEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupPlayerSndEntry()));

        }
        public async void SavePlayerSndEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SavePlayerSndEntry()));

        }
        public async void RemovePlayerSndEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemovePlayerSndEntry()));

        }
        public async void SearchPairSplEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SearchPairSplEntry()));

        }
        public async void SearchPlayerSndEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SearchPlayerSndEntry()));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
