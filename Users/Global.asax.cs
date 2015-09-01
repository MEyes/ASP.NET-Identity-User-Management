using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Users
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //AuthorizeRequest += MvcApplication_AuthorizeRequest;
        }

        private void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
