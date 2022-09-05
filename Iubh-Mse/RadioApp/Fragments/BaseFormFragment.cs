using System;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class BaseFormFragment<T> : MvxFragment<T>, View.IOnTouchListener where T: MvxViewModel
    {
        private ScrollView scrollView;
        private InputMethodManager inputMethodManager;

        protected void RegisterForHideKeyboard(ScrollView scrollView)
        {
            this.scrollView = scrollView;

            this.inputMethodManager = (InputMethodManager)this.Activity.GetSystemService(Android.Content.Context.InputMethodService);
            this.scrollView.SetOnTouchListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Move:
                    {
                        this.inputMethodManager.HideSoftInputFromWindow(v.WindowToken, HideSoftInputFlags.None);
                        break;
                    }

            }
            return false;
        }

        public void CloseKeboard(View v)
        {
            this.inputMethodManager.HideSoftInputFromWindow(v.WindowToken, HideSoftInputFlags.None);
        }
    }
}
