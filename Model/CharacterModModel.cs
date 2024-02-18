using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class CharacterModModel : INotifyPropertyChanged {
        private string _characode;
        public string Characode {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("Characode");
            }
        }
        private int _page;
        public int Page {
            get { return _page; }
            set {
                _page = value;
                OnPropertyChanged("Page");
            }
        }
        private int _slot;
        public int Slot {
            get { return _slot; }
            set {
                _slot = value;
                OnPropertyChanged("Slot");
            }
        }
        private bool _partner;
        public bool Partner {
            get { return _partner; }
            set {
                _partner = value;
                OnPropertyChanged("Partner");
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
