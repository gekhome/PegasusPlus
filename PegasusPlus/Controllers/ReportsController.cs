using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Telerik.Reporting.Cache.Interfaces;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;


namespace PegasusPlus.Controllers
{
    public class ReportsController : ReportsControllerBase
    {
        static ReportServiceConfiguration preservedConfiguration;

        static IReportServiceConfiguration PreservedConfiguration
        {
            get
            {
                if (null == preservedConfiguration)
                {
                    preservedConfiguration = new ReportServiceConfiguration
                    {
                        HostAppId = "WebApplication1",
                        Storage = new FileStorage(),
                        ReportResolver = CreateResolver(),
                        // ReportSharingTimeout = 0,
                        // ClientSessionTimeout = 15,
                    };
                }
                return preservedConfiguration;
            }
        }

        public ReportsController()
        {
            this.ReportServiceConfiguration = PreservedConfiguration;
        }

        static IReportResolver CreateResolver()
        {
            var appPath = HttpContext.Current.Server.MapPath("~/");
            var reportsPath = HttpContext.Current.Server.MapPath("~/Reports/");

            return new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());
        }
    }

}
