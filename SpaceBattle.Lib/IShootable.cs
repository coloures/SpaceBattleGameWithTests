namespace SpaceBattle.Lib;
public interface IShootable
{
    Angle GetAngle(); // угол
    Vector GetLocation(); // позиция начала выстрела
    Vector GetVelocity(); // скорость, с которой выстрел летит
}
