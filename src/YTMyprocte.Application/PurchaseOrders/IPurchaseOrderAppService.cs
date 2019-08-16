using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.PurchaseOrderDetails.Dto;
using YTMyprocte.PurchaseOrders.Dto;

namespace YTMyprocte.PurchaseOrders
{
    public interface IPurchaseOrderAppService:IApplicationService
    {
        //分页
        Task<PagedResultDto<PurchaseOrderListDto>> GetPagedOrdersAsync(GetPurchaseOrderInput input);

        PurchaseOrderDetailListDto GetPurchaseOrderDet(string orderCode);

        
        Task UpdateInboundStatusUpdateOrder(EntityDto input);//入库
        //创建或修改
        Task CreateOrUpdateOrderAsync(CreateOrUpdatePurchaseOrderInput input);
        //删除
        Task DeletePurchaseOrder(EntityDto input);
        Task<OrderEditDto> GetOrderForEditAsync(NullableIdDto input);

        Task<PagedResultDto<PurchaseOrederDeListDto>> GetPagedPurchaseDetail(NullableIdDto input);

        
        
    }
}
