namespace CsvHelper;

public interface ICsvHelperFactory : IFactory
{
    ICsvHelperFactory UseConfiguration(CsvConfiguration configuration);

    ICsvHelperFactory UseCultureInfo(CultureInfo cultureInfo);

    ICsvHelperFactory AddClassMap(params ClassMap[] maps);

    IParser CreateParser(TextReader reader);

    IReader CreateReader(TextReader reader);

    IWriter CreateWriter(TextWriter writer);
}

public class CsvHelperFactory : Factory, ICsvHelperFactory
{
    CsvConfiguration? _csvConfiguration;
    CultureInfo? _cultureInfo;

    protected internal ICollection<ClassMap> ClassMaps { get; } = new List<ClassMap> { };

    protected internal virtual CsvConfiguration? Configuration
    {
        get => _csvConfiguration;
        set => ResetFieldsThen(() => _csvConfiguration = value); 
    }

    protected internal virtual CultureInfo? CultureInfo
    {
        get => _cultureInfo;
        set => ResetFieldsThen(() => _cultureInfo = value); 
    }

    protected internal virtual CsvHelperFactory ResetFieldsThen(
        params Action[] actions)
    {
        (_csvConfiguration, _cultureInfo) = (default, default);
        for (var i = 0; i < actions.Length; i ++) actions[i]();
        return this;
    }

    public virtual ICsvHelperFactory AddClassMap(params ClassMap[] maps)
    {
        foreach (var map in maps) ClassMaps.Add(map);
        return this;
    }

    public virtual IParser CreateParser(TextReader reader) =>
        ExecuteBase(reader, CreateParser,  CreateParser);

    public virtual IReader CreateReader(TextReader reader)
    {
        var csv = ExecuteBase(reader, CreateReader, CreateReader);
        foreach (var map in ClassMaps) csv.Context.RegisterClassMap(map);
        return csv;
    }

    public virtual IWriter CreateWriter(TextWriter writer) =>
        ExecuteBase(writer, CreateWriter, CreateWriter);

    protected internal virtual TResult ExecuteBase<T, TResult>(
        T input,
        Func<T, CsvConfiguration, TResult> requiresConfiguration,
        Func<T, CultureInfo, TResult> requiresCultureInfo) =>
        Configuration != default ? requiresConfiguration(input, Configuration) :
        CultureInfo != default ? requiresCultureInfo(input, CultureInfo) :
        throw new NotSupportedException();

    public virtual CsvHelperFactory UseConfiguration(CsvConfiguration configuration)
    {
        Configuration = configuration;
        return this;
    }

    public virtual CsvHelperFactory UseCultureInfo(CultureInfo cultureInfo)
    {
        CultureInfo = cultureInfo;
        return this;
    }

    ICsvHelperFactory ICsvHelperFactory
        .UseConfiguration(CsvConfiguration configuration) =>
        UseConfiguration(configuration);

    ICsvHelperFactory ICsvHelperFactory
        .UseCultureInfo(CultureInfo cultureInfo) =>
        UseCultureInfo(cultureInfo);
}