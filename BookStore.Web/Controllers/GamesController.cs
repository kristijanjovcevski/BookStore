using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.PartnerDomain;
using BookStore.Repository;
using BookStore.Service.Interface;

namespace BookStore.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService gameService;
        private readonly IDeveloperService developerService;

        public GamesController(IGameService gameService, IDeveloperService developerService)
        {
            this.gameService = gameService;
            this.developerService = developerService;
        }



        // GET: Games
        public IActionResult Index()
        {
            return View(gameService.GetGames());
        }

        // GET: Games/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = gameService.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            var developer = developerService.GetDevelopers()
                .FirstOrDefault(s => s.Id == game.DeveloperId);
            ViewData["DeveloperName"] = developer.DevName;
            
            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(developerService.GetDevelopers(), "Id", "DevName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GameTitle,GameDesc,DateReleased,GameImage,Rating,Price,DeveloperId,Id")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.Id = Guid.NewGuid();
                gameService.CreateNewGame(game);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(developerService.GetDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // GET: Games/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = gameService.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(developerService.GetDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("GameTitle,GameDesc,DateReleased,GameImage,Rating,Price,DeveloperId,Id")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    gameService.UpdateGame(game);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["DeveloperId"] = new SelectList(developerService.GetDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = gameService.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var game = gameService.GetGameById(id);
            if (game != null)
            {
                gameService.DeleteGame(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return gameService.GetGames().Any(e => e.Id == id);
        }
    }
}
