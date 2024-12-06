namespace SpaceBattle.Lib;

public interface IRotatingObject
{
    public Angle angle { get; set; }
    public Angle Velocity { get; }
}

public class Angle
{
    public int A { get; set; }
    const int B = 360;
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
        int sum = (angl1.A + angl2.A) % B;
        return new Angle(sum);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Angle))
            return false;

        Angle angle1 = (Angle)obj;
        return this.A == angle1.A;
    }

    public override int GetHashCode()
    {
        return this.A.GetHashCode();
    }

}