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
            int currentUserId = (int) Membership.GetUser().ProviderUserKey;
            var transactions = db.Transactions.Include(t => t.TransType1).Include(t => t.UserProfile);
            Transaction[] trans = transactions.Where(t => t.AcctId.Equals(currentUserId)).ToArray();
            String[] cat = null ;
            Object[] data = null;

            for(int i = 0; i < trans.Length ; i++)
            {
                 cat[i] = trans[i].TransType.ToString();
                 data[i] = trans[i].Amount;
            }


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
.SetXAxis(new XAxis
{
    Categories = cat
})
.SetSeries(new Series
{
    Data = new DotNet.Highcharts.Helpers.Data( data )
});

            return View(chart);

        }

    }
}
