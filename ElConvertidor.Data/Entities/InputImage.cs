using ElConvertidor.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ElConvertidor.Data.Entities
{
    public class InputImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int ConversionId { get; set; }


        public virtual Conversion Conversion { get; set; }
    }
}
