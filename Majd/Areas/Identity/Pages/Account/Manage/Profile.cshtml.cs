// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Majd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Majd.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = " رقم التواصل")]
            public string PhoneNumber { get; set; }
            [Display(Name = "عن الشركة")]

            public string? CompanyDescription { get; set; }
            [Display(Name = "موقع الشركة")]
            public string? Headquarter { get; set; }
            [Display(Name = "مقر الشركة ")]
            public string? UrlLocation { get; set; }
            [Display(Name = " مجال الصناعة ")]

            public string CareerSectore { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                CompanyDescription = user.CompanyDescription,
                Headquarter = user.Headquarter,
                UrlLocation = user.UrlLocation,
                CareerSectore = user.CareerSectore,
                FirstName = user.FirstName,
                LastName = user.LastName,

                //about = user.about

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.PhoneNumber = Input.PhoneNumber != user.PhoneNumber ? Input.PhoneNumber : user.PhoneNumber;
            user.CompanyDescription = Input.CompanyDescription != user.CompanyDescription ? Input.CompanyDescription : user.CompanyDescription;
            user.Headquarter = Input.Headquarter != user.Headquarter ? Input.Headquarter : user.Headquarter;
            user.UrlLocation = Input.UrlLocation != user.UrlLocation ? Input.UrlLocation : user.UrlLocation;
            user.CareerSectore = Input.CareerSectore != user.CareerSectore ? Input.CareerSectore : user.CareerSectore;
            user.FirstName = Input.FirstName != user.FirstName ? Input.FirstName : user.FirstName;
            user.LastName = Input.LastName != user.LastName ? Input.LastName : user.LastName;



            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
