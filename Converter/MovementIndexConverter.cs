using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NSC_ModManager.Converter
{
    [ValueConversion(typeof(int), typeof(string))]
    class MovementIndexConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string state = "???";
            switch ((int)value) {
                case 0:
                    state = "PL_ANM_DSH_FWD_L Base";
                    break;
                case 1:
                    state = "PL_ANM_DSH_FWD_R Base";
                    break;
                case 2:
                    state = "PL_ANM_DSH_BK Base";
                    break;
                case 3:
                    state = "PL_ANM_DSH_L0 Base";
                    break;
                case 4:
                    state = "PL_ANM_DSH_L1 Base";
                    break;
                case 5:
                    state = "PL_ANM_DSH_R0 Base";
                    break;
                case 6:
                    state = "PL_ANM_DSH_R1 Base";
                    break;
                case 7:
                    state = "PL_ANM_DSH_FWD_L InstantAwakening";
                    break;
                case 8:
                    state = "PL_ANM_DSH_FWD_R InstantAwakening";
                    break;
                case 9:
                    state = "PL_ANM_DSH_BK InstantAwakening";
                    break;
                case 10:
                    state = "PL_ANM_DSH_L0 InstantAwakening";
                    break;
                case 11:
                    state = "PL_ANM_DSH_L1 InstantAwakening";
                    break;
                case 12:
                    state = "PL_ANM_DSH_R0 InstantAwakening";
                    break;
                case 13:
                    state = "PL_ANM_DSH_R1 InstantAwakening";
                    break;
                case 14:
                    state = "PL_ANM_DSH_FWD_L TrueAwakening";
                    break;
                case 15:
                    state = "PL_ANM_DSH_FWD_R TrueAwakening";
                    break;
                case 16:
                    state = "PL_ANM_DSH_BK TrueAwakening";
                    break;
                case 17:
                    state = "PL_ANM_DSH_L0 TrueAwakening";
                    break;
                case 18:
                    state = "PL_ANM_DSH_L1 TrueAwakening";
                    break;
                case 19:
                    state = "PL_ANM_DSH_R0 TrueAwakening";
                    break;
                case 20:
                    state = "PL_ANM_DSH_R1 TrueAwakening";
                    break;
            }

            return state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
