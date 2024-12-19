using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();

        private void CategoryDropDown()
        {

            var categoryList = db.TblCategories.ToList();
            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.CategoryId.ToString()
                                               }).ToList();
            ViewBag.categories = categories;

        }

        public ActionResult Index()
        {
            var projects = db.TblProjects.ToList();
            return View(projects);
        }

        //Ilist,IEnumerable,ICollection ve sadece List araştır
        [HttpGet]

        public ActionResult CreateProject()
        {
            CategoryDropDown();


            return View();
        }


        /*Parametre olan TblProject a(derste model yazmıştık) kısmında model binding gerçekleşiyor
        Formdan gönderilen veri a daki kısımlar ve TblProject deki kısımlara bağlanıyor
        BUrdaki alanlarda TblProject Db nesnesindeki alanlar için girdiğimiz
        [Required()] veya [MaxLength()] gibi attribute classlara göre hata verir veya vermez
        */
        [HttpPost]
        public ActionResult CreateProject(TblProject a)

        {
            CategoryDropDown(); 
            //değilin değili var if parantezinde, modelstate.isvalid 0 ise ! ile 1 olur ve if süslü parantezine girer
            //isterler karşılandı ise modelstate.isvalid 1 olur, ! yüzünden parantez 0 olur, if süslüsüne girmez
            if (!ModelState.IsValid)
            {

                //form doğrulama hataları varsa, return View(a); ile kullanıcıya aynı form
                //girdikleri verilerle geri döndürülür ve hatalar gösterilir.
                return View(a);
            }
            db.TblProjects.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult DeleteProject(int id)
        {

            var value = db.TblProjects.Find(id);
            db.TblProjects.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateProject(int id)
        {
            CategoryDropDown();
            var value = db.TblProjects.Find(id);
            return View(value);

        }

        [HttpPost]
        public ActionResult UpdateProject(TblProject model)
        {

            CategoryDropDown();
            var value = db.TblProjects.Find(model.ProjectId);
            value.Name = model.Name;
            value.ImageUrl = model.ImageUrl;
            value.Description = model.Description;
            value.CategoryId = model.CategoryId; 
            value.GithubUrl = model.GithubUrl;

            if (!ModelState.IsValid)
            {
                return View(model);

            }

            db.SaveChanges();
            return RedirectToAction("index");

        }

        //create ve update de parametrede binding var

        /*create ve update formu aynı kod kullanılıyor ancak update de hiddenfor ile db id si sayfada saklanması için
        tek satır ekleme var*/

        //delete httpget ve http post almaz

        //delete in sayfası yok,index e atar sadece

        //update ve create de model class olarak

        //index de list olarak model

        /*MVC'de View'deki form kodu, model binding aracılığıyla 
        hem yeni veri kaydetmek hem de mevcut veriyi güncellemek için 
        çift yönlü olarak kullanılıyor. Bu, model binding'in esnekliğini 
        ve MVC mimarisinin model ile veriyi çift yönlü ilişkilendirme yeteneğini gösterir.*/
    }
}