using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.SellorderDes.Dto;
using YTMyprocte.Sells.Dto;

namespace YTMyprocte.Sells
{
    public interface ISellAppService : IApplicationService
    {
        Task<PagedResultDto<SellListDto>> GetOrdersAsync(GetSellInput input);

        OutSellDeListDto GetOrderDetails(string orderCode);
        Task updateOutboundStatus(EntityDto input);//出库
        Task<OutSellEditDto> GetOrderForEdit(NullableIdDto input);

        Task<PagedResultDto<SellOrderDeListDto>> GetSellOrder(NullableIdDto input);
        Task CreateOrUpdateOrder(CreateOrUpdateOutOrderInput input);

       

        Task DeleteOrder(EntityDto input);
    }
}
