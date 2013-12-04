using EnterpriseBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using EnterpriseBudgetApp.Models;
using System.Web.Security;
using DotNet.Highcharts.Options;
using EnterpriseBudgetApp.Filters;
using DotNet.Highcharts.Enums;
using System.Drawing;
using DotNet.Highcharts.Helpers;
using EnterpriseBudgetApp.Controllers.BLL;

namespace EnterpriseBudgetApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ReportController : Controller
    {
        private vm343_01aEntities db = new vm343_01aEntities();

        private ReportLogic rptLogic = new ReportLogic(); 

        //
        // GET: /Report/
        public ActionResult Index()
        {
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;

            List<Object> dataSeries = rptLogic.getData( currentUserId );


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetTitle( new Title { Text = "Expense Report" } ) 
            .SetPlotOptions(new PlotOptions
            {
                Pie = new PlotOptionsPie
                {
                   
                    AllowPointSelect = true,
                    Cursor = Cursors.Pointer,
                    DataLabels = new PlotOptionsPieDataLabels
        {
            Color = ColorTranslator.FromHtml("#000000"),
            ConnectorColor = ColorTranslator.FromHtml("#000000"),
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
        },
                    Point = new PlotOptionsPiePoint
                    {
                    }

                }
            })
.SetSeries(new Series
{
    Type = ChartTypes.Pie,
    Name = "Browser share",
    Data = new Data(  dataSeries.ToArray() )

});

            return View(chart);

        }

    }



}
