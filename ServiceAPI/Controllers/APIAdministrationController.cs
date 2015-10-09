using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Controllers
{
    public class APIAdministrationController : Controller
    {
        // GET: APIAdministration
        public ActionResult Index()
        {
            return View();
        }

        // GET: APIAdministration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: APIAdministration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: APIAdministration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: APIAdministration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: APIAdministration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: APIAdministration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: APIAdministration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
