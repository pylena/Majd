using Majd.Data;
using Microsoft.AspNetCore.Mvc;
using Majd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Majd.Repository;
using Majd.Models.enums;


namespace Majd.Controllers
{
    public class JobController : Controller
    {

        private readonly IJobRepository _IJobRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobController(IJobRepository JobRepository, UserManager<ApplicationUser> usermanager)
        {
            _IJobRepository = JobRepository;
            _userManager = usermanager;
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        //GET Job/CreateJobOffer
        public IActionResult CreateJobOffer()
            {
                return View();
            }

          [HttpPost]
          [Authorize(Roles = "Employer")]
          //Post Job/CreateJobOffer
        public async Task<IActionResult> CreateJobOffer(Job job)
        {
            if (!ModelState.IsValid)
            {
                //for testing only, error handling is required here!
                var errors = ModelState.Values.SelectMany(v => v.Errors);

            }

            job.UserId = _userManager.GetUserId(User);

            await _IJobRepository.CreateAsync(job);

            return RedirectToAction("JobList");
        }
        //Get Jobs/JobList
        public async Task<IActionResult> JobList(JobIndustry? selectedIndustry, int pageNum)
            {
            var jobs = _IJobRepository.GetAllAsync();
            if (selectedIndustry is not null)
            {
                jobs = jobs.Where(i => i.JobIndustry == selectedIndustry);
            }
            if (!jobs.Any())
            {
                ViewBag.Message = "No jobs available for the selected industry.";
            }
            if (pageNum < 1)
            {
                pageNum = 1;
            }

            int pageSize = 5;
            var paging = await Paginator<Job>.CreateAsync(jobs, pageNum, pageSize);
            return View(paging);
            }

            // Get/Jobs/JobDetails
            public async Task<IActionResult> JobDetails(String id)
            {
            var job = await _IJobRepository.GetByIdAsync(id);

            return View(job);
            }

            [HttpPost]
            [Authorize(Roles = "Employer")]
            //Post Jobs/DeleteJob
        public async Task<IActionResult>  DeleteJob(string id)
            {
            await _IJobRepository.DeleteAsync(id);


            return RedirectToAction("JobList");


            }
        }
    }




