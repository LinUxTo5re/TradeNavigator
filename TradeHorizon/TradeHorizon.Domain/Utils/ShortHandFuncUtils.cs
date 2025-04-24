public static class ShortHandFuncUtils
{
    public static decimal StringToDecimal(string data)
    {
        return decimal.TryParse(data, out var result) ? result : decimal.Zero;
    }

}