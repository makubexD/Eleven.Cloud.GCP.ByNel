using System;
using System.ComponentModel.DataAnnotations;

namespace Eleven.Cloud.GCP.Entity
{
    public class Singer
    {
        [Key]
        public Guid SingerId { get; set; }
        public string Band { get; set; }
        public string Name { get; set; }
        public DateTimeOffset BirthDay { get; set; }
    }
}
