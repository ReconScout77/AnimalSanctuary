using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using AnimalSanctuary.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnimalSanctuary.Controllers
{
    [TestClass]
    public class VeterinarianControllerTest
    {
        EFVeterinarianRepository db = new EFVeterinarianRepository(new TestDbContext());

        Mock<IVeterinarianRepository> mock = new Mock<IVeterinarianRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Veterinarians).Returns(new Veterinarian[]
            {
                new Veterinarian{Name="Steve", Specialty="Dogs"},
                new Veterinarian{Name = "Jill", Specialty="Lizards"},
                new Veterinarian{Name="Hannah", Specialty ="Fish"}

            }.AsQueryable());
        }

        private void DeleteAll()
        {
            TestDbContext db = new TestDbContext();
            db.Veterinarians.RemoveRange(db.Veterinarians.ToList());
            db.SaveChanges();
        }

        [TestMethod]
        public void DB_CreateNewEntry_Test()
        {
            VeterinarianController controller = new VeterinarianController(db);
            Veterinarian testVet = new Veterinarian();
            testVet.Name = "Steve";

            controller.Create(testVet);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Veterinarian>;

            CollectionAssert.Contains(collection, testVet);
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            VeterinarianController controller = new VeterinarianController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        [TestMethod]
        public void Mock_IndexListOfVets_Test()
        {
            DbSetup();
            ViewResult indexView = new VeterinarianController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Veterinarian>));
        }

        [TestMethod]
        public void Mock_AnimalDetail_Test()
        {
            DbSetup();
            Veterinarian testVet = new Veterinarian();
            testVet.Name = "Jane";
            testVet.Specialty = "Whatever!";

            ViewResult detailView = new VeterinarianController(mock.Object).Details(testVet.VeterinarianId) as ViewResult;
            var result = detailView.ViewData.Model;

            Assert.AreEqual(result,testVet);


        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            VeterinarianController vetController = new VeterinarianController(mock.Object);
            Veterinarian testVet = new Veterinarian();
            testVet.Name = "Jane";
            testVet.Specialty = "Whatever!";

            ViewResult indexView = vetController.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Veterinarian>;

            CollectionAssert.Contains(collection,testVet);
        }

  

        public void Dispose()
        {
            this.DeleteAll();
        }

    }

}
