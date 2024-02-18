using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class PlayerIconModel : ICloneable, INotifyPropertyChanged {

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
        private string _baseIcon;
        public string BaseIcon {
            get { return _baseIcon; }
            set {
                _baseIcon = value;
                OnPropertyChanged("BaseIcon");
            }
        }
        private string _awakeIcon;
        public string AwakeIcon {
            get { return _awakeIcon; }
            set {
                _awakeIcon = value;
                OnPropertyChanged("AwakeIcon");
            }
        }
        private string _playerIconName;
        public string PlayerIconName {
            get { return _playerIconName; }
            set {
                _playerIconName = value;
                OnPropertyChanged("PlayerIconName");
            }
        }
        private string _subJutsuIcon;
        public string SubJutsuIcon {
            get { return _subJutsuIcon; }
            set {
                _subJutsuIcon = value;
                OnPropertyChanged("SubJutsuIcon");
            }
        }
        public object Clone() {
            return new PlayerIconModel {
                CharacodeID = this.CharacodeID,
                CostumeID = this.CostumeID,
                BaseIcon = this.BaseIcon,
                AwakeIcon = this.AwakeIcon,
                PlayerIconName = this.PlayerIconName,
                SubJutsuIcon = this.SubJutsuIcon
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
