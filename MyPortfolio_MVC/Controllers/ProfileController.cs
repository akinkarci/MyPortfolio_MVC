using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ProfileController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            string email = Session["email"].ToString();
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Login");
            }
            var admin = db.TblAdmins.FirstOrDefault(x => x.Email == email);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Index(TblAdmin model)
        {
            string email = Session["email"].ToString();
            var admin = db.TblAdmins.FirstOrDefault(x => x.Email == email);


            if (admin.Password == model.Password)
            {
                if (model.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "images\\";

                    /* post ile gelen model object sinde model.ImageFile da resim dosyası example.jpg yüklü
                     * FileName metodu bu object nin propertiesindeki bu dosyanın adını açık ediyor */ 
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);

                    /* burda resim dosyası kaydedildi*/
                    model.ImageFile.SaveAs(fileName);
                    
                    /* email eşleşmesi olan kişi admindir dolayısıyla o maile sahip kişinin Imageurl sine atanır bu yol*/
                    admin.ImageUrl = "/images/" + model.ImageFile.FileName;
                    
                }
                admin.Name = model.Name;
                admin.Surname = model.Surname;
                admin.Email = model.Email;
                db.SaveChanges();
                Session.Abandon();
                return RedirectToAction("Index", "Login");

            }

            ModelState.AddModelError("", "Girdiğiniz Şifre Hatalı");
            return View(model);

        }
    }
}