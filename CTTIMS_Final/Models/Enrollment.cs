//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBFirstSession.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enrollment
    {
        public Enrollment()
        {
            this.Assessments = new HashSet<Assessment>();
            this.Attendences = new HashSet<Attendence>();
        }
    
        public int ID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> CourseID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Status { get; set; }
        public Nullable<int> uID { get; set; }
    
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Attendence> Attendences { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual Course Course { get; set; }
        public virtual Section Section { get; set; }
        public virtual Student Student { get; set; }
        public virtual User User { get; set; }
    }
}