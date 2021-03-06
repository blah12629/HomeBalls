namespace CEo.Pokemon.HomeBalls.Data;

public interface IProgramOptions
{
    String? ExportRoot { get; }

    Boolean IsClearingExportRoot { get; }
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

    [
        Option(
            "clear-export",
            Required = false,
            HelpText = "Clears the export root before writing the serialized data.")
    ]
    public Boolean IsClearingExportRoot { get; init; }
}