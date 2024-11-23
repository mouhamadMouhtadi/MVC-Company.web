using Company.Data.Models;
using Company.Service.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;

namespace Company.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel input)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = input.Email.Split("@")[0],
					Email = input.Email,
					FirstName = input.FirstName,
					LastName = input.LastName,
					IsActive = input.IsActive,
				};
				var result = await _userManager.CreateAsync(user, input.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
				}
			}
			return View(input);
		}
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(LogInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					if (await _userManager.CheckPasswordAsync(user, model.Password))
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}
				ModelState.AddModelError("", "Invalid Login");
				return View(model);
			}
			return View(model);
		}
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(input.Email);
				if (user is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var url = Url.Action("ResetPassword", "Account", new { Email = input.Email, Token = token }, Request.Scheme);
					var email = new Email
					{
						Body = url,
						Subject = "Reset Passwork",
						To = input.Email
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
			}
			return View(input);
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ResetPassword (string	Email, string Token)
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var Res = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
					if (Res.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
					foreach (var err in Res.Errors)
					{
						ModelState.AddModelError("", err.Description);
					}
				}
			}
			return View(model);
		}
		public IActionResult AccessDenied ()
		{
			return View();
		}
	}
}