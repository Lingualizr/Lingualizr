using System.Diagnostics.CodeAnalysis;

namespace Lingualizr.Localisation.NumberToWords;

internal class TamilNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "சுழியம்",
        "ஒன்று",
        "இரண்டு",
        "மூன்று",
        "நான்கு",
        "ஐந்து",
        "ஆறு",
        "ஏழு",
        "எட்டு",
        "ஒன்பது",
        "பத்து",
        "பதினொன்று",
        "பனிரெண்டு",
        "பதிமூன்று",
        "பதினான்கு",
        "பதினைந்து",
        "பதினாறு",
        "பதினேழு",
        "பதினெட்டு",
        "பத்தொன்பது",
    };

    private static readonly string[] _tensMap = { "சுழியம்", "பத்து", "இருப", "முப்ப", "நாற்ப", "ஐம்ப", "அறுப", "எழுப", "எண்ப", "தொண்ணூ" };
    private static readonly string[] _hundredsMap = { "நூ", "இருநூ", "முன்னூ", "நானூ", "ஐந்நூ", "அறுநூ", "எழுநூ", "எண்ணூ", "தொள்ளாயிர", };

    private static readonly string[] _thousandsMap =
    {
        "ஆ",
        "இரண்டா",
        "மூன்றா",
        "நான்கா",
        "ஐந்தா",
        "ஆறா",
        "ஏழா",
        "எட்டா",
        "ஒன்பதா",
        "பத்தா",
        "பதினொன்றா",
        "பனிரெண்டா",
        "பதிமூன்றா",
        "பதினான்கா",
        "பதினைந்தா",
        "பதினாறா",
        "பதினேழா",
        "பதினெட்டா",
        "பத்தொன்பதா",
    };

    private static readonly string[] _lakhsMap = { "இலட்ச" };

    private static readonly Dictionary<long, string> _ordinalExceptions =
        new()
        {
            { 1, "முதலாவது" },
            { 2, "இரண்டாவது" },
            { 3, "மூன்றாவது" },
            { 4, "நான்காவது" },
            { 5, "ஐந்தாவது" },
            { 8, "எட்டாவது" },
            { 9, "ஒன்பதாவது" },
            { 12, "பனிரெண்டாவது" },
        };

    public override string Convert(long number)
    {
        return ConvertImpl(number, false);
    }

    public override string ConvertToOrdinal(int number)
    {
        return ConvertImpl(number, true);
    }

    private string ConvertImpl(long number, bool isOrdinal)
    {
        if (number == 0)
        {
            return GetUnitValue(0, isOrdinal);
        }

        if (number < 0)
        {
            return string.Format("கழித்தல் {0}", Convert(-number));
        }

        List<string> parts = new();

        if (number / 1000000000000000000 > 0)
        {
            parts.Add(string.Format("{0} quintillion", Convert(number / 1000000000000000000)));
            number %= 1000000000000000000;
        }

        if (number / 1000000000000000 > 0)
        {
            parts.Add(string.Format("{0} quadrillion", Convert(number / 1000000000000000)));
            number %= 1000000000000000;
        }

        if (number / 10000000 > 0)
        {
            parts.Add(GetCroresValue(ref number));
        }

        if (number / 100000 > 0)
        {
            parts.Add(GetLakhsValue(ref number, isOrdinal));
        }

        if (number / 1000 > 0)
        {
            parts.Add(GetThousandsValue(ref number));
        }

        if (number / 100 > 0)
        {
            parts.Add(GetHundredsValue(ref number));
        }

        if (number > 0)
        {
            parts.Add(GetTensValue(number, isOrdinal));
        }
        else if (isOrdinal)
        {
            parts[parts.Count - 1] += "வது";
        }

        string toWords = string.Join(" ", parts.ToArray());

        if (isOrdinal)
        {
            toWords = RemoveOnePrefix(toWords);
        }

        return toWords;
    }

    private static string GetUnitValue(long number, bool isOrdinal)
    {
        if (isOrdinal)
        {
            if (ExceptionNumbersToWords(number, out string? exceptionString))
            {
                return exceptionString;
            }
            else
            {
                return _unitsMap[number] + "வது";
            }
        }
        else
        {
            return _unitsMap[number];
        }
    }

    private static string GetTensValue(long number, bool isOrdinal, bool isThousand = false)
    {
        string localWord = string.Empty;
        if (number < 20)
        {
            localWord = GetUnitValue(number, isOrdinal);
        }
        else if (number <= 99)
        {
            string lastPart = _tensMap[number / 10];
            long quot = number / 10;
            if (number % 10 > 0)
            {
                if (quot == 9)
                {
                    lastPart += "ற்றி ";
                }
                else if (quot == 7 || quot == 8 || quot == 4)
                {
                    lastPart += "த்தி ";
                }
                else
                {
                    lastPart += "த்து ";
                }

                if (!isThousand)
                {
                    lastPart += string.Format("{0}", GetUnitValue(number % 10, isOrdinal));
                }
            }
            else if (number % 10 == 0)
            {
                if (isThousand)
                {
                    if (quot == 9)
                    {
                        lastPart += "றா";
                    }
                    else
                    {
                        lastPart += "தா";
                    }
                }
                else
                {
                    if (quot == 9)
                    {
                        lastPart += "று";
                    }
                    else
                    {
                        lastPart += "து";
                    }
                }
            }
            else if (isOrdinal)
            {
                lastPart = lastPart.TrimEnd('y') + "ieth";
            }

            localWord = lastPart;
        }

        return localWord;
    }

    private static string GetLakhsValue(ref long number, bool isOrdinal)
    {
        long numAbove10 = number / 100000;
        string localWord = string.Empty;
        if (numAbove10 >= 20)
        {
            localWord = GetTensValue(numAbove10, false);
            localWord += " " + _lakhsMap[0];
        }
        else if (numAbove10 == 1)
        {
            localWord = "ஒரு " + _lakhsMap[0];
        }
        else
        {
            localWord += GetTensValue(number / 100000, isOrdinal) + " " + _lakhsMap[0];
        }

        if (number % 1000000 == 0 || number % 100000 == 0)
        {
            localWord += "ம்";
        }
        else
        {
            localWord += "த்து";
        }

        number %= 100000;
        return localWord;
    }

    private static string GetCroresValue(ref long number)
    {
        string localWord = string.Empty;
        long numAbove10 = number / 10000000;
        string strCrore = "கோடி";

        if (numAbove10 > 99999 && numAbove10 <= 9999999)
        {
            localWord = GetLakhsValue(ref numAbove10, false);
            localWord += " ";
        }

        if (numAbove10 > 999 && numAbove10 <= 99999)
        {
            localWord += GetThousandsValue(ref numAbove10);
            localWord += " ";
        }

        if (numAbove10 > 99 && numAbove10 <= 999)
        {
            localWord += GetHundredsValue(ref numAbove10);
            localWord += " ";
        }

        if (numAbove10 >= 20)
        {
            localWord += GetTensValue(numAbove10, false);
            localWord += " ";
        }
        else if (numAbove10 == 1)
        {
            localWord = "ஒரு ";
        }
        else if (numAbove10 > 0)
        {
            localWord += GetTensValue(numAbove10, false) + " ";
        }

        localWord = localWord.TrimEnd() + " " + strCrore;
        if (number % 10000000 == 0 || number % 100000000 == 0)
        {
            localWord += string.Empty;
        }
        else
        {
            localWord += "யே";
        }

        number %= 10000000;
        return localWord;
    }

    private static string GetThousandsValue(ref long number)
    {
        long numAbove10 = number / 1000;
        string localWord = string.Empty;
        if (numAbove10 >= 20)
        {
            localWord = GetTensValue(numAbove10, false, true);

            if (numAbove10 % 10 == 1)
            {
                localWord += "ஓரா";
            }
            else if (numAbove10 % 10 > 1)
            {
                localWord += _thousandsMap[numAbove10 % 10 - 1];
            }
        }
        else
        {
            localWord += _thousandsMap[number / 1000 - 1];
        }

        number %= 1000;

        if (number > 0)
        {
            localWord = localWord + "யிரத்து";
        }
        else
        {
            localWord = localWord + "யிரம்";
        }

        return localWord;
    }

    private static string GetHundredsValue(ref long number)
    {
        string localWord = _hundredsMap[number / 100 - 1];
        if (number / 100 == 9)
        {
            if (number % 100 == 0)
            {
                localWord += "ம்";
            }
            else
            {
                localWord += "த்து";
            }
        }
        else if (number % 100 >= 1)
        {
            localWord += "ற்று";
        }
        else
        {
            localWord += "று";
        }

        number %= 100;

        return localWord;
    }

    private static string RemoveOnePrefix(string toWords)
    {
        // one hundred => hundredth
        if (toWords.StartsWith("one", StringComparison.Ordinal))
        {
            toWords = toWords.Remove(0, 4);
        }

        return toWords;
    }

    private static bool ExceptionNumbersToWords(long number, [MaybeNullWhen(false)] out string words)
    {
        return _ordinalExceptions.TryGetValue(number, out words);
    }
}
