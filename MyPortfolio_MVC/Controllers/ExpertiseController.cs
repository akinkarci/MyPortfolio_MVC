using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ExpertiseController : Controller
    {
       
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblExpertises.ToList();
            return View(values);
        }

        public ActionResult Delete(int id)
        {

            var value = db.TblExpertises.Find(id);
            db.TblExpertises.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(TblExpertis model) 
        {

            var value = db.TblExpertises.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        
        }



        [HttpGet]
        public ActionResult Update(int id)
        {
            var value = db.TblExpertises.Find(id);
            return View(value);
        }


        [HttpPost]
        public ActionResult Update(TblExpertis model) 
        {
            var value = db.TblExpertises.Find(model.ExpertiseId);

            value.Title = model.Title;

            db.SaveChanges();

            return RedirectToAction("Index");
        
        
        }



    }
}