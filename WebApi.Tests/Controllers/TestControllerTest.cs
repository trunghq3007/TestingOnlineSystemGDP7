using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class TestControllerTest : Controller
    {
        [TestMethod]
        public void Delete()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            string result = controller.Put(14);


            // Assert
            Assert.AreEqual("1", result);

        }
        [TestMethod]
        public void Getbyid()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            //Test result = JsonConvert.DeserializeObject<Test>(controller.Get(5));


            // Assert
          //  Assert.AreEqual(5, result.Id);

        }
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
           
            var result = JsonConvert.DeserializeObject<List<Test>>(controller.Getall());


            // Assert
            Assert.AreEqual(3, result.Count());

        }
        [TestMethod]
        public void search()
        {
            // Arrange
            TestController controller = new TestController();

            // Act

            var result = JsonConvert.DeserializeObject<List<Test>>(controller.Get("java"));


            // Assert
            Assert.AreEqual(4, result.Count());

        }
        [TestMethod]
        public void Post()
        {
            // Arrange
            TestController controller = new TestController();
            var item = new Test() { ExamId = 5, SemasterExamId = 2,Status = 0,StartDate = DateTime.Now,EndDate = DateTime.Now,CreateBy = "ngoc",PassScore = 20,TestName = "java",TotalTest = 20,TestTime = 20  };
            // Act

            string result = controller.Post(JsonConvert.SerializeObject(item));


            // Assert
            Assert.AreEqual("1", result);

        }
        [TestMethod]
        public void Put1()
        {

            TestController controller = new TestController();
            var item = new Test() { ExamId = 5, SemasterExamId = 2, Status = 0, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateBy = "ngoc", PassScore = 20, TestName = "java", TotalTest = 20, TestTime = 20 };
            string result = controller.Put(5, JsonConvert.SerializeObject(item));
            Assert.AreEqual("1", result);
        }
    }
}