using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using System.Drawing;
using Microsoft.Win32;
using NSC_ModManager.Properties;

namespace NSC_ModManager.ViewModel
{
    public class MessageInfoViewModel : INotifyPropertyChanged {

        private byte[] _crc32Code_field;
        public byte[] CRC32Code_field {
            get { return _crc32Code_field; }
            set {
                _crc32Code_field = value;
                OnPropertyChanged("CRC32Code_field");
            }
        }

        private string _mainText_lang_1_field;
        public string MainText_lang_1_field {
            get { return _mainText_lang_1_field; }
            set {
                _mainText_lang_1_field = value;
                OnPropertyChanged("MainText_lang_1_field");
            }
        }
        private string _secondaryText_lang_1_field;
        public string SecondaryText_lang_1_field {
            get { return _secondaryText_lang_1_field; }
            set {
                _secondaryText_lang_1_field = value;
                OnPropertyChanged("SecondaryText_lang_1_field");
            }
        }
        private string _speaker_lang_1_field;
        public string Speaker_lang_1_field {
            get { return _speaker_lang_1_field; }
            set {
                _speaker_lang_1_field = value;
                OnPropertyChanged("Speaker_lang_1_field");
            }
        }
        private string _mainText_lang_2_field;
        public string MainText_lang_2_field {
            get { return _mainText_lang_2_field; }
            set {
                _mainText_lang_2_field = value;
                OnPropertyChanged("MainText_lang_2_field");
            }
        }
        private string _secondaryText_lang_2_field;
        public string SecondaryText_lang_2_field {
            get { return _secondaryText_lang_2_field; }
            set {
                _secondaryText_lang_2_field = value;
                OnPropertyChanged("SecondaryText_lang_2_field");
            }
        }
        private string _speaker_lang_2_field;
        public string Speaker_lang_2_field {
            get { return _speaker_lang_2_field; }
            set {
                _speaker_lang_2_field = value;
                OnPropertyChanged("Speaker_lang_2_field");
            }
        }
        private string _mainText_lang_3_field;
        public string MainText_lang_3_field {
            get { return _mainText_lang_3_field; }
            set {
                _mainText_lang_3_field = value;
                OnPropertyChanged("MainText_lang_3_field");
            }
        }
        private string _secondaryText_lang_3_field;
        public string SecondaryText_lang_3_field {
            get { return _secondaryText_lang_3_field; }
            set {
                _secondaryText_lang_3_field = value;
                OnPropertyChanged("SecondaryText_lang_3_field");
            }
        }
        private string _speaker_lang_3_field;
        public string Speaker_lang_3_field {
            get { return _speaker_lang_3_field; }
            set {
                _speaker_lang_3_field = value;
                OnPropertyChanged("Speaker_lang_3_field");
            }
        }
        private string _mainText_lang_4_field;
        public string MainText_lang_4_field {
            get { return _mainText_lang_4_field; }
            set {
                _mainText_lang_4_field = value;
                OnPropertyChanged("MainText_lang_4_field");
            }
        }
        private string _secondaryText_lang_4_field;
        public string SecondaryText_lang_4_field {
            get { return _secondaryText_lang_4_field; }
            set {
                _secondaryText_lang_4_field = value;
                OnPropertyChanged("SecondaryText_lang_4_field");
            }
        }
        private string _speaker_lang_4_field;
        public string Speaker_lang_4_field {
            get { return _speaker_lang_4_field; }
            set {
                _speaker_lang_4_field = value;
                OnPropertyChanged("Speaker_lang_4_field");
            }
        }
        private string _mainText_lang_5_field;
        public string MainText_lang_5_field {
            get { return _mainText_lang_5_field; }
            set {
                _mainText_lang_5_field = value;
                OnPropertyChanged("MainText_lang_5_field");
            }
        }
        private string _secondaryText_lang_5_field;
        public string SecondaryText_lang_5_field {
            get { return _secondaryText_lang_5_field; }
            set {
                _secondaryText_lang_5_field = value;
                OnPropertyChanged("SecondaryText_lang_5_field");
            }
        }
        private string _speaker_lang_5_field;
        public string Speaker_lang_5_field {
            get { return _speaker_lang_5_field; }
            set {
                _speaker_lang_5_field = value;
                OnPropertyChanged("Speaker_lang_5_field");
            }
        }
        private string _mainText_lang_6_field;
        public string MainText_lang_6_field {
            get { return _mainText_lang_6_field; }
            set {
                _mainText_lang_6_field = value;
                OnPropertyChanged("MainText_lang_6_field");
            }
        }
        private string _secondaryText_lang_6_field;
        public string SecondaryText_lang_6_field {
            get { return _secondaryText_lang_6_field; }
            set {
                _secondaryText_lang_6_field = value;
                OnPropertyChanged("SecondaryText_lang_6_field");
            }
        }
        private string _speaker_lang_6_field;
        public string Speaker_lang_6_field {
            get { return _speaker_lang_6_field; }
            set {
                _speaker_lang_6_field = value;
                OnPropertyChanged("Speaker_lang_6_field");
            }
        }
        private string _mainText_lang_7_field;
        public string MainText_lang_7_field {
            get { return _mainText_lang_7_field; }
            set {
                _mainText_lang_7_field = value;
                OnPropertyChanged("MainText_lang_7_field");
            }
        }
        private string _secondaryText_lang_7_field;
        public string SecondaryText_lang_7_field {
            get { return _secondaryText_lang_7_field; }
            set {
                _secondaryText_lang_7_field = value;
                OnPropertyChanged("SecondaryText_lang_7_field");
            }
        }
        private string _speaker_lang_7_field;
        public string Speaker_lang_7_field {
            get { return _speaker_lang_7_field; }
            set {
                _speaker_lang_7_field = value;
                OnPropertyChanged("Speaker_lang_7_field");
            }
        }
        private string _mainText_lang_8_field;
        public string MainText_lang_8_field {
            get { return _mainText_lang_8_field; }
            set {
                _mainText_lang_8_field = value;
                OnPropertyChanged("MainText_lang_8_field");
            }
        }
        private string _secondaryText_lang_8_field;
        public string SecondaryText_lang_8_field {
            get { return _secondaryText_lang_8_field; }
            set {
                _secondaryText_lang_8_field = value;
                OnPropertyChanged("SecondaryText_lang_8_field");
            }
        }
        private string _speaker_lang_8_field;
        public string Speaker_lang_8_field {
            get { return _speaker_lang_8_field; }
            set {
                _speaker_lang_8_field = value;
                OnPropertyChanged("Speaker_lang_8_field");
            }
        }
        private string _mainText_lang_9_field;
        public string MainText_lang_9_field {
            get { return _mainText_lang_9_field; }
            set {
                _mainText_lang_9_field = value;
                OnPropertyChanged("MainText_lang_9_field");
            }
        }
        private string _secondaryText_lang_9_field;
        public string SecondaryText_lang_9_field {
            get { return _secondaryText_lang_9_field; }
            set {
                _secondaryText_lang_9_field = value;
                OnPropertyChanged("SecondaryText_lang_9_field");
            }
        }
        private string _speaker_lang_9_field;
        public string Speaker_lang_9_field {
            get { return _speaker_lang_9_field; }
            set {
                _speaker_lang_9_field = value;
                OnPropertyChanged("Speaker_lang_9_field");
            }
        }
        private string _mainText_lang_10_field;
        public string MainText_lang_10_field {
            get { return _mainText_lang_10_field; }
            set {
                _mainText_lang_10_field = value;
                OnPropertyChanged("MainText_lang_10_field");
            }
        }
        private string _secondaryText_lang_10_field;
        public string SecondaryText_lang_10_field {
            get { return _secondaryText_lang_10_field; }
            set {
                _secondaryText_lang_10_field = value;
                OnPropertyChanged("SecondaryText_lang_10_field");
            }
        }
        private string _speaker_lang_10_field;
        public string Speaker_lang_10_field {
            get { return _speaker_lang_10_field; }
            set {
                _speaker_lang_10_field = value;
                OnPropertyChanged("Speaker_lang_10_field");
            }
        }
        private string _mainText_lang_11_field;
        public string MainText_lang_11_field {
            get { return _mainText_lang_11_field; }
            set {
                _mainText_lang_11_field = value;
                OnPropertyChanged("MainText_lang_11_field");
            }
        }
        private string _secondaryText_lang_11_field;
        public string SecondaryText_lang_11_field {
            get { return _secondaryText_lang_11_field; }
            set {
                _secondaryText_lang_11_field = value;
                OnPropertyChanged("SecondaryText_lang_11_field");
            }
        }
        private string _speaker_lang_11_field;
        public string Speaker_lang_11_field {
            get { return _speaker_lang_11_field; }
            set {
                _speaker_lang_11_field = value;
                OnPropertyChanged("Speaker_lang_11_field");
            }
        }
        private string _mainText_lang_12_field;
        public string MainText_lang_12_field {
            get { return _mainText_lang_12_field; }
            set {
                _mainText_lang_12_field = value;
                OnPropertyChanged("MainText_lang_12_field");
            }
        }
        private string _secondaryText_lang_12_field;
        public string SecondaryText_lang_12_field {
            get { return _secondaryText_lang_12_field; }
            set {
                _secondaryText_lang_12_field = value;
                OnPropertyChanged("SecondaryText_lang_12_field");
            }
        }
        private string _speaker_lang_12_field;
        public string Speaker_lang_12_field {
            get { return _speaker_lang_12_field; }
            set {
                _speaker_lang_12_field = value;
                OnPropertyChanged("Speaker_lang_12_field");
            }
        }
        private string _mainText_lang_13_field;
        public string MainText_lang_13_field {
            get { return _mainText_lang_13_field; }
            set {
                _mainText_lang_13_field = value;
                OnPropertyChanged("MainText_lang_13_field");
            }
        }
        private string _secondaryText_lang_13_field;
        public string SecondaryText_lang_13_field {
            get { return _secondaryText_lang_13_field; }
            set {
                _secondaryText_lang_13_field = value;
                OnPropertyChanged("SecondaryText_lang_13_field");
            }
        }
        private string _speaker_lang_13_field;
        public string Speaker_lang_13_field {
            get { return _speaker_lang_13_field; }
            set {
                _speaker_lang_13_field = value;
                OnPropertyChanged("Speaker_lang_13_field");
            }
        }
        private string _mainText_lang_14_field;
        public string MainText_lang_14_field {
            get { return _mainText_lang_14_field; }
            set {
                _mainText_lang_14_field = value;
                OnPropertyChanged("MainText_lang_14_field");
            }
        }
        private string _secondaryText_lang_14_field;
        public string SecondaryText_lang_14_field {
            get { return _secondaryText_lang_14_field; }
            set {
                _secondaryText_lang_14_field = value;
                OnPropertyChanged("SecondaryText_lang_14_field");
            }
        }
        private string _speaker_lang_14_field;
        public string Speaker_lang_14_field {
            get { return _speaker_lang_14_field; }
            set {
                _speaker_lang_14_field = value;
                OnPropertyChanged("Speaker_lang_14_field");
            }
        }
        private string _mainText_lang_15_field;
        public string MainText_lang_15_field {
            get { return _mainText_lang_15_field; }
            set {
                _mainText_lang_15_field = value;
                OnPropertyChanged("MainText_lang_15_field");
            }
        }
        private string _secondaryText_lang_15_field;
        public string SecondaryText_lang_15_field {
            get { return _secondaryText_lang_15_field; }
            set {
                _secondaryText_lang_15_field = value;
                OnPropertyChanged("SecondaryText_lang_15_field");
            }
        }
        private string _speaker_lang_15_field;
        public string Speaker_lang_15_field {
            get { return _speaker_lang_15_field; }
            set {
                _speaker_lang_15_field = value;
                OnPropertyChanged("Speaker_lang_15_field");
            }
        }
        private Int16 _acbFileID_field;
        public Int16 ACBFileID_field {
            get { return _acbFileID_field; }
            set {
                _acbFileID_field = value;
                OnPropertyChanged("ACBFileID_field");
            }
        }
        private Int16 _cueID_field;
        public Int16 CueID_field {
            get { return _cueID_field; }
            set {
                _cueID_field = value;
                OnPropertyChanged("CueID_field");
            }
        }
        private bool _disableText_field;
        public bool DisableText_field {
            get { return _disableText_field; }
            set {
                _disableText_field = value;
                OnPropertyChanged("DisableText_field");
            }
        }
        private ObservableCollection<MessageInfoModel> _messageInfo_preview_List;
        public ObservableCollection<MessageInfoModel> MessageInfo_preview_List {
            get { return _messageInfo_preview_List; }
            set {
                _messageInfo_preview_List = value;
                OnPropertyChanged("MessageInfo_preview_List");
            }
        }
        private MessageInfoModel _selectedMessageInfo;
        public MessageInfoModel SelectedMessageInfo {
            get { return _selectedMessageInfo; }
            set {
                _selectedMessageInfo = value;
                OnPropertyChanged("SelectedMessageInfo");
            }
        }
        private string _searchEntry_field;
        public string SearchEntry_field {
            get { return _searchEntry_field; }
            set {
                _searchEntry_field = value;
                OnPropertyChanged("SearchEntry_field");
            }
        }
        public ObservableCollection<ObservableCollection<MessageInfoModel>> MessageInfo_List { get; set; }

        private int _selectedMessageIndex;
        public int SelectedMessageIndex {
            get { return _selectedMessageIndex; }
            set {
                _selectedMessageIndex = value;
                if (MessageInfo_List is not null) {
                    if (value > -1 && MessageInfo_List.Count != 0) {
                        MainText_lang_1_field = Encoding.UTF8.GetString(MessageInfo_List[0][value].MainText);
                        SecondaryText_lang_1_field = Encoding.UTF8.GetString(MessageInfo_List[0][value].SecondaryText);
                        Speaker_lang_1_field = Encoding.UTF8.GetString(MessageInfo_List[0][value].Speaker);
                        MainText_lang_2_field = Encoding.UTF8.GetString(MessageInfo_List[1][value].MainText);
                        SecondaryText_lang_2_field = Encoding.UTF8.GetString(MessageInfo_List[1][value].SecondaryText);
                        Speaker_lang_2_field = Encoding.UTF8.GetString(MessageInfo_List[1][value].Speaker);
                        MainText_lang_3_field = Encoding.UTF8.GetString(MessageInfo_List[2][value].MainText);
                        SecondaryText_lang_3_field = Encoding.UTF8.GetString(MessageInfo_List[2][value].SecondaryText);
                        Speaker_lang_3_field = Encoding.UTF8.GetString(MessageInfo_List[2][value].Speaker);
                        MainText_lang_4_field = Encoding.UTF8.GetString(MessageInfo_List[3][value].MainText);
                        SecondaryText_lang_4_field = Encoding.UTF8.GetString(MessageInfo_List[3][value].SecondaryText);
                        Speaker_lang_4_field = Encoding.UTF8.GetString(MessageInfo_List[3][value].Speaker);
                        MainText_lang_5_field = Encoding.UTF8.GetString(MessageInfo_List[4][value].MainText);
                        SecondaryText_lang_5_field = Encoding.UTF8.GetString(MessageInfo_List[4][value].SecondaryText);
                        Speaker_lang_5_field = Encoding.UTF8.GetString(MessageInfo_List[4][value].Speaker);
                        MainText_lang_6_field = Encoding.UTF8.GetString(MessageInfo_List[5][value].MainText);
                        SecondaryText_lang_6_field = Encoding.UTF8.GetString(MessageInfo_List[5][value].SecondaryText);
                        Speaker_lang_6_field = Encoding.UTF8.GetString(MessageInfo_List[5][value].Speaker);
                        MainText_lang_7_field = Encoding.UTF8.GetString(MessageInfo_List[6][value].MainText);
                        SecondaryText_lang_7_field = Encoding.UTF8.GetString(MessageInfo_List[6][value].SecondaryText);
                        Speaker_lang_7_field = Encoding.UTF8.GetString(MessageInfo_List[6][value].Speaker);
                        MainText_lang_8_field = Encoding.UTF8.GetString(MessageInfo_List[7][value].MainText);
                        SecondaryText_lang_8_field = Encoding.UTF8.GetString(MessageInfo_List[7][value].SecondaryText);
                        Speaker_lang_8_field = Encoding.UTF8.GetString(MessageInfo_List[7][value].Speaker);
                        MainText_lang_9_field = Encoding.UTF8.GetString(MessageInfo_List[8][value].MainText);
                        SecondaryText_lang_9_field = Encoding.UTF8.GetString(MessageInfo_List[8][value].SecondaryText);
                        Speaker_lang_9_field = Encoding.UTF8.GetString(MessageInfo_List[8][value].Speaker);
                        MainText_lang_10_field = Encoding.UTF8.GetString(MessageInfo_List[9][value].MainText);
                        SecondaryText_lang_10_field = Encoding.UTF8.GetString(MessageInfo_List[9][value].SecondaryText);
                        Speaker_lang_10_field = Encoding.UTF8.GetString(MessageInfo_List[9][value].Speaker);
                        MainText_lang_11_field = Encoding.UTF8.GetString(MessageInfo_List[10][value].MainText);
                        SecondaryText_lang_11_field = Encoding.UTF8.GetString(MessageInfo_List[10][value].SecondaryText);
                        Speaker_lang_11_field = Encoding.UTF8.GetString(MessageInfo_List[10][value].Speaker);
                        MainText_lang_12_field = Encoding.UTF8.GetString(MessageInfo_List[11][value].MainText);
                        SecondaryText_lang_12_field = Encoding.UTF8.GetString(MessageInfo_List[11][value].SecondaryText);
                        Speaker_lang_12_field = Encoding.UTF8.GetString(MessageInfo_List[11][value].Speaker);
                        MainText_lang_13_field = Encoding.UTF8.GetString(MessageInfo_List[12][value].MainText);
                        SecondaryText_lang_13_field = Encoding.UTF8.GetString(MessageInfo_List[12][value].SecondaryText);
                        Speaker_lang_13_field = Encoding.UTF8.GetString(MessageInfo_List[12][value].Speaker);
                        MainText_lang_14_field = Encoding.UTF8.GetString(MessageInfo_List[13][value].MainText);
                        SecondaryText_lang_14_field = Encoding.UTF8.GetString(MessageInfo_List[13][value].SecondaryText);
                        Speaker_lang_14_field = Encoding.UTF8.GetString(MessageInfo_List[13][value].Speaker);
                        MainText_lang_15_field = Encoding.UTF8.GetString(MessageInfo_List[14][value].MainText);
                        SecondaryText_lang_15_field = Encoding.UTF8.GetString(MessageInfo_List[14][value].SecondaryText);
                        Speaker_lang_15_field = Encoding.UTF8.GetString(MessageInfo_List[14][value].Speaker);


                        ACBFileID_field = (short)(MessageInfo_List[0][value].ACBFileID + 1);
                        CueID_field = MessageInfo_List[0][value].CueID;
                        DisableText_field = MessageInfo_List[0][value].DisableText;
                    }
                }
                OnPropertyChanged("SelectedMessageIndex");
            }
        }
        private int _selectedMessageInfoIndex;
        public int SelectedMessageInfoIndex {
            get { return _selectedMessageInfoIndex; }
            set {
                _selectedMessageInfoIndex = value;
                if (MessageInfo_List is not null) {
                    if (value > -1 && MessageInfo_List.Count != 0) {
                        MessageInfo_preview_List = MessageInfo_List[value];
                    }
                }
                
                OnPropertyChanged("SelectedMessageInfoIndex");
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
        public MessageInfoViewModel() {
            filePath = "";
            LoadingStatePlay = Visibility.Hidden;
            SelectedMessageInfoIndex = 2;
            MessageInfo_preview_List = new ObservableCollection<MessageInfoModel>();
            MessageInfo_List = new ObservableCollection<ObservableCollection<MessageInfoModel>>();
            for (int i =0; i < 15; i++)
                MessageInfo_List.Add(new ObservableCollection<MessageInfoModel>());
        }
        public void Clear() {
            filePath = "";
            MessageInfo_preview_List.Clear();
            MessageInfo_List.Clear();
            for (int i = 0; i < 15; i++)
                MessageInfo_List.Add(new ObservableCollection<MessageInfoModel>());
        }
        public void OpenFiles(string basepath = "") {
            if (basepath == "") {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                CommonFileDialogResult result = dialog.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                    filePath = dialog.FileName;
                else {
                    LoadingStatePlay = Visibility.Hidden;
                    return;
                }
            } else
                filePath = basepath;
            if (Directory.Exists(filePath)) {
                string[] files = Directory.GetFiles(filePath, "messageInfo.bin.xfbin", SearchOption.AllDirectories);
                Clear();
                if (files.Length == 15) {
                    for (int i = 0; i< files.Length; i++) {
                        CreateMessageInfoList(files[i], i);
                    }
                    MessageInfo_preview_List = MessageInfo_List[SelectedMessageInfoIndex];
                } else {
                    ModernWpf.MessageBox.Show("This tool requires all messageInfo files to be loaded in directory!");
                    LoadingStatePlay = Visibility.Hidden;
                    return;
                }
            }
        }
        public static ObservableCollection<MessageInfoModel> OpenFile(string basepath) {
            ObservableCollection<MessageInfoModel> return_message = new ObservableCollection<MessageInfoModel>();
            byte[] fileByte = File.ReadAllBytes(basepath);
            int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(fileByte, 16);
            int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
            for (int c = 0; c < entryCount; c++) {
                int ptr = StartOfFile + 0x10 + (c * 0x30);
                MessageInfoModel messageEntry = new MessageInfoModel();
                messageEntry.CRC32Code = BinaryReader.b_ReadByteArray(fileByte, ptr, 4);
                messageEntry.Speaker = BinaryReader.b_ReadByteArrayOfString(fileByte, BinaryReader.b_ReadInt(fileByte, ptr + 0x08) + ptr + 0x08);
                messageEntry.SecondaryText = BinaryReader.b_ReadByteArrayOfString(fileByte, BinaryReader.b_ReadInt(fileByte, ptr + 0x10) + ptr + 0x10);
                messageEntry.MainText = BinaryReader.b_ReadByteArrayOfString(fileByte, BinaryReader.b_ReadInt(fileByte, ptr + 0x18) + ptr + 0x18);
                messageEntry.ACBFileID = BinaryReader.b_ReadInt16(fileByte, ptr + 0x26);
                messageEntry.CueID = BinaryReader.b_ReadInt16(fileByte, ptr + 0x28);
                messageEntry.DisableText = (BinaryReader.b_ReadInt16(fileByte, ptr + 0x2A) == 1);
                return_message.Add(messageEntry);
            }
            return return_message;
        }

        public void CreateMessageInfoList(string basepath, int index) {
            MessageInfo_List[index] = OpenFile(basepath);
        }

        public void AddEntries() {
            MessageInfoModel messageEntry = new MessageInfoModel();
            messageEntry.CRC32Code = new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };
            messageEntry.MainText = new byte[0];
            messageEntry.SecondaryText = new byte[0];
            messageEntry.Speaker = new byte[0];
            messageEntry.ACBFileID = -1;
            messageEntry.CueID = -1;
            messageEntry.DisableText = false;
            for (int i = 0; i<15; i++) {
                MessageInfo_List[i].Add((MessageInfoModel)messageEntry.Clone());
            }
            MessageInfo_preview_List = MessageInfo_List[SelectedMessageInfoIndex]; 
            SelectedMessageIndex = MessageInfo_preview_List.Count - 1;
            CollectionViewSource.GetDefaultView(MessageInfo_preview_List).MoveCurrentTo(SelectedMessageInfo);
            ModernWpf.MessageBox.Show("Entry was added!");
        }
        public void DupEntries() {
            if (SelectedMessageIndex > -1) {
                for (int i = 0; i<15; i++) {
                    MessageInfoModel messageEntry = (MessageInfoModel)MessageInfo_List[i][SelectedMessageIndex].Clone();
                    MessageInfo_List[i].Add(messageEntry);
                }
            MessageInfo_preview_List = MessageInfo_List[SelectedMessageInfoIndex]; 
                SelectedMessageIndex = MessageInfo_preview_List.Count - 1;
                CollectionViewSource.GetDefaultView(MessageInfo_preview_List).MoveCurrentTo(SelectedMessageInfo);
                ModernWpf.MessageBox.Show("Entry was added!");
            }
        }
        public void DeleteEntries() {
            if (SelectedMessageIndex > -1) {
                int ind = SelectedMessageIndex;
                for (int i = 0; i < 15; i++) {
                    MessageInfo_List[i].RemoveAt(ind);
                }
                SelectedMessageIndex = ind - 1;
                ModernWpf.MessageBox.Show("Entry was added!");
            }
        }
        public int SearchStringIndex(ObservableCollection<MessageInfoModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = Encoding.UTF8.GetString(FunctionList[x].MainText);
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchTextEntry() {
            if (SearchEntry_field is not null && MessageInfo_preview_List.Count > 0) {
                if (SearchStringIndex(MessageInfo_preview_List, SearchEntry_field, SelectedMessageIndex) != -1) {
                    SelectedMessageIndex = SearchStringIndex(MessageInfo_preview_List, SearchEntry_field, SelectedMessageIndex);
                    CollectionViewSource.GetDefaultView(MessageInfo_preview_List).MoveCurrentTo(SelectedMessageInfo);
                } else {
                    if (SearchStringIndex(MessageInfo_preview_List, SearchEntry_field, 0) != -1) {
                        SelectedMessageIndex = SearchStringIndex(MessageInfo_preview_List, SearchEntry_field, -1);
                        CollectionViewSource.GetDefaultView(MessageInfo_preview_List).MoveCurrentTo(SelectedMessageInfo);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that text.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        public void SearchCRC32Entry() {
            if (SearchEntry_field is not null && MessageInfo_preview_List.Count > 0) {
                bool found = false;
                byte[] FindCRC32 = BinaryReader.crc32(SearchEntry_field);
                for (int i = 0; i < MessageInfo_preview_List.Count; i++) {
                    if (BitConverter.ToString(MessageInfo_preview_List[i].CRC32Code) == BitConverter.ToString(FindCRC32)) {
                        SelectedMessageIndex = i;
                        CollectionViewSource.GetDefaultView(MessageInfo_preview_List).MoveCurrentTo(SelectedMessageInfo);
                        found = true;
                        continue;
                    }
                }
                if (!found) {
                    ModernWpf.MessageBox.Show("Couldn't find section with that Message ID.");
                }
            }
        }
        public void SaveEntries() {
            if (MessageInfo_preview_List.Count > 0 && SelectedMessageIndex > -1) {
                if (SearchEntry_field is not null & SearchEntry_field != "") {
                    byte[] CRC32Code = BinaryReader.crc32(SearchEntry_field);
                    for (int i = 0; i < MessageInfo_preview_List.Count; i++) {
                        if (BitConverter.ToString(MessageInfo_preview_List[i].CRC32Code) == BitConverter.ToString(CRC32Code)) {
                            ModernWpf.MessageBox.Show("Entry with that Message ID already exist in file!");
                            return;
                        }
                    }
                    MessageInfo_List[0][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[1][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[2][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[3][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[4][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[5][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[6][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[7][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[8][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[9][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[10][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[11][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[12][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[13][SelectedMessageIndex].CRC32Code = CRC32Code;
                    MessageInfo_List[14][SelectedMessageIndex].CRC32Code = CRC32Code;
                    SearchEntry_field = "";
                }


                MessageInfo_List[0][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_1_field);
                MessageInfo_List[0][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_1_field);
                MessageInfo_List[0][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_1_field);
                MessageInfo_List[0][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[0][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[0][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[1][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_2_field);
                MessageInfo_List[1][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_2_field);
                MessageInfo_List[1][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_2_field);
                MessageInfo_List[1][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[1][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[1][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[2][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_3_field);
                MessageInfo_List[2][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_3_field);
                MessageInfo_List[2][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_3_field);
                MessageInfo_List[2][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[2][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[2][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[3][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_4_field);
                MessageInfo_List[3][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_4_field);
                MessageInfo_List[3][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_4_field);
                MessageInfo_List[3][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[3][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[3][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[4][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_5_field);
                MessageInfo_List[4][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_5_field);
                MessageInfo_List[4][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_5_field);
                MessageInfo_List[4][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[4][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[4][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[5][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_6_field);
                MessageInfo_List[5][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_6_field);
                MessageInfo_List[5][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_6_field);
                MessageInfo_List[5][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[5][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[5][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[6][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_7_field);
                MessageInfo_List[6][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_7_field);
                MessageInfo_List[6][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_7_field);
                MessageInfo_List[6][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[6][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[6][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[7][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_8_field);
                MessageInfo_List[7][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_8_field);
                MessageInfo_List[7][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_8_field);
                MessageInfo_List[7][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[7][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[7][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[8][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_9_field);
                MessageInfo_List[8][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_9_field);
                MessageInfo_List[8][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_9_field);
                MessageInfo_List[8][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[8][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[8][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[9][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_10_field);
                MessageInfo_List[9][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_10_field);
                MessageInfo_List[9][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_10_field);
                MessageInfo_List[9][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[9][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[9][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[10][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_11_field);
                MessageInfo_List[10][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_11_field);
                MessageInfo_List[10][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_11_field);
                MessageInfo_List[10][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[10][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[10][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[11][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_12_field);
                MessageInfo_List[11][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_12_field);
                MessageInfo_List[11][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_12_field);
                MessageInfo_List[11][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[11][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[11][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[12][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_13_field);
                MessageInfo_List[12][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_13_field);
                MessageInfo_List[12][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_13_field);
                MessageInfo_List[12][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[12][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[12][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[13][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_14_field);
                MessageInfo_List[13][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_14_field);
                MessageInfo_List[13][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_14_field);
                MessageInfo_List[13][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[13][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[13][SelectedMessageIndex].DisableText = DisableText_field;

                MessageInfo_List[14][SelectedMessageIndex].MainText = Encoding.UTF8.GetBytes(MainText_lang_15_field);
                MessageInfo_List[14][SelectedMessageIndex].SecondaryText = Encoding.UTF8.GetBytes(SecondaryText_lang_15_field);
                MessageInfo_List[14][SelectedMessageIndex].Speaker = Encoding.UTF8.GetBytes(Speaker_lang_15_field);
                MessageInfo_List[14][SelectedMessageIndex].ACBFileID = (short)(ACBFileID_field - 1);
                MessageInfo_List[14][SelectedMessageIndex].CueID = CueID_field;
                MessageInfo_List[14][SelectedMessageIndex].DisableText = DisableText_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            }
        }

        public void SaveFileAs(string basepath = "") {
            if (basepath == "") {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                dialog.Title = "Select data_win32 folder or folder where will be placed messageInfo folder";
                dialog.ShowDialog();
                basepath = dialog.FileName;
            }
            for (int i = 0; i < 15; i++) {
                string path = basepath + "\\message\\WIN64\\" + Program.langList[i];
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string file_path = path + "\\messageInfo.bin.xfbin";
                if (File.Exists(file_path)) {
                    if (File.Exists(file_path + ".backup")) {
                        File.Delete(file_path + ".backup");
                    }
                    File.Copy(file_path, file_path + ".backup");
                }
                File.WriteAllBytes(file_path, ConvertToFile(i));
            }

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
        private RelayCommand _openFileCommand;
        public RelayCommand OpenFileCommand {
            get {
                return _openFileCommand ??
                  (_openFileCommand = new RelayCommand(obj => {
                      OpenFileAsync();
                  }));
            }
        }
        private RelayCommand _addEntryCommand;
        public RelayCommand AddEntryCommand {
            get {
                return _addEntryCommand ??
                  (_addEntryCommand = new RelayCommand(obj => {
                      AddEntries();
                  }));
            }
        }
        private RelayCommand _dupEntryCommand;
        public RelayCommand DupEntryCommand {
            get {
                return _dupEntryCommand ??
                  (_dupEntryCommand = new RelayCommand(obj => {
                      DupEntries();
                  }));
            }
        }
        private RelayCommand _deleteEntryCommand;
        public RelayCommand DeleteEntryCommand {
            get {
                return _deleteEntryCommand ??
                  (_deleteEntryCommand = new RelayCommand(obj => {
                      DeleteEntries();
                  }));
            }
        }
        private RelayCommand _searchTextEntryCommand;
        public RelayCommand SearchTextEntryCommand {
            get {
                return _searchTextEntryCommand ??
                  (_searchTextEntryCommand = new RelayCommand(obj => {
                      SearchTextEntry();
                  }));
            }
        }
        private RelayCommand _searchCRC32EntryCommand;
        public RelayCommand SearchCRC32EntryCommand {
            get {
                return _searchCRC32EntryCommand ??
                  (_searchCRC32EntryCommand = new RelayCommand(obj => {
                      SearchCRC32Entry();
                  }));
            }
        }
        private RelayCommand _saveEntryCommand;
        public RelayCommand SaveEntryCommand {
            get {
                return _saveEntryCommand ??
                  (_saveEntryCommand = new RelayCommand(obj => {
                      SaveEntries();
                  }));
            }
        }

        public byte[] ConvertToFile(int ListIndex) {
            LoadingStatePlay = Visibility.Visible;
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "WIN64/" + Program.langList[ListIndex] + "/messageInfo.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "messageInfo");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
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


            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE9, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;
            //fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[MessageInfo_List[ListIndex].Count * 0x30]);
            //List<int> MainText_pointer = new List<int>();
            //List<int> SecondaryText_pointer = new List<int>();
            //List<int> Speaker_pointer = new List<int>();
            //ModernWpf.MessageBox.Show("test 1");
            //for (int x = 0; x < MessageInfo_List[ListIndex].Count; x++) {
            //    MainText_pointer.Add(fileBytes36.Length);
            //    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, MessageInfo_List[ListIndex][x].MainText);
            //    SecondaryText_pointer.Add(fileBytes36.Length);
            //    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, MessageInfo_List[ListIndex][x].SecondaryText);
            //    Speaker_pointer.Add(fileBytes36.Length);
            //    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, MessageInfo_List[ListIndex][x].Speaker);

            //}
            //ModernWpf.MessageBox.Show("test 2");
            //for (int x = 0; x < MessageInfo_List[ListIndex].Count; x++) {
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainText_pointer[x] - startPtr - (0x30 * x) - 0x08), startPtr + (0x30 * x) + 0x08);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainText_pointer[x] - startPtr - (0x30 * x) - 0x10), startPtr + (0x30 * x) + 0x10);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainText_pointer[x] - startPtr - (0x30 * x) - 0x18), startPtr + (0x30 * x) + 0x18);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, new byte[2] { 0xFF, 0xFF }, 0x24);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MessageInfo_List[ListIndex][x].ACBFileID), startPtr + (0x30 * x) + 0x26);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MessageInfo_List[ListIndex][x].CueID), startPtr + (0x30 * x) + 0x28);
            //    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MessageInfo_List[ListIndex][x].DisableText), startPtr + (0x30 * x) + 0x2A);
            //}


            List<byte> file = new List<byte>();
            for (int x3 = 0; x3 < MessageInfo_List[ListIndex].Count * 0x30; x3++) {
                file.Add(0);
            }

            List<int> MainTextPointer = new List<int>();
            List<int> SecondaryTextPointer = new List<int>();
            List<int> SpeakerTextPointer = new List<int>();

            for (int x2 = 0; x2 < MessageInfo_List[ListIndex].Count; x2++) {
                MainTextPointer.Add(file.Count);
                int nameLength3 = MessageInfo_List[ListIndex][x2].MainText.Length;
                if (Encoding.UTF8.GetString(MessageInfo_List[ListIndex][x2].MainText) != "") {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add(MessageInfo_List[ListIndex][x2].MainText[a17]);
                    }
                    file.Add(0);
                }

                SecondaryTextPointer.Add(file.Count);
                nameLength3 = MessageInfo_List[ListIndex][x2].SecondaryText.Length;
                if (Encoding.UTF8.GetString(MessageInfo_List[ListIndex][x2].SecondaryText) != "") {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add(MessageInfo_List[ListIndex][x2].SecondaryText[a17]);
                    }
                    file.Add(0);
                }
                SpeakerTextPointer.Add(file.Count);
                nameLength3 = MessageInfo_List[ListIndex][x2].Speaker.Length;
                if (Encoding.UTF8.GetString(MessageInfo_List[ListIndex][x2].Speaker) != "") {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add(MessageInfo_List[ListIndex][x2].Speaker[a17]);
                    }
                    file.Add(0);
                }
                int newPointer3 = MainTextPointer[x2] - 0x30 * x2 - 0x18;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
                for (int a7 = 0; a7 < 4; a7++) {
                    file[0x30 * x2 + 0x18 + a7] = ptrBytes3[a7];
                }
                newPointer3 = SecondaryTextPointer[x2] - 0x30 * x2 - 0x10;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                for (int a7 = 0; a7 < 4; a7++) {
                    file[0x30 * x2 + 0x10 + a7] = ptrBytes3[a7];
                }
                newPointer3 = SpeakerTextPointer[x2] - 0x30 * x2 - 0x08;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                for (int a7 = 0; a7 < 4; a7++) {
                    file[0x30 * x2 + 0x08 + a7] = ptrBytes3[a7];
                }




                // VALUES
                byte[] o_a = MessageInfo_List[ListIndex][x2].CRC32Code;
                for (int a8 = 0; a8 < 4; a8++) {
                    file[0x30 * x2 + 0 + a8] = o_a[a8];
                }
                for (int a8 = 0; a8 < 2; a8++) {
                    file[0x30 * x2 + 0x24 + a8] = 0xFF;
                }
                o_a = BitConverter.GetBytes(MessageInfo_List[ListIndex][x2].ACBFileID);
                for (int a8 = 0; a8 < 2; a8++) {
                    file[0x30 * x2 + 0x26 + a8] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(MessageInfo_List[ListIndex][x2].CueID);
                for (int a8 = 0; a8 < 2; a8++) {
                    file[0x30 * x2 + 0x28 + a8] = o_a[a8];
                }
                file[0x30 * x2 + 0x2A] = BitConverter.GetBytes(MessageInfo_List[ListIndex][x2].DisableText)[0];
            }
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, file.ToArray());
            file = new List<byte>();

            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - startPtr + 0x14), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(fileBytes36.Length - startPtr + 0x10), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MessageInfo_List[ListIndex].Count), count_index);
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
                0x79, 0x21, 0x77,
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

        public async void SaveFileAsAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFileAs(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void OpenFileAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(() => App.Current.Dispatcher.Invoke(() => OpenFiles(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void CreateMessageInfoListAsync(string basepath, int index) {
            await System.Threading.Tasks.Task.Run(() => App.Current.Dispatcher.Invoke(() => CreateMessageInfoList(basepath, index)));
            MessageInfo_preview_List = MessageInfo_List[SelectedMessageInfoIndex];

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
