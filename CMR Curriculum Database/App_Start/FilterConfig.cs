﻿using System.Web;
using System.Web.Mvc;

namespace CMR_Curriculum_Database
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
