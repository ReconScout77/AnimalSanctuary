using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalSanctuary.Models;
using Moq;

namespace AnimalSanctuaryTests
{
    [TestClass]
    public class AnimalTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var animal = new Animal();

            animal.Name = "Pennywise";
            var result = animal.Name;

            Assert.AreEqual("Pennywise", result);
        }
    }
}
