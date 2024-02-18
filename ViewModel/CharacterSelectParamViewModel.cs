using DynamicData;
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
using System.Windows.Media.TextFormatting;

namespace NSC_ModManager.ViewModel {
    public class CharacterSelectParamViewModel : INotifyPropertyChanged {
        private string _searchTextBox_field;
        public string SearchTextBox_field {
            get { return _searchTextBox_field; }
            set {
                _searchTextBox_field = value;
                OnPropertyChanged("SearchTextBox_field");
            }
        }

        private string _CSP_code_field;
        public string CSP_code_field {
            get { return _CSP_code_field; }
            set {
                _CSP_code_field = value;
                OnPropertyChanged("CSP_code_field");
            }
        }
        private int _pageIndex_field;
        public int PageIndex_field {
            get { return _pageIndex_field; }
            set {
                _pageIndex_field = value;
                OnPropertyChanged("PageIndex_field");
            }
        }
        private int _slotIndex_field;
        public int SlotIndex_field {
            get { return _slotIndex_field; }
            set {
                _slotIndex_field = value;
                OnPropertyChanged("SlotIndex_field");
            }
        }
        private int _costumeIndex_field;
        public int CostumeIndex_field {
            get { return _costumeIndex_field; }
            set {
                _costumeIndex_field = value;
                OnPropertyChanged("CostumeIndex_field");
            }
        }

        private string _costumeDescription_field;
        public string CostumeDescription_field {
            get { return _costumeDescription_field; }
            set {
                _costumeDescription_field = value;
                OnPropertyChanged("CostumeDescription_field");
            }
        }
        private int _unk_field;
        public int Unk_field {
            get { return _unk_field; }
            set {
                _unk_field = value;
                OnPropertyChanged("Unk_field");
            }
        }
        private string _costumeName_field;
        public string CostumeName_field {
            get { return _costumeName_field; }
            set {
                _costumeName_field = value;
                OnPropertyChanged("CostumeName_field");
            }
        }

        private string _accessory_field;
        public string Accessory_field {
            get { return _accessory_field; }
            set {
                _accessory_field = value;
                OnPropertyChanged("Accessory_field");
            }
        }
        private string _charselAnimation_field;
        public string CharselAnimation_field {
            get { return _charselAnimation_field; }
            set {
                _charselAnimation_field = value;
                OnPropertyChanged("CharselAnimation_field");
            }
        }
        private string _dictionaryCode_field;
        public string DictionaryCode_field {
            get { return _dictionaryCode_field; }
            set {
                _dictionaryCode_field = value;
                OnPropertyChanged("DictionaryCode_field");
            }
        }
        private int _dictionaryIndex;
        public int DictionaryIndex_field {
            get { return _dictionaryIndex; }
            set {
                _dictionaryIndex = value;
                OnPropertyChanged("DictionaryIndex_field");
            }
        }
        private float _p1_not_sel_pos_x_field;
        public float P1_not_sel_pos_x_field {
            get { return _p1_not_sel_pos_x_field; }
            set {
                _p1_not_sel_pos_x_field = value;
                OnPropertyChanged("P1_not_sel_pos_x_field");
            }
        }

        private float _p1_not_sel_pos_y_field;
        public float P1_not_sel_pos_y_field {
            get { return _p1_not_sel_pos_y_field; }
            set {
                _p1_not_sel_pos_y_field = value;
                OnPropertyChanged("P1_not_sel_pos_y_field");
            }
        }
        private float _p1_not_sel_pos_z_field;
        public float P1_not_sel_pos_z_field {
            get { return _p1_not_sel_pos_z_field; }
            set {
                _p1_not_sel_pos_z_field = value;
                OnPropertyChanged("P1_not_sel_pos_z_field");
            }
        }
        private float _p2_not_sel_pos_x_field;
        public float P2_not_sel_pos_x_field {
            get { return _p2_not_sel_pos_x_field; }
            set {
                _p2_not_sel_pos_x_field = value;
                OnPropertyChanged("P2_not_sel_pos_x_field");
            }
        }
        private float _p2_not_sel_pos_y_field;
        public float P2_not_sel_pos_y_field {
            get { return _p2_not_sel_pos_y_field; }
            set {
                _p2_not_sel_pos_y_field = value;
                OnPropertyChanged("P2_not_sel_pos_y_field");
            }
        }
        private float _p2_not_sel_pos_z_field;
        public float P2_not_sel_pos_z_field {
            get { return _p2_not_sel_pos_z_field; }
            set {
                _p2_not_sel_pos_z_field = value;
                OnPropertyChanged("P2_not_sel_pos_z_field");
            }
        }
        private float _p1_sel_pos_x_field;
        public float P1_sel_pos_x_field {
            get { return _p1_sel_pos_x_field; }
            set {
                _p1_sel_pos_x_field = value;
                OnPropertyChanged("P1_sel_pos_x_field");
            }
        }
        private float _p1_sel_pos_y_field;
        public float P1_sel_pos_y_field {
            get { return _p1_sel_pos_y_field; }
            set {
                _p1_sel_pos_y_field = value;
                OnPropertyChanged("P1_sel_pos_y_field");
            }
        }
        private float _p1_sel_pos_z_field;
        public float P1_sel_pos_z_field {
            get { return _p1_sel_pos_z_field; }
            set {
                _p1_sel_pos_z_field = value;
                OnPropertyChanged("P1_sel_pos_z_field");
            }
        }
        private float _p2_sel_pos_x_field;
        public float P2_sel_pos_x_field {
            get { return _p2_sel_pos_x_field; }
            set {
                _p2_sel_pos_x_field = value;
                OnPropertyChanged("P2_sel_pos_x_field");
            }
        }
        private float _p2_sel_pos_y_field;
        public float P2_sel_pos_y_field {
            get { return _p2_sel_pos_y_field; }
            set {
                _p2_sel_pos_y_field = value;
                OnPropertyChanged("P2_sel_pos_y_field");
            }
        }
        private float _p2_sel_pos_z_field;
        public float P2_sel_pos_z_field {
            get { return _p2_sel_pos_z_field; }
            set {
                _p2_sel_pos_z_field = value;
                OnPropertyChanged("P2_sel_pos_z_field");
            }

        }

        private float _p1_vsload_x_field;
        public float P1_vsload_x_field {
            get { return _p1_vsload_x_field; }
            set {
                _p1_vsload_x_field = value;
                OnPropertyChanged("P1_vsload_x_field");
            }
        }
        private float _p1_vsload_y_field;
        public float P1_vsload_y_field {
            get { return _p1_vsload_y_field; }
            set {
                _p1_vsload_y_field = value;
                OnPropertyChanged("P1_vsload_y_field");
            }
        }
        private float _p1_vsload_z_field;
        public float P1_vsload_z_field {
            get { return _p1_vsload_z_field; }
            set {
                _p1_vsload_z_field = value;
                OnPropertyChanged("P1_vsload_z_field");
            }
        }
        private float _p2_vsload_x_field;
        public float P2_vsload_x_field {
            get { return _p2_vsload_x_field; }
            set {
                _p2_vsload_x_field = value;
                OnPropertyChanged("P2_vsload_x_field");
            }
        }
        private float _p2_vsload_y_field;
        public float P2_vsload_y_field {
            get { return _p2_vsload_y_field; }
            set {
                _p2_vsload_y_field = value;
                OnPropertyChanged("P2_vsload_y_field");
            }
        }
        private float _p2_vsload_z_field;
        public float P2_vsload_z_field {
            get { return _p2_vsload_z_field; }
            set {
                _p2_vsload_z_field = value;
                OnPropertyChanged("P2_vsload_z_field");
            }
        }

        private float _p1_not_sel_rot_field;
        public float P1_not_sel_rot_field {
            get { return _p1_not_sel_rot_field; }
            set {
                _p1_not_sel_rot_field = value;
                OnPropertyChanged("P1_not_sel_rot_field");
            }
        }
        private float _p2_not_sel_rot_field;
        public float P2_not_sel_rot_field {
            get { return _p2_not_sel_rot_field; }
            set {
                _p2_not_sel_rot_field = value;
                OnPropertyChanged("P2_not_sel_rot_field");
            }
        }
        private float _p1_sel_rot_field;
        public float P1_sel_rot_field {
            get { return _p1_sel_rot_field; }
            set {
                _p1_sel_rot_field = value;
                OnPropertyChanged("P1_sel_rot_field");
            }
        }
        private float _p2_sel_rot_field;
        public float P2_sel_rot_field {
            get { return _p2_sel_rot_field; }
            set {
                _p2_sel_rot_field = value;
                OnPropertyChanged("P2_sel_rot_field");
            }
        }
        private float _p1_vsload_rot_field;
        public float P1_vsload_rot_field {
            get { return _p1_vsload_rot_field; }
            set {
                _p1_vsload_rot_field = value;
                OnPropertyChanged("P1_vsload_rot_field");
            }
        }
        private float _p2_vsload_rot_field;
        public float P2_vsload_rot_field {
            get { return _p2_vsload_rot_field; }
            set {
                _p2_vsload_rot_field = value;
                OnPropertyChanged("P2_vsload_rot_field");
            }
        }

        private float _p1_not_sel_light_x_field;
        public float P1_not_sel_light_x_field {
            get { return _p1_not_sel_light_x_field; }
            set {
                _p1_not_sel_light_x_field = value;
                OnPropertyChanged("P1_not_sel_light_x_field");
            }
        }
        private float _p1_not_sel_light_y_field;
        public float P1_not_sel_light_y_field {
            get { return _p1_not_sel_light_y_field; }
            set {
                _p1_not_sel_light_y_field = value;
                OnPropertyChanged("P1_not_sel_light_y_field");
            }
        }
        private float _p1_not_sel_light_z_field;
        public float P1_not_sel_light_z_field {
            get { return _p1_not_sel_light_z_field; }
            set {
                _p1_not_sel_light_z_field = value;
                OnPropertyChanged("P1_not_sel_light_z_field");
            }
        }
        private float _p2_not_sel_light_x_field;
        public float P2_not_sel_light_x_field {
            get { return _p2_not_sel_light_x_field; }
            set {
                _p2_not_sel_light_x_field = value;
                OnPropertyChanged("P2_not_sel_light_x_field");
            }
        }
        private float _p2_not_sel_light_y_field;
        public float P2_not_sel_light_y_field {
            get { return _p2_not_sel_light_y_field; }
            set {
                _p2_not_sel_light_y_field = value;
                OnPropertyChanged("P2_not_sel_light_y_field");
            }
        }
        private float _p2_not_sel_light_z_field;
        public float P2_not_sel_light_z_field {
            get { return _p2_not_sel_light_z_field; }
            set {
                _p2_not_sel_light_z_field = value;
                OnPropertyChanged("P2_not_sel_light_z_field");
            }
        }

        private float _p1_sel_light_x_field;
        public float P1_sel_light_x_field {
            get { return _p1_sel_light_x_field; }
            set {
                _p1_sel_light_x_field = value;
                OnPropertyChanged("P1_sel_light_x_field");
            }
        }
        private float _p1_sel_light_y_field;
        public float P1_sel_light_y_field {
            get { return _p1_sel_light_y_field; }
            set {
                _p1_sel_light_y_field = value;
                OnPropertyChanged("P1_sel_light_y_field");
            }
        }
        private float _p1_sel_light_z_field;
        public float P1_sel_light_z_field {
            get { return _p1_sel_light_z_field; }
            set {
                _p1_sel_light_z_field = value;
                OnPropertyChanged("P1_sel_light_z_field");
            }
        }
        private float _p2_sel_light_x_field;
        public float P2_sel_light_x_field {
            get { return _p2_sel_light_x_field; }
            set {
                _p2_sel_light_x_field = value;
                OnPropertyChanged("P2_sel_light_x_field");
            }
        }
        private float _p2_sel_light_y_field;
        public float P2_sel_light_y_field {
            get { return _p2_sel_light_y_field; }
            set {
                _p2_sel_light_y_field = value;
                OnPropertyChanged("P2_sel_light_y_field");
            }
        }
        private float _p2_sel_light_z_field;
        public float P2_sel_light_z_field {
            get { return _p2_sel_light_z_field; }
            set {
                _p2_sel_light_z_field = value;
                OnPropertyChanged("P2_sel_light_z_field");
            }
        }

        private float _p1_vsload_light_x_field;
        public float P1_vsload_light_x_field {
            get { return _p1_vsload_light_x_field; }
            set {
                _p1_vsload_light_x_field = value;
                OnPropertyChanged("P1_vsload_light_x_field");
            }
        }
        private float _p1_vsload_light_y_field;
        public float P1_vsload_light_y_field {
            get { return _p1_vsload_light_y_field; }
            set {
                _p1_vsload_light_y_field = value;
                OnPropertyChanged("P1_vsload_light_y_field");
            }
        }
        private float _p1_vsload_light_z_field;
        public float P1_vsload_light_z_field {
            get { return _p1_vsload_light_z_field; }
            set {
                _p1_vsload_light_z_field = value;
                OnPropertyChanged("P1_vsload_light_z_field");
            }
        }


        private float _p2_vsload_light_x_field;
        public float P2_vsload_light_x_field {
            get { return _p2_vsload_light_x_field; }
            set {
                _p2_vsload_light_x_field = value;
                OnPropertyChanged("P2_vsload_light_x_field");
            }
        }
        private float _p2_vsload_light_y_field;
        public float P2_vsload_light_y_field {
            get { return _p2_vsload_light_y_field; }
            set {
                _p2_vsload_light_y_field = value;
                OnPropertyChanged("P2_vsload_light_y_field");
            }
        }
        private float _p2_vsload_light_z_field;
        public float P2_vsload_light_z_field {
            get { return _p2_vsload_light_z_field; }
            set {
                _p2_vsload_light_z_field = value;
                OnPropertyChanged("P2_vsload_light_z_field");
            }
        }

        private float _p1_customization_pos_x_field;
        public float P1_customization_pos_x_field {
            get { return _p1_customization_pos_x_field; }
            set {
                _p1_customization_pos_x_field = value;
                OnPropertyChanged("P1_customization_pos_x_field");
            }
        }
        private float _p1_customization_pos_y_field;
        public float P1_customization_pos_y_field {
            get { return _p1_customization_pos_y_field; }
            set {
                _p1_customization_pos_y_field = value;
                OnPropertyChanged("P1_customization_pos_y_field");
            }
        }
        private float _p1_customization_pos_z_field;
        public float P1_customization_pos_z_field {
            get { return _p1_customization_pos_z_field; }
            set {
                _p1_customization_pos_z_field = value;
                OnPropertyChanged("P1_customization_pos_z_field");
            }
        }
        private float _p1_customization_rot_field;
        public float P1_customization_rot_field {
            get { return _p1_customization_rot_field; }
            set {
                _p1_customization_rot_field = value;
                OnPropertyChanged("P1_customization_rot_field");
            }
        }
        private float _p1_customization_light_x_field;
        public float P1_customization_light_x_field {
            get { return _p1_customization_light_x_field; }
            set {
                _p1_customization_light_x_field = value;
                OnPropertyChanged("P1_customization_light_x_field");
            }
        }
        private float _p1_customization_light_y_field;
        public float P1_customization_light_y_field {
            get { return _p1_customization_light_y_field; }
            set {
                _p1_customization_light_y_field = value;
                OnPropertyChanged("P1_customization_light_y_field");
            }
        }
        private float _p1_customization_light_z_field;
        public float P1_customization_light_z_field {
            get { return _p1_customization_light_z_field; }
            set {
                _p1_customization_light_z_field = value;
                OnPropertyChanged("P1_customization_light_z_field");
            }
        }
        private float _p2_customization_pos_x_field;
        public float P2_customization_pos_x_field {
            get { return _p2_customization_pos_x_field; }
            set {
                _p2_customization_pos_x_field = value;
                OnPropertyChanged("P2_customization_pos_x_field");
            }
        }
        private float _p2_customization_pos_y_field;
        public float P2_customization_pos_y_field {
            get { return _p2_customization_pos_y_field; }
            set {
                _p2_customization_pos_y_field = value;
                OnPropertyChanged("P2_customization_pos_y_field");
            }
        }
        private float _p2_customization_pos_z_field;
        public float P2_customization_pos_z_field {
            get { return _p2_customization_pos_z_field; }
            set {
                _p2_customization_pos_z_field = value;
                OnPropertyChanged("P2_customization_pos_z_field");
            }
        }
        private float _p2_customization_rot_field;
        public float P2_customization_rot_field {
            get { return _p2_customization_rot_field; }
            set {
                _p2_customization_rot_field = value;
                OnPropertyChanged("P2_customization_rot_field");
            }
        }
        private float _p2_customization_light_x_field;
        public float P2_customization_light_x_field {
            get { return _p2_customization_light_x_field; }
            set {
                _p2_customization_light_x_field = value;
                OnPropertyChanged("P2_customization_light_x_field");
            }
        }
        private float _p2_customization_light_y_field;
        public float P2_customization_light_y_field {
            get { return _p2_customization_light_y_field; }
            set {
                _p2_customization_light_y_field = value;
                OnPropertyChanged("P2_customization_light_y_field");
            }
        }
        private float _p2_customization_light_z_field;
        public float P2_customization_light_z_field {
            get { return _p2_customization_light_z_field; }
            set {
                _p2_customization_light_z_field = value;
                OnPropertyChanged("P2_customization_light_z_field");
            }
        }

        public ObservableCollection<CharacterSelectParamModel> CharacterSelectParamList { get; set; }
        private CharacterSelectParamModel _selectedCSP;
        public CharacterSelectParamModel SelectedCSP {
            get { return _selectedCSP; }
            set {
                _selectedCSP = value;
                if (value is not null) {
                    CSP_code_field = value.CSP_code;
                    PageIndex_field = value.PageIndex;
                    SlotIndex_field = value.SlotIndex;
                    CostumeIndex_field = value.CostumeIndex;
                    CostumeDescription_field = value.CostumeDescription;
                    CostumeName_field = value.CostumeName;
                    Accessory_field = value.Accessory;
                    CharselAnimation_field = value.CharselAnimation;
                    DictionaryCode_field = value.DictionaryCode;
                    DictionaryIndex_field = value.DictionaryIndex;
                    Unk_field = value.Unk;
                    P1_not_sel_pos_x_field = value.CharselValues.P1_not_sel_pos_x;
                    P1_not_sel_pos_y_field = value.CharselValues.P1_not_sel_pos_y;
                    P1_not_sel_pos_z_field = value.CharselValues.P1_not_sel_pos_z;
                    P2_not_sel_pos_x_field = value.CharselValues.P2_not_sel_pos_x;
                    P2_not_sel_pos_y_field = value.CharselValues.P2_not_sel_pos_y;
                    P2_not_sel_pos_z_field = value.CharselValues.P2_not_sel_pos_z;
                    P1_sel_pos_x_field = value.CharselValues.P1_sel_pos_x;
                    P1_sel_pos_y_field = value.CharselValues.P1_sel_pos_y;
                    P1_sel_pos_z_field = value.CharselValues.P1_sel_pos_z;
                    P2_sel_pos_x_field = value.CharselValues.P2_sel_pos_x;
                    P2_sel_pos_y_field = value.CharselValues.P2_sel_pos_y;
                    P2_sel_pos_z_field = value.CharselValues.P2_sel_pos_z;
                    P1_vsload_x_field = value.CharselValues.P1_vsload_x;
                    P1_vsload_y_field = value.CharselValues.P1_vsload_y;
                    P1_vsload_z_field = value.CharselValues.P1_vsload_z;
                    P2_vsload_x_field = value.CharselValues.P2_vsload_x;
                    P2_vsload_y_field = value.CharselValues.P2_vsload_y;
                    P2_vsload_z_field = value.CharselValues.P2_vsload_z;
                    P1_not_sel_rot_field = value.CharselValues.P1_not_sel_rot;
                    P2_not_sel_rot_field = value.CharselValues.P2_not_sel_rot;
                    P1_sel_rot_field = value.CharselValues.P1_sel_rot;
                    P2_sel_rot_field = value.CharselValues.P2_sel_rot;
                    P1_vsload_rot_field = value.CharselValues.P1_vsload_rot;
                    P2_vsload_rot_field = value.CharselValues.P2_vsload_rot;
                    P1_not_sel_light_x_field = value.CharselValues.P1_not_sel_light_x;
                    P1_not_sel_light_y_field = value.CharselValues.P1_not_sel_light_y;
                    P1_not_sel_light_z_field = value.CharselValues.P1_not_sel_light_z;
                    P2_not_sel_light_x_field = value.CharselValues.P2_not_sel_light_x;
                    P2_not_sel_light_y_field = value.CharselValues.P2_not_sel_light_y;
                    P2_not_sel_light_z_field = value.CharselValues.P2_not_sel_light_z;
                    P1_sel_light_x_field = value.CharselValues.P1_sel_light_x;
                    P1_sel_light_y_field = value.CharselValues.P1_sel_light_y;
                    P1_sel_light_z_field = value.CharselValues.P1_sel_light_z;
                    P2_sel_light_x_field = value.CharselValues.P2_sel_light_x;
                    P2_sel_light_y_field = value.CharselValues.P2_sel_light_y;
                    P2_sel_light_z_field = value.CharselValues.P2_sel_light_z;
                    P1_vsload_light_x_field = value.CharselValues.P1_vsload_light_x;
                    P1_vsload_light_y_field = value.CharselValues.P1_vsload_light_y;
                    P1_vsload_light_z_field = value.CharselValues.P1_vsload_light_z;
                    P2_vsload_light_x_field = value.CharselValues.P2_vsload_light_x;
                    P2_vsload_light_y_field = value.CharselValues.P2_vsload_light_y;
                    P2_vsload_light_z_field = value.CharselValues.P2_vsload_light_z;
                    P1_customization_pos_x_field = value.CharselValues.P1_customization_pos_x;
                    P1_customization_pos_y_field = value.CharselValues.P1_customization_pos_y;
                    P1_customization_pos_z_field = value.CharselValues.P1_customization_pos_z;
                    P1_customization_rot_field = value.CharselValues.P1_customization_rot;
                    P1_customization_light_x_field = value.CharselValues.P1_customization_light_x;
                    P1_customization_light_y_field = value.CharselValues.P1_customization_light_y;
                    P1_customization_light_z_field = value.CharselValues.P1_customization_light_z;
                    P2_customization_pos_x_field = value.CharselValues.P2_customization_pos_x;
                    P2_customization_pos_y_field = value.CharselValues.P2_customization_pos_y;
                    P2_customization_pos_z_field = value.CharselValues.P2_customization_pos_z;
                    P2_customization_rot_field = value.CharselValues.P2_customization_rot;
                    P2_customization_light_x_field = value.CharselValues.P2_customization_light_x;
                    P2_customization_light_y_field = value.CharselValues.P2_customization_light_y;
                    P2_customization_light_z_field = value.CharselValues.P2_customization_light_z;
                }

                OnPropertyChanged("SelectedCSP");
            }
        }
        private int _selectedCSPIndex;
        public int SelectedCSPIndex {
            get { return _selectedCSPIndex; }
            set {
                _selectedCSPIndex = value;
                OnPropertyChanged("SelectedCSPIndex");
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

        public byte[] fileByte;
        public string filePath;
        public CharacterSelectParamViewModel() {
            LoadingStatePlay = Visibility.Hidden;
            CharacterSelectParamList = new ObservableCollection<CharacterSelectParamModel>();
            filePath = "";
        }

        public void Clear() {
            SelectedCSPIndex = -1;
            CharacterSelectParamList.Clear();
        }
        public int FreeSlotOnPage(int page) {
            if (CharacterSelectParamList.Count > 0) {
                List<int> Slots = new List<int>();

                for (int i = 0; i < CharacterSelectParamList.Count; i++) {
                    if (CharacterSelectParamList[i].PageIndex == page) {
                        Slots.Add(CharacterSelectParamList[i].SlotIndex);
                    }
                }
                for (int i = 1; i <= Slots.Count + 1; i++) {
                    if (!Slots.Contains(i)) {
                        return i;
                    }
                }
            }
            return 1;
        }
        public void OpenFile(string basepath = "", bool saveIn = true) {
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
                byte[] FileBytes = File.ReadAllBytes(filePath);
                int Index3 = 128;
                string BinName = "";
                string BinPath = BinaryReader.b_ReadString(FileBytes, Index3);
                Index3 = Index3 + BinPath.Length + 2;
                for (int x = 0; x < 3; x++) {
                    string name = BinaryReader.b_ReadString(FileBytes, Index3);
                    if (x == 0)
                        BinName = name;
                    Index3 = Index3 + name.Length + 1;
                }
                int StartOfFile = 0x44 + BinaryReader.b_ReadIntRev(FileBytes, 16);


                if (BinName.Contains("characterSelectParam")) {
                    int entryCount = BinaryReader.b_ReadInt(FileBytes, StartOfFile + 4);
                    for (int c = 0; c < entryCount; c++) {
                        int ptr = StartOfFile + 0x10 + (c * 0x140);
                        CharacterSelectParamModel CSP_entry = new CharacterSelectParamModel();
                        CSP_entry.CSP_code = BinaryReader.b_ReadString(FileBytes, ptr + BinaryReader.b_ReadInt(FileBytes, ptr));
                        CSP_entry.PageIndex = BinaryReader.b_ReadInt(FileBytes, ptr + 0x08);
                        CSP_entry.SlotIndex = BinaryReader.b_ReadInt(FileBytes, ptr + 0x0C);
                        CSP_entry.CostumeIndex = BinaryReader.b_ReadInt(FileBytes, ptr + 0x10);
                        CSP_entry.CostumeDescription = BinaryReader.b_ReadString(FileBytes, ptr + 0x18 + BinaryReader.b_ReadInt(FileBytes, ptr + 0x18));
                        CSP_entry.Unk = BinaryReader.b_ReadInt(FileBytes, ptr + 0x20);
                        CSP_entry.CostumeName = BinaryReader.b_ReadString(FileBytes, ptr + 0x28 + BinaryReader.b_ReadInt(FileBytes, ptr + 0x28));
                        CSP_entry.Accessory = BinaryReader.b_ReadString(FileBytes, ptr + 0x30 + BinaryReader.b_ReadInt(FileBytes, ptr + 0x30));
                        CSP_entry.CharselAnimation = BinaryReader.b_ReadString(FileBytes, ptr + 0x38 + BinaryReader.b_ReadInt(FileBytes, ptr + 0x38));
                        CSP_entry.CharacterIconPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Resources\\Styles\\UI\\charsel_icons\\" + CSP_entry.CSP_code + ".png";
                        CSP_entry.SaveInFile = saveIn;
                        CharacterRosterPositionModel RosterEntry = new CharacterRosterPositionModel();
                        RosterEntry.P1_not_sel_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x40);
                        RosterEntry.P1_not_sel_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x44);
                        RosterEntry.P1_not_sel_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x48);
                        RosterEntry.P2_not_sel_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x4C);
                        RosterEntry.P2_not_sel_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x50);
                        RosterEntry.P2_not_sel_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x54);
                        RosterEntry.P1_sel_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x58);
                        RosterEntry.P1_sel_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x5C);
                        RosterEntry.P1_sel_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x60);
                        RosterEntry.P2_sel_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x64);
                        RosterEntry.P2_sel_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x68);
                        RosterEntry.P2_sel_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x6C);
                        RosterEntry.P1_vsload_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x70);
                        RosterEntry.P1_vsload_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x74);
                        RosterEntry.P1_vsload_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x78);
                        RosterEntry.P2_vsload_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x7C);
                        RosterEntry.P2_vsload_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x80);
                        RosterEntry.P2_vsload_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x84);
                        RosterEntry.P1_not_sel_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x88);
                        RosterEntry.P2_not_sel_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x8C);
                        RosterEntry.P1_sel_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x90);
                        RosterEntry.P2_sel_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x94);
                        RosterEntry.P1_vsload_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x98);
                        RosterEntry.P2_vsload_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x9C);
                        RosterEntry.P1_not_sel_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xA0);
                        RosterEntry.P1_not_sel_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xA4);
                        RosterEntry.P1_not_sel_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xA8);
                        RosterEntry.P2_not_sel_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xAC);
                        RosterEntry.P2_not_sel_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xB0);
                        RosterEntry.P2_not_sel_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xB4);
                        RosterEntry.P1_sel_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xB8);
                        RosterEntry.P1_sel_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xBC);
                        RosterEntry.P1_sel_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xC0);
                        RosterEntry.P2_sel_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xC4);
                        RosterEntry.P2_sel_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xC8);
                        RosterEntry.P2_sel_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xCC);
                        RosterEntry.P1_vsload_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xD0);
                        RosterEntry.P1_vsload_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xD4);
                        RosterEntry.P1_vsload_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xD8);
                        RosterEntry.P2_vsload_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xDC);
                        RosterEntry.P2_vsload_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xE0);
                        RosterEntry.P2_vsload_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xE4);
                        RosterEntry.P1_customization_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xE8);
                        RosterEntry.P1_customization_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xEC);
                        RosterEntry.P1_customization_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xF0);
                        RosterEntry.P1_customization_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xF4);
                        RosterEntry.P1_customization_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xF8);
                        RosterEntry.P1_customization_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0xFC);
                        RosterEntry.P1_customization_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x100);
                        RosterEntry.P2_customization_pos_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x104);
                        RosterEntry.P2_customization_pos_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x108);
                        RosterEntry.P2_customization_pos_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x10C);
                        RosterEntry.P2_customization_rot = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x110);
                        RosterEntry.P2_customization_light_x = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x114);
                        RosterEntry.P2_customization_light_y = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x118);
                        RosterEntry.P2_customization_light_z = BinaryReader.b_ReadFloat(FileBytes, ptr + 0x11C);

                        CSP_entry.CharselValues = RosterEntry;
                        CSP_entry.DictionaryCode = BinaryReader.b_ReadString(FileBytes, ptr + 0x130 + BinaryReader.b_ReadInt(FileBytes, ptr + 0x130));
                        CSP_entry.DictionaryIndex = BinaryReader.b_ReadInt(FileBytes, ptr + 0x138);
                        CharacterSelectParamList.Add(CSP_entry);
                    }
                } else {
                    ModernWpf.MessageBox.Show("You can't open that file with that tool. ");
                    return;
                }
            }

        }

        

        public int MaxPage() {
            if (CharacterSelectParamList.Count > 0) {

                List<int> Pages = new List<int>();
                for (int i = 0; i < CharacterSelectParamList.Count; i++){
                    if (!Pages.Contains(CharacterSelectParamList[i].PageIndex))
                        Pages.Add(CharacterSelectParamList[i].PageIndex);
                }

                return Pages.Max();
            } else {
                return 0;
            }
        }
        public int LastSlot() {
            if (CharacterSelectParamList.Count > 0) {
                int maxPage = MaxPage();
                List<int> Slots = new List<int>();
                for (int i = 0; i < CharacterSelectParamList.Count; i++) {
                    if (!Slots.Contains(CharacterSelectParamList[i].SlotIndex) && CharacterSelectParamList[i].PageIndex == maxPage)
                        Slots.Add(CharacterSelectParamList[i].PageIndex);
                }
                return Slots.Max();
            } else {
                return 1;
            }
        }
        public void RemoveEntry() {
            if (SelectedCSP is not null) {
                CharacterSelectParamList.Remove(SelectedCSP);
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public void SaveEntry() {
            if (SelectedCSP is not null) {
                SelectedCSP.CSP_code = CSP_code_field;
                SelectedCSP.PageIndex = PageIndex_field;
                SelectedCSP.SlotIndex = SlotIndex_field;
                SelectedCSP.CostumeIndex = CostumeIndex_field;
                SelectedCSP.CostumeDescription = CostumeDescription_field;
                SelectedCSP.Unk = Unk_field;
                SelectedCSP.CostumeName = CostumeName_field;
                SelectedCSP.Accessory = Accessory_field;
                SelectedCSP.CharselAnimation = CharselAnimation_field;
                SelectedCSP.CharselValues.P1_not_sel_pos_x = P1_not_sel_pos_x_field;
                SelectedCSP.CharselValues.P1_not_sel_pos_y = P1_not_sel_pos_y_field;
                SelectedCSP.CharselValues.P1_not_sel_pos_z = P1_not_sel_pos_z_field;
                SelectedCSP.CharselValues.P2_not_sel_pos_x = P2_not_sel_pos_x_field;
                SelectedCSP.CharselValues.P2_not_sel_pos_y = P2_not_sel_pos_y_field;
                SelectedCSP.CharselValues.P2_not_sel_pos_z = P2_not_sel_pos_z_field;
                SelectedCSP.CharselValues.P1_sel_pos_x = P1_sel_pos_x_field;
                SelectedCSP.CharselValues.P1_sel_pos_y = P1_sel_pos_y_field;
                SelectedCSP.CharselValues.P1_sel_pos_z = P1_sel_pos_z_field;
                SelectedCSP.CharselValues.P2_sel_pos_x = P2_sel_pos_x_field;
                SelectedCSP.CharselValues.P2_sel_pos_y = P2_sel_pos_y_field;
                SelectedCSP.CharselValues.P2_sel_pos_z = P2_sel_pos_z_field;
                SelectedCSP.CharselValues.P1_vsload_x = P1_vsload_x_field;
                SelectedCSP.CharselValues.P1_vsload_y = P1_vsload_y_field;
                SelectedCSP.CharselValues.P1_vsload_z = P1_vsload_z_field;
                SelectedCSP.CharselValues.P2_vsload_x = P2_vsload_x_field;
                SelectedCSP.CharselValues.P2_vsload_y = P2_vsload_y_field;
                SelectedCSP.CharselValues.P2_vsload_z = P2_vsload_z_field;
                SelectedCSP.CharselValues.P1_not_sel_rot = P1_not_sel_rot_field;
                SelectedCSP.CharselValues.P2_not_sel_rot = P2_not_sel_rot_field;
                SelectedCSP.CharselValues.P1_sel_rot = P1_sel_rot_field;
                SelectedCSP.CharselValues.P2_sel_rot = P2_sel_rot_field;
                SelectedCSP.CharselValues.P1_vsload_rot = P1_vsload_rot_field;
                SelectedCSP.CharselValues.P2_vsload_rot = P2_vsload_rot_field;
                SelectedCSP.CharselValues.P1_not_sel_light_x = P1_not_sel_light_x_field;
                SelectedCSP.CharselValues.P1_not_sel_light_y = P1_not_sel_light_y_field;
                SelectedCSP.CharselValues.P1_not_sel_light_z = P1_not_sel_light_z_field;
                SelectedCSP.CharselValues.P2_not_sel_light_x = P2_not_sel_light_x_field;
                SelectedCSP.CharselValues.P2_not_sel_light_y = P2_not_sel_light_y_field;
                SelectedCSP.CharselValues.P2_not_sel_light_z = P2_not_sel_light_z_field;
                SelectedCSP.CharselValues.P1_sel_light_x = P1_sel_light_x_field;
                SelectedCSP.CharselValues.P1_sel_light_y = P1_sel_light_y_field;
                SelectedCSP.CharselValues.P1_sel_light_z = P1_sel_light_z_field;
                SelectedCSP.CharselValues.P2_sel_light_x = P2_sel_light_x_field;
                SelectedCSP.CharselValues.P2_sel_light_y = P2_sel_light_y_field;
                SelectedCSP.CharselValues.P2_sel_light_z = P2_sel_light_z_field;
                SelectedCSP.CharselValues.P1_vsload_light_x = P1_vsload_light_x_field;
                SelectedCSP.CharselValues.P1_vsload_light_y = P1_vsload_light_y_field;
                SelectedCSP.CharselValues.P1_vsload_light_z = P1_vsload_light_z_field;
                SelectedCSP.CharselValues.P2_vsload_light_x = P2_vsload_light_x_field;
                SelectedCSP.CharselValues.P2_vsload_light_y = P2_vsload_light_y_field;
                SelectedCSP.CharselValues.P2_vsload_light_z = P2_vsload_light_z_field;
                SelectedCSP.CharselValues.P1_customization_pos_x = P1_customization_pos_x_field;
                SelectedCSP.CharselValues.P1_customization_pos_y = P1_customization_pos_y_field;
                SelectedCSP.CharselValues.P1_customization_pos_z = P1_customization_pos_z_field;
                SelectedCSP.CharselValues.P1_customization_rot = P1_customization_rot_field;
                SelectedCSP.CharselValues.P1_customization_light_x = P1_customization_light_x_field;
                SelectedCSP.CharselValues.P1_customization_light_y = P1_customization_light_y_field;
                SelectedCSP.CharselValues.P1_customization_light_z = P1_customization_light_z_field;
                SelectedCSP.CharselValues.P2_customization_pos_x = P2_customization_pos_x_field;
                SelectedCSP.CharselValues.P2_customization_pos_y = P2_customization_pos_y_field;
                SelectedCSP.CharselValues.P2_customization_pos_z = P2_customization_pos_z_field;
                SelectedCSP.CharselValues.P2_customization_rot = P2_customization_rot_field;
                SelectedCSP.CharselValues.P2_customization_light_x = P2_customization_light_x_field;
                SelectedCSP.CharselValues.P2_customization_light_y = P2_customization_light_y_field;
                SelectedCSP.CharselValues.P2_customization_light_z = P2_customization_light_z_field;
                SelectedCSP.DictionaryCode = DictionaryCode_field;
                SelectedCSP.DictionaryIndex = DictionaryIndex_field;
                ModernWpf.MessageBox.Show("Entry was saved!");
            } else {
                ModernWpf.MessageBox.Show("Select entry!");
            }
        }
        public int SearchStringIndex(ObservableCollection<CharacterSelectParamModel> FunctionList, string member_name, int Selected) {
            for (int x = 0; x < FunctionList.Count; x++) {

                string mainString = FunctionList[x].CSP_code;
                string subString = member_name;
                int index = mainString.ToLower().IndexOf(subString.ToLower());
                if (index != -1 && Selected < x) {
                    return x;
                }

            }
            return -1;
        }
        public void SearchEntry() {
            if (SearchTextBox_field is not null) {
                if (SearchStringIndex(CharacterSelectParamList, SearchTextBox_field, SelectedCSPIndex) != -1) {
                    SelectedCSPIndex = SearchStringIndex(CharacterSelectParamList, SearchTextBox_field, SelectedCSPIndex);
                    CollectionViewSource.GetDefaultView(CharacterSelectParamList).MoveCurrentTo(SelectedCSP);
                } else {
                    if (SearchStringIndex(CharacterSelectParamList, SearchTextBox_field, 0) != -1) {
                        SelectedCSPIndex = SearchStringIndex(CharacterSelectParamList, SearchTextBox_field, -1);
                        CollectionViewSource.GetDefaultView(CharacterSelectParamList).MoveCurrentTo(SelectedCSP);
                    } else {
                        ModernWpf.MessageBox.Show("There is no entry with that name.", "No result", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } else {
                ModernWpf.MessageBox.Show("Write text in field!");
            }
        }

        public void AddDupEntry() {
            CharacterSelectParamModel CSP_entry = new CharacterSelectParamModel();
            if (SelectedCSP is not null) {
                CSP_entry = (CharacterSelectParamModel)CharacterSelectParamList[SelectedCSPIndex].Clone();
            } else {
                CSP_entry.CSP_code = "1abc00";
                CSP_entry.PageIndex = 0;
                CSP_entry.SlotIndex = 1;
                CSP_entry.CostumeIndex = 0;
                CSP_entry.CostumeDescription = "";
                CSP_entry.Unk = 1;
                CSP_entry.CostumeName = "practice_normal";
                CSP_entry.Accessory = "";
                CSP_entry.CharselAnimation = "";

                CharacterRosterPositionModel RosterEntry = new CharacterRosterPositionModel();
                RosterEntry.P1_not_sel_pos_x = (float)-76.01000213623047;
                RosterEntry.P1_not_sel_pos_y = (float)73.93000030517578;
                RosterEntry.P1_not_sel_pos_z = (float)-323.4700012207031;
                RosterEntry.P2_not_sel_pos_x = (float)82.44999694824219;
                RosterEntry.P2_not_sel_pos_y = (float)73.93000030517578;
                RosterEntry.P2_not_sel_pos_z = (float)-323.4700012207031;
                RosterEntry.P1_sel_pos_x = (float)-76.01000213623047;
                RosterEntry.P1_sel_pos_y = (float)73.93000030517578;
                RosterEntry.P1_sel_pos_z = (float)-323.4700012207031;
                RosterEntry.P2_sel_pos_x = (float)82.44999694824219;
                RosterEntry.P2_sel_pos_y = (float)73.93000030517578;
                RosterEntry.P2_sel_pos_z = (float)-323.4700012207031;
                RosterEntry.P1_vsload_x = (float)-76.01000213623047;
                RosterEntry.P1_vsload_y = (float)91.70999908447266;
                RosterEntry.P1_vsload_z = (float)-319.8500061035156;
                RosterEntry.P2_vsload_x = (float)82.44999694824219;
                RosterEntry.P2_vsload_y = (float)91.70999908447266;
                RosterEntry.P2_vsload_z = (float)-319.8500061035156;
                RosterEntry.P1_not_sel_rot = (float)9.850000381469727;
                RosterEntry.P2_not_sel_rot = (float)349.54998779296875;
                RosterEntry.P1_sel_rot = (float)7.329999923706055;
                RosterEntry.P2_sel_rot = (float)349.0899963378906;
                RosterEntry.P1_vsload_rot = (float)10.760000228881836;
                RosterEntry.P2_vsload_rot = (float)345.6400146484375;
                RosterEntry.P1_not_sel_light_x = (float)1;
                RosterEntry.P1_not_sel_light_y = (float)7.150000095367432;
                RosterEntry.P1_not_sel_light_z = (float)-1.649999976158142;
                RosterEntry.P2_not_sel_light_x = (float)2.5299999713897705;
                RosterEntry.P2_not_sel_light_y = (float)4.880000114440918;
                RosterEntry.P2_not_sel_light_z = (float)-0.7900000214576721;
                RosterEntry.P1_sel_light_x = (float)18.649999618530273;
                RosterEntry.P1_sel_light_y = (float)68.86000061035156;
                RosterEntry.P1_sel_light_z = (float)0.38999998569488525;
                RosterEntry.P2_sel_light_x = (float)3.0899999141693115;
                RosterEntry.P2_sel_light_y = (float)68.86000061035156;
                RosterEntry.P2_sel_light_z = (float)0.38999998569488525;
                RosterEntry.P1_vsload_light_x = (float)18.649999618530273;
                RosterEntry.P1_vsload_light_y = (float)68.86000061035156;
                RosterEntry.P1_vsload_light_z = (float)0.38999998569488525;
                RosterEntry.P2_vsload_light_x = (float)3.0899999141693115;
                RosterEntry.P2_vsload_light_y = (float)4.880000114440918;
                RosterEntry.P2_vsload_light_z = (float)-0.9399999976158142;
                RosterEntry.P1_customization_pos_x = (float)-76.1235122680664;
                RosterEntry.P1_customization_pos_y = (float)73.89142608642578;
                RosterEntry.P1_customization_pos_z = (float)-323.99603271484375;
                RosterEntry.P1_customization_rot = (float)14.025724411010742;
                RosterEntry.P1_customization_light_x = (float)18.649999618530273;
                RosterEntry.P1_customization_light_y = (float)68.86000061035156;
                RosterEntry.P1_customization_light_z = (float)0.38999998569488525;
                RosterEntry.P2_customization_pos_x = (float)76.17376708984375;
                RosterEntry.P2_customization_pos_y = (float)360.3885498046875;
                RosterEntry.P2_customization_pos_z = (float)-285.6630859375;
                RosterEntry.P2_customization_rot = (float)345.3846130371094;
                RosterEntry.P2_customization_light_x = (float)11.158173561096191;
                RosterEntry.P2_customization_light_y = (float)68.86000061035156;
                RosterEntry.P2_customization_light_z = (float)-16.35211753845215;

                CSP_entry.CharselValues = RosterEntry;
                CSP_entry.DictionaryCode = "";
                CSP_entry.DictionaryIndex = -1;

            }
            CharacterSelectParamList.Add(CSP_entry);
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
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "bin_le/x64/characterSelectParam.bin");
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

            int PtrPath = fileBytes36.Length;
            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
            fileBytes36 = BinaryReader.b_AddString(fileBytes36, "characterSelectParam");
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
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0xC7,0x77,0x00,0x01,0xBC,0x64,0x00,0x00,0x00,0x01,0x00,0x79,0xC7,0x77,0x00,0x01,0xBC,0x60
                });

            int size1_index = fileBytes36.Length - 0x10;
            int size2_index = fileBytes36.Length - 0x4;
            int count_index = fileBytes36.Length + 0x4;


            ObservableCollection<CharacterSelectParamModel> tempCol = new ObservableCollection<CharacterSelectParamModel>();
            for (int i = 0; i< CharacterSelectParamList.Count; i++) {
                if (CharacterSelectParamList[i].SaveInFile == false) {
                    tempCol.Add(CharacterSelectParamList[i]);
                    CharacterSelectParamList.RemoveAt(i);
                    i--;
                }

            }



            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[0x10] { 0xE9, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            int startPtr = fileBytes36.Length;

            fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[CharacterSelectParamList.Count * 0x140]);
            int addSize = 0;

            List<int> CSP_Code_pointer = new List<int>();
            List<int> CostumeDescriptionPointer = new List<int>();
            List<int> CostumeNamePointer = new List<int>();
            List<int> AccessoryPointer = new List<int>();
            List<int> CharselFilePointer = new List<int>();
            List<int> DictionaryPointer = new List<int>();

            for (int x = 0; x < CharacterSelectParamList.Count; x++) {
                int ptr = startPtr + (x * 0x140);
                CSP_Code_pointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].CSP_code != "" && CharacterSelectParamList[x].CSP_code is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].CSP_code));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);
                    int newPointer3 = CSP_Code_pointer[x] - startPtr - x * 0x140;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr);
                    addSize += CharacterSelectParamList[x].CSP_code.Length + 1;
                }

                CostumeDescriptionPointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].CostumeDescription != "" && CharacterSelectParamList[x].CostumeDescription is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].CostumeDescription));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = CostumeDescriptionPointer[x] - startPtr - x * 0x140 - 0x18;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x18);
                    addSize += CharacterSelectParamList[x].CostumeDescription.Length + 1;
                }
                CostumeNamePointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].CostumeName != "" && CharacterSelectParamList[x].CostumeName is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].CostumeName));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = CostumeNamePointer[x] - startPtr - x * 0x140 - 0x28;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x28);
                    addSize += CharacterSelectParamList[x].CostumeName.Length + 1;
                }
                AccessoryPointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].Accessory != "" && CharacterSelectParamList[x].Accessory is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].Accessory));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = AccessoryPointer[x] - startPtr - x * 0x140 - 0x30;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x30);
                    addSize += CharacterSelectParamList[x].Accessory.Length + 1;
                }
                CharselFilePointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].CharselAnimation != "" && CharacterSelectParamList[x].CharselAnimation is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].CharselAnimation));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = CharselFilePointer[x] - startPtr - x * 0x140 - 0x38;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x38);
                    addSize += CharacterSelectParamList[x].CharselAnimation.Length + 1;
                }
                DictionaryPointer.Add(fileBytes36.Length);
                if (CharacterSelectParamList[x].DictionaryCode != "" && CharacterSelectParamList[x].DictionaryCode is not null) {
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, Encoding.ASCII.GetBytes(CharacterSelectParamList[x].DictionaryCode));
                    fileBytes36 = BinaryReader.b_AddBytes(fileBytes36, new byte[1]);

                    int newPointer3 = DictionaryPointer[x] - startPtr - x * 0x140 - 0x130;
                    fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(newPointer3), ptr + 0x130);
                    addSize += CharacterSelectParamList[x].DictionaryCode.Length + 1;
                }
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].PageIndex), ptr+0x08);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].SlotIndex), ptr + 0x0C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CostumeIndex), ptr + 0x10);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].Unk), ptr + 0x20);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_pos_x), ptr + 0x40);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_pos_y), ptr + 0x44);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_pos_z), ptr + 0x48);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_pos_x), ptr + 0x4C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_pos_y), ptr + 0x50);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_pos_z), ptr + 0x54);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_pos_x), ptr + 0x58);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_pos_y), ptr + 0x5C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_pos_z), ptr + 0x60);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_pos_x), ptr + 0x64);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_pos_y), ptr + 0x68);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_pos_z), ptr + 0x6C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_x), ptr + 0x70);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_y), ptr + 0x74);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_z), ptr + 0x78);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_x), ptr + 0x7C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_y), ptr + 0x80);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_z), ptr + 0x84);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_rot), ptr + 0x88);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_rot), ptr + 0x8C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_rot), ptr + 0x90);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_rot), ptr + 0x94);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_rot), ptr + 0x98);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_rot), ptr + 0x9C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_light_x), ptr + 0xA0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_light_y), ptr + 0xA4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_not_sel_light_z), ptr + 0xA8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_light_x), ptr + 0xAC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_light_y), ptr + 0xB0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_not_sel_light_z), ptr + 0xB4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_light_x), ptr + 0xB8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_light_y), ptr + 0xBC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_sel_light_z), ptr + 0xC0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_light_x), ptr + 0xC4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_light_y), ptr + 0xC8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_sel_light_z), ptr + 0xCC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_light_x), ptr + 0xD0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_light_y), ptr + 0xD4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_vsload_light_z), ptr + 0xD8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_light_x), ptr + 0xDC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_light_y), ptr + 0xE0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_vsload_light_z), ptr + 0xE4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_pos_x), ptr + 0xE8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_pos_y), ptr + 0xEC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_pos_z), ptr + 0xF0);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_rot), ptr + 0xF4);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_light_x), ptr + 0xF8);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_light_y), ptr + 0xFC);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P1_customization_light_z), ptr + 0x100);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_pos_x), ptr + 0x104);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_pos_y), ptr + 0x108);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_pos_z), ptr + 0x10C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_rot), ptr + 0x110);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_light_x), ptr + 0x114);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_light_y), ptr + 0x118);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].CharselValues.P2_customization_light_z), ptr + 0x11C);
                fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList[x].DictionaryIndex), ptr + 0x138);
            }
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CharacterSelectParamList.Count * 0x140) + 0x14 + addSize), size1_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((CharacterSelectParamList.Count * 0x140) + 0x10 + addSize), size2_index, 1);
            fileBytes36 = BinaryReader.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CharacterSelectParamList.Count), count_index);

            CharacterSelectParamList.AddRange(tempCol.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));

            return BinaryReader.b_AddBytes(fileBytes36, new byte[20]
            {
                0,
                0,
                0,
                8,
                0,
                0,
                0,
                2,
                0,
                0x79, 0xB9, 0x77,
                0,
                0,
                0,
                4,
                0,
                0,
                0,
                0
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
