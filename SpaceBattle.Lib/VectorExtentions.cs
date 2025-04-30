namespace SpaceBattle.Lib;
using System.Reflection;

public static class VectorExtensions
{
    public static int[] GetCoordinates(this Vector vector)
    {
        var field = typeof(Vector).GetField("coordinates", BindingFlags.NonPublic | BindingFlags.Instance);
        return (int[])field!.GetValue(vector)!;
    }
}
