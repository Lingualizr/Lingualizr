﻿// The MIT License (MIT)

// Copyright (c) 2013-2014 Omar Khudeira (http://omar.io)

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Globalization;
using Lingualizr.Configuration;
using Lingualizr.Localisation;
using Lingualizr.Localisation.Formatters;
using static System.Globalization.NumberStyles;

namespace Lingualizr.Bytes;

/// <summary>
/// Represents a byte size value.
/// </summary>
#pragma warning disable 1591
public struct ByteSize : IComparable<ByteSize>, IEquatable<ByteSize>, IComparable, IFormattable
{
    public static readonly ByteSize MinValue = FromBits(long.MinValue);
    public static readonly ByteSize MaxValue = FromBits(long.MaxValue);

    public const long BitsInByte = 8;
    public const long BytesInKilobyte = 1024;
    public const long BytesInMegabyte = 1048576;
    public const long BytesInGigabyte = 1073741824;
    public const long BytesInTerabyte = 1099511627776;

    public const string BitSymbol = "b";
    public const string Bit = "bit";
    public const string ByteSymbol = "B";
    public const string Byte = "byte";
    public const string KilobyteSymbol = "KB";
    public const string Kilobyte = "kilobyte";
    public const string MegabyteSymbol = "MB";
    public const string Megabyte = "megabyte";
    public const string GigabyteSymbol = "GB";
    public const string Gigabyte = "gigabyte";
    public const string TerabyteSymbol = "TB";
    public const string Terabyte = "terabyte";

    public long Bits { get; private set; }

    public double Bytes { get; private set; }

    public double Kilobytes { get; private set; }

    public double Megabytes { get; private set; }

    public double Gigabytes { get; private set; }

    public double Terabytes { get; private set; }

    public string LargestWholeNumberSymbol => GetLargestWholeNumberSymbol();

    public string GetLargestWholeNumberSymbol(IFormatProvider? provider = null)
    {
        IFormatter cultureFormatter = Configurator.GetFormatter(provider as CultureInfo);

        // Absolute value is used to deal with negative values
        if (Math.Abs(Terabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Terabyte, Terabytes, toSymbol: true);
        }

        if (Math.Abs(Gigabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Gigabyte, Gigabytes, toSymbol: true);
        }

        if (Math.Abs(Megabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Megabyte, Megabytes, toSymbol: true);
        }

        if (Math.Abs(Kilobytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Kilobyte, Kilobytes, toSymbol: true);
        }

        if (Math.Abs(Bytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Byte, Bytes, toSymbol: true);
        }

        return cultureFormatter.DataUnitHumanize(DataUnit.Bit, Bits, toSymbol: true);
    }

    public string LargestWholeNumberFullWord => GetLargestWholeNumberFullWord();

    public string GetLargestWholeNumberFullWord(IFormatProvider? provider = null)
    {
        IFormatter cultureFormatter = Configurator.GetFormatter(provider as CultureInfo);

        // Absolute value is used to deal with negative values
        if (Math.Abs(Terabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Terabyte, Terabytes, toSymbol: false);
        }

        if (Math.Abs(Gigabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Gigabyte, Gigabytes, toSymbol: false);
        }

        if (Math.Abs(Megabytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Megabyte, Megabytes, toSymbol: false);
        }

        if (Math.Abs(Kilobytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Kilobyte, Kilobytes, toSymbol: false);
        }

        if (Math.Abs(Bytes) >= 1)
        {
            return cultureFormatter.DataUnitHumanize(DataUnit.Byte, Bytes, toSymbol: false);
        }

        return cultureFormatter.DataUnitHumanize(DataUnit.Bit, Bits, toSymbol: false);
    }

    public double LargestWholeNumberValue
    {
        get
        {
            // Absolute value is used to deal with negative values
            if (Math.Abs(Terabytes) >= 1)
            {
                return Terabytes;
            }

            if (Math.Abs(Gigabytes) >= 1)
            {
                return Gigabytes;
            }

            if (Math.Abs(Megabytes) >= 1)
            {
                return Megabytes;
            }

            if (Math.Abs(Kilobytes) >= 1)
            {
                return Kilobytes;
            }

            if (Math.Abs(Bytes) >= 1)
            {
                return Bytes;
            }

            return Bits;
        }
    }

    public ByteSize(double byteSize)
        : this()
    {
        // Get ceiling because bis are whole units
        Bits = (long)Math.Ceiling(byteSize * BitsInByte);

        Bytes = byteSize;
        Kilobytes = byteSize / BytesInKilobyte;
        Megabytes = byteSize / BytesInMegabyte;
        Gigabytes = byteSize / BytesInGigabyte;
        Terabytes = byteSize / BytesInTerabyte;
    }

    public static ByteSize FromBits(long value)
    {
        return new ByteSize(value / (double)BitsInByte);
    }

    public static ByteSize FromBytes(double value)
    {
        return new ByteSize(value);
    }

    public static ByteSize FromKilobytes(double value)
    {
        return new ByteSize(value * BytesInKilobyte);
    }

    public static ByteSize FromMegabytes(double value)
    {
        return new ByteSize(value * BytesInMegabyte);
    }

    public static ByteSize FromGigabytes(double value)
    {
        return new ByteSize(value * BytesInGigabyte);
    }

    public static ByteSize FromTerabytes(double value)
    {
        return new ByteSize(value * BytesInTerabyte);
    }

    /// <summary>
    /// Converts the value of the current ByteSize object to a string.
    /// The metric prefix symbol (bit, byte, kilo, mega, giga, tera) used is
    /// the largest metric prefix such that the corresponding value is greater
    ///  than or equal to one.
    /// </summary>
    public override string ToString()
    {
        return ToString(NumberFormatInfo.CurrentInfo);
    }

    public string ToString(IFormatProvider? provider)
    {
        provider ??= CultureInfo.CurrentCulture;

        return string.Format(provider, "{0:0.##} {1}", LargestWholeNumberValue, GetLargestWholeNumberSymbol(provider));
    }

    public string ToString(string format)
    {
        return ToString(format, NumberFormatInfo.CurrentInfo);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString(format, formatProvider, toSymbol: true);
    }

    private string ToString(string? format, IFormatProvider? provider, bool toSymbol)
    {
        if (format == null)
        {
            format = "G";
        }

        provider ??= CultureInfo.CurrentCulture;

        if (format == "G")
        {
            format = "0.##";
        }

        if (!format.Contains('#') && !format.Contains('0'))
        {
            format = "0.## " + format;
        }

        format = format.Replace("#.##", "0.##");

        CultureInfo? culture = provider as CultureInfo ?? CultureInfo.CurrentCulture;

        bool has(string s) => culture.CompareInfo.IndexOf(format, s, CompareOptions.IgnoreCase) != -1;
        string output(double n) => n.ToString(format, provider);

        IFormatter cultureFormatter = Configurator.GetFormatter(provider as CultureInfo);

        if (has(TerabyteSymbol))
        {
            format = format.Replace(TerabyteSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Terabyte, Terabytes, toSymbol));
            return output(Terabytes);
        }

        if (has(GigabyteSymbol))
        {
            format = format.Replace(GigabyteSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Gigabyte, Gigabytes, toSymbol));
            return output(Gigabytes);
        }

        if (has(MegabyteSymbol))
        {
            format = format.Replace(MegabyteSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Megabyte, Megabytes, toSymbol));
            return output(Megabytes);
        }

        if (has(KilobyteSymbol))
        {
            format = format.Replace(KilobyteSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Kilobyte, Kilobytes, toSymbol));
            return output(Kilobytes);
        }

        // Byte and Bit symbol look must be case-sensitive
        if (format.Contains(ByteSymbol))
        {
            format = format.Replace(ByteSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Byte, Bytes, toSymbol));
            return output(Bytes);
        }

        if (format.Contains(BitSymbol))
        {
            format = format.Replace(BitSymbol, cultureFormatter.DataUnitHumanize(DataUnit.Bit, Bits, toSymbol));
            return output(Bits);
        }

        string formattedLargeWholeNumberValue = LargestWholeNumberValue.ToString(format, provider);

        formattedLargeWholeNumberValue = string.IsNullOrEmpty(formattedLargeWholeNumberValue) ? "0" : formattedLargeWholeNumberValue;

        return string.Format("{0} {1}", formattedLargeWholeNumberValue, toSymbol ? GetLargestWholeNumberSymbol(provider) : GetLargestWholeNumberFullWord(provider));
    }

    /// <summary>
    /// Converts the value of the current ByteSize object to a string with
    /// full words. The metric prefix symbol (bit, byte, kilo, mega, giga,
    /// tera) used is the largest metric prefix such that the corresponding
    /// value is greater than or equal to one.
    /// </summary>
    public string ToFullWords(string? format = null, IFormatProvider? provider = null)
    {
        return ToString(format, provider, toSymbol: false);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        ByteSize other;
        if (obj is ByteSize)
        {
            other = (ByteSize)obj;
        }
        else
        {
            return false;
        }

        return Equals(other);
    }

    public bool Equals(ByteSize other)
    {
        return Bits == other.Bits;
    }

    public override int GetHashCode()
    {
        return Bits.GetHashCode();
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (!(obj is ByteSize))
        {
            throw new ArgumentException("Object is not a ByteSize");
        }

        return CompareTo((ByteSize)obj);
    }

    public int CompareTo(ByteSize other)
    {
        return Bits.CompareTo(other.Bits);
    }

    public ByteSize Add(ByteSize bs)
    {
        return new ByteSize(Bytes + bs.Bytes);
    }

    public ByteSize AddBits(long value)
    {
        return this + FromBits(value);
    }

    public ByteSize AddBytes(double value)
    {
        return this + FromBytes(value);
    }

    public ByteSize AddKilobytes(double value)
    {
        return this + FromKilobytes(value);
    }

    public ByteSize AddMegabytes(double value)
    {
        return this + FromMegabytes(value);
    }

    public ByteSize AddGigabytes(double value)
    {
        return this + FromGigabytes(value);
    }

    public ByteSize AddTerabytes(double value)
    {
        return this + FromTerabytes(value);
    }

    public ByteSize Subtract(ByteSize bs)
    {
        return new ByteSize(Bytes - bs.Bytes);
    }

    public static ByteSize operator +(ByteSize b1, ByteSize b2)
    {
        return new ByteSize(b1.Bytes + b2.Bytes);
    }

    public static ByteSize operator -(ByteSize b1, ByteSize b2)
    {
        return new ByteSize(b1.Bytes - b2.Bytes);
    }

    public static ByteSize operator ++(ByteSize b)
    {
        return new ByteSize(b.Bytes + 1);
    }

    public static ByteSize operator -(ByteSize b)
    {
        return new ByteSize(-b.Bytes);
    }

    public static ByteSize operator --(ByteSize b)
    {
        return new ByteSize(b.Bytes - 1);
    }

    public static bool operator ==(ByteSize b1, ByteSize b2)
    {
        return b1.Bits == b2.Bits;
    }

    public static bool operator !=(ByteSize b1, ByteSize b2)
    {
        return b1.Bits != b2.Bits;
    }

    public static bool operator <(ByteSize b1, ByteSize b2)
    {
        return b1.Bits < b2.Bits;
    }

    public static bool operator <=(ByteSize b1, ByteSize b2)
    {
        return b1.Bits <= b2.Bits;
    }

    public static bool operator >(ByteSize b1, ByteSize b2)
    {
        return b1.Bits > b2.Bits;
    }

    public static bool operator >=(ByteSize b1, ByteSize b2)
    {
        return b1.Bits >= b2.Bits;
    }

    public static bool TryParse(ReadOnlySpan<char> s, out ByteSize result)
    {
        return TryParse(s, null, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? formatProvider, out ByteSize result)
    {
        // Arg checking
        if (s.IsEmpty || s.IsWhiteSpace())
        {
            result = default;
            return false;
        }

        // Acquiring culture-specific parsing info
        NumberFormatInfo numberFormat = GetNumberFormatInfo(formatProvider);

        const NumberStyles numberStyles = AllowDecimalPoint | AllowThousands | AllowLeadingSign;
        char[] numberSpecialChars = new[]
        {
            Convert.ToChar(numberFormat.NumberDecimalSeparator),
            Convert.ToChar(numberFormat.NumberGroupSeparator),
            Convert.ToChar(numberFormat.PositiveSign),
            Convert.ToChar(numberFormat.NegativeSign),
        };

        // Setup the result
        result = default;

        // Get the index of the first non-digit character
        s = s.TrimStart(); // Protect against leading spaces

        int num;
        bool found = false;

        // Pick first non-digit number
        for (num = 0; num < s.Length; num++)
        {
            if (!(char.IsDigit(s[num]) || numberSpecialChars.Contains(s[num])))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            return false;
        }

        int lastNumber = num;

        // Cut the input string in half
        ReadOnlySpan<char> numberPart = s.Slice(0, lastNumber).Trim();
        ReadOnlySpan<char> sizePart = s.Slice(lastNumber, s.Length - lastNumber).Trim();

        // Get the numeric part
        if (!double.TryParse(numberPart, numberStyles, formatProvider, out double number))
        {
            return false;
        }

        // Get the magnitude part
        if (sizePart.Equals(ByteSymbol, StringComparison.OrdinalIgnoreCase))
        {
            if (sizePart.Equals(BitSymbol, StringComparison.Ordinal))
            {
                // Bits
                // Can't have partial bits
                if (number % 1 != 0)
                {
                    return false;
                }

                result = FromBits((long)number);
            }
            else
            {
                // Bytes
                result = FromBytes(number);
            }
        }
        else if (sizePart.Equals(KilobyteSymbol, StringComparison.OrdinalIgnoreCase))
        {
            result = FromKilobytes(number);
        }
        else if (sizePart.Equals(MegabyteSymbol, StringComparison.OrdinalIgnoreCase))
        {
            result = FromMegabytes(number);
        }
        else if (sizePart.Equals(GigabyteSymbol, StringComparison.OrdinalIgnoreCase))
        {
            result = FromGigabytes(number);
        }
        else if (sizePart.Equals(TerabyteSymbol, StringComparison.OrdinalIgnoreCase))
        {
            result = FromTerabytes(number);
        }
        else
        {
            return false;
        }

        return true;
    }

    private static NumberFormatInfo GetNumberFormatInfo(IFormatProvider? formatProvider)
    {
        if (formatProvider is NumberFormatInfo numberFormat)
        {
            return numberFormat;
        }

        CultureInfo culture = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;

        return culture.NumberFormat;
    }

    public static ByteSize Parse(string? s)
    {
        return Parse(s, null);
    }

    public static ByteSize Parse(string? s, IFormatProvider? formatProvider)
    {
        ArgumentNullException.ThrowIfNull(s);

        if (TryParse(s, formatProvider, out ByteSize result))
        {
            return result;
        }

        throw new FormatException("Value is not in the correct format");
    }
}
#pragma warning restore 1591
