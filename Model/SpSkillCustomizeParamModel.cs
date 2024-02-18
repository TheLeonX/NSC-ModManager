using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class SpSkillCustomizeParamModel : ICloneable, INotifyPropertyChanged {

        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }

        private float _ultimate1Cost;
        public float Ultimate1Cost {
            get { return _ultimate1Cost; }
            set {
                _ultimate1Cost = value;
                OnPropertyChanged("Ultimate1Cost");
            }
        }
        private float _ultimate1DamageMultiplier;
        public float Ultimate1DamageMultiplier {
            get { return _ultimate1DamageMultiplier; }
            set {
                _ultimate1DamageMultiplier = value;
                OnPropertyChanged("Ultimate1DamageMultiplier");
            }
        }
        private float _ultimate2DamageMultiplier;
        public float Ultimate2DamageMultiplier {
            get { return _ultimate2DamageMultiplier; }
            set {
                _ultimate2DamageMultiplier = value;
                OnPropertyChanged("Ultimate2DamageMultiplier");
            }
        }

        private float _ultimate3DamageMultiplier;
        public float Ultimate3DamageMultiplier {
            get { return _ultimate3DamageMultiplier; }
            set {
                _ultimate3DamageMultiplier = value;
                OnPropertyChanged("Ultimate3DamageMultiplier");
            }
        }

        private float _ultimate4DamageMultiplier;
        public float Ultimate4DamageMultiplier {
            get { return _ultimate4DamageMultiplier; }
            set {
                _ultimate4DamageMultiplier = value;
                OnPropertyChanged("Ultimate4DamageMultiplier");
            }
        }
        private int _ultimate1prior;
        public int Ultimate1prior {
            get { return _ultimate1prior; }
            set {
                _ultimate1prior = value;
                OnPropertyChanged("Ultimate1prior");
            }
        }
        private string _ultimate1name;
        public string Ultimate1name {
            get { return _ultimate1name; }
            set {
                _ultimate1name = value;
                OnPropertyChanged("Ultimate1name");
            }
        }
        private float _ultimate2Cost;
        public float Ultimate2Cost {
            get { return _ultimate2Cost; }
            set {
                _ultimate2Cost = value;
                OnPropertyChanged("Ultimate2Cost");
            }
        }
        private int _ultimate2prior;
        public int Ultimate2prior {
            get { return _ultimate2prior; }
            set {
                _ultimate2prior = value;
                OnPropertyChanged("Ultimate2prior");
            }
        }
        private string _ultimate2name;
        public string Ultimate2name {
            get { return _ultimate2name; }
            set {
                _ultimate2name = value;
                OnPropertyChanged("Ultimate2name");
            }
        }
        private float _ultimate3Cost;
        public float Ultimate3Cost {
            get { return _ultimate3Cost; }
            set {
                _ultimate3Cost = value;
                OnPropertyChanged("Ultimate3Cost");
            }
        }
        private int _ultimate3prior;
        public int Ultimate3prior {
            get { return _ultimate3prior; }
            set {
                _ultimate3prior = value;
                OnPropertyChanged("Ultimate3prior");
            }
        }
        private string _ultimate3name;
        public string Ultimate3name {
            get { return _ultimate3name; }
            set {
                _ultimate3name = value;
                OnPropertyChanged("Ultimate3name");
            }
        }
        private float _ultimate4Cost;
        public float Ultimate4Cost {
            get { return _ultimate4Cost; }
            set {
                _ultimate4Cost = value;
                OnPropertyChanged("Ultimate4Cost");
            }
        }
        private int _ultimate4prior;
        public int Ultimate4prior {
            get { return _ultimate4prior; }
            set {
                _ultimate4prior = value;
                OnPropertyChanged("Ultimate4prior");
            }
        }
        private string _ultimate4name;
        public string Ultimate4name {
            get { return _ultimate4name; }
            set {
                _ultimate4name = value;
                OnPropertyChanged("Ultimate4name");
            }
        }
        public object Clone() {
            return new SpSkillCustomizeParamModel {
                CharacodeID = this.CharacodeID,
                Ultimate1Cost = this.Ultimate1Cost,
                Ultimate1prior = this.Ultimate1prior,
                Ultimate1name = this.Ultimate1name,
                Ultimate2Cost = this.Ultimate2Cost,
                Ultimate2prior = this.Ultimate2prior,
                Ultimate2name = this.Ultimate2name,
                Ultimate3Cost = this.Ultimate3Cost,
                Ultimate3prior = this.Ultimate3prior,
                Ultimate3name = this.Ultimate3name,
                Ultimate4Cost = this.Ultimate4Cost,
                Ultimate4prior = this.Ultimate4prior,
                Ultimate4name = this.Ultimate4name,
                Ultimate1DamageMultiplier = this.Ultimate1DamageMultiplier,
                Ultimate2DamageMultiplier = this.Ultimate2DamageMultiplier,
                Ultimate3DamageMultiplier = this.Ultimate3DamageMultiplier,
                Ultimate4DamageMultiplier = this.Ultimate4DamageMultiplier,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
