namespace App;

public class CaesarEncoder : ICryptable
{
    private int cryptKey;
    public CaesarEncoder(string cryptKey = "1")
    {
        this.cryptKey = int.Parse(cryptKey);
    }

    public string Encrypt(string inputText)
    {
        var outputText = string.Empty;
        var fullAlphabet = Constants.RussianAlphabetUpperCase + Constants.RussianAlphabetUpperCase.ToLower();
        var fullAlphabetLength = fullAlphabet.Length;

        foreach (var ch in inputText)
        {
            var index = fullAlphabet.IndexOf(ch);
            if (index < 0)
            {
                outputText += ch;
            }
            else
            {
                outputText += fullAlphabet[(fullAlphabetLength + index + cryptKey) % fullAlphabetLength];
            }
        }

        return outputText;
    }

    public string Decrypt(string inputText)
    {
        var outputText = string.Empty;
        var fullAlphabet = Constants.RussianAlphabetUpperCase + Constants.RussianAlphabetUpperCase.ToLower();
        var fullAlphabetLength = fullAlphabet.Length;

        foreach (var ch in inputText)
        {
            var index = fullAlphabet.IndexOf(ch);
            if (index < 0)
            {
                outputText += ch;
            }
            else
            {
                outputText += fullAlphabet[(fullAlphabetLength + index - cryptKey) % fullAlphabetLength];
            }
        }

        return outputText;
    }
}