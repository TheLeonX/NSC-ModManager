using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class PRMLoad_Model : ICloneable, INotifyPropertyChanged {

        private string _filepath;
        public string FilePath {
            get { return _filepath; }
            set {
                _filepath = value;
                OnPropertyChanged("FilePath");
            }
        }
        private string _fileName;
        public string FileName {
            get { return _fileName; }
            set {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        private int _type;
        public int Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private int _costumeIndex;
        public int CostumeIndex {
            get { return _costumeIndex; }
            set {
                _costumeIndex = value;
                OnPropertyChanged("CostumeIndex");
            }
        }
        private int _condition;
        public int Condition {
            get { return _condition; }
            set {
                _condition = value;
                OnPropertyChanged("Condition");
            }
        }
        public object Clone() {
            return new PRMLoad_Model {
                FilePath = this.FilePath,
                FileName = this.FileName,
                Type = this.Type,
                CostumeIndex = this.CostumeIndex,
                Condition = this.Condition
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
