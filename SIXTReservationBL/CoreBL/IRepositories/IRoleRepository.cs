using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IRoleRepository:IGenericRepository<Role>
    {
        IList<Role> GetUserRoles(int userId);
        List<Role> SearchRoles(RoleSC search);
        List<Role> GetUserRoles(List<int> users);
        void AttachPermissions(int roleId, List<int> permissions);
        void DetachPermissions(int roleId, List<int> permissions = null);
        bool IsRelatedToUser(int roleId);

    }
}
