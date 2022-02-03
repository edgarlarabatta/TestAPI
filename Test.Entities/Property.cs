using System;

namespace Test.Entities
{
    public class Property
    {
        public int Property_Id { get; set; }
        public string Property_Title { get; set; }
        public string Property_Address { get; set; }
        public string Property_Description { get; set; }
        public DateTime Property_Updated_at { get; set; }
        public DateTime Property_Disabled_at { get; set; }
        public string Property_Status { get; set; }

    }
}
