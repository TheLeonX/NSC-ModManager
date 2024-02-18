using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace NSC_ModManager.Model
{
    public class CostumeBreakColorParamModel : ICloneable, INotifyPropertyChanged {

        private int _playerSettingParamID;
        public int PlayerSettingParamID {
            get { return _playerSettingParamID; }
            set {
                _playerSettingParamID = value;
                OnPropertyChanged("PlayerSettingParamID");
            }
        }
        private Color _altColor1;
        public Color AltColor1 {
            get { return _altColor1; }
            set {
                _altColor1 = value;
                OnPropertyChanged("AltColor1");
            }
        }
        private Color _altColor2;
        public Color AltColor2 {
            get { return _altColor2; }
            set {
                _altColor2 = value;
                OnPropertyChanged("AltColor2");
            }
        }
        private Color _altColor3;
        public Color AltColor3 {
            get { return _altColor3; }
            set {
                _altColor3 = value;
                OnPropertyChanged("AltColor3");
            }
        }
        private Color _altColor4;
        public Color AltColor4 {
            get { return _altColor4; }
            set {
                _altColor4 = value;
                OnPropertyChanged("AltColor4");
            }
        }
        public object Clone() {
            return new CostumeBreakColorParamModel {
                PlayerSettingParamID = this.PlayerSettingParamID,
                AltColor1 = this.AltColor1,
                AltColor2 = this.AltColor2,
                AltColor3 = this.AltColor3,
                AltColor4 = this.AltColor4
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
