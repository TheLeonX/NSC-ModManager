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
    public class SpecialInteractionManagerModel : ICloneable, INotifyPropertyChanged
    {

        private int _mainCharacodeID;
        public int MainCharacodeID
        {
            get { return _mainCharacodeID; }
            set
            {
                _mainCharacodeID = value;
                OnPropertyChanged("MainCharacodeID");
            }
        }

        private ObservableCollection<int> _triggerList;
        public ObservableCollection<int> TriggerList
        {
            get { return _triggerList; }
            set
            {
                _triggerList = value;
                OnPropertyChanged("CharacodeList");
            }
        }
        public object Clone()
        {
            ObservableCollection<int> newTriggerList = new ObservableCollection<int>(this.TriggerList);

            return new SpecialInteractionManagerModel
            {
                MainCharacodeID = this.MainCharacodeID,
                TriggerList = newTriggerList
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
