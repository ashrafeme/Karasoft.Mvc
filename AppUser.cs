using System.Security.Claims;


namespace Karasoft.Mvc
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string UserDisplayName
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Gender
        {
            get
            {
                return this.FindFirst(ClaimTypes.Gender).Value;
            }
        }

        public string UserId
        {
            get
            {
                return this.FindFirst(ClaimTypes.Sid).Value;
            }
        }
    }
}