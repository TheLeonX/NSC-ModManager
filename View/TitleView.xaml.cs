using NSC_ModManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace NSC_ModManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            DataContext = new TitleViewModel();
            
        }

        private void RosterEditor_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("This tool will help you to edit character roster with drag and drop.",
                20);
        }
        private void CompileMods_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("Make sure you closed game before starting compiling process. This process will also clear whole game before adding any mod. If you have important files in ModdingAPI folder, it's better to save it somewhere else.",
                20);
        }
        private void ClearGame_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("This process will clear whole game. If you have important files in ModdingAPI folder, it's better to save it somewhere else.",
                20);
        }
        private void InstallMod_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("You can install any mod with .nsc format! Unfortunately, Storm 4 mods aren't support in that mod manager.",
                20);
        }
        private void DeleteMod_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("Didn't like mod?",
                20);
        }
    }
}
