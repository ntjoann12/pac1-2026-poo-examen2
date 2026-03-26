using Microsoft.AspNetCore.Identity;

namespace PersonsApp.Entities
{
    public class RolEntity : IdentityRole
    {
        public int MyProperty { get; set; }
    }
}