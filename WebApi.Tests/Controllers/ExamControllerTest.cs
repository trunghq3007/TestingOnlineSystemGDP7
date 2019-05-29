using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using Model;
using Newtonsoft.Json;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class ExamControllerTest
    {
        [TestMethod]
        public void GetExam()
        {
            ExamController controller = new ExamController();

            // Act
            Exam result = JsonConvert.DeserializeObject<Exam>(controller.GetExam(4));


            // Assert
            Assert.AreEqual(4, result.Id);

        }
        [TestMethod]
        public void search()
        {
            // Arrange
            ExamController controller = new ExamController();

            // Act

            var result = JsonConvert.DeserializeObject<List<Exam>>(controller.Get("JAVA")).ToList();


            // Assert
            Assert.AreEqual(3, result.Count());

        }
        [TestMethod]
        public void InsertExam()
        {
            ExamController controller = new ExamController();
            var item = new Exam { NameExam = "JavaCore1", CreateBy = "ltcuong", QuestionNumber = 10, Status = false, CreateAt = DateTime.Now, Note = "null" };
            // Act

            string result = controller.InsertExam(JsonConvert.SerializeObject(item));


            // Assert
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void Update()
        {
            ExamController controller = new ExamController();
            var item = new Exam { NameExam = "JavaCore1", CreateBy = "ltcuong123", QuestionNumber = 10, Status = false, CreateAt = DateTime.Now, Note = "null" };
            string result = controller.Update(4, JsonConvert.SerializeObject(item));
            Assert.AreEqual("1", result);
        }
        [TestMethod]
        public void Delete()
        {
            ExamController controller = new ExamController();
            string result = controller.Delete(9);

            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void Filter()
        {
            // Arrange
            ExamController controller = new ExamController();

            // Act

            var result = JsonConvert.DeserializeObject<List<Exam>>(controller.Filter("filter", "JAVA")).ToList();


            // Assert
            Assert.AreEqual(1, result.Count());
        }



    }
}