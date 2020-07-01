using Microsoft.EntityFrameworkCore;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Helper;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SIXTReservationBL.Repositories
{
   public class UserRepository:GenericRepository<AppUser>,IUserRepository
    {
        public UserRepository(SixtReservationContext context):base(context)
        {

        }
        public AppUser Login(string email, string password)
        {
            var passHash = PEncryption.Encrypt(password);
            var user = FindOne(u => u.Email == email && u.PasswordHash == passHash && u.IsDeleted != true);
            if (user != null)
            {
                Context.Entry(user).Reference(u => u.JobTitle).Load();
               // Context.Entry(user).Reference(u => u.Branch).Load();
            }
            return user;
        }

        public List<AppUser> SearchUsers(UserSC search)
        {
            var result = new List<AppUser>();
            try
            {
                var query = Context.AppUser
                                   .Where(u => u.IsDeleted != true && u.IsSysAdmin != true)
                                   .Include(u => u.JobTitle)
                                   .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrWhiteSpace(search.Name))
                    {
                        query = query.Where(u => u.FullName != null && u.FullName.Contains(search.Name));
                    }
                    if (!string.IsNullOrWhiteSpace(search.Email))
                    {
                        query = query.Where(u => u.Email != null && u.Email.Contains(search.Email));
                    }
                    if (!string.IsNullOrWhiteSpace(search.PhoneNumber))
                    {
                        query = query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(search.PhoneNumber));
                    }
                    if (search.Role?.Count > 0 && !search.Role.Contains(0))
                    {
                        query = query.Where(u => u.LnkUserRole.Any(lnk => search.Role.Contains(lnk.RoleId)));
                    }
                    if (search.JobTitle != null && search.JobTitle > 0)
                    {
                        query = query.Where(u => u.JobTitle != null && u.JobTitle.JobTitleId==search.JobTitle);
                    }
                   
                    if (search.Status > 0)
                    {
                        query = query.Where(u => (search.Status == 1 && u.IsActive == true)
                                                || (search.Status == 2 && u.IsActive != true));
                    }
                }
                result = query.ToList();
            }
            catch { }
            return result.OrderByDescending(p => p.Id).ToList();
        }

        public bool ValidateUser(AppUser user, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool result = true;

          
            if (CheckExist(u => u.Id != user.Id && u.Email == user.Email))
            {
                result = false;
                errorMessage = "Email already exists";
            }
            //else if (CheckExist(u => u.Id != user.Id && u.PhoneNumber == user.PhoneNumber))
            //{
            //    result = false;
            //    errorMessage = "Phone number already exists";
            //}
            else
            {
              //  var jobtitle = Context.JobTitle.Find(user.JobTitleId);
                //if ((user.JobTitleId==(int) JobTitleEnum.BranchAgent || user.JobTitleId == (int)JobTitleEnum.BranchManager) && (user.LnkUserBranch==null || user.LnkUserBranch.Count()==0 ))
                //{
                //    result = false;
                //    errorMessage = "Please select at least one branch";
                //}
            }

            return result;
        }

        public AppUser GetUserWithDetails(int userId)
        {
            var model = Context.AppUser
                                    .Where(f => f.Id == userId)
                                    .Include(u => u.LnkUserRole)
                                    .Include(u=>u.LnkUserBranch)
                                    .FirstOrDefault();
            return model;
        }
        public List<AppUser> GetAllWithSystemAdmin(Expression<Func<AppUser, bool>> predicate = null)
        {
            var result = new List<AppUser>();
            try
            {
                var query = Context.AppUser
                                  .Where(u => u.IsDeleted != true)
                                  .Include(u => u.JobTitle)
                                  .AsQueryable();


                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                result = query.ToList();
            }
            catch (Exception e)
            { }
            return result;
        }
        public bool ResetPassword(int userId, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            var user = GetByID(userId);
            if (user != null)
            {
                string newHash = PEncryption.Encrypt(password);
                user.PasswordHash = newHash;
                Update(user);
                return true;
            }
            else
            {
                errorMessage = "User not found";
                return false;
            }
        }

        public bool ChangePassword(string email, string password, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;
            string oldHash = PEncryption.Encrypt(password);

            var user = FindOne(u => u.IsActive == true && u.Email.ToLower() == email.ToLower() && u.PasswordHash == oldHash);
            if (user != null)
            {
                string newHash = PEncryption.Encrypt(newPassword);
                if (oldHash == newHash)
                {
                    errorMessage = "Please enter new password";
                    return false;
                }
                user.PasswordHash = newHash;
                user.IsChangedPassword = true;
                Update(user);
                return true;
            }
            else
            {
                errorMessage = "Incorrect password";
                return false;
            }
        }





    }
}
