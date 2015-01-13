using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogrum.Areas.Admin.Controllers
{
    public abstract class BaseAdminController : Controller
    {
        #region Properties

        #endregion

        #region Ctor

        #endregion

        #region Methods

        #endregion

        #region Utilities

        protected class AjaxJsonResult
        {
            public IList<string> Errors { get; set; }

            public AjaxJsonResult()
            {
                this.Errors = new List<string>();
            }

            public bool Success
            {
                get
                {
                    return this.Errors.Count == 0;
                }
            }

            public void AddError(string error)
            {
                this.Errors.Add(error);
            }
        }

        #endregion
    }
}