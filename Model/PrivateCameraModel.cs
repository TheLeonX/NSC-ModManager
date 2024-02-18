using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class PrivateCameraModel : ICloneable, INotifyPropertyChanged {
        private int _characodeIndex;
        public int CharacodeIndex {
            get { return _characodeIndex; }
            set {
                _characodeIndex = value;
                OnPropertyChanged("CharacodeIndex");
            }
        }
        private float _cameraDistance;
        public float CameraDistance {
            get { return _cameraDistance; }
            set {
                _cameraDistance = value;
                OnPropertyChanged("CameraDistance");
            }
        }
        private float _cameraSpeed;
        public float CameraSpeed {
            get { return _cameraSpeed; }
            set {
                _cameraSpeed = value;
                OnPropertyChanged("CameraSpeed");
            }
        }
        private float _cameraMovement;
        public float CameraMovement {
            get { return _cameraMovement; }
            set {
                _cameraMovement = value;
                OnPropertyChanged("CameraMovement");
            }
        }
        private float _unk1;
        public float Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private float _cameraHeight;
        public float CameraHeight {
            get { return _cameraHeight; }
            set {
                _cameraHeight = value;
                OnPropertyChanged("CameraHeight");
            }
        }
        private float _cameraAngle;
        public float CameraAngle {
            get { return _cameraAngle; }
            set {
                _cameraAngle = value;
                OnPropertyChanged("CameraAngle");
            }
        }
        private float _cameraHeight2;
        public float CameraHeight2 {
            get { return _cameraHeight2; }
            set {
                _cameraHeight2 = value;
                OnPropertyChanged("CameraHeight2");
            }
        }
        private float _fov;
        public float FOV {
            get { return _fov; }
            set {
                _fov = value;
                OnPropertyChanged("FOV");
            }
        }
        private float _unk2;
        public float Unk2 {
            get { return _unk2; }
            set {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private float _cameraDistance2;
        public float CameraDistance2 {
            get { return _cameraDistance2; }
            set {
                _cameraDistance2 = value;
                OnPropertyChanged("CameraDistance2");
            }
        }
        private float _fov2;
        public float FOV2 {
            get { return _fov2; }
            set {
                _fov2 = value;
                OnPropertyChanged("FOV2");
            }
        }
        public object Clone() {
            return new PrivateCameraModel {
                CharacodeIndex = this.CharacodeIndex,
                CameraDistance = this.CameraDistance,
                CameraSpeed = this.CameraSpeed,
                CameraMovement = this.CameraMovement,
                Unk1 = this.Unk1,
                CameraHeight = this.CameraHeight,
                CameraAngle = this.CameraAngle,
                CameraHeight2 = this.CameraHeight2,
                FOV = this.FOV,
                Unk2 = this.Unk2,
                CameraDistance2 = this.CameraDistance2,
                FOV2 = this.FOV2,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
