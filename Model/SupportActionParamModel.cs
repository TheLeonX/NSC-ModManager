using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class SupportActionParamModel : ICloneable, INotifyPropertyChanged {
        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private int _supportType;
        public int SupportType {
            get { return _supportType; }
            set {
                _supportType = value;
                OnPropertyChanged("SupportType");
            }
        }
        public object Clone() {
            return new SupportActionParamModel {
                CharacodeID = this.CharacodeID,
                SupportType = this.SupportType,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
