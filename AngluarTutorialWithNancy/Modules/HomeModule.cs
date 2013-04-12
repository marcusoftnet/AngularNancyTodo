using Nancy;

namespace AngluarTutorialWithNancy.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => { return View["Index"]; };
        }
    }
}