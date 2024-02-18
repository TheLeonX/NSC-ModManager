using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class AppearanceAnmModel : ICloneable, INotifyPropertyChanged {
        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private string _chunkName;
        public string ChunkName {
            get { return _chunkName; }
            set {
                _chunkName = value;
                OnPropertyChanged("ChunkName");
            }
        }
        private bool _toggleEntry;
        public bool ToggleEntry {
            get { return _toggleEntry; }
            set {
                _toggleEntry = value;
                OnPropertyChanged("ToggleEntry");
            }
        }
        private bool _enableNormalState;
        public bool EnableNormalState {
            get { return _enableNormalState; }
            set {
                _enableNormalState = value;
                OnPropertyChanged("EnableNormalState");
            }
        }
        private int _timing;
        public int Timing {
            get { return _timing; }
            set {
                _timing = value;
                OnPropertyChanged("Timing");
            }
        }
        private bool _enableAwakeningState;
        public bool EnableAwakeningState {
            get { return _enableAwakeningState; }
            set {
                _enableAwakeningState = value;
                OnPropertyChanged("EnableAwakeningState");
            }
        }
        private bool _toggleReverseAfterAwakening;
        public bool ToggleReverseAfterAwakening {
            get { return _toggleReverseAfterAwakening; }
            set {
                _toggleReverseAfterAwakening = value;
                OnPropertyChanged("ToggleReverseAfterAwakening");
            }
        }
        private bool _toggleSpAtkCutNC;
        public bool ToggleSpAtkCutNC {
            get { return _toggleSpAtkCutNC; }
            set {
                _toggleSpAtkCutNC = value;
                OnPropertyChanged("ToggleSpAtkCutNC");
            }
        }
        private bool _toggleSpAtk;
        public bool ToggleSpAtk {
            get { return _toggleSpAtk; }
            set {
                _toggleSpAtk = value;
                OnPropertyChanged("ToggleSpAtk");
            }
        }
        private int _chunkType;
        public int ChunkType {
            get { return _chunkType; }
            set {
                _chunkType = value;
                OnPropertyChanged("ChunkType");
            }
        }
        private float _blendValue;
        public float BlendValue {
            get { return _blendValue; }
            set {
                _blendValue = value;
                OnPropertyChanged("BlendValue");
            }
        }
        private bool _toggleWin;
        public bool ToggleWin {
            get { return _toggleWin; }
            set {
                _toggleWin = value;
                OnPropertyChanged("ToggleWin");
            }
        }
        private bool _toggleArmorBreak;
        public bool ToggleArmorBreak {
            get { return _toggleArmorBreak; }
            set {
                _toggleArmorBreak = value;
                OnPropertyChanged("ToggleArmorBreak");
            }
        }
        private bool _enableSlot1;
        public bool EnableSlot1 {
            get { return _enableSlot1; }
            set {
                _enableSlot1 = value;
                OnPropertyChanged("EnableSlot1");
            }
        }
        private bool _enableSlot2;
        public bool EnableSlot2 {
            get { return _enableSlot2; }
            set {
                _enableSlot2 = value;
                OnPropertyChanged("EnableSlot2");
            }
        }
        private bool _enableSlot3;
        public bool EnableSlot3 {
            get { return _enableSlot3; }
            set {
                _enableSlot3 = value;
                OnPropertyChanged("EnableSlot3");
            }
        }
        private bool _enableSlot4;
        public bool EnableSlot4 {
            get { return _enableSlot4; }
            set {
                _enableSlot4 = value;
                OnPropertyChanged("EnableSlot4");
            }
        }
        private bool _enableSlot5;
        public bool EnableSlot5 {
            get { return _enableSlot5; }
            set {
                _enableSlot5 = value;
                OnPropertyChanged("EnableSlot5");
            }
        }
        private bool _enableSlot6;
        public bool EnableSlot6 {
            get { return _enableSlot6; }
            set {
                _enableSlot6 = value;
                OnPropertyChanged("EnableSlot6");
            }
        }
        private bool _enableSlot7;
        public bool EnableSlot7 {
            get { return _enableSlot7; }
            set {
                _enableSlot7 = value;
                OnPropertyChanged("EnableSlot7");
            }
        }
        private bool _enableSlot8;
        public bool EnableSlot8 {
            get { return _enableSlot8; }
            set {
                _enableSlot8 = value;
                OnPropertyChanged("EnableSlot8");
            }
        }
        private bool _enableSlot9;
        public bool EnableSlot9 {
            get { return _enableSlot9; }
            set {
                _enableSlot9 = value;
                OnPropertyChanged("EnableSlot9");
            }
        }
        private bool _enableSlot10;
        public bool EnableSlot10 {
            get { return _enableSlot10; }
            set {
                _enableSlot10 = value;
                OnPropertyChanged("EnableSlot10");
            }
        }
        private bool _enableSlot11;
        public bool EnableSlot11 {
            get { return _enableSlot11; }
            set {
                _enableSlot11 = value;
                OnPropertyChanged("EnableSlot11");
            }
        }
        private bool _enableSlot12;
        public bool EnableSlot12 {
            get { return _enableSlot12; }
            set {
                _enableSlot12 = value;
                OnPropertyChanged("EnableSlot12");
            }
        }
        private bool _enableSlot13;
        public bool EnableSlot13 {
            get { return _enableSlot13; }
            set {
                _enableSlot13 = value;
                OnPropertyChanged("EnableSlot13");
            }
        }
        private bool _enableSlot14;
        public bool EnableSlot14 {
            get { return _enableSlot14; }
            set {
                _enableSlot14 = value;
                OnPropertyChanged("EnableSlot14");
            }
        }
        private bool _enableSlot15;
        public bool EnableSlot15 {
            get { return _enableSlot15; }
            set {
                _enableSlot15 = value;
                OnPropertyChanged("EnableSlot15");
            }
        }
        private bool _enableSlot16;
        public bool EnableSlot16 {
            get { return _enableSlot16; }
            set {
                _enableSlot16 = value;
                OnPropertyChanged("EnableSlot16");
            }
        }
        private bool _enableSlot17;
        public bool EnableSlot17 {
            get { return _enableSlot17; }
            set {
                _enableSlot17 = value;
                OnPropertyChanged("EnableSlot17");
            }
        }
        private bool _enableSlot18;
        public bool EnableSlot18 {
            get { return _enableSlot18; }
            set {
                _enableSlot18 = value;
                OnPropertyChanged("EnableSlot18");
            }
        }
        private bool _enableSlot19;
        public bool EnableSlot19 {
            get { return _enableSlot19; }
            set {
                _enableSlot19 = value;
                OnPropertyChanged("EnableSlot19");
            }
        }
        private bool _enableSlot20;
        public bool EnableSlot20 {
            get { return _enableSlot20; }
            set {
                _enableSlot20 = value;
                OnPropertyChanged("EnableSlot20");
            }
        }
        public object Clone() {
            return new AppearanceAnmModel {
                CharacodeID = this.CharacodeID,
                ChunkName = this.ChunkName,
                ToggleEntry = this.ToggleEntry,
                EnableNormalState = this.EnableNormalState,
                Timing = this.Timing,
                EnableAwakeningState = this.EnableAwakeningState,
                ToggleReverseAfterAwakening = this.ToggleReverseAfterAwakening,
                ToggleSpAtkCutNC = this.ToggleSpAtkCutNC,
                ToggleSpAtk = this.ToggleSpAtk,
                ChunkType = this.ChunkType,
                BlendValue = this.BlendValue,
                ToggleWin = this.ToggleWin,
                ToggleArmorBreak = this.ToggleArmorBreak,
                EnableSlot1 = this.EnableSlot1,
                EnableSlot2 = this.EnableSlot2,
                EnableSlot3 = this.EnableSlot3,
                EnableSlot4 = this.EnableSlot4,
                EnableSlot5 = this.EnableSlot5,
                EnableSlot6 = this.EnableSlot6,
                EnableSlot7 = this.EnableSlot7,
                EnableSlot8 = this.EnableSlot8,
                EnableSlot9 = this.EnableSlot9,
                EnableSlot10 = this.EnableSlot10,
                EnableSlot11 = this.EnableSlot11,
                EnableSlot12 = this.EnableSlot12,
                EnableSlot13 = this.EnableSlot13,
                EnableSlot14 = this.EnableSlot14,
                EnableSlot15 = this.EnableSlot15,
                EnableSlot16 = this.EnableSlot16,
                EnableSlot17 = this.EnableSlot17,
                EnableSlot18 = this.EnableSlot18,
                EnableSlot19 = this.EnableSlot19,
                EnableSlot20 = this.EnableSlot20,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
