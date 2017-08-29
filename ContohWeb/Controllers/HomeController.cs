using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContohWeb.Models;

namespace ContohWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello(string nama,string alamat)
        {
            return Content("Hello Nama anda " + nama+" alamat anda "+alamat);
        }

        public IActionResult HelloWorld(string id,string nama,string alamat)
        {
            return Content("ID Anda : "+id+" Nama: "+nama+" Alamat: "+alamat);
        }

        public IActionResult Profil(string nama,string alamat)
        {
            string[] hobby = new string[] {
                "berenang", "ngegame", "sepedaan", "marathon", "tidur" };
            ViewData["hobby"] = hobby;
            ViewData["nama"] = nama;
            ViewData["alamat"] = alamat;
            ViewBag.Telp = "08156881199";
            return View();
        }

        [HttpPost]
        public IActionResult TampilRegistrasi(string firstname,string lastname,int umur,
            string email,DateTime tanggal,string gender,string negara,string[] hobby,
            string summary)
        {
            ViewData["firstname"] = firstname;
            ViewData["lastname"] = lastname;
            ViewData["umur"] = umur;
            ViewData["email"] = email;
            ViewData["tanggal"] = tanggal.ToString("MM-dd-yyyy");
            ViewData["gender"] = gender;
            ViewData["negara"] = negara;
            ViewData["hobby"] = hobby;
            ViewData["summary"] = summary;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
