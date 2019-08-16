using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YTMyprocte.StoreManagers.Dto
{
    public class CreateOrUpdateStoreManagerInput
    {
        public StoreManagerEditDto storeManager { get; set; }
    }
}
