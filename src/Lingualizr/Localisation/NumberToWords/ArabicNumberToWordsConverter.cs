﻿namespace Lingualizr.Localisation.NumberToWords;

internal class ArabicNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _groups = { "مئة", "ألف", "مليون", "مليار", "تريليون", "كوادريليون", "كوينتليون", "سكستيليون" };
    private static readonly string[] _appendedGroups = { string.Empty, "ألفاً", "مليوناً", "ملياراً", "تريليوناً", "كوادريليوناً", "كوينتليوناً", "سكستيليوناً" };
    private static readonly string[] _pluralGroups = { string.Empty, "آلاف", "ملايين", "مليارات", "تريليونات", "كوادريليونات", "كوينتليونات", "سكستيليونات" };

    private static readonly string[] _onesGroup =
    {
        string.Empty,
        "واحد",
        "اثنان",
        "ثلاثة",
        "أربعة",
        "خمسة",
        "ستة",
        "سبعة",
        "ثمانية",
        "تسعة",
        "عشرة",
        "أحد عشر",
        "اثنا عشر",
        "ثلاثة عشر",
        "أربعة عشر",
        "خمسة عشر",
        "ستة عشر",
        "سبعة عشر",
        "ثمانية عشر",
        "تسعة عشر",
    };

    private static readonly string[] _tensGroup = { string.Empty, "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
    private static readonly string[] _hundredsGroup = { string.Empty, "مئة", "مئتان", "ثلاث مئة", "أربع مئة", "خمس مئة", "ست مئة", "سبع مئة", "ثمان مئة", "تسع مئة" };
    private static readonly string[] _appendedTwos = { "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونلن" };
    private static readonly string[] _twos = { "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونان" };

    private static readonly string[] _feminineOnesGroup =
    {
        string.Empty,
        "واحدة",
        "اثنتان",
        "ثلاث",
        "أربع",
        "خمس",
        "ست",
        "سبع",
        "ثمان",
        "تسع",
        "عشر",
        "إحدى عشرة",
        "اثنتا عشرة",
        "ثلاث عشرة",
        "أربع عشرة",
        "خمس عشرة",
        "ست عشرة",
        "سبع عشرة",
        "ثمان عشرة",
        "تسع عشرة",
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return "صفر";
        }

        if (number < 0)
        {
            return string.Format("ناقص {0}", Convert(-number, gender));
        }

        string result = string.Empty;
        int groupLevel = 0;

        while (number >= 1)
        {
            long groupNumber = number % 1000;
            number /= 1000;

            long tens = groupNumber % 100;
            long hundreds = groupNumber / 100;
            string process = string.Empty;

            if (hundreds > 0)
            {
                if (tens == 0 && hundreds == 2)
                {
                    process = _appendedTwos[0];
                }
                else
                {
                    process = _hundredsGroup[hundreds];
                }
            }

            if (tens > 0)
            {
                if (tens < 20)
                {
                    if (tens == 2 && hundreds == 0 && groupLevel > 0)
                    {
                        if (number == 2000 || number == 2000000 || number == 2000000000)
                        {
                            process = _appendedTwos[groupLevel];
                        }
                        else
                        {
                            process = _twos[groupLevel];
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(process))
                        {
                            process += " و ";
                        }

                        if (tens == 1 && groupLevel > 0 && hundreds == 0)
                        {
                            process += " ";
                        }
                        else
                        {
                            process += gender == GrammaticalGender.Feminine && groupLevel == 0 ? _feminineOnesGroup[tens] : _onesGroup[tens];
                        }
                    }
                }
                else
                {
                    long ones = tens % 10;
                    tens = tens / 10;

                    if (ones > 0)
                    {
                        if (!string.IsNullOrEmpty(process))
                        {
                            process += " و ";
                        }

                        process += gender == GrammaticalGender.Feminine ? _feminineOnesGroup[ones] : _onesGroup[ones];
                    }

                    if (!string.IsNullOrEmpty(process))
                    {
                        process += " و ";
                    }

                    process += _tensGroup[tens];
                }
            }

            if (!string.IsNullOrEmpty(process))
            {
                if (groupLevel > 0)
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = string.Format("{0} {1}", "و", result);
                    }

                    if (groupNumber != 2)
                    {
                        if (groupNumber % 100 != 1)
                        {
                            if (groupNumber >= 3 && groupNumber <= 10)
                            {
                                result = string.Format("{0} {1}", _pluralGroups[groupLevel], result);
                            }
                            else
                            {
                                result = string.Format("{0} {1}", !string.IsNullOrEmpty(result) ? _appendedGroups[groupLevel] : _groups[groupLevel], result);
                            }
                        }
                        else
                        {
                            result = string.Format("{0} {1}", _groups[groupLevel], result);
                        }
                    }
                }

                result = string.Format("{0} {1}", process, result);
            }

            groupLevel++;
        }

        return result.Trim();
    }

    private static readonly Dictionary<string, string> _ordinalExceptions =
        new()
        {
            { "واحد", "الحادي" },
            { "أحد", "الحادي" },
            { "اثنان", "الثاني" },
            { "اثنا", "الثاني" },
            { "ثلاثة", "الثالث" },
            { "أربعة", "الرابع" },
            { "خمسة", "الخامس" },
            { "ستة", "السادس" },
            { "سبعة", "السابع" },
            { "ثمانية", "الثامن" },
            { "تسعة", "التاسع" },
            { "عشرة", "العاشر" },
        };

    private static readonly Dictionary<string, string> _feminineOrdinalExceptions =
        new()
        {
            { "واحدة", "الحادية" },
            { "إحدى", "الحادية" },
            { "اثنتان", "الثانية" },
            { "اثنتا", "الثانية" },
            { "ثلاث", "الثالثة" },
            { "أربع", "الرابعة" },
            { "خمس", "الخامسة" },
            { "ست", "السادسة" },
            { "سبع", "السابعة" },
            { "ثمان", "الثامنة" },
            { "تسع", "التاسعة" },
            { "عشر", "العاشرة" },
        };

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        if (number == 0)
        {
            return "الصفر";
        }

        int beforeOneHundredNumber = number % 100;
        int overTensPart = number / 100 * 100;
        string beforeOneHundredWord = string.Empty;
        string overTensWord = string.Empty;

        if (beforeOneHundredNumber > 0)
        {
            beforeOneHundredWord = Convert(beforeOneHundredNumber, gender);
            beforeOneHundredWord = ParseNumber(beforeOneHundredWord, beforeOneHundredNumber, gender);
        }

        if (overTensPart > 0)
        {
            overTensWord = Convert(overTensPart);
            overTensWord = ParseNumber(overTensWord, overTensPart, gender);
        }

        string word = beforeOneHundredWord + (overTensPart > 0 ? (string.IsNullOrWhiteSpace(beforeOneHundredWord) ? string.Empty : " بعد ") + overTensWord : string.Empty);
        return word.Trim();
    }

    private static string ParseNumber(string word, int number, GrammaticalGender gender)
    {
        if (number == 1)
        {
            return gender == GrammaticalGender.Feminine ? "الأولى" : "الأول";
        }

        if (number <= 10)
        {
            Dictionary<string, string> ordinals = gender == GrammaticalGender.Feminine ? _feminineOrdinalExceptions : _ordinalExceptions;
            foreach (KeyValuePair<string, string> kv in ordinals.Where(kv => word.EndsWith(kv.Key)))
            {
#pragma warning disable S1751
                return word.Substring(0, word.Length - kv.Key.Length) + kv.Value;
#pragma warning restore S1751
            }
        }
        else if (number < 100)
        {
            string[] parts = word.Split(' ');
            string[] newParts = new string[parts.Length];
            int count = 0;

            foreach (string? part in parts)
            {
                string newPart = part;
                string oldPart = part;

                Dictionary<string, string> ordinals = gender == GrammaticalGender.Feminine ? _feminineOrdinalExceptions : _ordinalExceptions;
                foreach (KeyValuePair<string, string> kv in ordinals.Where(kv => oldPart.EndsWith(kv.Key)))
                {
                    // replace word with exception
                    newPart = oldPart.Substring(0, oldPart.Length - kv.Key.Length) + kv.Value;
                }

                if (number > 19 && newPart == oldPart && oldPart.Length > 1)
                {
                    newPart = "ال" + oldPart;
                }

                newParts[count++] = newPart;
            }

            word = string.Join(" ", newParts);
        }
        else
        {
            word = "ال" + word;
        }

        return word;
    }
}
