using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class WidgetController : Controller
    {
        private readonly IBookmarkService _bookmarkService;
        public WidgetController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }
        public IActionResult Index()
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Bookmark> model = _bookmarkService.GetMostRecentBookmarks(userId);
            return View(model);
        }
    }
}
