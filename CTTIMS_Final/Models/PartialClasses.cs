using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CTTIMS_Final.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User 
    {
        [Display(Name="New Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("RePassword2")]
        public string RePassword { get; set; }

        [Display(Name = "Re-Enter New Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("RePassword")]
        public string RePassword2 { get; set; }
    }

    [MetadataType(typeof(BatchMetadata))]
    public partial class Batch 
    {
        
    }

    [MetadataType(typeof(InstructorMetadata))]
    public partial class Instructor
    {

    }
}