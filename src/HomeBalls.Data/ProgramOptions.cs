namespace CEo.Pokemon.HomeBalls.Data;

public interface IProgramOptions
{
    String? ExportRoot { get; }
}

public record ProgramOptions : IProgramOptions
{
    [
        Option(
            'e', "export",
            Required = false,
            HelpText = "Sets the export root of the serialized data.")
    ]
    public String? ExportRoot { get; init; }
}