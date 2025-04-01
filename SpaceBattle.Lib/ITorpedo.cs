namespace SpaceBattle.Lib;
public interface ITorpedo
{
    void SetAngle(Angle angle); // установка угла
    void SetLocation(Vector vector); // установка позиции начала выстрела
    void SetVelocity(Vector vector); // установка скорости, с которой выстрел полетит
}
