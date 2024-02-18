using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class SupportSkillRecoverySpeedParamModel : ICloneable, INotifyPropertyChanged {
        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private float _jutsu1;
        public float Jutsu1 {
            get { return _jutsu1; }
            set {
                _jutsu1 = value;
                OnPropertyChanged("Jutsu1");
            }
        }
        private float _jutsu2;
        public float Jutsu2 {
            get { return _jutsu2; }
            set {
                _jutsu2 = value;
                OnPropertyChanged("Jutsu2");
            }
        }
        private float _jutsu3;
        public float Jutsu3 {
            get { return _jutsu3; }
            set {
                _jutsu3 = value;
                OnPropertyChanged("Jutsu3");
            }
        }
        private float _jutsu4;
        public float Jutsu4 {
            get { return _jutsu4; }
            set {
                _jutsu4 = value;
                OnPropertyChanged("Jutsu4");
            }
        }
        private float _jutsu5;
        public float Jutsu5 {
            get { return _jutsu5; }
            set {
                _jutsu5 = value;
                OnPropertyChanged("Jutsu5");
            }
        }
        private float _jutsu6;
        public float Jutsu6 {
            get { return _jutsu6; }
            set {
                _jutsu6 = value;
                OnPropertyChanged("Jutsu6");
            }
        }
        private float _jutsu1_awa;
        public float Jutsu1_awa {
            get { return _jutsu1_awa; }
            set {
                _jutsu1_awa = value;
                OnPropertyChanged("Jutsu1_awa");
            }
        }
        private float _jutsu2_awa;
        public float Jutsu2_awa {
            get { return _jutsu2_awa; }
            set {
                _jutsu2_awa = value;
                OnPropertyChanged("Jutsu2_awa");
            }
        }
        public object Clone() {
            return new SupportSkillRecoverySpeedParamModel {
                CharacodeID = this.CharacodeID,
                Jutsu1 = this.Jutsu1,
                Jutsu2 = this.Jutsu2,
                Jutsu3 = this.Jutsu3,
                Jutsu4 = this.Jutsu4,
                Jutsu5 = this.Jutsu5,
                Jutsu6 = this.Jutsu6,
                Jutsu1_awa = this.Jutsu1_awa,
                Jutsu2_awa = this.Jutsu2_awa,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
