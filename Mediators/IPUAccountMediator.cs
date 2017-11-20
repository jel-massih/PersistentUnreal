using PersistentUnreal.Data;
using PersistentUnreal.ViewModels;

namespace PersistentUnreal.Mediators
{
    public interface IPUAccountMediator
    {
        BaseApiResponse RegisterAccount(AccountRegisterRequest accountRequest);
        PUAccount GetAccountByAccountId(int accountId);
    }
}
