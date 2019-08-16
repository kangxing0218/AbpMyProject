using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.Materiels;
using YTMyprocte.Web.Models.Materiels;

namespace YTMyprocte.Web.Mvc.Controllers
{
    public class MaterielsController : YTMyprocteControllerBase
    {
        private readonly IMaterielAppService _materielService;

        public MaterielsController(IMaterielAppService materielAppService)
        {
            _materielService = materielAppService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> CreateOrUpdateModal(int? id)
        {
            var output = await _materielService.GetMaterielOrEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrEditMaterielViewModel(output);

            return PartialView("CreateOrUpdateModal", viewModel);
        }
    }
}