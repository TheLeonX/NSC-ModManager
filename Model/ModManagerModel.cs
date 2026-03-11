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
    public class ModManagerModel : INotifyPropertyChanged {
        private string _modName;
        public string ModName {
            get { return _modName; }
            set {
                _modName = value;
                OnPropertyChanged("ModName");
            }
        }
        private string _modFolder;
        public string ModFolder {
            get { return _modFolder; }
            set {
                _modFolder = value;
                OnPropertyChanged("ModFolder");
            }
        }
        private string _iconPath;
        public string IconPath {
            get { return _iconPath; }
            set {
                _iconPath = value;
                OnPropertyChanged("IconPath");
            }
        }
        private string _version;
        public string Version {
            get { return _version; }
            set {
                _version = value;
                OnPropertyChanged("Version");
            }
        }
        private string _description;
        public string Description {
            get { return _description; }
            set {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        private string _author;
        public string Author {
            get { return _author; }
            set {
                _author = value;
                OnPropertyChanged("Author");
            }
        }
        private string _game;
        public string Game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged("Game");
            }
        }
        private string _lastUpdate;
        public string LastUpdate {
            get { return _lastUpdate; }
            set {
                _lastUpdate = value;
                OnPropertyChanged("LastUpdate");
            }
        }
        private bool _enableMod;
        public bool EnableMod {
            get { return _enableMod; }
            set {
                _enableMod = value;
                OnPropertyChanged("EnableMod");
            }
        }
        private string _screenshotsPath;
        public string ScreenshotsPath
        {
            get { return _screenshotsPath; }
            set
            {
                _screenshotsPath = value;
                OnPropertyChanged("ScreenshotsPath");
            }
        }
        public BitmapImage ModIconPreview
        {
            get
            {
                if (string.IsNullOrEmpty(IconPath) || !File.Exists(IconPath))
                    return null;

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(IconPath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                } catch
                {
                    return null;
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
