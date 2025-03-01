using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class ConditionPrmModel : ICloneable, INotifyPropertyChanged
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
        private int _conditionDuration;
        public int ConditionDuration
        {
            get { return _conditionDuration; }
            set
            {
                _conditionDuration = value;
                OnPropertyChanged("ConditionDuration");
            }
        }
        private float _conditionATK;
        public float ConditionATK
        {
            get { return _conditionATK; }
            set
            {
                _conditionATK = value;
                OnPropertyChanged("ConditionATK");
            }
        }
        private float _conditionDEF;
        public float ConditionDEF
        {
            get { return _conditionDEF; }
            set
            {
                _conditionDEF = value;
                OnPropertyChanged("ConditionDEF");
            }
        }
        private float _conditionSPD;
        public float ConditionSPD
        {
            get { return _conditionSPD; }
            set
            {
                _conditionSPD = value;
                OnPropertyChanged("ConditionSPD");
            }
        }
        private float _conditionSPT_ATK;
        public float ConditionSPT_ATK
        {
            get { return _conditionSPT_ATK; }
            set
            {
                _conditionSPT_ATK = value;
                OnPropertyChanged("ConditionSPT_ATK");
            }
        }
        private float _conditionHP_Recover;
        public float ConditionHP_Recover
        {
            get { return _conditionHP_Recover; }
            set
            {
                _conditionHP_Recover = value;
                OnPropertyChanged("ConditionHP_Recover");
            }
        }
        private float _conditionPoison;
        public float ConditionPoison
        {
            get { return _conditionPoison; }
            set
            {
                _conditionPoison = value;
                OnPropertyChanged("ConditionPoison");
            }
        }
        private float _conditionChakra_recover;
        public float ConditionChakra_recover
        {
            get { return _conditionChakra_recover; }
            set
            {
                _conditionChakra_recover = value;
                OnPropertyChanged("ConditionChakra_recover");
            }
        }
        private float _conditionChakra_shave;
        public float ConditionChakra_shave
        {
            get { return _conditionChakra_shave; }
            set
            {
                _conditionChakra_shave = value;
                OnPropertyChanged("ConditionChakra_shave");
            }
        }
        private float _conditionChakra_revival;
        public float ConditionChakra_revival
        {
            get { return _conditionChakra_revival; }
            set
            {
                _conditionChakra_revival = value;
                OnPropertyChanged("ConditionChakra_revival");
            }
        }
        private float _conditionChakra_Drain;
        public float ConditionChakra_Drain
        {
            get { return _conditionChakra_Drain; }
            set
            {
                _conditionChakra_Drain = value;
                OnPropertyChanged("ConditionChakra_Drain");
            }
        }
        private float _conditionChakra_unk;
        public float ConditionChakra_unk
        {
            get { return _conditionChakra_unk; }
            set
            {
                _conditionChakra_unk = value;
                OnPropertyChanged("ConditionChakra_unk");
            }
        }
        private float _conditionChakra;
        public float ConditionChakra
        {
            get { return _conditionChakra; }
            set
            {
                _conditionChakra = value;
                OnPropertyChanged("ConditionChakra");
            }
        }
        private float _conditionChakra_Usage;
        public float ConditionChakra_Usage
        {
            get { return _conditionChakra_Usage; }
            set
            {
                _conditionChakra_Usage = value;
                OnPropertyChanged("ConditionChakra_Usage");
            }
        }
        private float _conditionSupport;
        public float ConditionSupport
        {
            get { return _conditionSupport; }
            set
            {
                _conditionSupport = value;
                OnPropertyChanged("ConditionSupport");
            }
        }
        private float _conditionTeam;
        public float ConditionTeam
        {
            get { return _conditionTeam; }
            set
            {
                _conditionTeam = value;
                OnPropertyChanged("ConditionTeam");
            }
        }
        private float _conditionGuardBreak;
        public float ConditionGuardBreak
        {
            get { return _conditionGuardBreak; }
            set
            {
                _conditionGuardBreak = value;
                OnPropertyChanged("ConditionGuardBreak");
            }
        }
        private float _conditionDodge;
        public float ConditionDodge
        {
            get { return _conditionDodge; }
            set
            {
                _conditionDodge = value;
                OnPropertyChanged("ConditionDodge");
            }
        }
        private bool _conditionProjectile;
        public bool ConditionProjectile
        {
            get { return _conditionProjectile; }
            set
            {
                _conditionProjectile = value;
                OnPropertyChanged("ConditionProjectile");
            }
        }
        private bool _conditionAutoDodge;
        public bool ConditionAutoDodge
        {
            get { return _conditionAutoDodge; }
            set
            {
                _conditionAutoDodge = value;
                OnPropertyChanged("ConditionAutoDodge");
            }
        }
        private bool _conditionSeal;
        public bool ConditionSeal
        {
            get { return _conditionSeal; }
            set
            {
                _conditionSeal = value;
                OnPropertyChanged("ConditionSeal");
            }
        }
        private bool _conditionSleep;
        public bool ConditionSleep
        {
            get { return _conditionSleep; }
            set
            {
                _conditionSleep = value;
                OnPropertyChanged("ConditionSleep");
            }
        }
        private bool _conditionStun;
        public bool ConditionStun
        {
            get { return _conditionStun; }
            set
            {
                _conditionStun = value;
                OnPropertyChanged("ConditionStun");
            }
        }
        private int _conditionFlashType;
        public int ConditionFlashType
        {
            get { return _conditionFlashType; }
            set
            {
                _conditionFlashType = value;
                OnPropertyChanged("ConditionFlashType");
            }
        }
        private Color _conditionFlashColor;
        public Color ConditionFlashColor
        {
            get { return _conditionFlashColor; }
            set
            {
                _conditionFlashColor = value;
                OnPropertyChanged("ConditionFlashColor");
            }
        }
        private float _conditionFlash_unk1;
        public float ConditionFlash_unk1
        {
            get { return _conditionFlash_unk1; }
            set
            {
                _conditionFlash_unk1 = value;
                OnPropertyChanged("ConditionFlash_unk1");
            }
        }


        private float _conditionFlash_interval;
        public float ConditionFlash_interval
        {
            get { return _conditionFlash_interval; }
            set
            {
                _conditionFlash_interval = value;
                OnPropertyChanged("ConditionFlash_interval");
            }
        }

        private float _conditionFlash_unk2;
        public float ConditionFlash_unk2
        {
            get { return _conditionFlash_unk2; }
            set
            {
                _conditionFlash_unk2 = value;
                OnPropertyChanged("ConditionFlash_unk2");
            }
        }

        private float _conditionFlash_opacity;
        public float ConditionFlash_opacity
        {
            get { return _conditionFlash_opacity; }
            set
            {
                _conditionFlash_opacity = value;
                OnPropertyChanged("ConditionFlash_opacity");
            }
        }

        public object Clone()
        {
            return new ConditionPrmModel
            {
                ConditionName = ConditionName,
                ConditionDuration = ConditionDuration,
                ConditionATK = ConditionATK,
                ConditionDEF = ConditionDEF,
                ConditionSPD = ConditionSPD,
                ConditionSPT_ATK = ConditionSPT_ATK,
                ConditionHP_Recover = ConditionHP_Recover,
                ConditionPoison = ConditionPoison,
                ConditionChakra_recover = ConditionChakra_recover,
                ConditionChakra_unk = ConditionChakra_unk,
                ConditionChakra_shave = ConditionChakra_shave,
                ConditionChakra_revival = ConditionChakra_revival,
                ConditionChakra_Drain = ConditionChakra_Drain,
                ConditionChakra = ConditionChakra,
                ConditionChakra_Usage = ConditionChakra_Usage,
                ConditionSupport = ConditionSupport,
                ConditionTeam = ConditionTeam,
                ConditionGuardBreak = ConditionGuardBreak,
                ConditionDodge = ConditionDodge,
                ConditionProjectile = ConditionProjectile,
                ConditionAutoDodge = ConditionAutoDodge,
                ConditionSeal = ConditionSeal,
                ConditionSleep = ConditionSleep,
                ConditionStun = ConditionStun,
                ConditionFlashType = ConditionFlashType,
                ConditionFlashColor = ConditionFlashColor,
                ConditionFlash_unk1 = ConditionFlash_unk1,
                ConditionFlash_interval = ConditionFlash_interval,
                ConditionFlash_unk2 = ConditionFlash_unk2,
                ConditionFlash_opacity = ConditionFlash_opacity
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
