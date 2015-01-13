using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogrum.Core.Services.Posts;
using Blogrum.Core.Models.Posts;

namespace Blogrum.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        #region Properties

        protected readonly ICategoryService _categoryService;

        #endregion

        #region Ctor

        public DashboardController(
            ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

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

        #region Categories

        public ActionResult Categories()
        {
            return View();
        }

        public JsonResult GetCategoryData()
        {
            var model = _categoryService.GetCategoryList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCategoryData(CategoryListModel model)
        {
            var result = new AjaxJsonResult();

            if (ModelState.IsValid)
            {
                _categoryService.SaveCategoryList(model);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            result.AddError("There was an error saving categories.");

            return Json(result, JsonRequestBehavior.AllowGet);
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