public class Country
{
    public enum Name { None, Blue, Cyan, Green, Orange, Purple, Red, White, Yellow };
    public static int Count => MyUtil.EnumCount<Name>();
}
