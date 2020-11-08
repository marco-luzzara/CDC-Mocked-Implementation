using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDCImplementation.DataObjects
{
    public class DataObject
    {
        [Key]
        public int FirstId { get; set; }

        [Key]
        public int SecondId { get; set; }

        public string Value { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset LastChangeTime { get; set; }
    }
}
