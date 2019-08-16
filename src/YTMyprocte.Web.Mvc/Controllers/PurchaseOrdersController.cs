using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.PurchaseOrders;
using YTMyprocte.Web.Models.PurchaseOrders;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YTMyprocte.Web.Controllers
{
    public class PurchaseOrdersController : YTMyprocteControllerBase
    {
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;

        public PurchaseOrdersController(IPurchaseOrderAppService purchaseOrderAppService)
        {
            _purchaseOrderAppService = purchaseOrderAppService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateOrUpdateModal(int? id)
        {
            var output = await _purchaseOrderAppService.GetOrderForEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrEditOrderViewModel(output);

            return PartialView("CreateOrUpdateModal", viewModel);

        }
        //public async Task<PartialViewResult> SelctMaterials(int? id)
        //{
        //    var output = await _purchaseOrderAppService.GetOrderForEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
        //    var viewModel = new CreateOrEditOrderViewModel(output);

        //    return PartialView("CreateOrUpdateModal", viewModel);

        //}


        public async Task<ActionResult> OrderDetails(string orderCode)
        {
            var output =  _purchaseOrderAppService.GetPurchaseOrderDet(orderCode);
            var viewModel = new OrderDetailViewModel(output);

            return View(viewModel);

        }
    }
}
