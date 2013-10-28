using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesRoom.Controllers;
using MoviesRoom.Data;
using MoviesRoom.Models;
using MoviesRoom.ViewModels.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MoviesRoom.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private List<Film> data;

        [TestInitialize]
        public void Initialize()
        {
            var cat = new Category
            {
                Name = "Movie Category"
            };

            this.data = new List<Film>();
            this.data.Add(new Film()
                {
                    Title = "The One",
                    StartDate = DateTime.Now.AddDays(10),
                    Category = cat
                });
            this.data.Add(new Film()
                {
                    Title = "The Hunger Games",
                    StartDate = DateTime.Now.AddDays(10),
                    Category = cat
                });
            this.data.Add(new Film()
                {
                    Title = "Avatar",
                    StartDate = DateTime.Now.AddDays(10),
                    Category = cat
                });
            this.data.Add(new Film()
                {
                    Title = "Lawless",
                    StartDate = DateTime.Now.AddDays(-10),
                    Category = cat
                });
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.data.Clear();
        }

        [TestMethod]
        public void IndexMethodTestWithNormalRequest_ShouldReturn3Of4ThatHaveFutureDate()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index(null);

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(3, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_EmptySearch_ShouldReturn3Of4ThatHaveFutureDate()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(3, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_SearchForFilmsWithTheInTitle_ShouldReturn2()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("the");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_LowercaseSearchForFilmsWithAvatarInTitle_ShouldReturn1()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("avatar");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_PascalcaseSearchForFilmsWithAvatarInTitle_ShouldReturn1()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("Avatar");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_SearchForFilmsWithTelerikInTitle_ShouldReturn0()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("telerik");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(0, model.Count());
        }

        [TestMethod]
        public void IndexMethodTestWithAjaxRequest_SearchForFilmLawlessThatHasPastDate_ShouldReturn0()
        {
            var filmsRepoMock = new Mock<IRepository<Film>>();
            filmsRepoMock.Setup(x => x.All()).Returns(this.data.AsQueryable());

            var uowMock = new Mock<IUowData>();
            uowMock.Setup(x => x.Films).Returns(filmsRepoMock.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "X-Requested-With", "XMLHttpRequest" }
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new HomeController(uowMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = controller.Index("Lawless");

            Assert.IsNotNull(viewResult, "ViewResult is Null!");

            var model = viewResult.Model as IEnumerable<FilmViewModel>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(0, model.Count());
        }
    }
}