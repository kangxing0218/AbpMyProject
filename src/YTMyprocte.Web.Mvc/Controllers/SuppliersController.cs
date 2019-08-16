using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.Suppliers;
using YTMyprocte.Web.Models.Suppliers;

namespace YTMyprocte.Web.Mvc.Controllers
{
    public class SuppliersController : YTMyprocteControllerBase
    {
        private readonly ISupplierAppService _supplierService;

        public SuppliersController(ISupplierAppService customerAppService)
        {
            _supplierService = customerAppService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateOrUpdateModal(int? id)
        {
            var output = await _supplierService.GetSupplierOrEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrEditSupplierViewModel(output);

            return PartialView("CreateOrUpdateModal", viewModel);
        }
    }
}