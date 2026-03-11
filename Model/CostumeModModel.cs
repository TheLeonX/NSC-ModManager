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
        private bool _enableRosterChange;
        public bool EnableRosterChange
        {
            get { return _enableRosterChange; }
            set
            {
                _enableRosterChange = value;
                OnPropertyChanged("EnableRosterChange");
            }
        }
        private bool _enableRosterChangeNS4;
        public bool EnableRosterChangeNS4
        {
            get { return _enableRosterChangeNS4; }
            set
            {
                _enableRosterChangeNS4 = value;
                OnPropertyChanged("EnableRosterChangeNS4");
            }
        }
        private int _page;
        public int Page
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged("Page");
            }
        }
        private int _slot;
        public int Slot
        {
            get { return _slot; }
            set
            {
                _slot = value;
                OnPropertyChanged("Slot");
            }
        }
        private int _costume;
        public int Costume
        {
            get { return _costume; }
            set
            {
                _costume = value;
                OnPropertyChanged("Costume");
            }
        }
        private int _page_NS4;
        public int Page_NS4
        {
            get { return _page_NS4; }
            set
            {
                _page_NS4 = value;
                OnPropertyChanged("Page_NS4");
            }
        }
        private int _slot_NS4;
        public int Slot_NS4
        {
            get { return _slot_NS4; }
            set
            {
                _slot_NS4 = value;
                OnPropertyChanged("Slot_NS4");
            }
        }

        private int _costume_NS4;
        public int Costume_NS4
        {
            get { return _costume_NS4; }
            set
            {
                _costume_NS4 = value;
                OnPropertyChanged("Costume_NS4");
            }
        }

        private string _gameVersion;
        public string GameVersion
        {
            get { return _gameVersion; }
            set
            {
                _gameVersion = value;
                OnPropertyChanged("GameVersion");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
