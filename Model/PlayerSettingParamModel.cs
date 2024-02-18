using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class PlayerSettingParamModel : ICloneable, INotifyPropertyChanged {

        private int _psp_id;
        public int PSP_ID {
            get { return _psp_id; }
            set {
                _psp_id = value;
                OnPropertyChanged("PSP_ID");
            }
        }
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

        private int _unk;
        public int Unk {
            get { return _unk; }
            set {
                _unk = value;
                OnPropertyChanged("Unk");
            }
        }
        private string _PSP_code;
        public string PSP_code {
            get { return _PSP_code; }
            set {
                _PSP_code = value;
                OnPropertyChanged("PSP_code");
            }
        }
        private int _defaultJutsu;
        public int DefaultJutsu {
            get { return _defaultJutsu; }
            set {
                _defaultJutsu = value;
                OnPropertyChanged("DefaultJutsu");
            }
        }

        private int _defaultUltimateJutsu;
        public int DefaultUltimateJutsu {
            get { return _defaultUltimateJutsu; }
            set {
                _defaultUltimateJutsu = value;
                OnPropertyChanged("DefaultUltimateJutsu");
            }
        }

        private string _characterNameMessageID;
        public string CharacterNameMessageID {
            get { return _characterNameMessageID; }
            set {
                _characterNameMessageID = value;
                OnPropertyChanged("CharacterNameMessageID");
            }
        }
        private string _costumeDescriptionMessageID;
        public string CostumeDescriptionMessageID {
            get { return _costumeDescriptionMessageID; }
            set {
                _costumeDescriptionMessageID = value;
                OnPropertyChanged("CostumeDescriptionMessageID");
            }
        }
        private int _dlc_id;
        public int DLC_ID {
            get { return _dlc_id; }
            set {
                _dlc_id = value;
                OnPropertyChanged("DLC_ID");
            }
        }
        private int _mainPSP_ID;
        public int MainPSP_ID {
            get { return _mainPSP_ID; }
            set {
                _mainPSP_ID = value;
                OnPropertyChanged("MainPSP_ID");
            }
        }
        private int _referenceCharacodeID;
        public int ReferenceCharacodeID {
            get { return _referenceCharacodeID; }
            set {
                _referenceCharacodeID = value;
                OnPropertyChanged("ReferenceCharacodeID");
            }
        }

        private int _unk1;
        public int Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        public object Clone() {
            return new PlayerSettingParamModel { PSP_ID = this.PSP_ID, CharacodeID = this.CharacodeID, CostumeID=this.CostumeID, Unk = this.Unk, PSP_code = this.PSP_code, DefaultJutsu = this.DefaultJutsu, DefaultUltimateJutsu = this.DefaultUltimateJutsu, CharacterNameMessageID = this.CharacterNameMessageID, CostumeDescriptionMessageID = this.CostumeDescriptionMessageID, DLC_ID=this.DLC_ID, MainPSP_ID =this.MainPSP_ID, ReferenceCharacodeID =this.ReferenceCharacodeID, Unk1 = this.Unk1};
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
