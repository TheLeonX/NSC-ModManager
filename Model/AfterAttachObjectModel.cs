using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class AfterAttachObjectModel : ICloneable, INotifyPropertyChanged {
        private string _characode;
        public string Characode {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("Characode");
            }
        }
        private string _costume;
        public string Costume {
            get { return _costume; }
            set {
                _costume = value;
                OnPropertyChanged("Costume");
            }
        }
        private string _attachBone1;
        public string AttachBone1 {
            get { return _attachBone1; }
            set {
                _attachBone1 = value;
                OnPropertyChanged("AttachBone1");
            }
        }
        private string _filePath;
        public string FilePath {
            get { return _filePath; }
            set {
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }
        private int _type;
        public int Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type");
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
        private string _attachBone2;
        public string AttachBone2 {
            get { return _attachBone2; }
            set {
                _attachBone2 = value;
                OnPropertyChanged("AttachBone2");
            }
        }
        
        private float _posX;
        public float PosX {
            get { return _posX; }
            set {
                _posX = value;
                OnPropertyChanged("PosX");
            }
        }
        private float _posY;
        public float PosY {
            get { return _posY; }
            set {
                _posY = value;
                OnPropertyChanged("PosY");
            }
        }
        private float _posZ;
        public float PosZ {
            get { return _posZ; }
            set {
                _posZ = value;
                OnPropertyChanged("PosZ");
            }
        }
        private float _rotX;
        public float RotX {
            get { return _rotX; }
            set {
                _rotX = value;
                OnPropertyChanged("RotX");
            }
        }
        private float _rotY;
        public float RotY {
            get { return _rotY; }
            set {
                _rotY = value;
                OnPropertyChanged("RotY");
            }
        }
        private float _rotZ;
        public float RotZ {
            get { return _rotZ; }
            set {
                _rotZ = value;
                OnPropertyChanged("RotZ");
            }
        }
        private float _scaleX;
        public float ScaleX {
            get { return _scaleX; }
            set {
                _scaleX = value;
                OnPropertyChanged("ScaleX");
            }
        }
        private float _scaleY;
        public float ScaleY {
            get { return _scaleY; }
            set {
                _scaleY = value;
                OnPropertyChanged("ScaleY");
            }
        }
        private float _scaleZ;
        public float ScaleZ {
            get { return _scaleZ; }
            set {
                _scaleZ = value;
                OnPropertyChanged("ScaleZ");
            }
        }
        private int _condition;
        public int Condition {
            get { return _condition; }
            set {
                _condition = value;
                OnPropertyChanged("Condition");
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
        public object Clone() {
            return new AfterAttachObjectModel {
                Characode = this.Characode,
                Costume = this.Costume,
                AttachBone1 = this.AttachBone1,
                FilePath = this.FilePath,
                Type = this.Type,
                ChunkName = this.ChunkName,
                AttachBone2 = this.AttachBone2,
                PosX = this.PosX,
                PosY = this.PosY,
                PosZ = this.PosZ,
                RotX = this.RotX,
                RotY = this.RotY,
                RotZ = this.RotZ,
                ScaleX = this.ScaleX,
                ScaleY = this.ScaleY,
                ScaleZ = this.ScaleZ,
                Condition = this.Condition,
                State = this.State
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
