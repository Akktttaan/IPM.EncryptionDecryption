namespace App;

public class MonoEncoder : ICryptable
{
    const string ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private const string REVERSE_ALPHABET = "ЯЮЭЬЫЪЩШЧЦХФУТСРПОНМЛКЙИЗЖЁЕДГВБАяюэьыъщшчцхфутсрпонмлкйизжёедгвба";

    public string Encrypt(string inputText)
    {
        string outputText = string.Empty;

        foreach (var ch in inputText)
        {
            var index = ALPHABET.IndexOf(ch);
            outputText += index < 0 ? ch : REVERSE_ALPHABET.ToCharArray()[index];
        }

        return outputText;
    }

    public string Decrypt(string inputText)
    {
        string outputText = string.Empty;

        foreach (var ch in inputText)
        {
            var index = REVERSE_ALPHABET.IndexOf(ch);
            outputText += index < 0 ? ch : ALPHABET.ToCharArray()[index];
        }

        return outputText;
    }
}