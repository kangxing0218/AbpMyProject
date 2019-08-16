using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.StoreManagers;
using YTMyprocte.Web.Models.StoreManagers;

namespace YTMyprocte.Web.Mvc.Controllers
{
    public class StoreManagersController : YTMyprocteControllerBase
    {
        private readonly IStoreManagerAppService _storeManagerService;

        public StoreManagersController(IStoreManagerAppService storeManagerAppService)
        {
            _storeManagerService = storeManagerAppService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateOrUpdateModal(int? id)
        {

            var output = await _storeManagerService.GetStoreManagerOrEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrUpdateStoreManagerViewModel(output);

            return PartialView("CreateOrUpdateModal", viewModel);
        }

    }
}