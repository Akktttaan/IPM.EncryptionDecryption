namespace App.Encoders;

public interface ICryptable
{
    string Encrypt(string inputText);
    string Decrypt(string inputText);
}