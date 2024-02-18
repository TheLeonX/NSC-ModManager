using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class CostumeModModel : INotifyPropertyChanged {
        private string _characode;
        public string Characode {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("Characode");
            }
        }
        private string _baseCostume;
        public string BaseCostume {
            get { return _baseCostume; }
            set {
                _baseCostume = value;
                OnPropertyChanged("BaseCostume");
            }
        }
        private string _awakeCostume;
        public string AwakeCostume {
            get { return _awakeCostume; }
            set {
                _awakeCostume = value;
                OnPropertyChanged("AwakeCostume");
            }
        }
        private string _rootPath;
        public string RootPath {
            get { return _rootPath; }
            set {
                _rootPath = value;
                OnPropertyChanged("RootPath");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
