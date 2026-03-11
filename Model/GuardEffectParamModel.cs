using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NSC_ModManager.Model
{
    public class GuardEffectParamModel : ICloneable, INotifyPropertyChanged
    {
        private int _characodeID;
        public int CharacodeID
        {
            get { return _characodeID; }
            set
            {
                _characodeID = value;
                OnPropertyChanged("CharacodeID");
            }
        }
        private int _state;
        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        private string _effectPath;
        public string EffectPath
        {
            get { return _effectPath; }
            set
            {
                _effectPath = value;
                OnPropertyChanged("EffectPath");
            }
        }
        private string _startEffect;
        public string StartEffect
        {
            get { return _startEffect; }
            set
            {
                _startEffect = value;
                OnPropertyChanged("StartEffect");
            }
        }
        private string _loopEffect;
        public string LoopEffect
        {
            get { return _loopEffect; }
            set
            {
                _loopEffect = value;
                OnPropertyChanged("LoopEffect");
            }
        }
        private string _endEffect;
        public string EndEffect
        {
            get { return _endEffect; }
            set
            {
                _endEffect = value;
                OnPropertyChanged("EndEffect");
            }
        }
        private string _attachBone;
        public string AttachBone
        {
            get { return _attachBone; }
            set
            {
                _attachBone = value;
                OnPropertyChanged("AttachBone");
            }
        }
        public object Clone()
        {
            return new GuardEffectParamModel
            {
                CharacodeID = this.CharacodeID,
                State = this.State,
                EffectPath = this.EffectPath,
                StartEffect = this.StartEffect,
                LoopEffect = this.LoopEffect,
                EndEffect = this.EndEffect,
                AttachBone = this.AttachBone,
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
