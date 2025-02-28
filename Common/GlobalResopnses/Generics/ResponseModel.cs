namespace Common.GlobalResopnses.Generics;

public class ResponseModel<T> : ResponseModel
{
    public T? Data { get; set; }

    public ResponseModel(List<string> message):base(message)
    {
        
    }

    public ResponseModel()
    {

    }
}