using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class WalletManager : IWalletService
    {
        private IWalletDal _walletDal;

        public WalletManager(IWalletDal walletDal)
        {
            _walletDal = walletDal;
        }

        public IResult Add(Wallet wallet)
        {
            _walletDal.Add(wallet);
            return new SuccessDataResult<Wallet>(Messages.WalletAdded);
        }
    }
}
