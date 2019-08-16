using Microsoft.AspNetCore.Antiforgery;
using YTMyprocte.Controllers;

namespace YTMyprocte.Web.Host.Controllers
{
    public class AntiForgeryController : YTMyprocteControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
