﻿using Xunit;

namespace Lingualizr.Tests.Localisation.cs;

[UseCulture("cs-CZ")]
public class NumberToWordsTests
{
    [Theory]
    [InlineData(0, "nula")]
    [InlineData(1, "jeden")]
    [InlineData(2, "dva")]
    [InlineData(3, "tři")]
    [InlineData(4, "čtyři")]
    [InlineData(5, "pět")]
    [InlineData(6, "šest")]
    [InlineData(7, "sedm")]
    [InlineData(8, "osm")]
    [InlineData(9, "devět")]
    [InlineData(10, "deset")]
    [InlineData(11, "jedenáct")]
    [InlineData(12, "dvanáct")]
    [InlineData(13, "třináct")]
    [InlineData(14, "čtrnáct")]
    [InlineData(15, "patnáct")]
    [InlineData(16, "šestnáct")]
    [InlineData(17, "sedmnáct")]
    [InlineData(18, "osmnáct")]
    [InlineData(19, "devatenáct")]
    [InlineData(20, "dvacet")]
    [InlineData(22, "dvacet dva")]
    [InlineData(30, "třicet")]
    [InlineData(40, "čtyřicet")]
    [InlineData(50, "padesát")]
    [InlineData(60, "šedesát")]
    [InlineData(70, "sedmdesát")]
    [InlineData(80, "osmdesát")]
    [InlineData(90, "devadesát")]
    [InlineData(100, "sto")]
    [InlineData(112, "sto dvanáct")]
    [InlineData(128, "sto dvacet osm")]
    [InlineData(1000, "jeden tisíc")]
    [InlineData(2000, "dva tisíce")]
    [InlineData(5000, "pět tisíc")]
    [InlineData(10000, "deset tisíc")]
    [InlineData(20000, "dvacet tisíc")]
    [InlineData(21000, "dvacet jedna tisíc")]
    [InlineData(22000, "dvacet dva tisíc")]
    [InlineData(25000, "dvacet pět tisíc")]
    [InlineData(100000, "sto tisíc")]
    [InlineData(500000, "pět set tisíc")]
    [InlineData(1000000, "jeden milion")]
    [InlineData(2000000, "dva miliony")]
    [InlineData(5000000, "pět milionů")]
    [InlineData(1000000000, "jedna miliarda")]
    [InlineData(1001001001, "jedna miliarda jeden milion jeden tisíc jeden")]
    [InlineData(2000000000, "dvě miliardy")]
    [InlineData(1501001892, "jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dva")]
    [InlineData(2147483647, "dvě miliardy sto čtyřicet sedm milionů čtyři sta osmdesát tři tisíc šest set čtyřicet sedm")]
    [InlineData(-1501001892, "mínus jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dva")]
    public void ToWordsCzech(int number, string expected)
    {
        Assert.Equal(expected, number.ToWords());
    }

    [Theory]
    [InlineData(0, "nula")]
    [InlineData(1, "jedna")]
    [InlineData(2, "dvě")]
    [InlineData(3, "tři")]
    [InlineData(4, "čtyři")]
    [InlineData(5, "pět")]
    [InlineData(6, "šest")]
    [InlineData(7, "sedm")]
    [InlineData(8, "osm")]
    [InlineData(9, "devět")]
    [InlineData(10, "deset")]
    [InlineData(11, "jedenáct")]
    [InlineData(12, "dvanáct")]
    [InlineData(13, "třináct")]
    [InlineData(14, "čtrnáct")]
    [InlineData(15, "patnáct")]
    [InlineData(16, "šestnáct")]
    [InlineData(17, "sedmnáct")]
    [InlineData(18, "osmnáct")]
    [InlineData(19, "devatenáct")]
    [InlineData(20, "dvacet")]
    [InlineData(22, "dvacet dvě")]
    [InlineData(30, "třicet")]
    [InlineData(40, "čtyřicet")]
    [InlineData(50, "padesát")]
    [InlineData(60, "šedesát")]
    [InlineData(70, "sedmdesát")]
    [InlineData(80, "osmdesát")]
    [InlineData(90, "devadesát")]
    [InlineData(100, "sto")]
    [InlineData(112, "sto dvanáct")]
    [InlineData(128, "sto dvacet osm")]
    [InlineData(1000, "jeden tisíc")]
    [InlineData(2000, "dva tisíce")]
    [InlineData(5000, "pět tisíc")]
    [InlineData(10000, "deset tisíc")]
    [InlineData(20000, "dvacet tisíc")]
    [InlineData(21000, "dvacet jedna tisíc")]
    [InlineData(22000, "dvacet dva tisíc")]
    [InlineData(25000, "dvacet pět tisíc")]
    [InlineData(100000, "sto tisíc")]
    [InlineData(500000, "pět set tisíc")]
    [InlineData(1000000, "jeden milion")]
    [InlineData(2000000, "dva miliony")]
    [InlineData(5000000, "pět milionů")]
    [InlineData(1000000000, "jedna miliarda")]
    [InlineData(1001001001, "jedna miliarda jeden milion jeden tisíc jedna")]
    [InlineData(2000000000, "dvě miliardy")]
    [InlineData(1501001892, "jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dvě")]
    [InlineData(2147483647, "dvě miliardy sto čtyřicet sedm milionů čtyři sta osmdesát tři tisíc šest set čtyřicet sedm")]
    [InlineData(-1501001892, "mínus jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dvě")]
    public void ToWordsCzechFeminine(int number, string expected)
    {
        Assert.Equal(expected, number.ToWords(GrammaticalGender.Feminine));
    }

    [Theory]
    [InlineData(0, "nula")]
    [InlineData(1, "jedno")]
    [InlineData(2, "dvě")]
    [InlineData(3, "tři")]
    [InlineData(4, "čtyři")]
    [InlineData(5, "pět")]
    [InlineData(6, "šest")]
    [InlineData(7, "sedm")]
    [InlineData(8, "osm")]
    [InlineData(9, "devět")]
    [InlineData(10, "deset")]
    [InlineData(11, "jedenáct")]
    [InlineData(12, "dvanáct")]
    [InlineData(13, "třináct")]
    [InlineData(14, "čtrnáct")]
    [InlineData(15, "patnáct")]
    [InlineData(16, "šestnáct")]
    [InlineData(17, "sedmnáct")]
    [InlineData(18, "osmnáct")]
    [InlineData(19, "devatenáct")]
    [InlineData(20, "dvacet")]
    [InlineData(22, "dvacet dvě")]
    [InlineData(30, "třicet")]
    [InlineData(40, "čtyřicet")]
    [InlineData(50, "padesát")]
    [InlineData(60, "šedesát")]
    [InlineData(70, "sedmdesát")]
    [InlineData(80, "osmdesát")]
    [InlineData(90, "devadesát")]
    [InlineData(100, "sto")]
    [InlineData(112, "sto dvanáct")]
    [InlineData(128, "sto dvacet osm")]
    [InlineData(1000, "jeden tisíc")]
    [InlineData(2000, "dva tisíce")]
    [InlineData(5000, "pět tisíc")]
    [InlineData(10000, "deset tisíc")]
    [InlineData(20000, "dvacet tisíc")]
    [InlineData(21000, "dvacet jedna tisíc")]
    [InlineData(22000, "dvacet dva tisíc")]
    [InlineData(25000, "dvacet pět tisíc")]
    [InlineData(100000, "sto tisíc")]
    [InlineData(500000, "pět set tisíc")]
    [InlineData(1000000, "jeden milion")]
    [InlineData(2000000, "dva miliony")]
    [InlineData(5000000, "pět milionů")]
    [InlineData(1000000000, "jedna miliarda")]
    [InlineData(1001001001, "jedna miliarda jeden milion jeden tisíc jedno")]
    [InlineData(2000000000, "dvě miliardy")]
    [InlineData(1501001892, "jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dvě")]
    [InlineData(2147483647, "dvě miliardy sto čtyřicet sedm milionů čtyři sta osmdesát tři tisíc šest set čtyřicet sedm")]
    [InlineData(-1501001892, "mínus jedna miliarda pět set jedna milionů jeden tisíc osm set devadesát dvě")]
    public void ToWordsCzechNeuter(int number, string expected)
    {
        Assert.Equal(expected, number.ToWords(GrammaticalGender.Neuter));
    }
}