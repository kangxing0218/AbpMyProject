using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.PurchaseAndSale.Suppliers;
using YTMyprocte.Suppliers.Dto;
using Abp.Linq.Extensions;

namespace YTMyprocte.Suppliers
{
    public class SupplierAppService: YTMyprocteAppServiceBase, ISupplierAppService
    {
        private readonly IRepository<Supplier, int> _supplierRepository;
        public SupplierAppService(IRepository<Supplier, int> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task CreateOrUpdateSupplierAsync(CreateOrUpdateSupplierInput input)
        {
            if (input.supplierEditDto.Id.HasValue)
            {
                await UpdateSupplierAsync(input.supplierEditDto);
            }
            else
            {
                await CreateSupplierAsync(input.supplierEditDto);
            }
            //throw new NotImplementedException();
        }

        private async Task UpdateSupplierAsync(SupplierEditDto input)
        {
            var entity = await _supplierRepository.GetAsync(input.Id.Value);

            await _supplierRepository.UpdateAsync(input.MapTo(entity));
        }

        private async Task CreateSupplierAsync(SupplierEditDto input)
        {
            var supplier = input.MapTo<Supplier>();
            await _supplierRepository.InsertAsync(supplier);
        }

        public async Task DeleteSupplier(EntityDto input)
        {
            var entity = _supplierRepository.GetAsync(input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("这个数据不存在");
            }
            await _supplierRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<SupplierListDto>> GetPagedSuppliersAsync(GetSupplierInput input)
        {
            var query = _supplierRepository.GetAll();
            var customerCount = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dtos = list.MapTo<List<SupplierListDto>>();
            return new PagedResultDto<SupplierListDto>(customerCount, dtos);
        }

        public async Task<SupplierListDto> GetSupplierByIdAsync(NullableIdDto input)
        {
            var customer = await _supplierRepository.GetAsync(input.Id.Value);
            return customer.MapTo<SupplierListDto>();
        }

        public async Task<SupplierEditDto> GetSupplierOrEditAsync(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var model = await _supplierRepository.GetAsync(input.Id.Value);
                var dto = model.MapTo<SupplierEditDto>();
                return dto;
            }
            else
            {
                return new SupplierEditDto();
            }
        }
    }
}
