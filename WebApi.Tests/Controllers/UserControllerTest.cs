using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUser()
        {
            // Arrange
            UserController controller = new UserController();
            // Act
            var result = JsonConvert.DeserializeObject<List<User>>(controller.GetUser());
            // Assert
            Assert.AreEqual(12, result.Count());
        }

        [TestMethod]
        public void GET()
        {
            // Arrange
            UserController controller = new UserController();
            // Act
            var result = JsonConvert.DeserializeObject<List<UserDetail>>(controller.GET(1));
            // Assert
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void Post()
        {
            // Arrange
            UserController controller = new UserController();
            var item = new User
            {
                UserName = "HTHang4",
                RoleId = 1,
                Password = "hthang",
                CreatedDate = DateTime.Now,
                FullName = "Hoang Thi Hang",
                Phone = "0123456",
                Email = "hthang@cmc.com.vn",
                Address = "Bac Giang",
                Department = "CMC Global",
                Position = "GDP07",
                Avatar = "AvtHthang",
                Note = "She is a Imployee",
                Status = false
            };
            // Act

            string result = controller.AddUser(JsonConvert.SerializeObject(item));


            // Assert
            Assert.AreEqual("1", result);

        }
        [TestMethod]
        public void Update()
        {
            // Arrange
            UserController controller = new UserController();
            var item = new User
            {
                UserName = "HTHang5",
                RoleId = 1,
                Password = "hthang",
                CreatedDate = DateTime.Now,
                FullName = "Hoang Thi Hang",
                Phone = "0123456",
                Email = "hthang@cmc.com.vn",
                Address = "Bac Giang",
                Department = "CMC Global",
                Position = "GDP07",
                Avatar = "AvtHthang",
                Note = "She is a Imployee",
                Status = true
            };
            // Act

            string result = controller.Update(26, JsonConvert.SerializeObject(item));

            // Assert
            Assert.AreEqual("1", result);

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            UserController controller = new UserController();
            // Act
            var result = controller.Delete(29);
            // Assert
            Assert.AreEqual("1", result);
        }


        [TestMethod]
        public void Search()
        {
            // Arrange
            UserController controller = new UserController();
            // Act
            var result = JsonConvert.DeserializeObject<List<User>>(controller.Get("Hang"));
            // Assert
            Assert.AreEqual(3, result.Count());
        }
    }
}
