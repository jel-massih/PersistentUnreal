using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.Data;
using PersistentUnreal.Mediators;
using PersistentUnreal.Models;

namespace PersistentUnreal.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly PUAccountMediator m_AccountMediator;

        public AccountController(PUAccountMediator accountMediator)
        {
            m_AccountMediator = accountMediator;
        }

        // GET: api/Account/5
        [HttpGet("{accountId}", Name = "GetAccount")]
        public IActionResult GetAccount(int accountId)
        {
            var account = m_AccountMediator.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return new ObjectResult(account);
        }

        // PUT: api/Account/5
        [HttpPost("register")]
        public IActionResult RegisterAccount ([FromBody]AccountRegisterRequest accountRequest)
        {
            if (accountRequest == null)
            {
                return BadRequest();
            }
            
            return m_AccountMediator.RegisterAccount(accountRequest, Request);
        }
    }
}
