using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Entities
{
    public class Activity
    {
        public int Activity_Id { get; set; }

        [Required(ErrorMessage ="Property_Id is required")]
        public int Property_Id { get; set; }

        [Required(ErrorMessage = "Activity_Schedule is required")]
        public DateTime Activity_Schedule { get; set; }
        
        public string Activity_Title { get; set; }
        
        public DateTime Activity_Created_at { get; set; }        
        public DateTime Activity_Updated_at { get; set; }        
        public string Activity_Status { get; set; }

        //public  virtual Property Property { get; set; }

        public virtual string Property_Title { get; set; }
        public virtual string Property_Address { get; set; }

    }
}
