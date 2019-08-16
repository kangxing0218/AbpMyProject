using Abp.Application.Services.Dto;

namespace YTMyprocte.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

