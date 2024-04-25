using Majd.Data;
using Majd.Models;
using Majd.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Majd.Controllers
{
    public class PostController : Controller
    {
        IWebHostEnvironment hostEnvironment;
        private readonly AppDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;


        public PostController(IWebHostEnvironment hostEnvironment, AppDBContext db, UserManager<ApplicationUser> userManager, ILogger<PostController> logger, IPostRepository postRepository)
        {
            this.hostEnvironment = hostEnvironment;
            _db = db;
            _userManager = userManager;
            _logger = logger;
            _postRepository = postRepository;
            _postRepository = postRepository;
        }

        // GET: /<controller>/

        public IActionResult AlumniHomepage()
        {


            var posts = _db.Posts.Include(p => p.User).ToList();
            var currentUser = _userManager.GetUserAsync(User).Result;
            ViewBag.UserName = $"{currentUser.FirstName} {currentUser.LastName}";
            ViewBag.Posts = posts;
            return View();
        }

        [HttpPost]
        public IActionResult Like(int id)
        {
            var post = _db.Posts.Find(id);
            if (post != null)
            {
                post.LikeCount++;
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AlumniHomepageAsync(Post model, IFormFile? imageFile, IFormFile? videoFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erorrs = ModelState.Values.SelectMany(v => v.Errors);

                }


                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    // Handle the case where the user is not found
                    return BadRequest("User not found");
                }
                else
                {
                    model.UserId = currentUser.Id;
                }


                string uploadFolder;
                string fileName;

                // Check if an image file was uploaded
                if (imageFile != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    uploadFolder = Path.Combine(hostEnvironment.WebRootPath, @"images");
                    model.ImageUrl = "/images/" + fileName;

                    using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                }

                // Check if a video file was uploaded
                if (videoFile != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                    uploadFolder = Path.Combine(hostEnvironment.WebRootPath, @"videos");
                    model.VideoUrl = "/videos/" + fileName;

                    using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await videoFile.CopyToAsync(fileStream);
                    }
                }



                await _postRepository.CreateAsync(model);


            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while saving changes to the database.");

                // Optionally, you can handle the exception and return an appropriate error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }


            return RedirectToAction(nameof(AlumniHomepage));
        }



        private class HttpStatusCodeResult : ActionResult
        {
            private HttpStatusCode badRequest;

            public HttpStatusCodeResult(HttpStatusCode badRequest)
            {
                this.badRequest = badRequest;
            }
        }
    }
}

    


    

