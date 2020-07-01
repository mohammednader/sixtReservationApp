using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IUserRepository:IGenericRepository<AppUser>
    {
        AppUser Login(string email, string password);
        List<AppUser> SearchUsers(UserSC search);
        bool ValidateUser(AppUser user, out string errorMessage);
        AppUser GetUserWithDetails(int userId);
        List<AppUser> GetAllWithSystemAdmin(Expression<Func<AppUser, bool>> predicate = null);
        bool ResetPassword(int userId, string password, out string errorMessage);
        bool ChangePassword(string email, string password, string newPassword, out string errorMessage);
    }
}
