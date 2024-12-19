using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class BannerController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();

        public ActionResult Index()
        {
            // Sadece aktif olan bannerları listele
            var banners = db.TblBanners.ToList();
            return View(banners);
        }



        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]

        public ActionResult Create(TblBanner model)
        {

            db.TblBanners.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {

            var value = db.TblBanners.Find(id);
            db.TblBanners.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Show(int id)
        {
            //seçilen id ye göre satırı seçti
            var selectedBanner = db.TblBanners.FirstOrDefault(b => b.BannerId == id);

            //selectedBanner değişkeni null değil, değerler atandı
            if (selectedBanner != null)
            {
                // Diğer tüm banner'ları listele ve her birini "IsShown" olarak false yap
                var allBanners = db.TblBanners.ToList();
                foreach (var banner in allBanners)
                {
                    banner.IsShown = false;
                }

                // Seçilen banner'ı "IsShown" olarak true yap
                selectedBanner.IsShown = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

            //db de bu değişiklik yapılınca uı kontolü  yapan DefaultController.cs deki defaultBanner() metodu sadece
            //true ları göster dediği için istenen veri partial alana yansıdı
        }

    }
}