﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SubSonic.Extensions;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult UmangLogin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> UmangLogin(LoginViewModel model, string returnUrl)
        {
            UM_DBEntities dbe = new UM_DBEntities();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    string id = User.Identity.GetUserId();
                    Session["CUser"] = null;
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        tbl_LoginDetail tbl = new tbl_LoginDetail();
                        tbl.ID = Guid.NewGuid();
                        tbl.UserId = Guid.Parse(id);
                        tbl.LoginDt = DateTime.Now;
                        tbl.IsActive = true;
                        dbe.tbl_LoginDetail.Add(tbl);
                        dbe.SaveChanges();
                    }
                    // return RedirectToLocal(returnUrl);
                    return RedirectToAction("Resource", "Report");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            UM_DBEntities dbe = new UM_DBEntities();
            Session["CUser"] = null;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    string id = User.Identity.GetUserId();
                    Session["CUser"] = null;
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        tbl_LoginDetail tbl = new tbl_LoginDetail();
                        tbl.ID = Guid.NewGuid();
                        tbl.UserId = Guid.Parse(id);
                        tbl.LoginDt = DateTime.Now;
                        tbl.IsActive = true;
                        dbe.tbl_LoginDetail.Add(tbl);
                        dbe.SaveChanges();
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Authorize]
        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [Authorize(Roles = "Admin,PCI_Representative,State")]
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            UM_DBEntities dbe = new UM_DBEntities();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.PhoneNumber.Trim(), Email = model.PhoneNumber.Trim() + "@gmail.com", PhoneNumber = model.PhoneNumber.Trim() };
                var result = await UserManager.CreateAsync(user, model.PhoneNumber.Trim());
                if (result.Succeeded)
                {
                    var uptbl = dbe.AspNetUsers.Find(user.Id);
                    if (uptbl != null)
                    {
                        //Role mapping
                        var result1 = UserManager.AddToRole(user.Id, model.Role);

                        uptbl.Name = model.Name.Trim();
                        uptbl.StateId = 20;
                        if (model.Role.ToLower() == "consultant")//BAA881FB-F8BD-4199-A6CA-6FCAF7E3A0C2
                        {
                            uptbl.DistrictId = model.DistrictId;
                        }
                        else if (model.Role.ToLower() == "teacher")//2CAC5128-08C7-4B95-8C6A-DB61E6B40376
                        {
                            var idblck = dbe.SchoolMaster_N.Where(x => x.ID == model.SchoolId).FirstOrDefault().BlockCode;
                            uptbl.DistrictId = model.DistrictId;
                            uptbl.BlockId = Convert.ToInt32(idblck);
                            uptbl.SchoolId = model.SchoolId;
                        }
                        //uptbl.ClusterId = model.ClusterId;
                        uptbl.CreatedBY = user.Id;
                        uptbl.CreatedDt = DateTime.Now;
                        dbe.SaveChanges();
                    }
                    // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("UserDetaillist", "Master");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/EditUser
        //[AllowAnonymous]
        [Authorize(Roles = "Admin,PCI_Representative,State")]
        public ActionResult EditUser(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                UM_DBEntities dbe = new UM_DBEntities();
                var user = dbe.AspNetUsers.Find(id);
                var idblck = 0;


                var model = new UserEditViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    //Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    //DistrictId = user.DistrictId.Value,
                    //BlockId = user.BlockId.Value,
                    //ClusterId = user.ClusterId.Value,

                    Role = user.AspNetRoles.FirstOrDefault().Name,
                    DistrictId = user.DistrictId,
                    BlockId  = user.BlockId,
                    SchoolId  = user.SchoolId,
                   // DistrictId = ("Consultant").ToLower() == (user.AspNetRoles.FirstOrDefault().Name).ToLower() ? user.DistrictId.Value : 0,
                   // BlockId = (("Teacher").ToLower()) == (user.AspNetRoles.FirstOrDefault().Name).ToLower() ? user.BlockId.Value : 0,
                   // SchoolId = (("Teacher").ToLower()) == (user.AspNetRoles.FirstOrDefault().Name).ToLower() ? user.SchoolId.Value : 0,
                };
                if (model == null)
                {
                    return RedirectToAction("Register", "Account");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditUser(UserEditViewModel model)
        public ActionResult EditUser(UserEditViewModel model)
        {
            UM_DBEntities dbe = new UM_DBEntities();
            if (ModelState.IsValid)
            {
                var user = dbe.AspNetUsers.Find(model.Id);
                if (user != null)
                {
                    if (!dbe.AspNetUsers.Any(x => x.PhoneNumber == model.PhoneNumber && x.Id != model.Id))
                    {
                        //user.UserName = model.PhoneNumber;
                        //user.Email = model.PhoneNumber+"@gmail.com";
                        // user.PhoneNumber = model.PhoneNumber;
                        user.Name = model.Name;
                        //user.DistrictId = model.DistrictId;
                        //user.BlockId = model.BlockId;
                        if (model.Role.ToLower() == "consultant")//BAA881FB-F8BD-4199-A6CA-6FCAF7E3A0C2
                        {
                            user.DistrictId = model.DistrictId;
                        }
                        else if (model.Role.ToLower() == "teacher")//2CAC5128-08C7-4B95-8C6A-DB61E6B40376
                        {
                            var idblck = dbe.SchoolMaster_N.Where(x => x.ID == model.SchoolId).FirstOrDefault().BlockCode;
                            user.DistrictId = model.DistrictId;
                            user.BlockId = Convert.ToInt32(idblck);
                            user.SchoolId = model.SchoolId;
                        }
                        dbe.SaveChanges();

                        var userRoles = UserManager.GetRoles(model.Id);
                        foreach (var item in userRoles)
                        {
                            if (model.Role != item)
                            {
                                UserManager.RemoveFromRoles(model.Id, item);
                                UserManager.AddToRole(model.Id, model.Role);
                            }
                        }

                        GlobalUtilityManager.MessageToaster(this, "Edit User", Enums.GetEnumDescription(Enums.eReturnReg.Update), eAlertType.success.ToString());
                        return RedirectToAction("UserDetaillist", "Master");
                    }
                    else
                    {
                        ModelState.AddModelError("DuplicateEmail", "Email already exists!");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserNotFound", "User Not Found");
                }
            }
            return View(model);
        }
        public ActionResult Register_Lock(RegisterViewModel model)
        {
            var lockoutEndDate = DateTime.Now.Date;
            UserManager.SetLockoutEndDate(model.Id, DateTimeOffset.Now.Date);
            UserManager.SetLockoutEnabled(model.Id, false);
            return RedirectToLocal("~/Master/UserDetaillist");
        }
        public ActionResult Register_Enable(RegisterViewModel model)
        {
            var lockoutEndDate = DateTime.Now.Date;
            UserManager.SetLockoutEndDate(model.Id, DateTimeOffset.Now.Date);
            UserManager.SetLockoutEnabled(model.Id, true);
            return RedirectToLocal("~/Master/UserDetaillist");
        }
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (User.IsInRole("Umang"))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("UmangLogin", "Account");
            }
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
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
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
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
            return RedirectToAction("Dashboard", "Report");
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