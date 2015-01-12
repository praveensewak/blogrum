using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogrum.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        #region Properties

        #endregion

        #region Ctor

        #endregion

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Editor(int? id)
        {
            return View();
        }

        #region Image Uploader

        [HttpPost]
        public ActionResult ImageUpload()
        {
            bool success = true;
            string filename = "",
                imagePath = "";
            try
            {
                foreach (string name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[name];
                    filename = CleanFilename(file.FileName);

                    if (file != null && file.ContentLength > 0)
                    {
                        string year = DateTime.Now.Year.ToString("0000");
                        string month = DateTime.Now.Month.ToString("00");

                        var mediaDirectory = new DirectoryInfo(string.Format("{0}media", Server.MapPath(@"\")));
                        string imageDirectory = Path.Combine(mediaDirectory.ToString(), string.Format("images\\{0}\\{1}", year, month));
                        
                        if (!Directory.Exists(imageDirectory))
                            Directory.CreateDirectory(imageDirectory);

                        var path = string.Format("{0}\\{1}", imageDirectory, filename);

                        file.SaveAs(path);

                        imagePath = string.Format("/media/images/{0}/{1}/{2}", year, month, filename);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            if (success)
                return Json(new { path = imagePath });

            return Json(new { message = "Error in saving file.", path = "" });
        }

        #endregion

        #endregion

        #region Utilities

        private static string CleanFilename(string filename)
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        private static bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        #endregion
    }
}