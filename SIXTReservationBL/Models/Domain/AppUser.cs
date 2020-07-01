using System;
using System.Collections.Generic;

namespace SIXTReservationBL.Models.Domain
{
    public partial class AppUser
    {
        public AppUser()
        {
            LnkUserBranch = new HashSet<LnkUserBranch>();
            LnkUserRole = new HashSet<LnkUserRole>();
            Notification = new HashSet<Notification>();
            ReservationAssignementFromUserNavigation = new HashSet<ReservationAssignement>();
            ReservationAssignementToUserNavigation = new HashSet<ReservationAssignement>();
            ReservationFormSubmit = new HashSet<ReservationFormSubmit>();
            UploadLog = new HashSet<UploadLog>();
        }

        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSysAdmin { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public bool? IsChangedPassword { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        public bool? IsPhoneNumberConfirmed { get; set; }
        public bool? IsTwoFactorEnabled { get; set; }
        public bool? IsDeleted { get; set; }
        public int? JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<LnkUserBranch> LnkUserBranch { get; set; }
        public virtual ICollection<LnkUserRole> LnkUserRole { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<ReservationAssignement> ReservationAssignementFromUserNavigation { get; set; }
        public virtual ICollection<ReservationAssignement> ReservationAssignementToUserNavigation { get; set; }
        public virtual ICollection<ReservationFormSubmit> ReservationFormSubmit { get; set; }
        public virtual ICollection<UploadLog> UploadLog { get; set; }
    }
}
