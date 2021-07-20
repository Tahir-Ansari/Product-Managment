using ProductManagement.DAL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        ProductManageContext db = new ProductManageContext();
        // GET: Product
        public ActionResult Index()
        {
            var model = db.Products.ToList();
            return View(model);
        }
        public ActionResult AddProduct()
        {
            ViewBag.Categories = db.Categories.ToList().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product prod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                #region File upload
                var filePath = ConfigurationManager.AppSettings["uploadProductImage"];
                filePath = HttpContext.Server.MapPath(filePath);
                var guid = Guid.NewGuid();
                var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}{Path.GetExtension(file.FileName)}";
                var path = Upload(file, filePath, fileName);
                #endregion

                prod.ImagePath = fileName;

                if (prod.Type == Models.Type.Physical)
                {
                    //Physical product shipping cost calculation formulas = weight * 50
                    //(here weight can be actual weight or volumetric weight whichever is higher)
                    //Volumetric weight formula = (length * width * height)/5000

                    var tempWeight = prod.Weight.Value;
                    var volumetricWeight = (prod.Length.Value * prod.Width.Value * prod.Height.Value) / 5000;
                    if (tempWeight > volumetricWeight)
                    {
                        prod.ShippingCost = tempWeight * 50;
                    }
                    else
                    {
                        prod.ShippingCost = volumetricWeight * 50;
                    }

                }
                else
                {
                    //Virtual Product shipping cost - Rs. 50 or 10% of selling price whichever is higher.
                    var percentAmount = (prod.SellingPrice * 10) / 100;
                    if (percentAmount > 50)
                    {
                        prod.ShippingCost = 50;
                    }
                    else
                    {
                        prod.ShippingCost = percentAmount;
                    }
                }

                db.Products.Add(prod);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

                return View(prod);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public ActionResult EditProduct(Product prod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var model = db.Products.Where(x => x.ID == prod.ID).FirstOrDefault();
                model.Name = prod.Name;
                model.Description = prod.Description;
                model.SKU = prod.SKU;
                model.SellingPrice = prod.SellingPrice;
                model.Length = prod.Length;
                model.Weight = prod.Weight;
                model.Height = prod.Height;
                model.Width = prod.Width;
                model.AvailableQuantity = prod.AvailableQuantity;
                model.CategoryId = prod.CategoryId;
                model.Type = prod.Type;


                if (prod.Type == Models.Type.Physical)
                {
                    //Physical product shipping cost calculation formulas = weight * 50
                    //(here weight can be actual weight or volumetric weight whichever is higher)
                    //Volumetric weight formula = (length * width * height)/5000

                    var tempWeight = prod.Weight.Value;
                    var volumetricWeight = (prod.Length.Value * prod.Width.Value * prod.Height.Value) / 5000;
                    if (tempWeight > volumetricWeight)
                    {
                        model.ShippingCost = tempWeight * 50;
                    }
                    else
                    {
                        model.ShippingCost = volumetricWeight * 50;
                    }

                }
                else
                {
                    //Virtual Product shipping cost - Rs. 50 or 10% of selling price whichever is higher.
                    var percentAmount = (prod.SellingPrice * 10) / 100;
                    if (percentAmount > 50)
                    {
                        model.ShippingCost = 50;
                    }
                    else
                    {
                        model.ShippingCost = percentAmount;
                    }
                }

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

                return View(prod);
            }

            return RedirectToAction("Index", "Product");
        }

        public ActionResult DeleteProduct(int id)
        {
            var model = db.Products.Where(x => x.ID == id).FirstOrDefault();

            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;

            try
            {
                db.SaveChanges();
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public string Upload(HttpPostedFileBase file, string filePath, string fileName)
        {
            bool exists = Directory.Exists(filePath);

            if (!exists)
                Directory.CreateDirectory(filePath);
            if (file.ContentLength > 0)
            {
                var path = Path.Combine(filePath, fileName);

                Stream strm = file.InputStream;
                file.SaveAs(path);
                return path;
            }
            return null;
        }
    }
}