using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class AboutController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();

        public ActionResult Index()
        {
            var about = db.TblAbouts.ToList();
            return View(about);
        }



        [HttpGet]
        public ActionResult Update(int id)
        {
            var about = db.TblAbouts.Find(id);
            return View(about);
        }

        [HttpPost]
        public ActionResult Update(TblAbout model)
        {
            var value = db.TblAbouts.Find(model.AboutId);
            if (ModelState.IsValid)
            {
                
                if (model.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                    model.ImageFile.SaveAs(fileName);
                    model.ImageUrl = "/images/" + model.ImageFile.FileName;
                }


                if (model.CvFile != null)
                {
                    // Cv dosyalarının kaydedileceği ana dizin
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var cvDirectory = Path.Combine(baseDirectory, "cv");

                    // Dizin yoksa oluştur
                    if (!Directory.Exists(cvDirectory))
                    {
                        Directory.CreateDirectory(cvDirectory);
                    }

                    // Dosya adını oluştur ve kaydet
                    var cvFilePath = Path.Combine(cvDirectory, model.CvFile.FileName);
                    model.CvFile.SaveAs(cvFilePath);

                    // Cv URL'sini oluştur ve modele ata
                    model.CvUrl = Path.Combine("/cv", model.CvFile.FileName).Replace("\\", "/");
                }


                value.Title = model.Title;
                value.ImageUrl = model.ImageUrl;
                value.Description = model.Description;
                value.CvUrl = model.CvUrl;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}