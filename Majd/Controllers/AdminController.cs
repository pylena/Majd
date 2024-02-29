using Majd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Majd.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IActionResult PendingRegistrations()
        {
            //get all unapproved employer
            var pendingUsers = _userManager.Users.Where(u => !u.IsAproved).ToList();
            return View(pendingUsers);
        }

        public async Task<IActionResult> ApproveEmployer(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsAproved = true;
                await _userManager.UpdateAsync(user);

                // Send email notification
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Registration Approved",
                    $"Your registration for majd has been approved!"
                );

                return RedirectToAction("PendingRegistrations");

            }
            else
            {
                return NotFound("User not found.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RejectEmployer(string userId, string response)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }


            user.RejectionReason = response;
            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(
                user.Email,
                "Registration Rejected",
                $"Your registration for Majd has been rejected. Reason: {response}"
            );

            return RedirectToAction("PendingRegistrations");

        }

    }
}
