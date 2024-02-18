using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class MessageInfoModel : ICloneable, INotifyPropertyChanged {

        private byte[] _crc32Code;
        public byte[] CRC32Code {
            get { return _crc32Code; }
            set {
                _crc32Code = value;
                OnPropertyChanged("CRC32Code");
            }
        }

        private byte[] _mainText;
        public byte[] MainText {
            get { return _mainText; }
            set {
                _mainText = value;
                OnPropertyChanged("MainText");
            }
        }
        private byte[] _secondaryText;
        public byte[] SecondaryText {
            get { return _secondaryText; }
            set {
                _secondaryText = value;
                OnPropertyChanged("SecondaryText");
            }
        }
        private byte[] _speaker;
        public byte[] Speaker {
            get { return _speaker; }
            set {
                _speaker = value;
                OnPropertyChanged("Speaker");
            }
        }
        private Int16 _acbFileID;
        public Int16 ACBFileID {
            get { return _acbFileID; }
            set {
                _acbFileID = value;
                OnPropertyChanged("ACBFileID");
            }
        }
        private Int16 _cueID;
        public Int16 CueID {
            get { return _cueID; }
            set {
                _cueID = value;
                OnPropertyChanged("CueID");
            }
        }
        private bool _disableText;
        public bool DisableText {
            get { return _disableText; }
            set {
                _disableText = value;
                OnPropertyChanged("DisableText");
            }
        }
        public object Clone() {
            return new MessageInfoModel {
                CRC32Code = this.CRC32Code,
                MainText = this.MainText,
                SecondaryText = this.SecondaryText,
                Speaker = this.Speaker,
                ACBFileID = this.ACBFileID,
                CueID = this.CueID,
                DisableText = this.DisableText,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
