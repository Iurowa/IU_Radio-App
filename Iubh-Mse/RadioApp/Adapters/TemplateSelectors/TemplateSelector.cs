using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace Iubh.RadioApp.Droid.Adapters.TemplateSelectors
{
    public abstract class TemplateSelector : IMvxTemplateSelector
    {
        public HeaderData Header { get; set; }

        public FooterData Footer { get; set; }

        public int ItemTemplateId { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public const int HeaderViewTypeId = 1;

        public const int FooterViewTypeId = 2;

        public const int MaxViewTypeId = FooterViewTypeId;

        public virtual int GetItemViewType(object forItemObject)
        {
            if (forItemObject is HeaderData)
            {
                return HeaderViewTypeId;
            }
            else if (forItemObject is FooterData)
            {
                return FooterViewTypeId;
            }

            return default(int);
        }

        public virtual int GetItemLayoutId(int fromViewType)
        {
            switch (fromViewType)
            {
                case HeaderViewTypeId:
                    return this.Header.LayoutId;
                case FooterViewTypeId:
                    return this.Footer.LayoutId;
                default:
                    return default(int);
            }
        }
    }
}