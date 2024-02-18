using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class DamagePrmModel : ICloneable, INotifyPropertyChanged {
        private byte[] _data;
        public byte[] Data {
            get { return _data; }
            set {
                _data = value;
                OnPropertyChanged("Data");
            }
        }
        
        public object Clone() {
            return new DamagePrmModel {
                Data = this.Data
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
