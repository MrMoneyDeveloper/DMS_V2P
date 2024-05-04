using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DMS_DUT.Models
{
    public class Document:BaseModel
    {

        [Column(Order = 2)]
        [Display(Name = "StringPath")]
        [MaxLength(100)]
        public string Path { get; set; }

        // Foreign Key property
        public int FormId { get; set; }
        // Navigation property
     


    }
}