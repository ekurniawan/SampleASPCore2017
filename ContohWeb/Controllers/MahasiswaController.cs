using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ContohWeb.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContohWeb.Controllers
{
    public class MahasiswaController : Controller
    {
        static List<Mahasiswa> lstMhs = new List<Mahasiswa>
        {
            new Mahasiswa {Nim="72009847",Nama="Erick Kurniawan",Email="erick@gmail.com",IPK=3.2},
            new Mahasiswa {Nim="71098475",Nama="Budi Sutejo",Email="budi@gmail.com",IPK=3.5}
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(lstMhs);
        }
    }
}
