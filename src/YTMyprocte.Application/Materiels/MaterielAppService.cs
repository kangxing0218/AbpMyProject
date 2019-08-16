using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.Materiels.Dto;
using YTMyprocte.PurchaseAndSale.Materiels;

namespace YTMyprocte.Materiels
{
    public class MaterielAppService:YTMyprocteAppServiceBase, IMaterielAppService
    {
        //注入
        private readonly IRepository<Materiel, int> _materielRepository;
        public MaterielAppService(IRepository<Materiel, int> materielRepository)
        {
            _materielRepository = materielRepository;
        }

        public async Task CreateOrUpdateMaterielAsync(CreateOrUpdateMaterielInput input)
        {
            if (input.materielEditDto.Id.HasValue)
            {
                await UpdateMaterielAsync(input.materielEditDto);
            }
            else{
                await CreateMaterielAsync(input.materielEditDto);
            }
        }

        private async Task CreateMaterielAsync(MaterielEditDto input)
        {
            var aa = input.MapTo<Materiel>();
            await _materielRepository.InsertAsync(aa);
        }

        private async Task UpdateMaterielAsync(MaterielEditDto input)
        {
            var entity = await _materielRepository.GetAsync(input.Id.Value);
            await _materielRepository.UpdateAsync(input.MapTo(entity));
        }

        public async Task DeleteMateriel(EntityDto input)
        {
            var entity = _materielRepository.GetAsync(input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("这个数据不存在");
            }
            await _materielRepository.DeleteAsync(input.Id);
        }

        public async Task<MaterielListDto> GetMaterielByIdAsync(NullableIdDto input)
        {
            var materiel = await _materielRepository.GetAsync(input.Id.Value);
            return materiel.MapTo<MaterielListDto>();
        }

        public async Task<MaterielEditDto> GetMaterielOrEditAsync(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var model = await _materielRepository.GetAsync(input.Id.Value);
                var dto = model.MapTo<MaterielEditDto>();
                return dto;
            }
            else
            {
                return new MaterielEditDto();
            }

        }

        public async Task<PagedResultDto<MaterielListDto>> GetPagedMaterielAsync(GetMaterielInput input)
        {
            var query = _materielRepository.GetAll();
            var materielCount = await query.CountAsync();
           var list = await query.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dots = list.MapTo<List<MaterielListDto>>();
            return new PagedResultDto<MaterielListDto>(materielCount, dots);

        }
    }
}
