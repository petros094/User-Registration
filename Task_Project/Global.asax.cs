using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Task_Project.App_Start;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace Task_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            OAuthConfig.RegisterProviders();
                
         
        }
    }
}
