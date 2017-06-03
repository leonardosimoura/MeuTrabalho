using System;
using System.Configuration;
using System.IO;

namespace MT.UITests.Helpers
{
    public class ConfigurationHelper
    {
        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["SiteUrl"]; }
        }

        public static string RegisterUrl
        {
            get { return string.Format("{0}{1}", SiteUrl, ConfigurationManager.AppSettings["RegisterUrl"]); }
        }

        public static string LoginUrl
        {
            get { return string.Format("{0}{1}", SiteUrl, ConfigurationManager.AppSettings["LoginUrl"]); }
        }

        public static string ChromeDrive
        {
            get { return string.Format("{0}", Directory.GetCurrentDirectory() + @"\" + ConfigurationManager.AppSettings["ChromeDrive"]); }
        }

        public static string TestUserName
        {
            get { return ConfigurationManager.AppSettings["TestUserName"]; }
        }

        public static string TestPassword
        {
            get { return ConfigurationManager.AppSettings["TestPassword"]; }
        }

        public static string FolderPath
        {
            get { return Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())); }
        }

        public static string FolderPicture
        {
            get { return string.Format("{0}{1}", FolderPath, ConfigurationManager.AppSettings["FolderPicture"]); }
        }

        public static int BrowserType
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["BrowserType"]); }
        }
    }

    public enum BrowserType
    {
        Chrome = 1,
        Firefox = 2
    }
}
