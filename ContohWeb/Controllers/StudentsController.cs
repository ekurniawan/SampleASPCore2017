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

            ViewData["pesan"] = TempData["pesan"];

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
            ViewData["Title"] = "Create Student";
            ViewData["bController"] = "Students";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Students";
            ViewData["bItemValue"] = "Create Student";

            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Students.Add(student);
                    context.SaveChanges();

                    TempData["pesan"] = @"<div class='alert alert-success alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        Proses tambah data Student berhasil</div>";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = @"<div class='alert alert-warning alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        <strong>Kesalahan:</strong>"+ex.Message+"</div>";
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            var result = (from s in context.Students
                          where s.StudentID == id
                          select s).SingleOrDefault();

            if (result != null)
                return View(result);

            return NotFound("Student tidak ditemukan !");
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