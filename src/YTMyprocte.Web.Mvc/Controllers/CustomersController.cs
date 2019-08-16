using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using YTMyprocte.Controllers;
using YTMyprocte.Customers;
using YTMyprocte.Customers.Dto;
using YTMyprocte.PurchaseAndSale.Customers;
using YTMyprocte.Web.Models.Customers;

namespace YTMyprocte.Web.Mvc.Controllers
{
    public class CustomersController: YTMyprocteControllerBase
    {
        private readonly ICustomerAppService _customerService;
        private readonly IRepository<Customer, int> _customerRepository;
        public CustomersController(ICustomerAppService customerAppService, IRepository<Customer, int> customerRepository)
        {
            _customerService = customerAppService;
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckName(string customerName)
        {
           var f = _customerRepository.FirstOrDefault(x => x.CustomerName == customerName);
            if (f != null)
            {
                return Json($"用户名{customerName}已经存在");
            }
            return Json(true);
        }
        

        
        public async Task<PartialViewResult> CreateOrUpdateModal(int? id)
        {
            var output = await _customerService.GetCustomerOrEditAsync(new Abp.Application.Services.Dto.NullableIdDto { Id = id });
            var viewModel = new CreateOrUpdateCustomerViewModel(output);

            return PartialView("CreateOrUpdateModal", viewModel);
        }

        
        


    }

}