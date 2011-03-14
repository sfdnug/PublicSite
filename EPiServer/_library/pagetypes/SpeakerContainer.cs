using PageTypeBuilder;

namespace UserGroup.Site.PageTypes
{
    [PageType(Filename = "~/Templates/Standard.aspx", Name = "Speaker Container", AvailablePageTypes= new[]{typeof(SpeakerPage)})]
    public class SpeakerContainer : PageTypeBase
    {
    }
}