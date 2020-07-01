using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class BranchRepository:GenericRepository<Branch>,IBranchRepository
    {
        public BranchRepository(SixtReservationContext context):base(context)
        {
                
        }

        public List<Branch> SearchBranch(BranchSC search)
        {
            var result = new List<Branch>();
            try
            {
                var query = Context.Branch.AsQueryable();
                if(search != null)
                {
                    if (!string.IsNullOrEmpty(search.Name))
                    {
                        query = query.Where(b => b.Name!=null&&b.Name.Contains(search.Name));
                    }
                    if (!string.IsNullOrEmpty(search.Code))
                    {
                        query = query.Where(b => b.Code != null && b.Code == search.Code);
                    }
                    if (!string.IsNullOrEmpty(search.Email))
                    {
                        query = query.Where(b => b.Email != null && b.Email.Contains(search.Email));
                    }
                }
                result = query.ToList();
            }
            catch { }

            return result;
        }
        public IList<Branch> GetUserBranches(int userId)
        {
            var result = new List<Branch>();
            try
            {
                result = Context.Branch
                                .Where(r => r.LnkUserBranch.Any(lnk => lnk.UserId == userId))
                                .ToList();
            }
            catch { }
            return result;
        }
    }
}
