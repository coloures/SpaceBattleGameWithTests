namespace SpaceBattle.Lib
{
    public static class AngleExtensions
    {
        public static double ToDouble(this Angle angle)
        {
            if (angle == null)
            {
                throw new ArgumentNullException(nameof(angle), "Угол не может быть null");
            }

            return (double)angle.A;
        }
    }
}
