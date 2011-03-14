using PageTypeBuilder;

namespace UserGroup.Site.PageTypes
{
    [PageType(Filename = "~/Templates/pages/EventsList.aspx", Name = "Event Container", AvailablePageTypes= new[]{typeof(EventPage)})]
    public class EventContainer : PageTypeBase
    {
    }
}