namespace App;

public static class FrequentCounter
{
    const string ALPHABET = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

    public static Dictionary<char, double> countAppearencesOfLetter(string text)
    {
        var freqDict = new Dictionary<char, double>(ALPHABET.Length);
        var lowerText = text.ToLower();

        foreach (var ch in ALPHABET)
        {
            freqDict[ch] = 0;
        }

        foreach (var letter in lowerText)
        {
            if (ALPHABET.Contains(letter.ToString()))
            {
                var counter = lowerText.Count(ch => ch == letter);
                freqDict[letter] = Math.Round(((double)counter / lowerText.Length) * 100, 2);
            }
        }

        return freqDict;
    }
}