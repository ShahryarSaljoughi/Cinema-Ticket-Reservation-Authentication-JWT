namespace BackgroundProcessing.Application;

public class RegisterationInput 
{
    public byte[] Document { get; set; } = [];
    public string Name { get; set; }
    public string Email { get; set; }
}
