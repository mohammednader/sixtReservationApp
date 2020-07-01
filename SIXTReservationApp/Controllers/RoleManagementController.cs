using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models;
using SIXTReservationApp.Models.RoleManagement;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class RoleManagementController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;

        public RoleManagementController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult RoleIndex()
        {
            return View();
        }

        public PartialViewResult _RoleList(RoleSC search)
        {
            var roles = UnitOfWork.RoleBL.SearchRoles(search);
            var users = UnitOfWork.UserBL.GetAllWithSystemAdmin(u => roles.Any(r => r.CreatedBy == u.Id || r.LastModifiedBy == u.Id));
            var landingPages = UnitOfWork.PermissionBL.Find(p => roles.Any(r => r.DefaultPageId == p.Id));

            var model = roles.Select(r => new RoleVM(r)
            {
                CreatedBy = users.FirstOrDefault(u => u.Id == r.CreatedBy)?.FullName,
                LastModifiedBy = users.FirstOrDefault(u => u.Id == r.LastModifiedBy)?.FullName,
            }).ToList();
            return PartialView(model);
        }

        public IActionResult CreateRole()
        {
            ViewBag.Title = "Create Role";

            return View(new RoleDto());
        }

        public IActionResult UpdateRole(int id)
        {
            ViewBag.Title = "Update Role";
            var model = UnitOfWork.RoleBL.GetByID(id);
            return View("CreateRole", new RoleDto(model));
        }

        [HttpPost]
        public JsonResult CreateRole(RoleDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            else
            {
                var selectedIds = new List<int>();
                if (!string.IsNullOrEmpty(model.SelectedPermissions))
                {
                    selectedIds = model.SelectedPermissions
                                       .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToList();
                }
                var role = new Role();
                if (UnitOfWork.RoleBL.CheckExist(r => r.Name == model.Name && r.IsDeleted != true))
                {
                    return Json(new
                    {
                        Success = false,
                        ModelMessage = "Role name already exists",
                    });
                }
                role.Name = model.Name?.Trim();
                role.Description = model.Description?.Trim();
               // role.CreatedBy = LoggedUserId;
                role.CreationDate = DateTime.Now;

                var allPermissions = UnitOfWork.PermissionBL.GetAll();

                var selectedPermissions = allPermissions.Where(p => selectedIds.Contains(p.Id)).ToList();
                var grantedPermissions = new List<Permission>();
                for (int i = 0; i < selectedPermissions.Count; i++)
                {
                    var permission = selectedPermissions[i];
                    GetParentPermissions(permission, allPermissions, ref grantedPermissions);
                }

                role.LnkRolePermission = grantedPermissions.Select(p => new LnkRolePermission
                {
                    PermissionId = p.Id,
                }).ToList();

                UnitOfWork.RoleBL.Add(role);
                if (UnitOfWork.Complete() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "Role added successfully",
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Failed to add role",
                    });
                }
            }
        }

        [HttpPost]
        public JsonResult UpdateRole(RoleDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            else
            {
                var selectedIds = new List<int>();
                if (!string.IsNullOrEmpty(model.SelectedPermissions))
                {
                    selectedIds = model.SelectedPermissions
                                       .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToList();
                }
                var role = UnitOfWork.RoleBL.GetByID(model.Id);
                if (UnitOfWork.RoleBL.CheckExist(r => r.Id != model.Id && r.Name == model.Name && r.IsDeleted != true))
                {
                    return Json(new
                    {
                        Success = false,
                        ModelMessage = "Role name already exists",
                    });
                }
                role.Name = model.Name?.Trim();
                role.Description = model.Description?.Trim();
                //role.LastModifiedBy = LoggedUserId;
                role.LastModificationDate = DateTime.Now;

                UnitOfWork.RoleBL.Update(role);

                var oldPermissions = UnitOfWork.PermissionBL
                                               .GetAllByRole(model.Id)
                                               .ToList();

                var allPermissions = UnitOfWork.PermissionBL
                                               .GetAll()
                                               .ToList();

                var selectedPermissions = allPermissions.Where(p => selectedIds.Contains(p.Id)).ToList();
                var grantedPermissions = new List<Permission>();
                for (int i = 0; i < selectedPermissions.Count; i++)
                {
                    var permission = selectedPermissions[i];
                    GetParentPermissions(permission, allPermissions, ref grantedPermissions);
                }

                var toRemove = oldPermissions.Except(grantedPermissions)
                                             .Select(p => p.Id)
                                             .ToList();

                var toAdd = grantedPermissions.Except(oldPermissions)
                                              .Select(p => p.Id)
                                              .ToList();

                UnitOfWork.RoleBL.DetachPermissions(model.Id, toRemove);
                UnitOfWork.RoleBL.AttachPermissions(model.Id, toAdd);

                if (UnitOfWork.Complete() > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "Role updated successfully",
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Failed to update role",
                    });
                }
            }
        }

        public JsonResult GetAllPermissions(int? roleId = null)
        {
            var tree = new List<JsTreeVM>();

            var allPermissions = UnitOfWork.PermissionBL.GetAll();

            var grantedPermissions = UnitOfWork.PermissionBL.GetAllByRole(roleId.GetValueOrDefault());

            for (int i = 0; i < allPermissions.Count; i++)
            {
                var item = allPermissions[i];
                var node = new JsTreeVM
                {
                    Id = item.Id.ToString(),
                    State = new Models.TreeNodeStatus(),
                    Parent = "#",
                    Text = item.DisplayName,
                    CanBeLanding = !string.IsNullOrEmpty(item.PermissionUrl),
                };
                if (item.ParentId != null)
                {
                    node.Parent = item.ParentId.ToString();
                }
                if (grantedPermissions.Contains(item) && allPermissions.All(p => p.ParentId != item.Id))
                {
                    node.State.Opened = true;
                    node.State.Selected = true;
                }
                tree.Add(node);
            }
            return Json(new
            {
                Success = true,
                Data = tree
            });
        }

        [HttpDelete]
        public JsonResult DeleteRole(int id)
        {
            if (UnitOfWork.RoleBL.IsRelatedToUser(id))
            {
                return Json(new
                {
                    Success = false,
                    Message = "you can't delete this role"
                });
            }
            UnitOfWork.RoleBL.RemoveFound(r => r.Id == id);
            if (UnitOfWork.Complete() > 0)
            {
                return Json(new
                {
                    Success = true,
                    Message = "Role deleted successfully"
                });
            }
            else
            {
                return Json(new
                {
                    Success = false,
                    Message = "Failed to delete role",
                });
            }
        }

        private void GetParentPermissions(Permission permission, List<Permission> allPermissions, ref List<Permission> tree)
        {
            if (tree.Any(p => p.Id == permission.Id))
            {
                return;
            }
            tree.Add(permission);
            var parent = allPermissions.FirstOrDefault(p => p.Id == permission.ParentId);
            if (parent == null)
            {
                return;
            }
            GetParentPermissions(parent, allPermissions, ref tree);
        }
    }
}