using System.Runtime.Serialization;

namespace Hackathon.Request
{
    [DataContract]
    public class UserRequest
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
    public class SaveUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get;set; }
        public string mobileNumber { get; set; }

    }
}
