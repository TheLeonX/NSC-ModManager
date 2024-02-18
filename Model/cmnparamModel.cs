using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSC_ModManager.Model
{
    public class pair_spl_sndModel : ICloneable, INotifyPropertyChanged {

        private int _pairSplID;
        public int PairSplID {
            get { return _pairSplID; }
            set {
                _pairSplID = value;
                OnPropertyChanged("PairSplID");
            }
        }
        private string _soundEvFileName;
        public string PairSoundEvFileName {
            get { return _soundEvFileName; }
            set {
                _soundEvFileName = value;
                OnPropertyChanged("PairSoundEvFileName");
            }
        }
        private string _cutInChunkName;
        public string PairCutInChunkName {
            get { return _cutInChunkName; }
            set {
                _cutInChunkName = value;
                OnPropertyChanged("PairCutInChunkName");
            }
        }
        private string _atkChunkName;
        public string PairAtkChunkName {
            get { return _atkChunkName; }
            set {
                _atkChunkName = value;
                OnPropertyChanged("PairAtkChunkName");
            }
        }
        private string _pairSplName1;
        public string PairSplName1 {
            get { return _pairSplName1; }
            set {
                _pairSplName1 = value;
                OnPropertyChanged("PairSplName1");
            }
        }
        private string _pairSplName2;
        public string PairSplName2 {
            get { return _pairSplName2; }
            set {
                _pairSplName2 = value;
                OnPropertyChanged("PairSplName2");
            }
        }
        public object Clone() {
            return new pair_spl_sndModel {
                PairSplID = this.PairSplID,
                PairSoundEvFileName = this.PairSoundEvFileName,
                PairCutInChunkName = this.PairCutInChunkName,
                PairAtkChunkName = this.PairAtkChunkName,
                PairSplName1 = this.PairSplName1,
                PairSplName2 = this.PairSplName2,
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class player_sndModel : ICloneable, INotifyPropertyChanged {

        private string _characode;
        public string PlayerCharacode {
            get { return _characode; }
            set {
                _characode = value;
                OnPropertyChanged("PlayerCharacode");
            }
        }
        private string _sndBaseFileName;
        public string PlayerSndBaseFileName {
            get { return _sndBaseFileName; }
            set {
                _sndBaseFileName = value;
                OnPropertyChanged("PlayerSndBaseFileName");
            }
        }
        private string _sndAwa1FileName;
        public string PlayerSndAwa1FileName {
            get { return _sndAwa1FileName; }
            set {
                _sndAwa1FileName = value;
                OnPropertyChanged("PlayerSndAwa1FileName");
            }
        }
        private string _sndAwa2FileName;
        public string PlayerSndAwa2FileName {
            get { return _sndAwa2FileName; }
            set {
                _sndAwa2FileName = value;
                OnPropertyChanged("PlayerSndAwa2FileName");
            }
        }
        private string _sndEventFileName;
        public string PlayerSndEventFileName {
            get { return _sndEventFileName; }
            set {
                _sndEventFileName = value;
                OnPropertyChanged("PlayerSndEventFileName");
            }
        }
        private string _sndUJEventFileName;
        public string PlayerSndUJEventFileName {
            get { return _sndUJEventFileName; }
            set {
                _sndUJEventFileName = value;
                OnPropertyChanged("PlayerSndUJEventFileName");
            }
        }
        private string _sndUJ_1_CutIn_ChunkName;
        public string PlayerSndUJ_1_CutIn_ChunkName {
            get { return _sndUJ_1_CutIn_ChunkName; }
            set {
                _sndUJ_1_CutIn_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_1_CutIn_ChunkName");
            }
        }
        private string _sndUJ_1_Atk_ChunkName;
        public string PlayerSndUJ_1_Atk_ChunkName {
            get { return _sndUJ_1_Atk_ChunkName; }
            set {
                _sndUJ_1_Atk_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_1_Atk_ChunkName");
            }
        }
        private string _sndUJ_2_CutIn_ChunkName;
        public string PlayerSndUJ_2_CutIn_ChunkName {
            get { return _sndUJ_2_CutIn_ChunkName; }
            set {
                _sndUJ_2_CutIn_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_2_CutIn_ChunkName");
            }
        }
        private string _sndUJ_2_Atk_ChunkName;
        public string PlayerSndUJ_2_Atk_ChunkName {
            get { return _sndUJ_2_Atk_ChunkName; }
            set {
                _sndUJ_2_Atk_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_2_Atk_ChunkName");
            }
        }
        private string _playerSndUJ_3_CutIn_ChunkName;
        public string PlayerSndUJ_3_CutIn_ChunkName {
            get { return _playerSndUJ_3_CutIn_ChunkName; }
            set {
                _playerSndUJ_3_CutIn_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_3_CutIn_ChunkName");
            }
        }
        private string _playerSndUJ_3_Atk_ChunkName;
        public string PlayerSndUJ_3_Atk_ChunkName {
            get { return _playerSndUJ_3_Atk_ChunkName; }
            set {
                _playerSndUJ_3_Atk_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_3_Atk_ChunkName");
            }
        }
        private string _playerSndUJ_alt_CutIn_ChunkName;
        public string PlayerSndUJ_alt_CutIn_ChunkName {
            get { return _playerSndUJ_alt_CutIn_ChunkName; }
            set {
                _playerSndUJ_alt_CutIn_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_alt_CutIn_ChunkName");
            }
        }
        private string _playerSndUJ_alt_Atk_ChunkName;
        public string PlayerSndUJ_alt_Atk_ChunkName {
            get { return _playerSndUJ_alt_Atk_ChunkName; }
            set {
                _playerSndUJ_alt_Atk_ChunkName = value;
                OnPropertyChanged("PlayerSndUJ_alt_Atk_ChunkName");
            }
        }
        private string _playerPartnerCharacodeBase;
        public string PlayerPartnerCharacodeBase {
            get { return _playerPartnerCharacodeBase; }
            set {
                _playerPartnerCharacodeBase = value;
                OnPropertyChanged("PlayerPartnerCharacodeBase");
            }
        }
        private string _playerPartnerCharacodeAwake;
        public string PlayerPartnerCharacodeAwake {
            get { return _playerPartnerCharacodeAwake; }
            set {
                _playerPartnerCharacodeAwake = value;
                OnPropertyChanged("PlayerPartnerCharacodeAwake");
            }
        }
        public object Clone() {
            return new player_sndModel {
                PlayerCharacode = this.PlayerCharacode,
                PlayerSndBaseFileName = this.PlayerSndBaseFileName,
                PlayerSndAwa1FileName = this.PlayerSndAwa1FileName,
                PlayerSndAwa2FileName = this.PlayerSndAwa2FileName,
                PlayerSndEventFileName = this.PlayerSndEventFileName,
                PlayerSndUJEventFileName = this.PlayerSndUJEventFileName,
                PlayerSndUJ_1_CutIn_ChunkName = this.PlayerSndUJ_1_CutIn_ChunkName,
                PlayerSndUJ_1_Atk_ChunkName = this.PlayerSndUJ_1_Atk_ChunkName,
                PlayerSndUJ_2_CutIn_ChunkName = this.PlayerSndUJ_2_CutIn_ChunkName,
                PlayerSndUJ_2_Atk_ChunkName = this.PlayerSndUJ_2_Atk_ChunkName,
                PlayerSndUJ_3_CutIn_ChunkName = this.PlayerSndUJ_3_CutIn_ChunkName,
                PlayerSndUJ_3_Atk_ChunkName = this.PlayerSndUJ_3_Atk_ChunkName,
                PlayerSndUJ_alt_CutIn_ChunkName = this.PlayerSndUJ_alt_CutIn_ChunkName,
                PlayerSndUJ_alt_Atk_ChunkName = this.PlayerSndUJ_alt_Atk_ChunkName,
                PlayerPartnerCharacodeBase = this.PlayerPartnerCharacodeBase,
                PlayerPartnerCharacodeAwake = this.PlayerPartnerCharacodeAwake,
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
