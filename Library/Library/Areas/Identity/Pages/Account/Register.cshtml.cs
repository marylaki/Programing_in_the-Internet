using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Ключ Доступа")]
        [DataType(DataType.Password)]
        [Remote(action: "ValidateKey", controller: "Home")]
        [Editable(true)]
        [BindProperty]
        public string SecretKey { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Remote(action: "ValidateEmail", controller: "Home")]
        [BindProperty]
        [RegularExpression(@"^[-a-zA-Z0-9]*[-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$")]
        public string Email { get; set; }
        public class InputModel
        {
            

            [Required]
            [StringLength(14, ErrorMessage = "{0} должена быть не кророче {2} символов и не больше {1} символов.", MinimumLength = 3)]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required]
            [StringLength(14, ErrorMessage = "{0} должено быть не кророче {2} символов и не больше {1} символов.", MinimumLength = 3)]
            [Display(Name = "Имя")]
            public string Name { get; set; }

            [Display(Name = "Отчество")]
            public string FatherName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Дата рождения")]
            public DateTime BDay { get; set; }

            [Required]
            [Display(Name = "Адрес")]
            public string Address { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Телефон")]
            [RegularExpression(@"^((8|\+3)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")]
            public string Phone { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} должен быть не кророче {2} символов и не больше {1} символов.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль (min 7)")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторите пароль")]
            [Compare("Password", ErrorMessage = "Пароли не совпадают")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                LibraryModel lib = LibraryModel.getLib();
                LibraryCard userCard = new LibraryCard();
                userCard.Name = Input.LastName + " " + Input.Name + " " + Input.FatherName;
                userCard.Email = Email;
                userCard.Birthday = Input.BDay;
                userCard.Adress = Input.Address;
                userCard.Telephone = Input.Phone;
                var user = new IdentityUser { UserName = Email, Email = Email, PhoneNumber = Input.Phone };
                if (SecretKey == "KH_2286879_Library_key")
                {
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        lib.Library.LibraryCard.Add(userCard);
                        lib.Library.SaveChanges();
                        await _userManager.AddToRoleAsync(user, "Librarian");
                        _logger.LogInformation("{0} зарегистривован как библиотекарь.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Email, "Библиотека",
                            "Здравствуйте, "+ userCard.Name+"! Наша библиотека рада новому библиотекарю!");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
                else if (SecretKey == null)
                {
                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        lib.Library.LibraryCard.Add(userCard);
                        lib.Library.SaveChanges();
                        await _userManager.AddToRoleAsync(user, "User");
                        _logger.LogInformation("{0} зарегистривован как пользователь.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Email, "Библиотека",
                            "Здравствуйте, " + userCard.Name + "! Наша библиотека рада новому пользоватею!");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            // If we got this far, something failed, redisplay form
            return Page();
            
        }
    }
}