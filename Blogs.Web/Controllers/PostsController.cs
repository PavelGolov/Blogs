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

namespace Blogs.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostManager _postManager;
        private readonly Markdown _markdown;
        public PostsController(PostManager postManager, Markdown markdown)
        {
            _postManager = postManager;
            _markdown = markdown;
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
                HtmlDesc = p.HtmlDesc,
                ShortHtmlDesc = PostHelper.GetShortHtmlDesc(p.HtmlDesc)
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
                HtmlDesc = post.HtmlDesc
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
                _markdown.Set(createViewModel.MdDesc);
                var post = new Post
                {
                    Category = createViewModel.Category,
                    Title = createViewModel.Title,
                    MdDesc = createViewModel.MdDesc,
                    ReleaseDate = DateTime.Now,
                    HtmlDesc = _markdown.ToHtml()
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
            _markdown.Set(editViewModel.MdDesc);
            post.HtmlDesc = _markdown.ToHtml();
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
