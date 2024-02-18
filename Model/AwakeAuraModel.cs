using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model {
    public class AwakeAuraModel : ICloneable, INotifyPropertyChanged {
        private string _characode;
        public string Characode {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("Characode");
            }
        }
        private string _skillFile;
        public string SkillFile {
            get { return _skillFile; }
            set {
                _skillFile = value;
                OnPropertyChanged("SkillFile");
            }
        }
        private string _skillEntryName;
        public string SkillEntryName {
            get { return _skillEntryName; }
            set {
                _skillEntryName = value;
                OnPropertyChanged("SkillEntryName");
            }
        }
        private string _mainBone;
        public string MainBone {
            get { return _mainBone; }
            set {
                _mainBone = value;
                OnPropertyChanged("MainBone");
            }
        }
        private string _secondaryBone;
        public string SecondaryBone {
            get { return _secondaryBone; }
            set {
                _secondaryBone = value;
                OnPropertyChanged("SecondaryBone");
            }
        }
        private bool _mainBoneUsePlayerModel;
        public bool MainBoneUsePlayerModel {
            get { return _mainBoneUsePlayerModel; }
            set {
                _mainBoneUsePlayerModel = value;
                OnPropertyChanged("MainBoneUsePlayerModel");
            }
        }
        private bool _secondaryBoneUsePlayerModel;
        public bool SecondaryBoneUsePlayerModel {
            get { return _secondaryBoneUsePlayerModel; }
            set {
                _secondaryBoneUsePlayerModel = value;
                OnPropertyChanged("SecondaryBoneUsePlayerModel");
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
        private int _condition;
        public int Condition {
            get { return _condition; }
            set {
                _condition = value;
                OnPropertyChanged("Condition");
            }
        }
        private bool _unk1;
        public bool Unk1 {
            get { return _unk1; }
            set {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        public object Clone() {
            return new AwakeAuraModel {
                Characode = this.Characode,
                SkillFile = this.SkillFile,
                SkillEntryName = this.SkillEntryName,
                MainBone = this.MainBone,
                SecondaryBone = this.SecondaryBone,
                MainBoneUsePlayerModel = this.MainBoneUsePlayerModel,
                SecondaryBoneUsePlayerModel = this.SecondaryBoneUsePlayerModel,
                State = this.State,
                Condition = this.Condition,
                Unk1 = this.Unk1,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
