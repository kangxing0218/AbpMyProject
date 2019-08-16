using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.SellorderDes;
using YTMyprocte.Sells;
using YTMyprocte.Web.Models.SellOrders;

namespace YTMyprocte.Web.Mvc.Controllers
{
    public class SellOrdersController : YTMyprocteControllerBase
    {
        private readonly ISellAppService _sellAppService;
        private readonly ISellOrderDeAppService _sellOrderAppService;

        public SellOrdersController(ISellAppService outboundAppService,
           ISellOrderDeAppService outboundOrderAppService)
        {
            _sellAppService = outboundAppService;
            _sellOrderAppService = outboundOrderAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _sellAppService.GetOrderForEdit(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrEditSellOrderViewModel(output);

            return PartialView("CreateOrEditModal", viewModel);
        }

        public async Task<ActionResult> OrderDetails(string orderCode)
        {
            var output = _sellAppService.GetOrderDetails(orderCode);
            var viewModel = new OrderDetailListViewModel(output);
            return View(viewModel);
        }
    }
}