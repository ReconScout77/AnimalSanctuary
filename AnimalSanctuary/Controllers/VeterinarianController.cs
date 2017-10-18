using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuary.Controllers
{
    public class VeterinarianController : Controller
    {
        private IVeterinarianRepository veterinarianRepo;

        public VeterinarianController(IVeterinarianRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.veterinarianRepo = new EFVeterinarianRepository();
            }
            else
            {
                this.veterinarianRepo = thisRepo;
            }
        }

        public ViewResult Index()
        {
            return View(veterinarianRepo.Veterinarians.ToList());
        }

        public IActionResult Details(int id)
        {
            Veterinarian thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(x => x.VeterinarianId == id);
            return View(thisVeterinarian);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Veterinarian veterinarian)
        {
            veterinarianRepo.Save(veterinarian);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Veterinarian thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(x => x.VeterinarianId == id);
            return View(thisVeterinarian);
        }

        [HttpPost]
        public IActionResult Edit(Veterinarian veterinarian)
        {
            veterinarianRepo.Edit(veterinarian);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Veterinarian thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(x => x.VeterinarianId == id);
            return View(thisVeterinarian);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Veterinarian thisVeterinarian = veterinarianRepo.Veterinarians.FirstOrDefault(x => x.VeterinarianId == id);
            veterinarianRepo.Remove(thisVeterinarian);
            return RedirectToAction("Index");
        }
    }
}
