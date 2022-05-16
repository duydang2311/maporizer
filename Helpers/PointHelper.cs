namespace Maporizer.Helpers;

public static class PointHelper
{
    public static float DistanceSquared(Point a, Point b)
    {
        float dx = (float)(a.X - b.X);
        float dy = (float)(a.Y - b.Y);
        return dx * dx + dy * dy;
    }
    public static float DistanceSquared(PointF a, PointF b)
    {
        float dx = a.X - b.X;
        float dy = a.Y - b.Y;
        return dx * dx + dy * dy;
    }
}
