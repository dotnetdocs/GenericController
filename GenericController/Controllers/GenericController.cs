using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GenericController.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GenericController.Controllers
{
    public class GenericController<TEntity, IRepository> : Controller
        where TEntity : class, new()
        where IRepository : IGenericRepository<TEntity>
    {
        private readonly IRepository _repository;

        public GenericController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _repository.GetAll(cancellationToken);
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TEntity entity, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity, cancellationToken);
            }
            else return View(entity);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(cancellationToken, id);
            return View(data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TEntity entity, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            else return View(entity);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return View(await _repository.GetByIdAsync(cancellationToken, id));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TEntity entity, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(entity, cancellationToken);
            return View(nameof(Index));
        }
    }
}