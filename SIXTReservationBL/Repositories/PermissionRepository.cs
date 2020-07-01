using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class PermissionRepository:GenericRepository<Permission>,IPermissionRepository
    {
        public PermissionRepository(SixtReservationContext context):base(context)
        {

        }

        public List<Permission> GetPermissions(int userId)
        {
            var result = new List<Permission>();
            try
            {
                var user = Context.AppUser
                                  .SingleOrDefault(u => u.Id == userId && u.IsDeleted != true);
                if (user != null)
                {
                    if (user.IsSysAdmin == true)
                    {
                        result = Context.Permission
                                        .Where(p => p.IsDeleted != true).OrderBy(p => p.Order)
                                        .ToList();
                    }
                    else
                    {

                        result = Context.Permission.Where(p => p.IsDeleted != true &&
                        p.LnkRolePermission.Any(rp => rp.Role.LnkUserRole.Any(ur => ur.UserId == userId))
                        ).OrderBy(p => p.Order).ToList();

                        //result = Context.Role
                        //                   .Where(r =>
                        //                                r.IsDeleted != true
                        //                                && r.LnkUserRole.Any(u => u.UserId == userId)
                        //                          )
                        //                   .SelectMany(r => r.LnkRolePermission.Select(lnk => lnk.Permission))
                        //                   .ToList();
                    }
                }
            }
            catch
            { }
            return result;
        }

        public List<Permission> GetAllByRole(int roleId)
        {
            var result = new List<Permission>();
            try
            {
                result = Context.Role
                                .Where(r => r.Id == roleId && r.IsDeleted != true)
                                .SelectMany(r => r.LnkRolePermission.Select(lnk => lnk.Permission))
                                .OrderBy(r => r.Order)
                                .ToList();
            }
            catch { }

            return result;
        }

        public List<LnkRolePermission> GetRolePermissions(int roleId)
        {
            var result = new List<LnkRolePermission>();
            try
            {
                result = Context.LnkRolePermission
                                .Where(lnk => lnk.RoleId == roleId && lnk.Role.IsDeleted != true)
                                .ToList();
            }
            catch { }

            return result;
        }

        public bool UserHasPermission(int userId, string permissionName)
        {
            try
            {
                return Context.LnkUserRole
                              .Any(lur =>
                                            lur.UserId == userId
                                            && lur.Role.LnkRolePermission
                                                       .Any(lrp => lrp.Permission.Name == permissionName)
                                   );
            }
            catch
            {
                return false;
            }
        }

        public override List<Permission> GetAll()
        {
            return Context.Permission.Where(p => p.IsDeleted != true)
                                     .OrderBy(p => p.Order)
                                     .ToList();
        }
    }
}
