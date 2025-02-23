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
    public class SpecialInteractionModModel : INotifyPropertyChanged
    {
        private string _mainCharacode;
        public string MainCharacode
        {
            get { return _mainCharacode; }
            set
            {
                _mainCharacode = value;
                OnPropertyChanged("MainCharacode");
            }
        }
        private ObservableCollection<string> _triggerList;
        public ObservableCollection<string> TriggerList
        {
            get { return _triggerList; }
            set
            {
                _triggerList = value;
                OnPropertyChanged("TriggerList");
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
