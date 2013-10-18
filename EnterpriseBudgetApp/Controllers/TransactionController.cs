﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnterpriseBudgetApp.Models;

namespace EnterpriseBudgetApp.Controllers
{
    public class TransactionController : Controller
    {
        private vm343_01aEntities db = new vm343_01aEntities();

        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.TransType1).Include(t => t.UserProfile);
            return View(transactions.ToList());
        }

        //
        // GET: /Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Create

        public ActionResult Create()
        {
            ViewBag.TransType = new SelectList(db.TransTypes, "TransId", "Name");
            ViewBag.AcctId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Transaction/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransType = new SelectList(db.TransTypes, "TransId", "Name", transaction.TransType);
            ViewBag.AcctId = new SelectList(db.UserProfiles, "UserId", "UserName", transaction.AcctId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransType = new SelectList(db.TransTypes, "TransId", "Name", transaction.TransType);
            ViewBag.AcctId = new SelectList(db.UserProfiles, "UserId", "UserName", transaction.AcctId);
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransType = new SelectList(db.TransTypes, "TransId", "Name", transaction.TransType);
            ViewBag.AcctId = new SelectList(db.UserProfiles, "UserId", "UserName", transaction.AcctId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}