using System;
using System.Configuration;

namespace Blogrum.Core
{
    public static class Site
    {
        public static string Version
        {
            get
            {
                string blogVersion = "0.1";
                string webConfigVersion = ConfigurationManager.AppSettings["SiteVersion"];
                return String.IsNullOrEmpty(webConfigVersion) ? blogVersion : webConfigVersion;
            }
        }

        public static string Title
        {
            get
            {
                string blogTitle = "Blogrum";
                string webConfigTitle = ConfigurationManager.AppSettings["SiteTitle"];

                return String.IsNullOrEmpty(webConfigTitle) ? blogTitle : webConfigTitle;
            }
        }
    }
}
