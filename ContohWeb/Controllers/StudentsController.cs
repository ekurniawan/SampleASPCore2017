using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> Index(string sortOrder)
        {
            ViewData["FirstSortParam"] = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";

            ViewData["pesan"] = TempData["pesan"];

            //var results = context.Students.OrderBy(s => s.LastName).ToList();
            var results = from s in context.Students
                          select s;

            switch (sortOrder)
            {
                case "first_desc":
                    results = results.OrderByDescending(s => s.FirstMidName);
                    break;
                default:
                    results = results.OrderBy(s => s.FirstMidName);
                    break;
            }

            return View(await results.AsNoTracking().ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await (from s in context.Students
                                where s.StudentID == id
                                select s).SingleOrDefaultAsync();

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
        public async Task<ActionResult> Create([Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Students.Add(student);
                    await context.SaveChangesAsync();

                    TempData["pesan"] = @"<div class='alert alert-success alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        Proses tambah data Student berhasil</div>";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (DbUpdateException ex)
            {
                /*ViewData["pesan"] = @"<div class='alert alert-warning alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        <strong>Kesalahan:</strong>"+ex.Message+"</div>";
                return View();*/
                ModelState.AddModelError("", "Kesalahan :" + ex.Message);
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await (from s in context.Students
                                where s.StudentID == id
                                select s).SingleOrDefaultAsync();

            if (result != null)
                return View(result);

            return NotFound("Student tidak ditemukan !");
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Update(student);
                    await context.SaveChangesAsync();

                    TempData["pesan"] = @"<div class='alert alert-success alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        Proses edit data Student " + student.LastName + " berhasil</div>";

                    return RedirectToAction("Index");

                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = @"<div class='alert alert-warning alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        <strong>Kesalahan:</strong>" + ex.Message + "</div>";
                return View();
            }
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await (from s in context.Students
                                where s.StudentID == id
                                select s).SingleOrDefaultAsync();
            if (result != null)
                return View(result);

            return NotFound("Data tidak ditemukan");
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Student student)
        {
            try
            {
                var result = await (from s in context.Students
                                    where s.StudentID == student.StudentID
                                    select s).SingleOrDefaultAsync();

                if (result != null)
                {
                    context.Students.Remove(result);
                    await context.SaveChangesAsync();

                    TempData["pesan"] = @"<div class='alert alert-success alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        Proses delete data berhasil</div>";

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = @"<div class='alert alert-warning alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        <strong>Kesalahan:</strong>" + ex.Message + "</div>";
                return View();
            }
        }
    }
}