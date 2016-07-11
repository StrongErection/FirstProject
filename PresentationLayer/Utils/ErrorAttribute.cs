using System;
using System.Web.Mvc;
using SoccerTeams.DatabaseOperationsService;
using System.ServiceModel;

namespace SoccerTeams.Utils
{
    public class ErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Home", "Error");
            if (ex is FaultException<ServiceData>)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "ServerError",
                    ViewData = new ViewDataDictionary(ex as FaultException<ServiceData>)
                };
            }
            else
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "ClientError",
                    ViewData = new ViewDataDictionary(ex)
                };
            }
        }
    }
}