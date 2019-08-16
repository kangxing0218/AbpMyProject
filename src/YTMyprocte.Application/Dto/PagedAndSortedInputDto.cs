using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YTMyprocte.Dto
{
    public class PagedAndSortedInputDto : IPagedResultRequest, ISortedResultRequest
    {
        public string Sorting { get; set; }
        [Range(0, int.MaxValue)]//跳转数量
        public int SkipCount { get; set; }
        [Range(1,500)]//每页多少条
        public int MaxResultCount { get; set; }
    }
}
