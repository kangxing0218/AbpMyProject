using Abp.Application.Navigation;
using Abp.Localization;
using YTMyprocte.Authorization;

namespace YTMyprocte.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class YTMyprocteNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Customers,
                        L("Customers"),
                        url: "Customers",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Suppliers,
                        L("Suppliers"),
                        url: "Suppliers",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Materiels,
                        L("Materiels"),
                        url: "Materiels",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.StoreManagers,
                        L("StoreManagers"),
                        url: "StoreManagers",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.PurchaseOrders,
                        L("PurchaseOrder"),
                        url: "PurchaseOrders",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.SellOrders,
                        L("SellOrder"),
                        url: "SellOrders",
                        icon: "local_offer"
                    // requiredPermissionName: PermissionNames.BasicData_Customers
                    )
                    )
                    .AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "info"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, YTMyprocteConsts.LocalizationSourceName);
        }
    }
}
