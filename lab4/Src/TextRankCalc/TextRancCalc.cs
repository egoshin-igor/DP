namespace TextRankCalc
{
    public static class TextRancCalc
    {
        public static double Calculate(string text)
        {

            int consonantsQuantity = 0;
            int vowelsQuantity = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (isConsonant(ch))
                {
                    consonantsQuantity++;
                }
                else if (isVowel(ch))
                {
                    vowelsQuantity++;
                }
            }

            return consonantsQuantity != 0 ? (double) vowelsQuantity / consonantsQuantity : double.PositiveInfinity;
        }

        private static bool isVowel(char ch)
        {
            const string vowels = "aeiouyAEIOY";

            return vowels.IndexOf(ch) >= 0;
        }

        private static bool isConsonant(char ch)
        {
            const string consonants = "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";

            return consonants.IndexOf(ch) >= 0;
        }
    }
}