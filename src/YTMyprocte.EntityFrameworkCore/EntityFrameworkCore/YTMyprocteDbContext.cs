using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using YTMyprocte.Authorization.Roles;
using YTMyprocte.Authorization.Users;
using YTMyprocte.MultiTenancy;
using YTMyprocte.PurchaseAndSale.Customers;
using YTMyprocte.PurchaseAndSale.Suppliers;
using YTMyprocte.PurchaseAndSale.Materiels;
using YTMyprocte.PurchaseAndSale.StoreManagers;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.PurchaseAndSale.Sells;

namespace YTMyprocte.EntityFrameworkCore
{
    public class YTMyprocteDbContext : AbpZeroDbContext<Tenant, Role, User, YTMyprocteDbContext>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Materiel> Materiels { get; set; }
        public DbSet<StoreManager> StoreManagers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<SellOrderDe> SellOrderDes { get; set; }

        /* Define a DbSet for each entity of the application */

        public YTMyprocteDbContext(DbContextOptions<YTMyprocteDbContext> options)
            : base(options)
        {
        }


        
    }
}
