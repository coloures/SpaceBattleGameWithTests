public class Vector
{
    private int[] coordinates;
    public Vector(params int[] coordinates)
    {
        this.coordinates = coordinates;
    }
    public static Vector operator +(Vector a, Vector b)
    {
        if (a.coordinates.Length != b.coordinates.Length)
        {
            throw new ArgumentException();
        }

        a.coordinates = a.coordinates.Zip(b.coordinates, (x, y) => x + y).ToArray<int>();
        return a;
    }
    public static bool operator ==(Vector left, Vector right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }
    public static bool operator !=(Vector left, Vector right)
    {
        return !(left == right);
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
