namespace CEo.Pokemon.HomeBalls;

public static class HomeBallsStringExtensions
{
    public static String AddFileExtension(this String name, String extension)
    {
        name = name.EndsWith('.') ? name[.. ^1] : name;
        extension = extension.StartsWith('.') ? extension[1 ..] : extension;
        return String.Join('.', new[] { name, extension });
    }
}