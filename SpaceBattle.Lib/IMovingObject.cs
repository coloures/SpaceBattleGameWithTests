namespace SpaceBattle.Lib;
public interface IMovingObject
{
    public Vector Location { get; set; }
    public Vector Velocity { get; }
}

public class Vector
{
    public int[] coordinates;
    public Vector(params int[] coordinates)
    {
        this.coordinates = coordinates;
    }
    public static Vector operator +(Vector a, Vector b)
    {
        if (a.coordinates.Length != b.coordinates.Length)
        {
            throw new Exception();
        }

        a.coordinates = a.coordinates.Zip(b.coordinates, (x, y) => x + y).ToArray<int>();
        return a;
    }
    public static Vector operator *(Vector a, int b)
    {
        a.coordinates = a.coordinates.Select(x => x * b).ToArray();
        return a;
    }
    public override bool Equals(object? obj)
    {
        if (obj is Vector other)
        {
            if (coordinates.Length != other.coordinates.Length)
            {
                return false;
            }

            var result = coordinates.Zip(other.coordinates, (x, y) => x == y ? true : false).ToArray<bool>();
            return result.All(x => x == true);
        }

        return false;
    }

    public override int GetHashCode()
    {
        const int prime = 31;
        return coordinates.Aggregate(1, (current, next) => prime * current + next.GetHashCode());
    }
}
