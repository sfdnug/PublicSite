using EPiServer.Core;
using EPiServer.SpecializedProperties;
using PageTypeBuilder;
using UserGroup.PageTypes.Tabs;

namespace UserGroup.Site.PageTypes
{
    public class PageTypeBase : TypedPageData
    {
        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 0, HelpText = "Replacement Title For the PageName")]
        public virtual string AlternatePageTitle { get; set; }

        [PageTypeProperty(Type = typeof(PropertyXhtmlString), SortOrder = 1)]
        public virtual string MainBody { get; set; }

        [PageTypeProperty(Type = typeof(PropertyXhtmlString), SortOrder = 1)]
        public virtual string Summary { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 1, EditCaption = "Template Override", HelpText = "Over writes the default pagetype template", Tab = typeof(AdvancedSettings))]
        public virtual string CustomTemplate { get; set; }
    }
}
