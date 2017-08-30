using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContohWeb.Controllers
{
    //sample
    public class MahasiswaController : Controller
    {
        static List<Mahasiswa> lstMhs = new List<Mahasiswa>
        {
            new Mahasiswa {Nim="72009847",Nama="Erick Kurniawan",Email="erick@gmail.com",IPK=3.2},
            new Mahasiswa {Nim="71098475",Nama="Budi Sutejo",Email="budi@gmail.com",IPK=3.5},
            new Mahasiswa {Nim="71098476",Nama="Halim Budi",Email="halim@gmail.com",IPK=3.9},
            new Mahasiswa {Nim="71098477",Nama="Joko Sutopo",Email="joko@gmail.com",IPK=2.5},
            new Mahasiswa {Nim="71098478",Nama="Bambang",Email="bambang@gmail.com",IPK=3.0}
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Daftar Mahasiswa";
            ViewData["bController"] = "Home";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Home";
            ViewData["bItemValue"] = "Daftar Mahasiswa";

            return View(lstMhs);
        }

        public IActionResult Details(string id)
        {
            ViewData["Title"] = "Detail Mahasiswa";
            ViewData["bController"] = "Mahasiswa";
            ViewData["bAction"] = "Index";
            ViewData["bValue"] = "Daftar Mahasiswa";
            ViewData["bItemValue"] = "Detail Mahasiswa";

            var mhs = lstMhs.Where(m => m.Nim == id).SingleOrDefault();
            return View(mhs);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
