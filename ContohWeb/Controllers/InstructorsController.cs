using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;
using Microsoft.EntityFrameworkCore;
using ContohWeb.Models.ViewModels;

namespace ContohWeb.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext context;

        public InstructorsController(SchoolContext context)
        {
            this.context = context;
        }

        // GET: Instructors
        /*public async Task<ActionResult> Index()
        {
            var results = from i in context.Instructors.Include(i => i.OfficeAssignment)
                          orderby i.LastName
                          select i;
            return View(await results.AsNoTracking().ToListAsync());
        }*/

        public async Task<IActionResult> Index(int? id,int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await (from i in context.Instructors.Include(i => i.OfficeAssignment).Include(i => i.CourseAssignments)
                                            .ThenInclude(i => i.Course).ThenInclude(i => i.Enrollments).ThenInclude(i => i.Student)
                                        .Include(i => i.CourseAssignments).ThenInclude(i => i.Course).ThenInclude(i => i.Department)
                                           orderby i.LastName select i)
                                        .AsNoTracking().ToListAsync();
                                        
            if(id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = (from i in viewModel.Instructors
                                         where i.InstructorID == id.Value
                                         select i).SingleOrDefault();

                viewModel.Courses = from c in instructor.CourseAssignments
                                    select c.Course;
            }

            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                viewModel.Enrollments = (from c in viewModel.Courses
                                         where c.CourseID == courseID
                                         select c).Single().Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
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

        // GET: Instructors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instructors/Edit/5
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

        // GET: Instructors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instructors/Delete/5
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