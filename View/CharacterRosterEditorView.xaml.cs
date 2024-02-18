using NSC_ModManager.Model;
using NSC_ModManager.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NSC_ModManager.View {
    /// <summary>
    /// Логика взаимодействия для CharacterRosterEditorView.xaml
    /// </summary>
    public partial class CharacterRosterEditorView : Window {
        public CharacterRosterEditorView() {
            InitializeComponent();
            DataContext = new CharacterRosterEditorViewModel();
        }

        public string sender_name;
        public string reciver_name;

        private void LBoxSort_OnPreviewMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {

                ListBox lb = (ListBox)sender;
                sender_name = lb.Name;
                var pos = e.GetPosition(lb);  // Get location 

                #region source location
                HitTestResult result = VisualTreeHelper.HitTest(lb, pos);    //According to the position to get the result
                if (result == null) {
                    return;        //Cannot find return
                }
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != lb.SelectedItem) {
                    return;
                }
                #endregion

                DataObject dataObj = new DataObject(listBoxItem.Content as CharacterSelectParamModel);
                DragDrop.DoDragDrop(lb, dataObj, DragDropEffects.Move);    //Call method
            }
        }

        private void LBoxSort_OnDrop(object sender, DragEventArgs e) {
            ListBox lb = (ListBox)sender;
            reciver_name = lb.Name;
            CharacterRosterEditorViewModel VM = ((CharacterRosterEditorViewModel)this.DataContext);
            var pos = e.GetPosition(lb);      //Get location 
            var result = VisualTreeHelper.HitTest(lb, pos);      //According to the position to get the result
            if (result == null) {
                return;      //Cannot find return
            }
            #region Finding metadata
            var sourcePerson = e.Data.GetData(typeof(CharacterSelectParamModel)) as CharacterSelectParamModel;
            if (sourcePerson == null) {
                return;
            }
            #endregion
            int sourceIndex = -1;
            int targetIndex = -1;

            if (reciver_name == sender_name) {
                #region Find target data
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null) {
                    return;
                }
                var targetPerson = listBoxItem.Content as CharacterSelectParamModel;
                if (ReferenceEquals(targetPerson, sourcePerson)) {
                    return;
                }
                #endregion
                if (sender_name == "CharacterIconListPreview") {
                    sourceIndex = VM.CharacterList.IndexOf(sourcePerson);
                    targetIndex = VM.CharacterList.IndexOf(targetPerson);
                } else {

                    sourceIndex = VM.CharacterPlaceHolderList.IndexOf(sourcePerson);
                    targetIndex = VM.CharacterPlaceHolderList.IndexOf(targetPerson);
                }
            }
            else {
                if (sender_name == "CharacterIconListPreview") {
                    sourceIndex = VM.CharacterList.IndexOf(sourcePerson);
                } else {

                    sourceIndex = VM.CharacterPlaceHolderList.IndexOf(sourcePerson);
                }
            }
            int sourceSlot = sourceIndex + 1;
            int targetSlot = targetIndex + 1;
            int page = VM.RosterPage_field;
            if (reciver_name == sender_name) {
                if (sender_name == "CharacterIconListPreview")
                    VM.ReplaceSlots(page, sourceSlot, page, targetSlot);
                else
                    VM.ReplaceSlots(-1, sourceSlot, -1, targetSlot);
                } else {
                if (sender_name == "CharacterIconListPreview") {
                    VM.ReplaceSlots(page, sourceSlot, -1);
                } else {
                    VM.ReplaceSlots(-1, sourceSlot, page);
                }
            }
            VM.RosterPage_field = 100;
            VM.RosterPage_field = page;
        }
        internal static class Utils {
            //Find the parent element according to the child element
            public static T FindVisualParent<T>(DependencyObject obj) where T : class {
                while (obj != null) {
                    if (obj is T)
                        return obj as T;

                    obj = VisualTreeHelper.GetParent(obj);
                }
                return null;
            }
        }

    }
}