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
        public ActionResult Upload()
        {
            bool success = true;
            string filename = "",
                path = "";
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
                        string pathString = Path.Combine(mediaDirectory.ToString(), string.Format("images\\{0}\\{1}", year, month));
                        
                        if (!Directory.Exists(pathString))
                            Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, filename);

                        // check file exists
                        int count = 1;
                        string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                        string extension = Path.GetExtension(path);
                        string directoryPath = Path.GetDirectoryName(path);

                        while (FileExists(path))
                        {
                            string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                            filename = tempFileName + extension;
                            path = Path.Combine(directoryPath, filename);
                        }

                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            if (success)
                return Json(new { path = path });

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