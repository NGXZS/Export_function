namespace PWAY_ASPNetCore_WebAPI.Wrappers
{
    public class Respond<T> // creating data packet eg with success, error messages
    {
        public Respond() { }
        public Respond(T data) 
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
