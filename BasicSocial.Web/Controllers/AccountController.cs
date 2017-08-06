using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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

		public AccountController()
        {
        }

        public AccountController(SignInManager<BasicSocialUser, int> signInManager, UserManager<BasicSocialUser, int> userManager)
        {
	        _signInManager = signInManager;
	        _userManager = userManager;
        }

	    //public ApplicationSignInManager SignInManager
	    //{
	    //    get
	    //    {
	    //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
	    //    }
	    //    private set 
	    //    { 
	    //        _signInManager = value; 
	    //    }
	    //}

	    //public ApplicationUserManager UserManager
	    //{
	    //    get
	    //    {
	    //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
	    //    }
	    //    private set
	    //    {
	    //        _userManager = value;
	    //    }
	    //}

		//public ApplicationSignInManager SignInManager
		//{
		//    get
		//    {
		//        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
		//    }
		//    private set 
		//    { 
		//        _signInManager = value; 
		//    }
		//}

		//public ApplicationUserManager UserManager
		//{
		//    get
		//    {
		//        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
		//    }
		//    private set
		//    {
		//        _userManager = value;
		//    }
		//}

		//
		// GET: /Account/Login
		[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
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
	        var user = _signInManager.UserManager.FindByName(model.Email);

	        if (ModelState.IsValid && !string.IsNullOrWhiteSpace(model.Password) && user != null)
	        {
		        var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

		        switch (signInResult)
		        {
			        case SignInStatus.Success:
				        return RedirectToAction("index", "home");
			        case SignInStatus.Failure:
				        break;
		        }

				//ModelState.AddModelError(string.Empty, Translations.Translations.pleaseEnterUsernameAndPassword);
			}
			return View(model);
		}

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
	            var user = new BasicSocialUser()
	            {
					FirstName = "asd",
					LastName = "dsa",
					PasswordHash = model.Password,
					UserName = model.Email,
					Age = 17
	            };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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