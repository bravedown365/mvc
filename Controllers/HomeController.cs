using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string search = "", int page = 1, string sortGia = null)
        {
            Database1Entities db = new Database1Entities();

            List<Product> pd = db.Product.Where(row => row.Name.Contains(search)).ToList();

            // Sort
            ViewBag.Sort = sortGia;
            if (sortGia == "tang")
            {
                pd = pd.OrderBy(x => x.Price).ToList();
            }
            else if (sortGia == "giam")
            {
                pd = pd.OrderByDescending(x => x.Price).ToList();
            }

            // Paging
            int numPerPage = 4;
            int numPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pd.Count) / Convert.ToDouble(numPerPage)));
            int numSkip = (page - 1) * numPerPage;

            ViewBag.Page = page;
            ViewBag.NumPages = numPages;

            pd = pd.Skip(numSkip).Take(numPerPage).ToList();

            return View(pd);
        }

        public ActionResult Detail(int ID)
        {
            Database1Entities db = new Database1Entities();

            Product pd = db.Product.Where(row => row.Id == ID).FirstOrDefault();

            Brand br = db.Brand.Where(row => row.Id == pd.Id).FirstOrDefault();
            ViewBag.Brand = br.Name;

            return View(pd);
        }
    }
}