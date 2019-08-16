using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YTMyprocte.PurchaseAndSale.Customers
{
    public class CustomerManager: DomainService
    {
        private readonly IRepository<Customer, int> _customerRespository;

        public CustomerManager(IRepository<Customer, int> customerRespository)
        {
            _customerRespository = customerRespository;
        }

        public async Task<bool> CheckCodeAsync(long? id, string code)
        {
            var flag = await _customerRespository.FirstOrDefaultAsync(x => x.Code == code);
            if (flag == null)
            {
                return true;
            }
            else
            {
                if (id.HasValue)
                {
                    if (flag.Id == id.Value)
                    {
                        return true;
                    }
                }
                return false;
            }

        }
    }
}
