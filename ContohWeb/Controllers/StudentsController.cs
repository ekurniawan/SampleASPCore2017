﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;
using ContohWeb.Models.ViewModels;
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
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,int? page)
        {
            ViewData["FirstSortParam"] = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
            ViewData["LastSortParam"] = sortOrder == "last_asc" ? "last_desc" : "last_asc";
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            ViewData["pesan"] = TempData["pesan"];

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var results = from s in context.Students
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.FirstMidName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "first_desc":
                    results = results.OrderByDescending(s => s.FirstMidName);
                    break;
                case "last_asc":
                    results = results.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    results = results.OrderByDescending(s => s.LastName);
                    break;
                case "date_asc":
                    results = results.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    results = results.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    results = results.OrderBy(s => s.FirstMidName);
                    break;
            }

            int pageSize = 5;

            return View(await PaginatedList<Student>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize));
            //return View(await results.AsNoTracking().ToListAsync());
        }

        public async Task<ActionResult> About()
        {
            var data = from s in context.Students
                       group s by s.EnrollmentDate into dateGroup
                       select new EnrollmentDateGroup
                       {
                           EnrollmentDate = dateGroup.Key,
                           StudentCount = dateGroup.Count()
                       };
            return View(await data.AsNoTracking().ToListAsync());
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