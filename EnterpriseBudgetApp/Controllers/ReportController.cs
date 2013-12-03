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


namespace EnterpriseBudgetApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private vm343_01aEntities db = new vm343_01aEntities();

        //
        // GET: /Report/
        public ActionResult Index()
        {

            int currentUserId = (int) Membership.GetUser().ProviderUserKey;
            var transactions = db.Transactions.Include(t => t.TransType1).Include(t => t.UserProfile);
            Transaction[] trans = transactions.Where(t => t.AcctId.Equals(currentUserId)).ToArray();
            String[] cat;
            Decimal[] data;
            int i = 0;

            foreach (Transaction tr in trans)
            {

                cat[i] = tr.TransType;
                data[i] = tr.Amount;

                i++;
            }


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
.SetXAxis(new XAxis
{
    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
})
.SetSeries(new Series
{
    Data = new DotNet.Highcharts.Helpers.Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
});

            return View(chart);

        }

    }
}
