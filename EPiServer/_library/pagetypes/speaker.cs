using EPiServer.Core;
using EPiServer.SpecializedProperties;
using PageTypeBuilder;
using UserGroup.PageTypes;

namespace UserGroup.Site.PageTypes
{
    [PageType(Filename = "~/Templates/Pages/Speaker.aspx", Name = "Speaker Page")]
    public class SpeakerPage : TypedPageData
    {
        [PageTypeProperty(Type = typeof(PropertyXhtmlString), SortOrder = 30, EditCaption = "Bio")]
        public virtual string Bio { get; set; }

        [PageTypeProperty(Type = typeof(PropertyDocumentUrl), SortOrder = 2, EditCaption = "ProfilePicture")]
        public virtual string ProfilePicture { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 22, EditCaption = "Blog URL")]
        public virtual string BlogUrl { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 24, EditCaption = "Facebook URL")]
        public virtual string FacebookUrl { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 23, EditCaption = "LinkedIn URL")]
        public virtual string LinkedInUrl { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 21, EditCaption = "Twitter Name")]
        public virtual string TwitterHandle { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 1, EditCaption = "First Name")]
        public virtual string FirstName { get; set; }

        [PageTypeProperty(Type = typeof(PropertyString), SortOrder = 1, EditCaption = "Last Name")]
        public virtual string LastName { get; set; }
    }
}