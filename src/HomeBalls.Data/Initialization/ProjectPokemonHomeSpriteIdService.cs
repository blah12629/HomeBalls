namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IProjectPokemonHomeSpriteIdService
{
    String GenerateId(HomeBallsPokemonForm form);

    String GenerateSpeciesId(HomeBallsPokemonForm form);

    String GenerateFormId(HomeBallsPokemonForm form);

    String GenerateFormIdentifier(HomeBallsPokemonForm form);

    String GenerateGenderId(HomeBallsPokemonForm form);
}

public class ProjectPokemonHomeSpriteIdService :
    IProjectPokemonHomeSpriteIdService
{
    public ProjectPokemonHomeSpriteIdService(
        ILogger? logger = default)
    {
        Logger = logger;
    }

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyList<UInt16> GenderSpeciesIds { get; } =
        new List<UInt16>
        {
            0003, 0012, 0019, 0020, 0025, 0026, 0041, 0042, 0044, 0045,
            0064, 0065, 0084, 0085, 0097, 0111, 0112, 0118, 0119, 0123,
            0129, 0130, 0133, 0154, 0165, 0166, 0178, 0185, 0186, 0190,
            0194, 0195, 0198, 0202, 0203, 0207, 0208, 0212, 0214, 0215,
            0217, 0221, 0224, 0229, 0232, 0255, 0256, 0257, 0267, 0269,
            0272, 0274, 0275, 0307, 0308, 0315, 0316, 0317, 0322, 0323,
            0332, 0350, 0369, 0396, 0397, 0398, 0399, 0400, 0401, 0402,
            0403, 0404, 0405, 0407, 0415, 0417, 0418, 0419, 0424, 0443,
            0444, 0445, 0449, 0450, 0453, 0454, 0456, 0457, 0459, 0460,
            0461, 0464, 0465, 0473, 0521, 0592, 0593, 0668, 0678, 0876
        }
        .AsReadOnly();

    protected internal IReadOnlyList<UInt16> KeptFormIds { get; } = 
        new List<UInt16>
        {
            0025, 0172, 0201, 0351, 0382, 0383, 0386, 0412, 0413, 0421,
            0422, 0423, 0479, 0487, 0492, 0493, 0550, 0555, 0585, 0586,
            0641, 0642, 0645, 0646, 0647, 0648, 0649, 0658, 0666, 0669,
            0670, 0671, 0676, 0678, 0681, 0710, 0711, 0716, 0718, 0720,
            0741, 0745, 0746, 0773, 0774, 0778, 0800, 0801, 0845, 0849,
            0854, 0855, 0875, 0877, 0876, 0888, 0889, 0890, 0892, 0893,
            0898
        }
        .AsReadOnly();

    public virtual String GenerateId(HomeBallsPokemonForm form) =>
        String.Join("_", new[]
        {
            GenerateSpeciesId(form),
            GenerateFormId(form),
            GenerateGenderId(form),
            GenerateFormIdentifier(form),
            "00000000", "f", "n"
        });

    public virtual String GenerateSpeciesId(HomeBallsPokemonForm form) =>
        form.SpeciesId.ToString().PadLeft(4, '0');

    public virtual String GenerateFormId(HomeBallsPokemonForm originalForm)
    {
        var form = ReplaceFormId(originalForm);
        var isDefault = form.FormId == 1;
        if (isDefault) return "000";

        var formIdentifier = form.FormIdentifier;
        if (formIdentifier == default) return "nnn";

        if (isDefault ||
            KeptFormIds.Contains(form.SpeciesId) ||
            formIdentifier.Contains("mega") ||
            formIdentifier.Contains("alola") ||
            formIdentifier.Contains("galar"))
            return GenerateSpriteDefaultFormId(form);

        if (formIdentifier.Contains("gmax"))
        {
            if (formIdentifier.Contains("single-strike")) return "000";
            if (formIdentifier.Contains("rapid-strike")) return "001";
            return "000";
        }

        return "nnn";
    }

    protected internal virtual HomeBallsPokemonForm ReplaceFormId(
        HomeBallsPokemonForm form)
    {
        var formId = form.FormId;
        if (form.SpeciesId == 25) formId = formId == 15 ? (Byte)1 : Convert.ToByte(Math.Max(1, formId - 6));
        if (form.SpeciesId == 172 && formId == 2) formId = 1;
        if (form.SpeciesId == 414) formId = 1;
        if (form.SpeciesId == 493 && formId == 19) formId = 1;
        if (form.SpeciesId.IsInBetweenOf(664, 665)) formId = 1;
        if (form.SpeciesId == 658) formId = formId == 2 ? (Byte)1 : formId;
        if (form.SpeciesId == 718) formId = formId switch { 1 or 3 => 1, 4 => 5, _ => formId };
        if (form.SpeciesId == 744) formId = formId == 2 ? (Byte)1 : formId;
        if (form.SpeciesId == 774) formId = formId < 8 ? (Byte)1 : formId;
        if (form.SpeciesId == 849 || form.SpeciesId == 892)
            formId = formId switch { 1 or 3 => 1, 2 or 4 => 2, _ => formId };
        if (form.SpeciesId.IsInBetweenOf(854, 855)) formId = 1;
        if ((form.FormIdentifier?.Contains("totem") ?? false))
            formId -= (Byte)(form.SpeciesId == 778 ? 2 : 1);

        return form with { FormId = formId };
    }

    protected internal virtual String GenerateSpriteDefaultFormId(
        HomeBallsPokemonForm form) =>
        (form.FormId - 1).ToString().PadLeft(3, '0');

    public virtual String GenerateFormIdentifier(HomeBallsPokemonForm form)
    {
        var identifier = form.FormIdentifier;
        if (identifier == default) return "n";
        if (identifier.Contains("gmax")) return "g";
        return "n";
    }

    public virtual String GenerateGenderId(HomeBallsPokemonForm form)
    {
        var genderRate = form.Species.GenderRate;
        if (genderRate == -1) return "uk";
        if (genderRate == 0) return "mo";
        if (genderRate == 8) return "fo";

        if (form.SpeciesId == 25 && (form.FormIdentifier?.Contains("cap") ?? false))
            return "mo";

        if (form.SpeciesId == 658 && form.FormId == 3) return "mo";

        if (form.SpeciesId == 678 || form.SpeciesId == 876)
        {
            if (form.FormId == 1) return "mo";
            else if (form.FormId == 2) return "fo";
        }

        if (GenderSpeciesIds.Contains(form.SpeciesId))
        {
            if (form.FormId == 1) return "md";
            if (form.SpeciesId == 25 && form.FormId <= 7) return "md";
        }
        return "mf";
    }
}