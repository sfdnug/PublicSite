using EPiServer.Security;
using PageTypeBuilder;

namespace UserGroup.PageTypes.Tabs
{
    public class Widgets : Tab
    {
        public override string Name
        {
            get { return "Widgets"; }
        }

        public override AccessLevel RequiredAccess
        {
            get { return AccessLevel.Edit; }
        }

        public override int SortIndex
        {
            get { return 100; }
        }
    }
}