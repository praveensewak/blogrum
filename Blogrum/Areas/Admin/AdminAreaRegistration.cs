using System.Web.Mvc;
using LowercaseRoutesMVC4;

namespace Blogrum.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRouteLowercase(
                "AdminCategories",
                "Admin/categories",
                new { controller = "Dashboard", action = "Categories", id = UrlParameter.Optional },
                new[] { "Blogrum.Areas.Admin.Controllers" }
            );

            context.MapRouteLowercase(
                "AdminEditor",
                "Admin/editor/{id}",
                new { controller = "Dashboard", action = "Editor", id = UrlParameter.Optional },
                new[] { "Blogrum.Areas.Admin.Controllers" }
            );

            context.MapRouteLowercase(
                "AdminUpdate",
                "Admin/imgupload",
                new { controller = "Dashboard", action = "Upload", id = UrlParameter.Optional },
                new[] { "Blogrum.Areas.Admin.Controllers" }
            );

            context.MapRouteLowercase(
                "Admin",
                "Admin",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new[] { "Blogrum.Areas.Admin.Controllers" }
            );
        }
    }
}