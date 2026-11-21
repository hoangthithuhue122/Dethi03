using System.Diagnostics;
using HoangThiThuHue_241230737.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HoangThiThuHue_241230737.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLTaiSanContext _db;
        public HomeController(QLTaiSanContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var ds = _db.TaiSans.Where(t => t.NguyenGia != null && t.NguyenGia >= 20).ToList();
            return View(ds);
        }
        public IActionResult GetTaiSanByLoai(int id)
        {
            var ds = _db.TaiSans.Where(t => t.MaLoai == id).ToList();
            ViewBag.TieuDe = _db.LoaiTaiSans
                            .Where(l => l.MaLoai == id)
                            .Select(l => l.TenLoai)
                            .FirstOrDefault();
            return PartialView("HoangThiThuHue_MainContent", ds);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DsLoai = new SelectList(_db.LoaiTaiSans, "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("MaLoai","TenTs","NguyenGia","Anh")] TaiSan model)
        {
            if (model.NguyenGia == null)
                ModelState.AddModelError("NguyenGia", "Nguyên giá không đc để trống");

            ModelState.Remove("MaLoaiNavigation");
            if (ModelState.IsValid)
            {
                _db.TaiSans.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DsLoai = new SelectList(_db.LoaiTaiSans, "MaLoai", "TenLoai", model.MaLoai);
            return View(model);
        }
    }
}
