using PageTypeBuilder;
using UserGroup.PageTypes;

namespace UserGroup.Site.PageTypes
{
    [PageType(Filename = "~/Templates/Pages/Standard.aspx", Name = "Standard Page", AvailablePageTypes = new []{typeof(StandardPage), typeof(ContainerPage),typeof(SpeakerContainer),typeof(EventContainer)})]
    public class StandardPage : PageTypeBase
    {

    }
}   