using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class CharacodeEditorModel : ICloneable, INotifyPropertyChanged {

        private string _characodeName;
        public string CharacodeName {
            get { return _characodeName;}
            set { _characodeName = value;
                OnPropertyChanged("CharacodeName");
            }
        }

        private int _characodeIndex;
        public int CharacodeIndex {
            get { return _characodeIndex; }
            set {
                _characodeIndex = value;
                OnPropertyChanged("CharacodeIndex");
            }
        }
        public object Clone() {
            return new CharacodeEditorModel {
                CharacodeName = this.CharacodeName,
                CharacodeIndex = this.CharacodeIndex,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
