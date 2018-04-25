using System;
using System.Collections.Generic;

namespace ElConvertidor.Data.Entities
{
    public class Conversion
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int NumberOfPages { get; set; }


        public virtual ICollection<InputImage> InputImages { get; set; }
    }
}
