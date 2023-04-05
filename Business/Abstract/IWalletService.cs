using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IWalletService
    {
        IResult Add(Wallet wallet);
    }
}
