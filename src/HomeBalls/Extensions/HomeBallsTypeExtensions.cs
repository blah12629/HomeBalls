namespace CEo.Pokemon.HomeBalls;

public static class HomeBallsTypeExtensions
{
    public static String GetFullNameNonNull(this Type type) =>
        type.FullName ?? type.Name;
}