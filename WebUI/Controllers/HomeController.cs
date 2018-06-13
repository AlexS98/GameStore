using GameStore.StoreDomain.Abstract;
using GameStore.StoreDomain.Entities;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext?.GetOwinContext().Authentication;
        private readonly IGenericRepository<UserCabinet> cabinetRepository;
        private readonly IGenericRepository<User> userRepository;

        public HomeController(IGenericRepository<UserCabinet> cabinets, IGenericRepository<User> users)
        {
            cabinetRepository = cabinets;
            userRepository = users;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.UserName = AuthenticationManager?.User.Identity.Name;
            return View();
        }

        [HttpGet]
        public ActionResult CreateCabinet(int id)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCabinet(int id, UserCabinet cabinet, HttpPostedFileBase image = null)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            cabinet.User = userRepository.FindById(id);
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    cabinet.AvatarImageMimeType = image.ContentType;
                    cabinet.AvatarImageData = new byte[image.ContentLength];
                    image.InputStream.Read(cabinet.AvatarImageData, 0, image.ContentLength);
                }
                cabinetRepository.Create(cabinet);
                return RedirectToAction("Index");
            }
            return View(cabinet);
        }

        public FileContentResult GetImage(int cabinetId)
        {
            var cab = cabinetRepository.FindById(cabinetId);
            if (cab != null)
            {
                return File(cab.AvatarImageData, cab.AvatarImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}