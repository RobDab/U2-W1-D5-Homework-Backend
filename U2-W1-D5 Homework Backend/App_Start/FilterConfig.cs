﻿using System.Web;
using System.Web.Mvc;

namespace U2_W1_D5_Homework_Backend
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
