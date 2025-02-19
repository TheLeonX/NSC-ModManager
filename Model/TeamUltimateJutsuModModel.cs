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
    public class TeamUltimateJutsuModModel : INotifyPropertyChanged
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged("Label");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private bool _flag1;
        public bool Flag1
        {
            get { return _flag1; }
            set
            {
                _flag1 = value;
                OnPropertyChanged("Flag1");
            }
        }
        private bool _flag2;
        public bool Flag2
        {
            get { return _flag2; }
            set
            {
                _flag2 = value;
                OnPropertyChanged("Flag2");
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
        private ObservableCollection<string> _characodeList;
        public ObservableCollection<string> CharacodeList
        {
            get { return _characodeList; }
            set
            {
                _characodeList = value;
                OnPropertyChanged("CharacodeList");
            }
        }
        private string _rootPath;
        public string RootPath
        {
            get { return _rootPath; }
            set
            {
                _rootPath = value;
                OnPropertyChanged("RootPath");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
