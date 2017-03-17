using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalModule.Boilerplate.Controllers
{
    public class GalleryController : Controller
    {

        public ActionResult BasicGallery()
        {
            return View();
        }

        public ActionResult BootstrapCarusela()
        {
            return View();
        }
	}
}