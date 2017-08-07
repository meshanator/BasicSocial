using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BasicSocial.BasicIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BasicSocial.Web.Models;

namespace BasicSocial.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
	{
		private readonly SignInManager<BasicSocialUser, int> _signInManager;
		private readonly UserManager<BasicSocialUser, int> _userManager;

        public AccountController(SignInManager<BasicSocialUser, int> signInManager, UserManager<BasicSocialUser, int> userManager)
        {
	        _signInManager = signInManager;
	        _userManager = userManager;
        }

		[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
		
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

	        _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
	        var user = _signInManager.UserManager.FindByName(model.UserName);

	        if (ModelState.IsValid && !string.IsNullOrWhiteSpace(model.Password) && user != null)
	        {
		        var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

		        switch (signInResult)
		        {
			        case SignInStatus.Success:
				        return RedirectToAction("index", "home");
			        case SignInStatus.Failure:
				        break;
		        }
			}
			return View(model);
		}

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
	            var user = new BasicSocialUser()
	            {
					FirstName = model.FirstName,
					LastName = model.LastName,
		            Age = model.Age,
					PasswordHash = model.Password,
					UserName = model.UserName
	            };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }
		
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
		
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    //_userManager.Dispose();
                }

                if (_signInManager != null)
                {
                    //_signInManager.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

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
        #endregion
    }
}