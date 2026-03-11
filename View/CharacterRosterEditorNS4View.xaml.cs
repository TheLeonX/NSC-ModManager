using NSC_ModManager.Model;
using NSC_ModManager.ViewModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NSC_ModManager.View
{
    /// <summary>
    /// Логика взаимодействия для CharacterRosterEditorNS4View.xaml
    /// </summary>
    public partial class CharacterRosterEditorNS4View : Window
    {
        public CharacterRosterEditorNS4View(TitleViewModel VM)
        {
            InitializeComponent();
            DataContext = new CharacterRosterEditorNS4ViewModel(VM);
        }

        public string sender_name;
        public string reciver_name;

        private void LBoxSort_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBox lb = (ListBox)sender;
                sender_name = lb.Name;
                var pos = e.GetPosition(lb);

                // source location
                HitTestResult result = VisualTreeHelper.HitTest(lb, pos);
                if (result == null)
                {
                    return;
                }
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != lb.SelectedItem)
                {
                    return;
                }

                DataObject dataObj = new DataObject(listBoxItem.Content as CharacterSelectParamModel);
                DragDrop.DoDragDrop(lb, dataObj, DragDropEffects.Move);
            }
        }

        private void LBoxSort_OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        private void LBoxSort_OnDrop(object sender, DragEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            reciver_name = lb.Name;
            var VM = (CharacterRosterEditorNS4ViewModel)this.DataContext;
            var pos = e.GetPosition(lb);
            var result = VisualTreeHelper.HitTest(lb, pos);
            if (result == null) return;

            var sourcePerson = e.Data.GetData(typeof(CharacterSelectParamModel)) as CharacterSelectParamModel;
            if (sourcePerson == null) return;

            int sourceIndex = -1;
            int targetIndex = -1;
            int page = VM.RosterPage_field;

            // Save selection to restore later
            string selectedCSP = null;
            int selectedSlot = -1;
            int selectedPage = page;

            if (VM.SelectedCharacterIndex >= 0 && VM.SelectedCharacterIndex < VM.CharacterList.Count)
            {
                selectedCSP = VM.CharacterList[VM.SelectedCharacterIndex].CSP_code;
                selectedSlot = VM.CharacterList[VM.SelectedCharacterIndex].SlotIndex;
                selectedPage = page;
            } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
            {
                selectedCSP = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].CSP_code;
                selectedSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                selectedPage = -1;
            }

            // Determine source list index (may be -1 if not found)
            if (sender_name == "CharacterIconListPreview")
                sourceIndex = VM.CharacterList.IndexOf(sourcePerson);
            else if (sender_name == "CharacterPlaceHolderIconListPreview")
                sourceIndex = VM.CharacterPlaceHolderList.IndexOf(sourcePerson);
            else if (sender_name == "PlaceholderCostumeIconListPreview")
                sourceIndex = VM.PlaceholderCostumePlaceHolderList.IndexOf(sourcePerson);
            else if (sender_name == "CharacterCostumeIconListPreview")
                sourceIndex = VM.CostumePlaceHolderList.IndexOf(sourcePerson);

            // Helper to check moving into same slot's costume list
            bool IsMovingIntoOwnSlot(int srcPage, int srcSlot, int tgtPage, int tgtSlot)
            {
                return srcPage == tgtPage && srcSlot == tgtSlot && tgtSlot != -1;
            }

            // If dropped inside same list -> reorder or swap
            if (reciver_name == sender_name)
            {
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null) return;
                var targetPerson = listBoxItem.Content as CharacterSelectParamModel;
                if (ReferenceEquals(targetPerson, sourcePerson)) return;

                if (sender_name == "CharacterIconListPreview")
                    targetIndex = VM.CharacterList.IndexOf(targetPerson);
                else if (sender_name == "CharacterPlaceHolderIconListPreview")
                    targetIndex = VM.CharacterPlaceHolderList.IndexOf(targetPerson);
                else if (sender_name == "PlaceholderCostumeIconListPreview")
                    targetIndex = VM.PlaceholderCostumePlaceHolderList.IndexOf(targetPerson);
                else if (sender_name == "CharacterCostumeIconListPreview")
                    targetIndex = VM.CostumePlaceHolderList.IndexOf(targetPerson);

                int sourceSlot = sourceIndex + 1;
                int targetSlot = targetIndex + 1;

                if (sender_name == "CharacterIconListPreview")
                    VM.ReplaceSlots(page, sourceSlot, page, targetSlot);
                else if (sender_name == "CharacterPlaceHolderIconListPreview")
                    VM.ReplaceSlots(-1, sourceSlot, -1, targetSlot);
                else if (sender_name == "PlaceholderCostumeIconListPreview")
                {
                    // Swap costume indices within placeholder costume list
                    int activePage, activeSlot;
                    if (VM.PlaceholderCostumePlaceHolderList != null && VM.PlaceholderCostumePlaceHolderList.Count > 0)
                    {
                        activePage = VM.PlaceholderCostumePlaceHolderList[0].PageIndex;
                        activeSlot = VM.PlaceholderCostumePlaceHolderList[0].SlotIndex;
                    } else
                    {
                        if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            activePage = -1;
                            activeSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                        } else
                        {
                            return;
                        }
                    }

                    SwapCostumeIndices(VM, activePage, activeSlot, sourcePerson.CostumeIndex, targetPerson.CostumeIndex);
                } else if (sender_name == "CharacterCostumeIconListPreview")
                {
                    // Swap costume indices within character costume list
                    int activePage, activeSlot;
                    if (VM.CostumePlaceHolderList != null && VM.CostumePlaceHolderList.Count > 0)
                    {
                        activePage = VM.CostumePlaceHolderList[0].PageIndex;
                        activeSlot = VM.CostumePlaceHolderList[0].SlotIndex;
                    } else
                    {
                        if (VM.SelectedCharacterIndex >= 0 && VM.SelectedCharacterIndex < VM.CharacterList.Count)
                        {
                            activePage = page;
                            activeSlot = VM.CharacterList[VM.SelectedCharacterIndex].SlotIndex;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            activePage = -1;
                            activeSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                        } else
                        {
                            return;
                        }
                    }

                    SwapCostumeIndices(VM, activePage, activeSlot, sourcePerson.CostumeIndex, targetPerson.CostumeIndex);
                }
            } else
            {
                int sourceSlot = sourceIndex + 1;

                // From CharacterIconListPreview (regular slot)
                if (sender_name == "CharacterIconListPreview")
                {
                    if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        MoveOnlyBaseCostume(VM, page, sourceSlot, -1);
                    } else if (reciver_name == "PlaceholderCostumeIconListPreview")
                    {
                        // Move to placeholder costume list
                        int targetSlot = -1;
                        int targetPageForCheck = -1;

                        if (VM.PlaceholderCostumePlaceHolderList != null && VM.PlaceholderCostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.PlaceholderCostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.PlaceholderCostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            targetSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                            targetPageForCheck = -1;
                        } else
                        {
                            return;
                        }

                        if (IsMovingIntoOwnSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, targetPageForCheck, targetSlot))
                            return;

                        if (sourcePerson.CostumeIndex == 1 && sourcePerson.PageIndex == targetPageForCheck && sourcePerson.SlotIndex == targetSlot)
                            return;

                        VM.ReplaceSlots(page, sourceSlot, targetPageForCheck, targetSlot);
                    } else if (reciver_name == "CharacterCostumeIconListPreview")
                    {
                        // Move to character costume list
                        int targetSlot = -1;
                        int targetPageForCheck = page;

                        if (VM.CostumePlaceHolderList != null && VM.CostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.CostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.CostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedCharacterIndex >= 0 && VM.SelectedCharacterIndex < VM.CharacterList.Count)
                        {
                            targetSlot = VM.CharacterList[VM.SelectedCharacterIndex].SlotIndex;
                            targetPageForCheck = page;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            targetSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                            targetPageForCheck = -1;
                        } else
                        {
                            return;
                        }

                        if (IsMovingIntoOwnSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, targetPageForCheck, targetSlot))
                            return;

                        if (sourcePerson.CostumeIndex == 1 && sourcePerson.PageIndex == targetPageForCheck && sourcePerson.SlotIndex == targetSlot)
                            return;

                        VM.ReplaceSlots(page, sourceSlot, targetPageForCheck, targetSlot);
                    }
                }
                // From CharacterPlaceHolderIconListPreview (placeholder slot)
                else if (sender_name == "CharacterPlaceHolderIconListPreview")
                {
                    if (reciver_name == "CharacterIconListPreview")
                    {
                        MoveOnlyBaseCostume(VM, -1, sourceSlot, page);
                    } else if (reciver_name == "PlaceholderCostumeIconListPreview")
                    {
                        int targetSlot = -1;
                        int targetPageForCheck = -1;

                        if (VM.PlaceholderCostumePlaceHolderList != null && VM.PlaceholderCostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.PlaceholderCostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.PlaceholderCostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            targetSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                            targetPageForCheck = -1;
                        } else
                        {
                            return;
                        }

                        if (IsMovingIntoOwnSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, targetPageForCheck, targetSlot))
                            return;

                        if (sourcePerson.CostumeIndex == 1 && sourcePerson.PageIndex == targetPageForCheck && sourcePerson.SlotIndex == targetSlot)
                            return;

                        VM.ReplaceSlots(-1, sourceSlot, targetPageForCheck, targetSlot);
                    } else if (reciver_name == "CharacterCostumeIconListPreview")
                    {
                        // Move placeholder slot to character costume list (convert to costume)
                        int targetSlot = -1;
                        int targetPageForCheck = page;

                        if (VM.CostumePlaceHolderList != null && VM.CostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.CostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.CostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedCharacterIndex >= 0 && VM.SelectedCharacterIndex < VM.CharacterList.Count)
                        {
                            targetSlot = VM.CharacterList[VM.SelectedCharacterIndex].SlotIndex;
                            targetPageForCheck = page;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            targetSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                            targetPageForCheck = -1;
                        } else
                        {
                            return;
                        }

                        if (IsMovingIntoOwnSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, targetPageForCheck, targetSlot))
                            return;

                        // Don't allow moving base costume into its own slot
                        if (sourcePerson.CostumeIndex == 1 && sourcePerson.PageIndex == targetPageForCheck && sourcePerson.SlotIndex == targetSlot)
                            return;

                        // Use ReplaceSlots which handles conversion to costume
                        VM.ReplaceSlots(-1, sourceSlot, targetPageForCheck, targetSlot);
                    }
                }
                // From PlaceholderCostumeIconListPreview (placeholder costume)
                else if (sender_name == "PlaceholderCostumeIconListPreview")
                {
                    if (reciver_name == "CharacterIconListPreview")
                    {
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, page);
                    } else if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, -1);
                    } else if (reciver_name == "CharacterCostumeIconListPreview")
                    {
                        // Move placeholder costume to character costume list
                        int targetSlot = -1;
                        int targetPageForCheck = page;

                        if (VM.CostumePlaceHolderList != null && VM.CostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.CostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.CostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedCharacterIndex >= 0 && VM.SelectedCharacterIndex < VM.CharacterList.Count)
                        {
                            targetSlot = VM.CharacterList[VM.SelectedCharacterIndex].SlotIndex;
                            targetPageForCheck = page;
                        } else
                        {
                            return;
                        }

                        // Find max costume index at target
                        int maxCostume = 0;
                        foreach (CharacterSelectParamModel existingEntry in VM.CharacterFullList)
                        {
                            if (existingEntry.PageIndex == targetPageForCheck && existingEntry.SlotIndex == targetSlot && existingEntry.CostumeIndex > maxCostume)
                            {
                                maxCostume = existingEntry.CostumeIndex;
                            }
                        }

                        // Update source entry
                        foreach (CharacterSelectParamModel entry in VM.CharacterFullList)
                        {
                            if (entry.PageIndex == sourcePerson.PageIndex &&
                                entry.SlotIndex == sourcePerson.SlotIndex &&
                                entry.CostumeIndex == sourcePerson.CostumeIndex)
                            {
                                entry.PageIndex = targetPageForCheck;
                                entry.SlotIndex = targetSlot;
                                entry.CostumeIndex = maxCostume + 1;
                                break;
                            }
                        }

                        var sorted = new ObservableCollection<CharacterSelectParamModel>(
                            VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex)
                        );
                        VM.CharacterFullList = sorted;
                    }
                }
                // From CharacterCostumeIconListPreview (character costume)
                else if (sender_name == "CharacterCostumeIconListPreview")
                {
                    if (reciver_name == "CharacterIconListPreview")
                    {
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, page);
                    } else if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, -1);
                    } else if (reciver_name == "PlaceholderCostumeIconListPreview")
                    {
                        // Move character costume to placeholder costume list
                        int targetSlot = -1;
                        int targetPageForCheck = -1;

                        if (VM.PlaceholderCostumePlaceHolderList != null && VM.PlaceholderCostumePlaceHolderList.Count > 0)
                        {
                            targetPageForCheck = VM.PlaceholderCostumePlaceHolderList[0].PageIndex;
                            targetSlot = VM.PlaceholderCostumePlaceHolderList[0].SlotIndex;
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0 && VM.SelectedPlaceholderCharacterIndex < VM.CharacterPlaceHolderList.Count)
                        {
                            targetSlot = VM.CharacterPlaceHolderList[VM.SelectedPlaceholderCharacterIndex].SlotIndex;
                            targetPageForCheck = -1;
                        } else
                        {
                            return;
                        }

                        // Find max costume index at target
                        int maxCostume = 0;
                        foreach (CharacterSelectParamModel existingEntry in VM.CharacterFullList)
                        {
                            if (existingEntry.PageIndex == targetPageForCheck && existingEntry.SlotIndex == targetSlot && existingEntry.CostumeIndex > maxCostume)
                            {
                                maxCostume = existingEntry.CostumeIndex;
                            }
                        }

                        // Update source entry
                        foreach (CharacterSelectParamModel entry in VM.CharacterFullList)
                        {
                            if (entry.PageIndex == sourcePerson.PageIndex &&
                                entry.SlotIndex == sourcePerson.SlotIndex &&
                                entry.CostumeIndex == sourcePerson.CostumeIndex)
                            {
                                entry.PageIndex = targetPageForCheck;
                                entry.SlotIndex = targetSlot;
                                entry.CostumeIndex = maxCostume + 1;
                                break;
                            }
                        }

                        var sorted = new ObservableCollection<CharacterSelectParamModel>(
                            VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex)
                        );
                        VM.CharacterFullList = sorted;
                    }
                }
            }

            // Refresh page and restore selection
            VM.RosterPage_field = 100;
            VM.RosterPage_field = page;

            if (selectedCSP != null && selectedSlot >= 0)
            {
                if (selectedPage >= 0)
                {
                    for (int i = 0; i < VM.CharacterList.Count; i++)
                    {
                        if (VM.CharacterList[i].CSP_code == selectedCSP && VM.CharacterList[i].SlotIndex == selectedSlot)
                        {
                            VM.SelectedCharacterIndex = i;
                            break;
                        }
                    }
                } else if (selectedPage == -1)
                {
                    for (int i = 0; i < VM.CharacterPlaceHolderList.Count; i++)
                    {
                        if (VM.CharacterPlaceHolderList[i].CSP_code == selectedCSP && VM.CharacterPlaceHolderList[i].SlotIndex == selectedSlot)
                        {
                            VM.SelectedPlaceholderCharacterIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        // Move only the base costume (CostumeIndex == 1)
        private void MoveOnlyBaseCostume(CharacterRosterEditorNS4ViewModel VM, int sourcePage, int sourceSlot, int targetPage)
        {
            int freeSlot = VM.FreeSlotOnPage(targetPage);

            // Gather all entries at the same page+slot (all costumes of one character)
            var entriesToMove = VM.CharacterFullList
                .Where(e => e.PageIndex == sourcePage && e.SlotIndex == sourceSlot)
                .ToList();

            if (!entriesToMove.Any())
                return;

            // Move all found entries to target page and free slot
            foreach (var entry in entriesToMove)
            {
                entry.PageIndex = targetPage;
                entry.SlotIndex = freeSlot;
            }

            // Compact slots on source page: all slots > sourceSlot decrease by 1
            foreach (var entry in VM.CharacterFullList)
            {
                if (entry.PageIndex == sourcePage && entry.SlotIndex > sourceSlot)
                {
                    entry.SlotIndex--;
                }
            }

            // Sort and reassign
            var sorted = new ObservableCollection<CharacterSelectParamModel>(
                VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex)
            );
            VM.CharacterFullList = sorted;
        }

        // Helper for costume reordering
        private void SwapCostumeIndices(CharacterRosterEditorNS4ViewModel VM, int page, int slot, int costume1, int costume2)
        {
            foreach (CharacterSelectParamModel entry in VM.CharacterFullList)
            {
                if (entry.PageIndex == page && entry.SlotIndex == slot)
                {
                    if (entry.CostumeIndex == costume1)
                    {
                        entry.CostumeIndex = -999; // Temp value
                    } else if (entry.CostumeIndex == costume2)
                    {
                        entry.CostumeIndex = costume1;
                    }
                }
            }

            foreach (CharacterSelectParamModel entry in VM.CharacterFullList)
            {
                if (entry.PageIndex == page && entry.SlotIndex == slot && entry.CostumeIndex == -999)
                {
                    entry.CostumeIndex = costume2;
                }
            }

            var sorted = new ObservableCollection<CharacterSelectParamModel>(
                VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex)
            );
            VM.CharacterFullList = sorted;
        }

        internal static class Utils
        {
            //Find the parent element according to the child element
            public static T FindVisualParent<T>(DependencyObject obj) where T : class
            {
                while (obj != null)
                {
                    if (obj is T)
                        return obj as T;

                    obj = VisualTreeHelper.GetParent(obj);
                }
                return null;
            }
        }
    }
}
