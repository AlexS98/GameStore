﻿using WebUI.Controllers;
using System.Web.Mvc;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void IndexTest()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}
