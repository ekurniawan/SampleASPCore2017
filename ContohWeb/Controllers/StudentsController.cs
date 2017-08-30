using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;

namespace ContohWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext context)
        {
            this.context = context;
        }

        // GET: Students
        public ActionResult Index()
        {
            ViewData["Title"] = "Students";
            ViewData["bController"] = "Students";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Students";
            ViewData["bItemValue"] = "Student List";

            //var results = context.Students.OrderBy(s => s.LastName).ToList();
            var results = (from s in context.Students
                          orderby s.LastName ascending
                          select s).ToList();
            return View(results);
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            ViewData["Title"] = "Student Detail";
            ViewData["bController"] = "Students";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Students";
            ViewData["bItemValue"] = "Student Detail";

            var result = (from s in context.Students
                          where s.StudentID == id
                          select s).SingleOrDefault();

            if (result != null)
            {
                return View(result);
            }
            return NotFound("Data tidak ditemukan..");
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}