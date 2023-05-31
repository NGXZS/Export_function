namespace PWAY_ASPNetCore_WebAPI.Wrappers
{
    public class Respond<T> // include more info in data packet
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
