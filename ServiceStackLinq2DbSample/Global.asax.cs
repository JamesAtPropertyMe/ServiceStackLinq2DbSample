using System.Web;

namespace ServiceStackLinq2DbSample
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            new AppHost().Init();
        }
    }
}
