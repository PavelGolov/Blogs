using System;
using System.Linq;
using Blogs.Web.Managers;
using Blogs.Core.Markdown;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogs.SharedKernel.Models;
using System.Collections.Generic;
using Blogs.Web.ViewModels.Posts;
using Blogs.Core.Helpers;
using Blogs.Core.Intefaces;

namespace Blogs.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostManager _postManager;
        private readonly IParser _markdown;
        private readonly IPostRedactor _postRedactor;
        public PostsController(PostManager postManager, IParser markdown, IPostRedactor postRedactor)
        {
            _postManager = postManager;
            _markdown = markdown;
            _postRedactor = postRedactor;
        }


        public IActionResult Index()
        {
            var posts = _postManager.List().OrderByDescending(s => s.ReleaseDate).ToList();
            var indexViewModels = posts.Select(p => new IndexViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ReleaseDate = p.ReleaseDate,
                Category = p.Category,
                ShortHtmlDesc = _postRedactor.GetShortHtmlDesc(_markdown.Parse(p.MdDesc))
            }).ToList();
            return View(indexViewModels);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postManager.GetPost((int)id);
            if (post == null)
            {
                return NotFound();
            }
            var detailsViewModel = new DetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                ReleaseDate = post.ReleaseDate,
                Category = post.Category,
                HtmlDesc = _markdown.Parse(post.MdDesc)
            };
            return View(detailsViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Category = createViewModel.Category,
                    Title = createViewModel.Title,
                    MdDesc = createViewModel.MdDesc,
                    ReleaseDate = DateTime.Now
                };
                _postManager.Insert(post);
                return RedirectToAction(nameof(Index));
            }
            return View(createViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postManager.GetPost((int)id);
            if (post == null)
            {
                return NotFound();
            }
            var editViewModel = new EditViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Category = post.Category,
                MdDesc = post.MdDesc
            };
            return View(editViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            if (id != editViewModel.Id)
            {
                return NotFound();
            }
            var post = _postManager.GetPost(id);
            post.Title = editViewModel.Title;
            post.Category = editViewModel.Category;
            post.MdDesc = editViewModel.MdDesc;
            if (ModelState.IsValid)
            {
                try
                {
                    _postManager.Update(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_postManager.Exists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var post = _postManager.GetPost(id);
            _postManager.Delete(post);
            return RedirectToAction(nameof(Index));
        }
    }
}
