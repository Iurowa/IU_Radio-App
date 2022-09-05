﻿using Android.Support.V7.Widget;
using Android.Views;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters.ViewHolders;
using System.Collections.Specialized;

namespace Iubh.RadioApp.Droid.Adapters
{
    public class WishlistAdapter : BaseRecyclerAdapter
    {

        public WishlistAdapter(IMvxAndroidBindingContext bindingContext)
            : base(bindingContext)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, this.BindingContext.LayoutInflaterHolder);
                       
            View view = this.InflateViewForHolder(parent, Resource.Layout.Cell_Wishlist, itemBindingContext);
            viewType = Resource.Layout.Cell_Wishlist;
            
            return this.GetViewHolder(viewType, view, itemBindingContext);
        }

        protected override RecyclerView.ViewHolder GetViewHolder(int viewType, View view, IMvxAndroidBindingContext itemBindingContext)
        {
            return new WishlistCellViewHolder(view, itemBindingContext);
        }

        protected override void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsSourceCollectionChanged(sender, e);
        }

    }
}