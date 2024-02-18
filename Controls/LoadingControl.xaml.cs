using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSC_ModManager.Controls {
    /// <summary>
    /// Логика взаимодействия для LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl {

        public Visibility LoadingState {
            get { return (Visibility)GetValue(LoadingStateProperty); }
            set { SetValue(LoadingStateProperty, value); }
        }
        public static readonly DependencyProperty LoadingStateProperty
            = DependencyProperty.Register(
                  "LoadingState",
                  typeof(Visibility),
                  typeof(LoadingControl),
                  new PropertyMetadata(Visibility.Hidden)
              );
        public LoadingControl() {
            InitializeComponent();
        }
    }
}
