using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrustRadhe
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var cctype = context.Request.ContentLength;
            var uploadFileSize = context.Request.ContentLength;

            string co = DateTime.Now.ToString("ddmmyyyyHHmm");
            if (uploadFileSize > 1)
            {
                context.Response.Write("HttpPostedFile.ContentLength!");
                //return;
            }
            if (context.Request.Files.Count > 0) // always Zero..??
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fname = context.Server.MapPath("~/KYCDocuments/" + co + file.FileName);
                    file.SaveAs(fname);
                }
                context.Response.ContentType = "application/x-zip-compressed";
                context.Response.Write("file uploaded successfully!");
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}