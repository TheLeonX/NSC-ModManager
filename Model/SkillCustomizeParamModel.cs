using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NSC_ModManager.Model {
    public class SkillCustomizeParamModel : ICloneable, INotifyPropertyChanged {

        private int _characodeID;
        public int CharacodeID {
            get { return _characodeID; }
            set {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }

        private float _jutsu1Cost;
        public float Jutsu1Cost {
            get { return _jutsu1Cost; }
            set {
                _jutsu1Cost = value;
                OnPropertyChanged("Jutsu1Cost");
            }
        }
        private float _jutsu1Cancel;
        public float Jutsu1Cancel {
            get { return _jutsu1Cancel; }
            set {
                _jutsu1Cancel = value;
                OnPropertyChanged("Jutsu1Cancel");
            }
        }
        private string _jutsu1_normal_name;
        public string Jutsu1_normal_name {
            get { return _jutsu1_normal_name; }
            set {
                _jutsu1_normal_name = value;
                OnPropertyChanged("Jutsu1_normal_name");
            }
        }
        private string _jutsu1_ex_name;
        public string Jutsu1_ex_name {
            get { return _jutsu1_ex_name; }
            set {
                _jutsu1_ex_name = value;
                OnPropertyChanged("Jutsu1_ex_name");
            }
        }
        private string _jutsu1_air_name;
        public string Jutsu1_air_name {
            get { return _jutsu1_air_name; }
            set {
                _jutsu1_air_name = value;
                OnPropertyChanged("Jutsu1_air_name");
            }
        }
        private int _jutsu1_normal_prior;
        public int Jutsu1_normal_prior {
            get { return _jutsu1_normal_prior; }
            set {
                _jutsu1_normal_prior = value;
                OnPropertyChanged("Jutsu1_normal_prior");
            }
        }
        private int _jutsu1_ex_prior;
        public int Jutsu1_ex_prior {
            get { return _jutsu1_ex_prior; }
            set {
                _jutsu1_ex_prior = value;
                OnPropertyChanged("Jutsu1_ex_prior");
            }
        }
        private int _jutsu1_air_prior;
        public int Jutsu1_air_prior {
            get { return _jutsu1_air_prior; }
            set {
                _jutsu1_air_prior = value;
                OnPropertyChanged("Jutsu1_air_prior");
            }
        }
        private float _jutsu2Cost;
        public float Jutsu2Cost {
            get { return _jutsu2Cost; }
            set {
                _jutsu2Cost = value;
                OnPropertyChanged("Jutsu2Cost");
            }
        }
        private float _jutsu2Cancel;
        public float Jutsu2Cancel {
            get { return _jutsu2Cancel; }
            set {
                _jutsu2Cancel = value;
                OnPropertyChanged("Jutsu2Cancel");
            }
        }
        private string _jutsu2_normal_name;
        public string Jutsu2_normal_name {
            get { return _jutsu2_normal_name; }
            set {
                _jutsu2_normal_name = value;
                OnPropertyChanged("Jutsu2_normal_name");
            }
        }
        private string _jutsu2_ex_name;
        public string Jutsu2_ex_name {
            get { return _jutsu2_ex_name; }
            set {
                _jutsu2_ex_name = value;
                OnPropertyChanged("Jutsu2_ex_name");
            }
        }
        private string _jutsu2_air_name;
        public string Jutsu2_air_name {
            get { return _jutsu2_air_name; }
            set {
                _jutsu2_air_name = value;
                OnPropertyChanged("Jutsu2_air_name");
            }
        }
        private int _jutsu2_normal_prior;
        public int Jutsu2_normal_prior {
            get { return _jutsu2_normal_prior; }
            set {
                _jutsu2_normal_prior = value;
                OnPropertyChanged("Jutsu2_normal_prior");
            }
        }
        private int _jutsu2_ex_prior;
        public int Jutsu2_ex_prior {
            get { return _jutsu2_ex_prior; }
            set {
                _jutsu2_ex_prior = value;
                OnPropertyChanged("Jutsu2_ex_prior");
            }
        }
        private int _jutsu2_air_prior;
        public int Jutsu2_air_prior {
            get { return _jutsu2_air_prior; }
            set {
                _jutsu2_air_prior = value;
                OnPropertyChanged("Jutsu2_air_prior");
            }
        }
        private float _jutsu3Cost;
        public float Jutsu3Cost {
            get { return _jutsu3Cost; }
            set {
                _jutsu3Cost = value;
                OnPropertyChanged("Jutsu3Cost");
            }
        }
        private float _jutsu3Cancel;
        public float Jutsu3Cancel {
            get { return _jutsu3Cancel; }
            set {
                _jutsu3Cancel = value;
                OnPropertyChanged("Jutsu3Cancel");
            }
        }
        private string _jutsu3_normal_name;
        public string Jutsu3_normal_name {
            get { return _jutsu3_normal_name; }
            set {
                _jutsu3_normal_name = value;
                OnPropertyChanged("Jutsu3_normal_name");
            }
        }
        private string _jutsu3_ex_name;
        public string Jutsu3_ex_name {
            get { return _jutsu3_ex_name; }
            set {
                _jutsu3_ex_name = value;
                OnPropertyChanged("Jutsu3_ex_name");
            }
        }
        private string _jutsu3_air_name;
        public string Jutsu3_air_name {
            get { return _jutsu3_air_name; }
            set {
                _jutsu3_air_name = value;
                OnPropertyChanged("Jutsu3_air_name");
            }
        }
        private int _jutsu3_normal_prior;
        public int Jutsu3_normal_prior {
            get { return _jutsu3_normal_prior; }
            set {
                _jutsu3_normal_prior = value;
                OnPropertyChanged("Jutsu3_normal_prior");
            }
        }
        private int _jutsu3_ex_prior;
        public int Jutsu3_ex_prior {
            get { return _jutsu3_ex_prior; }
            set {
                _jutsu3_ex_prior = value;
                OnPropertyChanged("Jutsu3_ex_prior");
            }
        }
        private int _jutsu3_air_prior;
        public int Jutsu3_air_prior {
            get { return _jutsu3_air_prior; }
            set {
                _jutsu3_air_prior = value;
                OnPropertyChanged("Jutsu3_air_prior");
            }
        }
        private float _jutsu4Cost;
        public float Jutsu4Cost {
            get { return _jutsu4Cost; }
            set {
                _jutsu4Cost = value;
                OnPropertyChanged("Jutsu4Cost");
            }
        }
        private float _jutsu4Cancel;
        public float Jutsu4Cancel {
            get { return _jutsu4Cancel; }
            set {
                _jutsu4Cancel = value;
                OnPropertyChanged("Jutsu4Cancel");
            }
        }
        private string _jutsu4_normal_name;
        public string Jutsu4_normal_name {
            get { return _jutsu4_normal_name; }
            set {
                _jutsu4_normal_name = value;
                OnPropertyChanged("Jutsu4_normal_name");
            }
        }
        private string _jutsu4_ex_name;
        public string Jutsu4_ex_name {
            get { return _jutsu4_ex_name; }
            set {
                _jutsu4_ex_name = value;
                OnPropertyChanged("Jutsu4_ex_name");
            }
        }
        private string _jutsu4_air_name;
        public string Jutsu4_air_name {
            get { return _jutsu4_air_name; }
            set {
                _jutsu4_air_name = value;
                OnPropertyChanged("Jutsu4_air_name");
            }
        }
        private int _jutsu4_normal_prior;
        public int Jutsu4_normal_prior {
            get { return _jutsu4_normal_prior; }
            set {
                _jutsu4_normal_prior = value;
                OnPropertyChanged("Jutsu4_normal_prior");
            }
        }
        private int _jutsu4_ex_prior;
        public int Jutsu4_ex_prior {
            get { return _jutsu4_ex_prior; }
            set {
                _jutsu4_ex_prior = value;
                OnPropertyChanged("Jutsu4_ex_prior");
            }
        }
        private int _jutsu4_air_prior;
        public int Jutsu4_air_prior {
            get { return _jutsu4_air_prior; }
            set {
                _jutsu4_air_prior = value;
                OnPropertyChanged("Jutsu4_air_prior");
            }
        }
        private float _jutsu5Cost;
        public float Jutsu5Cost {
            get { return _jutsu5Cost; }
            set {
                _jutsu5Cost = value;
                OnPropertyChanged("Jutsu5Cost");
            }
        }
        private float _jutsu5Cancel;
        public float Jutsu5Cancel {
            get { return _jutsu5Cancel; }
            set {
                _jutsu5Cancel = value;
                OnPropertyChanged("Jutsu5Cancel");
            }
        }
        private string _jutsu5_normal_name;
        public string Jutsu5_normal_name {
            get { return _jutsu5_normal_name; }
            set {
                _jutsu5_normal_name = value;
                OnPropertyChanged("Jutsu5_normal_name");
            }
        }
        private string _jutsu5_ex_name;
        public string Jutsu5_ex_name {
            get { return _jutsu5_ex_name; }
            set {
                _jutsu5_ex_name = value;
                OnPropertyChanged("Jutsu5_ex_name");
            }
        }
        private string _jutsu5_air_name;
        public string Jutsu5_air_name {
            get { return _jutsu5_air_name; }
            set {
                _jutsu5_air_name = value;
                OnPropertyChanged("Jutsu5_air_name");
            }
        }
        private int _jutsu5_normal_prior;
        public int Jutsu5_normal_prior {
            get { return _jutsu5_normal_prior; }
            set {
                _jutsu5_normal_prior = value;
                OnPropertyChanged("Jutsu5_normal_prior");
            }
        }
        private int _jutsu5_ex_prior;
        public int Jutsu5_ex_prior {
            get { return _jutsu5_ex_prior; }
            set {
                _jutsu5_ex_prior = value;
                OnPropertyChanged("Jutsu5_ex_prior");
            }
        }
        private int _jutsu5_air_prior;
        public int Jutsu5_air_prior {
            get { return _jutsu5_air_prior; }
            set {
                _jutsu5_air_prior = value;
                OnPropertyChanged("Jutsu5_air_prior");
            }
        }
        private float _jutsu6Cost;
        public float Jutsu6Cost {
            get { return _jutsu6Cost; }
            set {
                _jutsu6Cost = value;
                OnPropertyChanged("Jutsu6Cost");
            }
        }
        private float _jutsu6Cancel;
        public float Jutsu6Cancel {
            get { return _jutsu6Cancel; }
            set {
                _jutsu6Cancel = value;
                OnPropertyChanged("Jutsu6Cancel");
            }
        }
        private string _jutsu6_normal_name;
        public string Jutsu6_normal_name {
            get { return _jutsu6_normal_name; }
            set {
                _jutsu6_normal_name = value;
                OnPropertyChanged("Jutsu6_normal_name");
            }
        }
        private string _jutsu6_ex_name;
        public string Jutsu6_ex_name {
            get { return _jutsu6_ex_name; }
            set {
                _jutsu6_ex_name = value;
                OnPropertyChanged("Jutsu6_ex_name");
            }
        }
        private string _jutsu6_air_name;
        public string Jutsu6_air_name {
            get { return _jutsu6_air_name; }
            set {
                _jutsu6_air_name = value;
                OnPropertyChanged("Jutsu6_air_name");
            }
        }
        private int _jutsu6_normal_prior;
        public int Jutsu6_normal_prior {
            get { return _jutsu6_normal_prior; }
            set {
                _jutsu6_normal_prior = value;
                OnPropertyChanged("Jutsu6_normal_prior");
            }
        }
        private int _jutsu6_ex_prior;
        public int Jutsu6_ex_prior {
            get { return _jutsu6_ex_prior; }
            set {
                _jutsu6_ex_prior = value;
                OnPropertyChanged("Jutsu6_ex_prior");
            }
        }
        private int _jutsu6_air_prior;
        public int Jutsu6_air_prior {
            get { return _jutsu6_air_prior; }
            set {
                _jutsu6_air_prior = value;
                OnPropertyChanged("Jutsu6_air_prior");
            }
        }
        private float _jutsu1_awaCost;
        public float Jutsu1_awaCost {
            get { return _jutsu1_awaCost; }
            set {
                _jutsu1_awaCost = value;
                OnPropertyChanged("Jutsu1_awaCost");
            }
        }
        private float _jutsu1_awaCancel;
        public float Jutsu1_awaCancel {
            get { return _jutsu1_awaCancel; }
            set {
                _jutsu1_awaCancel = value;
                OnPropertyChanged("Jutsu1_awaCancel");
            }
        }
        private string _jutsu1_awa_normal_name;
        public string Jutsu1_awa_normal_name {
            get { return _jutsu1_awa_normal_name; }
            set {
                _jutsu1_awa_normal_name = value;
                OnPropertyChanged("Jutsu1_awa_normal_name");
            }
        }
        private string _jutsu1_awa_ex_name;
        public string Jutsu1_awa_ex_name {
            get { return _jutsu1_awa_ex_name; }
            set {
                _jutsu1_awa_ex_name = value;
                OnPropertyChanged("Jutsu1_awa_ex_name");
            }
        }
        private string _jutsu1_awa_air_name;
        public string Jutsu1_awa_air_name {
            get { return _jutsu1_awa_air_name; }
            set {
                _jutsu1_awa_air_name = value;
                OnPropertyChanged("Jutsu1_awa_air_name");
            }
        }
        private int _jutsu1_awa_normal_prior;
        public int Jutsu1_awa_normal_prior {
            get { return _jutsu1_awa_normal_prior; }
            set {
                _jutsu1_awa_normal_prior = value;
                OnPropertyChanged("Jutsu1_awa_normal_prior");
            }
        }
        private int _jutsu1_awa_ex_prior;
        public int Jutsu1_awa_ex_prior {
            get { return _jutsu1_awa_ex_prior; }
            set {
                _jutsu1_awa_ex_prior = value;
                OnPropertyChanged("Jutsu1_awa_ex_prior");
            }
        }
        private int _jutsu1_awa_air_prior;
        public int Jutsu1_awa_air_prior {
            get { return _jutsu1_awa_air_prior; }
            set {
                _jutsu1_awa_air_prior = value;
                OnPropertyChanged("Jutsu1_awa_air_prior");
            }
        }
        private float _jutsu2_awaCost;
        public float Jutsu2_awaCost {
            get { return _jutsu2_awaCost; }
            set {
                _jutsu2_awaCost = value;
                OnPropertyChanged("Jutsu2_awaCost");
            }
        }
        private float _jutsu2_awaCancel;
        public float Jutsu2_awaCancel {
            get { return _jutsu2_awaCancel; }
            set {
                _jutsu2_awaCancel = value;
                OnPropertyChanged("Jutsu2_awaCancel");
            }
        }
        private string _jutsu2_awa_normal_name;
        public string Jutsu2_awa_normal_name {
            get { return _jutsu2_awa_normal_name; }
            set {
                _jutsu2_awa_normal_name = value;
                OnPropertyChanged("Jutsu2_awa_normal_name");
            }
        }
        private string _jutsu2_awa_ex_name;
        public string Jutsu2_awa_ex_name {
            get { return _jutsu2_awa_ex_name; }
            set {
                _jutsu2_awa_ex_name = value;
                OnPropertyChanged("Jutsu2_awa_ex_name");
            }
        }
        private string _jutsu2_awa_air_name;
        public string Jutsu2_awa_air_name {
            get { return _jutsu2_awa_air_name; }
            set {
                _jutsu2_awa_air_name = value;
                OnPropertyChanged("Jutsu2_awa_air_name");
            }
        }
        private int _jutsu2_awa_normal_prior;
        public int Jutsu2_awa_normal_prior {
            get { return _jutsu2_awa_normal_prior; }
            set {
                _jutsu2_awa_normal_prior = value;
                OnPropertyChanged("Jutsu2_awa_normal_prior");
            }
        }
        private int _jutsu2_awa_ex_prior;
        public int Jutsu2_awa_ex_prior {
            get { return _jutsu2_awa_ex_prior; }
            set {
                _jutsu2_awa_ex_prior = value;
                OnPropertyChanged("Jutsu2_awa_ex_prior");
            }
        }
        private int _jutsu2_awa_air_prior;
        public int Jutsu2_awa_air_prior {
            get { return _jutsu2_awa_air_prior; }
            set {
                _jutsu2_awa_air_prior = value;
                OnPropertyChanged("Jutsu2_awa_air_prior");
            }
        }
        public object Clone() {
            return new SkillCustomizeParamModel {
                CharacodeID = this.CharacodeID,
                Jutsu1Cost = this.Jutsu1Cost,
                Jutsu1Cancel = this.Jutsu1Cancel,
                Jutsu1_normal_name = this.Jutsu1_normal_name,
                Jutsu1_air_name = this.Jutsu1_air_name,
                Jutsu1_ex_name = this.Jutsu1_ex_name,
                Jutsu1_normal_prior = this.Jutsu1_normal_prior,
                Jutsu1_ex_prior = this.Jutsu1_ex_prior,
                Jutsu1_air_prior = this.Jutsu1_air_prior,
                Jutsu2Cost = this.Jutsu2Cost,
                Jutsu2Cancel = this.Jutsu2Cancel,
                Jutsu2_normal_name = this.Jutsu2_normal_name,
                Jutsu2_air_name = this.Jutsu2_air_name,
                Jutsu2_ex_name = this.Jutsu2_ex_name,
                Jutsu2_normal_prior = this.Jutsu2_normal_prior,
                Jutsu2_ex_prior = this.Jutsu2_ex_prior,
                Jutsu2_air_prior = this.Jutsu2_air_prior,
                Jutsu3Cost = this.Jutsu3Cost,
                Jutsu3Cancel = this.Jutsu3Cancel,
                Jutsu3_normal_name = this.Jutsu3_normal_name,
                Jutsu3_air_name = this.Jutsu3_air_name,
                Jutsu3_ex_name = this.Jutsu3_ex_name,
                Jutsu3_normal_prior = this.Jutsu3_normal_prior,
                Jutsu3_ex_prior = this.Jutsu3_ex_prior,
                Jutsu3_air_prior = this.Jutsu3_air_prior,
                Jutsu4Cost = this.Jutsu4Cost,
                Jutsu4Cancel = this.Jutsu4Cancel,
                Jutsu4_normal_name = this.Jutsu4_normal_name,
                Jutsu4_air_name = this.Jutsu4_air_name,
                Jutsu4_ex_name = this.Jutsu4_ex_name,
                Jutsu4_normal_prior = this.Jutsu4_normal_prior,
                Jutsu4_ex_prior = this.Jutsu4_ex_prior,
                Jutsu4_air_prior = this.Jutsu4_air_prior,
                Jutsu5Cost = this.Jutsu5Cost,
                Jutsu5Cancel = this.Jutsu5Cancel,
                Jutsu5_normal_name = this.Jutsu5_normal_name,
                Jutsu5_air_name = this.Jutsu5_air_name,
                Jutsu5_ex_name = this.Jutsu5_ex_name,
                Jutsu5_normal_prior = this.Jutsu5_normal_prior,
                Jutsu5_ex_prior = this.Jutsu5_ex_prior,
                Jutsu5_air_prior = this.Jutsu5_air_prior,
                Jutsu6Cost = this.Jutsu6Cost,
                Jutsu6Cancel = this.Jutsu6Cancel,
                Jutsu6_normal_name = this.Jutsu6_normal_name,
                Jutsu6_air_name = this.Jutsu6_air_name,
                Jutsu6_ex_name = this.Jutsu6_ex_name,
                Jutsu6_normal_prior = this.Jutsu6_normal_prior,
                Jutsu6_ex_prior = this.Jutsu6_ex_prior,
                Jutsu6_air_prior = this.Jutsu6_air_prior,
                Jutsu1_awaCost = this.Jutsu1_awaCost,
                Jutsu1_awaCancel = this.Jutsu1_awaCancel,
                Jutsu1_awa_normal_name = this.Jutsu1_awa_normal_name,
                Jutsu1_awa_air_name = this.Jutsu1_awa_air_name,
                Jutsu1_awa_ex_name = this.Jutsu1_awa_ex_name,
                Jutsu1_awa_normal_prior = this.Jutsu1_awa_normal_prior,
                Jutsu1_awa_ex_prior = this.Jutsu1_awa_ex_prior,
                Jutsu1_awa_air_prior = this.Jutsu1_awa_air_prior,
                Jutsu2_awaCost = this.Jutsu2_awaCost,
                Jutsu2_awaCancel = this.Jutsu2_awaCancel,
                Jutsu2_awa_normal_name = this.Jutsu2_awa_normal_name,
                Jutsu2_awa_air_name = this.Jutsu2_awa_air_name,
                Jutsu2_awa_ex_name = this.Jutsu2_awa_ex_name,
                Jutsu2_awa_normal_prior = this.Jutsu2_awa_normal_prior,
                Jutsu2_awa_ex_prior = this.Jutsu2_awa_ex_prior,
                Jutsu2_awa_air_prior = this.Jutsu2_awa_air_prior,
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
