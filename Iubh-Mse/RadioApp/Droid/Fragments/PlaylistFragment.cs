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
    public class PlaylistFragment : MvxFragment<PlaylistViewModel>
    {
        private MvxRecyclerView recyclerView;
        private HeaderView header;
        private Spinner date;
        private Spinner time;
        private Button reloadBtn;
        private RelativeLayout loading;
        private TextView isEmtpy;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_playlist, container, false);

            var source = new PlaylistAdapter((IMvxAndroidBindingContext)this.BindingContext);

            this.recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.Playlist_RecyclerView);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Playlist_Header);
            this.date = view.FindViewById<Spinner>(Resource.Id.Playlist_Date);
            this.time = view.FindViewById<Spinner>(Resource.Id.Playlist_Time);
            this.reloadBtn = view.FindViewById<Button>(Resource.Id.Playlist_ReloadButton);
            this.loading = view.FindViewById<RelativeLayout>(Resource.Id.Playlist_Loading);
            this.isEmtpy = view.FindViewById<TextView>(Resource.Id.Playlist_EmtpyItems); 

            this.date.Adapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, this.ViewModel.Dates);
            this.time.Adapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, this.ViewModel.Times);
            
            this.recyclerView.AddItemDecoration(new LineDividerItemDecoration(this.Context));
            this.recyclerView.Adapter = source;

            var set = this.CreateBindingSet<PlaylistFragment, PlaylistViewModel>();

            set.Bind(source).For(x => x.ItemsSource).To(x => x.Items);
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.reloadBtn).To(x => x.ReloadCommand);
            set.Bind(this.loading).For(x => x.Visibility).To(x => x.IsLoading).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.isEmtpy).For(x => x.Visibility).To(x => x.IsEmpty).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.time).For(x => x.Enabled).To(x => x.IsLoading).WithConversion(Converters.Converters.InvertedBoolConverter);
            set.Bind(this.date).For(x => x.Enabled).To(x => x.IsLoading).WithConversion(Converters.Converters.InvertedBoolConverter);
            set.Bind(this.reloadBtn).For(x => x.Enabled).To(x => x.IsLoading).WithConversion(Converters.Converters.InvertedBoolConverter);
            set.Apply();

            date.ItemSelected += DateItemSelected;
            time.ItemSelected += TimeItemSelected;

            var popup = this.time.Class.GetDeclaredField("mPopup");
            popup.Accessible = true;
            var popupWindow = popup.Get(this.time) as ListPopupWindow;
            popupWindow.Height = 800;

            return view;
        }

        private void DateItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.ViewModel.UpdateDate(this.date.SelectedItem.ToString());
            this.time.Adapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, this.ViewModel.Times);
        }

        private void TimeItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.ViewModel.SelectedTime = this.time.SelectedItem.ToString();
        }
    }
}