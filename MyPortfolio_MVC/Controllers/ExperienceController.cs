using Microsoft.Ajax.Utilities;
using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ExperienceController : Controller
    {
        
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();

        public ActionResult Index()
        {

            var values = db.TblExperiences.ToList();
            return View(values);
        }


        public ActionResult Delete(int id)
        {
            var value = db.TblExperiences.Find(id);
            db.TblExperiences.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }


        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(TblExperience model)
        {
            db.TblExperiences.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Update (int id)
        {
            var value = db.TblExperiences.Find(id);
            return View(value);

        }

        [HttpPost]
        public ActionResult Update(TblExperience model)
        {

            var value = db.TblExperiences.Find(model.ExperienceId);
            
            value.CompanyName = model.CompanyName;
            value.Description = model.Description;
            value.StartDate = model.StartDate;
            value.EndDate = model.EndDate;
            value.Title = model.Title;

            db.SaveChanges();

            return RedirectToAction("Index");


        }







    }
}