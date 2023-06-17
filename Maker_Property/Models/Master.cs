using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TrustRadhe.Models;
namespace TrustRadhe.Models
{
    public class Master : Common
    {
        public string Image { get; set; }
        public List<Master> lstGallery1 { get; set; }
        public string PK_GalleryID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string BinaryPercent { get; set; }
        public string DirectPercent { get; set; }
        public string ROIPercent { get; set; }
        public List<Master> lstproduct { get; set; }

        public string NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string NewsDate { get; set; }
        public string BV { get; set; }
        public List<Master> lstNews { get; set; }

        #region ProductMaster

        public DataSet SaveProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV",BV),
                                  new SqlParameter("@AddedBy", AddedBy)};

            DataSet ds = DBHelper.ExecuteQuery("AddProduct", para);
            return ds;
        }

        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", ProductID) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }

        public DataSet SavingGalleryMaster()
        {
            SqlParameter[] para = { new SqlParameter("@AddedBy", AddedBy),
             new SqlParameter("@Image", Image)
            };
            DataSet ds = DBHelper.ExecuteQuery("GalleryMaster", para);
            return ds;
        }

        public DataSet GetGalleryList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_GalleryID", PK_GalleryID)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetGalleryImages",para);
            return ds;
        }

        public DataSet DeleteProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", ProductID),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteProduct", para);
            return ds;
        }

        public DataSet UpdateProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", ProductID),
                                  new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV", BV),
                                  new SqlParameter("@UpdatedBy", UpdatedBy)};

            DataSet ds = DBHelper.ExecuteQuery("UpdateProduct", para);
            return ds;
        }

        #endregion

        #region NewsMaster

        public DataSet SaveNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsHeading", NewsHeading),
                                  new SqlParameter("@NewsBody", NewsBody),
                                  new SqlParameter("@AddedBy", AddedBy)};

            DataSet ds = DBHelper.ExecuteQuery("AddNews", para);
            return ds;
        }

        public DataSet NewsList()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID) };
            DataSet ds = DBHelper.ExecuteQuery("NewsList", para);
            return ds;
        }

        public DataSet UpdateNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID),
                                  new SqlParameter("@NewsHeading", NewsHeading),
                                  new SqlParameter("@NewsBody", NewsBody),
                                  new SqlParameter("@UpdatedBy", UpdatedBy) };

            DataSet ds = DBHelper.ExecuteQuery("UpdateNews", para);
            return ds;
        }

        public DataSet DeleteNews()
        {
            SqlParameter[] para = { new SqlParameter("@NewsID", NewsID),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteNews", para);
            return ds;
        }

        public DataSet DeleteGalleryImages()
        {
            SqlParameter[] para = { new SqlParameter("@PK_GalleryID", PK_GalleryID),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteGallery", para);
            return ds;
        }

        #endregion

    }
}