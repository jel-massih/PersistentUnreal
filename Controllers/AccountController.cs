using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.Helpers;
using PersistentUnreal.Mediators;
using PersistentUnreal.ViewModels;
using System.Net;

namespace PersistentUnreal.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Account")]
    public class AccountController : Controller
    {
        private readonly IPUAccountMediator m_AccountMediator;

        public AccountController(IPUAccountMediator accountMediator)
        {
            m_AccountMediator = accountMediator;
        }

        // GET: api/Account/5
        [HttpGet("{accountId}")]
        public IActionResult GetAccount(int accountId)
        {
            var account = m_AccountMediator.GetAccountByAccountId(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return new ObjectResult(account);
        }

        // POST: api/Account/register
        [HttpPost("register")]
        [ApiValidationFilter]
        public IActionResult RegisterAccount ([FromBody]AccountRegisterRequest accountRequest)
        {
            if (accountRequest == null)
            {
                return BadRequest();
            }
            
            var resp = m_AccountMediator.RegisterAccount(accountRequest);

            if(resp.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest(resp);
            }
        }
    }
}
