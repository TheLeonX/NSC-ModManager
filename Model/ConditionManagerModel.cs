using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class ConditionManagerModel : ICloneable, INotifyPropertyChanged
    {
        private string _conditionName;
        public string ConditionName
        {
            get { return _conditionName; }
            set
            {
                _conditionName = value;
                OnPropertyChanged("ConditionName");
            }
        }
        private string _afterConditionName;
        public string AfterConditionName
        {
            get { return _afterConditionName; }
            set
            {
                _afterConditionName = value;
                OnPropertyChanged("AfterConditionName");
            }
        }
        private int _conditionValue;
        public int ConditionValue
        {
            get { return _conditionValue; }
            set
            {
                _conditionValue = value;
                OnPropertyChanged("ConditionValue");
            }
        }
        private int _conditionIcon;
        public int ConditionIcon
        {
            get { return _conditionIcon; }
            set
            {
                _conditionIcon = value;
                OnPropertyChanged("ConditionIcon");
            }
        }
        private int _conditionStatusEffect;
        public int ConditionStatusEffect
        {
            get { return _conditionStatusEffect; }
            set
            {
                _conditionStatusEffect = value;
                OnPropertyChanged("ConditionStatusEffect");
            }
        }
        private int _conditionAuraEffect;
        public int ConditionAuraEffect
        {
            get { return _conditionAuraEffect; }
            set
            {
                _conditionAuraEffect = value;
                OnPropertyChanged("ConditionAuraEffect");
            }
        }
        public object Clone()
        {
            return new ConditionManagerModel
            {
                ConditionName = ConditionName,
                AfterConditionName = AfterConditionName,
                ConditionValue = ConditionValue,
                ConditionIcon = ConditionIcon,
                ConditionStatusEffect = ConditionStatusEffect,
                ConditionAuraEffect = ConditionAuraEffect
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
