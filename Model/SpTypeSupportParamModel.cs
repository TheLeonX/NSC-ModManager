using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class SpTypeSupportParamModel : ICloneable, INotifyPropertyChanged {

        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private int _state;
        public int State {
            get { return _state; }
            set {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        private int _direction;
        public int Direction {
            get { return _direction; }
            set {
                _direction = value;
                OnPropertyChanged("Direction");
            }
        }
        private string _leftJutsuName;
        public string LeftJutsuName {
            get { return _leftJutsuName; }
            set {
                _leftJutsuName = value;
                OnPropertyChanged("LeftJutsuName");
            }
        }
        private bool _leftJutsuEnableOnGround;
        public bool LeftJutsuEnableOnGround {
            get { return _leftJutsuEnableOnGround; }
            set {
                _leftJutsuEnableOnGround = value;
                OnPropertyChanged("LeftJutsuEnableOnGround");
            }
        }
        private bool _leftJutsuEnableInAir;
        public bool LeftJutsuEnableInAir {
            get { return _leftJutsuEnableInAir; }
            set {
                _leftJutsuEnableInAir = value;
                OnPropertyChanged("LeftJutsuEnableInAir");
            }
        }
        private bool _leftJutsuEnableSpecialCondition;
        public bool LeftJutsuEnableSpecialCondition {
            get { return _leftJutsuEnableSpecialCondition; }
            set {
                _leftJutsuEnableSpecialCondition = value;
                OnPropertyChanged("LeftJutsuEnableSpecialCondition");
            }
        }
        private int _leftJutsuCostumeIndex;
        public int LeftJutsuCostumeIndex {
            get { return _leftJutsuCostumeIndex; }
            set {
                _leftJutsuCostumeIndex = value;
                OnPropertyChanged("LeftJutsuCostumeIndex");
            }
        }
        private string _rightJutsuName;
        public string RightJutsuName {
            get { return _rightJutsuName; }
            set {
                _rightJutsuName = value;
                OnPropertyChanged("RightJutsuName");
            }
        }
        private bool _rightJutsuEnableOnGround;
        public bool RightJutsuEnableOnGround {
            get { return _rightJutsuEnableOnGround; }
            set {
                _rightJutsuEnableOnGround = value;
                OnPropertyChanged("RightJutsuEnableOnGround");
            }
        }
        private bool _rightJutsuEnableInAir;
        public bool RightJutsuEnableInAir {
            get { return _rightJutsuEnableInAir; }
            set {
                _rightJutsuEnableInAir = value;
                OnPropertyChanged("RightJutsuEnableInAir");
            }
        }
        private bool _rightJutsuEnableSpecialCondition;
        public bool RightJutsuEnableSpecialCondition {
            get { return _rightJutsuEnableSpecialCondition; }
            set {
                _rightJutsuEnableSpecialCondition = value;
                OnPropertyChanged("RightJutsuEnableSpecialCondition");
            }
        }
        private int _rightJutsuCostumeIndex;
        public int RightJutsuCostumeIndex {
            get { return _rightJutsuCostumeIndex; }
            set {
                _rightJutsuCostumeIndex = value;
                OnPropertyChanged("RightJutsuCostumeIndex");
            }
        }
        private string _upJutsuName;
        public string UpJutsuName {
            get { return _upJutsuName; }
            set {
                _upJutsuName = value;
                OnPropertyChanged("UpJutsuName");
            }
        }
        private bool _upJutsuEnableOnGround;
        public bool UpJutsuEnableOnGround {
            get { return _upJutsuEnableOnGround; }
            set {
                _upJutsuEnableOnGround = value;
                OnPropertyChanged("UpJutsuEnableOnGround");
            }
        }
        private bool _upJutsuEnableInAir;
        public bool UpJutsuEnableInAir {
            get { return _upJutsuEnableInAir; }
            set {
                _upJutsuEnableInAir = value;
                OnPropertyChanged("UpJutsuEnableInAir");
            }
        }
        private bool _upJutsuEnableSpecialCondition;
        public bool UpJutsuEnableSpecialCondition {
            get { return _upJutsuEnableSpecialCondition; }
            set {
                _upJutsuEnableSpecialCondition = value;
                OnPropertyChanged("UpJutsuEnableSpecialCondition");
            }
        }
        private int _upJutsuCostumeIndex;
        public int UpJutsuCostumeIndex {
            get { return _upJutsuCostumeIndex; }
            set {
                _upJutsuCostumeIndex = value;
                OnPropertyChanged("UpJutsuCostumeIndex");
            }
        }
        private string _downJutsuName;
        public string DownJutsuName {
            get { return _downJutsuName; }
            set {
                _downJutsuName = value;
                OnPropertyChanged("DownJutsuName");
            }
        }
        private bool _downJutsuEnableOnGround;
        public bool DownJutsuEnableOnGround {
            get { return _downJutsuEnableOnGround; }
            set {
                _downJutsuEnableOnGround = value;
                OnPropertyChanged("DownJutsuEnableOnGround");
            }
        }
        private bool _downJutsuEnableInAir;
        public bool DownJutsuEnableInAir {
            get { return _downJutsuEnableInAir; }
            set {
                _downJutsuEnableInAir = value;
                OnPropertyChanged("DownJutsuEnableInAir");
            }
        }
        private bool _downJutsuEnableSpecialCondition;
        public bool DownJutsuEnableSpecialCondition {
            get { return _downJutsuEnableSpecialCondition; }
            set {
                _downJutsuEnableSpecialCondition = value;
                OnPropertyChanged("DownJutsuEnableSpecialCondition");
            }
        }
        private int _downJutsuCostumeIndex;
        public int DownJutsuCostumeIndex {
            get { return _downJutsuCostumeIndex; }
            set {
                _downJutsuCostumeIndex = value;
                OnPropertyChanged("DownJutsuCostumeIndex");
            }
        }

        public object Clone() {
            return new SpTypeSupportParamModel {
                CharacodeID = this.CharacodeID,
                State = this.State,
                Direction = this.Direction,
                LeftJutsuName = this.LeftJutsuName,
                LeftJutsuCostumeIndex = this.LeftJutsuCostumeIndex,
                LeftJutsuEnableOnGround = this.LeftJutsuEnableOnGround,
                LeftJutsuEnableInAir = this.LeftJutsuEnableInAir,
                LeftJutsuEnableSpecialCondition = this.LeftJutsuEnableSpecialCondition,
                RightJutsuName = this.RightJutsuName,
                RightJutsuCostumeIndex = this.RightJutsuCostumeIndex,
                RightJutsuEnableOnGround = this.RightJutsuEnableOnGround,
                RightJutsuEnableInAir = this.RightJutsuEnableInAir,
                RightJutsuEnableSpecialCondition = this.RightJutsuEnableSpecialCondition,
                UpJutsuName = this.UpJutsuName,
                UpJutsuCostumeIndex = this.UpJutsuCostumeIndex,
                UpJutsuEnableOnGround = this.UpJutsuEnableOnGround,
                UpJutsuEnableInAir = this.UpJutsuEnableInAir,
                UpJutsuEnableSpecialCondition = this.UpJutsuEnableSpecialCondition,
                DownJutsuName = this.DownJutsuName,
                DownJutsuCostumeIndex = this.DownJutsuCostumeIndex,
                DownJutsuEnableOnGround = this.DownJutsuEnableOnGround,
                DownJutsuEnableInAir = this.DownJutsuEnableInAir,
                DownJutsuEnableSpecialCondition = this.DownJutsuEnableSpecialCondition
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
