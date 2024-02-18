using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class DamageEffModel : ICloneable, INotifyPropertyChanged {
        private int _damageEffID;
        public int DamageEffID {
            get { return _damageEffID; }
            set {
                _damageEffID = value;
                OnPropertyChanged("DamageEffID");
            }
        }
        private int _extradamageEffID;
        public int ExtraDamageEffID {
            get { return _extradamageEffID; }
            set {
                _extradamageEffID = value;
                OnPropertyChanged("ExtraDamageEffID");
            }
        }
        private int _extraSoundID;
        public int ExtraSoundID {
            get { return _extraSoundID; }
            set {
                _extraSoundID = value;
                OnPropertyChanged("ExtraSoundID");
            }
        }
        private int _effectPrmID;
        public int EffectPrmID {
            get { return _effectPrmID; }
            set {
                _effectPrmID = value;
                OnPropertyChanged("EffectPrmID");
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
        private int _unk1;
        public int Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private int _unk2;
        public int Unk2 {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private int _extraEffectPrmID;
        public int ExtraEffectPrmID {
            get { return _extraEffectPrmID; }
            set {
                _extraEffectPrmID = value;
                OnPropertyChanged("ExtraEffectPrmID");
            }
        }
        public object Clone() {
            return new DamageEffModel {
                DamageEffID = this.DamageEffID,
                ExtraDamageEffID = this.ExtraDamageEffID,
                ExtraSoundID = this.ExtraSoundID,
                EffectPrmID = this.EffectPrmID,
                SoundID = this.SoundID,
                Unk1 = this.Unk1,
                Unk2 = this.Unk2,
                ExtraEffectPrmID = this.ExtraEffectPrmID,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
