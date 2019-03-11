using Starcounter;
using Starcounter.Startup.Routing.Middleware;


namespace BootstrapExample.ViewModels
{
    public partial class MasterNavigationPage : MasterPageBase
    {
        public override void SetPartial(Json partial)
        {
            InnerJson = partial;
        }
    }
}