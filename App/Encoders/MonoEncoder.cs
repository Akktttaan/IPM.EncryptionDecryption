namespace App.Encoders;

public class MonoEncoder : ICryptable
{
    private string RandomAlphabet;

    public MonoEncoder(string key)
    {
        RandomAlphabet = key;
    }

    public string Encrypt(string inputText)
    {
        string outputText = string.Empty;

        foreach (var ch in inputText)
        {
            var index = Constants.FullRussianAlphabet.IndexOf(ch);
            outputText += index < 0 ? ch : RandomAlphabet.ToCharArray()[index];
        }

        return outputText;
    }

    public string Decrypt(string inputText)
    {
        string outputText = string.Empty;

        foreach (var ch in inputText)
        {
            var index = RandomAlphabet.IndexOf(ch);
            outputText += index < 0 ? ch : Constants.FullRussianAlphabet.ToCharArray()[index];
        }

        return outputText;
    }
}