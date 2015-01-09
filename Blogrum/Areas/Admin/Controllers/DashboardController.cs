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
            string fileName = "";
            try
            {
                foreach (string name in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[name];
                    fileName = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            if (success)
                return Json(new { Message = fileName });

            return Json(new { Message = "Error in saving file." });
        }

        #endregion

        #endregion

        #region Utilities

        #endregion
    }
}