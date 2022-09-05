using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Adapters.TemplateSelectors;

namespace Iubh.RadioApp.Droid.Views.RecyclerView
{
    public class LineDividerItemDecoration : Android.Support.V7.Widget.RecyclerView.ItemDecoration
    {
        private readonly Drawable divider;

        public LineDividerItemDecoration(Context context)
        {
            this.divider = ContextCompat.GetDrawable(context, Resource.Drawable.LineDivider);
        }

        public virtual List<int> GetNoDividerIndexes(BaseRecyclerAdapter adapter)
        {
            var noDividerIndex = new List<int>();
            if (adapter.ItemTemplateSelector is TemplateSelector)
            {
                var templateSelector = (TemplateSelector)adapter.ItemTemplateSelector;
                if (templateSelector.Header != null)
                {
                    noDividerIndex.Add(0);
                }

                if (templateSelector.Footer != null)
                {
                    noDividerIndex.Add(adapter.ItemCount - 1);
                }
            }

            return noDividerIndex;
        }

        public override void OnDrawOver(Canvas cValue, Android.Support.V7.Widget.RecyclerView parent, Android.Support.V7.Widget.RecyclerView.State state)
        {
            var noDividerIndexes = new List<int>();

            var adapter = parent.GetAdapter() as BaseRecyclerAdapter;
            if (adapter != null)
            {
                noDividerIndexes = this.GetNoDividerIndexes(adapter);
            }

            int childCount = parent.ChildCount;
            for (int i = 0; i < childCount; i++)
            {
                var childView = parent.GetChildAt(i);
                var position = parent.GetChildAdapterPosition(childView);

                if (noDividerIndexes.Contains(position) == true)
                {
                    continue;
                }

                this.DrawDivider(i, cValue, parent);
            }
        }

        protected void DrawDivider(int index, Canvas cValue, Android.Support.V7.Widget.RecyclerView parent)
        {
            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;

            var childView = parent.GetChildAt(index);

            int top = childView.Bottom + 20;
            int bottom = top + this.divider.IntrinsicHeight;

            this.divider.SetBounds(left, top, right, bottom);
            this.divider.Draw(cValue);
        }
    }
}