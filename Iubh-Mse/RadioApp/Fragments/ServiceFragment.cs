using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;
using Iubh.RadioApp.Droid.Views.RecyclerView;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class ServiceFragment : MvxFragment<ServiceViewModel>
    {
        private MvxRecyclerView recyclerView;
        private HeaderView header;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_service, container, false);

            var source = new ServiceAdapter((IMvxAndroidBindingContext)this.BindingContext, this.Activity.WindowManager.DefaultDisplay);

            this.recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.Service_RecyclerView);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Service_Header);

            this.recyclerView.Adapter = source;

            var set = this.CreateBindingSet<ServiceFragment, ServiceViewModel>();

            set.Bind(source).For(x => x.ItemClick).To(x => x.NavigateDetailCommand);

            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(source).For(x => x.ItemsSource).To(x => x.Items);

            set.Apply();

            return view;
        }
    }
}