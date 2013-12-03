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
                data.Add(trans[i].Amount);
                cat.Add(trans[i].TransType1.Name);
                dataSeries.Add(new Object[] { trans[i].Amount, trans[i].TransType1.Name });
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
    Data = new Data( new object[]
                                           {
                                               new object[] { "Firefox", 45.0 },
                                               new object[] { "IE", 26.8 },
                                               new DotNet.Highcharts.Options.Point
                                               {
                                                   Name = "Chrome",
                                                   Y = 12.8,
                                                   Sliced = true,
                                                   Selected = true
                                               },
                                               new object[] { "Safari", 8.5 },
                                               new object[] { "Opera", 6.2 },
                                               new object[] { "Others", 0.7 }
                                           })

});

            return View(chart);

        }

    }



}
