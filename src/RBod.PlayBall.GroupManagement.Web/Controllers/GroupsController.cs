﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RBod.PlayBall.GroupManagement.Web.Demo;
using RBod.PlayBall.GroupManagement.Web.Models;

namespace RBod.PlayBall.GroupManagement.Web.Controllers
{
    [Route("groups")]
    public class GroupsController : Controller
    {
        private IGroupIdGenerator idGenerator;
        private static List<GroupViewModel> groups = new List<GroupViewModel>
        {
            new GroupViewModel { Id = 1, Name = "First"},
        };

        public GroupsController(IGroupIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = groups.SingleOrDefault(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = groups.SingleOrDefault(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            group.Name = model.Name;

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
            model.Id = this.idGenerator.Next();
            groups.Add(model);
            return RedirectToAction(nameof(this.Index));
        }
    }
}