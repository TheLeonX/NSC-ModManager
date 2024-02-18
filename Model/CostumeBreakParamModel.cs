using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class CostumeBreakParamModel : ICloneable, INotifyPropertyChanged {
        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private int _costumeID;
        public int CostumeID {
            get { return _costumeID; }
            set {
                _costumeID = value;
                OnPropertyChanged("CostumeID");
            }
        }
        private string _modelPath;
        public string ModelPath {
            get { return _modelPath; }
            set {
                _modelPath = value;
                OnPropertyChanged("ModelPath");
            }
        }
        private bool _enableInAwakening;
        public bool EnableInAwakening {
            get { return _enableInAwakening; }
            set {
                _enableInAwakening = value;
                OnPropertyChanged("EnableInAwakening");
            }
        }
        private bool _enableForClones;
        public bool EnableForClones {
            get { return _enableForClones; }
            set {
                _enableForClones = value;
                OnPropertyChanged("EnableForClones");
            }
        }
        private int _cloneCount;
        public int CloneCount {
            get { return _cloneCount; }
            set {
                _cloneCount = value;
                OnPropertyChanged("CloneCount");
            }
        }
        
        public object Clone() {
            return new CostumeBreakParamModel {
                CharacodeID = this.CharacodeID,
                CostumeID = this.CostumeID,
                ModelPath = this.ModelPath,
                EnableInAwakening = this.EnableInAwakening,
                EnableForClones = this.EnableForClones,
                CloneCount = this.CloneCount
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
