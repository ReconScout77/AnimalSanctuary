using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using AnimalSanctuary.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnimalSanctuaryTests
{
    [TestClass]
    public class AnimalControllerTest : IDisposable
    {
        EFAnimalRepository db = new EFAnimalRepository(new TestDbContext());

        Mock<IAnimalRepository> mock = new Mock<IAnimalRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Animals).Returns(new Animal[]
            {
                new Animal{Name = "spot", HabitatType = "kennel", Sex = "M", MedicalEmergency = false, Species = "doggo", VeterinarianId = 2},
                new Animal{Name = "dot", HabitatType = "kennel", Sex = "F", MedicalEmergency = true, Species = "doggo", VeterinarianId = 2},
                new Animal{Name = "grape", HabitatType = "kennel", Sex = "M", MedicalEmergency = true, Species = "penguin", VeterinarianId = 2},
            }.AsQueryable());
        }

        private void DeleteAll()
        {
            TestDbContext db = new TestDbContext();
            db.Animals.RemoveRange(db.Animals.ToList());
            db.SaveChanges();
        }

        [TestMethod]
        public void DB_CreateNewEntry_Test()

        {
            // Arrange
            AnimalController controller = new AnimalController(db);
            Animal testAnimal = new Animal();
            testAnimal.Name = "TestDb Animal Name";
            testAnimal.VeterinarianId = 2;

            // Act
            controller.Create(testAnimal);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Animal>;

            // Assert
            CollectionAssert.Contains(collection, testAnimal);
        }

        [TestMethod]
        public void DB_EditMedicalState_Test()
        {
            AnimalController controller = new AnimalController(db);
            Animal testAnimal = new Animal();
            testAnimal.Name = "TestDb Animal Name";
            testAnimal.VeterinarianId = 2;
            testAnimal.MedicalEmergency = false;

            controller.Create(testAnimal);

            // Act
			testAnimal.MedicalEmergency = true;
            controller.Edit(testAnimal);

            ViewResult detailView = new AnimalController(db).Details(testAnimal.AnimalId) as ViewResult;
            var result = detailView.ViewData.Model as Animal;

            // Assert
            Assert.AreEqual(true, result.MedicalEmergency);
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            AnimalController controller = new AnimalController(mock.Object);

            var result = controller.Index();
           
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexListOfAnimals_Test()
        {
            DbSetup();
            ViewResult indexView = new AnimalController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Animal>));
        }

        [TestMethod]
        public void Mock_AnimalDetail_Test()
        {
            DbSetup();
            Animal testAnimal = new Animal();
            testAnimal.Name = "Peppa Pigsworth";
            testAnimal.HabitatType = "test habitat";
            testAnimal.Sex = "M";
            testAnimal.Species = "Pig";
            testAnimal.MedicalEmergency = true;
            testAnimal.VeterinarianId = 2;

            ViewResult detailView = new AnimalController(mock.Object).Details(testAnimal.AnimalId) as ViewResult;
            var result = detailView.ViewData.Model;

            Assert.AreEqual(result,testAnimal);
        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            AnimalController controller = new AnimalController(mock.Object);
            Animal testAnimal = new Animal();
            testAnimal.Name = "Peppa Pigsworth";
            testAnimal.HabitatType = "test habitat";
            testAnimal.Sex = "M";
            testAnimal.Species = "Pig";
            testAnimal.MedicalEmergency = true;
            testAnimal.VeterinarianId = 2;

            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Animal>;

            CollectionAssert.Contains(collection,testAnimal);
        }

        [TestMethod]
        public void Mock_MedicalView_Test()
        {
            DbSetup();

            ViewResult medicalView = new AnimalController(mock.Object).ViewMedical() as ViewResult;
            var result = medicalView.ViewData.Model as List<Animal>;
            var expected = mock.Object.Animals.Where(x => x.MedicalEmergency == true).ToList();
            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Db_Delete_Test()
        {
            AnimalController controller = new AnimalController(db);
            Animal testAnimal = new Animal();
            testAnimal.Name = "TestDb Animal Name";
            testAnimal.VeterinarianId = 2;

            controller.Create(testAnimal);
            controller.DeleteConfirmed(testAnimal.AnimalId);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Animal>;

            CollectionAssert.DoesNotContain(collection, testAnimal);
        }

        public void Dispose()
        {
            this.DeleteAll();
        }
    }
}
