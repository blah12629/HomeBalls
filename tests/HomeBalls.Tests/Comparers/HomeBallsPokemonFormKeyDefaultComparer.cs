namespace CEo.Pokemon.HomeBalls.Comparers.Tests;

public class HomeBallsPokemonFormKeyDefaultComparerTests
{
    protected HomeBallsPokemonFormKeyDefaultComparer Sut { get; } =
        HomeBallsPokemonFormKeyDefaultComparer.Instance;

    [Theory, InlineData(710), InlineData(711)]
    public void Compare_ShouldPutForm2BeforeForm1_WhenSpeciesIs710Or711(UInt16 id)
    {
        HomeBallsPokemonFormKey form1 = (id, 1), form2 = (id, 2); //, form3 = (id, 3), form4 = (id, 4);

        var formsSorted = new[] { form1, form2, }
            .OrderBy(form => form, Sut)
            .ToList();

        formsSorted[0].Should().BeSameAs(form2);
        formsSorted[1].Should().BeSameAs(form1);
    }
}