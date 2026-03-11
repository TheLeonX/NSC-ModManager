using DynamicData;
using NSC_ModManager.Model;
using NSC_ModManager.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NSC_ModManager.View
{
    public partial class CharacterRosterEditorView : Window
    {
        public CharacterRosterEditorView(TitleViewModel VM)
        {
            InitializeComponent();
            DataContext = new CharacterRosterEditorViewModel(VM);
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

        private void LBoxSort_OnDrop(object sender, DragEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            reciver_name = lb.Name;
            CharacterRosterEditorViewModel VM = ((CharacterRosterEditorViewModel)this.DataContext);
            var pos = e.GetPosition(lb);
            var result = VisualTreeHelper.HitTest(lb, pos);
            if (result == null)
            {
                return;
            }

            var sourcePerson = e.Data.GetData(typeof(CharacterSelectParamModel)) as CharacterSelectParamModel;
            if (sourcePerson == null)
            {
                return;
            }

            int sourceIndex = -1;
            int targetIndex = -1;
            int page = VM.RosterPage_field;

            // Save the currently selected character's info to restore after refresh
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

            // Determine source
            if (sender_name == "CharacterIconListPreview")
            {
                sourceIndex = VM.CharacterList.IndexOf(sourcePerson);
            } else if (sender_name == "CharacterPlaceHolderIconListPreview")
            {
                sourceIndex = VM.CharacterPlaceHolderList.IndexOf(sourcePerson);
            } else if (sender_name == "PlaceholderCostumeIconListPreview")
            {
                sourceIndex = VM.PlaceholderCostumePlaceHolderList.IndexOf(sourcePerson);
            } else if (sender_name == "CharacterCostumeIconListPreview")
            {
                sourceIndex = VM.CostumePlaceHolderList.IndexOf(sourcePerson);
            }

            // Handle drop based on source and target
            if (reciver_name == sender_name)
            {
                // Same list - reorder
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null)
                {
                    return;
                }
                var targetPerson = listBoxItem.Content as CharacterSelectParamModel;
                if (ReferenceEquals(targetPerson, sourcePerson))
                {
                    return;
                }

                if (sender_name == "CharacterIconListPreview")
                {
                    targetIndex = VM.CharacterList.IndexOf(targetPerson);
                } else if (sender_name == "CharacterPlaceHolderIconListPreview")
                {
                    targetIndex = VM.CharacterPlaceHolderList.IndexOf(targetPerson);
                } else if (sender_name == "PlaceholderCostumeIconListPreview")
                {
                    targetIndex = VM.PlaceholderCostumePlaceHolderList.IndexOf(targetPerson);
                } else if (sender_name == "CharacterCostumeIconListPreview")
                {
                    targetIndex = VM.CostumePlaceHolderList.IndexOf(targetPerson);
                }

                int sourceSlot = sourceIndex + 1;
                int targetSlot = targetIndex + 1;

                if (sender_name == "CharacterIconListPreview")
                {
                    VM.ReplaceSlots(page, sourceSlot, page, targetSlot);
                } else if (sender_name == "CharacterPlaceHolderIconListPreview")
                {
                    VM.ReplaceSlots(-1, sourceSlot, -1, targetSlot);
                } else if (sender_name == "PlaceholderCostumeIconListPreview")
                {
                    // Swap costume indices within placeholder costume list
                    if (selectedPage == -1 && VM.SelectedPlaceholderCharacterIndex >= 0)
                    {
                        SwapCostumeIndices(VM, -1, selectedSlot, sourcePerson.CostumeIndex, targetPerson.CostumeIndex);
                    }
                } else if (sender_name == "CharacterCostumeIconListPreview")
                {
                    // Swap costume indices within character costume list
                    if (selectedPage >= 0 && VM.SelectedCharacterIndex >= 0)
                    {
                        SwapCostumeIndices(VM, selectedPage, selectedSlot, sourcePerson.CostumeIndex, targetPerson.CostumeIndex);
                    } else if (selectedPage == -1 && VM.SelectedPlaceholderCharacterIndex >= 0)
                    {
                        SwapCostumeIndices(VM, -1, selectedSlot, sourcePerson.CostumeIndex, targetPerson.CostumeIndex);
                    }
                }
            } else
            {
                // Cross-list - convert
                int sourceSlot = sourceIndex + 1;

                // From CharacterIconListPreview (regular slot)
                if (sender_name == "CharacterIconListPreview")
                {
                    if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        // Move only base costume to placeholder
                        MoveOnlyBaseCostume(VM, page, sourceSlot, -1);
                    } else if (reciver_name == "PlaceholderCostumeIconListPreview")
                    {
                        // Convert to placeholder costume
                        if (VM.SelectedPlaceholderCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedPlaceholderCharacterIndex + 1;
                            VM.ReplaceSlots(page, sourceSlot, -1, targetSlot);
                        }
                    } else if (reciver_name == "CharacterCostumeIconListPreview")
                    {
                        // Convert to costume of the selected character
                        if (VM.SelectedCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedCharacterIndex + 1;
                            VM.ReplaceSlots(page, sourceSlot, page, targetSlot);
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedPlaceholderCharacterIndex + 1;
                            VM.ReplaceSlots(page, sourceSlot, -1, targetSlot);
                        }
                    }
                }
                // From CharacterPlaceHolderIconListPreview (placeholder slot)
                else if (sender_name == "CharacterPlaceHolderIconListPreview")
                {
                    if (reciver_name == "CharacterIconListPreview")
                    {
                        // Move only base costume to regular page
                        MoveOnlyBaseCostume(VM, -1, sourceSlot, page);
                    } else if (reciver_name == "PlaceholderCostumeIconListPreview")
                    {
                        // Convert to placeholder costume
                        if (VM.SelectedPlaceholderCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedPlaceholderCharacterIndex + 1;
                            VM.ReplaceSlots(-1, sourceSlot, -1, targetSlot);
                        }
                    } else if (reciver_name == "CharacterCostumeIconListPreview")
                    {
                        // Convert to costume of the selected character
                        if (VM.SelectedCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedCharacterIndex + 1;
                            VM.ReplaceSlots(-1, sourceSlot, page, targetSlot);
                        } else if (VM.SelectedPlaceholderCharacterIndex >= 0)
                        {
                            int targetSlot = VM.SelectedPlaceholderCharacterIndex + 1;
                            VM.ReplaceSlots(-1, sourceSlot, -1, targetSlot);
                        }
                    }
                }
                // From PlaceholderCostumeIconListPreview (placeholder costume)
                else if (sender_name == "PlaceholderCostumeIconListPreview")
                {
                    if (reciver_name == "CharacterIconListPreview")
                    {
                        // Convert placeholder costume to standalone slot on current page
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, page);
                    } else if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        // Convert placeholder costume to placeholder slot
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
                        // Convert costume to standalone slot on current page
                        VM.ConvertCostumeToSlot(sourcePerson.PageIndex, sourcePerson.SlotIndex, sourcePerson.CostumeIndex, page);
                    } else if (reciver_name == "CharacterPlaceHolderIconListPreview")
                    {
                        // Convert costume to placeholder slot
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

            // Refresh the page
            VM.RosterPage_field = 100;
            VM.RosterPage_field = page;

            // Restore the selection
            if (selectedCSP != null && selectedSlot >= 0)
            {
                if (selectedPage >= 0)
                {
                    // Restore selection in CharacterList
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
                    // Restore selection in CharacterPlaceHolderList
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

        // Move only the base costume (CostumeIndex == 0)
        private void MoveOnlyBaseCostume(CharacterRosterEditorViewModel VM, int sourcePage, int sourceSlot, int targetPage)
        {
            int freeSlot = VM.FreeSlotOnPage(targetPage);

            // Gather all entries at the same page+slot (all costumes of one character)
            var entriesToMove = VM.CharacterFullList
                .Where(entry => entry.PageIndex == sourcePage && entry.SlotIndex == sourceSlot)
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
            var sortCharacterList = new ObservableCollection<CharacterSelectParamModel>(
                VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex)
            );
            VM.CharacterFullList = sortCharacterList;
        }

        // Helper for costume reordering
        private void SwapCostumeIndices(CharacterRosterEditorViewModel VM, int page, int slot, int costume1, int costume2)
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

            var sortCharacterList = new ObservableCollection<CharacterSelectParamModel>();
            sortCharacterList.AddRange(VM.CharacterFullList.OrderBy(x => x.PageIndex).ThenBy(x => x.SlotIndex).ThenBy(x => x.CostumeIndex));
            VM.CharacterFullList = sortCharacterList;
        }

        internal static class Utils
        {
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
