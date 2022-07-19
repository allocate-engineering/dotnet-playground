using System;

namespace UserApi.Web.Models
{
    public class UserRequestModel
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int Age { get; set; }
    }
}