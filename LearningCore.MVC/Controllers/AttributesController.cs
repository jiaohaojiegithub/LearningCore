using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LearningCore.Common.Extentions;
using LearningCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearningCore.MVC.Controllers
{
    public class AttributesController : Controller
    {
        private readonly ILogger<AttributesController> _logger;
        private readonly LearningCoreContext _context;

        public AttributesController(ILogger<AttributesController> logger
            , LearningCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Attributes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Attributes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Attributes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attributes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!ModelState.IsValid)
                        {
                            var strerror = JsonSerializer.Serialize(ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => new { key = x.GetKeyValue("Key"), x.RawValue, x.Errors.First().ErrorMessage }).ToList());
                            return Content("<script>alert('未通过验证')</script>","text/html");
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        _logger.LogError(ex, "更新失败", collection);
                    }
                }

                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Attributes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attributes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
 
    }
}