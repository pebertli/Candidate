using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICandidate.Model;

namespace APICandidate.Controllers
{
    [Produces("text/plain")]
    [Route("api/UpdateInfo")]
    public class UpdateInfoController : Controller
    {
        private readonly DBContext _context;

        public UpdateInfoController(DBContext context)
        {
            _context = context;
        }

        // GET: api/UpdateInfo
        [HttpGet]
        public string GetUpdateInfo()
        {
            return _context.UpdateInfo.Single().LastUpdate.ToString();
        }

       
    }
}