using ExpenseTracker.Web.Stores.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAuthStore _store;

    public AccountController(IAuthStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _store.LoginAsync(request);

        return View(request);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _store.RegisterAsync(request);

        return View(request);
    }

    //public IActionResult RegisterConfirmation()
    //{
    //    return View();
    //}

    //public IActionResult ResendConfirmation()
    //{
    //    return View();
    //}


    //[HttpPost]
    //public async Task<IActionResult> ResendConfirmation(ResendConfirmationRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest("Invalid request");
    //    }

    //    var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);

    //    if (user is null)
    //    {
    //        return BadRequest("Invalid request");
    //    }

    //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //    var confirmationUrl = Url.Action(
    //        nameof(EmailConfirmed),
    //        "Account",
    //        new { email = user.Email, token },
    //        protocol: Request.Scheme);

    //    var emailMessage = new EmailMessage(request.Email, request.Email, "Email Confirmation", confirmationUrl);
    //    var userAgent = _contextAccessor.HttpContext?.Request?.Headers?.UserAgent;
    //    var agent = Parser.GetDefault().Parse(userAgent);
    //    var userInfo = new UserInfo(agent.UA.ToString(), agent.OS.ToString());

    //    _emailService.SendEmailConfirmation(emailMessage, userInfo);

    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Logout()
    //{
    //    await _signInManager.SignOutAsync();

    //    return RedirectToAction(nameof(Login));
    //}

    //public async Task<IActionResult> EmailConfirmed(string email, string token)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);

    //    if (user is null)
    //    {
    //        ViewData["ErrorMessage"] = "The email confirmation link is invalid or expired. Please request a new confirmation email.";
    //        return View();
    //    }

    //    var result = await _userManager.ConfirmEmailAsync(user, token);

    //    if (!result.Succeeded)
    //    {
    //        ViewData["ErrorMessage"] = "The email confirmation link is invalid or expired. Please request a new confirmation email.";
    //        ViewBag.Email = email;
    //        return View(new ResendConfirmationRequest(email));
    //    }

    //    var actionUrl = Url.Action(
    //            nameof(Login),
    //            "Account",
    //            new { email = user.Email, token },
    //            protocol: Request.Scheme);
    //    var emailMessage = new EmailMessage(email, email, "Welcome!", actionUrl);
    //    _emailService.SendWelcome(emailMessage);

    //    return View();
    //}

    //public IActionResult ForgotPassword()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> ForgotPassword(Application.Requests.Auth.ForgotPasswordRequest request)
    //{
    //    var user = await _userManager.FindByEmailAsync(request.Email);

    //    if (user is null)
    //    {
    //        return BadRequest("Invalid request");
    //    }

    //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

    //    var confirmationUrl = Url.Action(
    //        nameof(PasswordReset),
    //        "Account",
    //        new { email = user.Email, token },
    //        protocol: Request.Scheme);

    //    var emailMessage = new EmailMessage(request.Email, request.Email, "Password reset", confirmationUrl);
    //    var userAgent = _contextAccessor.HttpContext?.Request?.Headers?.UserAgent;
    //    var agent = Parser.GetDefault().Parse(userAgent);
    //    var userInfo = new UserInfo(agent.UA.ToString(), agent.OS.ToString());

    //    _emailService.SendResetPassword(emailMessage, userInfo);

    //    return RedirectToAction(nameof(PasswordResetConfirmation));
    //}

    //public IActionResult PasswordResetConfirmation()
    //{
    //    return View();
    //}

    //public IActionResult PasswordReset(string email, string token)
    //{
    //    var request = new Application.Requests.Auth.ResetPasswordRequest(email, null, null, token);

    //    return View(request);
    //}

    //[HttpPost]

    //public async Task<IActionResult> PasswordReset([FromForm] Application.Requests.Auth.ResetPasswordRequest request)

    //{
    //    var user = await _userManager.FindByEmailAsync(request.Email);

    //    if (user is null)
    //    {
    //        return BadRequest("Invalid request");
    //    }

    //    if (!user.EmailConfirmed)
    //    {
    //        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //        await _userManager.ConfirmEmailAsync(user, token);
    //    }

    //    var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

    //    if (result.Succeeded)
    //    {
    //        return RedirectToAction(nameof(Login));
    //    }

    //    return BadRequest("Invalid request");
    //}

    //private IActionResult RedirectToLocal(string? returnUrl)
    //{
    //    if (Url.IsLocalUrl(returnUrl))
    //    {
    //        return Redirect(returnUrl);
    //    }
    //    else
    //    {
    //        return RedirectToAction(nameof(HomeController.Index), "Home");
    //    }
    //}

    //private void AddErrors(IdentityResult result)
    //{
    //    foreach (var error in result.Errors)
    //    {
    //        ModelState.AddModelError(string.Empty, error.Description);
    //    }
    //}
}