using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class CostumeParamModel : ICloneable, INotifyPropertyChanged {
        private string _entryString;
        public string EntryString {
            get { return _entryString; }
            set {
                _entryString = value;
                OnPropertyChanged("EntryString");
            }
        }
        private int _entryIndex;
        public int EntryIndex {
            get { return _entryIndex; }
            set {
                _entryIndex = value;
                OnPropertyChanged("EntryIndex");
            }
        }
        private int _playerSettingParamID;
        public int PlayerSettingParamID {
            get { return _playerSettingParamID; }
            set {
                _playerSettingParamID = value;
                OnPropertyChanged("PlayerSettingParamID");
            }
        }
        private string _characterName;
        public string CharacterName {
            get { return _characterName; }
            set {
                _characterName = value;
                OnPropertyChanged("CharacterName");
            }
        }
        private int _entryType;
        public int EntryType {
            get { return _entryType; }
            set {
                _entryType = value;
                OnPropertyChanged("EntryType");
            }
        }
        private int _unlockCost;
        public int UnlockCost {
            get { return _unlockCost; }
            set {
                _unlockCost = value;
                OnPropertyChanged("UnlockCost");
            }
        }
        private int _unlockCondition;
        public int UnlockCondition {
            get { return _unlockCondition; }
            set {
                _unlockCondition = value;
                OnPropertyChanged("UnlockCondition");
            }
        }
        public object Clone() {
            return new CostumeParamModel {
                EntryString = this.EntryString,
                EntryIndex = this.EntryIndex,
                PlayerSettingParamID = this.PlayerSettingParamID,
                CharacterName = this.CharacterName,
                EntryType = this.EntryType,
                UnlockCost = this.UnlockCost,
                UnlockCondition = this.UnlockCondition
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
