using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.Customers.Dto;
using YTMyprocte.PurchaseAndSale.Customers;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using AutoMapper.QueryableExtensions;

namespace YTMyprocte.Customers
{
    public class CustomerAppService: YTMyprocteAppServiceBase, ICustomerAppService
    {
       //注入
        private readonly IRepository<Customer,int> _customerRepository;
        private readonly CustomerManager _customerManager;
        public CustomerAppService(IRepository<Customer,int> customerRepository, CustomerManager customerManager)
        {
            _customerRepository = customerRepository;
            _customerManager = customerManager;

        }

        public async Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input)
        {
            if (input.customerEditDto.Id.HasValue)
            {
                await UpdateCustomerAsync(input.customerEditDto);
            }
            else
            {
                await CreateCustomerAsync(input.customerEditDto);
            }
          //  throw new NotImplementedException();
        }

       

        public async Task DeleteCustomer(EntityDto input)
        {
            var entity = _customerRepository.GetAsync(input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("这个数据不存在");
            }
            await _customerRepository.DeleteAsync(input.Id);
           
        }

       
        public async Task<CustomerListDto> GetCustomerByIdAsync(NullableIdDto input)
        {
         var customer = await _customerRepository.GetAsync(input.Id.Value);
            return customer.MapTo<CustomerListDto>();
        }
        //public async Task<CustomerEditDto> GetCustomerByIdAsync(NullableIdDto input)
        //{
        //    var customer = await _customerRepository.GetAsync(input.Id.Value);
        //    return customer.MapTo<CustomerListDto>();
        //}

        //查询联系人，分页
        public async Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input)
        {
            var query = _customerRepository.GetAll();
            var customerCount = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dtos = list.MapTo<List<CustomerListDto>>();
            return new PagedResultDto<CustomerListDto>(customerCount, dtos);
        }


        protected async Task UpdateCustomerAsync(CustomerEditDto input)
        {
           var flag =  await _customerManager.CheckCodeAsync(input.Id, input.Code);
            if (flag)
            {
                var entity = await _customerRepository.GetAsync(input.Id.Value);
                await _customerRepository.UpdateAsync(input.MapTo(entity));
            }
            else
            {
                throw new UserFriendlyException($"客户[{input.Code}]已存在！");
            }
        }

        protected async Task CreateCustomerAsync(CustomerEditDto input)
        {
            var flag = await _customerManager.CheckCodeAsync(null, input.Code);
            if (flag)
            {
                var aa = input.MapTo<Customer>();
                await _customerRepository.InsertAsync(aa);
            }
            else
            {
                throw new UserFriendlyException($"客户[{input.Code}]已存在！");
            }
        }



       //public async Task GetCustomerNameAsync(string customerName)
       // {
       //     var 
       // }

        public async Task<CustomerEditDto> GetCustomerOrEditAsync(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var model = await _customerRepository.GetAsync(input.Id.Value);
                var dto = model.MapTo<CustomerEditDto>();
                return dto;
            }   
            else
            {
                return new CustomerEditDto();
            }
        }
    }
}
