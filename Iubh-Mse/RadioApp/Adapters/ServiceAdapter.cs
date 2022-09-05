using Android.Support.V7.Widget;
using Android.Views;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters.ViewHolders;

namespace Iubh.RadioApp.Droid.Adapters
{
    public class ServiceAdapter : BaseRecyclerAdapter
    {
        private Display DefaultDisplay;

        public ServiceAdapter(IMvxAndroidBindingContext bindingContext, Display defaultDisplay)
            : base(bindingContext)
        {
            this.DefaultDisplay = defaultDisplay;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, this.BindingContext.LayoutInflaterHolder);
            var item = this.ItemsSource.ElementAt(this.CurrentPosition);

            View view;
            if (item is TableHeadViewModel)
            {
                view = this.InflateViewForHolder(parent, Resource.Layout.Cell_Head, itemBindingContext);
                viewType = Resource.Layout.Cell_Head;
            }
            else
            {
                view = this.InflateViewForHolder(parent, Resource.Layout.Cell_Service, itemBindingContext);
                viewType = Resource.Layout.Cell_Service;
            }

            return this.GetViewHolder(viewType, view, itemBindingContext);
        }


        protected override RecyclerView.ViewHolder GetViewHolder(int viewType, View view, IMvxAndroidBindingContext itemBindingContext)
        {
            switch (viewType)
            {
                case Resource.Layout.Cell_Head:
                    return new HeadCellViewHolder(view, itemBindingContext);
                default:
                    return new ServiceCellViewHolder(view, itemBindingContext);
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
        }

    }
}