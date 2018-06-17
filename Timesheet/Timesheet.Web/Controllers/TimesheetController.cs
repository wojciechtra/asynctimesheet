using DataAccessLayer.Core.Interfaces.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Timesheet.BLL.Models;
using Timesheet.ViewModels;
using Timesheet.ViewModels.DataTable;

namespace Timesheet.Web.Controllers
{
    [Authorize]
    public class TimesheetController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;

        public TimesheetController(IUnitOfWork uow, UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            
            var entries = _uow.Repository<EntryType>().GetRange().AsEnumerable();
            return View(entries);
        }

        public IActionResult Add()
        {
            AddTimesheetItemViewModel model = new AddTimesheetItemViewModel
            {
                EntryTypes = _uow.Repository<EntryType>().GetRange().AsEnumerable(),                
            };

            if (User.IsInRole("Admin"))
            {
                model.Users = _userManager.Users.AsEnumerable();
            }
            else
            {
                model.Users = _userManager.Users.Where(u => u.NormalizedUserName == User.Identity.Name.ToUpper());
            }

            return View(model);
        }

        [HttpPost]
        public  IActionResult Add(AddTimesheetItemViewModel model)
        {
            if (!ModelState.IsValid)
            {                
                model.EntryTypes = _uow.Repository<EntryType>().GetRange().AsEnumerable();
                model.Users = _userManager.Users.AsEnumerable();

                return View(model);
            }

            EntryType entryType = _uow.Repository<EntryType>().Get(model.EntryTypeId);

            TimesheetItem timesheetItem = new TimesheetItem
            {
                Day = model.Day,
                DayPercent = model.DayPercent,
                Comment = model.Comment,
                EntryType = entryType,
                IsSettled = false,
                UserId = model.UserId
            };

            try
            {
                _uow.Repository<TimesheetItem>().Add(timesheetItem);
                _uow.Save();

            }
            catch(Exception)
            {

            }

            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetTimesheetEntries(int start, int length)
        {

            var timesheetEntries = _uow.Repository<TimesheetItem>()
                .GetRange(u => u.User.NormalizedUserName == User.Identity.Name.ToUpper())                
                .OrderBy(d => d.Day)
                .Select(e => new TimesheetItemViewModel
                {
                    UserName = e.User.UserName,
                    Day = e.Day.ToString("dd-MM-yyyy"),
                    DayPercent = e.DayPercent,
                    Comment = e.Comment,
                    EntryType = e.EntryType.Name

                });
            var dataTable = new DataTableObject<TimesheetItemViewModel>();
            dataTable.aaData = timesheetEntries
                .Skip(start)
                .Take(length)
                .ToList();
            dataTable.iDisplayCount = timesheetEntries.Count();
            dataTable.iTotalRecords = timesheetEntries.Count();

            return Json(new
            {
                iTotalRecords = dataTable.iTotalRecords,
                iTotalDisplayRecords = dataTable.iDisplayCount,
                aaData = dataTable.aaData
            });
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}