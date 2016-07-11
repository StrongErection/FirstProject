using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerTeams.Controllers;
using System;
using System.Web.Mvc;
using Moq;
namespace SoccerTeams.Controllers.Tests
{
    [TestClass()]
    public class UnitTests
    {
        private HomeController controller;
        private ViewResult result;
        private int playerId = 3;
        private int teamId = 3;

        [TestInitialize()]
        public void SetupContext()
        {
            controller = new HomeController();
        }


        [TestMethod()]
        public void IndexTestResultNotNull()
        {
            result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void ErrorTestResultNotNull()
        {
            result = controller.Error() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod()]
        public void CreateTestResultNotNull()
        {
            result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void EditTestResultNotNull()
        {
            result = controller.Edit(playerId) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod()]
        public void DeleteTestResultNotNull()
        {
            result = controller.Delete(playerId) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod()]
        public void AjaxTestTestResultNotNull()
        {
            result = controller.AjaxTest(20, 60) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("AjaxTest", result.ViewName);
        }

        [TestMethod()]
        public void TeamDetailsTestResultNotNull()
        {
            result = controller.TeamDetails(teamId) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("TeamDetails", result.ViewName);
        }
    }
}