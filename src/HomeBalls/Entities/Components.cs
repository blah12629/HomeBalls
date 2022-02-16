namespace CEo.Pokemon.HomeBalls.Entities;

public interface IIdentifiable
{
    String Identifier { get; }
}

public interface IKeyed<out TKey>
    where TKey : notnull, IEquatable<TKey>
{
    TKey Id { get; }
}

public interface INamed
{
    IEnumerable<IHomeBallsString> Names { get; }
}

public interface INamed<out TString> :
    INamed
    where TString : notnull, IHomeBallsString
{
    new IEnumerable<TString> Names { get; }
}

public interface ISlottable
{
    Byte Slot { get; }
}