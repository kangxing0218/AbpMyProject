﻿using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using YTMyprocte.Controllers;

namespace YTMyprocte.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : YTMyprocteControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
