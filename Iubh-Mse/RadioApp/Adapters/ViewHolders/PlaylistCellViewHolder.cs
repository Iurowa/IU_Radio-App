using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public class PlaylistCellViewHolder : BaseViewHolder
    {
        private TextView title;
        private TextView artist;
        private TextView time;

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
        }

        public PlaylistCellViewHolder(View itemView, IMvxAndroidBindingContext context)
            : base(itemView, context)
        {            
            this.artist = itemView.FindViewById<TextView>(Resource.Id.Cell_Playlist_Artist);
            this.title = itemView.FindViewById<TextView>(Resource.Id.Cell_Playlist_Title);
            this.time = itemView.FindViewById<TextView>(Resource.Id.Cell_Playlist_Time); 


            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<PlaylistCellViewHolder, PlaylistTableViewModel>();
                set.Bind(this.title).To(x => x.Title);
                set.Bind(this.artist).To(x => x.Interpreter);
                set.Bind(this.time).To(x => x.Time);
                set.Apply();

            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.title.Dispose();
            this.title = null;

            this.time.Dispose();
            this.time = null;

            this.artist.Dispose();
            this.artist = null;
        }
    }
}