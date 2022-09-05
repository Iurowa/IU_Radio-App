using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Converters;

namespace Iubh.RadioApp.Droid.Converters
{
    public class VisibilityConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == true)
            {
                return ViewStates.Visible;
            }
            return ViewStates.Gone;
        }
    }
}
