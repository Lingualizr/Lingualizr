namespace Lingualizr.Localisation.NumberToWords;

internal class ThaiNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    public override string Convert(long number)
    {
        string textReturn = string.Empty;
        if (number == 0)
        {
            return "ศูนย์";
        }

        if (number < 0)
        {
            textReturn = "ลบ";
            number = -number;
        }

        if (number / 1000000 > 0)
        {
            textReturn += Convert(number / 1000000) + "ล้าน";
            number %= 1000000;
        }

        if (number / 100000 > 0)
        {
            textReturn += Convert(number / 100000) + "แสน";
            number %= 100000;
        }

        if (number / 10000 > 0)
        {
            textReturn += Convert(number / 10000) + "หมื่น";
            number %= 10000;
        }

        if (number / 1000 > 0)
        {
            textReturn += Convert(number / 1000) + "พัน";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            textReturn += Convert(number / 100) + "ร้อย";
            number %= 100;
        }

        if (number > 0)
        {
            if (!string.IsNullOrEmpty(textReturn))
            {
                textReturn += string.Empty;
            }

            string[] unitsMap = new[] { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "เเปด", "เก้า", "สิบ", "สิบเอ็ด", "สิบสอง", "สิบสาม", "สิบสี่", "สิบห้า", "สิบหก", "สิบเจ็ด", "สิบเเปด", "สิบเก้า" };
            string[] tensMap = new[] { "ศูนย์", "สิบ", "ยี่สิบ", "สามสิบ", "สี่สิบ", "ห้าสิบ", "หกสิบ", "เจ็ดสิบ", "แปดสิบ", "เก้าสิบ" };

            if (number < 20)
            {
                textReturn += unitsMap[number];
            }
            else
            {
                textReturn += tensMap[number / 10];
                if (number % 10 > 0)
                {
                    textReturn += string.Empty + unitsMap[number % 10];
                }
            }
        }

        return textReturn;
    }

    public override string ConvertToOrdinal(int number)
    {
        return Convert(number);
    }
}
