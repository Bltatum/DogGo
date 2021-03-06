﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Reopsitories;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {
        private readonly OwnerRepository _ownerRepo;
        private readonly DogRepository _dogRepo;

        public DogController(IConfiguration config)
        {
            _ownerRepo = new OwnerRepository(config);
            _dogRepo = new DogRepository(config);
        }

        // GET: DogController
        [Authorize]
        public ActionResult Index()
        {

            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {

            try
            {
                dog.OwnerId = GetCurrentUserId();
                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            int ownerId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);

            if (dog.OwnerId != ownerId)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Dog dog)
        {
            int ownerId = GetCurrentUserId();
            if(dog.OwnerId == ownerId)
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            else
            {
                return View(dog);
            }
        }
        [Authorize]
        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            int ownerId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);
            if(dog.OwnerId != ownerId)
            {
                return NotFound();
            }
            else
            { 
            return View(dog);
            }
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Delete(int id, Dog dog)
        {
            int ownerId = GetCurrentUserId();
            if(dog.OwnerId == ownerId)
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(dog);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
