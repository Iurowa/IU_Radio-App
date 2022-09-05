using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.headerview")]
    public class HeaderView : RelativeLayout
    {
        ImageButton back;
        ImageButton actionButton;
        TextView title;


        public ViewStates ShowBack
        {
            get
            {
                return this.back.Visibility;
            }
            set
            {
                this.back.Visibility = value == ViewStates.Visible ? ViewStates.Visible : ViewStates.Invisible;
            }
        }


        public Drawable ActionButtonImage
        {
            get
            {
                return this.actionButton.Drawable;;
            }
            set
            {
                this.actionButton.SetImageDrawable(value);
            }
        }

        public string HeaderTitle
        {
            get
            {
                return this.title.Text;
            }
            set
            {
                this.title.Text = value;
            }
        }

        public ViewStates ShowHeaderButton
        {
            get
            {
                return this.actionButton.Visibility;
            }
            set
            {
                this.actionButton.Visibility = value == ViewStates.Visible ? ViewStates.Visible : ViewStates.Invisible;
            }
        }

       
        public ImageButton BackBtn
        {
            get
            {
                return this.back;
            }
            set
            {

            }
        }

        public ImageButton HeaderButton
        {
            get
            {
                return this.actionButton;
            }
        }

        public HeaderView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.Initialize(context, attrs);
        }

        private void Initialize(Context context, IAttributeSet attrs)
        {
            HeaderView.Inflate(context, Resource.Layout.header_view, this);

            this.back = this.FindViewById<ImageButton>(Resource.Id.Header_Back);
            this.actionButton = this.FindViewById<ImageButton>(Resource.Id.Header_ActionButton);
            this.title = this.FindViewById<TextView>(Resource.Id.Header_Title);
        }
    }
}