using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ClaimManager : IClaimService
    {
        private IUserOperationClaimDal _roleclaim;

        public ClaimManager(IUserOperationClaimDal roleClaim)
        {
            _roleclaim = roleClaim;
        }

        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _roleclaim.Add(userOperationClaim);
            return new SuccessDataResult<UserOperationClaim>(Messages.RoleAdded);
        }
    }
}
