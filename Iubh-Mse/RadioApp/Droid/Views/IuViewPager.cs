using System.Collections;
using System.Windows.Input;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Binding.Attributes;
using MvvmCross.Platforms.Android.Binding.Views;
using Iubh.RadioApp.Droid.Adapters;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.iuviewpager")]
    public class IuViewPager
       : Android.Support.V4.View.ViewPager
    {
        public IuViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            this.ItemTemplateId = itemTemplateId;
        }

        public new BasePagerAdapter Adapter
        {
            get { return base.Adapter as BasePagerAdapter; }
            set
            {
                var existing = Adapter;
                if (existing == value)
                    return;

                if (existing != null && value != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                    value.ItemTemplateId = existing.ItemTemplateId;
                }

                base.Adapter = value;
                this.Adapter.ItemTemplateId = this.ItemTemplateId;
            }
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return Adapter.ItemsSource; }
            set { Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId { get; set; }

        private ICommand _itemPageSelected;
        public ICommand ItemPageSelected
        {
            get { return _itemPageSelected; }
            set { _itemPageSelected = value; if (_itemPageSelected != null) EnsureItemPageSelectedOverloaded(); }
        }

        private bool _itemPageSelectedOverloaded;
        private void EnsureItemPageSelectedOverloaded()
        {
            if (_itemPageSelectedOverloaded)
                return;

            _itemPageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => ExecuteCommandOnItem(ItemPageSelected, args.Position);
        }

        protected virtual void ExecuteCommandOnItem(ICommand command, int position)
        {
            if (command == null)
                return;

            var item = Adapter.GetRawItem(position);
            if (item == null)
                return;

            if (!command.CanExecute(item))
                return;

            command.Execute(item);
        }

        private ICommand _pageSelected;
        public new ICommand PageSelected
        {
            get { return _pageSelected; }
            set { _pageSelected = value; if (_pageSelected != null) EnsurePageSelectedOverloaded(); }
        }

        private bool _pageSelectedOverloaded;
        private void EnsurePageSelectedOverloaded()
        {
            if (_pageSelectedOverloaded)
                return;

            _pageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => ExecuteCommand(PageSelected, args.Position);
        }

        protected virtual void ExecuteCommand(ICommand command, int toPage)
        {
            if (command == null)
                return;

            if (!command.CanExecute(toPage))
                return;

            command.Execute(toPage);
        }
    }
}