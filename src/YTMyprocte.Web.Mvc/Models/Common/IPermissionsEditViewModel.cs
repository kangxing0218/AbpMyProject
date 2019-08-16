using System.Collections.Generic;
using YTMyprocte.Roles.Dto;

namespace YTMyprocte.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}