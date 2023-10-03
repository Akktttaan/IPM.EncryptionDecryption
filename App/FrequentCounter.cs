namespace App;

public static class FrequentCounter
{
    public static Dictionary<char, double> countAppearencesOfLetter(string text)
    {
        var freqDict = new Dictionary<char, double>(Constants.RussianAlphabetLowerCase.Length);
        var lowerText = text.ToLower();

        foreach (var ch in Constants.RussianAlphabetLowerCase)
        {
            freqDict[ch] = 0;
        }

        foreach (var letter in lowerText)
        {
            if (Constants.RussianAlphabetLowerCase.Contains(letter.ToString()))
            {
                var counter = lowerText.Count(ch => ch == letter);
                freqDict[letter] = Math.Round(((double)counter / lowerText.Length) * 100, 2);
            }
        }

        return freqDict;
    }
}