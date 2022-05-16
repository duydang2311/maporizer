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
    public static float DistanceToLineSquared(PointF point, PointF lineStart, PointF lineEnd)
    {
        var lengthSquared = (float)DistanceSquared(lineStart, lineEnd);
        if (lengthSquared == 0)
            return (float)DistanceSquared(point, lineStart);
        var t = ((point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y)) / lengthSquared;
        if (t < 0)
        {
            return (float)DistanceSquared(point, lineStart);
        }
        else if (t > 1.0)
        {
            return (float)DistanceSquared(point, lineEnd);
        }
        else
        {
            var proj = new PointF
            (
                lineStart.X + t * (lineEnd.X - lineStart.X),
                lineStart.Y + t * (lineEnd.Y - lineStart.Y)
            );
            return (float)DistanceSquared(point, proj);
        }
    }
}
