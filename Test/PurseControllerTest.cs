﻿using System.Collections.Generic;
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
            mock.Verify(m => m.AddOperation(It.IsAny<RepositoryOperation>()));
        }
        [TestMethod]
        public void DeleteOperationTest()
        {
            Mock<IOperationRepository> mock = new Mock<IOperationRepository>();
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
            Month result = (Month)controller.Index().ViewData.Model;
            Assert.AreEqual(result.GetDay(1).SpanDaysSingleOperations[0].OperationName,"Novus");
            Assert.AreEqual(result.GetDay(1).SpanDaysSingleOperations[1].OperationName, "MacDonalds");
            Assert.AreEqual(result.GetDay(5).SpanDaysSingleOperations[0].OperationName, "Mouse");
        }
        [TestMethod]
        public void ChangeOperation()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            controller.ChangeOperation(1, "Ticket", 125);
            mock.Verify(x => x.ChangeOperation(It.IsAny<SingleOperation>(), 1));

        }
        [TestMethod]
        public void NextMonth()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            Month result = (Month)((ViewResult)controller.NextMonth(1, 2012)).ViewData.Model;
            Assert.AreEqual(result.GetThisMonth(), 2);
            Assert.AreEqual(result.GetThisYear(), 2012);
            Assert.AreEqual(result.GetDay(1).SpanDaysSingleOperations[0].OperationName, "MacDonalds");
            Assert.AreEqual(result.GetDay(5).SpanDaysSingleOperations[0].OperationName, "Mouse");
        }
        [TestMethod]
        public void PrevMonth()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            Month result = (Month)((ViewResult)controller.PrevMonth(11, 2012)).ViewData.Model;
            Assert.AreEqual(result.GetThisMonth(), 10);
            Assert.AreEqual(result.GetThisYear(), 2012);
        }
        [TestMethod]
        public void SpanStatistics()
        {
            Mock<IOperationRepository> mock = CreateRepository();
            Mock<IUserRepository> mockUser = CreateUserRepository();

            PurseController controller = new PurseController(mock.Object, mockUser.Object);
            Month result = (Month)((ViewResult)controller.SpanStatistics(1, 2012)).ViewData.Model;
            Assert.AreEqual(result.SpanStatistics().Sum(x=>x.Value), 120);
        }

        private Mock<IOperationRepository> CreateRepository()
        {
            Mock<IOperationRepository> mock = new Mock<IOperationRepository>();
            var model = new PreviewModel();
            model.AddDaySpanOperation(2012,1,1,new SingleOperation{Id = 0,OperationName = "Novus", Value = 40});
            model.AddDaySpanOperation(2012,1,1,new SingleOperation{Id = 1,OperationName = "MacDonalds", Value = 40});
            model.AddDaySpanOperation(2012,1,5,new SingleOperation{Id = 2,OperationName = "Mouse", Value = 40});
            model.AddDaySpanOperation(2012, 2, 1, new SingleOperation { Id = 3, OperationName = "MacDonalds", Value = 40 });
            model.AddDaySpanOperation(2012, 2, 5, new SingleOperation { Id = 4, OperationName = "Mouse", Value = 40 });
            model.SetCurrentMonth(1, 2012);
            mock.Setup(x => x.Model(It.IsAny<int>())).Returns(model);
            return mock;
        }
        private Mock<IUserRepository> CreateUserRepository()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            
            mock.Setup(x => x.GetUserID(null)).Returns(1);
            mock.Setup(x => x.GetUserName(It.IsAny<int>())).Returns("akorop");
            return mock;
        }
    }
}
