namespace CEo.Pokemon.HomeBalls;

public interface INamed
{
    IEnumerable<IHomeBallsString> Names { get; }
}

public interface INamed<out TString> : INamed
    where TString : notnull, IHomeBallsString
{
    new IEnumerable<TString> Names { get; }
}