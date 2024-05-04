using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DMS_DUT.Models
{
    public class BaseModel
    {
        [Required]
        [Column(Order = 1)]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 100)]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Column(Order = 101)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Column(Order = 102)]
        [Display(Name = "Is Locked")]
        public bool? IsLocked { get; set; }

        [Column(Order = 103)]
        public int? CreatedBySystemUserId { get; set; }
        //[ForeignKey("CreatedBySystemUserId")]
        //[Display(Name = "Created By")]
        //[ScriptIgnore]
        //public SystemUser CreatedBySystemUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created On")]
        [Column(Order = 104)]
        public DateTime? CreatedDateTime { get; set; }

        [Column(Order = 105)]
        public int? ModifiedBySystemUserId { get; set; }
        //[ForeignKey("ModifiedBySystemUserId")]
        //[Display(Name = "Modified By")]
        //[ScriptIgnore]
        //[JsonIgnore]
        //public SystemUser ModifiedBySystemUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified On")]
        [Column(Order = 106)]
        public DateTime? ModifiedDateTime { get; set; }


    }
}