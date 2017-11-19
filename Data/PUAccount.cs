using System.ComponentModel.DataAnnotations;

namespace PersistentUnreal.Data
{
    public class PUAccountRecord
    {
        [Key]
        public int AccountId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
    
    public class PUAccount
    {
        public PUAccount(PUAccountRecord account)
        {
            AccountId = account.AccountId;
            Username = account.Username;
        }

        public int AccountId { get; set; }

        public string Username { get; set; }
    }
}
