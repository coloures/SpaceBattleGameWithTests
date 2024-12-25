namespace SpaceBattle.Lib;

public class Angle
{
    public int A { get; set; }

    private const int B = 360;
    public Angle(int a)
    {
        A = a;
    }

    public static Angle operator +(Angle angl1, Angle angl2)
    {
        if (angl1 == null || angl2 == null)
        {
            throw new ArgumentNullException("Один из углов равен null");
        }

        var sum = (angl1.A + angl2.A) % B;
        return new Angle(sum);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Angle))
        {
            return false;
        }

        var angle1 = (Angle)obj;
        return A == angle1.A;
    }

    public override int GetHashCode()
    {
        return A.GetHashCode();
    }
}
