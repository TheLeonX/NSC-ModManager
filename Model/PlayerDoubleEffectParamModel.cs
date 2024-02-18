using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class PlayerDoubleEffectParamModel : ICloneable, INotifyPropertyChanged {
        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private string _attachBone;
        public string AttachBone {
            get { return _attachBone; }
            set {
                _attachBone = value;
                OnPropertyChanged("AttachBone");
            }
        }
        private bool _playAtStart;
        public bool PlayAtStart {
            get { return _playAtStart; }
            set {
                _playAtStart = value;
                OnPropertyChanged("PlayAtStart");
            }
        }
        private bool _playAtEnd;
        public bool PlayAtEnd {
            get { return _playAtEnd; }
            set {
                _playAtEnd = value;
                OnPropertyChanged("PlayAtEnd");
            }
        }
        private int _unk1;
        public int Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private string _effectEntry;
        public string EffectEntry {
            get { return _effectEntry; }
            set {
                _effectEntry = value;
                OnPropertyChanged("EffectEntry");
            }
        }
        private string _plAnmEntry1;
        public string PlAnmEntry1 {
            get { return _plAnmEntry1; }
            set {
                _plAnmEntry1 = value;
                OnPropertyChanged("PlAnmEntry1");
            }
        }
        private string _plAnmEntry2;
        public string PlAnmEntry2 {
            get { return _plAnmEntry2; }
            set {
                _plAnmEntry2 = value;
                OnPropertyChanged("PlAnmEntry2");
            }
        }
        private string _plAnmEntry3;
        public string PlAnmEntry3 {
            get { return _plAnmEntry3; }
            set {
                _plAnmEntry3 = value;
                OnPropertyChanged("PlAnmEntry3");
            }
        }
        private string _plAnmEntry4;
        public string PlAnmEntry4 {
            get { return _plAnmEntry4; }
            set {
                _plAnmEntry4 = value;
                OnPropertyChanged("PlAnmEntry4");
            }
        }
        private string _plAnmEntry5;
        public string PlAnmEntry5 {
            get { return _plAnmEntry5; }
            set {
                _plAnmEntry5 = value;
                OnPropertyChanged("PlAnmEntry5");
            }
        }
        private float _heightSpawn;
        public float HeightSpawn {
            get { return _heightSpawn; }
            set {
                _heightSpawn = value;
                OnPropertyChanged("HeightSpawn");
            }
        }
        private int _soundID;
        public int SoundID {
            get { return _soundID; }
            set {
                _soundID = value;
                OnPropertyChanged("SoundID");
            }
        }
        private float _unk2;
        public float Unk2 {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private bool _unk3;
        public bool Unk3 {
            get { return _unk3; }
            set {
                _unk3 = value;
                OnPropertyChanged("Unk3");
            }
        }
        private bool _enableNearestGroundPos;
        public bool EnableNearestGroundPos {
            get { return _enableNearestGroundPos; }
            set {
                _enableNearestGroundPos = value;
                OnPropertyChanged("EnableNearestGroundPos");
            }
        }
        public object Clone() {
            return new PlayerDoubleEffectParamModel {
                CharacodeID = this.CharacodeID,
                AttachBone = this.AttachBone,
                PlayAtStart = this.PlayAtStart,
                PlayAtEnd = this.PlayAtEnd,
                Unk1 = this.Unk1,
                EffectEntry = this.EffectEntry,
                PlAnmEntry1 = this.PlAnmEntry1,
                PlAnmEntry2 = this.PlAnmEntry2,
                PlAnmEntry3 = this.PlAnmEntry3,
                PlAnmEntry4 = this.PlAnmEntry4,
                PlAnmEntry5 = this.PlAnmEntry5,
                HeightSpawn = this.HeightSpawn,
                SoundID = this.SoundID,
                Unk2 = this.Unk2,
                Unk3 = this.Unk3,
                EnableNearestGroundPos = this.EnableNearestGroundPos
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
