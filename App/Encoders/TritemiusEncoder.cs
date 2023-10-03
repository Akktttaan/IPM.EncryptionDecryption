using System.Text;

namespace App;

public class TritemiusEncoder : ICryptable
{
    private readonly string _key;

    public TritemiusEncoder(string key)
        => _key = string.IsNullOrEmpty(key) ? "ключ" : key;

    public string Encrypt(string text) => Encode(text, (index, keyIndex) =>
    ((index + keyIndex) + Constants.RussianAlphabetUpperCase.Length) % Constants.RussianAlphabetUpperCase.Length);

    public string Decrypt(string text) => Encode(text, (index, keyIndex) =>
    ((index - keyIndex) + Constants.RussianAlphabetUpperCase.Length) % Constants.RussianAlphabetUpperCase.Length);



    private string Encode(string text, Func<int, int, int> operation)
    {
        var sb = new StringBuilder();
        var fullAlphabet = Constants.RussianAlphabetUpperCase.ToLower();
        for (int i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            var index = fullAlphabet.IndexOf(ch);
            var kI = fullAlphabet.IndexOf(_key[i % _key.Length]);
            sb.Append(index < 0 ? ch : fullAlphabet[operation(index, kI)]);
        }

        return sb.ToString();
    }
}