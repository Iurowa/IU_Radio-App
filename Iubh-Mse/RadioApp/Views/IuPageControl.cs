using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Droid.Themes;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.iupagecontrol")]
    public class IuPageControl : LinearLayout
    {
        private IList<IuCircleButton> items;

        private ViewPager viewPager;
        public ViewPager ViewPager
        {
            get { return this.viewPager; }
            set
            {
                this.viewPager = value;
                this.SetView();
            }
        }

        public IuPageControl(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public IuPageControl(Context context) : base(context) { }
        public IuPageControl(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.items = new List<IuCircleButton>();
            this.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent);
        }

        private void SetView()
        {
            var layoutParams = new LayoutParams(Default.PageControlItemSize, Default.PageControlItemSize);
            layoutParams.LeftMargin = Default.PageControlItemSize / 2;
            layoutParams.RightMargin = Default.PageControlItemSize / 2;

            for (int i = 0; i < this.ViewPager.Adapter.Count; i++)
            {
                var circleButton = new IuCircleButton(this.Context);
                circleButton.LayoutParameters = layoutParams;

                circleButton.Click += this.OnItemClick;

                this.items.Add(circleButton);
                this.AddView(circleButton);
            }

            this.HighlightSelectedPage();
            this.ViewPager.PageSelected += this.OnPageSelected;
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            this.ViewPager.CurrentItem = this.items.IndexOf((IuCircleButton)sender);
        }

        private void OnPageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            this.HighlightSelectedPage();
        }

        private void HighlightSelectedPage()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                var item = this.items[i];
                int backgroundDrawableId = Resource.Drawable.DarkCircle;
                if (i == this.ViewPager.CurrentItem)
                {
                    backgroundDrawableId = Resource.Drawable.HighlightCircle;
                }
                item.Background = ContextCompat.GetDrawable(this.Context, backgroundDrawableId);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.ViewPager != null && this.ViewPager is IuViewPager == false)
            {
                this.ViewPager.PageSelected -= this.OnPageSelected;
            }

            foreach (var item in this.items)
            {
                item.Click -= this.OnItemClick;
            }

            base.Dispose(disposing);
        }
    }
}