using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModel;
using DogGo.Reopsitories;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DogGo.Controllers
{
    
    public class WalkerController : Controller
    {
        private readonly WalkerRepository _walkerRepo;
        private readonly WalkRepository _walkRepo;
        private readonly DogRepository _dogRepo;
        private readonly OwnerRepository _ownerRepo;
        private readonly NeighborhoodRepository _neighborhoodRepo;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerController(IConfiguration config)
        {
            _walkerRepo = new WalkerRepository(config);
            _walkRepo = new WalkRepository(config);
            _dogRepo = new DogRepository(config);
            _ownerRepo = new OwnerRepository(config);
            _neighborhoodRepo = new NeighborhoodRepository(config);
        }
        // GET: WalkerController
        public ActionResult Index()
        {

            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            return View(walkers);
        }

        // GET: WalkerController/Details/5
       public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            Owner owner = _ownerRepo.GetOwnerById(id);
            Neighborhood neighborhood = _neighborhoodRepo.GetNeighborhoodById(walker.NeighborhoodId);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walk> walks = _walkRepo.GetWalkByWalkerId(walker.Id);

            WalkerDetailsViewModel vm = new WalkerDetailsViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walker = walker,
                Walk = walks,
                Neighborhood = neighborhood
            };
            return View(vm);
        }

        // GET: WalkerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
