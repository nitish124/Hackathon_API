using Hackathon.Model;

namespace Hackathon.Response
{
    public class UserResponse : BaseResponse
    {
        public Users User { get; set; }
 
    }
    public class UsersResponse : BaseResponse
    {
        public List<Users> User { get; set; }

    }
}
