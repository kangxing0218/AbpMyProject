using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using YTMyprocte.PurchaseAndSale.Materiels;
using YTMyprocte.PurchaseAndSale.StoreManagers;
using YTMyprocte.StoreManagers.Dto;

namespace YTMyprocte.StoreManagers
{
    public class StoreManagerAppService : YTMyprocteAppServiceBase, IStoreManagerAppService
    {
        //注入
        private readonly IRepository<StoreManager, int> _storeManagerRepository;
        private readonly IRepository<Materiel, int> _materielRepository;
        public StoreManagerAppService(IRepository<StoreManager, int> storeManagerRepository, IRepository<Materiel, int> materielRepository)
        {
            _materielRepository = materielRepository;
            _storeManagerRepository = storeManagerRepository;
        }
        public async Task CreateOrUpdateStoreManagerAsync(CreateOrUpdateStoreManagerInput input)
        {
            if (input.storeManager.Id.HasValue)
            {
                await UpdateStoreManagerAsync(input.storeManager);
            }
            else
            {
                await CreateStoreManagerAsync(input.storeManager);
            }
        }

        private async Task CreateStoreManagerAsync(StoreManagerEditDto input)
        {
            var aa = input.MapTo<StoreManager>();
            await _storeManagerRepository.InsertAsync(aa);
        }

        private async Task UpdateStoreManagerAsync(StoreManagerEditDto input)
        {
            var entity = await _storeManagerRepository.GetAsync(input.Id.Value);
            //double aa = Convert.ToDouble(entity.CurrentAmount);
            //double bb = Convert.ToDouble(entity.MaterielMoney);
            //entity.CountMoney = aa * bb;
            await _storeManagerRepository.UpdateAsync(input.MapTo(entity));

        }

       
        public async Task DeleteStoreManager(EntityDto input)
        {
            var delStore = await _storeManagerRepository.GetAsync(input.Id);
            if (delStore == null)
            {
                throw new UserFriendlyException("这个数据不存在");
            }
            await _storeManagerRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<StoreManagerListDto>> GetPagedStoreManagerAsync(GetStoreManagerInput input)
        {
            //得到库存中的全部
            var query =  _storeManagerRepository.GetAll();
            var storeAccount = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dots = list.MapTo<List<StoreManagerListDto>>();
            foreach (var item in dots)
            {
                var materiel = await _materielRepository.GetAsync(item.MaterielId);
                item.MaterielName = materiel.WaresName;
                item.MaterielUnit = materiel.WaresUnit;
                item.MaterielMoney =  materiel.BuyMoney;
                
            }
            return new PagedResultDto<StoreManagerListDto>(storeAccount, dots);
        }

        public async Task<StoreManagerListDto> GetStoreManagerByIdAsync(NullableIdDto input)
        {
            var store = await _storeManagerRepository.GetAsync(input.Id.Value);
            return store.MapTo<StoreManagerListDto>();
        }

        public async Task<StoreManagerEditDto> GetStoreManagerOrEditAsync(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var store = await _storeManagerRepository.GetAsync(input.Id.Value);
                var dto = store.MapTo<StoreManagerEditDto>();
                return dto;
            }
            else
            {
                return new StoreManagerEditDto();
            }
        }
    }
}
