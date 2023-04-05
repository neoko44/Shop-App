using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
    }
}
