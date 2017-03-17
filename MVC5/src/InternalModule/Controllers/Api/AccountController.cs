using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Description;
using System.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;
using InternalModule.Boilerplate.EF.Repository;
using InternalModule.Boilerplate.Core;
using InternalModule.Boilerplate.Models;
using InternalModule.Boilerplate.Results;
using InternalModule.Boilerplate.Providers;
using InternalModule.Boilerplate.Core.Model;
using Facebook;
using System.Net;
using TweetSharp;
using System.Configuration;
using System.Dynamic;
using InternalModule.Boilerplate.Models.ResponseMessage;
using Microsoft.Owin.Security.DataProtection;
using InternalModule.Boilerplate.Helpers;

namespace InternalModule.Boilerplate.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        private const string LocalLoginProvider = "Local";
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, UserRepository userRepository, IUnitOfWork unitOfWork) : base(userManager, roleManager)
        {
            AccessTokenFormat = accessTokenFormat;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userdetail = _userRepository.FindByName(User.Identity.GetUserName());
            return new UserInfoViewModel
            {
                UserName = User.Identity.GetUserName(),
                Role = CurrentUser.Role,
                FirstName = userdetail.FirstName,
                LastName = userdetail.LastName,
                EmployeeCode = userdetail.EmployeeCode,
                Email = userdetail.Email,
                PhoneNumber = userdetail.PhoneNumber,
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        [HttpPost]
        public IHttpActionResult PostUserInfo([FromBody] Device device)
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            if (externalLogin == null)
            {
                var userdetail = _userRepository.FindByName(User.Identity.GetUserName());
                if (userdetail.LockoutEnabled == true)
                {
                    return BadRequest();
                }
                if (device != null)
                {
                    userdetail.DeviceId = device.deviceId;
                    userdetail.oneSignalDeviceId = device.oneSignalDeviceId;
                    _unitOfWork.Commit();
                }

                return Ok(new UserInfoViewModel
                {
                    UserName = User.Identity.GetUserName(),
                    Role = CurrentUser.Role,
                    FirstName = userdetail.FirstName,
                    LastName = userdetail.LastName,
                    EmployeeCode = userdetail.EmployeeCode,
                    Email = userdetail.Email,
                    PhoneNumber = userdetail.PhoneNumber,
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                });
            }
            else if (externalLogin.LoginProvider != "Twitter")
            {
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                var access_token = identity.FindFirstValue("FacebookAccessToken");
                var client = new FacebookClient(access_token);
                dynamic result = client.Get("me", new { fields = "name,id,email,first_name,last_name" });
                //string path = friend_id + "/feed";   
                //var loginInfo = AuthenticationManager.GetExternalLoginInfoAsync();
                //var claimId = loginInfo.Result.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:id");
                //var claimFirstName = loginInfo.Result.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:first_name");
                //var claimLastName = loginInfo.Result.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:last_name");
                //var claimEmailName = loginInfo.Result.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:email");
                //var claimUserName = loginInfo.Result.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:name");
                //meassage only
                var parameters = new Dictionary<string, object>()
                {
                     {"message", "Welcome to pdc" },
                     {"tags", "100003203928567,100001678737370"}
                };
                //photo
                var tags = new[]
                {
                    new { tag_uid = "100003203928567", x = 20, y = 20 },
                    new { tag_uid = "100001678737370", x = 40, y = 40 }
                };

                //dynamic parameters2 = new ExpandoObject();
                //parameters2.message = "Welcome to pdc";
                //parameters2.tags = tags;
                //parameters2.url = "http://1.bp.blogspot.com/-evheT51sfeM/TlO_wZ8YDqI/AAAAAAAAA8I/fjlg0G8AgMY/s1600/The-best-top-hd-desktop-naruto-shippuden-wallpaper-naruto-shippuden-wallpapers-hd-11.jpg";
                //dynamic dialog2 = client.Post("me/photos", parameters2);
                //dynamic dialog = client.Post("me/feed", parameters);
                //var newPostId = dialog.id;
                return Ok(new UserInfoViewModel
                {
                    UserName = result.name == null ? string.Empty : result.name,
                    FirstName = result.first_name == null ? string.Empty : result.first_name,
                    LastName = result.last_name == null ? string.Empty : result.last_name,
                    Email = result.email == null ? string.Empty : result.email,
                    ExternalId = result.id == null ? string.Empty : result.id,
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                });
            }
            else
            {
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                //var AccessToken = "335060047-kTg3dlxv0YaUjfjJUiFCCVYOtZDew6bOCSCmxDxc";
                //var AccessTokenSecret = "dUTjnrf07DHO6bIycyy7LP7rtwS2dt0BGPLNCNfKZ8EMy";
                var AccessToken = identity.Claims.SingleOrDefault(c => c.Type.StartsWith("urn:tokens:twitter:accesstoken"));
                var AccessTokenSecret = identity.Claims.SingleOrDefault(c => c.Type.StartsWith("urn:tokens:twitter:accesssecret"));
                var service = new TwitterService(ConfigurationManager.AppSettings["TwiiterConsumerKey"], ConfigurationManager.AppSettings["TwiiterConsumerSecret"]);
                service.AuthenticateWith(AccessToken.Value, AccessTokenSecret.Value);
                var profile = service.GetUserProfile(new GetUserProfileOptions());
                //SendDirectMessageOptions msgOpt = new SendDirectMessageOptions();
                //msgOpt.UserId = 1360775870;
                //msgOpt.ScreenName = "";
                //msgOpt.Text = "Hello world PDC tooyen";
                //var dm = service.SendDirectMessage(msgOpt);
                return Ok(new UserInfoViewModel
                {
                    UserName = profile.ScreenName,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Email = string.Empty,
                    ExternalId = profile.ProfileImageUrlHttps,
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                });
            }
            
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                EmployeeCode = model.EmployeeCode,
                PhoneNumber = model.PhoneNumber
            };

            try
            {
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                IdentityResult response = await UserManager.AddToRoleAsync(user.Id, model.Role);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            var existingEmail = await _userRepository.FindByEmailAsync(model.Email);

            if (existingEmail != null)
            {
                //check if login provider is facebook -> connect existing account with facebook account.
                //if (externalLogin.LoginProvider == "Facebook")
                //{
                //    if (!existingEmail.UserLogins.Any(n => n.ProviderKey == "Facebook"))
                //    {
                //        existingEmail.UserLogins.Add(new IdentityUserLogin
                //        {
                //            LoginProvider = externalLogin.LoginProvider,
                //            ProviderKey = externalLogin.ProviderKey
                //        });
                //        _unitOfWork.Commit();
                //        return Ok();
                //    }
                //}
                //else
                //{
                //    //var siteName = ConfigurationManager.AppSettings["SiteName"];
                //    //ModelState.AddModelError("", string.Format(Resources.ErrorResource.ExistsEmail, existingEmail.Email, siteName));
                //    return BadRequest();
                //}
            }

            var existingUser = await _userRepository.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                var siteName = ConfigurationManager.AppSettings["SiteName"];
                ModelState.AddModelError("", string.Format(model.UserName + "is registered on" + siteName));
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = "Male",
                EmployeeCode = "500010"
            };

            try
            {
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                IdentityResult response = await UserManager.AddToRoleAsync(user.Id, model.Role);
                result = await UserManager.AddLoginAsync(user.Id, info.Login);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return Ok();
        }

        [HttpGet]
        [Route("User")]
        [ResponseType(typeof(ListResponseBase<UserModel>))]
        public IHttpActionResult GetAllUser([FromUri] int start, [FromUri] int max)
        {
            var user = _userRepository.FindAll(start, max);

            var results = from u in user.Result
                          select new UserModel
                          {
                              EmployeeCode = u.EmployeeCode,
                              UserName = u.UserName,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              FullName = u.FirstName + "  " + u.LastName,
                              Id = u.Id,
                              Gender = u.Gender,
                              PhoneNumber = u.PhoneNumber,
                              Email = u.Email,
                              DeviceId = u.DeviceId,
                              Role = UserManager.GetRoles(u.Id).Count() == 0 ? "" : UserManager.GetRoles(u.Id)[0]
                          };

            return Ok(new ListResponseBase<UserModel>
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200,
                items = results,
                total = user.Total
            });
        }

        [HttpPut]
        [Route("User/{id}")]
        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult UpdateUser(string id, UserModel userForm)
        {
            ApplicationUser user = _userRepository.FindById(id);
            if (user != null)
            {
                user.EmployeeCode = userForm.EmployeeCode;
                user.UserName = userForm.UserName;
                user.FirstName = userForm.FirstName;
                user.LastName = userForm.LastName;
                user.Email = userForm.Email;
                user.Gender = userForm.Gender;
                user.PhoneNumber = userForm.PhoneNumber;

                var oldRole = UserManager.GetRoles(user.Id).Count() == 0 ? "" : UserManager.GetRoles(user.Id)[0];
                if (oldRole != userForm.Role)
                {
                    UserManager.RemoveFromRole(user.Id, oldRole);
                    UserManager.AddToRole(user.Id, userForm.Role);
                }

                _unitOfWork.Commit();
            }

            return Ok(new ResponseBase
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200
            });
        }

        [HttpPost]
        [Route("User/{id}")]
        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult DeleteUser(string id)
        {
            ApplicationUser user = _userRepository.FindById(id);
            user.LockoutEnabled = true;
            //var role = UserManager.GetRoles(user.Id).Count() == 0 ? "" : UserManager.GetRoles(user.Id)[0];
            //UserManager.RemoveFromRole(user.Id, role);

            //_userRepository.Delete(user);
            _unitOfWork.Commit();

            return Ok(new ResponseBase
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200
            });
        }

        [HttpGet]
        [Route("User/{id}")]
        [ResponseType(typeof(ResponseBase<UserModel>))]
        public IHttpActionResult GetUserById(string id)
        {
            ApplicationUser user = _userRepository.FindById(id);

            UserModel userModel = new UserModel();
            user.Id = user.Id;
            userModel.EmployeeCode = user.EmployeeCode;
            userModel.UserName = user.UserName;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.FullName = user.FirstName + "  " + user.LastName;
            userModel.Gender = user.Gender;
            userModel.PhoneNumber = user.PhoneNumber;
            userModel.Email = user.Email;
            userModel.DeviceId = user.DeviceId;
            userModel.Role = UserManager.GetRoles(user.Id).Count() == 0 ? "" : UserManager.GetRoles(user.Id)[0];

            return Ok(new ResponseBase<UserModel>
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200,
                item = userModel
            });
        }

        [HttpGet]
        [Route("Role")]
        [ResponseType(typeof(ListResponseBase<IdentityRole>))]
        public IHttpActionResult GetAllRoles()
        {
            var roles = RoleManager.Roles.ToList();

            return Ok(new ListResponseBase<IdentityRole>
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200,
                items = roles
            });
        }

        [HttpGet]
        [Route("Search")]
        [ResponseType(typeof(ListResponseBase<UserModel>))]
        public IHttpActionResult Search([FromUri] int start, [FromUri] int max, [FromUri] string ec, [FromUri] string un, [FromUri] string fn, [FromUri] string ro)
        {
            var user = _userRepository.FindAll();

            var query = user.Where(a => (string.IsNullOrEmpty(ec) ? true : a.EmployeeCode.ToLower().Contains(ec.ToLower()))
                && (string.IsNullOrEmpty(un) ? true : a.UserName.ToLower().Contains(un.ToLower()))
                && (string.IsNullOrEmpty(fn) ? true : a.FirstName.ToLower().Contains(fn.ToLower()))
                && (string.IsNullOrEmpty(fn) ? true : a.LastName.ToLower().Contains(fn.ToLower()))
                && (string.IsNullOrEmpty(ro) ? true : UserManager.GetRoles(a.Id).Contains(ro)))
                .OrderBy(a => a.UserName);

            var totalUser = query.Count();

            user = query.Skip(start).Take(max).ToList();

            var results = from u in user
                          select new UserModel
                          {
                              EmployeeCode = u.EmployeeCode,
                              UserName = u.UserName,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              FullName = u.FirstName + "  " + u.LastName,
                              Id = u.Id,
                              Gender = u.Gender,
                              PhoneNumber = u.PhoneNumber,
                              Email = u.Email,
                              DeviceId = u.DeviceId,
                              Role = UserManager.GetRoles(u.Id).Count() == 0 ? "" : UserManager.GetRoles(u.Id)[0]
                          };

            return Ok(new ListResponseBase<UserModel>
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200,
                items = results,
                total = totalUser
            });
        }

        [HttpPost]
        [Route("AddToRole")]
        public async Task<IHttpActionResult> AddToRole(string id, params string[] selectedRole)
        {
            var user = _userRepository.FindByName(id);
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            selectedRole = selectedRole ?? new string[] { };

            var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.First());
                return BadRequest(ModelState);
            }
            result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.First());
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost]
        [Route("ResetPassword/{id}")]
        [ResponseType(typeof(ResponseBase))]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> ResetPassword(string id)
        {
            ApplicationUser user = _userRepository.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var provider = new DpapiDataProtectionProvider("BMW.Web");
                var userManage = UserManager;
                userManage.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ResetPassword"));

                string resetToken = await userManage.GeneratePasswordResetTokenAsync(id);
                var newPassword = RandomString(6);
                IdentityResult passwordChangeResult = await userManage.ResetPasswordAsync(id, resetToken, newPassword);

                var email = new EmailModel();
                email.fullName = user.FirstName + " " + user.LastName;
                email.receiver = user.Email;
                email.sender = System.Configuration.ConfigurationManager.AppSettings["sender"];
                email.senderPassword = System.Configuration.ConfigurationManager.AppSettings["senderPassword"];
                email.userName = user.UserName;
                email.newPassword = newPassword;
                email.subject = "Your password changed";

                EmailHelper.sendEmail(email, "ForgotPassword");
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok(new ResponseBase
            {
                devMessage = "Success",
                userMessage = "Success",
                statusCode = 200
            });
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
