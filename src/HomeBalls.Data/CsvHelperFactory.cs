using CsvHelper.TypeConversion;

namespace CsvHelper;

public interface ICsvHelperFactory : IFactory
{
    ICsvHelperFactory UseConfiguration(CsvConfiguration configuration);

    ICsvHelperFactory UseCultureInfo(CultureInfo cultureInfo);

    ICsvHelperFactory AddClassMap(params ClassMap[] maps);

    ICsvParser CreateParser(TextReader reader);

    ICsvReader CreateReader(TextReader reader);

    ICsvWriter CreateWriter(TextWriter writer);
}

public interface ICsvParser : IParser
{
    Task<Boolean> ReadAsync(CancellationToken cancellationToken = default);
}

public interface ICsvReader : IReader { }

public interface ICsvWriter : IWriter { }

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

    public virtual ICsvParser CreateParser(TextReader reader) =>
        new HomeBallsCsvParser(ExecuteBase(reader, CreateParser,  CreateParser));

    public virtual ICsvReader CreateReader(TextReader reader)
    {
        var csv = ExecuteBase(reader, CreateReader, CreateReader);
        foreach (var map in ClassMaps) csv.Context.RegisterClassMap(map);
        return new HomeBallsCsvReader(csv);
    }

    public virtual ICsvWriter CreateWriter(TextWriter writer) =>
        new HomeBallsCsvWriter(ExecuteBase(writer, CreateWriter, CreateWriter));

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

public class HomeBallsCsvParser : ICsvParser
{
    public HomeBallsCsvParser(IParser parser, ILogger? logger = default) =>
        (Parser, Logger) = (parser, logger);

    public virtual String this[Int32 index] => Parser[index];

    public virtual Int64 ByteCount => Parser.ByteCount;

    public virtual Int64 CharCount => Parser.CharCount;

    public virtual Int32 Count => Parser.Count;

    public virtual String[] Record => Parser.Record;

    public virtual String RawRecord => Parser.RawRecord;

    public virtual Int32 Row => Parser.Row;

    public virtual Int32 RawRow => Parser.RawRow;

    public virtual String Delimiter => Parser.Delimiter;

    public virtual CsvContext Context => Parser.Context;

    public virtual IParserConfiguration Configuration => Parser.Configuration;

    protected internal IParser Parser { get; }

    protected internal ILogger? Logger { get; }

    public virtual void Dispose() => Parser.Dispose();

    public virtual Boolean Read() => Parser.Read();

    public virtual Task<Boolean> ReadAsync() => Parser.ReadAsync();

    public virtual Task<Boolean> ReadAsync(
        CancellationToken cancellationToken = default) =>
        ReadAsync();
}

public class HomeBallsCsvReader : ICsvReader
{
    public HomeBallsCsvReader(IReader reader, ILogger? logger = default) =>
        (Reader, Logger) = (reader, logger);

    public virtual String this[Int32 index] => Reader[index];

    public virtual String this[String name] => Reader[name];

    public virtual String this[String name, Int32 index] => Reader[name, index];

    public virtual Int32 ColumnCount => Reader.ColumnCount;

    public virtual Int32 CurrentIndex => Reader.CurrentIndex;

    public virtual String[] HeaderRecord => Reader.HeaderRecord;

    public virtual IParser Parser => Reader.Parser;

    public virtual CsvContext Context => Reader.Context;

    public virtual IReaderConfiguration Configuration => Reader.Configuration;

    protected internal IReader Reader { get; }

    protected internal ILogger? Logger { get; }

    public virtual void Dispose() => Reader.Dispose();

    public virtual IEnumerable<T> EnumerateRecords<T>(T record) =>
        Reader.EnumerateRecords(record);

    public virtual IAsyncEnumerable<T> EnumerateRecordsAsync<T>(
        T record,
        CancellationToken cancellationToken = default) =>
        Reader.EnumerateRecordsAsync(record, cancellationToken);

    public virtual String GetField(Int32 index) =>
        Reader.GetField(index);

    public virtual String GetField(String name) =>
        Reader.GetField(name);

    public virtual String GetField(String name, Int32 index) =>
        Reader.GetField(name, index);

    public virtual object GetField(Type type, Int32 index) =>
        Reader.GetField(type, index);

    public virtual object GetField(Type type, String name) =>
        Reader.GetField(type, name);

    public virtual object GetField(Type type, String name, Int32 index) =>
        Reader.GetField(type, name, index);

    public virtual object GetField(Type type, Int32 index, ITypeConverter converter) =>
        Reader.GetField(type, index, converter);

    public virtual object GetField(Type type, String name, ITypeConverter converter) =>
        Reader.GetField(type, name, converter);

    public virtual object GetField(
        Type type,
        String name,
        Int32 index,
        ITypeConverter converter) =>
        Reader.GetField(type, name, index, converter);

    public virtual T GetField<T>(Int32 index) =>
        Reader.GetField<T>(index);

    public virtual T GetField<T>(String name) =>
        Reader.GetField<T>(name);

    public virtual T GetField<T>(String name, Int32 index) =>
        Reader.GetField<T>(name, index);

    public virtual T GetField<T>(Int32 index, ITypeConverter converter) =>
        Reader.GetField<T>(index, converter);

    public virtual T GetField<T>(String name, ITypeConverter converter) =>
        Reader.GetField<T>(name, converter);

    public virtual T GetField<T>(String name, Int32 index, ITypeConverter converter) =>
        Reader.GetField<T>(name, index, converter);

    public virtual T GetField<T, TConverter>(Int32 index)
        where TConverter : ITypeConverter =>
        Reader.GetField<T, TConverter>(index);

    public virtual T GetField<T, TConverter>(String name)
        where TConverter : ITypeConverter =>
        Reader.GetField<T, TConverter>(name);

    public virtual T GetField<T, TConverter>(String name, Int32 index)
        where TConverter : ITypeConverter =>
        Reader.GetField<T, TConverter>(name, index);

    public virtual T GetRecord<T>() =>
        Reader.GetRecord<T>();

    public virtual T GetRecord<T>(T anonymousTypeDefinition) =>
        Reader.GetRecord<T>(anonymousTypeDefinition);

    public virtual object GetRecord(Type type) =>
        Reader.GetRecord(type);

    public virtual IEnumerable<T> GetRecords<T>() =>
        Reader.GetRecords<T>();

    public virtual IEnumerable<T> GetRecords<T>(T anonymousTypeDefinition) =>
        Reader.GetRecords<T>(anonymousTypeDefinition);

    public virtual IEnumerable<object> GetRecords(Type type) =>
        Reader.GetRecords(type);

    public virtual IAsyncEnumerable<T> GetRecordsAsync<T>(
        CancellationToken cancellationToken = default) =>
        Reader.GetRecordsAsync<T>(cancellationToken);

    public virtual IAsyncEnumerable<T> GetRecordsAsync<T>(
        T anonymousTypeDefinition,
        CancellationToken cancellationToken = default) =>
        Reader.GetRecordsAsync<T>(anonymousTypeDefinition, cancellationToken);

    public virtual IAsyncEnumerable<object> GetRecordsAsync(
        Type type,
        CancellationToken cancellationToken = default) =>
        Reader.GetRecordsAsync(type, cancellationToken);

    public virtual Boolean Read() => Reader.Read();

    public virtual Task<Boolean> ReadAsync() => Reader.ReadAsync();

    public virtual Boolean ReadHeader() => Reader.ReadHeader();

    public virtual Boolean TryGetField(Type type, Int32 index, out object field) =>
        Reader.TryGetField(type, index, out field);

    public virtual Boolean TryGetField(Type type, String name, out object field) =>
        Reader.TryGetField(type, name, out field);

    public virtual Boolean TryGetField(Type type, String name, Int32 index, out object field) =>
        Reader.TryGetField(type, name, index, out field);

    public virtual Boolean TryGetField(
        Type type,
        Int32 index,
        ITypeConverter converter,
        out object field) =>
        Reader.TryGetField(type, index, converter, out field);

    public virtual Boolean TryGetField(
        Type type,
        String name,
        ITypeConverter converter,
        out object field) =>
        Reader.TryGetField(type, name, converter, out field);

    public virtual Boolean TryGetField(
        Type type,
        String name,
        Int32 index,
        ITypeConverter converter,
        out object field) =>
        Reader.TryGetField(type, name, index, converter, out field);

    public virtual Boolean TryGetField<T>(Int32 index, out T field) =>
        Reader.TryGetField<T>(index, out field);

    public virtual Boolean TryGetField<T>(String name, out T field) =>
        Reader.TryGetField<T>(name, out field);

    public virtual Boolean TryGetField<T>(String name, Int32 index, out T field) =>
        Reader.TryGetField<T>(name, index, out field);

    public virtual Boolean TryGetField<T>(Int32 index, ITypeConverter converter, out T field) =>
        Reader.TryGetField<T>(index, converter, out field);

    public virtual Boolean TryGetField<T>(String name, ITypeConverter converter, out T field) =>
        Reader.TryGetField<T>(name, converter, out field);

    public virtual Boolean TryGetField<T>(
        String name,
        Int32 index,
        ITypeConverter converter,
        out T field) =>
        Reader.TryGetField<T>(name, index, converter, out field);

    public virtual Boolean TryGetField<T, TConverter>(Int32 index, out T field)
        where TConverter : ITypeConverter =>
        Reader.TryGetField<T, TConverter>(index, out field);

    public virtual Boolean TryGetField<T, TConverter>(String name, out T field)
        where TConverter : ITypeConverter =>
        Reader.TryGetField<T, TConverter>(name, out field);

    public virtual Boolean TryGetField<T, TConverter>(String name, Int32 index, out T field)
        where TConverter : ITypeConverter =>
        Reader.TryGetField<T, TConverter>(name, index, out field);
}

public class HomeBallsCsvWriter : ICsvWriter
{
    public HomeBallsCsvWriter(IWriter writer, ILogger? logger = default) =>
        (Writer, Logger) = (writer, logger);

    public virtual String[] HeaderRecord => throw new NotImplementedException();

    public virtual Int32 Row => throw new NotImplementedException();

    public virtual Int32 Index => throw new NotImplementedException();

    public CsvContext Context => throw new NotImplementedException();

    public IWriterConfiguration Configuration => throw new NotImplementedException();

    protected internal IWriter Writer { get; }

    protected internal ILogger? Logger { get; }

    public void Dispose() => throw new NotImplementedException();

    public ValueTask DisposeAsync() => throw new NotImplementedException();

    public void Flush() => throw new NotImplementedException();

    public Task FlushAsync() => throw new NotImplementedException();

    public void NextRecord() => throw new NotImplementedException();

    public Task NextRecordAsync() => throw new NotImplementedException();

    public void WriteComment(String comment) => throw new NotImplementedException();

    public void WriteConvertedField(String field, Type fieldType) => throw new NotImplementedException();

    public void WriteField(String field) => throw new NotImplementedException();

    public void WriteField(String field, Boolean shouldQuote) => throw new NotImplementedException();

    public void WriteField<T>(T field) => throw new NotImplementedException();

    public void WriteField<T>(T field, ITypeConverter converter) => throw new NotImplementedException();

    public void WriteField<T, TConverter>(T field) => throw new NotImplementedException();

    public void WriteHeader<T>() => throw new NotImplementedException();

    public void WriteHeader(Type type) => throw new NotImplementedException();

    public void WriteRecord<T>(T record) => throw new NotImplementedException();

    public void WriteRecords(IEnumerable records) => throw new NotImplementedException();

    public void WriteRecords<T>(IEnumerable<T> records) => throw new NotImplementedException();

    public Task WriteRecordsAsync(IEnumerable records, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task WriteRecordsAsync<T>(IEnumerable<T> records, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task WriteRecordsAsync<T>(IAsyncEnumerable<T> records, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}