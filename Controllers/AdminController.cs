using eshop.DAL;
using eshop.Models;
using eshop.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eshop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin



        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.categoryId.ToString(), Text = item.categoryName });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categories()
        {
            List<category> allcategories = _unitOfWork.GetRepositoryInstance<category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(category tbl)
        {
            _unitOfWork.GetRepositoryInstance<category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<product>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<product>().GetFirstorDefault(productId));
        }
        [HttpPost]
        public ActionResult ProductEdit(product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.productImg = file != null ? pic : tbl.productImg;
            
            _unitOfWork.GetRepositoryInstance<product>().Update(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.productImg = pic;
            
            _unitOfWork.GetRepositoryInstance<product>().Add(tbl);
            return RedirectToAction("Product");
        }
    }
}