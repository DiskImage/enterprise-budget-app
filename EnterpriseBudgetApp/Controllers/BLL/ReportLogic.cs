using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnterpriseBudgetApp.Models;
using System.Collections.Generic;
using System.Data.Entity;



namespace EnterpriseBudgetApp.Controllers.BLL
{
    public class ReportLogic
    {

        private vm343_01aEntities db = new vm343_01aEntities();


        public List<Object> getData(int id)
        {

            var transactions = db.Transactions.Include(t => t.TransType1).Include(t => t.UserProfile);
            Transaction[] trans = transactions.Where(t => t.AcctId.Equals(id)).ToArray();
            List<Object> dataSeries = new List<Object>();


            //Iterates through all the transactions
            //they need to be organized by categories 
            for (int i = 0; i < 4; i++)
            {

                var query = from tr in db.Transactions
                            where tr.AcctId == id
                            where tr.TransType1.TransId == i
                            select tr;

                DotNet.Highcharts.Helpers.Number num = 0;
                String name = "";


                foreach (Transaction tr in query)
                {

                    num += (DotNet.Highcharts.Helpers.Number)tr.Amount;
                    name = tr.TransType1.Name;

                }

                if (num == 0)
                {
                    continue;
                }

                dataSeries.Add(new DotNet.Highcharts.Options.Point
                                                   {
                                                       Name = name,
                                                       Y = num,
                                                       Sliced = false,
                                                       Selected = false
                                                   });

            }

            return dataSeries;


        }

    }

}