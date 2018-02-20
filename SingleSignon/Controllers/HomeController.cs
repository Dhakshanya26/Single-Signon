using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security;
using Unity;
using Unity.AspNet.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
      IAuthenticationManager AuthenticationManager;
        public HomeController(IAuthenticationManager authenticationManager)
        {
            AuthenticationManager = authenticationManager;
        }
        public ActionResult Index()
        {
            return View();
        }


        #region External login in modal popup

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ExternalLoginInModalPopUp(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginInModalPopUpCallback", "Home", new { returnUrl, provider }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginInModalPopUpCallback(string returnUrl, string provider)
        {
            

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return View("ExternalLoginResult", new ExternalLoginResultModel
                {
                    Callback = "myExternalModal.socialLoginCallback",
                    CallbackParameters = new DefaultExternalLoginCallbackParameters
                    {
                        Success = false,
                        Message = "Unable to login. Please try again.",
                        Provider = provider
                    }
                });
            }
            return View("ExternalLoginResult", new ExternalLoginResultModel
            {
                Callback = "loginCallback",
                CallbackParameters = new DefaultExternalLoginCallbackParameters
                {
                    Success = true,
                    ConfirmPassword = true,
                    Provider = provider
                }
            });
            return null;
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
                //case SignInStatus.Success:
                //          var externalLoginUser = await UserManager.FindByEmailAsync(loginInfo.Email);
                //  await LoginSuccessPostProcess(externalLoginUser);
                //  return View("ExternalLoginResult", new ExternalLoginResultModel
                //  {
                //      Callback = "myExternalModal.socialLoginCallback",
                //      CallbackParameters = new DefaultExternalLoginCallbackParameters
                //      {
                //          Success = true,
                //          ReturnUrl = returnUrl,
                //          Provider = provider
                //      }
                //  });
                //      case SignInStatus.LockedOut:
                //          return View("ExternalLoginResult", new ExternalLoginResultModel
                //          {
                //              Callback = "myExternalModal.socialLoginCallback",
                //              CallbackParameters = new DefaultExternalLoginCallbackParameters
                //              {
                //                  Success = false,
                //                  ReturnUrl = Url.Action("Lockout"),
                //                  Redirect = true,
                //                  Provider = provider
                //              }
                //          });
                //  default:
                //          var existingUser = await UserManager.FindByEmailAsync(loginInfo.Email);
                //  if (existingUser != null)
                //  {
                //      var hasPassword = await UserManager.HasPasswordAsync(existingUser.Id);
                //      if (hasPassword)
                //      {
                //          return View("ExternalLoginResult", new ExternalLoginResultModel
                //          {
                //              Callback = "myExternalModal.socialLoginCallback",
                //              CallbackParameters = new DefaultExternalLoginCallbackParameters
                //              {
                //                  Success = true,
                //                  ConfirmPassword = true,
                //                  Provider = provider
                //              }
                //          });
                //      }

                //      var createdLogin = await UserManager.AddLoginAsync(existingUser.Id, loginInfo.Login);
                //      if (createdLogin.Succeeded)
                //      {
                //          await SignInManager.SignInAsync(existingUser, isPersistent: false, rememberBrowser: false);
                //          await LoginSuccessPostProcess(existingUser);
                //          return View("ExternalLoginResult", new ExternalLoginResultModel
                //          {
                //              Callback = "myExternalModal.socialLoginCallback",
                //              CallbackParameters = new DefaultExternalLoginCallbackParameters
                //              {
                //                  Success = true,
                //                  ReturnUrl = returnUrl,
                //                  Provider = provider
                //              }
                //          });
                //      }
                //  }
                //  else
                //  {
                //      // First check if user exist on strip
                //      var stripContact = _stripService.GetWebContact(loginInfo.Email);
                //      if (stripContact != null)
                //      {
                //          var appUser = await CreateUserFromStrip(loginInfo.Email);
                //          if (appUser != null)
                //          {
                //              return View("ExternalLoginResult", new ExternalLoginResultModel
                //              {
                //                  Callback = "myExternalModal.socialLoginCallback",
                //                  CallbackParameters = new DefaultExternalLoginCallbackParameters
                //                  {
                //                      Success = true,
                //                      ConfirmPassword = true,
                //                      Provider = provider
                //                  }
                //              });
                //          }
                //      }
                //      else
                //      {
                //          var humanName = new HumanName(loginInfo.ExternalIdentity.Name);
                //          var socialRegisterModel = new SocialRegisterModel
                //          {
                //              Email = loginInfo.Email,
                //              FirstName = humanName.First,
                //              Surname = humanName.Last
                //          };
                //          var rldCustomerId = _stripService.CreateContact(socialRegisterModel);

                //          if (rldCustomerId != null)
                //          {
                //              var user = new ApplicationUser
                //              {
                //                  UserName = loginInfo.Email,
                //                  Email = loginInfo.Email,
                //                  FirstName = humanName.First,
                //                  Surname = humanName.Last
                //              };

                //              var createResult = await UserManager.CreateAsync(user);
                //              if (createResult.Succeeded)
                //              {
                //                  var customerRegistered = await _customerService.CreateNewCustomerAsync(socialRegisterModel, user.Id, rldCustomerId.Value);
                //                  if (customerRegistered)
                //                  {
                //                      createResult = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);

                //                      if (createResult.Succeeded)
                //                      {
                //                          await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //                          await LoginSuccessPostProcess(user);
                //                          return View("ExternalLoginResult", new ExternalLoginResultModel
                //                          {
                //                              Callback = "myExternalModal.socialLoginCallback",
                //                              CallbackParameters = new DefaultExternalLoginCallbackParameters
                //                              {
                //                                  Success = true,
                //                                  ReturnUrl = returnUrl,
                //                                  Provider = provider
                //                              }
                //                          });
                //                      }
                //                  }
                //              }
                //          }
                //      }
                //  }
                //  return View("ExternalLoginResult", new ExternalLoginResultModel
                //  {
                //      Callback = "myExternalModal.socialLoginCallback",
                //      CallbackParameters = new DefaultExternalLoginCallbackParameters
                //      {
                //          Success = false,
                //          Message = "Unable to login. Please try again.",
                //          Provider = provider
                //      }
                //  });
            }

        
        #endregion
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            //read https://en.wikipedia.org/wiki/Cross-site_request_forgery
            private const string XsrfKey = "CsrfId";

            public override void ExecuteResult(ControllerContext context)
            {
              
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

    }
    public class ExternalLoginResultModel
    {
        public string Callback { get; set; }
        public IExternalLoginCallbackParameters CallbackParameters { get; set; }
    }

    public interface IExternalLoginCallbackParameters
    {
        bool Success { get; set; }
        string Message { get; set; }
        string ReturnUrl { get; set; }
        string Provider { get; set; }
    }

    public class DefaultExternalLoginCallbackParameters : IExternalLoginCallbackParameters
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public string Provider { get; set; }
        public bool ConfirmPassword { get; set; }
        public bool Redirect { get; set; }
    }

}