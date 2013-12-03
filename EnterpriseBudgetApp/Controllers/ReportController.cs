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

namespace EnterpriseBudgetApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ReportController : Controller
    {
        private vm343_01aEntities db = new vm343_01aEntities();

        //
        // GET: /Report/
        public ActionResult Index()
        {
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;
            var transactions = db.Transactions.Include(t => t.TransType1).Include(t => t.UserProfile);
            Transaction[] trans = transactions.Where(t => t.AcctId.Equals(currentUserId)).ToArray();
            List<Object> dataSeries = new List<Object>();
            List<String> cat = new List<String>();
            List<Object> data = new List<Object>();

            for (int i = 0; i < trans.Length; i++)
            {
                dataSeries.Add(new DotNet.Highcharts.Options.Point
                                               {
                                                   Name = trans[i].TransType1.Name,
                                                   Y = (DotNet.Highcharts.Helpers.Number) 
                                                        trans[i].Amount,
                                                   Sliced = true,
                                                   Selected = true
                                               });
                                               
            }


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
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
                        Events = new PlotOptionsPiePointEvents
                        {
                            Click = "function() { alert (this.category +': '+ this.y); }"

                        }
                    }

                }
            })
.SetSeries(new Series
{
    Type = ChartTypes.Pie,
    Name = "Browser share",
    Data = new Data(dataSeries.ToArray() )

});

            return View(chart);

        }

    }



}
