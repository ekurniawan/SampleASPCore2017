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
        public async Task<ActionResult> Index()
        {
            ViewData["Title"] = "Students";
            ViewData["bController"] = "Students";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Students";
            ViewData["bItemValue"] = "Student List";

            ViewData["pesan"] = TempData["pesan"];

            //var results = context.Students.OrderBy(s => s.LastName).ToList();
            var results = await (from s in context.Students
                          orderby s.LastName ascending
                          select s).AsNoTracking().ToListAsync();
            return View(results);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewData["Title"] = "Student Detail";
            ViewData["bController"] = "Students";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Students";
            ViewData["bItemValue"] = "Student Detail";

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
            catch(DbUpdateException ex)
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
                var editStudent = await (from s in context.Students
                                   where s.StudentID == id
                                   select s).SingleOrDefaultAsync();

                if (ModelState.IsValid)
                {
                    if (editStudent != null)
                    {
                        editStudent.LastName = student.LastName;
                        editStudent.FirstMidName = student.FirstMidName;
                        editStudent.EnrollmentDate = student.EnrollmentDate;
                        await context.SaveChangesAsync();

                        TempData["pesan"] = @"<div class='alert alert-success alert-dismissable'>
                        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>
                        Proses edit data Student " + student.LastName + " berhasil</div>";

                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch(Exception ex)
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
            if(result!=null)
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

                if(result!=null)
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