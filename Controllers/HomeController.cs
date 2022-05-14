using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MeToMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MeToMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.allMovies = _context.Movies.OrderBy(a => a.Title).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("addMovie")]
        public IActionResult addMovie(Movie newMovie)
        {
            if(ModelState.IsValid)
            {
                _context.Add(newMovie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }else{
                ViewBag.allMovies = _context.Movies.OrderBy(a => a.Title).ToList();
                return View("Index");
            }
        }
        [HttpGet("actors")]
        public IActionResult Actors()
        {
            ViewBag.allActors = _context.Actors.OrderBy(s => s.LastName).ToList();
            return View();
        }

        [HttpPost("addActor")]
        public IActionResult addActor (Actor newActor)
        {
            if(ModelState.IsValid)
            {
                _context.Add(newActor);
                _context.SaveChanges();
                return RedirectToAction("Actors");
            }
            else{
                ViewBag.allActors = _context.Actors.OrderBy(s => s.LastName).ToList();
                return View("Actors");
            }
        }
        [HttpGet("movie/{movieId}")]
        public IActionResult OneMovie (int movieId)
        {
            Movie one = _context.Movies.Include(f => f.CastList).ThenInclude(j => j.Actor).FirstOrDefault(d => d.MovieId == movieId);
            ViewBag.allActors = _context.Actors.OrderBy(s => s.LastName).ToList();
            return View(one);
        }

        [HttpPost("addToCast")]
        public IActionResult addToCast (Cast newRole)
        {
            _context.Add(newRole);
            _context.SaveChanges();
            return Redirect($"/movie/{newRole.MovieId}");
        }

        [HttpGet("actor/{actId}")]
        public IActionResult OneActor (int actId)
        {
            Actor oneActor = _context.Actors.Include(f => f.ActedIn).ThenInclude(j => j.Movie).FirstOrDefault(d => d.ActorId == actId);
            ViewBag.allMovies = _context.Movies.OrderBy(s => s.Title).ToList();
            return View(oneActor);
        }

        [HttpPost("addToFilm")]
        public IActionResult addToFilm (Cast newRole)
        {
            _context.Add(newRole);
            _context.SaveChanges();
            return Redirect($"/actor/{newRole.ActorId}");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
