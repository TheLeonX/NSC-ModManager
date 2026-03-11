using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class StageModModel : INotifyPropertyChanged {
        private string _stageName;
        public string StageName {
            get { return _stageName; }
            set {
                _stageName = value;
                OnPropertyChanged("StageName");
            }
        }
        private string _messageID;
        public string MessageID {
            get { return _messageID; }
            set {
                _messageID = value;
                OnPropertyChanged("MessageID");
            }
        }
        private int _bgmID;
        public int BgmID {
            get { return _bgmID; }
            set {
                _bgmID = value;
                OnPropertyChanged("BgmID");
            }
        }
        private int _bgmID_NS4;
        public int BgmID_NS4
        {
            get { return _bgmID_NS4; }
            set
            {
                _bgmID_NS4 = value;
                OnPropertyChanged("BgmID_NS4");
            }
        }
        private bool _hell;
        public bool Hell {
            get { return _hell; }
            set {
                _hell = value;
                OnPropertyChanged("Hell");
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
