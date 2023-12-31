﻿using System.ComponentModel;
using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class HebrewNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _unitsFeminine = { "אפס", "אחת", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה", "תשע", "עשר" };
    private static readonly string[] _unitsMasculine = { "אפס", "אחד", "שניים", "שלושה", "ארבעה", "חמישה", "שישה", "שבעה", "שמונה", "תשעה", "עשרה" };
    private static readonly string[] _tensUnit = { "עשר", "עשרים", "שלושים", "ארבעים", "חמישים", "שישים", "שבעים", "שמונים", "תשעים" };

    private readonly CultureInfo _culture;

    private enum Group
    {
        Hundreds = 100,
        Thousands = 1000,

        [Description("מיליון")]
        Millions = 1000000,

        [Description("מיליארד")]
        Billions = 1000000000,
    }

    public HebrewNumberToWordsConverter(CultureInfo culture)
        : base(GrammaticalGender.Feminine)
    {
        _culture = culture;
    }

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        int numberInt = (int)number;

        if (numberInt < 0)
        {
            return string.Format("מינוס {0}", Convert(-numberInt, gender));
        }

        if (numberInt == 0)
        {
            return _unitsFeminine[0];
        }

        List<string> parts = new();
        if (numberInt >= (int)Group.Billions)
        {
            ToBigNumber(numberInt, Group.Billions, parts);
            numberInt %= (int)Group.Billions;
        }

        if (numberInt >= (int)Group.Millions)
        {
            ToBigNumber(numberInt, Group.Millions, parts);
            numberInt %= (int)Group.Millions;
        }

        if (numberInt >= (int)Group.Thousands)
        {
            ToThousands(numberInt, parts);
            numberInt %= (int)Group.Thousands;
        }

        if (numberInt >= (int)Group.Hundreds)
        {
            ToHundreds(numberInt, parts);
            numberInt %= (int)Group.Hundreds;
        }

        if (numberInt > 0)
        {
            bool appendAnd = parts.Count != 0;

            if (numberInt <= 10)
            {
                string unit = gender == GrammaticalGender.Masculine ? _unitsMasculine[numberInt] : _unitsFeminine[numberInt];
                if (appendAnd)
                {
                    unit = "ו" + unit;
                }

                parts.Add(unit);
            }
            else if (numberInt < 20)
            {
                string unit = Convert(numberInt % 10, gender);
                unit = unit.Replace("יי", "י");
                unit = string.Format("{0} {1}", unit, gender == GrammaticalGender.Masculine ? "עשר" : "עשרה");
                if (appendAnd)
                {
                    unit = "ו" + unit;
                }

                parts.Add(unit);
            }
            else
            {
                string tenUnit = _tensUnit[numberInt / 10 - 1];
                if (numberInt % 10 == 0)
                {
                    parts.Add(tenUnit);
                }
                else
                {
                    string unit = Convert(numberInt % 10, gender);
                    parts.Add(string.Format("{0} ו{1}", tenUnit, unit));
                }
            }
        }

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return number.ToString(_culture);
    }

    private void ToBigNumber(int number, Group group, List<string> parts)
    {
        // Big numbers (million and above) always use the masculine form
        // See https://www.safa-ivrit.org/dikduk/numbers.php

        int digits = number / (int)@group;
        if (digits == 2)
        {
            parts.Add("שני");
        }
        else if (digits > 2)
        {
            parts.Add(Convert(digits, GrammaticalGender.Masculine));
        }

        parts.Add(@group.Humanize());
    }

    private void ToThousands(int number, List<string> parts)
    {
        int thousands = number / (int)Group.Thousands;

        if (thousands == 1)
        {
            parts.Add("אלף");
        }
        else if (thousands == 2)
        {
            parts.Add("אלפיים");
        }
        else if (thousands == 8)
        {
            parts.Add("שמונת אלפים");
        }
        else if (thousands <= 10)
        {
            parts.Add(_unitsFeminine[thousands] + "ת" + " אלפים");
        }
        else
        {
            parts.Add(Convert(thousands) + " אלף");
        }
    }

    private static void ToHundreds(int number, List<string> parts)
    {
        // For hundreds, Hebrew is using the feminine form
        // See https://www.safa-ivrit.org/dikduk/numbers.php

        int hundreds = number / (int)Group.Hundreds;

        if (hundreds == 1)
        {
            parts.Add("מאה");
        }
        else if (hundreds == 2)
        {
            parts.Add("מאתיים");
        }
        else
        {
            parts.Add(_unitsFeminine[hundreds] + " מאות");
        }
    }
}
