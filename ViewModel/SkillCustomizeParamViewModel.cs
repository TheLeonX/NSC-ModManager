using Microsoft.Win32;
using NSC_ModManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NSC_ModManager.ViewModel
{
    public class SkillCustomizeParamViewModel : INotifyPropertyChanged {
        private int _searchIndex_field;
        public int SearchIndex_field {
            get { return _searchIndex_field; }
            set {
                _searchIndex_field = value;
                OnPropertyChanged("SearchIndex_field");
            }
        }

        private Visibility _loadingStatePlay;
        public Visibility LoadingStatePlay {
            get { return _loadingStatePlay; }
            set {
                _loadingStatePlay = value;
                OnPropertyChanged("LoadingStatePlay");
            }
        }

        private int _characodeID_field;
        public int CharacodeID_field {
            get { return _characodeID_field; }
            set {
                _characodeID_field = value;
                OnPropertyChanged("CharacodeID_field");
            }
        }

        private float _jutsu1Cost_field;
        public float Jutsu1Cost_field {
            get { return _jutsu1Cost_field; }
            set {
                _jutsu1Cost_field = value;
                OnPropertyChanged("Jutsu1Cost_field");
            }
        }
        private float _jutsu1Cancel_field;
        public float Jutsu1Cancel_field {
            get { return _jutsu1Cancel_field; }
            set {
                _jutsu1Cancel_field = value;
                OnPropertyChanged("Jutsu1Cancel_field");
            }
        }
        private string _jutsu1_normal_name_field;
        public string Jutsu1_normal_name_field {
            get { return _jutsu1_normal_name_field; }
            set {
                _jutsu1_normal_name_field = value;
                OnPropertyChanged("Jutsu1_normal_name_field");
            }
        }
        private string _jutsu1_ex_name_field;
        public string Jutsu1_ex_name_field {
            get { return _jutsu1_ex_name_field; }
            set {
                _jutsu1_ex_name_field = value;
                OnPropertyChanged("Jutsu1_ex_name_field");
            }
        }
        private string _jutsu1_air_name_field;
        public string Jutsu1_air_name_field {
            get { return _jutsu1_air_name_field; }
            set {
                _jutsu1_air_name_field = value;
                OnPropertyChanged("Jutsu1_air_name_field");
            }
        }
        private int _jutsu1_normal_prior_field;
        public int Jutsu1_normal_prior_field {
            get { return _jutsu1_normal_prior_field; }
            set {
                _jutsu1_normal_prior_field = value;
                OnPropertyChanged("Jutsu1_normal_prior_field");
            }
        }
        private int _jutsu1_ex_prior_field;
        public int Jutsu1_ex_prior_field {
            get { return _jutsu1_ex_prior_field; }
            set {
                _jutsu1_ex_prior_field = value;
                OnPropertyChanged("Jutsu1_ex_prior_field");
            }
        }
        private int _jutsu1_air_prior_field;
        public int Jutsu1_air_prior_field {
            get { return _jutsu1_air_prior_field; }
            set {
                _jutsu1_air_prior_field = value;
                OnPropertyChanged("Jutsu1_air_prior_field");
            }
        }
        private float _jutsu2Cost_field;
        public float Jutsu2Cost_field {
            get { return _jutsu2Cost_field; }
            set {
                _jutsu2Cost_field = value;
                OnPropertyChanged("Jutsu2Cost_field");
            }
        }
        private float _jutsu2Cancel_field;
        public float Jutsu2Cancel_field {
            get { return _jutsu2Cancel_field; }
            set {
                _jutsu2Cancel_field = value;
                OnPropertyChanged("Jutsu2Cancel_field");
            }
        }
        private string _jutsu2_normal_name_field;
        public string Jutsu2_normal_name_field {
            get { return _jutsu2_normal_name_field; }
            set {
                _jutsu2_normal_name_field = value;
                OnPropertyChanged("Jutsu2_normal_name_field");
            }
        }
        private string _jutsu2_ex_name_field;
        public string Jutsu2_ex_name_field {
            get { return _jutsu2_ex_name_field; }
            set {
                _jutsu2_ex_name_field = value;
                OnPropertyChanged("Jutsu2_ex_name_field");
            }
        }
        private string _jutsu2_air_name_field;
        public string Jutsu2_air_name_field {
            get { return _jutsu2_air_name_field; }
            set {
                _jutsu2_air_name_field = value;
                OnPropertyChanged("Jutsu2_air_name_field");
            }
        }
        private int _jutsu2_normal_prior_field;
        public int Jutsu2_normal_prior_field {
            get { return _jutsu2_normal_prior_field; }
            set {
                _jutsu2_normal_prior_field = value;
                OnPropertyChanged("Jutsu2_normal_prior_field");
            }
        }
        private int _jutsu2_ex_prior_field;
        public int Jutsu2_ex_prior_field {
            get { return _jutsu2_ex_prior_field; }
            set {
                _jutsu2_ex_prior_field = value;
                OnPropertyChanged("Jutsu2_ex_prior_field");
            }
        }
        private int _jutsu2_air_prior_field;
        public int Jutsu2_air_prior_field {
            get { return _jutsu2_air_prior_field; }
            set {
                _jutsu2_air_prior_field = value;
                OnPropertyChanged("Jutsu2_air_prior_field");
            }
        }
        private float _jutsu3Cost_field;
        public float Jutsu3Cost_field {
            get { return _jutsu3Cost_field; }
            set {
                _jutsu3Cost_field = value;
                OnPropertyChanged("Jutsu3Cost_field");
            }
        }
        private float _jutsu3Cancel_field;
        public float Jutsu3Cancel_field {
            get { return _jutsu3Cancel_field; }
            set {
                _jutsu3Cancel_field = value;
                OnPropertyChanged("Jutsu3Cancel_field");
            }
        }
        private string _jutsu3_normal_name_field;
        public string Jutsu3_normal_name_field {
            get { return _jutsu3_normal_name_field; }
            set {
                _jutsu3_normal_name_field = value;
                OnPropertyChanged("Jutsu3_normal_name_field");
            }
        }
        private string _jutsu3_ex_name_field;
        public string Jutsu3_ex_name_field {
            get { return _jutsu3_ex_name_field; }
            set {
                _jutsu3_ex_name_field = value;
                OnPropertyChanged("Jutsu3_ex_name_field");
            }
        }
        private string _jutsu3_air_name_field;
        public string Jutsu3_air_name_field {
            get { return _jutsu3_air_name_field; }
            set {
                _jutsu3_air_name_field = value;
                OnPropertyChanged("Jutsu3_air_name_field");
            }
        }
        private int _jutsu3_normal_prior_field;
        public int Jutsu3_normal_prior_field {
            get { return _jutsu3_normal_prior_field; }
            set {
                _jutsu3_normal_prior_field = value;
                OnPropertyChanged("Jutsu3_normal_prior_field");
            }
        }
        private int _jutsu3_ex_prior_field;
        public int Jutsu3_ex_prior_field {
            get { return _jutsu3_ex_prior_field; }
            set {
                _jutsu3_ex_prior_field = value;
                OnPropertyChanged("Jutsu3_ex_prior_field");
            }
        }
        private int _jutsu3_air_prior_field;
        public int Jutsu3_air_prior_field {
            get { return _jutsu3_air_prior_field; }
            set {
                _jutsu3_air_prior_field = value;
                OnPropertyChanged("Jutsu3_air_prior_field");
            }
        }
        private float _jutsu4Cost_field;
        public float Jutsu4Cost_field {
            get { return _jutsu4Cost_field; }
            set {
                _jutsu4Cost_field = value;
                OnPropertyChanged("Jutsu4Cost_field");
            }
        }
        private float _jutsu4Cancel_field;
        public float Jutsu4Cancel_field {
            get { return _jutsu4Cancel_field; }
            set {
                _jutsu4Cancel_field = value;
                OnPropertyChanged("Jutsu4Cancel_field");
            }
        }
        private string _jutsu4_normal_name_field;
        public string Jutsu4_normal_name_field {
            get { return _jutsu4_normal_name_field; }
            set {
                _jutsu4_normal_name_field = value;
                OnPropertyChanged("Jutsu4_normal_name_field");
            }
        }
        private string _jutsu4_ex_name_field;
        public string Jutsu4_ex_name_field {
            get { return _jutsu4_ex_name_field; }
            set {
                _jutsu4_ex_name_field = value;
                OnPropertyChanged("Jutsu4_ex_name_field");
            }
        }
        private string _jutsu4_air_name_field;
        public string Jutsu4_air_name_field {
            get { return _jutsu4_air_name_field; }
            set {
                _jutsu4_air_name_field = value;
                OnPropertyChanged("Jutsu4_air_name_field");
            }
        }
        private int _jutsu4_normal_prior_field;
        public int Jutsu4_normal_prior_field {
            get { return _jutsu4_normal_prior_field; }
            set {
                _jutsu4_normal_prior_field = value;
                OnPropertyChanged("Jutsu4_normal_prior_field");
            }
        }
        private int _jutsu4_ex_prior_field;
        public int Jutsu4_ex_prior_field {
            get { return _jutsu4_ex_prior_field; }
            set {
                _jutsu4_ex_prior_field = value;
                OnPropertyChanged("Jutsu4_ex_prior_field");
            }
        }
        private int _jutsu4_air_prior_field;
        public int Jutsu4_air_prior_field {
            get { return _jutsu4_air_prior_field; }
            set {
                _jutsu4_air_prior_field = value;
                OnPropertyChanged("Jutsu4_air_prior_field");
            }
        }
        private float _jutsu5Cost_field;
        public float Jutsu5Cost_field {
            get { return _jutsu5Cost_field; }
            set {
                _jutsu5Cost_field = value;
                OnPropertyChanged("Jutsu5Cost_field");
            }
        }
        private float _jutsu5Cancel_field;
        public float Jutsu5Cancel_field {
            get { return _jutsu5Cancel_field; }
            set {
                _jutsu5Cancel_field = value;
                OnPropertyChanged("Jutsu5Cancel_field");
            }
        }
        private string _jutsu5_normal_name_field;
        public string Jutsu5_normal_name_field {
            get { return _jutsu5_normal_name_field; }
            set {
                _jutsu5_normal_name_field = value;
                OnPropertyChanged("Jutsu5_normal_name_field");
            }
        }
        private string _jutsu5_ex_name_field;
        public string Jutsu5_ex_name_field {
            get { return _jutsu5_ex_name_field; }
            set {
                _jutsu5_ex_name_field = value;
                OnPropertyChanged("Jutsu5_ex_name_field");
            }
        }
        private string _jutsu5_air_name_field;
        public string Jutsu5_air_name_field {
            get { return _jutsu5_air_name_field; }
            set {
                _jutsu5_air_name_field = value;
                OnPropertyChanged("Jutsu5_air_name_field");
            }
        }
        private int _jutsu5_normal_prior_field;
        public int Jutsu5_normal_prior_field {
            get { return _jutsu5_normal_prior_field; }
            set {
                _jutsu5_normal_prior_field = value;
                OnPropertyChanged("Jutsu5_normal_prior_field");
            }
        }
        private int _jutsu5_ex_prior_field;
        public int Jutsu5_ex_prior_field {
            get { return _jutsu5_ex_prior_field; }
            set {
                _jutsu5_ex_prior_field = value;
                OnPropertyChanged("Jutsu5_ex_prior_field");
            }
        }
        private int _jutsu5_air_prior_field;
        public int Jutsu5_air_prior_field {
            get { return _jutsu5_air_prior_field; }
            set {
                _jutsu5_air_prior_field = value;
                OnPropertyChanged("Jutsu5_air_prior_field");
            }
        }
        private float _jutsu6Cost_field;
        public float Jutsu6Cost_field {
            get { return _jutsu6Cost_field; }
            set {
                _jutsu6Cost_field = value;
                OnPropertyChanged("Jutsu6Cost_field");
            }
        }
        private float _jutsu6Cancel_field;
        public float Jutsu6Cancel_field {
            get { return _jutsu6Cancel_field; }
            set {
                _jutsu6Cancel_field = value;
                OnPropertyChanged("Jutsu6Cancel_field");
            }
        }
        private string _jutsu6_normal_name_field;
        public string Jutsu6_normal_name_field {
            get { return _jutsu6_normal_name_field; }
            set {
                _jutsu6_normal_name_field = value;
                OnPropertyChanged("Jutsu6_normal_name_field");
            }
        }
        private string _jutsu6_ex_name_field;
        public string Jutsu6_ex_name_field {
            get { return _jutsu6_ex_name_field; }
            set {
                _jutsu6_ex_name_field = value;
                OnPropertyChanged("Jutsu6_ex_name_field");
            }
        }
        private string _jutsu6_air_name_field;
        public string Jutsu6_air_name_field {
            get { return _jutsu6_air_name_field; }
            set {
                _jutsu6_air_name_field = value;
                OnPropertyChanged("Jutsu6_air_name_field");
            }
        }
        private int _jutsu6_normal_prior_field;
        public int Jutsu6_normal_prior_field {
            get { return _jutsu6_normal_prior_field; }
            set {
                _jutsu6_normal_prior_field = value;
                OnPropertyChanged("Jutsu6_normal_prior_field");
            }
        }
        private int _jutsu6_ex_prior_field;
        public int Jutsu6_ex_prior_field {
            get { return _jutsu6_ex_prior_field; }
            set {
                _jutsu6_ex_prior_field = value;
                OnPropertyChanged("Jutsu6_ex_prior_field");
            }
        }
        private int _jutsu6_air_prior_field;
        public int Jutsu6_air_prior_field {
            get { return _jutsu6_air_prior_field; }
            set {
                _jutsu6_air_prior_field = value;
                OnPropertyChanged("Jutsu6_air_prior_field");
            }
        }
        private float _jutsu1_awaCost_field;
        public float Jutsu1_awaCost_field {
            get { return _jutsu1_awaCost_field; }
            set {
                _jutsu1_awaCost_field = value;
                OnPropertyChanged("Jutsu1_awaCost_field");
            }
        }
        private float _jutsu1_awaCancel_field;
        public float Jutsu1_awaCancel_field {
            get { return _jutsu1_awaCancel_field; }
            set {
                _jutsu1_awaCancel_field = value;
                OnPropertyChanged("Jutsu1_awaCancel_field");
            }
        }
        private string _jutsu1_awa_normal_name_field;
        public string Jutsu1_awa_normal_name_field {
            get { return _jutsu1_awa_normal_name_field; }
            set {
                _jutsu1_awa_normal_name_field = value;
                OnPropertyChanged("Jutsu1_awa_normal_name_field");
            }
        }
        private string _jutsu1_awa_ex_name_field;
        public string Jutsu1_awa_ex_name_field {
            get { return _jutsu1_awa_ex_name_field; }
            set {
                _jutsu1_awa_ex_name_field = value;
                OnPropertyChanged("Jutsu1_awa_ex_name_field");
            }
        }
        private string _jutsu1_awa_air_name_field;
        public string Jutsu1_awa_air_name_field {
            get { return _jutsu1_awa_air_name_field; }
            set {
                _jutsu1_awa_air_name_field = value;
                OnPropertyChanged("Jutsu1_awa_air_name_field");
            }
        }
        private int _jutsu1_awa_normal_prior_field;
        public int Jutsu1_awa_normal_prior_field {
            get { return _jutsu1_awa_normal_prior_field; }
            set {
                _jutsu1_awa_normal_prior_field = value;
                OnPropertyChanged("Jutsu1_awa_normal_prior_field");
            }
        }
        private int _jutsu1_awa_ex_prior_field;
        public int Jutsu1_awa_ex_prior_field {
            get { return _jutsu1_awa_ex_prior_field; }
            set {
                _jutsu1_awa_ex_prior_field = value;
                OnPropertyChanged("Jutsu1_awa_ex_prior_field");
            }
        }
        private int _jutsu1_awa_air_prior_field;
        public int Jutsu1_awa_air_prior_field {
            get { return _jutsu1_awa_air_prior_field; }
            set {
                _jutsu1_awa_air_prior_field = value;
                OnPropertyChanged("Jutsu1_awa_air_prior_field");
            }
        }
        private float _jutsu2_awaCost_field;
        public float Jutsu2_awaCost_field {
            get { return _jutsu2_awaCost_field; }
            set {
                _jutsu2_awaCost_field = value;
                OnPropertyChanged("Jutsu2_awaCost_field");
            }
        }
        private float _jutsu2_awaCancel_field;
        public float Jutsu2_awaCancel_field {
            get { return _jutsu2_awaCancel_field; }
            set {
                _jutsu2_awaCancel_field = value;
                OnPropertyChanged("Jutsu2_awaCancel_field");
            }
        }
        private string _jutsu2_awa_normal_name_field;
        public string Jutsu2_awa_normal_name_field {
            get { return _jutsu2_awa_normal_name_field; }
            set {
                _jutsu2_awa_normal_name_field = value;
                OnPropertyChanged("Jutsu2_awa_normal_name_field");
            }
        }
        private string _jutsu2_awa_ex_name_field;
        public string Jutsu2_awa_ex_name_field {
            get { return _jutsu2_awa_ex_name_field; }
            set {
                _jutsu2_awa_ex_name_field = value;
                OnPropertyChanged("Jutsu2_awa_ex_name_field");
            }
        }
        private string _jutsu2_awa_air_name_field;
        public string Jutsu2_awa_air_name_field {
            get { return _jutsu2_awa_air_name_field; }
            set {
                _jutsu2_awa_air_name_field = value;
                OnPropertyChanged("Jutsu2_awa_air_name_field");
            }
        }
        private int _jutsu2_awa_normal_prior_field;
        public int Jutsu2_awa_normal_prior_field {
            get { return _jutsu2_awa_normal_prior_field; }
            set {
                _jutsu2_awa_normal_prior_field = value;
                OnPropertyChanged("Jutsu2_awa_normal_prior_field");
            }
        }
        private int _jutsu2_awa_ex_prior_field;
        public int Jutsu2_awa_ex_prior_field {
            get { return _jutsu2_awa_ex_prior_field; }
            set {
                _jutsu2_awa_ex_prior_field = value;
                OnPropertyChanged("Jutsu2_awa_ex_prior_field");
            }
        }
        private int _jutsu2_awa_air_prior_field;
        public int Jutsu2_awa_air_prior_field {
            get { return _jutsu2_awa_air_prior_field; }
            set {
                _jutsu2_awa_air_prior_field = value;
                OnPropertyChanged("Jutsu2_awa_air_prior_field");
            }
        }

        public ObservableCollection<SkillCustomizeParamModel> SkillCustomizeParamList { get; set; }
        private SkillCustomizeParamModel _selectedSkill;
        public SkillCustomizeParamModel SelectedSkill {
            get { return _selectedSkill; }
            set {
                _selectedSkill = value;
                if (value is not null) {
                    CharacodeID_field = value.CharacodeID;
                    Jutsu1Cost_field = value.Jutsu1Cost;
                    Jutsu1Cancel_field = value.Jutsu1Cancel;
                    Jutsu1_normal_name_field = value.Jutsu1_normal_name;
                    Jutsu1_air_name_field = value.Jutsu1_air_name;
                    Jutsu1_ex_name_field = value.Jutsu1_ex_name;
                    Jutsu1_normal_prior_field = value.Jutsu1_normal_prior;
                    Jutsu1_ex_prior_field = value.Jutsu1_ex_prior;
                    Jutsu1_air_prior_field = value.Jutsu1_air_prior;
                    Jutsu2Cost_field = value.Jutsu2Cost;
                    Jutsu2Cancel_field = value.Jutsu2Cancel;
                    Jutsu2_normal_name_field = value.Jutsu2_normal_name;
                    Jutsu2_air_name_field = value.Jutsu2_air_name;
                    Jutsu2_ex_name_field = value.Jutsu2_ex_name;
                    Jutsu2_normal_prior_field = value.Jutsu2_normal_prior;
                    Jutsu2_ex_prior_field = value.Jutsu2_ex_prior;
                    Jutsu2_air_prior_field = value.Jutsu2_air_prior;
                    Jutsu3Cost_field = value.Jutsu3Cost;
                    Jutsu3Cancel_field = value.Jutsu3Cancel;
                    Jutsu3_normal_name_field = value.Jutsu3_normal_name;
                    Jutsu3_air_name_field = value.Jutsu3_air_name;
                    Jutsu3_ex_name_field = value.Jutsu3_ex_name;
                    Jutsu3_normal_prior_field = value.Jutsu3_normal_prior;
                    Jutsu3_ex_prior_field = value.Jutsu3_ex_prior;
                    Jutsu3_air_prior_field = value.Jutsu3_air_prior;
                    Jutsu4Cost_field = value.Jutsu4Cost;
                    Jutsu4Cancel_field = value.Jutsu4Cancel;
                    Jutsu4_normal_name_field = value.Jutsu4_normal_name;
                    Jutsu4_air_name_field = value.Jutsu4_air_name;
                    Jutsu4_ex_name_field = value.Jutsu4_ex_name;
                    Jutsu4_normal_prior_field = value.Jutsu4_normal_prior;
                    Jutsu4_ex_prior_field = value.Jutsu4_ex_prior;
                    Jutsu4_air_prior_field = value.Jutsu4_air_prior;
                    Jutsu5Cost_field = value.Jutsu5Cost;
                    Jutsu5Cancel_field = value.Jutsu5Cancel;
                    Jutsu5_normal_name_field = value.Jutsu5_normal_name;
                    Jutsu5_air_name_field = value.Jutsu5_air_name;
                    Jutsu5_ex_name_field = value.Jutsu5_ex_name;
                    Jutsu5_normal_prior_field = value.Jutsu5_normal_prior;
                    Jutsu5_ex_prior_field = value.Jutsu5_ex_prior;
                    Jutsu5_air_prior_field = value.Jutsu5_air_prior;
                    Jutsu6Cost_field = value.Jutsu6Cost;
                    Jutsu6Cancel_field = value.Jutsu6Cancel;
                    Jutsu6_normal_name_field = value.Jutsu6_normal_name;
                    Jutsu6_air_name_field = value.Jutsu6_air_name;
                    Jutsu6_ex_name_field = value.Jutsu6_ex_name;
                    Jutsu6_normal_prior_field = value.Jutsu6_normal_prior;
                    Jutsu6_ex_prior_field = value.Jutsu6_ex_prior;
                    Jutsu6_air_prior_field = value.Jutsu6_air_prior;
                    Jutsu1_awaCost_field = value.Jutsu1_awaCost;
                    Jutsu1_awaCancel_field = value.Jutsu1_awaCancel;
                    Jutsu1_awa_normal_name_field = value.Jutsu1_awa_normal_name;
                    Jutsu1_awa_air_name_field = value.Jutsu1_awa_air_name;
                    Jutsu1_awa_ex_name_field = value.Jutsu1_awa_ex_name;
                    Jutsu1_awa_normal_prior_field = value.Jutsu1_awa_normal_prior;
                    Jutsu1_awa_ex_prior_field = value.Jutsu1_awa_ex_prior;
                    Jutsu1_awa_air_prior_field = value.Jutsu1_awa_air_prior;
                    Jutsu2_awaCost_field = value.Jutsu2_awaCost;
                    Jutsu2_awaCancel_field = value.Jutsu2_awaCancel;
                    Jutsu2_awa_normal_name_field = value.Jutsu2_awa_normal_name;
                    Jutsu2_awa_air_name_field = value.Jutsu2_awa_air_name;
                    Jutsu2_awa_ex_name_field = value.Jutsu2_awa_ex_name;
                    Jutsu2_awa_normal_prior_field = value.Jutsu2_awa_normal_prior;
                    Jutsu2_awa_ex_prior_field = value.Jutsu2_awa_ex_prior;
                    Jutsu2_awa_air_prior_field = value.Jutsu2_awa_air_prior;
                }

                OnPropertyChanged("SelectedSkill");
            }
        }
        private int _selectedSkillIndex;
        public int SelectedSkillIndex {
            get { return _selectedSkillIndex; }
            set {
                _selectedSkillIndex = value;
                OnPropertyChanged("SelectedSkillIndex");
            }
        }

        public byte[] fileByte;
        public string filePath;
        public SkillCustomizeParamViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            SkillCustomizeParamList = new ObservableCollection<SkillCustomizeParamModel>();
            filePath = "";
        }

        public void Clear() {
            SkillCustomizeParamList.Clear();
        }

        public void OpenFile(string basepath = "") {
            Clear();
            if (basepath == "") {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "XFBIN Container (*.xfbin)|*.xfbin";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = false;
                if (myDialog.ShowDialog() == true) {
                    filePath = myDialog.FileName;
                } else {
                    return;
                }
            } else {
                filePath = basepath;
            }
            if (File.Exists(filePath)) {
                fileByte = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(fileByte, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++) {
                    string name = BinaryReader.b_ReadString(fileByte, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(fileByte, 16);
                if (BinName.Contains("skillCustomizeParam")) {

                    int entryCount = BinaryReader.b_ReadInt(fileByte, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x1C8);
                        SkillCustomizeParamModel Skill_entry = new SkillCustomizeParamModel();
                        Skill_entry.CharacodeID = BinaryReader.b_ReadInt(fileByte, ptr);
                        Skill_entry.Jutsu1Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x4);
                        Skill_entry.Jutsu1Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x8);
                        Skill_entry.Jutsu2Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0xC);
                        Skill_entry.Jutsu2Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x10);
                        Skill_entry.Jutsu3Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x14);
                        Skill_entry.Jutsu3Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x18);
                        Skill_entry.Jutsu4Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x1C);
                        Skill_entry.Jutsu4Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x20);
                        Skill_entry.Jutsu5Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x24);
                        Skill_entry.Jutsu5Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x28);
                        Skill_entry.Jutsu6Cost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x2C);
                        Skill_entry.Jutsu6Cancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x30);
                        Skill_entry.Jutsu1_awaCost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x34);
                        Skill_entry.Jutsu1_awaCancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x38);
                        Skill_entry.Jutsu2_awaCost = BinaryReader.b_ReadFloat(fileByte, ptr + 0x3C);
                        Skill_entry.Jutsu2_awaCancel = BinaryReader.b_ReadFloat(fileByte, ptr + 0x40);
                        Skill_entry.Jutsu1_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x48 + BinaryReader.b_ReadInt(fileByte, ptr + 0x48));
                        Skill_entry.Jutsu1_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x50);
                        Skill_entry.Jutsu1_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x58 + BinaryReader.b_ReadInt(fileByte, ptr + 0x58));
                        Skill_entry.Jutsu1_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x60);
                        Skill_entry.Jutsu1_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x68 + BinaryReader.b_ReadInt(fileByte, ptr + 0x68));
                        Skill_entry.Jutsu1_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x70);
                        Skill_entry.Jutsu2_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x78 + BinaryReader.b_ReadInt(fileByte, ptr + 0x78));
                        Skill_entry.Jutsu2_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x80);
                        Skill_entry.Jutsu2_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x88 + BinaryReader.b_ReadInt(fileByte, ptr + 0x88));
                        Skill_entry.Jutsu2_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x90);
                        Skill_entry.Jutsu2_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x98 + BinaryReader.b_ReadInt(fileByte, ptr + 0x98));
                        Skill_entry.Jutsu2_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xA0);
                        Skill_entry.Jutsu3_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0xA8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xA8));
                        Skill_entry.Jutsu3_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xB0);
                        Skill_entry.Jutsu3_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0xB8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xB8));
                        Skill_entry.Jutsu3_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xC0);
                        Skill_entry.Jutsu3_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0xC8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xC8));
                        Skill_entry.Jutsu3_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xD0);
                        Skill_entry.Jutsu4_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0xD8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xD8));
                        Skill_entry.Jutsu4_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xE0);
                        Skill_entry.Jutsu4_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0xE8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xE8));
                        Skill_entry.Jutsu4_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0xF0);
                        Skill_entry.Jutsu4_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0xF8 + BinaryReader.b_ReadInt(fileByte, ptr + 0xF8));
                        Skill_entry.Jutsu4_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x100);
                        Skill_entry.Jutsu5_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x108 + BinaryReader.b_ReadInt(fileByte, ptr + 0x108));
                        Skill_entry.Jutsu5_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x110);
                        Skill_entry.Jutsu5_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x118 + BinaryReader.b_ReadInt(fileByte, ptr + 0x118));
                        Skill_entry.Jutsu5_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x120);
                        Skill_entry.Jutsu5_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x128 + BinaryReader.b_ReadInt(fileByte, ptr + 0x128));
                        Skill_entry.Jutsu5_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x130);
                        Skill_entry.Jutsu6_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x138 + BinaryReader.b_ReadInt(fileByte, ptr + 0x138));
                        Skill_entry.Jutsu6_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x140);
                        Skill_entry.Jutsu6_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x148 + BinaryReader.b_ReadInt(fileByte, ptr + 0x148));
                        Skill_entry.Jutsu6_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x150);
                        Skill_entry.Jutsu6_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x158 + BinaryReader.b_ReadInt(fileByte, ptr + 0x158));
                        Skill_entry.Jutsu6_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x160);
                        Skill_entry.Jutsu1_awa_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x168 + BinaryReader.b_ReadInt(fileByte, ptr + 0x168));
                        Skill_entry.Jutsu1_awa_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x170);
                        Skill_entry.Jutsu1_awa_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x178 + BinaryReader.b_ReadInt(fileByte, ptr + 0x178));
                        Skill_entry.Jutsu1_awa_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x180);
                        Skill_entry.Jutsu1_awa_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x188 + BinaryReader.b_ReadInt(fileByte, ptr + 0x188));
                        Skill_entry.Jutsu1_awa_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x190);
                        Skill_entry.Jutsu2_awa_normal_name = BinaryReader.b_ReadString(fileByte, ptr + 0x198 + BinaryReader.b_ReadInt(fileByte, ptr + 0x198));
                        Skill_entry.Jutsu2_awa_normal_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x1A0);
                        Skill_entry.Jutsu2_awa_ex_name = BinaryReader.b_ReadString(fileByte, ptr + 0x1A8 + BinaryReader.b_ReadInt(fileByte, ptr + 0x1A8));
                        Skill_entry.Jutsu2_awa_ex_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x1B0);
                        Skill_entry.Jutsu2_awa_air_name = BinaryReader.b_ReadString(fileByte, ptr + 0x1B8 + BinaryReader.b_ReadInt(fileByte, ptr + 0x1B8));
                        Skill_entry.Jutsu2_awa_air_prior = BinaryReader.b_ReadInt(fileByte, ptr + 0x1C0);
                        SkillCustomizeParamList.Add(Skill_entry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        public void RemoveEntry() {
            if (SelectedSkill is not null) {
                SkillCustomizeParamList.Remove(SelectedSkill);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedSkill is not null) {
                SkillCustomizeParamList[SelectedSkillIndex].CharacodeID = CharacodeID_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1Cost = Jutsu1Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1Cancel = Jutsu1Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_normal_name = Jutsu1_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_air_name = Jutsu1_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_ex_name = Jutsu1_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_normal_prior = Jutsu1_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_ex_prior = Jutsu1_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_air_prior = Jutsu1_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2Cost = Jutsu2Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2Cancel = Jutsu2Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_normal_name = Jutsu2_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_air_name = Jutsu2_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_ex_name = Jutsu2_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_normal_prior = Jutsu2_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_ex_prior = Jutsu2_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_air_prior = Jutsu2_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3Cost = Jutsu3Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3Cancel = Jutsu3Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_normal_name = Jutsu3_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_air_name = Jutsu3_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_ex_name = Jutsu3_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_normal_prior = Jutsu3_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_ex_prior = Jutsu3_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu3_air_prior = Jutsu3_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4Cost = Jutsu4Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4Cancel = Jutsu4Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_normal_name = Jutsu4_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_air_name = Jutsu4_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_ex_name = Jutsu4_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_normal_prior = Jutsu4_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_ex_prior = Jutsu4_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu4_air_prior = Jutsu4_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5Cost = Jutsu5Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5Cancel = Jutsu5Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_normal_name = Jutsu5_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_air_name = Jutsu5_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_ex_name = Jutsu5_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_normal_prior = Jutsu5_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_ex_prior = Jutsu5_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu5_air_prior = Jutsu5_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6Cost = Jutsu6Cost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6Cancel = Jutsu6Cancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_normal_name = Jutsu6_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_air_name = Jutsu6_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_ex_name = Jutsu6_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_normal_prior = Jutsu6_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_ex_prior = Jutsu6_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu6_air_prior = Jutsu6_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awaCost = Jutsu1_awaCost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awaCancel = Jutsu1_awaCancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_normal_name = Jutsu1_awa_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_air_name = Jutsu1_awa_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_ex_name = Jutsu1_awa_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_normal_prior = Jutsu1_awa_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_ex_prior = Jutsu1_awa_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu1_awa_air_prior = Jutsu1_awa_air_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awaCost = Jutsu2_awaCost_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awaCancel = Jutsu2_awaCancel_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_normal_name = Jutsu2_awa_normal_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_air_name = Jutsu2_awa_air_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_ex_name = Jutsu2_awa_ex_name_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_normal_prior = Jutsu2_awa_normal_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_ex_prior = Jutsu2_awa_ex_prior_field;
                SkillCustomizeParamList[SelectedSkillIndex].Jutsu2_awa_air_prior = Jutsu2_awa_air_prior_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchByteIndex(ObservableCollection<SkillCustomizeParamModel> FunctionList, int member_index, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {
                if (FunctionList[x].CharacodeID == member_index) {
                    return x;
                }

            }
            return -1;
        }

        public void SearchEntry() {
            if (SearchIndex_field > 0) {
                if (SearchByteIndex(SkillCustomizeParamList, SearchIndex_field, SelectedSkillIndex) != -1) {
                    SelectedSkillIndex = SearchByteIndex(SkillCustomizeParamList, SearchIndex_field, SelectedSkillIndex);
                    CollectionViewSource.GetDefaultView(SkillCustomizeParamList).MoveCurrentTo(SelectedSkill);
                } else {
                    if (SearchByteIndex(SkillCustomizeParamList, SearchIndex_field, 0) != -1) {
                        SelectedSkillIndex = SearchByteIndex(SkillCustomizeParamList, SearchIndex_field, -1);
                        CollectionViewSource.GetDefaultView(SkillCustomizeParamList).MoveCurrentTo(SelectedSkill);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that Characode ID.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write ID in field!");
            }
        }


        public void AddDupEntry() {
            SkillCustomizeParamModel Skill_entry = new SkillCustomizeParamModel();
            if (SelectedSkill is not null) {
                Skill_entry = (SkillCustomizeParamModel)SkillCustomizeParamList[SelectedSkillIndex].Clone();
            } else {
                Skill_entry.CharacodeID = 0;
                Skill_entry.Jutsu1Cost = 30;
                Skill_entry.Jutsu1Cancel = 20;
                Skill_entry.Jutsu1_normal_name = "skill1_message_id";
                Skill_entry.Jutsu1_air_name = "";
                Skill_entry.Jutsu1_ex_name = "";
                Skill_entry.Jutsu1_normal_prior = 0;
                Skill_entry.Jutsu1_ex_prior = 0;
                Skill_entry.Jutsu1_air_prior = 0;
                Skill_entry.Jutsu2Cost = 30;
                Skill_entry.Jutsu2Cancel = 20;
                Skill_entry.Jutsu2_normal_name = "skill2_message_id";
                Skill_entry.Jutsu2_air_name = "";
                Skill_entry.Jutsu2_ex_name = "";
                Skill_entry.Jutsu2_normal_prior = 0;
                Skill_entry.Jutsu2_ex_prior = 0;
                Skill_entry.Jutsu2_air_prior = 0;
                Skill_entry.Jutsu3Cost = 30;
                Skill_entry.Jutsu3Cancel = 20;
                Skill_entry.Jutsu3_normal_name = "";
                Skill_entry.Jutsu3_air_name = "";
                Skill_entry.Jutsu3_ex_name = "";
                Skill_entry.Jutsu3_normal_prior = 0;
                Skill_entry.Jutsu3_ex_prior = 0;
                Skill_entry.Jutsu3_air_prior = 0;
                Skill_entry.Jutsu4Cost = 30;
                Skill_entry.Jutsu4Cancel = 20;
                Skill_entry.Jutsu4_normal_name = "";
                Skill_entry.Jutsu4_air_name = "";
                Skill_entry.Jutsu4_ex_name = "";
                Skill_entry.Jutsu4_normal_prior = 0;
                Skill_entry.Jutsu4_ex_prior = 0;
                Skill_entry.Jutsu4_air_prior = 0;
                Skill_entry.Jutsu5Cost = 30;
                Skill_entry.Jutsu5Cancel = 20;
                Skill_entry.Jutsu5_normal_name = "";
                Skill_entry.Jutsu5_air_name = "";
                Skill_entry.Jutsu5_ex_name = "";
                Skill_entry.Jutsu5_normal_prior = 0;
                Skill_entry.Jutsu5_ex_prior = 0;
                Skill_entry.Jutsu5_air_prior = 0;
                Skill_entry.Jutsu6Cost = 30;
                Skill_entry.Jutsu6Cancel = 20;
                Skill_entry.Jutsu6_normal_name = "";
                Skill_entry.Jutsu6_air_name = "";
                Skill_entry.Jutsu6_ex_name = "";
                Skill_entry.Jutsu6_normal_prior = 0;
                Skill_entry.Jutsu6_ex_prior = 0;
                Skill_entry.Jutsu6_air_prior = 0;
                Skill_entry.Jutsu1_awaCost = 30;
                Skill_entry.Jutsu1_awaCancel = 20;
                Skill_entry.Jutsu1_awa_normal_name = "";
                Skill_entry.Jutsu1_awa_air_name = "";
                Skill_entry.Jutsu1_awa_ex_name = "";
                Skill_entry.Jutsu1_awa_normal_prior = 0;
                Skill_entry.Jutsu1_awa_ex_prior = 0;
                Skill_entry.Jutsu1_awa_air_prior = 0;
                Skill_entry.Jutsu2_awaCost = 30;
                Skill_entry.Jutsu2_awaCancel =20;
                Skill_entry.Jutsu2_awa_normal_name = "";
                Skill_entry.Jutsu2_awa_air_name = "";
                Skill_entry.Jutsu2_awa_ex_name = "";
                Skill_entry.Jutsu2_awa_normal_prior = 0;
                Skill_entry.Jutsu2_awa_ex_prior = 0;
                Skill_entry.Jutsu2_awa_air_prior = 0;
            }
            SkillCustomizeParamList.Add(Skill_entry);
            ModernWpf.MessageBox.Show("Entry was added!");
        }

        public void SaveFile() {
            if (filePath != "") {

                if (File.Exists(filePath + ".backup")) {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
                File.WriteAllBytes(filePath, ConvertToFile());
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
            } else {
                SaveFileAs();
            }
        }

        public void SaveFileAs(string basepath = "") {
            SaveFileDialog s = new SaveFileDialog();
            {
                s.DefaultExt = ".xfbin";
                s.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "")
                s.FileName = basepath;
            else
                s.ShowDialog();
            if (s.FileName == "") {
                return;
            }
            if (s.FileName == filePath) {
                if (File.Exists(filePath + ".backup")) {
                    File.Delete(filePath + ".backup");
                }
                File.Copy(filePath, filePath + ".backup");
            } else {
                filePath = s.FileName;
            }
            File.WriteAllBytes(filePath, ConvertToFile());
            if (basepath == "")
                ModernWpf.MessageBox.Show("File saved to " + filePath + ".");
        }

        public byte[] ConvertToFile() {
            // Build the header
            int totalLength4 = 0;

            byte[] fileBytes36 = new byte[127] { 0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0xBC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x01, 0x49, 0x00, 0x00, 0x4C, 0xE3, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x6F, 0x00, 0x00, 0x01, 0x4B, 0x00, 0x00, 0x0F, 0x84, 0x00, 0x00, 0x05, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00 };
            int PtrNucc = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/skillCustomizeParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "skillCustomizeParam");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "Page0");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "index");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrName = fileBytes36.Length;
            totalLength4 = PtrName;
            int AddedBytes = 0;

            while (fileBytes36.Length % 4 != 0) {
                AddedBytes++;
                fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            }

            // Build bin1
            totalLength4 = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[48]
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03
            });

            int PtrSection = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[16]
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                0,
                0,
                0,
                2,
                0,
                0,
                0,
                3
            });

            totalLength4 = fileBytes36.Length;

            int PathLength = PtrPath - 127;
            int NameLength = PtrName - PtrPath;
            int Section1Length = PtrSection - PtrName - AddedBytes;
            int FullLength = totalLength4 - 68 + 40;
            int ReplaceIndex8 = 16;
            byte[] buffer8 = BitConverter.GetBytes(FullLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 36;
            buffer8 = BitConverter.GetBytes(2);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 40;
            buffer8 = BitConverter.GetBytes(PathLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 44;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 48;
            buffer8 = BitConverter.GetBytes(NameLength);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 52;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 56;
            buffer8 = BitConverter.GetBytes(Section1Length);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
            ReplaceIndex8 = 60;
            buffer8 = BitConverter.GetBytes(4);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[40]
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x5C,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x5C,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x79,0x5C,0x00,0x00,0x00,0x00,0x00
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length + 0x4;



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE9, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;




            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[SkillCustomizeParamList.Count * 0x1C8]);

            int addSize = 0;

            List<int> jutsu1_normal_name_pointer = new List<int>();
            List<int> jutsu1_ex_name_pointer = new List<int>();
            List<int> jutsu1_air_name_pointer = new List<int>();
            List<int> jutsu2_normal_name_pointer = new List<int>();
            List<int> jutsu2_ex_name_pointer = new List<int>();
            List<int> jutsu2_air_name_pointer = new List<int>();
            List<int> jutsu3_normal_name_pointer = new List<int>();
            List<int> jutsu3_ex_name_pointer = new List<int>();
            List<int> jutsu3_air_name_pointer = new List<int>();
            List<int> jutsu4_normal_name_pointer = new List<int>();
            List<int> jutsu4_ex_name_pointer = new List<int>();
            List<int> jutsu4_air_name_pointer = new List<int>();
            List<int> jutsu5_normal_name_pointer = new List<int>();
            List<int> jutsu5_ex_name_pointer = new List<int>();
            List<int> jutsu5_air_name_pointer = new List<int>();
            List<int> jutsu6_normal_name_pointer = new List<int>();
            List<int> jutsu6_ex_name_pointer = new List<int>();
            List<int> jutsu6_air_name_pointer = new List<int>();
            List<int> jutsu1_awa_normal_name_pointer = new List<int>();
            List<int> jutsu1_awa_ex_name_pointer = new List<int>();
            List<int> jutsu1_awa_air_name_pointer = new List<int>();
            List<int> jutsu2_awa_normal_name_pointer = new List<int>();
            List<int> jutsu2_awa_ex_name_pointer = new List<int>();
            List<int> jutsu2_awa_air_name_pointer = new List<int>();
            for (int x = 0; x < SkillCustomizeParamList.Count; x++) {
                int ptr = startPtr + (x * 0x1C8);
                jutsu1_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_normal_name != "" && SkillCustomizeParamList[x].Jutsu1_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x48;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x48);
                    addSize += SkillCustomizeParamList[x].Jutsu1_normal_name.Length + 1;
                }
                jutsu1_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_ex_name != "" && SkillCustomizeParamList[x].Jutsu1_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x58;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x58);
                    addSize += SkillCustomizeParamList[x].Jutsu1_ex_name.Length + 1;
                }
                jutsu1_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_air_name != "" && SkillCustomizeParamList[x].Jutsu1_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x68;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x68);
                    addSize += SkillCustomizeParamList[x].Jutsu1_air_name.Length + 1;
                }

                jutsu2_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_normal_name != "" && SkillCustomizeParamList[x].Jutsu2_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x78;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x78);
                    addSize += SkillCustomizeParamList[x].Jutsu2_normal_name.Length + 1;
                }
                jutsu2_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_ex_name != "" && SkillCustomizeParamList[x].Jutsu2_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x88;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x88);
                    addSize += SkillCustomizeParamList[x].Jutsu2_ex_name.Length + 1;
                }
                jutsu2_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_air_name != "" && SkillCustomizeParamList[x].Jutsu2_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x98;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x98);
                    addSize += SkillCustomizeParamList[x].Jutsu2_air_name.Length + 1;
                }
                jutsu3_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu3_normal_name != "" && SkillCustomizeParamList[x].Jutsu3_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu3_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu3_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0xA8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xA8);
                    addSize += SkillCustomizeParamList[x].Jutsu3_normal_name.Length + 1;
                }
                jutsu3_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu3_ex_name != "" && SkillCustomizeParamList[x].Jutsu3_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu3_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu3_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0xB8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xB8);
                    addSize += SkillCustomizeParamList[x].Jutsu3_ex_name.Length + 1;
                }
                jutsu3_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu3_air_name != "" && SkillCustomizeParamList[x].Jutsu3_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu3_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu3_air_name_pointer[x] - startPtr - x * 0x1C8 - 0xC8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xC8);
                    addSize += SkillCustomizeParamList[x].Jutsu3_air_name.Length + 1;
                }
                jutsu4_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu4_normal_name != "" && SkillCustomizeParamList[x].Jutsu4_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu4_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu4_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0xD8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xD8);
                    addSize += SkillCustomizeParamList[x].Jutsu4_normal_name.Length + 1;
                }
                jutsu4_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu4_ex_name != "" && SkillCustomizeParamList[x].Jutsu4_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu4_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu4_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0xE8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xE8);
                    addSize += SkillCustomizeParamList[x].Jutsu4_ex_name.Length + 1;
                }
                jutsu4_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu4_air_name != "" && SkillCustomizeParamList[x].Jutsu4_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu4_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu4_air_name_pointer[x] - startPtr - x * 0x1C8 - 0xF8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0xF8);
                    addSize += SkillCustomizeParamList[x].Jutsu4_air_name.Length + 1;
                }
                jutsu5_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu5_normal_name != "" && SkillCustomizeParamList[x].Jutsu5_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu5_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu5_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x108;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x108);
                    addSize += SkillCustomizeParamList[x].Jutsu5_normal_name.Length + 1;
                }
                jutsu5_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu5_ex_name != "" && SkillCustomizeParamList[x].Jutsu5_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu5_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu5_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x118;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x118);
                    addSize += SkillCustomizeParamList[x].Jutsu5_ex_name.Length + 1;
                }
                jutsu5_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu5_air_name != "" && SkillCustomizeParamList[x].Jutsu5_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu5_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu5_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x128;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x128);
                    addSize += SkillCustomizeParamList[x].Jutsu5_air_name.Length + 1;
                }
                jutsu6_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu6_normal_name != "" && SkillCustomizeParamList[x].Jutsu6_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu6_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu6_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x138;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x138);
                    addSize += SkillCustomizeParamList[x].Jutsu6_normal_name.Length + 1;
                }
                jutsu6_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu6_ex_name != "" && SkillCustomizeParamList[x].Jutsu6_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu6_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu6_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x148;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x148);
                    addSize += SkillCustomizeParamList[x].Jutsu6_ex_name.Length + 1;
                }
                jutsu6_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu6_air_name != "" && SkillCustomizeParamList[x].Jutsu6_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu6_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu6_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x158;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x158);
                    addSize += SkillCustomizeParamList[x].Jutsu6_air_name.Length + 1;
                }
                jutsu1_awa_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_awa_normal_name != "" && SkillCustomizeParamList[x].Jutsu1_awa_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_awa_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x168;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x168);
                    addSize += SkillCustomizeParamList[x].Jutsu1_awa_normal_name.Length + 1;
                }
                jutsu1_awa_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_awa_ex_name != "" && SkillCustomizeParamList[x].Jutsu1_awa_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_awa_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x178;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x178);
                    addSize += SkillCustomizeParamList[x].Jutsu1_awa_ex_name.Length + 1;
                }
                jutsu1_awa_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu1_awa_air_name != "" && SkillCustomizeParamList[x].Jutsu1_awa_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu1_awa_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x188;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x188);
                    addSize += SkillCustomizeParamList[x].Jutsu1_awa_air_name.Length + 1;
                }
                jutsu2_awa_normal_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_awa_normal_name != "" && SkillCustomizeParamList[x].Jutsu2_awa_normal_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_normal_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_awa_normal_name_pointer[x] - startPtr - x * 0x1C8 - 0x198;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x198);
                    addSize += SkillCustomizeParamList[x].Jutsu2_awa_normal_name.Length + 1;
                }
                jutsu2_awa_ex_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_awa_ex_name != "" && SkillCustomizeParamList[x].Jutsu2_awa_ex_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_ex_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_awa_ex_name_pointer[x] - startPtr - x * 0x1C8 - 0x1A8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x1A8);
                    addSize += SkillCustomizeParamList[x].Jutsu2_awa_ex_name.Length + 1;
                }
                jutsu2_awa_air_name_pointer.Add(fileBytes36.Length);
                if (SkillCustomizeParamList[x].Jutsu2_awa_air_name != "" && SkillCustomizeParamList[x].Jutsu2_awa_air_name is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_air_name));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = jutsu2_awa_air_name_pointer[x] - startPtr - x * 0x1C8 - 0x1B8;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x1B8);
                    addSize += SkillCustomizeParamList[x].Jutsu2_awa_air_name.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].CharacodeID), ptr);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1Cost), ptr + 0x04);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1Cancel), ptr + 0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2Cost), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2Cancel), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu3Cost), ptr + 0x14);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu3Cancel), ptr + 0x18);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu4Cost), ptr + 0x1C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu4Cancel), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu5Cost), ptr + 0x24);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu5Cancel), ptr + 0x28);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu6Cost), ptr + 0x2C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu6Cancel), ptr + 0x30);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_awaCost), ptr + 0x34);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_awaCancel), ptr + 0x38);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_awaCost), ptr + 0x3C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_awaCancel), ptr + 0x40);

                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_normal_prior), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_ex_prior), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_air_prior), ptr + 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_normal_prior), ptr + 0x80);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_ex_prior), ptr + 0x90);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_air_prior), ptr + 0xA0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu3_normal_prior), ptr + 0xB0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu3_ex_prior), ptr + 0xC0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu3_air_prior), ptr + 0xD0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu4_normal_prior), ptr + 0xE0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu4_ex_prior), ptr + 0xF0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu4_air_prior), ptr + 0x100);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu5_normal_prior), ptr + 0x110);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu5_ex_prior), ptr + 0x120);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu5_air_prior), ptr + 0x130);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu6_normal_prior), ptr + 0x140);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu6_ex_prior), ptr + 0x150);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu6_air_prior), ptr + 0x160);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_normal_prior), ptr + 0x170);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_ex_prior), ptr + 0x180);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu1_awa_air_prior), ptr + 0x190);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_normal_prior), ptr + 0x1A0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_ex_prior), ptr + 0x1B0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList[x].Jutsu2_awa_air_prior), ptr + 0x1C0);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SkillCustomizeParamList.Count * 0x1C8) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((SkillCustomizeParamList.Count * 0x1C8) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SkillCustomizeParamList.Count), count_index);
            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x5D,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            });
        }

        private RelayCommand _saveFileAsCommand;
        public RelayCommand SaveFileAsCommand {
            get {
                return _saveFileAsCommand ??
                  (_saveFileAsCommand = new RelayCommand(obj => {
                      SaveFileAsAsync();
                  }));
            }
        }
        private RelayCommand _saveFileCommand;
        public RelayCommand SaveFileCommand {
            get {
                return _saveFileCommand ??
                  (_saveFileCommand = new RelayCommand(obj => {
                      SaveFileAsync();
                  }));
            }
        }
        private RelayCommand _openFileCommand;
        public RelayCommand OpenFileCommand {
            get {
                return _openFileCommand ??
                  (_openFileCommand = new RelayCommand(obj => {
                      OpenFileAsync();
                  }));
            }
        }
        private RelayCommand _deleteEntryCommand;
        public RelayCommand DeleteEntryCommand {
            get {
                return _deleteEntryCommand ??
                  (_deleteEntryCommand = new RelayCommand(obj => {
                      RemoveEntryAsync();
                  }));
            }
        }

        private RelayCommand _addDupEntryCommand;
        public RelayCommand AddDupEntryCommand {
            get {
                return _addDupEntryCommand ??
                  (_addDupEntryCommand = new RelayCommand(obj => {
                      AddDupEntryAsync();
                  }));
            }
        }
        private RelayCommand _saveEntryCommand;
        public RelayCommand SaveEntryCommand {
            get {
                return _saveEntryCommand ??
                  (_saveEntryCommand = new RelayCommand(obj => {
                      SaveEntryAsync();
                  }));
            }
        }
        private RelayCommand _searchEntryCommand;
        public RelayCommand SearchEntryCommand {
            get {
                return _searchEntryCommand ??
                  (_searchEntryCommand = new RelayCommand(obj => {
                      SearchEntryAsync();
                  }));
            }
        }
        public async void SaveFileAsync() {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFile()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SaveFileAsAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveFileAs(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void OpenFileAsync(string basepath = "") {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => OpenFile(basepath)));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void SearchEntryAsync() {
            LoadingStatePlay = Visibility.Visible;
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SearchEntry()));
            LoadingStatePlay = Visibility.Hidden;

        }
        public async void AddDupEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => AddDupEntry()));

        }
        public async void SaveEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => SaveEntry()));

        }
        public async void RemoveEntryAsync() {
            await Task.Run(() => App.Current.Dispatcher.Invoke(() => RemoveEntry()));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
