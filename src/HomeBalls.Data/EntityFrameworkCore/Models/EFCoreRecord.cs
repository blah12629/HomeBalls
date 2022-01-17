namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public abstract record EFCoreBaseRecord :
        HomeBallsRecord { }

    public abstract record EFCoreRecord<TKey> :
        EFCoreBaseRecord,
        IKeyed<TKey>
        where TKey : notnull, IEquatable<TKey>
    {
        #nullable disable
        protected EFCoreRecord() { }
        #nullable enable

        public virtual TKey Id { get; init; }
    }

    public abstract record EFCoreNamedRecord<TKey> :
        EFCoreRecord<TKey>,
        IIdentifiable,
        INamed<EFCoreString>
        where TKey : notnull, IEquatable<TKey>
    {
        #nullable disable
        protected EFCoreNamedRecord() { }
        #nullable enable

        public virtual String Identifier { get; init; }

        public virtual IEnumerable<EFCoreString> Names { get; init; } =
            new List<EFCoreString> { };

        IEnumerable<IHomeBallsString> INamed.Names => Names;
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public abstract class EFCoreRecordConfiguration<TRecord> :
        IEntityTypeConfiguration<TRecord>
        where TRecord : notnull, EFCoreBaseRecord
    {
        EntityTypeBuilder<TRecord>? _builder;
        Type? _recordType;

        protected EFCoreRecordConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger)
        {
            Configurations = configurations ?? new List<Expression<Action>> { };
            Logger = logger;
        }

        protected internal EntityTypeBuilder<TRecord> Builder
        {
            get => _builder ?? throw new NullReferenceException();
            set => _builder = value;
        }

        protected internal Type RecordType => _recordType ??= typeof(TRecord);

        protected internal IList<Expression<Action>> Configurations { get; }

        protected internal ILogger? Logger { get; }

        public void Configure(EntityTypeBuilder<TRecord> builder)
        {
            Builder = builder;
            ConfigureCore();
            ExecuteConfigurations();
        }

        protected internal virtual void ConfigureCore()
        {
            ConfigureKey();
            ConfigureIdentifier();
        }

        protected internal virtual void ExecuteConfigurations()
        {
            var typeName = GetType().Name;

            foreach (var configuration in Configurations)
                try { configuration.Compile().Invoke(); }
                catch(Exception exception)
                {
                    Logger?.LogError(
                        exception,
                        $"Configuration `{typeName}` failed at " +
                        $"{configuration.Body.ToString()}.");

                    throw;
                }

            Logger?.LogDebug(String.Join(Environment.NewLine, Configurations
                .Select(configuration => $"\t{configuration.Body.ToString()}")
                .Prepend($"Configuration `{typeName}` successful.")));
        }

        protected internal virtual void ConfigureLogged(
            Expression<Action> configuration) =>
            Configurations.Add(configuration);

        protected internal virtual void ConfigureKey()
        {
            if (isRecord(typeof(IKeyed<Byte>)))
                ConfigureLogged(() => Builder
                    .HasKey(record => ((IKeyed<Byte>)record).Id));

            else if (isRecord(typeof(IKeyed<UInt16>)))
                ConfigureLogged(() => Builder
                    .HasKey(record => ((IKeyed<UInt16>)record).Id));

            else if (isRecord(typeof(IKeyed<UInt32>)))
                ConfigureLogged(() => Builder
                    .HasKey(record => ((IKeyed<UInt32>)record).Id));

            Boolean isRecord(Type type) => RecordType.IsAssignableTo(type);
        }

        protected internal virtual void ConfigureIdentifier()
        {
            if (RecordType.IsAssignableTo(typeof(IIdentifiable)))
                ConfigureLogged(() => Builder
                    .HasAlternateKey(record => ((IIdentifiable)record).Identifier));
        }

        protected internal virtual void ConfigureNames()
        {
            if (RecordType.IsAssignableTo(typeof(INamed<EFCoreString>)))
                ConfigureLogged(() => Builder
                    .HasMany(record => ((INamed<EFCoreString>)record).Names));
        }
    }
}