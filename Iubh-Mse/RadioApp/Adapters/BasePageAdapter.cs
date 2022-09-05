using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.Extensions;
using MvvmCross.Exceptions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.WeakSubscription;
using MvvmCross.WeakSubscription;
using Iubh.RadioApp.Droid.Adapters.ViewHolders;

namespace Iubh.RadioApp.Droid.Adapters
{
    public abstract class BasePagerAdapter : PagerAdapter
    {
        private readonly IMvxAndroidBindingContext bindingContext;
        private int itemTemplateId;
        private IEnumerable itemsSource;
        private IDisposable subscription;

        public bool ReloadAllOnDataSetChange { get; set; }

        protected BasePagerAdapter(Context context, IMvxAndroidBindingContext bindingContext)
        {
            this.Context = context;
            this.bindingContext = bindingContext;
            if (this.bindingContext == null)
            {
                throw new MvxException("MvxBindableListView can only be used within a Context which supports IMvxBindingActivity");
            }

            ReloadAllOnDataSetChange = true; // default is to reload all
        }

        protected Context Context { get; }

        protected IMvxAndroidBindingContext BindingContext => this.bindingContext;

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get => this.itemsSource;
            set => this.SetItemsSource(value);
        }

        public int ItemTemplateId
        {
            get => this.itemTemplateId;
            set
            {
                if (this.itemTemplateId == value)
                {
                    return;
                }
                this.itemTemplateId = value;

                // since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
                if (itemsSource != null)
                {
                    NotifyDataSetChanged();
                }
            }
        }

        public override int Count => this.itemsSource.Count();

        protected virtual void SetItemsSource(IEnumerable value)
        {
            if (Equals(itemsSource, value))
            {
                return;
            }

            if (subscription != null)
            {
                subscription.Dispose();
                subscription = null;
            }

            this.itemsSource = value;
            if (this.itemsSource != null && !(itemsSource is IList))
            {
                //MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
            }
            var newObservable = itemsSource as INotifyCollectionChanged;
            if (newObservable != null)
            {
                subscription = newObservable.WeakSubscribe(this.OnItemsSourceCollectionChanged);
            }
            NotifyDataSetChanged();
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged(e);
        }

        public virtual void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
        {
            base.NotifyDataSetChanged();
        }

        public int GetPosition(object item)
        {
            return this.itemsSource.GetPosition(item);
        }

        public System.Object GetRawItem(int position)
        {
            return this.itemsSource.ElementAt(position);
        }

        private BaseViewPagerViewHolder GetViewHolder(int position, int templateId)
        {
            if (this.itemsSource == null)
            {
                //MvxBindingTrace.Trace(MvxTraceLevel.Error, "GetViewHolder called when ItemsSource is null");
                return null;
            }

            var source = this.GetRawItem(position);

            return this.GetBindableViewHolder(source, templateId);
        }

        protected virtual BaseViewPagerViewHolder GetBindableViewHolder(object source, int templateId)
        {
            if (templateId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(templateId), "templateId should not be 0");
            }

            var viewHolder = this.CreateBindableViewHolder(templateId);
            viewHolder.DataContext = source;
            return viewHolder;
        }

        protected abstract BaseViewPagerViewHolder CreateBindableViewHolder(int templateId);

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var viewHolder = this.GetViewHolder(position, this.ItemTemplateId);

            container.AddView(viewHolder.View);

            return viewHolder.View;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
        {
            var view = (View)obj;
            container.RemoveView(view);
            view.Dispose();
        }

        public override bool IsViewFromObject(View p0, Java.Lang.Object p1)
        {
            return p0 == p1;
        }

        // this as a simple non-performant fix for non-updating views - see http://stackoverflow.com/a/7287121/373321        
        public override int GetItemPosition(Java.Lang.Object obj)
        {
            if (ReloadAllOnDataSetChange)
                return PagerAdapter.PositionNone;

            return base.GetItemPosition(obj);
        }
    }
}