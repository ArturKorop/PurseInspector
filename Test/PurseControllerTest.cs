using System.Collections.Generic;
using System.Linq;
using Domain.Purse;
using Domain.Repository;
using Domain.User;
using Moq;
using WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Domain.Abstract;
using System.Web.Mvc;

namespace Test
{


    [TestClass]
    public class PurseControllerTest
    {
        [TestMethod]
        public void AddOperationTest()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            controller.AddOperation(2012, 1, 5, "span", "KeyBoard", 100);
            mock.Verify(m => m.AddOperation(It.IsAny<RepositoryOperation>(),It.IsAny<int>()));
        }
        [TestMethod]
        public void DeleteOperationTest()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            controller.DeleteOperation(2);
            mock.Verify(m => m.RemoveOperation(2, 1));
        }
        [TestMethod]
        public void IndexTest()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            PreviewModel result = (PreviewModel)controller.Index().ViewData.Model;
            Assert.AreEqual(result.GetYear(2012).GetMonth(1).GetDay(1).SpanDaysSingleOperations[0].OperationName,"Novus");
      //      Assert.AreEqual(result.Years[0].Months[0].Days[0].SpanDaysSingleOperations[1].OperationName, "MacDonalds");
     //       Assert.AreEqual(result.Years[0].Months[0].Days[4].SpanDaysSingleOperations[0].OperationName, "Mouse");
        }

        private Mock<IOperationRepository> CreateRepository()
        {
            Mock<IOperationRepository> mock = new Mock<IOperationRepository>();
            mock.Setup(x => x.Repository(1)).Returns(new[]
                {
                    new RepositoryOperation
                        {
                            ID = 0,
                            Day = 1,
                            Month = 1,
                            OperationName = "Novus",
                            OperationType = "span",
                            OperationValue = 40,
                            UserID = 1,
                            UserName = "akorop",
                            Year = 2012
                        },
                    new RepositoryOperation
                        {
                            ID = 1,
                            Day = 1,
                            Month = 1,
                            OperationName = "MacDonalds",
                            OperationType = "span",
                            OperationValue = 36,
                            UserID = 1,
                            UserName = "akorop",
                            Year = 2012
                        },
                    new RepositoryOperation
                        {
                            ID = 2,
                            Day = 5,
                            Month = 1,
                            OperationName = "Mouse",
                            OperationType = "span",
                            OperationValue = 80,
                            UserID = 1,
                            UserName = "akorop",
                            Year = 2012
                        }
                }.AsQueryable());
            return mock;
        }
        private Mock<IUserRepository> CreateUserRepository()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(x => x.Repository()).Returns(new[]
                {
                    new UserInformation
                        {
                            UserId = 1,
                            UserName = "akorop"
                        },
                    new UserInformation
                        {
                            UserId = 2,
                            UserName = "root"
                        }
                }.AsQueryable());
            mock.Setup(x => x.GetUserID(null)).Returns(1);
            return mock;
        }
    }
}
