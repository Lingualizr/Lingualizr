﻿using Xunit;

namespace Lingualizr.Tests.Localisation.bg;

[UseCulture("bg")]
public class NumberToWordsTests
{

    [Theory]
    [InlineData(0, "нула")]
    [InlineData(1, "един")]
    [InlineData(10, "десет")]
    [InlineData(11, "единадесет")]
    [InlineData(12, "дванадесет")]
    [InlineData(13, "тринадесет")]
    [InlineData(14, "четиринадесет")]
    [InlineData(15, "петнадесет")]
    [InlineData(16, "шестнадесет")]
    [InlineData(17, "седемнадесет")]
    [InlineData(18, "осемнадесет")]
    [InlineData(19, "деветнадесет")]
    [InlineData(20, "двадесет")]
    [InlineData(30, "тридесет")]
    [InlineData(40, "четиридесет")]
    [InlineData(50, "петдесет")]
    [InlineData(60, "шестдесет")]
    [InlineData(70, "седемдесет")]
    [InlineData(80, "осемдесет")]
    [InlineData(90, "деветдесет")]
    [InlineData(100, "сто")]
    [InlineData(200, "двеста")]
    [InlineData(300, "триста")]
    [InlineData(400, "четиристотин")]
    [InlineData(500, "петстотин")]
    [InlineData(600, "шестстотин")]
    [InlineData(700, "седемстотин")]
    [InlineData(800, "осемстотин")]
    [InlineData(900, "деветстотин")]
    [InlineData(122, "сто двадесет и две")]
    [InlineData(111, "сто и единадесет")]
    [InlineData(55, "петдесет и пет")]
    [InlineData(555, "петстотин петдесет и пет")]
    [InlineData(4213, "четири хиляди двеста и тринадесет")]
    [InlineData(5000, "пет хиляди")]
    [InlineData(28205, "двадесет и осем хиляди двеста и пет")]
    [InlineData(35000, "тридесет и пет хиляди")]
    [InlineData(352192, "триста петдесет и две хиляди сто деветдесет и две")]
    [InlineData(4000210, "четири милиона двеста и десет")]
    [InlineData(5200, "пет хиляди и двеста")]
    [InlineData(1125000, "един милион и сто двадесет и пет хиляди")]
    public void ToWordsBg(long number, string expected)
    {
        Assert.Equal(expected, number.ToWords());
    }

    [Theory]
    [InlineData(0, "нулев")]
    [InlineData(1, "първи")]
    [InlineData(2, "втори")]
    [InlineData(3, "трети")]
    [InlineData(4, "четвърти")]
    [InlineData(5, "пети")]
    [InlineData(6, "шести")]
    [InlineData(7, "седми")]
    [InlineData(8, "осми")]
    [InlineData(11, "единадесети")]
    [InlineData(12, "дванадесети")]
    [InlineData(13, "тринадесети")]
    [InlineData(14, "четиринадесети")]
    [InlineData(15, "петнадесети")]
    [InlineData(16, "шестнадесети")]
    [InlineData(17, "седемнадесети")]
    [InlineData(18, "осемнадесети")]
    [InlineData(19, "деветнадесети")]
    [InlineData(20, "двадесети")]
    [InlineData(30, "тридесети")]
    [InlineData(40, "четиридесети")]
    [InlineData(50, "петдесети")]
    [InlineData(60, "шестдесети")]
    [InlineData(70, "седемдесети")]
    [InlineData(80, "осемдесети")]
    [InlineData(90, "деветдесети")]
    [InlineData(21, "двадесет и първи")]
    [InlineData(22, "двадесет и втори")]
    [InlineData(35, "тридесет и пети")]
    public void ToOrdinalWordsMasculine(int number, string expected)
    {
        Assert.Equal(expected, number.ToOrdinalWords(GrammaticalGender.Masculine));
    }

    [Theory]
    [InlineData(0, "нулева")]
    [InlineData(1, "първа")]
    [InlineData(2, "втора")]
    [InlineData(3, "трета")]
    [InlineData(4, "четвърта")]
    [InlineData(5, "пета")]
    [InlineData(6, "шеста")]
    [InlineData(7, "седма")]
    [InlineData(8, "осма")]
    [InlineData(11, "единадесета")]
    [InlineData(12, "дванадесета")]
    [InlineData(13, "тринадесета")]
    [InlineData(14, "четиринадесета")]
    [InlineData(15, "петнадесета")]
    [InlineData(16, "шестнадесета")]
    [InlineData(17, "седемнадесета")]
    [InlineData(18, "осемнадесета")]
    [InlineData(19, "деветнадесета")]
    [InlineData(20, "двадесета")]
    [InlineData(30, "тридесета")]
    [InlineData(40, "четиридесета")]
    [InlineData(50, "петдесета")]
    [InlineData(60, "шестдесета")]
    [InlineData(70, "седемдесета")]
    [InlineData(80, "осемдесета")]
    [InlineData(90, "деветдесета")]
    [InlineData(21, "двадесет и първа")]
    [InlineData(22, "двадесет и втора")]
    [InlineData(35, "тридесет и пета")]
    public void ToOrdinalWordsFeminine(int number, string expected)
    {
        Assert.Equal(expected, number.ToOrdinalWords(GrammaticalGender.Feminine));
    }

    [Theory]
    [InlineData(0, "нулево")]
    [InlineData(1, "първо")]
    [InlineData(2, "второ")]
    [InlineData(3, "трето")]
    [InlineData(4, "четвърто")]
    [InlineData(5, "пето")]
    [InlineData(6, "шесто")]
    [InlineData(7, "седмо")]
    [InlineData(8, "осмо")]
    [InlineData(11, "единадесето")]
    [InlineData(12, "дванадесето")]
    [InlineData(13, "тринадесето")]
    [InlineData(14, "четиринадесето")]
    [InlineData(15, "петнадесето")]
    [InlineData(16, "шестнадесето")]
    [InlineData(17, "седемнадесето")]
    [InlineData(18, "осемнадесето")]
    [InlineData(19, "деветнадесето")]
    [InlineData(20, "двадесето")]
    [InlineData(30, "тридесето")]
    [InlineData(40, "четиридесето")]
    [InlineData(50, "петдесето")]
    [InlineData(60, "шестдесето")]
    [InlineData(70, "седемдесето")]
    [InlineData(80, "осемдесето")]
    [InlineData(90, "деветдесето")]
    [InlineData(21, "двадесет и първо")]
    [InlineData(22, "двадесет и второ")]
    [InlineData(35, "тридесет и пето")]
    public void ToOrdinalWordsNeuter(int number, string expected)
    {
        Assert.Equal(expected, number.ToOrdinalWords(GrammaticalGender.Neuter));
    }
}
