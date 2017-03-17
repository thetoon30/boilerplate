using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using InternalModule.Boilerplate.Models;

namespace InternalModule.Boilerplate.Controllers.Api
{
    [Authorize]
    public class BaseController : ApiController
    {
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationUserManager _userManager;
        public BaseController(ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager?? Request.GetOwinContext().Get<RoleManager<IdentityRole>>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        protected UserViewModel CurrentUser
        {
            get
            {
                
                if (AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    var user = UserManager.FindById(userId);
                    var role = UserManager.GetRoles(userId); //rolename
                    //var rolename = RoleManager.FindById(role[0]);
                    return new UserViewModel
                    {
                        userId = userId,
                        Role = role[0],
                    };
                }

                return null;
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}
