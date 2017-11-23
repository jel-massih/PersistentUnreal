using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.Data;
using PersistentUnreal.ViewModels;
using Sodium;

namespace PersistentUnreal.Mediators
{
    public class PUAccountMediator : IPUAccountMediator
    {
        private readonly PUAccountDbContext m_AccountContext;

        public PUAccountMediator(PUAccountDbContext accountContext)
        {
            m_AccountContext = accountContext;
        }

        public BaseApiResponse RegisterAccount(AccountRegisterRequest accountRequest)
        {
            var validUsername = ValidUsername(accountRequest.Username);
            if(!validUsername)
            {
                return new BaseApiResponse(HttpStatusCode.BadRequest, "Please enter a valid username.");
            }

            //check if username exists
            var usernameExists = UsernameTaken(accountRequest.Username);
            if(usernameExists)
            {
                return new BaseApiResponse(HttpStatusCode.BadRequest, $"Username {accountRequest.Username} is already taken.");
            }

            var validPassword = ValidPassword(accountRequest.Password);
            if(!validPassword)
            {
                return new BaseApiResponse(HttpStatusCode.BadRequest, "Please enter a valid password");
            }

            var passwordSaltBytes = PasswordHash.ArgonGenerateSalt();
            var passwordSalt = System.Text.Encoding.ASCII.GetString(passwordSaltBytes);

            var saltedPassword = accountRequest.Password + passwordSalt;

            var passwordHash = PasswordHash.ArgonHashString(saltedPassword, PasswordHash.StrengthArgon.Interactive);

            var newAccount = new PUAccountRecord
            {
                Username = accountRequest.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            m_AccountContext.Accounts.Add(newAccount);
            m_AccountContext.SaveChanges();

            return new ApiOkResponse();
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
            return m_AccountContext.Accounts.FirstOrDefault(r => r.Username == username) != null;
        }

        private bool ValidPassword(string password)
        {
            return Regex.Match(password, "^(?=.{3,15}$)([A-Za-z0-9][._()\\[\\]-]?)*$").Success;
        }
    }
}
