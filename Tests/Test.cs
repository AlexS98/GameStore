using WebUI.Controllers;
using System.Web.Mvc;
using Xunit;
using GameStore.StoreDomain.Abstract;
using GameStore.StoreDomain.Entities;
using Moq;

namespace GameStore.Tests
{
    public class Test
    {
        [Fact]
        public void IndexTest()
        {
            var cabinetsMock = new Mock<IGenericRepository<UserCabinet>>();
            var usersMock = new Mock<IGenericRepository<User>>();
            HomeController controller = new HomeController(cabinetsMock.Object, usersMock.Object);
            ViewResult result = controller.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}
