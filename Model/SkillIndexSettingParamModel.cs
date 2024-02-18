using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class SkillIndexSettingParamModel : ICloneable, INotifyPropertyChanged {

        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }

        private int _jutsuIndex1;
        public int JutsuIndex1 {
            get { return _jutsuIndex1; }
            set {
                _jutsuIndex1 = value;
                OnPropertyChanged("JutsuIndex1");
            }
        }
        private int _jutsuIndex2;
        public int JutsuIndex2 {
            get { return _jutsuIndex2; }
            set {
                _jutsuIndex2 = value;
                OnPropertyChanged("JutsuIndex2");
            }
        }
        public object Clone() {
            return new SkillIndexSettingParamModel {
                CharacodeID = this.CharacodeID,
                JutsuIndex1 = this.JutsuIndex1,
                JutsuIndex2 = this.JutsuIndex2
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
