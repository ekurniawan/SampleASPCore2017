using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContohWeb.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext context;

        public CoursesController(SchoolContext context)
        {
            this.context = context;
        }

        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var results = from c in context.Courses.Include(c => c.Department)
                          orderby c.Title
                          select c;

            return View(await results.AsNoTracking().ToListAsync());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(),
                "DepartmentID", "Name", selectedDepartment);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Add(course);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateDepartmentsDropDownList();
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Courses/Edit/5
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

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Courses/Delete/5
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