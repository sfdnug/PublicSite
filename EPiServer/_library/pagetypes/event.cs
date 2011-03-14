using EPiServer.Core;
using PageTypeBuilder;
using EPiServer.SpecializedProperties;

namespace UserGroup.Site.PageTypes
{
    [PageType(Filename = "~/Templates/Pages/Event.aspx", Name = "Event Page")]
    public class EventPage : PageTypeBase
    {
        [PageTypeProperty(Type = typeof(PropertyDate), SortOrder = 1, EditCaption = "Starting Date")]
        public virtual string StartTime { get; set; }

        [PageTypeProperty(Type = typeof(PropertyDate), SortOrder = 1, EditCaption = "Ending Time")]
        public virtual string EndiTime { get; set; }

        [PageTypeProperty(Type = typeof(PropertyPageReference), SortOrder = 1, EditCaption = "Speaker")]
        public virtual string Speaker { get; set; }

        [PageTypeProperty(Type = typeof(PropertyXhtmlString), SortOrder = 1, EditCaption = "Location")]
        public virtual string Location { get; set; }

        [PageTypeProperty(Type = typeof(PropertyNumber), SortOrder = 1, EditCaption = "Attendance")]
        public virtual string Attendance { get; set; }

        [PageTypeProperty(Type = typeof(PropertyUrl), SortOrder = 1, EditCaption = "RSVP Link")]
        public virtual string RsvpLink { get; set; }
    }
}