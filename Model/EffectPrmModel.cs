using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class EffectPrmModel : ICloneable, INotifyPropertyChanged {
        private int _effectPrmID;
        public int EffectPrmID {
            get { return _effectPrmID; }
            set {
                _effectPrmID = value;
                OnPropertyChanged("EffectPrmID");
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
        private string _filePath;
        public string FilePath {
            get { return _filePath; }
            set {
                _filePath = value;
                OnPropertyChanged("FilePath");
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
        public object Clone() {
            return new EffectPrmModel {
                EffectPrmID = this.EffectPrmID,
                Type = this.Type,
                FilePath = this.FilePath,
                ChunkName = this.ChunkName,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
