namespace Hackathon.Response
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public BaseResponse()
        {
            Status = false;
        }
    }
}
