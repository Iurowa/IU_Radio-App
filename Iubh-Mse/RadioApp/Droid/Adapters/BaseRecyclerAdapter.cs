using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.Extensions;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System.Collections;
using System.Collections.Generic;

namespace Iubh.RadioApp.Droid.Adapters
{
    public abstract class BaseRecyclerAdapter : MvxRecyclerAdapter
    {
        public int CurrentPosition { get; set; }
        protected BaseRecyclerAdapter(IMvxAndroidBindingContext bindingContext)
            : base(bindingContext)
        {
        }

   
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, this.BindingContext.LayoutInflaterHolder);
            var view = this.InflateViewForHolder(parent, viewType, itemBindingContext);

            return this.GetViewHolder(viewType, view, itemBindingContext);
        }

        protected abstract RecyclerView.ViewHolder GetViewHolder(int viewType, View view, IMvxAndroidBindingContext itemBindingContext);

        protected override View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
        {
            var view = base.InflateViewForHolder(parent, viewType, bindingContext);
            return view;
        }

        public override object GetItem(int position)
        {
            this.CurrentPosition = position;
            return this.ItemsSource.ElementAt(position);
        }

        public override int ItemCount => this.ItemsSource.Count();

        [MvxSetToNullAfterBinding]
        public override IEnumerable ItemsSource
        {
            get { return this.GetInternalItemsSource(); }
            set { base.ItemsSource = value; }
        }

        private IEnumerable GetInternalItemsSource()
        {
            var itemsSource = GetItemsSource();
            if (itemsSource == null)
            {
                yield break;
            }

            foreach (var item in itemsSource)
            {
                yield return item;
            }
        }

        protected virtual IEnumerable GetItemsSource()
        {
            return base.ItemsSource;
        }
    }
}