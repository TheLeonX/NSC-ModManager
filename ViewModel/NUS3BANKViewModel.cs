using DynamicData;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NSC_ModManager.Model;
using NSC_ModManager.Properties;
using Octokit;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using XFBIN_LIB;
using XFBIN_LIB.XFBIN;
using static System.Windows.Forms.DataFormats;

namespace NSC_ModManager.ViewModel
{
   
    public class NUS3BANKViewModel : INotifyPropertyChanged
    {

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
        private int _dton_id;
        public int DTON_ID
        {
            get { return _dton_id; }
            set
            {
                _dton_id = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.DTON_ID = value;
                }
                OnPropertyChanged("DTON_ID");
            }
        }
        private float _volume;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Volume = value;
                }
                OnPropertyChanged("Volume");
            }
        }
        private float _playOffsetX;
        public float PlayOffsetX
        {
            get { return _playOffsetX; }
            set
            {
                _playOffsetX = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.PlayOffsetX = value;
                }
                OnPropertyChanged("PlayOffsetX");
            }
        }
        private float _unk1;
        public float Unk1
        {
            get { return _unk1; }
            set
            {
                _unk1 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk1 = value;
                }
                OnPropertyChanged("Unk1");
            }
        }
        private float _unk2;
        public float Unk2
        {
            get { return _unk2; }
            set
            {
                _unk2 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk2 = value;
                }
                OnPropertyChanged("Unk2");
            }
        }
        private float _unk3;
        public float Unk3
        {
            get { return _unk3; }
            set
            {
                _unk3 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk3 = value;
                }
                OnPropertyChanged("Unk3");
            }
        }
        private float _unk4;
        public float Unk4
        {
            get { return _unk4; }
            set
            {
                _unk4 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk4 = value;
                }
                OnPropertyChanged("Unk4");
            }
        }
        private float _playOffsetY;
        public float PlayOffsetY
        {
            get { return _playOffsetY; }
            set
            {
                _playOffsetY = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.PlayOffsetY = value;
                }
                OnPropertyChanged("PlayOffsetY");
            }
        }
        private float _unk5;
        public float Unk5
        {
            get { return _unk5; }
            set
            {
                _unk5 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk5 = value;
                }
                OnPropertyChanged("Unk5");
            }
        }
        private int _playStart;
        public int PlayStart
        {
            get { return _playStart; }
            set
            {
                _playStart = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.PlayStart = value;
                }
                OnPropertyChanged("PlayStart");
            }
        }
        private int _playEnd;
        public int PlayEnd
        {
            get { return _playEnd; }
            set
            {
                _playEnd = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.PlayEnd = value;
                }
                OnPropertyChanged("PlayEnd");
            }
        }
        private int _playDelay;
        public int PlayDelay
        {
            get { return _playDelay; }
            set
            {
                _playDelay = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.PlayDelay = value;
                }
                OnPropertyChanged("PlayDelay");
            }
        }
        private int _unk8;
        public int Unk8
        {
            get { return _unk8; }
            set
            {
                _unk8 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk8 = value;
                }
                OnPropertyChanged("Unk8");
            }
        }
        private int _unk9;
        public int Unk9
        {
            get { return _unk9; }
            set
            {
                _unk9 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk9 = value;
                }
                OnPropertyChanged("Unk9");
            }
        }
        private float _unk10;
        public float Unk10
        {
            get { return _unk10; }
            set
            {
                _unk10 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk10 = value;
                }
                OnPropertyChanged("Unk10");
            }
        }
        private float _unk11;
        public float Unk11
        {
            get { return _unk11; }
            set
            {
                _unk11 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk11 = value;
                }
                OnPropertyChanged("Unk11");
            }
        }
        private int _unk12;
        public int Unk12
        {
            get { return _unk12; }
            set
            {
                _unk12 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk12 = value;
                }
                OnPropertyChanged("Unk12");
            }
        }
        private int _unk13;
        public int Unk13
        {
            get { return _unk13; }
            set
            {
                _unk13 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk13 = value;
                }
                OnPropertyChanged("Unk13");
            }
        }
        private bool _unk13Bool;
        public bool Unk13Bool
        {
            get { return _unk13Bool; }
            set
            {
                _unk13Bool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 1, value);
                }
                OnPropertyChanged("Unk13Bool");
            }
        }
        private int _unk14;
        public int Unk14
        {
            get { return _unk14; }
            set
            {
                _unk14 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk14 = value;
                }
                OnPropertyChanged("Unk14");
            }
        }
        private bool _unk14Bool;
        public bool Unk14Bool
        {
            get { return _unk14Bool; }
            set
            {
                _unk14Bool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 2, value);
                }
                OnPropertyChanged("Unk14Bool");
            }
        }
        private int _unk16;
        public int Unk16
        {
            get { return _unk16; }
            set
            {
                _unk16 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk16 = value;
                }
                OnPropertyChanged("Unk16");
            }
        }
        private bool _unk16Bool;
        public bool Unk16Bool
        {
            get { return _unk16Bool; }
            set
            {
                _unk16Bool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 3, value);
                }
                OnPropertyChanged("Unk16Bool");
            }
        }
        private bool _sampleRateBool;
        public bool SampleRateBool
        {
            get { return _sampleRateBool; }
            set
            {
                _sampleRateBool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 4, value);
                }
                OnPropertyChanged("SampleRateBool");
            }
        }
        private bool _channelTypeBool;
        public bool ChannelTypeBool
        {
            get { return _channelTypeBool; }
            set
            {
                _channelTypeBool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 5, value);
                }
                OnPropertyChanged("ChannelTypeBool");
            }
        }
        private bool _totalSamples1Bool;
        public bool TotalSamples1Bool
        {
            get { return _totalSamples1Bool; }
            set
            {
                _totalSamples1Bool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 6, value);
                }
                OnPropertyChanged("TotalSamples1Bool");
            }
        }
        private bool _totalSamples2Bool;
        public bool TotalSamples2Bool
        {
            get { return _totalSamples2Bool; }
            set
            {
                _totalSamples2Bool = value;
                if (SelectedTone is not null)
                {
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[4], 7, value);
                }
                OnPropertyChanged("TotalSamples2Bool");
            }
        }
        private int _unk17;
        public int Unk17
        {
            get { return _unk17; }
            set
            {
                _unk17 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk17 = value;
                }
                OnPropertyChanged("Unk17");
            }
        }
        private int _unk18;
        public int Unk18
        {
            get { return _unk18; }
            set
            {
                _unk18 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk18 = value;
                }
                OnPropertyChanged("Unk18");
            }
        }
        private int _unk19;
        public int Unk19
        {
            get { return _unk19; }
            set
            {
                _unk19 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk19 = value;
                }
                OnPropertyChanged("Unk19");
            }
        }
        private int _unk20;
        public int Unk20
        {
            get { return _unk20; }
            set
            {
                _unk20 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk20 = value;
                }
                OnPropertyChanged("Unk20");
            }
        }
        private int _unk21;
        public int Unk21
        {
            get { return _unk21; }
            set
            {
                _unk21 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Unk21 = value;
                }
                OnPropertyChanged("Unk21");
            }
        }
        private bool _enableOverlay;
        public bool EnableOverlay
        {
            get { return _enableOverlay; }
            set
            {
                _enableOverlay = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.EnableOverlay = value;
                    BinaryWriterV2.SetFlagBit(ref SelectedTone.Flags[5], 3, value);
                }
                OnPropertyChanged("EnableOverlay");
            }
        }
        private Visibility _EmptySlotVisibility;
        public Visibility EmptySlotVisibility
        {
            get { return _EmptySlotVisibility; }
            set
            {
                _EmptySlotVisibility = value;
                OnPropertyChanged("EmptySlotVisibility");
            }
        }
        private Visibility _randomizerVisibility;
        public Visibility RandomizerVisibility
        {
            get { return _randomizerVisibility; }
            set
            {
                _randomizerVisibility = value;
                OnPropertyChanged("RandomizerVisibility");
            }
        }
        private Visibility _RegularSlotVisibility;
        public Visibility RegularSlotVisibility
        {
            get { return _RegularSlotVisibility; }
            set
            {
                _RegularSlotVisibility = value;
                OnPropertyChanged("RegularSlotVisibility");
            }
        }
        private int _entryType;
        public int EntryType
        {
            get { return _entryType; }
            set
            {
                _entryType = value;
                switch (value)
                {
                    case 0:
                        EmptySlotVisibility = Visibility.Visible;
                        RandomizerVisibility = Visibility.Collapsed;
                        RegularSlotVisibility = Visibility.Collapsed;
                        break;
                    case 1:
                        EmptySlotVisibility = Visibility.Collapsed;
                        RandomizerVisibility = Visibility.Visible;
                        RegularSlotVisibility = Visibility.Collapsed;
                        break;
                    case 2:
                        EmptySlotVisibility = Visibility.Collapsed;
                        RandomizerVisibility = Visibility.Collapsed;
                        RegularSlotVisibility = Visibility.Visible;
                        break;
                }
                if (SelectedTone is not null)
                {
                    SelectedTone.EnableRandomizer = false;
                    SelectedTone.Type = value;
                    if (value == 1)
                        SelectedTone.EnableRandomizer = true;
                }
                OnPropertyChanged("EntryType");
            }
        }
        private int _fileID;
        public int FileID
        {
            get { return _fileID; }
            set
            {
                _fileID = value;

                if (NUS3BANK_FILE.BINF_DATA is not null)
                {
                    NUS3BANK_FILE.BINF_DATA.FileID = value;
                }

                OnPropertyChanged("FileID");
            }
        }
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;

                if (NUS3BANK_FILE.BINF_DATA is not null)
                {
                    NUS3BANK_FILE.BINF_DATA.FileName = value;
                }

                OnPropertyChanged("FileName");
            }
        }
        private float _soundVolume;
        public float SoundVolume
        {
            get { return _soundVolume; }
            set
            {
                _soundVolume = value;

                OnPropertyChanged("SoundVolume");
                BnsfPlayer.SetVolume(value);
            }
        }

        private string _timeDisplay;
        public string TimeDisplay
        {
            get { return _timeDisplay; }
            set
            {
                _timeDisplay = value;

                OnPropertyChanged("TimeDisplay");
            }
        }


        private ObservableCollection<NUS3_GRP_Entry_Model> _groupList;
        public ObservableCollection<NUS3_GRP_Entry_Model> GroupList
        {
            get { return _groupList; }
            set
            {
                _groupList = value;

                OnPropertyChanged("GroupList");
            }
        }
        private float _randomizer_unk1;
        public float Randomizer_unk1
        {
            get { return _randomizer_unk1; }
            set
            {
                _randomizer_unk1 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk1 = value;
                }
                OnPropertyChanged("Randomizer_unk1");
            }
        }
        private float _randomizer_unk2;
        public float Randomizer_unk2
        {
            get { return _randomizer_unk2; }
            set
            {
                _randomizer_unk2 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk2 = value;
                }
                OnPropertyChanged("Randomizer_unk2");
            }
        }
        private float _randomizer_unk3;
        public float Randomizer_unk3
        {
            get { return _randomizer_unk3; }
            set
            {
                _randomizer_unk3 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk3 = value;
                }
                OnPropertyChanged("Randomizer_unk3");
            }
        }
        private float _randomizer_unk4;
        public float Randomizer_unk4
        {
            get { return _randomizer_unk4; }
            set
            {
                _randomizer_unk4 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk4 = value;
                }
                OnPropertyChanged("Randomizer_unk4");
            }
        }
        private float _randomizer_unk5;
        public float Randomizer_unk5
        {
            get { return _randomizer_unk5; }
            set
            {
                _randomizer_unk5 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk5 = value;
                }
                OnPropertyChanged("Randomizer_unk5");
            }
        }
        private int _randomizer_unk6;
        public int Randomizer_unk6
        {
            get { return _randomizer_unk6; }
            set
            {
                _randomizer_unk6 = value;
                if (SelectedTone is not null)
                {
                    SelectedTone.Randomizer_unk6 = value;
                }
                OnPropertyChanged("Randomizer_unk6");
            }
        }

        private ObservableCollection<NUS3_TONE_Randomizer_Entry_Model> _randomizerList;
        public ObservableCollection<NUS3_TONE_Randomizer_Entry_Model> RandomizerList
        {
            get { return _randomizerList; }
            set
            {
                _randomizerList = value;

                OnPropertyChanged("RandomizerList");
            }
        }
        private NUS3_TONE_Randomizer_Entry_Model _selectedRandomizer;
        public NUS3_TONE_Randomizer_Entry_Model SelectedRandomizer
        {
            get { return _selectedRandomizer; }
            set
            {
                _selectedRandomizer = value;

                OnPropertyChanged("SelectedRandomizer");
            }
        }
        private int _selectedRandomizerIndex;
        public int SelectedRandomizerIndex
        {
            get { return _selectedRandomizerIndex; }
            set
            {
                _selectedRandomizerIndex = value;

                OnPropertyChanged("SelectedRandomizerIndex");
            }
        }

        private NUS3_GRP_Entry_Model _selectedGroup;
        public NUS3_GRP_Entry_Model SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;

                OnPropertyChanged("SelectedGroup");
            }
        }
        private int _selectedGroupIndex;
        public int SelectedGroupIndex
        {
            get { return _selectedGroupIndex; }
            set
            {
                _selectedGroupIndex = value;

                OnPropertyChanged("SelectedGroupIndex");
            }
        }

        private ObservableCollection<NUS3_TONE_Entry_Model> _toneList;
        public ObservableCollection<NUS3_TONE_Entry_Model> ToneList
        {
            get { return _toneList; }
            set
            {
                _toneList = value;

                OnPropertyChanged("ToneList");
            }
        }
        private NUS3_TONE_Entry_Model _selectedTone;
        public NUS3_TONE_Entry_Model SelectedTone
        {
            get { return _selectedTone; }
            set
            {
                _selectedTone = value;
                if (value is not null)
                {
                    EntryType = value.Type;
                    GroupIndexList = value.Groups;
                    ToneChannelList = value.Channels;
                    RandomizerList = value.RandomizerEntries;
                    Randomizer_unk1 = value.Randomizer_unk1;
                    Randomizer_unk2 = value.Randomizer_unk2;
                    Randomizer_unk3 = value.Randomizer_unk3;
                    Randomizer_unk4 = value.Randomizer_unk4;
                    Randomizer_unk5 = value.Randomizer_unk5;
                    Randomizer_unk6 = value.Randomizer_unk6;

                    Volume = value.Volume;
                    PlayOffsetX = value.PlayOffsetX;
                    Unk1 = value.Unk1;
                    Unk2 = value.Unk2;
                    Unk3 = value.Unk3;
                    Unk4 = value.Unk4;
                    PlayOffsetY = value.PlayOffsetY;
                    Unk5 = value.Unk5;
                    PlayStart = value.PlayStart;
                    PlayEnd = value.PlayEnd;
                    PlayDelay = value.PlayDelay;
                    Unk8 = value.Unk8;
                    Unk9 = value.Unk9;
                    Unk10 = value.Unk10;
                    Unk11 = value.Unk11;
                    Unk12 = value.Unk12;
                    Unk13 = value.Unk13; // flags[4], 1
                    Unk14 = value.Unk14; // flags[4], 2
                    Unk16 = value.Unk16; // flags[4], 3
                    Unk17 = value.Unk17; // flags[4], 6              
                    Unk18 = value.Unk18; // flags[4], 2
                    Unk19 = value.Unk19;
                    Unk20 = value.Unk20; //Some Condition
                    Unk21 = value.Unk21; //Some Condition
                    DTON_ID = value.DTON_ID;

                    Unk13Bool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 1);
                    Unk14Bool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 2);
                    Unk16Bool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 3);
                    SampleRateBool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 4);
                    ChannelTypeBool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 5);
                    TotalSamples1Bool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 6);
                    TotalSamples2Bool = BinaryReaderV2.IsFlagBitSet(value.Flags[4], 7);

                    EnableOverlay = this.EnableOverlay; //BinaryReaderV2.IsFlagBitSet(value.Flags[5], 3)

                }
                OnPropertyChanged("SelectedTone");
            }
        }
        private int _selectedToneIndex;
        public int SelectedToneIndex
        {
            get { return _selectedToneIndex; }
            set
            {
                _selectedToneIndex = value;

                OnPropertyChanged("SelectedToneIndex");
            }
        }

        private ObservableCollection<GroupIndexModel> _groupIndexList;
        public ObservableCollection<GroupIndexModel> GroupIndexList
        {
            get { return _groupIndexList; }
            set
            {
                _groupIndexList = value;

                OnPropertyChanged("GroupIndexList");
            }
        }
        private GroupIndexModel _selectedGroupIndexItem;
        public GroupIndexModel SelectedGroupIndexItem
        {
            get { return _selectedGroupIndexItem; }
            set
            {
                _selectedGroupIndexItem = value;
                OnPropertyChanged("SelectedGroupIndexItem");
            }
        }
        private int _selectedGroupIndexItemIndex;
        public int SelectedGroupIndexItemIndex
        {
            get { return _selectedGroupIndexItemIndex; }
            set
            {
                _selectedGroupIndexItemIndex = value;
                OnPropertyChanged("SelectedGroupIndexItemIndex");
            }
        }

        private ObservableCollection<NUS3_TONE_channel_Entry_Model> _toneChannelList;
        public ObservableCollection<NUS3_TONE_channel_Entry_Model> ToneChannelList
        {
            get { return _toneChannelList; }
            set
            {
                _toneChannelList = value;

                OnPropertyChanged("ToneChannelList");
            }
        }
        private NUS3_TONE_channel_Entry_Model _selectedChannel;
        public NUS3_TONE_channel_Entry_Model SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                _selectedChannel = value;

                OnPropertyChanged("SelectedChannel");
            }
        }
        private int _selectedChannelIndex;
        public int SelectedChannelIndex
        {
            get { return _selectedChannelIndex; }
            set
            {
                _selectedChannelIndex = value;

                OnPropertyChanged("SelectedChannelIndex");
            }
        }

        private ObservableCollection<NUS3_DTON_Entry_Model> _dtonList;
        public ObservableCollection<NUS3_DTON_Entry_Model> DTONList
        {
            get { return _dtonList; }
            set
            {
                _dtonList = value;

                OnPropertyChanged("DTONList");
            }
        }

        public NUS3_DataType_Collection DataTypes = new NUS3_DataType_Collection();
        private readonly System.Timers.Timer _timeTimer;

        public static XFBIN_READER S_XFBIN_READER = new XFBIN_READER();
        public string FilePath = string.Empty;
        public byte[] fileBytes;

        private NUS3BANK_Model _NUS3BANK_FILE;
        public NUS3BANK_Model NUS3BANK_FILE
        {
            get { return _NUS3BANK_FILE; }
            set
            {
                _NUS3BANK_FILE = value;
                OnPropertyChanged("NUS3BANK_FILE");
            }
        }
        public NUS3BANKViewModel()
        {
            BnsfPlayer.Bind(this);
            EntryType = 0;
            SoundVolume = 0.25f;
            S_XFBIN_READER = new XFBIN_READER();
            LoadingStatePlay = Visibility.Hidden;
            FilePath = string.Empty;
            fileBytes = new byte[0];
            NUS3BANK_FILE = new NUS3BANK_Model();
            DataTypes = new NUS3_DataType_Collection();
            GroupList = new ObservableCollection<NUS3_GRP_Entry_Model>();
            ToneList = new ObservableCollection<NUS3_TONE_Entry_Model>();
            GroupIndexList = new ObservableCollection<GroupIndexModel>();
            ToneChannelList = new ObservableCollection<NUS3_TONE_channel_Entry_Model>();
            DTONList = new ObservableCollection<NUS3_DTON_Entry_Model>();
            _timeTimer = new System.Timers.Timer(200);
            _timeTimer.Elapsed += (s, e) => UpdateTimeDisplay();
            _timeTimer.Start();
        }
        private void UpdateTimeDisplay()
        {
            TimeSpan cur = BnsfPlayer.GetCurrentTime();
            TimeSpan tot = BnsfPlayer.GetTotalTime();

            TimeDisplay = $"{cur:mm\\:ss\\.fff}/{tot:mm\\:ss\\.fff}";
        }
        public void Clear()
        {
            EntryType = 0;
            SoundVolume = 0.25f;
            S_XFBIN_READER = new XFBIN_READER();
            LoadingStatePlay = Visibility.Hidden;
            FilePath = string.Empty;
            fileBytes = new byte[0];
            NUS3BANK_FILE = new NUS3BANK_Model();
            DataTypes.Clear();
            GroupList.Clear();
            ToneList.Clear();
            GroupIndexList.Clear();
            ToneChannelList.Clear();
        }

        public void OpenFile(string basepath = "")
        {
            Clear();
            if (basepath == "")
            {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "XFBIN Container (*.xfbin)|*.xfbin";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true)
                {
                    FilePath = myDialog.FileName;
                } else
                {
                    return;
                }
            } else
            {
                FilePath = basepath;
            }
            if (File.Exists(FilePath))
            {
                S_XFBIN_READER.ReadXFBIN(FilePath);
                List<XFBIN_READER.FoundChunk> foundChunks = S_XFBIN_READER.FindChunks("nuccChunkNub");
                if (foundChunks.Count < 1)
                    foundChunks = S_XFBIN_READER.FindChunks("nuccChunkBinary");


                if (foundChunks.Count < 1)
                    return;
                CHUNK SoundChunk = foundChunks[0].Chunk;
                fileBytes = SoundChunk.ChunkData;
                int start_ptr = 4;
                string Format = BinaryReader.b_ReadString(fileBytes, start_ptr, 4);
                if (Format == "NUS3")
                {
                    NUS3BANK_FILE = new NUS3BANK_Model();
                    NUS3BANK_FILE.Format = Format;

                    //int DataTypeCount = BinaryReader.b_ReadInt(fileBytes, start_ptr + 0x14);

                    int DataTypeCount = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, start_ptr + 0x14);
                    NUS3_PROP_Model PROP_DATA = new NUS3_PROP_Model();
                    NUS3_BINF_Model BINF_DATA = new NUS3_BINF_Model();
                    NUS3_GRP_Model GRP_DATA = new NUS3_GRP_Model();
                    NUS3_DTON_Model DTON_DATA = new NUS3_DTON_Model();
                    NUS3_TONE_Model TONE_DATA = new NUS3_TONE_Model();
                    for (int i = 0; i < DataTypeCount; i++)
                    {
                        int entryOffset = start_ptr + 0x18 + (0x8 * i);
                        string type = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), entryOffset, 4);
                        int size = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entryOffset + 0x4);

                        var dt = new NUS3_DataType_Model
                        {
                            Type = type,
                            Size = size
                        };

                        DataTypes.Add(dt);
                    }

                    //PROP
                    start_ptr = start_ptr + 0x18 + (0x8 * DataTypeCount);
                    PROP_DATA.Data = BinaryReaderV2.ReadBytes(fileBytes, start_ptr, DataTypes["PROP"].Size + 8);
                    start_ptr = start_ptr + DataTypes["PROP"].Size + 8;

                    //BINF
                    BINF_DATA.HeaderName = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), start_ptr, 4);
                    BINF_DATA.Unk = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0xC);
                    int name_length = BinaryReaderV2.ReadInt8(fileBytes, start_ptr + 0x10);
                    BINF_DATA.FileName = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), start_ptr + 0x11, name_length).TrimEnd('\0');
                    start_ptr = start_ptr + 0x11 + name_length + (3 - (name_length % 4));
                    BINF_DATA.FileID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr);
                    start_ptr += 4;

                    //GRP
                    int GRP_ptr = start_ptr;
                    GRP_DATA.HeaderName = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), start_ptr, 4);
                    int GRP_entryCount = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x8);
                    start_ptr += 0x08;

                    GRP_DATA.Entries = new ObservableCollection<NUS3_GRP_Entry_Model>();

                    for (int i = 0; i < GRP_entryCount; i++)
                    {
                        NUS3_GRP_Entry_Model GRP_entry = new NUS3_GRP_Entry_Model();

                        int entry_ptr = start_ptr + BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i));
                        int entry_size = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i) + 0x04);
                        GRP_entry.Unk1 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                        GRP_entry.Unk2 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x04);
                        int GRP_name_length = BinaryReaderV2.ReadInt8(fileBytes, entry_ptr + 0x08);
                        if (GRP_name_length > 0)
                            GRP_entry.Name = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), entry_ptr + 0x09, GRP_name_length).TrimEnd('\0');
                        GRP_DATA.Entries.Add(GRP_entry);
                    }
                    start_ptr = GRP_ptr + DataTypes["GRP "].Size + 8;
                    int DTON_ptr = start_ptr;
                    //DTON
                    DTON_DATA.HeaderName = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), start_ptr, 4);
                    DTON_DATA.Entries = new ObservableCollection<NUS3_DTON_Entry_Model>();

                    int DTON_entry_count = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x8);
                    start_ptr += 0x08;

                    for (int i = 0; i < DTON_entry_count; i++)
                    {
                        NUS3_DTON_Entry_Model DTON_entry = new NUS3_DTON_Entry_Model();

                        int entry_ptr = start_ptr + BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i));
                        int entry_size = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i) + 0x04);


                        DTON_entry.ID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);

                        DTON_entry.Flags = BinaryReaderV2.ReadBytes(fileBytes.AsSpan(), entry_ptr + 0x04, 8);
                        int DTON_name_length = BinaryReaderV2.ReadInt8(fileBytes, entry_ptr + 0x0C);
                        if (DTON_name_length > 0)
                            DTON_entry.Name = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), entry_ptr + 0x0D, DTON_name_length).TrimEnd('\0');

                        DTON_entry.Data = BinaryReaderV2.ReadBytes(fileBytes.AsSpan(), entry_ptr, entry_size);
                        DTON_DATA.Entries.Add(DTON_entry);
                    }

                    start_ptr = DTON_ptr + DataTypes["DTON"].Size + 8;
                    //TONE

                    int TONE_ptr = start_ptr;
                    int JUNK_ptr = TONE_ptr + DataTypes["TONE"].Size + 8;
                    int PACK_ptr = JUNK_ptr + DataTypes["JUNK"].Size + 8;

                    TONE_DATA.HeaderName = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), start_ptr, 4);
                    TONE_DATA.Entries = new ObservableCollection<NUS3_TONE_Entry_Model>();

                    int TONE_entry_count = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x8);
                    start_ptr += 0x08;
                    for (int i = 0; i < TONE_entry_count; i++)
                    {
                        NUS3_TONE_Entry_Model TONE_entry = new NUS3_TONE_Entry_Model();

                        int entry_ptr = start_ptr + BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i));
                        int entry_size = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), start_ptr + 0x4 + (0x08 * i) + 0x04);

                        TONE_entry.DTON_ID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);

                        TONE_entry.Flags = BinaryReaderV2.ReadBytes(fileBytes.AsSpan(), entry_ptr + 0x04, 8);

                        TONE_entry.RandomizerEntries = new ObservableCollection<NUS3_TONE_Randomizer_Entry_Model>();
                        TONE_entry.Groups = new ObservableCollection<GroupIndexModel>();
                        TONE_entry.Channels = new ObservableCollection<NUS3_TONE_channel_Entry_Model>();
                        if (entry_size > 0x0C)
                        {
                            int TONE_name_length = BinaryReaderV2.ReadInt8(fileBytes, entry_ptr + 0x0C);
                            if (TONE_name_length > 0)
                                TONE_entry.Name = BinaryReaderV2.ReadFixedString(fileBytes.AsSpan(), entry_ptr + 0x0D, TONE_name_length).TrimEnd('\0');


                            entry_ptr = entry_ptr + 0x0D + TONE_name_length + (3 - (TONE_name_length % 4));

                            TONE_entry.EnableRandomizer = Convert.ToBoolean(BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr));
                            if (TONE_entry.EnableRandomizer)
                            {

                                TONE_entry.Type = 1;
                                int TONE_Randomzier_entry_count = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, entry_ptr + 0x0C);
                                for (int j = 0; j < TONE_Randomzier_entry_count; j++)
                                {
                                    NUS3_TONE_Randomizer_Entry_Model randomizer_entry = new NUS3_TONE_Randomizer_Entry_Model();
                                    randomizer_entry.ID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * j));
                                    randomizer_entry.Unk = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * j) + 0x04);
                                    randomizer_entry.PlayChance = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * j) + 0x08);
                                    randomizer_entry.ToneID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * j) + 0x0C);
                                    TONE_entry.RandomizerEntries.Add(randomizer_entry);
                                }

                                TONE_entry.Randomizer_unk1 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count));
                                TONE_entry.Randomizer_unk2 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count) + 0x4);
                                TONE_entry.Randomizer_unk3 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count) + 0x8);
                                TONE_entry.Randomizer_unk4 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count) + 0xC);
                                TONE_entry.Randomizer_unk5 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count) + 0x10);
                                TONE_entry.Randomizer_unk6 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes, entry_ptr + 0x10 + (0x10 * TONE_Randomzier_entry_count) + 0x14);
                            } else
                            {

                                TONE_entry.Type = 2;
                                int SoundPtr = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x08);
                                int SoundSize = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x0C);
                                if (SoundSize > 0)
                                {

                                    TONE_entry.SoundData = BinaryReaderV2.ReadBytes(fileBytes.AsSpan(), PACK_ptr + SoundPtr + 0x08, SoundSize);
                                    string test_name = BinaryReaderV2.ReadFixedString(TONE_entry.SoundData.AsSpan(), 0, 4);
                                }
                                TONE_entry.Volume = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x10);
                                TONE_entry.PlayOffsetX = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x14);
                                TONE_entry.Unk1 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x18);
                                TONE_entry.Unk2 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x1C);
                                TONE_entry.Unk3 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x20);
                                TONE_entry.Unk4 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x24);
                                TONE_entry.PlayOffsetY = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x28);
                                TONE_entry.Unk5 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x2C);
                                TONE_entry.PlayStart = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x30);
                                TONE_entry.PlayEnd = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x34);
                                TONE_entry.PlayDelay = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x38);
                                TONE_entry.Unk8 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x3C);
                                int GroupCount = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x40);
                                for (int j = 0; j < GroupCount; j++)
                                {
                                    GroupIndexModel groupIndexEntry = new GroupIndexModel();
                                    groupIndexEntry.GroupID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x44 + (4 * j));
                                    TONE_entry.Groups.Add(groupIndexEntry);
                                }
                                int ChannelCount = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x44 + (4 * GroupCount));

                                for (int j = 0; j < ChannelCount; j++)
                                {
                                    NUS3_TONE_channel_Entry_Model channel_entry = new NUS3_TONE_channel_Entry_Model();
                                    channel_entry.ChannelID = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x44 + (4 * GroupCount) + 0x04 + (0x8 * j));
                                    channel_entry.Volume = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x44 + (4 * GroupCount) + 0x04 + (0x8 * j) + 0x04);
                                    TONE_entry.Channels.Add(channel_entry);
                                }
                                entry_ptr = entry_ptr + 0x44 + (4 * GroupCount) + 0x04 + (0x8 * ChannelCount);
                                TONE_entry.Unk9 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                TONE_entry.Unk10 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x04);
                                TONE_entry.Unk11 = BinaryReaderV2.ReadSingleLittleEndian(fileBytes.AsSpan(), entry_ptr + 0x08);
                                TONE_entry.Unk12 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x0C);

                                entry_ptr += 0x10;
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 1))
                                {

                                    TONE_entry.Unk13 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 2))
                                {

                                    TONE_entry.Unk14 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 3))
                                {

                                    TONE_entry.Unk16 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 4))
                                {

                                    TONE_entry.SampleRate = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 5))
                                {

                                    TONE_entry.ChannelType = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 6))
                                {

                                    TONE_entry.TotalSamples1 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    TONE_entry.Unk17 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x04);
                                    entry_ptr += 0x08;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 7))
                                {

                                    TONE_entry.TotalSamples2 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }
                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[4], 2))
                                {

                                    TONE_entry.Unk18 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                    entry_ptr += 0x04;
                                }

                                TONE_entry.Unk19 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr);
                                TONE_entry.Unk20 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x04);
                                TONE_entry.Unk21 = BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x08);

                                if (BinaryReaderV2.IsFlagBitSet(TONE_entry.Flags[5], 3))
                                {
                                    TONE_entry.EnableOverlay = Convert.ToBoolean(BinaryReaderV2.ReadInt32LittleEndian(fileBytes.AsSpan(), entry_ptr + 0x0C));

                                }
                            }
                        } else
                        {

                            TONE_entry.Type = 0;
                        }
                        TONE_DATA.Entries.Add(TONE_entry);
                    }
                    NUS3BANK_FILE.PROP_DATA = PROP_DATA;
                    NUS3BANK_FILE.BINF_DATA = BINF_DATA;
                    NUS3BANK_FILE.GRP_DATA = GRP_DATA;
                    NUS3BANK_FILE.DTON_DATA = DTON_DATA;
                    NUS3BANK_FILE.TONE_DATA = TONE_DATA;


                    FileID = NUS3BANK_FILE.BINF_DATA.FileID;
                    FileName = NUS3BANK_FILE.BINF_DATA.FileName;

                    GroupList = NUS3BANK_FILE.GRP_DATA.Entries;

                    ToneList = NUS3BANK_FILE.TONE_DATA.Entries;
                    DTONList = NUS3BANK_FILE.DTON_DATA.Entries;


                    //MessageBox.Show(S_XFBIN_READER.GetChunkNameByMapIndex((int)SoundChunk.ChunkMapIndex));
                } else
                    ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_error_1"]);
            }

        }

        public void PlaySound()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                if (SelectedTone.SoundData is not null)
                {
                    BnsfPlayer.PlayBnsfWithVgmstreamAsync(SelectedTone.SoundData, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vgmstream", "vgmstream-cli.exe"));
                }
            }
        }
        public void StopSound()
        {
            BnsfPlayer.Stop();
        }
        public void ExtractAllRaw(bool ExtractWithIndex = false, string basepath = "")
        {
            try
            {
                if (NUS3BANK_FILE.PROP_DATA is null)
                {
                    ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                    return;

                }
                if (basepath == "")
                {
                    var dialog = new CommonOpenFileDialog();
                    dialog.IsFolderPicker = true;
                    dialog.ShowDialog();
                    basepath = dialog.FileName;
                }
                for (int i = 0; i < ToneList.Count; i++)
                {
                    if (ToneList[i].SoundData is not null)
                    {
                        string format = BinaryReaderV2.ReadFixedString(ToneList[i].SoundData, 0, 4);
                        if (format.Contains("VAG"))
                            format = "VAG";
                        else if (format.Contains("IDSP"))
                            format = "IDSP";
                        else if (format.Contains("RIFF"))
                            format = "WAV";
                        if (!ExtractWithIndex)
                        {

                            File.WriteAllBytes(Path.Combine(basepath, ToneList[i].Name + "." + format.ToLower()), ToneList[i].SoundData);
                        } else
                        {

                            File.WriteAllBytes(Path.Combine(basepath, i.ToString("D4") + "_" + ToneList[i].Name + "." + format.ToLower()), ToneList[i].SoundData);
                        }
                    }
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\n\n" + ex.Message);
            }

        }
        public async Task ExtractAllWavAsync(bool ExtractWithIndex = false, string basepath = "")
        {
            try
            {
                if (NUS3BANK_FILE.PROP_DATA is null)
                {
                    ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                    return;

                }
                if (basepath == "")
                {
                    var dialog = new CommonOpenFileDialog();
                    dialog.IsFolderPicker = true;
                    dialog.ShowDialog();
                    basepath = dialog.FileName;
                }

                string vgmstreamExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vgmstream", "vgmstream-cli.exe");
                if (!File.Exists(vgmstreamExe))
                    throw new FileNotFoundException("vgmstream-cli.exe not found", vgmstreamExe);

                for (int i = 0; i < ToneList.Count; i++)
                {
                    try
                    {
                        if (ToneList[i]?.SoundData == null) continue;

                        // sanitize file name
                        string name = string.IsNullOrEmpty(ToneList[i].Name) ? Guid.NewGuid().ToString("N") : ToneList[i].Name;
                        foreach (var c in Path.GetInvalidFileNameChars())
                            name = name.Replace(c, '_');

                        string outPath = Path.Combine(basepath, name + ".wav");
                        if (ExtractWithIndex)
                            outPath = Path.Combine(basepath, i.ToString("D4") + "_" + name + ".wav");

                        // convert to wav bytes and write
                        byte[] wavBytes = await BnsfConverter.ConvertBnsfToWavAsync(ToneList[i].SoundData, vgmstreamExe, CancellationToken.None).ConfigureAwait(false);
                        await File.WriteAllBytesAsync(outPath, wavBytes).ConfigureAwait(false);
                    } catch (Exception exTone)
                    {
                        // log per-tone error and continue (optionally show message)
                        // MessageBox.Show($"Failed to extract {tone?.Name}: {exTone.Message}");
                    }
                }
                // optional: notify finished on UI thread
                System.Windows.Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Export finished."));
            } catch (Exception ex)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => MessageBox.Show(ex.StackTrace + "\n\n" + ex.Message));
            }
        }
        public async void ExtractAllWavCommandExecute(bool ExtractWithIndex = false)
        {
            await ExtractAllWavAsync(ExtractWithIndex);
        }

        public void AddRandomizerEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                NUS3_TONE_Randomizer_Entry_Model RandomizerEntry = new NUS3_TONE_Randomizer_Entry_Model();
                RandomizerEntry.ID = SelectedTone.RandomizerEntries.Count;
                RandomizerEntry.Unk = 1;
                RandomizerEntry.PlayChance = 1;
                RandomizerEntry.ToneID = 1;
                SelectedTone.RandomizerEntries.Add(RandomizerEntry);
            }
        }
        public void DeleteRandomizerEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null && SelectedRandomizer is not null)
            {
                SelectedTone.RandomizerEntries.Remove(SelectedRandomizer);
            }
        }
        public void DuplicateRandomizerEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null && SelectedRandomizer is not null)
            {
                NUS3_TONE_Randomizer_Entry_Model RandomizerEntry = (NUS3_TONE_Randomizer_Entry_Model)SelectedRandomizer.Clone();
                RandomizerEntry.ID = SelectedTone.RandomizerEntries.Count;
                SelectedTone.RandomizerEntries.Add(RandomizerEntry);
            }
        }

        public void AddRawSoundData()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "WAV (*.wav)|*.wav|Namco Bandai BNSF (*.bnsf)|*.bnsf";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true)
                {
                    byte[] NewSoundData = File.ReadAllBytes(myDialog.FileName);

                    SelectedTone.SoundData = NewSoundData;
                    SelectedTone.TotalSamples2 = 0;
                    string format = BinaryReaderV2.ReadFixedString(NewSoundData, 0, 4);
                    if (format == "BNSF")
                    {
                        SelectedTone.SampleRate = BinaryReaderV2.ReadInt32BigEndian(NewSoundData, 0x18);
                        SelectedTone.TotalSamples1 = BinaryReaderV2.ReadInt32BigEndian(NewSoundData, 0x1C);
                        if (SelectedTone.ChannelType == 2)
                            SelectedTone.TotalSamples2 = BinaryReaderV2.ReadInt32BigEndian(NewSoundData, 0x1C);
                        SelectedTone.ChannelType = BinaryReaderV2.ReadInt16BigEndian(NewSoundData, 0x16);
                        SelectedTone.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };
                    } else if (format == "RIFF")
                    {
                        int chunkSize = BinaryReaderV2.ReadInt32LittleEndian(NewSoundData, 0x10);
                        int BitsPerSample = BinaryReaderV2.ReadInt16LittleEndian(NewSoundData, 0x22);
                        SelectedTone.ChannelType = BinaryReaderV2.ReadInt16LittleEndian(NewSoundData, 0x16);
                        SelectedTone.SampleRate = BinaryReaderV2.ReadInt32LittleEndian(NewSoundData, 0x18);

                        SelectedTone.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };

                        int offset = BinaryReaderV2.FindString(NewSoundData, "data", System.Text.Encoding.UTF8);

                        if (offset != -1)
                        {
                            int DataChunkSize = BinaryReaderV2.ReadInt32LittleEndian(NewSoundData, offset + 0x4);

                            if ((BitsPerSample == 8))
                            {
                                SelectedTone.TotalSamples1 = DataChunkSize / (1 * SelectedTone.ChannelType);
                                if (SelectedTone.ChannelType == 2)
                                    SelectedTone.TotalSamples2 = DataChunkSize / (1 * SelectedTone.ChannelType);
                            } else if ((BitsPerSample == 16))
                            {
                                SelectedTone.TotalSamples1 = DataChunkSize / (2 * SelectedTone.ChannelType);
                                if (SelectedTone.ChannelType == 2)
                                    SelectedTone.TotalSamples2 = DataChunkSize / (2 * SelectedTone.ChannelType);
                            } else if ((BitsPerSample == 32))
                            {
                                SelectedTone.TotalSamples1 = DataChunkSize / (4 * SelectedTone.ChannelType);
                                if (SelectedTone.ChannelType == 2)
                                    SelectedTone.TotalSamples2 = DataChunkSize / (4 * SelectedTone.ChannelType);
                            }
                        }
                        

                    }
                } else
                    return;
            }
        }

        public void DeleteSoundData()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                SelectedTone.SampleRate = 0;
                SelectedTone.TotalSamples1 = 0;
                SelectedTone.TotalSamples2 = 0;
                SelectedTone.SoundData = new byte[0];
                SelectedTone.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x80, 0x18, 0x00, 0x00 };
            }
        }
        public async void AddBNSFSoundDataExecute(bool ExtractWithIndex = false)
        {
            await AddBNSFSoundData();
        }
        public async Task AddBNSFSoundData()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "WAV (*.wav)|*.wav";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true)
                {
                    byte[] NewSoundData = File.ReadAllBytes(myDialog.FileName);

                    string format = BinaryReaderV2.ReadFixedString(NewSoundData, 0, 4);
                    if (format == "RIFF")
                    {
                        int chunkSize = BinaryReaderV2.ReadInt32LittleEndian(NewSoundData, 0x10);
                        int BitsPerSample = BinaryReaderV2.ReadInt16LittleEndian(NewSoundData, 0x22);
                        SelectedTone.ChannelType = 1;
                        SelectedTone.SampleRate = 48000;

                        SelectedTone.SoundData = await BnsfConverter.ConvertWavToBnsfAsync(NewSoundData, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vgmstream", "ConvertBNSF.exe")).ConfigureAwait(false);

                        SelectedTone.TotalSamples1 = BinaryReaderV2.ReadInt32BigEndian(SelectedTone.SoundData, 0x1C);
                        SelectedTone.TotalSamples2 = BinaryReaderV2.ReadInt32BigEndian(SelectedTone.SoundData, 0x1C);

                        SelectedTone.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };
                    }
                } else
                    return;
            }
        }

        public void AddChannelEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                NUS3_TONE_channel_Entry_Model channelEntry = new NUS3_TONE_channel_Entry_Model();
                channelEntry.ChannelID = SelectedTone.Channels.Count;
                channelEntry.Volume = 0;

                SelectedTone.Channels.Add(channelEntry);
            }
        }
        public void DeleteChannelEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null && SelectedChannel is not null)
            {
                SelectedTone.Channels.Remove(SelectedChannel);
            }
        }

        public void AddToneEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (S_XFBIN_READER.XfbinFile is not null)
            {
                NUS3_TONE_Entry_Model ToneEntry = new NUS3_TONE_Entry_Model();


                ToneEntry.DTON_ID = 0;
                ToneEntry.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };
                ToneEntry.Type = 2;
                ToneEntry.Name = "New Sound Entry";
                ToneEntry.EnableRandomizer = false;
                ToneEntry.RandomizerEntries = new ObservableCollection<NUS3_TONE_Randomizer_Entry_Model>();
                ToneEntry.Randomizer_unk1 = 0;
                ToneEntry.Randomizer_unk2 = 0;
                ToneEntry.Randomizer_unk3 = 0;
                ToneEntry.Randomizer_unk4 = 1;
                ToneEntry.Randomizer_unk5 = 1;
                ToneEntry.Randomizer_unk6 = 0;

                ToneEntry.SoundData = new byte[0];
                ToneEntry.Volume = 0;
                ToneEntry.PlayOffsetX = 0;
                ToneEntry.Unk1 = 0;
                ToneEntry.Unk2 = 1;
                ToneEntry.Unk3 = 1;
                ToneEntry.Unk4 = -90;
                ToneEntry.PlayOffsetY = 0;
                ToneEntry.Unk5 = 1;
                ToneEntry.PlayStart = 0;
                ToneEntry.PlayEnd = 20;
                ToneEntry.PlayDelay = 0;
                ToneEntry.Unk8 = 3;
                ToneEntry.Groups = new ObservableCollection<GroupIndexModel>();
                ToneEntry.Channels = new ObservableCollection<NUS3_TONE_channel_Entry_Model>();
                ToneEntry.Unk9 = 9;
                ToneEntry.Unk10 = 1;
                ToneEntry.Unk11 = 1;
                ToneEntry.Unk12 = 0;
                ToneEntry.Unk13 = -1;
                ToneEntry.Unk14 = 0;
                ToneEntry.Unk16 = 0;
                ToneEntry.SampleRate = 0;
                ToneEntry.ChannelType = 1;
                ToneEntry.TotalSamples1 = 0;
                //Might Be Channel ID
                ToneEntry.Unk17 = 0;   
                ToneEntry.TotalSamples2 = 0;
                //Might Be Channel ID
                ToneEntry.Unk18 = 0;
                ToneEntry.Unk19 = 0;
                ToneEntry.Unk20 = 0;
                ToneEntry.Unk21 = 0;
                ToneEntry.EnableOverlay = false;

                NUS3BANK_FILE.TONE_DATA.Entries.Add(ToneEntry);

            }
        }

        public void DeleteGrpEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedGroup is null)
                return;

            int index = NUS3BANK_FILE.GRP_DATA.Entries.IndexOf(SelectedGroup);
            if (index < 0) return;

            foreach (var tone in NUS3BANK_FILE.TONE_DATA.Entries)
            {
                for (int i = tone.Groups.Count - 1; i >= 0; i--)
                {
                    var group = tone.Groups[i];
                    if (group.GroupID == index)
                    {
                        tone.Groups.RemoveAt(i);
                    } else if (group.GroupID > index)
                    {
                        group.GroupID -= 1;
                    }
                }
            }

            NUS3BANK_FILE.GRP_DATA.Entries.RemoveAt(index);
        }

        public void DeleteToneEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is null)
                return;

            int index = NUS3BANK_FILE.TONE_DATA.Entries.IndexOf(SelectedTone);
            if (index < 0) return;

            foreach (var tone in NUS3BANK_FILE.TONE_DATA.Entries)
            {
                for (int i = tone.RandomizerEntries.Count - 1; i >= 0; i--)
                {
                    var randomizer = tone.RandomizerEntries[i];
                    if (randomizer.ToneID == index)
                    {
                        tone.RandomizerEntries.RemoveAt(i);
                    } else if (randomizer.ToneID > index)
                    {
                        randomizer.ToneID -= 1;
                    }
                }
            }

            NUS3BANK_FILE.TONE_DATA.Entries.RemoveAt(index);
        }
        public void AddGrpEntry()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            NUS3_GRP_Entry_Model GrpEntry = new NUS3_GRP_Entry_Model();
            GrpEntry.Unk1 = 0;
            GrpEntry.Unk2 = 1;
            GrpEntry.Name = "New_Group";

                
            NUS3BANK_FILE.GRP_DATA.Entries.Add(GrpEntry);
        }

        public void AddToneGroup()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null)
            {
                GroupIndexModel GroupTone = new GroupIndexModel();
                GroupTone.GroupID = 0;

                SelectedTone.Groups.Add(GroupTone);
            }
        }
        public void DeleteToneGroup()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            if (SelectedTone is not null && SelectedGroupIndexItem is not null)
            {

                SelectedTone.Groups.Remove(SelectedGroupIndexItem);
            }
        }
        public void BatchImportRaw()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "WAV (*.wav)|*.wav|Namco Bandai BNSF (*.bnsf)|*.bnsf",
                CheckFileExists = true,
                Multiselect = true
            };

            if (dlg.ShowDialog() != true) return;

            foreach (var file in dlg.FileNames)
            {
                try
                {
                    byte[] data = File.ReadAllBytes(file);
                    if (data == null || data.Length == 0) continue;

                    string name = Path.GetFileNameWithoutExtension(file);
                    // ищем по имени (регистронезависимо)
                    var existing = NUS3BANK_FILE.TONE_DATA.Entries
                        .FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));

                    NUS3_TONE_Entry_Model entry;
                    bool isNew = false;
                    if (existing != null)
                    {
                        entry = existing;
                    } else
                    {
                        // создать по шаблону (как в AddToneEntry)
                        entry = new NUS3_TONE_Entry_Model
                        {
                            DTON_ID = 0,
                            Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 },
                            Type = 2,
                            Name = name,
                            EnableRandomizer = false,
                            RandomizerEntries = new ObservableCollection<NUS3_TONE_Randomizer_Entry_Model>(),
                            Randomizer_unk1 = 0,
                            Randomizer_unk2 = 0,
                            Randomizer_unk3 = 0,
                            Randomizer_unk4 = 1,
                            Randomizer_unk5 = 1,
                            Randomizer_unk6 = 0,
                            SoundData = new byte[0],
                            Volume = 0,
                            PlayOffsetX = 0,
                            Unk1 = 0,
                            Unk2 = 1,
                            Unk3 = 1,
                            Unk4 = -90,
                            PlayOffsetY = 0,
                            Unk5 = 1,
                            PlayStart = 0,
                            PlayEnd = 20,
                            PlayDelay = 0,
                            Unk8 = 3,
                            Groups = new ObservableCollection<GroupIndexModel>(),
                            Channels = new ObservableCollection<NUS3_TONE_channel_Entry_Model>(),
                            Unk9 = 9,
                            Unk10 = 1,
                            Unk11 = 1,
                            Unk12 = 0,
                            Unk13 = -1,
                            Unk14 = 0,
                            Unk16 = 0,
                            SampleRate = 0,
                            ChannelType = 1,
                            TotalSamples1 = 0,
                            Unk17 = 0,
                            TotalSamples2 = 0,
                            Unk18 = 0,
                            Unk19 = 0,
                            Unk20 = 0,
                            Unk21 = 0,
                            EnableOverlay = false
                        };
                        isNew = true;
                    }

                    // присвоить SoundData
                    entry.SoundData = data;

                    // определить формат по первым 4 байтам
                    string fmt = BinaryReaderV2.ReadFixedString(data, 0, 4);
                    if (fmt == "BNSF")
                    {
                        // BNSF: BE поля
                        try
                        {
                            entry.SampleRate = BinaryReaderV2.ReadInt32BigEndian(data, 0x18);
                        } catch { entry.SampleRate = 0; }

                        try
                        {
                            entry.TotalSamples1 = BinaryReaderV2.ReadInt32BigEndian(data, 0x1C);
                            entry.TotalSamples2 = entry.TotalSamples1;
                        } catch { entry.TotalSamples1 = entry.TotalSamples2 = 0; }

                        try
                        {
                            entry.ChannelType = BinaryReaderV2.ReadInt16BigEndian(data, 0x16);
                        } catch { entry.ChannelType = 1; }

                        entry.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };
                    } else if (fmt == "RIFF" || fmt == "RIFX")
                    {
                        // WAV (RIFF) - little endian
                        try
                        {
                            entry.ChannelType = BinaryReaderV2.ReadInt16LittleEndian(data, 0x16);
                        } catch { entry.ChannelType = 1; }

                        try
                        {
                            entry.SampleRate = BinaryReaderV2.ReadInt32LittleEndian(data, 0x18);
                        } catch { entry.SampleRate = 0; }

                        int bitsPerSample = 16;
                        try { bitsPerSample = BinaryReaderV2.ReadInt16LittleEndian(data, 0x22); } catch { bitsPerSample = 16; }

                        entry.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };

                        int dataOffset = BinaryReaderV2.FindString(data, "data", Encoding.UTF8);
                        if (dataOffset == -1)
                        {
                            // ещё попытка искать ASCII "data"
                            dataOffset = BinaryReaderV2.FindString(data, "data", Encoding.ASCII);
                        }

                        if (dataOffset != -1 && dataOffset + 4 + 4 <= data.Length)
                        {
                            int dataChunkSize = BinaryReaderV2.ReadInt32LittleEndian(data, dataOffset + 4);
                            int bytesPerSample = Math.Max(1, bitsPerSample / 8);
                            if (bytesPerSample <= 0) bytesPerSample = 1;

                            long totalSamples = 0;
                            if (bytesPerSample > 0 && entry.ChannelType > 0)
                                totalSamples = dataChunkSize / (bytesPerSample * entry.ChannelType);

                            if (totalSamples > int.MaxValue) totalSamples = int.MaxValue;
                            entry.TotalSamples1 = (int)totalSamples;
                            entry.TotalSamples2 = (int)totalSamples;
                        } else
                        {
                            entry.TotalSamples1 = entry.TotalSamples2 = 0;
                        }
                    } else
                    {
                        // неподдерживаемый формат — пропустить
                        continue;
                    }

                    // добавить новую запись, если нужно
                    if (isNew)
                        NUS3BANK_FILE.TONE_DATA.Entries.Add(entry);
                } catch
                {
                    // игнорировать отдельные файлы с ошибками
                    continue;
                }
            }
        }
        public async Task BatchImportBNSF()
        {
            if (NUS3BANK_FILE.PROP_DATA is null)
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_nus3bankEditor_warning"]);
                return;

            }
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "WAV (*.wav)|*.wav",
                CheckFileExists = true,
                Multiselect = true
            };

            if (dlg.ShowDialog() != true) return;

            string toolPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vgmstream", "ConvertBNSF.exe");

            foreach (var file in dlg.FileNames)
            {
                try
                {
                    byte[] wavBytes = File.ReadAllBytes(file);
                    if (wavBytes == null || wavBytes.Length == 0) continue;

                    // проверяем, что это WAV
                    string hdr = BinaryReaderV2.ReadFixedString(wavBytes, 0, 4);
                    if (!string.Equals(hdr, "RIFF", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(hdr, "RIFX", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string name = Path.GetFileNameWithoutExtension(file);
                    var existing = NUS3BANK_FILE.TONE_DATA.Entries
                        .FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));

                    bool isNew = false;
                    NUS3_TONE_Entry_Model entry;
                    if (existing != null)
                    {
                        entry = existing;
                    } else
                    {
                        entry = new NUS3_TONE_Entry_Model
                        {
                            DTON_ID = 0,
                            Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 },
                            Type = 2,
                            Name = name,
                            EnableRandomizer = false,
                            RandomizerEntries = new ObservableCollection<NUS3_TONE_Randomizer_Entry_Model>(),
                            Randomizer_unk1 = 0,
                            Randomizer_unk2 = 0,
                            Randomizer_unk3 = 0,
                            Randomizer_unk4 = 1,
                            Randomizer_unk5 = 1,
                            Randomizer_unk6 = 0,
                            SoundData = new byte[0],
                            Volume = 0,
                            PlayOffsetX = 0,
                            Unk1 = 0,
                            Unk2 = 1,
                            Unk3 = 1,
                            Unk4 = -90,
                            PlayOffsetY = 0,
                            Unk5 = 1,
                            PlayStart = 0,
                            PlayEnd = 20,
                            PlayDelay = 0,
                            Unk8 = 3,
                            Groups = new ObservableCollection<GroupIndexModel>(),
                            Channels = new ObservableCollection<NUS3_TONE_channel_Entry_Model>(),
                            Unk9 = 9,
                            Unk10 = 1,
                            Unk11 = 1,
                            Unk12 = 0,
                            Unk13 = -1,
                            Unk14 = 0,
                            Unk16 = 0,
                            SampleRate = 0,
                            ChannelType = 1,
                            TotalSamples1 = 0,
                            Unk17 = 0,
                            TotalSamples2 = 0,
                            Unk18 = 0,
                            Unk19 = 0,
                            Unk20 = 0,
                            Unk21 = 0,
                            EnableOverlay = false
                        };
                        isNew = true;
                    }

                    // конвертация WAV -> BNSF (асинхронно)
                    byte[] bnsfBytes = await BnsfConverter.ConvertWavToBnsfAsync(wavBytes, toolPath).ConfigureAwait(false);
                    if (bnsfBytes == null || bnsfBytes.Length == 0) continue;

                    entry.SoundData = bnsfBytes;

                    // заполняем поля из BNSF (BE)
                    try { entry.SampleRate = BinaryReaderV2.ReadInt32BigEndian(bnsfBytes, 0x18); } catch { entry.SampleRate = 0; }
                    try
                    {
                        int total = BinaryReaderV2.ReadInt32BigEndian(bnsfBytes, 0x1C);
                        entry.TotalSamples1 = total;
                        entry.TotalSamples2 = total;
                    } catch { entry.TotalSamples1 = entry.TotalSamples2 = 0; }
                    try { entry.ChannelType = BinaryReaderV2.ReadInt16BigEndian(bnsfBytes, 0x16); } catch { entry.ChannelType = 1; }

                    entry.Flags = new byte[8] { 0xFF, 0xFF, 0x27, 0x84, 0x9F, 0x18, 0x00, 0x00 };

                    if (isNew)
                        NUS3BANK_FILE.TONE_DATA.Entries.Add(entry);
                } catch
                {
                    // пропускаем проблемный файл
                    continue;
                }
            }
        }
        public void SaveFile()
        {
            if (NUS3BANK_FILE.PROP_DATA is not null)
            {
                if (FilePath != "")
                {

                    if (File.Exists(FilePath + ".backup"))
                    {
                        File.Delete(FilePath + ".backup");
                    }
                    File.Copy(FilePath, FilePath + ".backup");
                    byte[] ConvertedChunk = ConvertToChunk();


                    List<XFBIN_READER.FoundChunk> foundChunks = S_XFBIN_READER.FindChunks("nuccChunkNub");
                    if (foundChunks.Count < 1)
                        foundChunks = S_XFBIN_READER.FindChunks("nuccChunkBinary");
                    if (foundChunks.Count < 1)
                        return;

                    foundChunks[0].Chunk.ChunkData = ConvertedChunk;
                    foundChunks[0].Chunk.Size = (uint)ConvertedChunk.Length;

                    XFBIN_WRITER.RepackXfbinData(S_XFBIN_READER.XfbinFile, FilePath);
                } else
                {
                    SaveFileAs();
                }
            }
        }

        public void SaveFileAs(string basepath = "")
        {
            if (NUS3BANK_FILE.PROP_DATA is not null)
            {
                SaveFileDialog s = new SaveFileDialog();
                {
                    s.DefaultExt = ".xfbin";
                    s.Filter = "*.xfbin|*.xfbin";
                }
                if (basepath != "")
                    s.FileName = basepath;
                else
                    s.ShowDialog();
                if (s.FileName == "")
                {
                    return;
                }
                if (s.FileName == FilePath)
                {
                    if (File.Exists(FilePath + ".backup"))
                    {
                        File.Delete(FilePath + ".backup");
                    }
                    File.Copy(FilePath, FilePath + ".backup");
                } else
                {
                    FilePath = s.FileName;
                }
                byte[] ConvertedChunk = ConvertToChunk();



                List<XFBIN_READER.FoundChunk> foundChunks = S_XFBIN_READER.FindChunks("nuccChunkNub");
                if (foundChunks.Count < 1)
                    foundChunks = S_XFBIN_READER.FindChunks("nuccChunkBinary");
                if (foundChunks.Count < 1)
                    return;

                foundChunks[0].Chunk.ChunkData = ConvertedChunk;
                foundChunks[0].Chunk.Size = (uint)ConvertedChunk.Length;
                XFBIN_WRITER.ChangeChunkNameAndPath(S_XFBIN_READER.XfbinFile, foundChunks[0].ChunkName, $"C:/usr/sounddata/storm4/PC/{FileName}/{FileName}.nus3bank", FileName);
                XFBIN_WRITER.RepackXfbinData(S_XFBIN_READER.XfbinFile, FilePath);
                if (basepath == "")
                    ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_tool_3"] + FilePath);
            } else
            {
                ModernWpf.MessageBox.Show((string)System.Windows.Application.Current.Resources["m_error_72"]);
            }
        }

        public byte[] ConvertToChunk()
        {
            var bb = new ByteBuilder();

            int chunkSizeOffset = bb.AppendZeros(4);
            int startNUS3 = bb.WriteFixedString("NUS3", 4);
            int nus3SizeOffset = bb.AppendZeros(4);
            bb.WriteFixedString("BANKTOC ", 8);
            bb.WriteInt32LittleEndian(0x3C);
            // BANKTOC Size (reserve 4) и проставить значение (пример: 0)
            bb.WriteInt32LittleEndian(DataTypes.Count);
            int dataTypeOffsets = bb.AppendZeros(0);
            for (int i = 0; i< DataTypes.Count; i++)
            {
                bb.WriteFixedString(DataTypes[i].Type, 4);
                bb.AppendZeros(4);
            }

            //PROP DATA
            bb.WriteInt32LittleEndianAt(dataTypeOffsets+4, DataTypes["PROP"].Size);
            bb.WriteBytes(NUS3BANK_FILE.PROP_DATA.Data);

            //BINF DATA
            bb.WriteFixedString("BINF", 4);
            int binfSizeOffset = bb.AppendZeros(8);
            bb.WriteInt32LittleEndian(3);
            bb.WriteInt8((sbyte)(NUS3BANK_FILE.BINF_DATA.FileName.Length + 1));
            bb.WriteNullTerminatedString(NUS3BANK_FILE.BINF_DATA.FileName);

            int lenWithNull = NUS3BANK_FILE.BINF_DATA.FileName.Length + 1; // +1 for trailing 0 written by WriteNullTerminatedString
            int pad = (3 - (lenWithNull % 4)); // result 0..3
            if (pad > 0) bb.AppendZeros(pad);
            int binfEndOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.BINF_DATA.FileID) + 4;

            int binfSize = binfEndOffset - binfSizeOffset - 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x0C, binfSize); //BINF Size in header
            bb.WriteInt32LittleEndianAt(binfSizeOffset, binfSize); // BINF size in section

            //GRP DATA
            bb.WriteFixedString("GRP ", 4);
            int GrpSizeOffset = bb.AppendZeros(4);
            int GrpEndOffset = 0;
            int GrpCountOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.GRP_DATA.Entries.Count);
            bb.AppendZeros(NUS3BANK_FILE.GRP_DATA.Entries.Count * 8);
            for (int i = 0; i< NUS3BANK_FILE.GRP_DATA.Entries.Count; i++)
            {
                int startOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.GRP_DATA.Entries[i].Unk1);
                int endOffset = 0;
                int EntrySize = 0;
                bb.WriteInt32LittleEndian(NUS3BANK_FILE.GRP_DATA.Entries[i].Unk2);
                if (NUS3BANK_FILE.GRP_DATA.Entries[i].Name is not null && NUS3BANK_FILE.GRP_DATA.Entries[i].Name != "")
                {
                    bb.WriteInt8((sbyte)(NUS3BANK_FILE.GRP_DATA.Entries[i].Name.Length + 1));
                    endOffset = bb.WriteNullTerminatedString(NUS3BANK_FILE.GRP_DATA.Entries[i].Name) + NUS3BANK_FILE.GRP_DATA.Entries[i].Name.Length + 1;
                    lenWithNull = NUS3BANK_FILE.GRP_DATA.Entries[i].Name.Length + 1; // +1 for trailing 0 written by WriteNullTerminatedString
                    pad = (3 - (lenWithNull % 4)); // result 0..3
                    if (pad > 0) endOffset = bb.AppendZeros(pad) + pad;

                } else
                {
                    bb.WriteInt8(-1);
                    endOffset = bb.AppendZeros(3) + 3;
                }

                EntrySize = endOffset - startOffset;

                GrpEndOffset = endOffset;
                bb.WriteInt32LittleEndianAt(GrpCountOffset + 4 + (8 * i), startOffset - GrpCountOffset);
                bb.WriteInt32LittleEndianAt(GrpCountOffset + 4 + (8 * i) + 4, EntrySize);
            }

            int grpSize = GrpEndOffset - GrpSizeOffset - 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x14, grpSize); //GRP Size in header
            bb.WriteInt32LittleEndianAt(GrpSizeOffset, grpSize); // GRP size in section

            //DTON DATA
            bb.WriteFixedString("DTON", 4);
            int DtonSizeOffset = bb.AppendZeros(4);
            int DtonEndOffset = 0;
            int DtonCountOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.DTON_DATA.Entries.Count);
            bb.AppendZeros(NUS3BANK_FILE.DTON_DATA.Entries.Count * 8);
            for (int i = 0; i < NUS3BANK_FILE.DTON_DATA.Entries.Count; i++)
            {
                int startOffset = bb.WriteBytes(NUS3BANK_FILE.DTON_DATA.Entries[i].Data);
                int endOffset = startOffset + NUS3BANK_FILE.DTON_DATA.Entries[i].Data.Length;
                int EntrySize = NUS3BANK_FILE.DTON_DATA.Entries[i].Data.Length;
                
                EntrySize = endOffset - startOffset;

                DtonEndOffset = endOffset;
                bb.WriteInt32LittleEndianAt(DtonCountOffset + 4 + (8 * i), startOffset - DtonCountOffset);
                bb.WriteInt32LittleEndianAt(DtonCountOffset + 4 + (8 * i) + 4, EntrySize);
            }

            int dtonSize = DtonEndOffset - DtonSizeOffset - 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x1C, dtonSize); //DTON Size in header
            bb.WriteInt32LittleEndianAt(DtonSizeOffset, dtonSize); // DTON size in section

            //TONE DATA
            bb.WriteFixedString("TONE", 4);
            int ToneSizeOffset = bb.AppendZeros(4);
            int ToneEndOffset = 0;
            int ToneCountOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries.Count);
            bb.AppendZeros(NUS3BANK_FILE.TONE_DATA.Entries.Count * 8);
            int SoundDataPtr = 0;
            for (int i = 0; i < NUS3BANK_FILE.TONE_DATA.Entries.Count; i++)
            {
                int startOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].DTON_ID);
                int endOffset = 0;
                int EntrySize = 0x0C;

                int FlagOffset = bb.WriteBytes(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags);
                if (NUS3BANK_FILE.TONE_DATA.Entries[i].Type == 0)
                {
                    bb.WriteBytesAt(FlagOffset, new byte[8] { 0x01, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00});
                    endOffset = startOffset + EntrySize;
                } else if (NUS3BANK_FILE.TONE_DATA.Entries[i].Type == 1)
                {
                    bb.WriteBytesAt(FlagOffset, new byte[8] { 0x7F, 0x00, 0x00, 0x80, 0x80, 0x00, 0x00, 0x00 });
                    bb.WriteInt8((sbyte)(NUS3BANK_FILE.TONE_DATA.Entries[i].Name.Length + 1));
                    bb.WriteNullTerminatedString(NUS3BANK_FILE.TONE_DATA.Entries[i].Name);
                    lenWithNull = NUS3BANK_FILE.TONE_DATA.Entries[i].Name.Length + 1; // +1 for trailing 0 written by WriteNullTerminatedString
                    pad = (3 - (lenWithNull % 4)); // result 0..3
                    if (pad > 0) bb.AppendZeros(pad);

                    bb.WriteInt32LittleEndian(Convert.ToInt32(NUS3BANK_FILE.TONE_DATA.Entries[i].EnableRandomizer));

                    bb.WriteInt32LittleEndian((NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries.Count * 0x10) + 8);

                    bb.WriteInt32LittleEndian(-1);

                    bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries.Count);

                    for (int j = 0; j < NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries.Count; j++)
                    {

                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries[j].ID);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries[j].Unk);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries[j].PlayChance);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].RandomizerEntries[j].ToneID);
                    }

                    bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk1);
                    bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk2);
                    bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk3);
                    bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk4);
                    bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk5);
                    endOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Randomizer_unk6) + 4;

                } else
                {
                    if (NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[0] != 0x01)
                    {
                        bb.WriteInt8((sbyte)(NUS3BANK_FILE.TONE_DATA.Entries[i].Name.Length + 1));
                        bb.WriteNullTerminatedString(NUS3BANK_FILE.TONE_DATA.Entries[i].Name);
                        lenWithNull = NUS3BANK_FILE.TONE_DATA.Entries[i].Name.Length + 1; // +1 for trailing 0 written by WriteNullTerminatedString
                        pad = (3 - (lenWithNull % 4)); // result 0..3
                        if (pad > 0) bb.AppendZeros(pad);
                        bb.WriteInt32LittleEndian(Convert.ToInt32(NUS3BANK_FILE.TONE_DATA.Entries[i].EnableRandomizer));
                        bb.WriteInt32LittleEndian(8);
                        if (NUS3BANK_FILE.TONE_DATA.Entries[i].SoundData is not null)
                        {

                            bb.WriteInt32LittleEndian(SoundDataPtr);
                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].SoundData.Length);
                            SoundDataPtr += NUS3BANK_FILE.TONE_DATA.Entries[i].SoundData.Length;
                        } else
                        {
                            bb.WriteInt32LittleEndian(0);
                            bb.WriteInt32LittleEndian(0);

                        }
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Volume);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].PlayOffsetX);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk1);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk2);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk3);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk4);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].PlayOffsetY);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk5);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].PlayStart);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].PlayEnd);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].PlayDelay);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk8);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Groups.Count);
                        for (int j = 0; j < NUS3BANK_FILE.TONE_DATA.Entries[i].Groups.Count; j++)
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Groups[j].GroupID);
                        }
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Channels.Count);

                        for (int j = 0; j < NUS3BANK_FILE.TONE_DATA.Entries[i].Channels.Count; j++)
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Channels[j].ChannelID);
                            bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Channels[j].Volume);
                        }

                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk9);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk10);
                        bb.WriteSingleLittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk11);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk12);

                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 1))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk13);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 2))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk14);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 3))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk16);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 4))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].SampleRate);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 5))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].ChannelType);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 6))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].TotalSamples1);
                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk17);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 7))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].TotalSamples2);
                        }
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[4], 2))
                        {

                            bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk18);
                        }
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk19);
                        bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk20);
                        endOffset = bb.WriteInt32LittleEndian(NUS3BANK_FILE.TONE_DATA.Entries[i].Unk21) + 4;
                        if (BinaryReaderV2.IsFlagBitSet(NUS3BANK_FILE.TONE_DATA.Entries[i].Flags[5], 3))
                        {

                            endOffset = bb.WriteInt32LittleEndian(Convert.ToInt32(NUS3BANK_FILE.TONE_DATA.Entries[i].EnableOverlay)) + 4;
                        }
                    } else
                    {

                        endOffset = startOffset + EntrySize;
                    }
                }

                EntrySize = endOffset - startOffset;

                ToneEndOffset = endOffset;
                bb.WriteInt32LittleEndianAt(ToneCountOffset + 4 + (8 * i), startOffset - ToneCountOffset);
                bb.WriteInt32LittleEndianAt(ToneCountOffset + 4 + (8 * i) + 4, EntrySize);
            }

            int ToneSize = ToneEndOffset - ToneSizeOffset - 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x24, ToneSize); //DTON Size in header
            bb.WriteInt32LittleEndianAt(ToneSizeOffset, ToneSize); // DTON size in section

            //JUNK DATA

            bb.WriteFixedString("JUNK", 4);
            int JunkSizeOffset = bb.AppendZeros(8);
            int JunkSize = 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x2C, JunkSize); //JUNK Size in header
            bb.WriteInt32LittleEndianAt(JunkSizeOffset, JunkSize); // JUNK size in section

            //PACK DATA

            bb.WriteFixedString("PACK", 4);
            int packSizeOffset = bb.AppendZeros(4);
            int packEndOffset = packSizeOffset + 4;
            for (int j = 0; j<NUS3BANK_FILE.TONE_DATA.Entries.Count; j++)
            {
                if (NUS3BANK_FILE.TONE_DATA.Entries[j].SoundData is not null && NUS3BANK_FILE.TONE_DATA.Entries[j].Type == 2)
                {

                    packEndOffset = bb.AppendBytes(NUS3BANK_FILE.TONE_DATA.Entries[j].SoundData) + NUS3BANK_FILE.TONE_DATA.Entries[j].SoundData.Length;
                }
            }
            int packSize = packEndOffset - packSizeOffset - 4;
            bb.WriteInt32LittleEndianAt(dataTypeOffsets + 0x34, packSize); //BINF Size in header
            bb.WriteInt32LittleEndianAt(packSizeOffset, packSize); // BINF size in section

            int TotalSize = packEndOffset - startNUS3;
            bb.WriteInt32BigEndianAt(chunkSizeOffset, TotalSize);
            bb.WriteInt32LittleEndianAt(nus3SizeOffset, TotalSize - 8);


            // возвращаем массив ровно по длине
            return bb.ToArray();
        }


        public async void BatchImportBNSFExecute()
        {
            await BatchImportBNSF();
        }
        private RelayCommand _batchImportRawCommand;
        public RelayCommand BatchImportRawCommand
        {
            get
            {
                return _batchImportRawCommand ??
                  (_batchImportRawCommand = new RelayCommand(obj =>
                  {
                      BatchImportRaw();
                  }));
            }
        }

        private RelayCommand _batchImportBNSFCommand;
        public RelayCommand BatchImportBNSFCommand
        {
            get
            {
                return _batchImportBNSFCommand ??
                  (_batchImportBNSFCommand = new RelayCommand(obj =>
                  {
                      BatchImportBNSFExecute();
                  }));
            }
        }
        private RelayCommand _addChannelEntryCommand;
        public RelayCommand AddChannelEntryCommand
        {
            get
            {
                return _addChannelEntryCommand ??
                  (_addChannelEntryCommand = new RelayCommand(obj =>
                  {
                      AddChannelEntry();
                  }));
            }
        }

        private RelayCommand _deleteChannelEntryCommand;
        public RelayCommand DeleteChannelEntryCommand
        {
            get
            {
                return _deleteChannelEntryCommand ??
                  (_deleteChannelEntryCommand = new RelayCommand(obj =>
                  {
                      DeleteChannelEntry();
                  }));
            }
        }
        private RelayCommand _addRawSoundDataCommand;
        public RelayCommand AddRawSoundDataCommand
        {
            get
            {
                return _addRawSoundDataCommand ??
                  (_addRawSoundDataCommand = new RelayCommand(obj =>
                  {
                      AddRawSoundData();
                  }));
            }
        }
        private RelayCommand _addBNSFSoundDataCommand;
        public RelayCommand AddBNSFSoundDataCommand
        {
            get
            {
                return _addBNSFSoundDataCommand ??
                  (_addBNSFSoundDataCommand = new RelayCommand(obj =>
                  {
                      AddBNSFSoundDataExecute();
                  }));
            }
        }
        private RelayCommand _deleteSoundDataCommand;
        public RelayCommand DeleteSoundDataCommand
        {
            get
            {
                return _deleteSoundDataCommand ??
                  (_deleteSoundDataCommand = new RelayCommand(obj =>
                  {
                      DeleteSoundData();
                  }));
            }
        }

        private RelayCommand _addRandomizerEntryCommand;
        public RelayCommand AddRandomizerEntryCommand
        {
            get
            {
                return _addRandomizerEntryCommand ??
                  (_addRandomizerEntryCommand = new RelayCommand(obj =>
                  {
                      AddRandomizerEntry();
                  }));
            }
        }

        private RelayCommand _deleteRandomizerEntryCommand;
        public RelayCommand DeleteRandomizerEntryCommand
        {
            get
            {
                return _deleteRandomizerEntryCommand ??
                  (_deleteRandomizerEntryCommand = new RelayCommand(obj =>
                  {
                      DeleteRandomizerEntry();
                  }));
            }
        }

        private RelayCommand _duplicateRandomizerEntryCommand;
        public RelayCommand DuplicateRandomizerEntryCommand
        {
            get
            {
                return _duplicateRandomizerEntryCommand ??
                  (_duplicateRandomizerEntryCommand = new RelayCommand(obj =>
                  {
                      DuplicateRandomizerEntry();
                  }));
            }
        }
        private RelayCommand _playSoundCommand;
        public RelayCommand PlaySoundCommand
        {
            get
            {
                return _playSoundCommand ??
                  (_playSoundCommand = new RelayCommand(obj =>
                  {
                      PlaySound();
                  }));
            }
        }
        private RelayCommand _stopSoundCommand;
        public RelayCommand StopSoundCommand
        {
            get
            {
                return _stopSoundCommand ??
                  (_stopSoundCommand = new RelayCommand(obj =>
                  {
                      StopSound();
                  }));
            }
        }

        private RelayCommand _extractAllRawCommand;
        public RelayCommand ExtractAllRawCommand
        {
            get
            {
                return _extractAllRawCommand ??
                  (_extractAllRawCommand = new RelayCommand(obj =>
                  {
                      ExtractAllRaw();
                  }));
            }
        }
        private RelayCommand _extractAllWavCommand;
        public RelayCommand ExtractAllWavCommand
        {
            get
            {
                return _extractAllWavCommand ??
                    (_extractAllWavCommand = new RelayCommand(obj =>
                    {
                        ExtractAllWavCommandExecute();
                    }));
            }
        }
        private RelayCommand _extractAllRawWithIndexCommand;
        public RelayCommand ExtractAllRawWithIndexCommand
        {
            get
            {
                return _extractAllRawWithIndexCommand ??
                  (_extractAllRawWithIndexCommand = new RelayCommand(obj =>
                  {
                      ExtractAllRaw(true);
                  }));
            }
        }
        private RelayCommand _extractAllWavWithIndexCommand;
        public RelayCommand ExtractAllWavWithIndexCommand
        {
            get
            {
                return _extractAllWavWithIndexCommand ??
                    (_extractAllWavWithIndexCommand = new RelayCommand(obj =>
                    {
                        ExtractAllWavCommandExecute(true);
                    }));
            }
        }
        private RelayCommand _addToneGroupCommand;
        public RelayCommand AddToneGroupCommand
        {
            get
            {
                return _addToneGroupCommand ??
                  (_addToneGroupCommand = new RelayCommand(obj =>
                  {
                      AddToneGroup();
                  }));
            }
        }
        private RelayCommand _deleteToneGroupCommand;
        public RelayCommand DeleteToneGroupCommand
        {
            get
            {
                return _deleteToneGroupCommand ??
                  (_deleteToneGroupCommand = new RelayCommand(obj =>
                  {
                      DeleteToneGroup();
                  }));
            }
        }
        private RelayCommand _addToneEntryCommand;
        public RelayCommand AddToneEntryCommand
        {
            get
            {
                return _addToneEntryCommand ??
                  (_addToneEntryCommand = new RelayCommand(obj =>
                  {
                      AddToneEntry();
                  }));
            }
        }
        private RelayCommand _deleteToneEntryCommand;
        public RelayCommand DeleteToneEntryCommand
        {
            get
            {
                return _deleteToneEntryCommand ??
                  (_deleteToneEntryCommand = new RelayCommand(obj =>
                  {
                      DeleteToneEntry();
                  }));
            }
        }
        private RelayCommand _addGrpEntryCommand;
        public RelayCommand AddGrpEntryCommand
        {
            get
            {
                return _addGrpEntryCommand ??
                  (_addGrpEntryCommand = new RelayCommand(obj =>
                  {
                      AddGrpEntry();
                  }));
            }
        }
        private RelayCommand _deleteGrpEntryCommand;
        public RelayCommand DeleteGrpEntryCommand
        {
            get
            {
                return _deleteGrpEntryCommand ??
                  (_deleteGrpEntryCommand = new RelayCommand(obj =>
                  {
                      DeleteGrpEntry();
                  }));
            }
        }
        private RelayCommand _openFileCommand;
        public RelayCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ??
                  (_openFileCommand = new RelayCommand(obj =>
                  {
                      OpenFile();
                  }));
            }
        }
        private RelayCommand _saveFileCommand;
        public RelayCommand SaveFileCommand
        {
            get
            {
                return _saveFileCommand ??
                  (_saveFileCommand = new RelayCommand(obj =>
                  {
                      SaveFile();
                  }));
            }
        }
        private RelayCommand _saveFileAsCommand;
        public RelayCommand SaveFileAsCommand
        {
            get
            {
                return _saveFileAsCommand ??
                  (_saveFileAsCommand = new RelayCommand(obj =>
                  {
                      SaveFileAs();
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public static class BnsfMath
    {
        /// <summary>
        /// Возвращает total_samples (сэмплы на канал).
        /// bitsPerFrame и frameSize берите из спецификации: 960 и 640 в вашем примере.
        /// roundUp = true если считать неполный фрейм как полный.
        /// </summary>
        public static uint CalculateTotalSamples(long payloadBytes, int bitsPerFrame = 960, int frameSize = 640, bool roundUp = false)
        {
            if (payloadBytes < 0) throw new ArgumentOutOfRangeException(nameof(payloadBytes));
            if (bitsPerFrame <= 0) throw new ArgumentOutOfRangeException(nameof(bitsPerFrame));
            if (frameSize <= 0) throw new ArgumentOutOfRangeException(nameof(frameSize));

            long payloadBits = checked(payloadBytes * 8L);

            long frames = payloadBits / bitsPerFrame;
            if (roundUp && (payloadBits % bitsPerFrame) != 0)
                frames++;

            long totalSamples = checked(frames * (long)frameSize);

            if (totalSamples > uint.MaxValue) throw new OverflowException("totalSamples > uint.MaxValue");

            return (uint)totalSamples;
        }


    }
    public static class BnsfConverter
    {
        /// <summary>
        /// Конвертирует BNSF (в byte[]) в WAV и возвращает байты WAV.
        /// Требует наличия vgmstream-cli.exe; путь передаётся в vgmstreamCliPath.
        /// </summary>
        /// 


        public static async Task<byte[]> ConvertWavToBnsfAsync(byte[] wavData, string toolPath, CancellationToken ct = default)
        {
            if (wavData == null) throw new ArgumentNullException(nameof(wavData));
            if (string.IsNullOrWhiteSpace(toolPath)) throw new ArgumentNullException(nameof(toolPath));

            string tempIn = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
            string tempResampled = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
            string tempOut = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".bin");
            string tempResampledCut = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".bin");

            try
            {
                // сохраняем исходный WAV (временный)
                await File.WriteAllBytesAsync(tempIn, wavData, ct).ConfigureAwait(false);

                // требуемый формат
                var newFormat = new WaveFormat(48000, 16, 1);

                // прочитать исходный WAV из памяти и привести к newFormat
                using (var inStream = new MemoryStream(wavData))
                using (var reader = new WaveFileReader(inStream))
                {
                    ISampleProvider sampleProvider = reader.ToSampleProvider();

                    // поддерживаем только 1 или 2 канала во входном файле (обычный случай)
                    if (reader.WaveFormat.Channels == 2 && newFormat.Channels == 1)
                    {
                        sampleProvider = new StereoToMonoSampleProvider(sampleProvider);
                        // StereoToMonoSampleProvider сводит левый+правый в моно (по умолчанию 0.5/0.5)
                    } else if (reader.WaveFormat.Channels != 1 && reader.WaveFormat.Channels != 2)
                    {
                        throw new NotSupportedException("Input WAV has unsupported channel count.");
                    }

                    // ресемплируем к newFormat.SampleRate
                    var resampler = new WdlResamplingSampleProvider(sampleProvider, newFormat.SampleRate);

                    // получаем 16-bit PCM провайдер
                    var waveProvider = new SampleToWaveProvider16(resampler);

                    // записываем временный ресемплированный WAV
                    WaveFileWriter.CreateWaveFile(tempResampled, waveProvider);
                }
                byte[] newWavFile = File.ReadAllBytes(tempResampled);
                int data_offset = BinaryReaderV2.FindString(newWavFile, "data", System.Text.Encoding.UTF8);
                int data_size = BinaryReaderV2.ReadInt32LittleEndian(newWavFile, data_offset);

                byte[] result = new byte[0];
                result = BinaryReader.b_AddBytes(result, newWavFile, 0, data_offset + 8, data_size);

                File.WriteAllBytes(tempResampledCut, result);
                // вызываем вашу внешнюю утилиту, подаём resampled WAV
                var psi = new ProcessStartInfo
                {
                    FileName = toolPath,
                    Arguments = $"0 \"{tempResampledCut}\" \"{tempOut}\" 48000 14000",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };

                using (var p = Process.Start(psi) ?? throw new InvalidOperationException("Unable to start conversion process."))
                {
                    string err = await p.StandardError.ReadToEndAsync().ConfigureAwait(false);
                    await p.WaitForExitAsync(ct).ConfigureAwait(false);
                    if (p.ExitCode != 0)
                        throw new InvalidOperationException($"Converter failed (exit {p.ExitCode}): {err}");
                }

                if (!File.Exists(tempOut))
                    throw new FileNotFoundException("Converter did not produce output file.", tempOut);

                byte[] payload = await File.ReadAllBytesAsync(tempOut, ct).ConfigureAwait(false);



                // --- формируем BNSF заголовок (48 байт), оставляем места для заполнения:
                byte[] header = new byte[0x30]
                {
                0x42,0x4E,0x53,0x46,             // "BNSF"
                0x00,0x00,0x00,0x00,             // file_size (BE) - заполнится ниже
                0x49,0x53,0x31,0x34,0x73,0x66,0x6D,0x74, // format[8] "IS14sfmt"
                0x00,0x00,0x00,0x14,             // flags (пример)
                0x00,0x00,0x00,0x00,             // channels (offset 20..23)  <-- заполним ниже
                0x00,0x00,0x00,0x00,             // sample_rate (offset 24..27) <-- заполним ниже
                0x00,0x00,0x00,0x00,             // total_samples (offset 28..31) <-- заполним ниже
                0x00,0x00,0x00,0x00,             // loop_start
                0x00,0x78,0x02,0x80,             // loop_end (пример оставлен как в вашем шаблоне)
                0x73,0x64,0x61,0x74,             // "sdat"
                0x00,0x00,0x00,0x00              // data_size (offset 44..47) <-- заполним ниже
                };

                // Заполняем поля, используя newFormat (мы уже привели WAV к newFormat)
                // file_size: BE, общая длина (header (0x30) + payload) -> ваш код использовал 0x28 раньше, оставляю 0x30 (48) как header size
                uint totalFileSize = (uint)(header.Length + payload.Length);
                BinaryWriterV2.WriteUInt32BigEndian(header, 0x04, totalFileSize - 8);

                // channels (BE at offset 0x14..0x17 (20..23))
                BinaryWriterV2.WriteUInt32BigEndian(header, 0x14, (uint)newFormat.Channels);

                // sample_rate (BE at offset 0x18..0x1B (24..27))
                BinaryWriterV2.WriteUInt32BigEndian(header, 0x18, (uint)newFormat.SampleRate);

                // data_size (BE at offset 0x2C..0x2F (44..47))
                BinaryWriterV2.WriteUInt32BigEndian(header, 0x2C, (uint)payload.Length);

                // total_samples: рассчитываем по payload (сэмплы на канал)
                uint totalSamples = BnsfMath.CalculateTotalSamples(payload.Length, bitsPerFrame: 960, frameSize: 640, roundUp: false);
                BinaryWriterV2.WriteUInt32BigEndian(header, 0x1C, totalSamples);

                // результирующий BNSF = header + payload
                byte[] result2 = new byte[header.Length + payload.Length];
                Buffer.BlockCopy(header, 0, result2, 0, header.Length);
                Buffer.BlockCopy(payload, 0, result2, header.Length, payload.Length);
                return result2;
            } finally
            {
                TryDelete(tempIn);
                TryDelete(tempResampled);
                TryDelete(tempOut);
            }
        }

        public static async Task<byte[]> ConvertBnsfToWavAsync(byte[] bnsfData, string vgmstreamCliPath, CancellationToken ct = default)
        {
            if (bnsfData == null) throw new ArgumentNullException(nameof(bnsfData));
            if (string.IsNullOrWhiteSpace(vgmstreamCliPath)) throw new ArgumentNullException(nameof(vgmstreamCliPath));

            string tempIn = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".bnsf");
            string tempOut = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");

            try
            {
                await File.WriteAllBytesAsync(tempIn, bnsfData, ct).ConfigureAwait(false);

                var psi = new ProcessStartInfo
                {
                    FileName = vgmstreamCliPath,
                    Arguments = $"-o \"{tempOut}\" \"{tempIn}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };

                using (var p = Process.Start(psi) ?? throw new InvalidOperationException("Unable to start vgmstream process."))
                {
                    string err = await p.StandardError.ReadToEndAsync().ConfigureAwait(false);
                    await p.WaitForExitAsync(ct).ConfigureAwait(false);

                    if (p.ExitCode != 0)
                        throw new InvalidOperationException($"vgmstream failed (exit {p.ExitCode}): {err}");
                }

                // убедимся, что файл существует
                if (!File.Exists(tempOut))
                    throw new FileNotFoundException("vgmstream did not produce output file.", tempOut);

                var wavBytes = await File.ReadAllBytesAsync(tempOut, ct).ConfigureAwait(false);
                return wavBytes;
            } finally
            {
                TryDelete(tempIn);
                TryDelete(tempOut);
            }
        }

        private static void TryDelete(string path)
        {
            try { if (File.Exists(path)) File.Delete(path); } catch { }
        }
    }
    public static class BnsfPlayer
    {
        private static AudioFileReader _audio;
        private static WaveOutEvent _currentPlayer;
        private static Task _currentTask;
        private static CancellationTokenSource _cts;
        private static NUS3BANKViewModel _boundVm;

        public static void Bind(NUS3BANKViewModel vm)
        {
            Unbind();
            _boundVm = vm;
            if (_boundVm != null)
            {
                _boundVm.PropertyChanged += BoundVm_PropertyChanged;
                // применим текущую громкость к уже проигрываемому треку
                try { if (_audio != null) _audio.Volume = Clamp(_boundVm.SoundVolume); } catch { }
            }
        }

        public static void Unbind()
        {
            if (_boundVm != null)
            {
                _boundVm.PropertyChanged -= BoundVm_PropertyChanged;
                _boundVm = null;
            }
        }

        private static void BoundVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NUS3BANKViewModel.SoundVolume))
            {
                var vol = _boundVm?.SoundVolume ?? 1.0f;
                SetVolume(vol);
            }
        }

        public static async Task PlayBnsfWithVgmstreamAsync(byte[] bnsfData, string vgmstreamCliPath, float initialVolume = 1.0f)
        {
            if (bnsfData == null) return;

            // stop previous
            if (_currentPlayer != null)
            {
                _cts?.Cancel();
                try { _currentPlayer.Stop(); } catch { }
                try { _currentPlayer.Dispose(); } catch { }
                _currentPlayer = null;
                _audio = null;
            }

            _cts = new CancellationTokenSource();
            var token = _cts.Token;


            string format = BinaryReaderV2.ReadFixedString(bnsfData, 0, 4);
            short Flag = BinaryReaderV2.ReadInt16BigEndian(bnsfData, 0x14);
            if (format == "BNSF")
                BinaryWriterV2.WriteInt16BigEndian(bnsfData, 0x14, 0);
            string tempIn = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bnsf");
            string tempOut = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".wav");

            try
            {
                await File.WriteAllBytesAsync(tempIn, bnsfData, token).ConfigureAwait(false);

                if (format == "BNSF")
                    BinaryWriterV2.WriteInt16BigEndian(bnsfData, 0x14, Flag);
                var psi = new ProcessStartInfo
                {
                    FileName = vgmstreamCliPath,
                    Arguments = $"-o \"{tempOut}\" \"{tempIn}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };

                using (var p = Process.Start(psi))
                {
                    string err = await p.StandardError.ReadToEndAsync().ConfigureAwait(false);
                    await p.WaitForExitAsync(token).ConfigureAwait(false);
                    if (p.ExitCode != 0) throw new Exception($"vgmstream failed: {err}");
                }

                // create audio and player, set initial volume from bound VM if available
                var af = new AudioFileReader(tempOut);
                float startVol = _boundVm != null ? Clamp(_boundVm.SoundVolume) : Clamp(initialVolume);
                af.Volume = startVol;

                var wo = new WaveOutEvent();
                wo.Init(af);

                // publish instances so SetVolume can modify them
                _audio = af;
                _currentPlayer = wo;

                _currentTask = Task.Run(async () =>
                {
                    try
                    {
                        wo.Play();
                        while (wo.PlaybackState == PlaybackState.Playing && !token.IsCancellationRequested)
                            await Task.Delay(200, token).ConfigureAwait(false);
                    } catch (OperationCanceledException) { } finally
                    {
                        try { wo.Stop(); } catch { }
                        try { wo.Dispose(); } catch { }
                        try { af.Dispose(); } catch { }

                        if (ReferenceEquals(_currentPlayer, wo)) _currentPlayer = null;
                        if (ReferenceEquals(_audio, af)) _audio = null;
                    }
                }, token);

                await _currentTask.ConfigureAwait(false);
            } catch (OperationCanceledException) { } finally
            {
                TryDelete(tempIn);
                TryDelete(tempOut);
            }
        }

        public static void SetVolume(float volume)
        {
            var v = Clamp(volume);
            try { if (_audio != null) _audio.Volume = v; } catch { }
        }
        public static TimeSpan GetCurrentTime()
        {
            try
            {
                if (_audio == null) return TimeSpan.Zero;
                return _audio.CurrentTime;
            } catch { return TimeSpan.Zero; }
        }

        public static TimeSpan GetTotalTime()
        {
            try
            {
                if (_audio == null) return TimeSpan.Zero;
                return _audio.TotalTime;
            } catch { return TimeSpan.Zero; }
        }
        public static void Stop()
        {
            try { _cts?.Cancel(); } catch { }
            try { _currentPlayer?.Stop(); } catch { }
        }

        private static float Clamp(float v) => Math.Max(0f, Math.Min(1f, v));
        private static void TryDelete(string path) { try { File.Delete(path); } catch { } }
    }
}
