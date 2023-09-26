namespace App;

public class TritemiusEncoder : ICryptable
{
    private string _cryptWord;
    const string ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    public TritemiusEncoder(string key)
    {
        if (key == string.Empty) _cryptWord = "тритемиус";
        else _cryptWord = key;
    }

    public string Encrypt(string inputText)
    {
        var outputText = string.Empty;
        var fullAlphabet = ALPHABET + ALPHABET.ToLower();
        var fullAlphabetLength = fullAlphabet.Length;

        for (int i = 0; i < inputText.Length; i++)
        {
            var ch = inputText[i];
            var index = fullAlphabet.IndexOf(ch);
            if (index < 0)
            {
                outputText += ch;
            }
            else
            {
                var charInKey = _cryptWord[i % _cryptWord.Length];
                outputText += fullAlphabet[(index + fullAlphabet.IndexOf(charInKey)) % fullAlphabetLength];
            }
        }

        return outputText;
    }

    public string Decrypt(string inputText)
    {
        var outputText = string.Empty;
        var fullAlphabet = ALPHABET + ALPHABET.ToLower();
        var fullAlphabetLength = fullAlphabet.Length;

        for (int i = 0; i < inputText.Length; i++)
        {
            var ch = inputText[i];
            var index = fullAlphabet.IndexOf(ch);
            if (index < 0)
            {
                outputText += ch;
            }
            else
            {
                var charInKey = _cryptWord[i % _cryptWord.Length];
                outputText += fullAlphabet[(index + fullAlphabetLength) - fullAlphabet.IndexOf(charInKey)];
            }
        }

        return outputText;
    }
}