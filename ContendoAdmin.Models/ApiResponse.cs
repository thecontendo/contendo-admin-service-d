namespace ContendoAdmin.Models;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public ServerResponseMessage Message { get; set; }
        
    public Dictionary<string, object> Dictionaries { get; set; }

    public ApiResponse()
    {
        Message = new ServerResponseMessage();
    }

    public void AddSuccess(string text = "Success")
    {
        Success = true;
        Message.Code = 0;
        Message.Text = text;
    }

    public void AddError(Exception ex)
    {
        Success = false;
        Message.Code = -1;
        Message.Text = ex.Message;
    }
        
    public void AddError(string message, int code = -1)
    {
        Success = false;
        Message.Code = code;
        Message.Text = message == string.Empty ?  "Error" : message;
    }

    public void AddWarning(string message = "")
    {
        Message.Code = -2;
        Message.Text = message;
    }

    public void AddMessage(string message, int code = 0)
    {
        Message.Code = code;
        Message.Text = message;
    }

    public void AddNotFound(string message = "")
    {
        Message.Code = -4;
        Message.Text = message;
    }
}