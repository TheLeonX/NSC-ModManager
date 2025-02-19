using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class PairSpSkillCombinationParamModel : ICloneable, INotifyPropertyChanged
    {
        private string _TUJ_Name;
        public string TUJ_Name
        {
            get { return _TUJ_Name; }
            set
            {
                _TUJ_Name = value;
                OnPropertyChanged("TUJ_Name");
            }
        }
        private int _TUJ_ID;
        public int TUJ_ID
        {
            get { return _TUJ_ID; }
            set
            {
                _TUJ_ID = value;
                OnPropertyChanged("TUJ_ID");
            }
        }
        private float _unk1;
        public float Unk1
        {
            get { return _unk1; }
            set
            {
                _unk1 = value;
                OnPropertyChanged("Unk1");
            }
        }
        private float _unk2;
        public float Unk2
        {
            get { return _unk2; }
            set
            {
                _unk2 = value;
                OnPropertyChanged("Unk2");
            }
        }
        private int _memberCount;
        public int MemberCount
        {
            get { return _memberCount; }
            set
            {
                _memberCount = value;
                OnPropertyChanged("MemberCount");
            }
        }
        private bool _condition1;
        public bool Condition1
        {
            get { return _condition1; }
            set
            {
                _condition1 = value;
                OnPropertyChanged("Condition1");
            }
        }
        private bool _condition2;
        public bool Condition2
        {
            get { return _condition2; }
            set
            {
                _condition2 = value;
                OnPropertyChanged("Condition2");
            }
        }

        private ObservableCollection<int> _characodeList;
        public ObservableCollection<int> CharacodeList
        {
            get { return _characodeList; }
            set
            {
                _characodeList = value;
                OnPropertyChanged("CharacodeList");
            }
        }
        public object Clone()
        {
            ObservableCollection<int> newCharacodeList = new ObservableCollection<int>(this.CharacodeList);

            return new PairSpSkillCombinationParamModel
            {
                TUJ_Name = this.TUJ_Name,
                TUJ_ID = this.TUJ_ID,
                Unk1 = this.Unk1,
                Unk2 = this.Unk2,
                MemberCount = this.MemberCount,
                Condition1 = this.Condition1,
                Condition2 = this.Condition2,
                CharacodeList = newCharacodeList
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
