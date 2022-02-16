namespace CEo.Pokemon.HomeBalls;

public static class HomeBallsNumberExtensions
{
    public static Boolean IsInBetweenOf<TNumber>(
        this TNumber number,
        TNumber lowerBound,
        TNumber upperBound)
        where TNumber : notnull, INumber<TNumber> =>
        number >= lowerBound && number <= upperBound;

    public static Boolean IsInBetweenOf<TNumber>(
        this TNumber number,
        Int32 lowerBound,
        Int32 upperBound)
        where TNumber : notnull, INumber<TNumber> =>
        IsInBetweenOf(number, TNumber.Create(lowerBound), TNumber.Create(upperBound));

    public static String ToBase64String(
        this Byte[] bytes,
        Boolean isModifiedBase64 = false)
    {
        var base64 = Convert.ToBase64String(bytes);
        return isModifiedBase64 ?
            base64.Replace('+', '-').Replace('/', '_').Replace('=', ' ') :
            base64;
    }
}