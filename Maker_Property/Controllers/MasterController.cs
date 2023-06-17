using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using System.IO;

namespace TrustRadhe.Controllers
{
    public class MasterController : Controller
    {
        #region ProductMaster
        public ActionResult ProductList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["Pk_ProductId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductPrice = r["ProductPrice"].ToString();
                    obj.IGST = r["IGST"].ToString();
                    obj.CGST = r["CGST"].ToString();
                    obj.SGST = (r["SGST"].ToString());
                    obj.BinaryPercent = (r["BinaryPercent"].ToString());
                    obj.DirectPercent = (r["DirectPercent"].ToString());
                    obj.ROIPercent = (r["ROIPercent"].ToString());
                    obj.BV = (r["BV"].ToString());

                    lst.Add(obj);
                }
                model.lstproduct = lst;
            }
            return View(model);
        }

        public ActionResult DeleteProduct(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.ProductID = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Product"] = "Product deleted successfully";
                        FormName = "ProductList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Product"] = ex.Message;
                FormName = "ProductList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ProductMaster(string productID)
        {
            if (productID != null)
            {
                Master obj = new Master();
                try
                {
                    obj.ProductID = productID;

                    DataSet ds = obj.ProductList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.ProductID = ds.Tables[0].Rows[0]["Pk_ProductId"].ToString();
                        obj.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        obj.ProductPrice = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
                        obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                        obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                        obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                        obj.BinaryPercent = ds.Tables[0].Rows[0]["BinaryPercent"].ToString();
                        obj.DirectPercent = ds.Tables[0].Rows[0]["DirectPercent"].ToString();
                        obj.ROIPercent = ds.Tables[0].Rows[0]["ROIPercent"].ToString();
                        obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Product"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult SaveProduct(string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent,string BV)
        {
            Master obj = new Master();
            try
            {
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.AddedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.SaveProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product saved successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProduct(string ProductID, string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent,string BV)
        {
            Master obj = new Master();
            try
            {
                obj.ProductID = ProductID;
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.UpdatedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.UpdateProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
              
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NewsMaster

        public ActionResult AddNews(string NewsID)
        {
            if (NewsID != null)
            {
                Master obj = new Master();
                try
                {
                    obj.NewsID = NewsID;

                    DataSet ds = obj.NewsList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.NewsID = ds.Tables[0].Rows[0]["PK_NewsID"].ToString();
                        obj.NewsDate = ds.Tables[0].Rows[0]["NewsDate"].ToString();
                        obj.NewsHeading = ds.Tables[0].Rows[0]["NewsHeading"].ToString();
                        obj.NewsBody = ds.Tables[0].Rows[0]["NewsBody"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["News"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
        }

        public ActionResult SaveNews(string NewsHeading, string NewsBody)
        {
            Master obj = new Master();
            try
            {
                obj.NewsHeading = NewsHeading;
                obj.NewsBody = NewsBody;
                obj.AddedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.SaveNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "News saved successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNews(string NewsID, string NewsHeading, string NewsBody)
        {
            Master obj = new Master();
            try
            {
                obj.NewsID = NewsID;
                obj.NewsHeading = NewsHeading;
                obj.NewsBody = NewsBody;
                obj.UpdatedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.UpdateNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "News updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewsList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.NewsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.NewsID = r["PK_NewsID"].ToString();
                    obj.NewsDate = r["NewsDate"].ToString();
                    obj.NewsHeading = r["NewsHeading"].ToString();
                    obj.NewsBody = r["NewsBody"].ToString();
                    
                    lst.Add(obj);
                }
                model.lstNews = lst;
            }
            return View(model);
        }

        public ActionResult DeleteNews(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.NewsID = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Product"] = "News deleted successfully";
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Product"] = ex.Message;
                FormName = "NewsList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region GalleryMaster
        
        public ActionResult GalleryMaster(Master model)
        {
            return View(model);
        }


        [HttpPost]
        [OnAction(ButtonName = "btnSave")]
        [ActionName("GalleryMaster")]
        public ActionResult SaveGalleryMaster(Master model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.Image = "/images/GalleryImages/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                }

                DataSet ds = model.SavingGalleryMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["GalleryMaster"] = "Image Saved Successully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["GalleryMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                TempData["GalleryMaster"] = ex.Message;
            }
            return RedirectToAction("GalleryMaster");
        }



        public ActionResult DeleteImage(string PK_GalleryID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PK_GalleryID = PK_GalleryID;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteGalleryImages();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GalleryMasterList"] = "Image deleted successfully";
                        FormName = "GalleryMasterList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["GalleryMasterList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GalleryMasterList"] = ex.Message;
            }

            return RedirectToAction(FormName,Controller);
        }
        #endregion
    }
}
