using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Converters;

namespace Iubh.RadioApp.Droid.Converters
{
    public class InvertedBoolConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == true)
            {
                return false;
            }
            return true;
        }
    }
}
