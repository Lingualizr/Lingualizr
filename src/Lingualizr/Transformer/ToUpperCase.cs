﻿using System.Globalization;

namespace Lingualizr.Transformer
{
    internal class ToUpperCase : ICulturedStringTransformer
    {
        public string Transform(string input)
        {
            return Transform(input, null);
        }

        public string Transform(string input, CultureInfo culture)
        {
            culture ??= CultureInfo.CurrentCulture;

            return culture.TextInfo.ToUpper(input);
        }
    }
}
