using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RBod.PlayBall.GroupManagement.Business.Services;
using RBod.PlayBall.GroupManagement.Web.Mappings;
using RBod.PlayBall.GroupManagement.Web.Models;

namespace RBod.PlayBall.GroupManagement.Web.Controllers
{
    [Route("groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService groupService;

        public GroupsController( IGroupsService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(this.groupService.Get().ToViewModels());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = this.groupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = this.groupService.Update(model.ToServiceModel());
            if (group == null)
            {
                return NotFound();
            }
            
            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreatePost")]
        public IActionResult Create(GroupViewModel model)
        {
            this.groupService.Add(model.ToServiceModel());
            return RedirectToAction(nameof(this.Index));
        }
    }
}