using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class NUS3_DataType_Collection : ObservableCollection<NUS3_DataType_Model>
    {
        private readonly Dictionary<string, NUS3_DataType_Model> _map = new Dictionary<string, NUS3_DataType_Model>(StringComparer.OrdinalIgnoreCase);

        public NUS3_DataType_Model this[string key]
        {
            get
            {
                if (key == null) return null;
                _map.TryGetValue(key, out var value);
                return value;
            }
            set
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                if (value == null) throw new ArgumentNullException(nameof(value));
                // ensure item's Type matches key
                value.Type = key;
                if (_map.ContainsKey(key))
                {
                    var existing = _map[key];
                    int idx = IndexOf(existing);
                    if (idx >= 0) SetItem(idx, value);
                    else Add(value);
                    _map[key] = value;
                } else
                {
                    Add(value);
                    _map[key] = value;
                }
            }
        }

        protected override void InsertItem(int index, NUS3_DataType_Model item)
        {
            base.InsertItem(index, item);
            if (!string.IsNullOrEmpty(item?.Type))
                _map[item.Type] = item;
        }

        protected override void SetItem(int index, NUS3_DataType_Model item)
        {
            var old = this[index];
            if (!string.IsNullOrEmpty(old?.Type))
                _map.Remove(old.Type);

            base.SetItem(index, item);

            if (!string.IsNullOrEmpty(item?.Type))
                _map[item.Type] = item;
        }

        protected override void RemoveItem(int index)
        {
            var old = this[index];
            if (!string.IsNullOrEmpty(old?.Type))
                _map.Remove(old.Type);
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            _map.Clear();
        }

        public bool ContainsKey(string key) => key != null && _map.ContainsKey(key);
        public bool TryGetValue(string key, out NUS3_DataType_Model value)
        {
            if (key == null)
            {
                value = null;
                return false;
            }

            return _map.TryGetValue(key, out value);
        }

        public NUS3_DataType_Model GetOrCreate(string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (_map.TryGetValue(type, out var existing))
                return existing;

            var created = new NUS3_DataType_Model
            {
                Type = type,
                Size = 0
            };

            Add(created);
            return created;
        }
    }
    public class NUS3_DataType_Model : ICloneable, INotifyPropertyChanged
    {

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private int _size;
        public int Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }

        public object Clone()
        {
            return new NUS3_DataType_Model
            {
                Type = this.Type,
                Size = this.Size,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class NUS3_PROP_Model : ICloneable, INotifyPropertyChanged
    {

        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }
       
        public object Clone()
        {
            return new NUS3_PROP_Model
            {
                Data = this.Data,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class NUS3_BINF_Model : ICloneable, INotifyPropertyChanged
    {

        private string _headerName;
        public string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                OnPropertyChanged("HeaderName");
            }
        }
        private int _unk1;
        public int Unk
        {
            get { return _unk1; }
            set
            {
                _unk1 = value;
                OnPropertyChanged("Unk");
            }
        }
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        private int _fileID;
        public int FileID
        {
            get { return _fileID; }
            set
            {
                _fileID = value;
                OnPropertyChanged("FileID");
            }
        }
        public object Clone()
        {
            return new NUS3_BINF_Model
            {
                HeaderName = this.HeaderName,
                Unk = this.Unk,
                FileName = this.FileName,
                FileID = this.FileID,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class NUS3_GRP_Entry_Model : ICloneable, INotifyPropertyChanged
    {

        

        private int _unk1;
        public int Unk1
        {
            get { return _unk1; }
            set
            {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private int _unk2;
        public int Unk2
        {
            get { return _unk2; }
            set
            {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        public object Clone()
        {
            return new NUS3_GRP_Entry_Model
            {
                Name = this.Name,
                Unk1 = this.Unk1,
                Unk2 = this.Unk2,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class NUS3_GRP_Model : ICloneable, INotifyPropertyChanged
    {

        private string _headerName;
        public string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                OnPropertyChanged("HeaderName");
            }
        }
        
        private ObservableCollection<NUS3_GRP_Entry_Model> _entries;
        public ObservableCollection<NUS3_GRP_Entry_Model> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged("Entries");
            }
        }
        public object Clone()
        {
            ObservableCollection<NUS3_GRP_Entry_Model> newEntryList = new ObservableCollection<NUS3_GRP_Entry_Model>();
            for (int i = 0; i < this.Entries.Count; i++)
            {
                newEntryList.Add((NUS3_GRP_Entry_Model)this.Entries[i].Clone());
            }
            return new NUS3_GRP_Model
            {
                Entries = newEntryList,
                HeaderName = this.HeaderName
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class NUS3_DTON_Entry_Model : ICloneable, INotifyPropertyChanged
    {

        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        private byte[] _flags;
        public byte[] Flags
        {
            get { return _flags; }
            set
            {
                _flags = value;
                OnPropertyChanged("Flags");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }
        public object Clone()
        {
            
            return new NUS3_DTON_Entry_Model
            {
                ID = this.ID,
                Flags = this.Flags,
                Name = this.Name,
                Data = this.Data,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class NUS3_DTON_Model : ICloneable, INotifyPropertyChanged
    {

        private string _headerName;
        public string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                OnPropertyChanged("HeaderName");
            }
        }

        private ObservableCollection<NUS3_DTON_Entry_Model> _entries;
        public ObservableCollection<NUS3_DTON_Entry_Model> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged("Entries");
            }
        }
        public object Clone()
        {
            ObservableCollection<NUS3_DTON_Entry_Model> newEntryList = new ObservableCollection<NUS3_DTON_Entry_Model>();
            for (int i = 0; i < this.Entries.Count; i++)
            {
                newEntryList.Add((NUS3_DTON_Entry_Model)this.Entries[i].Clone());
            }
            return new NUS3_DTON_Model
            {
                Entries = newEntryList,
                HeaderName = this.HeaderName
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class GroupIndexModel : ICloneable, INotifyPropertyChanged
    {
        private int _groupID;
        public int GroupID
        {
            get => _groupID;
            set
            {
                if (_groupID == value) return;
                _groupID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupID)));
            }
        }

        public object Clone()
        {
            
            return new GroupIndexModel
            {
                GroupID = GroupID,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class NUS3_TONE_Randomizer_Entry_Model : ICloneable, INotifyPropertyChanged
    {

        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        private int _unk;
        public int Unk
        {
            get { return _unk; }
            set
            {
                _unk = value;
                OnPropertyChanged("Unk");
            }
        }
        private float _playChance;
        public float PlayChance
        {
            get { return _playChance; }
            set
            {
                _playChance = value;
                OnPropertyChanged("PlayChance");
            }
        }
        private int _toneID;
        public int ToneID
        {
            get { return _toneID; }
            set
            {
                _toneID = value;
                OnPropertyChanged("ToneID");
            }
        }
       
        public object Clone()
        {

            return new NUS3_TONE_Randomizer_Entry_Model
            {
                ID = this.ID,
                Unk = this.Unk,
                PlayChance = this.PlayChance,
                ToneID = this.ToneID,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class NUS3_TONE_channel_Entry_Model : ICloneable, INotifyPropertyChanged
    {

        private int _channelid;
        public int ChannelID
        {
            get { return _channelid; }
            set
            {
                _channelid = value;
                OnPropertyChanged("ChannelID");
            }
        }

        private float _volume;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }
        

        public object Clone()
        {

            return new NUS3_TONE_channel_Entry_Model
            {
                ChannelID = this.ChannelID,
                Volume = this.Volume
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class NUS3_TONE_Entry_Model : ICloneable, INotifyPropertyChanged
    {

        private int _dton_id;
        public int DTON_ID
        {
            get { return _dton_id; }
            set
            {
                _dton_id = value;
                OnPropertyChanged("DTON_ID");
            }
        }
        private byte[] _flags;
        public byte[] Flags
        {
            get { return _flags; }
            set
            {
                _flags = value;
                OnPropertyChanged("Flags");
            }
        }
        private int _type;
        public int Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _enableRandomizer;
        public bool EnableRandomizer
        {
            get { return _enableRandomizer; }
            set
            {
                _enableRandomizer = value;
                OnPropertyChanged("EnableRandomizer");
            }
        }

        private ObservableCollection<NUS3_TONE_Randomizer_Entry_Model> _randomizerEntries;
        public ObservableCollection<NUS3_TONE_Randomizer_Entry_Model> RandomizerEntries
        {
            get { return _randomizerEntries; }
            set
            {
                _randomizerEntries = value;
                OnPropertyChanged("RandomizerEntries");
            }
        }

        private float _randomizer_unk1;
        public float Randomizer_unk1
        {
            get { return _randomizer_unk1; }
            set
            {
                _randomizer_unk1 = value;
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
                OnPropertyChanged("Randomizer_unk6");
            }
        }

        private byte[] _soundData;
        public byte[] SoundData
        {
            get { return _soundData; }
            set
            {
                _soundData = value;
                OnPropertyChanged("SoundData");
            }
        }
        private float _volume;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
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
                OnPropertyChanged("Unk8");
            }
        }
        private ObservableCollection<GroupIndexModel> _groups;
        public ObservableCollection<GroupIndexModel> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged("Groups");
            }
        }
        private ObservableCollection<NUS3_TONE_channel_Entry_Model> _channels;
        public ObservableCollection<NUS3_TONE_channel_Entry_Model> Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                OnPropertyChanged("Channels");
            }
        }
        private int _unk9;
        public int Unk9
        {
            get { return _unk9; }
            set
            {
                _unk9 = value;
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
                OnPropertyChanged("Unk13");
            }
        }
        private int _sampleRate;
        public int SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;
                OnPropertyChanged("SampleRate");
            }
        }
        private int _channelType;
        public int ChannelType
        {
            get { return _channelType; }
            set
            {
                _channelType = value;
                OnPropertyChanged("ChannelType");
            }
        }
        private int _totalSamples1;
        public int TotalSamples1
        {
            get { return _totalSamples1; }
            set
            {
                _totalSamples1 = value;
                OnPropertyChanged("TotalSamples1");
            }
        }
        private int _unk14;
        public int Unk14
        {
            get { return _unk14; }
            set
            {
                _unk14 = value;
                OnPropertyChanged("Unk14");
            }
        }
        private int _totalSamples2;
        public int TotalSamples2
        {
            get { return _totalSamples2; }
            set
            {
                _totalSamples2 = value;
                OnPropertyChanged("TotalSamples2");
            }
        }
        private int _unk16;
        public int Unk16
        {
            get { return _unk16; }
            set
            {
                _unk16 = value;
                OnPropertyChanged("Unk16");
            }
        }
        private int _unk17;
        public int Unk17
        {
            get { return _unk17; }
            set
            {
                _unk17 = value;
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
                OnPropertyChanged("EnableOverlay");
            }
        }
       
        public object Clone()
        {
            ObservableCollection<NUS3_TONE_Randomizer_Entry_Model> newRandomizerList = new ObservableCollection<NUS3_TONE_Randomizer_Entry_Model>();
            for (int i = 0; i < this.RandomizerEntries.Count; i++)
            {
                newRandomizerList.Add(this.RandomizerEntries[i]);
            }
            ObservableCollection<GroupIndexModel> newGroupList = new ObservableCollection<GroupIndexModel>();
            for (int i = 0; i < this.Groups.Count; i++)
            {
                newGroupList.Add(this.Groups[i]);
            }
            ObservableCollection<NUS3_TONE_channel_Entry_Model> newChannelList = new ObservableCollection<NUS3_TONE_channel_Entry_Model>();
            for (int i = 0; i < this.Channels.Count; i++)
            {
                newChannelList.Add((NUS3_TONE_channel_Entry_Model)this.Channels[i].Clone());
            }
            return new NUS3_TONE_Entry_Model
            {
                DTON_ID = this.DTON_ID,
                Flags = this.Flags,
                Type = this.Type,
                Name = this.Name,

                EnableRandomizer = this.EnableRandomizer,
                RandomizerEntries = newRandomizerList,
                Randomizer_unk1 = this.Randomizer_unk1,
                Randomizer_unk2 = this.Randomizer_unk2,
                Randomizer_unk3 = this.Randomizer_unk3,
                Randomizer_unk4 = this.Randomizer_unk4,
                Randomizer_unk5 = this.Randomizer_unk5,
                Randomizer_unk6 = this.Randomizer_unk6,

                SoundData = this.SoundData,
                Volume = this.Volume,
                PlayOffsetX = this.PlayOffsetX,
                Unk1 = this.Unk1,
                Unk2 = this.Unk2,   
                Unk3 = this.Unk3,
                Unk4 = this.Unk4,
                PlayOffsetY = this.PlayOffsetY,
                Unk5 = this.Unk5,
                PlayStart = this.PlayStart,
                PlayEnd = this.PlayEnd,
                PlayDelay = this.PlayDelay,
                Unk8 = this.Unk8,
                Groups = newGroupList,
                Channels = newChannelList,
                Unk9 = this.Unk9,
                Unk10 = this.Unk10,
                Unk11 = this.Unk11,
                Unk12 = this.Unk12,
                Unk13 = this.Unk13, // flags[4], 1
                Unk14 = this.Unk14, // flags[4], 2
                Unk16 = this.Unk16, // flags[4], 3
                SampleRate = this.SampleRate, // flags[4], 4
                ChannelType = this.ChannelType, // flags[4], 5
                TotalSamples1 = this.TotalSamples1, // flags[4], 6
                //Might Be Channel ID
                Unk17 = this.Unk17, // flags[4], 6              
                TotalSamples2 = this.TotalSamples2, // flags[4], 7
                //Might Be Channel ID
                Unk18 = this.Unk18, // flags[4], 2
                Unk19 = this.Unk19,
                Unk20 = this.Unk20, //Some Condition
                Unk21 = this.Unk21, //Some Condition
                EnableOverlay = this.EnableOverlay,


            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class NUS3_TONE_Model : ICloneable, INotifyPropertyChanged
    {

        private string _headerName;
        public string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                OnPropertyChanged("HeaderName");
            }
        }

        private ObservableCollection<NUS3_TONE_Entry_Model> _entries;
        public ObservableCollection<NUS3_TONE_Entry_Model> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged("Entries");
            }
        }
        public object Clone()
        {
            ObservableCollection<NUS3_TONE_Entry_Model> newEntryList = new ObservableCollection<NUS3_TONE_Entry_Model>();
            for (int i = 0; i < this.Entries.Count; i++)
            {
                newEntryList.Add((NUS3_TONE_Entry_Model)this.Entries[i].Clone());
            }
            return new NUS3_TONE_Model
            {
                Entries = newEntryList,
                HeaderName = this.HeaderName
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class NUS3BANK_Model : ICloneable, INotifyPropertyChanged
    {

        private string _format;
        public string Format
        {
            get { return _format; }
            set
            {
                _format = value;
                OnPropertyChanged("Format");
            }
        }
        private ObservableCollection<NUS3_DataType_Model> _dataTypes;
        public ObservableCollection<NUS3_DataType_Model> DataTypes
        {
            get { return _dataTypes; }
            set
            {
                _dataTypes = value;
                OnPropertyChanged("DataTypes");
            }
        }
        private NUS3_PROP_Model _PROP_DATA;
        public NUS3_PROP_Model PROP_DATA
        {
            get { return _PROP_DATA; }
            set
            {
                _PROP_DATA = value;
                OnPropertyChanged("PROP_DATA");
            }
        }
        private NUS3_BINF_Model _BINF_DATA;
        public NUS3_BINF_Model BINF_DATA
        {
            get { return _BINF_DATA; }
            set
            {
                _BINF_DATA = value;
                OnPropertyChanged("BINF_DATA");
            }
        }
        private NUS3_GRP_Model _GRP_DATA;
        public NUS3_GRP_Model GRP_DATA
        {
            get { return _GRP_DATA; }
            set
            {
                _GRP_DATA = value;
                OnPropertyChanged("GRP_DATA");
            }
        }
        private NUS3_DTON_Model _DTON_DATA;
        public NUS3_DTON_Model DTON_DATA
        {
            get { return _DTON_DATA; }
            set
            {
                _DTON_DATA = value;
                OnPropertyChanged("DTON_DATA");
            }
        }
        private NUS3_TONE_Model _TONE_DATA;
        public NUS3_TONE_Model TONE_DATA
        {
            get { return _TONE_DATA; }
            set
            {
                _TONE_DATA = value;
                OnPropertyChanged("TONE_DATA");
            }
        }
        public object Clone()
        {
            ObservableCollection<NUS3_DataType_Model> newDataTypeList = new ObservableCollection<NUS3_DataType_Model>();
            for (int i = 0; i < this.DataTypes.Count; i++)
            {
                newDataTypeList.Add((NUS3_DataType_Model)this.DataTypes[i]);
            }
            return new NUS3BANK_Model
            {
                Format = this.Format,
                DataTypes = newDataTypeList,
                PROP_DATA = this.PROP_DATA,
                BINF_DATA = this.BINF_DATA,
                GRP_DATA = this.GRP_DATA,
                DTON_DATA = this.DTON_DATA,
                TONE_DATA = this.TONE_DATA


            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
