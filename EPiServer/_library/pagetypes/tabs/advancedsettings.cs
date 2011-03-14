using EPiServer.Security;
using PageTypeBuilder;

namespace UserGroup.PageTypes.Tabs
{
    public class AdvancedSettings : Tab
    {
        public override string Name
        {
            get { return "Advanced Settings"; }
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