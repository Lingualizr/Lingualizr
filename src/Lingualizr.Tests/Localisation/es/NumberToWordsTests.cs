﻿using Xunit;

namespace Lingualizr.Tests.Localisation.es;

[UseCulture("es-ES")]
public class NumberToWordsTests
{
    [Theory]
    [InlineData(-1, "primero", GrammaticalGender.Neuter)]
    [InlineData(0, "cero", GrammaticalGender.Neuter)]
    [InlineData(1, "primero", GrammaticalGender.Neuter)]
    [InlineData(1, "primero", GrammaticalGender.Masculine)]
    [InlineData(1, "primera", GrammaticalGender.Feminine)]
    [InlineData(2, "segundo", GrammaticalGender.Masculine)]
    [InlineData(2, "segunda", GrammaticalGender.Feminine)]
    [InlineData(3, "tercero", GrammaticalGender.Neuter)]
    [InlineData(3, "tercero", GrammaticalGender.Masculine)]
    [InlineData(3, "tercera", GrammaticalGender.Feminine)]
    [InlineData(4, "cuarto", GrammaticalGender.Masculine)]
    [InlineData(4, "cuarta", GrammaticalGender.Feminine)]
    [InlineData(5, "quinto", GrammaticalGender.Masculine)]
    [InlineData(5, "quinta", GrammaticalGender.Feminine)]
    [InlineData(6, "sexto", GrammaticalGender.Masculine)]
    [InlineData(6, "sexta", GrammaticalGender.Feminine)]
    [InlineData(7, "séptimo", GrammaticalGender.Masculine)]
    [InlineData(7, "séptima", GrammaticalGender.Feminine)]
    [InlineData(8, "octavo", GrammaticalGender.Masculine)]
    [InlineData(8, "octava", GrammaticalGender.Feminine)]
    [InlineData(9, "noveno", GrammaticalGender.Masculine)]
    [InlineData(9, "novena", GrammaticalGender.Feminine)]
    [InlineData(10, "décimo", GrammaticalGender.Masculine)]
    [InlineData(10, "décima", GrammaticalGender.Feminine)]
    [InlineData(11, "décimo primero", GrammaticalGender.Masculine)]
    [InlineData(11, "décima primera", GrammaticalGender.Feminine)]
    [InlineData(20, "vigésimo", GrammaticalGender.Masculine)]
    [InlineData(20, "vigésima", GrammaticalGender.Feminine)]
    [InlineData(22, "vigésimo segundo", GrammaticalGender.Masculine)]
    [InlineData(22, "vigésima segunda", GrammaticalGender.Feminine)]
    [InlineData(30, "trigésimo", GrammaticalGender.Masculine)]
    [InlineData(30, "trigésima", GrammaticalGender.Feminine)]
    [InlineData(34, "trigésimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(34, "trigésima cuarta", GrammaticalGender.Feminine)]
    [InlineData(40, "cuadragésimo", GrammaticalGender.Masculine)]
    [InlineData(40, "cuadragésima", GrammaticalGender.Feminine)]
    [InlineData(46, "cuadragésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(46, "cuadragésima sexta", GrammaticalGender.Feminine)]
    [InlineData(50, "quincuagésimo", GrammaticalGender.Masculine)]
    [InlineData(50, "quincuagésima", GrammaticalGender.Feminine)]
    [InlineData(57, "quincuagésimo séptimo", GrammaticalGender.Masculine)]
    [InlineData(57, "quincuagésima séptima", GrammaticalGender.Feminine)]
    [InlineData(60, "sexagésimo", GrammaticalGender.Masculine)]
    [InlineData(60, "sexagésima", GrammaticalGender.Feminine)]
    [InlineData(69, "sexagésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(69, "sexagésima novena", GrammaticalGender.Feminine)]
    [InlineData(70, "septuagésimo", GrammaticalGender.Masculine)]
    [InlineData(70, "septuagésima", GrammaticalGender.Feminine)]
    [InlineData(74, "septuagésimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(74, "septuagésima cuarta", GrammaticalGender.Feminine)]
    [InlineData(80, "octogésimo", GrammaticalGender.Masculine)]
    [InlineData(80, "octogésima", GrammaticalGender.Feminine)]
    [InlineData(85, "octogésimo quinto", GrammaticalGender.Masculine)]
    [InlineData(85, "octogésima quinta", GrammaticalGender.Feminine)]
    [InlineData(90, "nonagésimo", GrammaticalGender.Masculine)]
    [InlineData(90, "nonagésima", GrammaticalGender.Feminine)]
    [InlineData(99, "nonagésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(99, "nonagésima novena", GrammaticalGender.Feminine)]
    [InlineData(100, "centésimo", GrammaticalGender.Masculine)]
    [InlineData(100, "centésima", GrammaticalGender.Feminine)]
    [InlineData(101, "centésimo primero", GrammaticalGender.Masculine)]
    [InlineData(101, "centésima primera", GrammaticalGender.Feminine)]
    [InlineData(131, "centésimo trigésimo primero", GrammaticalGender.Masculine)]
    [InlineData(131, "centésima trigésima primera", GrammaticalGender.Feminine)]
    [InlineData(156, "centésimo quincuagésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(156, "centésima quincuagésima sexta", GrammaticalGender.Feminine)]
    [InlineData(214, "ducentésimo décimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(214, "ducentésima décima cuarta", GrammaticalGender.Feminine)]
    [InlineData(330, "tricentésimo trigésimo", GrammaticalGender.Masculine)]
    [InlineData(330, "tricentésima trigésima", GrammaticalGender.Feminine)]
    [InlineData(334, "tricentésimo trigésimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(334, "tricentésima trigésima cuarta", GrammaticalGender.Feminine)]
    [InlineData(400, "cuadringentésimo", GrammaticalGender.Masculine)]
    [InlineData(400, "cuadringentésima", GrammaticalGender.Feminine)]
    [InlineData(407, "cuadringentésimo séptimo", GrammaticalGender.Masculine)]
    [InlineData(407, "cuadringentésima séptima", GrammaticalGender.Feminine)]
    [InlineData(476, "cuadringentésimo septuagésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(476, "cuadringentésima septuagésima sexta", GrammaticalGender.Feminine)]
    [InlineData(500, "quingentésimo", GrammaticalGender.Masculine)]
    [InlineData(500, "quingentésima", GrammaticalGender.Feminine)]
    [InlineData(509, "quingentésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(509, "quingentésima novena", GrammaticalGender.Feminine)]
    [InlineData(549, "quingentésimo cuadragésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(549, "quingentésima cuadragésima novena", GrammaticalGender.Feminine)]
    [InlineData(600, "sexcentésimo", GrammaticalGender.Masculine)]
    [InlineData(600, "sexcentésima", GrammaticalGender.Feminine)]
    [InlineData(605, "sexcentésimo quinto", GrammaticalGender.Masculine)]
    [InlineData(605, "sexcentésima quinta", GrammaticalGender.Feminine)]
    [InlineData(670, "sexcentésimo septuagésimo", GrammaticalGender.Masculine)]
    [InlineData(670, "sexcentésima septuagésima", GrammaticalGender.Feminine)]
    [InlineData(692, "sexcentésimo nonagésimo segundo", GrammaticalGender.Masculine)]
    [InlineData(692, "sexcentésima nonagésima segunda", GrammaticalGender.Feminine)]
    [InlineData(700, "septingentésimo", GrammaticalGender.Masculine)]
    [InlineData(700, "septingentésima", GrammaticalGender.Feminine)]
    [InlineData(771, "septingentésimo septuagésimo primero", GrammaticalGender.Masculine)]
    [InlineData(771, "septingentésima septuagésima primera", GrammaticalGender.Feminine)]
    [InlineData(800, "octingentésimo", GrammaticalGender.Masculine)]
    [InlineData(800, "octingentésima", GrammaticalGender.Feminine)]
    [InlineData(849, "octingentésimo cuadragésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(849, "octingentésima cuadragésima novena", GrammaticalGender.Feminine)]
    [InlineData(900, "noningentésimo", GrammaticalGender.Masculine)]
    [InlineData(900, "noningentésima", GrammaticalGender.Feminine)]
    [InlineData(921, "noningentésimo vigésimo primero", GrammaticalGender.Masculine)]
    [InlineData(921, "noningentésima vigésima primera", GrammaticalGender.Feminine)]
    [InlineData(1000, "milésimo", GrammaticalGender.Masculine)]
    [InlineData(1000, "milésima", GrammaticalGender.Feminine)]
    [InlineData(1006, "milésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(1006, "milésima sexta", GrammaticalGender.Feminine)]
    [InlineData(1108, "milésimo centésimo octavo", GrammaticalGender.Masculine)]
    [InlineData(1108, "milésima centésima octava", GrammaticalGender.Feminine)]
    [InlineData(1323, "milésimo tricentésimo vigésimo tercero", GrammaticalGender.Masculine)]
    [InlineData(1323, "milésima tricentésima vigésima tercera", GrammaticalGender.Feminine)]
    [InlineData(2000, "dosmilésimo", GrammaticalGender.Masculine)]
    [InlineData(2000, "dosmilésima", GrammaticalGender.Feminine)]
    [InlineData(2164, "dosmilésimo centésimo sexagésimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(2164, "dosmilésima centésima sexagésima cuarta", GrammaticalGender.Feminine)]
    [InlineData(2915, "dosmilésimo noningentésimo décimo quinto", GrammaticalGender.Masculine)]
    [InlineData(2915, "dosmilésima noningentésima décima quinta", GrammaticalGender.Feminine)]
    [InlineData(3000, "tresmilésimo", GrammaticalGender.Masculine)]
    [InlineData(3000, "tresmilésima", GrammaticalGender.Feminine)]
    [InlineData(3456, "tresmilésimo cuadringentésimo quincuagésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(3456, "tresmilésima cuadringentésima quincuagésima sexta", GrammaticalGender.Feminine)]
    [InlineData(4000, "cuatromilésimo", GrammaticalGender.Masculine)]
    [InlineData(4000, "cuatromilésima", GrammaticalGender.Feminine)]
    [InlineData(4354, "cuatromilésimo tricentésimo quincuagésimo cuarto", GrammaticalGender.Masculine)]
    [InlineData(4354, "cuatromilésima tricentésima quincuagésima cuarta", GrammaticalGender.Feminine)]
    [InlineData(5000, "cincomilésimo", GrammaticalGender.Masculine)]
    [InlineData(5000, "cincomilésima", GrammaticalGender.Feminine)]
    [InlineData(5695, "cincomilésimo sexcentésimo nonagésimo quinto", GrammaticalGender.Masculine)]
    [InlineData(5695, "cincomilésima sexcentésima nonagésima quinta", GrammaticalGender.Feminine)]
    [InlineData(6000, "seismilésimo", GrammaticalGender.Masculine)]
    [InlineData(6000, "seismilésima", GrammaticalGender.Feminine)]
    [InlineData(6642, "seismilésimo sexcentésimo cuadragésimo segundo", GrammaticalGender.Masculine)]
    [InlineData(6642, "seismilésima sexcentésima cuadragésima segunda", GrammaticalGender.Feminine)]
    [InlineData(7000, "sietemilésimo", GrammaticalGender.Masculine)]
    [InlineData(7000, "sietemilésima", GrammaticalGender.Feminine)]
    [InlineData(7676, "sietemilésimo sexcentésimo septuagésimo sexto", GrammaticalGender.Masculine)]
    [InlineData(7676, "sietemilésima sexcentésima septuagésima sexta", GrammaticalGender.Feminine)]
    [InlineData(8000, "ochomilésimo", GrammaticalGender.Masculine)]
    [InlineData(8000, "ochomilésima", GrammaticalGender.Feminine)]
    [InlineData(8431, "ochomilésimo cuadringentésimo trigésimo primero", GrammaticalGender.Masculine)]
    [InlineData(8431, "ochomilésima cuadringentésima trigésima primera", GrammaticalGender.Feminine)]
    [InlineData(9000, "nuevemilésimo", GrammaticalGender.Masculine)]
    [InlineData(9000, "nuevemilésima", GrammaticalGender.Feminine)]
    [InlineData(9620, "nuevemilésimo sexcentésimo vigésimo", GrammaticalGender.Masculine)]
    [InlineData(9620, "nuevemilésima sexcentésima vigésima", GrammaticalGender.Feminine)]
    [InlineData(9999, "nuevemilésimo noningentésimo nonagésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(9999, "nuevemilésima noningentésima nonagésima novena", GrammaticalGender.Feminine)]
    [InlineData(10000, "diezmilésimo", GrammaticalGender.Masculine)]
    [InlineData(10000, "diezmilésima", GrammaticalGender.Feminine)]
    [InlineData(11000, "oncemilésimo", GrammaticalGender.Masculine)]
    [InlineData(11000, "oncemilésima", GrammaticalGender.Feminine)]
    [InlineData(20000, "veintemilésimo", GrammaticalGender.Masculine)]
    [InlineData(21000, "veintiunmilésimo", GrammaticalGender.Masculine)]
    [InlineData(21000, "veintiunmilésima", GrammaticalGender.Feminine)]
    [InlineData(30000, "treintamilésimo", GrammaticalGender.Masculine)]
    [InlineData(31000, "treinta y un milésimo", GrammaticalGender.Masculine)]
    [InlineData(31000, "treinta y una milésima", GrammaticalGender.Feminine)]
    [InlineData(84301, "ochenta y cuatro milésimo tricentésimo primero", GrammaticalGender.Masculine)]
    [InlineData(84301, "ochenta y cuatro milésima tricentésima primera", GrammaticalGender.Feminine)]
    [InlineData(99999, "noventa y nueve milésimo noningentésimo nonagésimo noveno", GrammaticalGender.Masculine)]
    [InlineData(99999, "noventa y nueve milésima noningentésima nonagésima novena", GrammaticalGender.Feminine)]
    [InlineData(100000, "cienmilésimo", GrammaticalGender.Masculine)]
    [InlineData(100000, "cienmilésima", GrammaticalGender.Feminine)]
    [InlineData(200000, "doscientosmilésimo", GrammaticalGender.Masculine)]
    [InlineData(200000, "doscientasmilésima", GrammaticalGender.Feminine)]
    [InlineData(380000, "trescientos ochenta milésimo", GrammaticalGender.Masculine)]
    [InlineData(380000, "trescientas ochenta milésima", GrammaticalGender.Feminine)]
    [InlineData(850000, "ochocientos cincuenta milésimo", GrammaticalGender.Masculine)]
    [InlineData(850000, "ochocientas cincuenta milésima", GrammaticalGender.Feminine)]
    [InlineData(214748, "doscientos catorce milésimo septingentésimo cuadragésimo octavo", GrammaticalGender.Masculine)]
    [InlineData(214748, "doscientas catorce milésima septingentésima cuadragésima octava", GrammaticalGender.Feminine)]
    [InlineData(221221, "doscientos veintiún milésimo ducentésimo vigésimo primero", GrammaticalGender.Masculine)]
    [InlineData(221221, "doscientas veintiuna milésima ducentésima vigésima primera", GrammaticalGender.Feminine)]
    [InlineData(1000000, "millonésimo", GrammaticalGender.Masculine)]
    [InlineData(1000000, "millonésima", GrammaticalGender.Feminine)]
    [InlineData(2000000, "dosmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(2000000, "dosmillonésima", GrammaticalGender.Feminine)]
    [InlineData(1001000, "un millón milésimo", GrammaticalGender.Masculine)]
    [InlineData(1221000, "un millón doscientos veintiún milésimo", GrammaticalGender.Masculine)]
    [InlineData(1221000, "un millón doscientas veintiuna milésima", GrammaticalGender.Feminine)]
    [InlineData(1500000, "un millón quinientos milésimo", GrammaticalGender.Masculine)]
    [InlineData(1500000, "un millón quinientas milésima", GrammaticalGender.Feminine)]
    [InlineData(10000000, "diezmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(10000000, "diezmillonésima", GrammaticalGender.Feminine)]
    [InlineData(15000000, "quincemillonésimo", GrammaticalGender.Masculine)]
    [InlineData(15000000, "quincemillonésima", GrammaticalGender.Feminine)]
    [InlineData(21000000, "veintiunmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(21000000, "veintiunmillonésima", GrammaticalGender.Feminine)]
    [InlineData(31000000, "treinta y un millonésimo", GrammaticalGender.Masculine)]
    [InlineData(31000000, "treinta y una millonésima", GrammaticalGender.Feminine)]
    [InlineData(50000000, "cincuentamillonésimo", GrammaticalGender.Masculine)]
    [InlineData(50000000, "cincuentamillonésima", GrammaticalGender.Feminine)]
    [InlineData(100000000, "cienmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(100000000, "cienmillonésima", GrammaticalGender.Feminine)]
    [InlineData(150000000, "ciento cincuenta millonésimo", GrammaticalGender.Masculine)]
    [InlineData(150000000, "ciento cincuenta millonésima", GrammaticalGender.Feminine)]
    [InlineData(500000000, "quinientosmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(500000000, "quinientasmillonésima", GrammaticalGender.Feminine)]
    [InlineData(1000000000, "milmillonésimo", GrammaticalGender.Masculine)]
    [InlineData(1000000000, "milmillonésima", GrammaticalGender.Feminine)]
    [InlineData(1001000000, "mil un millonésimo", GrammaticalGender.Masculine)]
    [InlineData(1001000000, "mil una millonésima", GrammaticalGender.Feminine)]
    [InlineData(1500000000, "mil quinientos millonésimo", GrammaticalGender.Masculine)]
    [InlineData(1500000000, "mil quinientas millonésima", GrammaticalGender.Feminine)]
    [InlineData(2000000000, "dos mil millonésimo", GrammaticalGender.Masculine)]
    [InlineData(2000000000, "dos mil millonésima", GrammaticalGender.Feminine)]
    [InlineData(2147483647, "dos mil ciento cuarenta y siete millones cuatrocientos ochenta y tres milésimo sexcentésimo cuadragésimo séptimo", GrammaticalGender.Masculine)]
    [InlineData(2147483647, "dos mil ciento cuarenta y siete millones cuatrocientas ochenta y tres milésima sexcentésima cuadragésima séptima", GrammaticalGender.Feminine)]
    public void ToOrdinalWords(int number, string words, GrammaticalGender gender)
    {
        Assert.Equal(words, number.ToOrdinalWords(gender));
    }

    [Theory]
    [InlineData(1, WordForm.Normal, "primero")]
    [InlineData(1, WordForm.Abbreviation, "primer")]
    [InlineData(2, WordForm.Normal, "segundo")]
    [InlineData(2, WordForm.Abbreviation, "segundo")]
    [InlineData(3, WordForm.Normal, "tercero")]
    [InlineData(3, WordForm.Abbreviation, "tercer")]
    [InlineData(21, WordForm.Normal, "vigésimo primero")]
    [InlineData(21, WordForm.Abbreviation, "vigésimo primer")]
    [InlineData(43, WordForm.Normal, "cuadragésimo tercero")]
    [InlineData(43, WordForm.Abbreviation, "cuadragésimo tercer")]
    public void ToOrdinalWordsWithWordForm(int number, WordForm wordForm, string expected)
    {
        Assert.Equal(expected, number.ToOrdinalWords(wordForm));
    }

    [Theory]
    [InlineData(1, WordForm.Normal, GrammaticalGender.Masculine, "primero")]
    [InlineData(1, WordForm.Abbreviation, GrammaticalGender.Masculine, "primer")]
    [InlineData(1, WordForm.Abbreviation, GrammaticalGender.Feminine, "primera")]
    [InlineData(2, WordForm.Normal, GrammaticalGender.Masculine, "segundo")]
    [InlineData(2, WordForm.Abbreviation, GrammaticalGender.Masculine, "segundo")]
    [InlineData(2, WordForm.Abbreviation, GrammaticalGender.Feminine, "segunda")]
    [InlineData(3, WordForm.Normal, GrammaticalGender.Masculine, "tercero")]
    [InlineData(3, WordForm.Abbreviation, GrammaticalGender.Masculine, "tercer")]
    [InlineData(3, WordForm.Abbreviation, GrammaticalGender.Feminine, "tercera")]
    [InlineData(21, WordForm.Normal, GrammaticalGender.Masculine, "vigésimo primero")]
    [InlineData(21, WordForm.Abbreviation, GrammaticalGender.Masculine, "vigésimo primer")]
    [InlineData(21, WordForm.Abbreviation, GrammaticalGender.Feminine, "vigésima primera")]
    [InlineData(43, WordForm.Normal, GrammaticalGender.Masculine, "cuadragésimo tercero")]
    [InlineData(43, WordForm.Abbreviation, GrammaticalGender.Masculine, "cuadragésimo tercer")]
    [InlineData(43, WordForm.Abbreviation, GrammaticalGender.Feminine, "cuadragésima tercera")]
    public void ToOrdinalWordsWithWordFormAndGender(int number, WordForm wordForm, GrammaticalGender gender, string expected)
    {
        Assert.Equal(expected, number.ToOrdinalWords(gender, wordForm));
    }

    [Theory]
    [InlineData(0, "cero veces")]
    [InlineData(2, "doble")]
    [InlineData(100, "cien veces")]
    public void ToTuple(int number, string expected)
    {
        Assert.Equal(expected, number.ToTuple());
    }

    [Theory]
    [InlineData(0, "cero")]
    [InlineData(1, "uno")]
    [InlineData(1, "una", GrammaticalGender.Feminine)]
    [InlineData(10, "diez")]
    [InlineData(11, "once")]
    [InlineData(15, "quince")]
    [InlineData(16, "dieciséis")]
    [InlineData(20, "veinte")]
    [InlineData(21, "veintiuno")]
    [InlineData(21, "veintiuna", GrammaticalGender.Feminine)]
    [InlineData(22, "veintidós")]
    [InlineData(25, "veinticinco")]
    [InlineData(35, "treinta y cinco")]
    [InlineData(122, "ciento veintidós")]
    [InlineData(1999, "mil novecientos noventa y nueve")]
    [InlineData(2014, "dos mil catorce")]
    [InlineData(2048, "dos mil cuarenta y ocho")]
    [InlineData(3501, "tres mil quinientos uno")]
    [InlineData(21000, "veintiún mil")]
    [InlineData(21000, "veintiuna mil", GrammaticalGender.Feminine)]
    [InlineData(21501, "veintiún mil quinientos uno")]
    [InlineData(21501, "veintiuna mil quinientas una", GrammaticalGender.Feminine)]
    [InlineData(31000, "treinta y un mil")]
    [InlineData(31000, "treinta y una mil", GrammaticalGender.Feminine)]
    [InlineData(31501, "treinta y un mil quinientos uno")]
    [InlineData(31501, "treinta y una mil quinientas una", GrammaticalGender.Feminine)]
    [InlineData(101501, "ciento un mil quinientos uno")]
    [InlineData(101501, "ciento una mil quinientas una", GrammaticalGender.Feminine)]
    [InlineData(100, "cien")]
    [InlineData(1000, "mil")]
    [InlineData(100000, "cien mil")]
    [InlineData(1000000, "un millón")]
    [InlineData(10000000, "diez millones")]
    [InlineData(100000000, "cien millones")]
    [InlineData(1000000000, "mil millones")]
    [InlineData(1000000000000, "un billón")]
    [InlineData(1_000_000_000_000_000_000, "un trillón")]
    [InlineData(111, "ciento once")]
    [InlineData(1111, "mil ciento once")]
    [InlineData(111111, "ciento once mil ciento once")]
    [InlineData(1111111, "un millón ciento once mil ciento once")]
    [InlineData(11111111, "once millones ciento once mil ciento once")]
    [InlineData(111111111, "ciento once millones ciento once mil ciento once")]
    [InlineData(1111111111, "mil ciento once millones ciento once mil ciento once")]
    [InlineData(1111111111111, "un billón ciento once mil ciento once millones ciento once mil ciento once")]
    [InlineData(1111111111111111, "mil ciento once billones ciento once mil ciento once millones ciento once mil ciento once")]
    [InlineData(1111111111111111111, "un trillón ciento once mil ciento once billones ciento once mil ciento once millones ciento once mil ciento once")]
    [InlineData(9223372036854775807, "nueve trillones doscientos veintitrés mil trescientos setenta y dos billones treinta y seis mil ochocientos cincuenta y cuatro millones setecientos setenta y cinco mil ochocientos siete")]
    [InlineData(1001111111, "mil un millones ciento once mil ciento once")]
    [InlineData(1001000001, "mil un millones uno")]
    [InlineData(1002000001, "mil dos millones uno")]
    [InlineData(2001000001, "dos mil un millones uno")]
    [InlineData(1001000000001, "un billón mil millones uno")]
    [InlineData(1001000000000001, "mil un billones uno")]
    [InlineData(1002000000000001, "mil dos billones uno")]
    [InlineData(2002000000000001, "dos mil dos billones uno")]
    [InlineData(123, "ciento veintitrés")]
    [InlineData(1234, "mil doscientos treinta y cuatro")]
    [InlineData(12345, "doce mil trescientos cuarenta y cinco")]
    [InlineData(123456, "ciento veintitrés mil cuatrocientos cincuenta y seis")]
    [InlineData(1234567, "un millón doscientos treinta y cuatro mil quinientos sesenta y siete")]
    [InlineData(12345678, "doce millones trescientos cuarenta y cinco mil seiscientos setenta y ocho")]
    [InlineData(123456789, "ciento veintitrés millones cuatrocientos cincuenta y seis mil setecientos ochenta y nueve")]
    [InlineData(1234567890, "mil doscientos treinta y cuatro millones quinientos sesenta y siete mil ochocientos noventa")]
    [InlineData(-15, "menos quince")]
    [InlineData(-123, "menos ciento veintitrés")]
    [InlineData(-1234567890, "menos mil doscientos treinta y cuatro millones quinientos sesenta y siete mil ochocientos noventa")]
    [InlineData(-9223372036854775808, "menos nueve trillones doscientos veintitrés mil trescientos setenta y dos billones treinta y seis mil ochocientos cincuenta y cuatro millones setecientos setenta y cinco mil ochocientos ocho")]
    public void ToWords(long number, string expected, GrammaticalGender gender = GrammaticalGender.Masculine)
    {
        Assert.Equal(expected, number.ToWords(gender));
    }

    [Theory]
    [InlineData(1, WordForm.Abbreviation, "un")]
    [InlineData(1, WordForm.Normal, "uno")]
    [InlineData(21, WordForm.Abbreviation, "veintiún")]
    [InlineData(21, WordForm.Normal, "veintiuno")]
    [InlineData(21501, WordForm.Abbreviation, "veintiún mil quinientos un")]
    [InlineData(21501, WordForm.Normal, "veintiún mil quinientos uno")]
    public void ToWordsIntWithWordForm(int number, WordForm wordForm, string expected)
    {
        Assert.Equal(expected, number.ToWords(wordForm));
        Assert.Equal(expected, number.ToWords(wordForm: wordForm, addAnd: false));
        Assert.Equal(expected, number.ToWords(wordForm: wordForm, addAnd: true));
    }

    [Theory]
    [InlineData(1, WordForm.Abbreviation, GrammaticalGender.Masculine, "un")]
    [InlineData(1, WordForm.Abbreviation, GrammaticalGender.Feminine, "una")]
    [InlineData(21, WordForm.Abbreviation, GrammaticalGender.Masculine, "veintiún")]
    [InlineData(21, WordForm.Abbreviation, GrammaticalGender.Feminine, "veintiuna")]
    [InlineData(21501, WordForm.Abbreviation, GrammaticalGender.Masculine, "veintiún mil quinientos un")]
    [InlineData(21501, WordForm.Normal, GrammaticalGender.Masculine, "veintiún mil quinientos uno")]
    [InlineData(21501, WordForm.Abbreviation, GrammaticalGender.Feminine, "veintiuna mil quinientas una")]
    public void ToWordsIntWithWordFormAndGender(int number, WordForm wordForm, GrammaticalGender gender, string expected)
    {
        Assert.Equal(expected, number.ToWords(wordForm, gender));
    }

    [Theory]
    [InlineData((long)1, WordForm.Abbreviation, "un")]
    [InlineData((long)1, WordForm.Normal, "uno")]
    [InlineData((long)21, WordForm.Abbreviation, "veintiún")]
    [InlineData((long)21, WordForm.Normal, "veintiuno")]
    [InlineData((long)21501, WordForm.Abbreviation, "veintiún mil quinientos un")]
    [InlineData((long)21501, WordForm.Normal, "veintiún mil quinientos uno")]
    public void ToWordsLongWithWordForm(long number, WordForm wordForm, string expected)
    {
        Assert.Equal(expected, number.ToWords(wordForm));
        Assert.Equal(expected, number.ToWords(wordForm: wordForm, addAnd: false));
        Assert.Equal(expected, number.ToWords(wordForm: wordForm, addAnd: true));
    }

    [Theory]
    [InlineData((long)1, WordForm.Abbreviation, GrammaticalGender.Masculine, "un")]
    [InlineData((long)1, WordForm.Abbreviation, GrammaticalGender.Feminine, "una")]
    [InlineData((long)21, WordForm.Abbreviation, GrammaticalGender.Masculine, "veintiún")]
    [InlineData((long)21, WordForm.Abbreviation, GrammaticalGender.Feminine, "veintiuna")]
    [InlineData((long)21501, WordForm.Abbreviation, GrammaticalGender.Masculine, "veintiún mil quinientos un")]
    [InlineData((long)21501, WordForm.Normal, GrammaticalGender.Masculine, "veintiún mil quinientos uno")]
    [InlineData((long)21501, WordForm.Abbreviation, GrammaticalGender.Feminine, "veintiuna mil quinientas una")]
    public void ToWordsLongWithWordFormAndGender(long number, WordForm wordForm, GrammaticalGender gender, string expected)
    {
        Assert.Equal(expected, number.ToWords(wordForm, gender));
    }
}