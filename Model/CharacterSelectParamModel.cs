using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NSC_ModManager.Model {
    public class CharacterSelectParamModel : ICloneable, INotifyPropertyChanged {

        private string _CSP_code;
        public string CSP_code {
            get { return _CSP_code; }
            set {
                _CSP_code = value;
                OnPropertyChanged("CSP_code");
            }
        }
        private int _pageIndex;
        public int PageIndex {
            get { return _pageIndex; }
            set {
                _pageIndex = value;
                OnPropertyChanged("PageIndex");
            }
        }
        private int _slotIndex;
        public int SlotIndex {
            get { return _slotIndex; }
            set {
                _slotIndex = value;
                OnPropertyChanged("SlotIndex");
            }
        }
        private int _costumeIndex;
        public int CostumeIndex {
            get { return _costumeIndex; }
            set {
                _costumeIndex = value;
                OnPropertyChanged("CostumeIndex");
            }
        }

        private string _costumeDescription;
        public string CostumeDescription {
            get { return _costumeDescription; }
            set {
                _costumeDescription = value;
                OnPropertyChanged("CostumeDescription");
            }
        }
        private int _unk;
        public int Unk {
            get { return _unk; }
            set {
                _unk = value;
                OnPropertyChanged("Unk");
            }
        }
        private string _costumeName;
        public string CostumeName {
            get { return _costumeName; }
            set {
                _costumeName = value;
                OnPropertyChanged("CostumeName");
            }
        }

        private string _accessory;
        public string Accessory {
            get { return _accessory; }
            set {
                _accessory = value;
                OnPropertyChanged("Accessory");
            }
        }
        private string _charselAnimation;
        public string CharselAnimation {
            get { return _charselAnimation; }
            set {
                _charselAnimation = value;
                OnPropertyChanged("CharselAnimation");
            }
        }
        private CharacterRosterPositionModel _charselValues;
        public CharacterRosterPositionModel CharselValues {
            get { return _charselValues; }
            set {
                _charselValues = value;
                OnPropertyChanged("CharselValues");
            }
        }
        private string _dictionaryCode;
        public string DictionaryCode {
            get { return _dictionaryCode; }
            set {
                _dictionaryCode = value;
                OnPropertyChanged("DictionaryCode");
            }
        }
        private int _dictionaryIndex;
        public int DictionaryIndex {
            get { return _dictionaryIndex; }
            set {
                _dictionaryIndex = value;
                OnPropertyChanged("DictionaryIndex");
            }
        }

        private string _characterIconPath;
        public string CharacterIconPath {
            get {
                return _characterIconPath;
            }
            set {
                _characterIconPath = value;

                MemoryStream memoryStream = new MemoryStream();


                byte[] fileBytes = new byte[0];

                if (File.Exists(value)) {
                    fileBytes = File.ReadAllBytes(value);
                } else {
                    fileBytes = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Resources\\Styles\\UI\\charsel_icons\\pt_brank_emp.png");
                }
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;
                CharacterIconPreview = BitmapFrame.Create(memoryStream);
                OnPropertyChanged("CharacterIconPath");
            }
        }
        private BitmapSource _characterIconPreview;
        public BitmapSource CharacterIconPreview {
            get { return _characterIconPreview; }
            set {
                _characterIconPreview = value;
                OnPropertyChanged("CharacterIconPreview");
            }
        }
        private bool _saveInFile;
        public bool SaveInFile {
            get { return _saveInFile; }
            set {
                _saveInFile = value;
                OnPropertyChanged("SaveInFile");
            }
        }

        public object Clone() {

            CharacterRosterPositionModel newCharselValues = new CharacterRosterPositionModel();
            newCharselValues = (CharacterRosterPositionModel)CharselValues.Clone();
            return new CharacterSelectParamModel {
                CSP_code = this.CSP_code,
                PageIndex = this.PageIndex,
                SlotIndex = this.SlotIndex,
                CostumeIndex = this.CostumeIndex,
                CostumeDescription = this.CostumeDescription,
                CostumeName = this.CostumeName,
                Accessory = this.Accessory,
                CharselAnimation = this.CharselAnimation,
                CharselValues = newCharselValues,
                DictionaryCode = this.DictionaryCode,
                DictionaryIndex = this.DictionaryIndex,
                Unk = this.Unk,
                CharacterIconPath = this.CharacterIconPath,
                CharacterIconPreview = this.CharacterIconPreview,
                SaveInFile = this.SaveInFile

            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class CharacterRosterPositionModel : ICloneable, INotifyPropertyChanged {
        private float _p1_not_sel_pos_x;
        public float P1_not_sel_pos_x {
            get { return _p1_not_sel_pos_x; }
            set {
                _p1_not_sel_pos_x = value;
                OnPropertyChanged("P1_not_sel_pos_x");
            }
        }

        private float _p1_not_sel_pos_y;
        public float P1_not_sel_pos_y {
            get { return _p1_not_sel_pos_y; }
            set {
                _p1_not_sel_pos_y = value;
                OnPropertyChanged("P1_not_sel_pos_y");
            }
        }
        private float _p1_not_sel_pos_z;
        public float P1_not_sel_pos_z {
            get { return _p1_not_sel_pos_z; }
            set {
                _p1_not_sel_pos_z = value;
                OnPropertyChanged("P1_not_sel_pos_z");
            }
        }
        private float _p2_not_sel_pos_x;
        public float P2_not_sel_pos_x {
            get { return _p2_not_sel_pos_x; }
            set {
                _p2_not_sel_pos_x = value;
                OnPropertyChanged("P2_not_sel_pos_x");
            }
        }
        private float _p2_not_sel_pos_y;
        public float P2_not_sel_pos_y {
            get { return _p2_not_sel_pos_y; }
            set {
                _p2_not_sel_pos_y = value;
                OnPropertyChanged("P2_not_sel_pos_y");
            }
        }
        private float _p2_not_sel_pos_z;
        public float P2_not_sel_pos_z {
            get { return _p2_not_sel_pos_z; }
            set {
                _p2_not_sel_pos_z = value;
                OnPropertyChanged("P2_not_sel_pos_z");
            }
        }
        private float _p1_sel_pos_x;
        public float P1_sel_pos_x {
            get { return _p1_sel_pos_x; }
            set {
                _p1_sel_pos_x = value;
                OnPropertyChanged("P1_sel_pos_x");
            }
        }
        private float _p1_sel_pos_y;
        public float P1_sel_pos_y {
            get { return _p1_sel_pos_y; }
            set {
                _p1_sel_pos_y = value;
                OnPropertyChanged("P1_sel_pos_y");
            }
        }
        private float _p1_sel_pos_z;
        public float P1_sel_pos_z {
            get { return _p1_sel_pos_z; }
            set {
                _p1_sel_pos_z = value;
                OnPropertyChanged("P1_sel_pos_z");
            }
        }
        private float _p2_sel_pos_x;
        public float P2_sel_pos_x {
            get { return _p2_sel_pos_x; }
            set {
                _p2_sel_pos_x = value;
                OnPropertyChanged("P2_sel_pos_x");
            }
        }
        private float _p2_sel_pos_y;
        public float P2_sel_pos_y {
            get { return _p2_sel_pos_y; }
            set {
                _p2_sel_pos_y = value;
                OnPropertyChanged("P2_sel_pos_y");
            }
        }
        private float _p2_sel_pos_z;
        public float P2_sel_pos_z {
            get { return _p2_sel_pos_z; }
            set {
                _p2_sel_pos_z = value;
                OnPropertyChanged("P2_sel_pos_z");
            }

        }

        private float _p1_vsload_x;
        public float P1_vsload_x {
            get { return _p1_vsload_x; }
            set {
                _p1_vsload_x = value;
                OnPropertyChanged("P1_vsload_x");
            }
        }
        private float _p1_vsload_y;
        public float P1_vsload_y {
            get { return _p1_vsload_y; }
            set {
                _p1_vsload_y = value;
                OnPropertyChanged("P1_vsload_y");
            }
        }
        private float _p1_vsload_z;
        public float P1_vsload_z {
            get { return _p1_vsload_z; }
            set {
                _p1_vsload_z = value;
                OnPropertyChanged("P1_vsload_z");
            }
        }
        private float _p2_vsload_x;
        public float P2_vsload_x {
            get { return _p2_vsload_x; }
            set {
                _p2_vsload_x = value;
                OnPropertyChanged("P2_vsload_x");
            }
        }
        private float _p2_vsload_y;
        public float P2_vsload_y {
            get { return _p2_vsload_y; }
            set {
                _p2_vsload_y = value;
                OnPropertyChanged("P2_vsload_y");
            }
        }
        private float _p2_vsload_z;
        public float P2_vsload_z {
            get { return _p2_vsload_z; }
            set {
                _p2_vsload_z = value;
                OnPropertyChanged("P2_vsload_z");
            }
        }

        private float _p1_not_sel_rot;
        public float P1_not_sel_rot {
            get { return _p1_not_sel_rot; }
            set {
                _p1_not_sel_rot = value;
                OnPropertyChanged("P1_not_sel_rot");
            }
        }
        private float _p2_not_sel_rot;
        public float P2_not_sel_rot {
            get { return _p2_not_sel_rot; }
            set {
                _p2_not_sel_rot = value;
                OnPropertyChanged("P2_not_sel_rot");
            }
        }
        private float _p1_sel_rot;
        public float P1_sel_rot {
            get { return _p1_sel_rot; }
            set {
                _p1_sel_rot = value;
                OnPropertyChanged("P1_sel_rot");
            }
        }
        private float _p2_sel_rot;
        public float P2_sel_rot {
            get { return _p2_sel_rot; }
            set {
                _p2_sel_rot = value;
                OnPropertyChanged("P2_sel_rot");
            }
        }
        private float _p1_vsload_rot;
        public float P1_vsload_rot {
            get { return _p1_vsload_rot; }
            set {
                _p1_vsload_rot = value;
                OnPropertyChanged("P1_vsload_rot");
            }
        }
        private float _p2_vsload_rot;
        public float P2_vsload_rot {
            get { return _p2_vsload_rot; }
            set {
                _p2_vsload_rot = value;
                OnPropertyChanged("P2_vsload_rot");
            }
        }

        private float _p1_not_sel_light_x;
        public float P1_not_sel_light_x {
            get { return _p1_not_sel_light_x; }
            set {
                _p1_not_sel_light_x = value;
                OnPropertyChanged("P1_not_sel_light_x");
            }
        }
        private float _p1_not_sel_light_y;
        public float P1_not_sel_light_y {
            get { return _p1_not_sel_light_y; }
            set {
                _p1_not_sel_light_y = value;
                OnPropertyChanged("P1_not_sel_light_y");
            }
        }
        private float _p1_not_sel_light_z;
        public float P1_not_sel_light_z {
            get { return _p1_not_sel_light_z; }
            set {
                _p1_not_sel_light_z = value;
                OnPropertyChanged("P1_not_sel_light_z");
            }
        }
        private float _p2_not_sel_light_x;
        public float P2_not_sel_light_x {
            get { return _p2_not_sel_light_x; }
            set {
                _p2_not_sel_light_x = value;
                OnPropertyChanged("P2_not_sel_light_x");
            }
        }
        private float _p2_not_sel_light_y;
        public float P2_not_sel_light_y {
            get { return _p2_not_sel_light_y; }
            set {
                _p2_not_sel_light_y = value;
                OnPropertyChanged("P2_not_sel_light_y");
            }
        }
        private float _p2_not_sel_light_z;
        public float P2_not_sel_light_z {
            get { return _p2_not_sel_light_z; }
            set {
                _p2_not_sel_light_z = value;
                OnPropertyChanged("P2_not_sel_light_z");
            }
        }

        private float _p1_sel_light_x;
        public float P1_sel_light_x {
            get { return _p1_sel_light_x; }
            set {
                _p1_sel_light_x = value;
                OnPropertyChanged("P1_sel_light_x");
            }
        }
        private float _p1_sel_light_y;
        public float P1_sel_light_y {
            get { return _p1_sel_light_y; }
            set {
                _p1_sel_light_y = value;
                OnPropertyChanged("P1_sel_light_y");
            }
        }
        private float _p1_sel_light_z;
        public float P1_sel_light_z {
            get { return _p1_sel_light_z; }
            set {
                _p1_sel_light_z = value;
                OnPropertyChanged("P1_sel_light_z");
            }
        }
        private float _p2_sel_light_x;
        public float P2_sel_light_x {
            get { return _p2_sel_light_x; }
            set {
                _p2_sel_light_x = value;
                OnPropertyChanged("P2_sel_light_x");
            }
        }
        private float _p2_sel_light_y;
        public float P2_sel_light_y {
            get { return _p2_sel_light_y; }
            set {
                _p2_sel_light_y = value;
                OnPropertyChanged("P2_sel_light_y");
            }
        }
        private float _p2_sel_light_z;
        public float P2_sel_light_z {
            get { return _p2_sel_light_z; }
            set {
                _p2_sel_light_z = value;
                OnPropertyChanged("P2_sel_light_z");
            }
        }

        private float _p1_vsload_light_x;
        public float P1_vsload_light_x {
            get { return _p1_vsload_light_x; }
            set {
                _p1_vsload_light_x = value;
                OnPropertyChanged("P1_vsload_light_x");
            }
        }
        private float _p1_vsload_light_y;
        public float P1_vsload_light_y {
            get { return _p1_vsload_light_y; }
            set {
                _p1_vsload_light_y = value;
                OnPropertyChanged("P1_vsload_light_y");
            }
        }
        private float _p1_vsload_light_z;
        public float P1_vsload_light_z {
            get { return _p1_vsload_light_z; }
            set {
                _p1_vsload_light_z = value;
                OnPropertyChanged("P1_vsload_light_z");
            }
        }


        private float _p2_vsload_light_x;
        public float P2_vsload_light_x {
            get { return _p2_vsload_light_x; }
            set {
                _p2_vsload_light_x = value;
                OnPropertyChanged("P2_vsload_light_x");
            }
        }
        private float _p2_vsload_light_y;
        public float P2_vsload_light_y {
            get { return _p2_vsload_light_y; }
            set {
                _p2_vsload_light_y = value;
                OnPropertyChanged("P2_vsload_light_y");
            }
        }
        private float _p2_vsload_light_z;
        public float P2_vsload_light_z {
            get { return _p2_vsload_light_z; }
            set {
                _p2_vsload_light_z = value;
                OnPropertyChanged("P2_vsload_light_z");
            }
        }

        private float _p1_customization_pos_x;
        public float P1_customization_pos_x {
            get { return _p1_customization_pos_x; }
            set {
                _p1_customization_pos_x = value;
                OnPropertyChanged("P1_customization_pos_x");
            }
        }
        private float _p1_customization_pos_y;
        public float P1_customization_pos_y {
            get { return _p1_customization_pos_y; }
            set {
                _p1_customization_pos_y = value;
                OnPropertyChanged("P1_customization_pos_y");
            }
        }
        private float _p1_customization_pos_z;
        public float P1_customization_pos_z {
            get { return _p1_customization_pos_z; }
            set {
                _p1_customization_pos_z = value;
                OnPropertyChanged("P1_customization_pos_z");
            }
        }
        private float _p1_customization_rot;
        public float P1_customization_rot {
            get { return _p1_customization_rot; }
            set {
                _p1_customization_rot = value;
                OnPropertyChanged("P1_customization_rot");
            }
        }
        private float _p1_customization_light_x;
        public float P1_customization_light_x {
            get { return _p1_customization_light_x; }
            set {
                _p1_customization_light_x = value;
                OnPropertyChanged("P1_customization_light_x");
            }
        }
        private float _p1_customization_light_y;
        public float P1_customization_light_y {
            get { return _p1_customization_light_y; }
            set {
                _p1_customization_light_y = value;
                OnPropertyChanged("P1_customization_light_y");
            }
        }
        private float _p1_customization_light_z;
        public float P1_customization_light_z {
            get { return _p1_customization_light_z; }
            set {
                _p1_customization_light_z = value;
                OnPropertyChanged("P1_customization_light_z");
            }
        }
        private float _p2_customization_pos_x;
        public float P2_customization_pos_x {
            get { return _p2_customization_pos_x; }
            set {
                _p2_customization_pos_x = value;
                OnPropertyChanged("P2_customization_pos_x");
            }
        }
        private float _p2_customization_pos_y;
        public float P2_customization_pos_y {
            get { return _p2_customization_pos_y; }
            set {
                _p2_customization_pos_y = value;
                OnPropertyChanged("P2_customization_pos_y");
            }
        }
        private float _p2_customization_pos_z;
        public float P2_customization_pos_z {
            get { return _p2_customization_pos_z; }
            set {
                _p2_customization_pos_z = value;
                OnPropertyChanged("P2_customization_pos_z");
            }
        }
        private float _p2_customization_rot;
        public float P2_customization_rot {
            get { return _p2_customization_rot; }
            set {
                _p2_customization_rot = value;
                OnPropertyChanged("P2_customization_rot");
            }
        }
        private float _p2_customization_light_x;
        public float P2_customization_light_x {
            get { return _p2_customization_light_x; }
            set {
                _p2_customization_light_x = value;
                OnPropertyChanged("P2_customization_light_x");
            }
        }
        private float _p2_customization_light_y;
        public float P2_customization_light_y {
            get { return _p2_customization_light_y; }
            set {
                _p2_customization_light_y = value;
                OnPropertyChanged("P2_customization_light_y");
            }
        }
        private float _p2_customization_light_z;
        public float P2_customization_light_z {
            get { return _p2_customization_light_z; }
            set {
                _p2_customization_light_z = value;
                OnPropertyChanged("P2_customization_light_z");
            }
        }
        public object Clone() {
            return new CharacterRosterPositionModel {
                P1_not_sel_pos_x = this.P1_not_sel_pos_x,
                P1_not_sel_pos_y = this.P1_not_sel_pos_y,
                P1_not_sel_pos_z = this.P1_not_sel_pos_z,
                P2_not_sel_pos_x = this.P2_not_sel_pos_x,
                P2_not_sel_pos_y = this.P2_not_sel_pos_y,
                P2_not_sel_pos_z = this.P2_not_sel_pos_z,
                P1_sel_pos_x = this.P1_sel_pos_x,
                P1_sel_pos_y = this.P1_sel_pos_y,
                P1_sel_pos_z = this.P1_sel_pos_z,
                P2_sel_pos_x = this.P2_sel_pos_x,
                P2_sel_pos_y = this.P2_sel_pos_y,
                P2_sel_pos_z = this.P2_sel_pos_z,
                P1_vsload_x = this.P1_vsload_x,
                P1_vsload_y = this.P1_vsload_y,
                P1_vsload_z = this.P1_vsload_z,
                P2_vsload_x = this.P2_vsload_x,
                P2_vsload_y = this.P2_vsload_y,
                P2_vsload_z = this.P2_vsload_z,
                P1_not_sel_rot = this.P1_not_sel_rot,
                P2_not_sel_rot = this.P2_not_sel_rot,
                P1_sel_rot = this.P1_sel_rot,
                P2_sel_rot = this.P2_sel_rot,
                P1_vsload_rot = this.P1_vsload_rot,
                P2_vsload_rot = this.P2_vsload_rot,
                P1_not_sel_light_x = this.P1_not_sel_light_x,
                P1_not_sel_light_y = this.P1_not_sel_light_y,
                P1_not_sel_light_z = this.P1_not_sel_light_z,
                P2_not_sel_light_x = this.P2_not_sel_light_x,
                P2_not_sel_light_y = this.P2_not_sel_light_y,
                P2_not_sel_light_z = this.P2_not_sel_light_z,
                P1_sel_light_x = this.P1_sel_light_x,
                P1_sel_light_y = this.P1_sel_light_y,
                P1_sel_light_z = this.P1_sel_light_z,
                P2_sel_light_x = this.P2_sel_light_x,
                P2_sel_light_y = this.P2_sel_light_y,
                P2_sel_light_z = this.P2_sel_light_z,
                P1_vsload_light_x = this.P1_vsload_light_x,
                P1_vsload_light_y = this.P1_vsload_light_y,
                P1_vsload_light_z = this.P1_vsload_light_z,
                P2_vsload_light_x = this.P2_vsload_light_x,
                P2_vsload_light_y = this.P2_vsload_light_y,
                P2_vsload_light_z = this.P2_vsload_light_z,
                P1_customization_pos_x = this.P1_customization_pos_x,
                P1_customization_pos_y = this.P1_customization_pos_y,
                P1_customization_pos_z = this.P1_customization_pos_z,
                P1_customization_rot = this.P1_customization_rot,
                P1_customization_light_x = this.P1_customization_light_x,
                P1_customization_light_y = this.P1_customization_light_y,
                P1_customization_light_z = this.P1_customization_light_z,
                P2_customization_pos_x = this.P2_customization_pos_x,
                P2_customization_pos_y = this.P2_customization_pos_y,
                P2_customization_pos_z = this.P2_customization_pos_z,
                P2_customization_rot = this.P2_customization_rot,
                P2_customization_light_x = this.P2_customization_light_x,
                P2_customization_light_y = this.P2_customization_light_y,
                P2_customization_light_z = this.P2_customization_light_z

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
