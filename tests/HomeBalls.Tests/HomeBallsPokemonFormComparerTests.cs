namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsPokemonFormComparerTests
{
    protected HomeBallsPokemonFormComparer Sut { get; } = new();

    [Theory, InlineData(710), InlineData(711)]
    public void Compare_ShouldPutForm2BeforeForm1_WhenSpeciesIs710Or711(UInt16 id)
    {
        var form1 = new ProtobufPokemonForm { SpeciesId = id, FormId = 1 };
        var form2 = new ProtobufPokemonForm { SpeciesId = id, FormId = 2 };
        var form3 = new ProtobufPokemonForm { SpeciesId = id, FormId = 3 };
        var form4 = new ProtobufPokemonForm { SpeciesId = id, FormId = 4 };

        var formsSorted = new[] { form1, form2, form3, form4 }
            .OrderBy(form => form, Sut)
            .ToList();

        formsSorted[0].Should().BeSameAs(form2);
        formsSorted[1].Should().BeSameAs(form1);
    }
}