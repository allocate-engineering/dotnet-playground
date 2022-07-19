using System;

namespace UserApi.Web.Models
{
    public class UserDataModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}