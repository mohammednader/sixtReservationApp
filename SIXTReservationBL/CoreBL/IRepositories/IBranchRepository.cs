using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IBranchRepository:IGenericRepository<Branch>
    {
        List<Branch> SearchBranch(BranchSC search);
        IList<Branch> GetUserBranches(int userId);
    }
}
