using System;

namespace Eleven.Cloud.GCP.Dto
{
    public class SingerDto
    {
        public Guid SingerId { get; set; }
        public string Band { get; set; }
        public string Name { get; set; }
        public DateTimeOffset BirthDay { get; set; }
    }
}
