using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.Data;
using PersistentUnreal.Models;
using Sodium;

namespace PersistentUnreal.Mediators
{
    public class PUAccountMediator
    {
        private readonly PUAccountDbContext m_AccountContext;

        public PUAccountMediator(PUAccountDbContext accountContext)
        {
            m_AccountContext = accountContext;
        }

        public IActionResult RegisterAccount(AccountRegisterRequest accountRequest, HttpRequest request)
        {
            var validUsername = ValidUsername(accountRequest.Username);
            if(!validUsername)
            {
                return ResponseMessage
            }
            // var hash = PasswordHash.ArgonHashString()


            return true;
        }

        public PUAccount GetAccountByAccountId(int accountId)
        {
            var dbAccount = m_AccountContext.Accounts.FirstOrDefault(r => r.AccountId == accountId);

            if(dbAccount == null)
            {
                return null;
            }

            return new PUAccount(dbAccount);
        }

        private bool ValidUsername(string username)
        {
            return Regex.Match(username, "^(?=.{3,15}$)([A-Za-z0-9][._()\\[\\]-]?)*$").Success;
        }

        private bool UsernameTaken(string username)
        {
            return false;
        }

        private bool ValidPassword(string password)
        {
            return false;
        }
    }
}
