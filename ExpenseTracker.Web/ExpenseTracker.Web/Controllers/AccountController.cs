using ExpenseTracker.Web.Requests.Auth;
using ExpenseTracker.Web.Stores.Auth;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace ExpenseTracker.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAuthStore _store;
    private readonly ILogger<AccountController> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountController(
        IAuthStore store,
        IHttpContextAccessor contextAccessor,
        ILogger<AccountController> logger)
    {
        _store = store;
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        _logger.LogInformation("Entered Login page");
        _logger.LogWarning("Warning from Login");
        _logger.LogError("Error from Login");
        _logger.LogTrace("Trace from Login");
        _logger.LogCritical("Critical from Login");

        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserRequest request, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var isSuccess = await _store.LoginAsync(request);

        if (isSuccess)
        {
            _logger.LogInformation("Login successful");
            return RedirectToAction("Index", "Home");
        }

        _logger.LogWarning("Login failed");
        ViewData["ErrorMessage"] = "Invalid username or password.";
        return View(request);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request, string? returnUrl = null)
    {
        try
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var userAgent = _contextAccessor.HttpContext?.Request?.Headers.UserAgent;
            var agent = Parser.GetDefault().Parse(userAgent);

            request.Browser = agent.UA.ToString();
            request.OS = agent.OS.ToString();
            request.ConfirmUrl = Url.Action(nameof(EmailConfirmed), "Account", null, protocol: Request.Scheme);


            await _store.RegisterAsync(request);

            return View(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error registering: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> EmailConfirmed(string token, string email)
    {
        await _store.EmailConfirmedAsync(new EmailConfirmedRequest(email, token));

        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var userAgent = _contextAccessor.HttpContext?.Request?.Headers.UserAgent;
        var agent = Parser.GetDefault().Parse(userAgent);

        request.Browser = agent.UA.ToString();
        request.OS = agent.OS.ToString();
        request.RedirectUrl = Url.Action(nameof(PasswordReset), "Account", null, protocol: Request.Scheme);

        var response = await _store.ResetPasswordAsync(request);

        if (response is false)
        {
            return BadRequest("Invalid request");
        }

        return RedirectToAction(nameof(PasswordResetConfirmation));
    }

    public IActionResult PasswordResetConfirmation()
    {
        return View();
    }

    public IActionResult PasswordReset(string email, string token)
    {
        var request = new ResetPasswordRequest(email, token, null!, null!);

        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> PasswordReset([FromForm] ResetPasswordRequest request)

    {
        if (!ModelState.IsValid)
        {
            BadRequest("Passwords must match");
        }

        var response = await _store.ConfirmResetPasswordAsync(request);

        if (response is false)
        {
            return BadRequest("Invalid request");
        }


        return RedirectToAction(nameof(Login));
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