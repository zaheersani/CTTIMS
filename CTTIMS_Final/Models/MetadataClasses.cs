using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CTTIMS_Final.Models
{
    public class UserMetadata
    {
        [Required(ErrorMessage = "Username Cannot be Empty")]
        [Display(Name = "User Name")]
        public string UserName;

        [Required(ErrorMessage = "Password Cannot be Empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UPassword;

        [ScaffoldColumn(false)]
        public string Role { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> LastAccessedOn { get; set; }

        [ScaffoldColumn(false)]
        public string Status { get; set; }
    }

    public class BatchMetadata
    {
        [Display(Name = "Batch Name")]
        public string Name;

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public Nullable<System.DateTime> StartDt;

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> EndDt;

        //[EnumDataType(typeof(BatchStatus))]
        //public string Status { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;

        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class InstructorMetadata
    {
        [ScaffoldColumn(false)]
        public Nullable<int> uID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        [EnumDataType(typeof(Designation))]
        public string Designation { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string Email { get; set; }
        public string DeptPosition { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentCity { get; set; }
        public string PermanentCity { get; set; }

        [Range(0, 20,ErrorMessage="Experience in years can be between 0 to 20")]
        public Nullable<int> ExperienceYear { get; set; }
        [Range(0, 12, ErrorMessage="Experience in months can be between 0 to 12")]
        public Nullable<int> ExperienceMonth { get; set; }
        
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> JoiningDate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ResignationDate { get; set; }

        public byte[] Photo { get; set; }
        public string Status { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }

    // Enum to be used as Session String
    public enum SessionRole { Admin, Instructor, Student }

    public enum Designation : int
    {
        Instructor,
        JuniorIntructor,
        Manager,
        [Display(Name = "Chief Instructor (CI)")]
        ChiefInstructor,
        Clerk,
        NetworkEngineer,
        NetworkAdministrator,
        DatabaseAdministrator,
        LabSupervisor,
        EquipmentSpecialist
    }
}