using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;
using Iubh.RadioApp.Droid.Views.RecyclerView;
using MvvmCross.Droid.Support.V4;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class WishListFragment : MvxFragment<WishListViewModel>
    {
        private MvxRecyclerView recyclerView;
        private MvxSwipeRefreshLayout refreshrecyclerView;
        private HeaderView header;
        private RelativeLayout loading;
        private TextView isEmtpy;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_wishlist, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Wishlist_Header);
            this.recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.Wishlist_RecyclerView);
            this.loading = view.FindViewById<RelativeLayout>(Resource.Id.Wishlist_Loading);
            this.isEmtpy = view.FindViewById<TextView>(Resource.Id.Wishlist_EmtpyItems);
            this.refreshrecyclerView = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.Wishlist_RecyclerViewRefresh); 

            var source = new WishlistAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.recyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.recyclerView.Adapter = source;
       
            var set = this.CreateBindingSet<WishListFragment, WishListViewModel>();
            
            set.Bind(source).For(x => x.ItemsSource).To(x => x.Items);
            set.Bind(source).For(x => x.ItemClick).To(x => x.NavigateDetailCommand);
            set.Bind(this.loading).For(x => x.Visibility).To(x => x.IsLoading).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.isEmtpy).For(x => x.Visibility).To(x => x.IsEmpty).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.refreshrecyclerView).For(x => x.Refreshing).To(x => x.IsLoading);
            set.Bind(this.refreshrecyclerView).For(x => x.RefreshCommand).To(x => x.RefreshCommand);

            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header.HeaderButton).To(x => x.NavigateLogoutCommand);

            set.Apply();
            this.refreshrecyclerView.Refresh += RefreshrecyclerView_Refresh;
            return view;
        }

        private void RefreshrecyclerView_Refresh(object sender, System.EventArgs e)
        {
            var source = new WishlistAdapter((IMvxAndroidBindingContext)this.BindingContext);
            this.recyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.recyclerView.Adapter = source;
            this.recyclerView.ItemsSource = this.ViewModel.Items;
        }
    }
}