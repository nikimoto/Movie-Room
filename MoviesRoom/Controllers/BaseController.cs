using MoviesRoom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesRoom.Controllers
{
     public class BaseController : Controller
    {
        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        protected IUowData Data { get; set; }
    }
}