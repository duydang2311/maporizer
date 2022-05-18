namespace Maporizer.Helpers;

public static class GeometryHelper
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
    public static PointF GetIntersectionPointBetweenLines(PointF line1a, PointF line1b, PointF line2a, PointF line2b)
    {
        // TODO: validate precision
        float v1x = line1b.Y - line1a.Y;
        float v1y = line1a.X - line1b.X;
        float v2x = line2b.Y - line2a.Y;
        float v2y = line2a.X - line2b.X;
        float delta = v1x * v2y - v1y * v2x;
        if (delta == 0)
        {
            return PointF.Zero;
        }
        float v1c = v1x * line1a.X + v1y * line1a.Y;
        float v2c = v2x * line2a.X + v2y * line2a.Y;
        float x = (v2y * v1c - v1y * v2c) / delta;
        float y = (v1x * v2c - v2x * v1c) / delta;
        return new PointF(x, y);
    }
    public static bool IsPointInsidePath(PathF path, PointF point)
    {
        // Ray-casting
        float x = point.X;
        float y = point.Y;
        float xi;
        float yi;
        float xj;
        float yj;
        bool intersect;
        bool inside = false;
        var points = path.Points.ToArray();
        for (int i = 0, j = points.Length - 1, length = points.Length; i != length; j = i++)
        {
            xi = points[i].X;
            yi = points[i].Y;
            xj = points[j].X;
            yj = points[j].Y;

            intersect = ((yi > y) != (yj > y))
                && (x < (xj - xi) * (y - yi) / (yj - yi) + xi);
            if (intersect) inside = !inside;
        }
        return inside;
    }
    public static bool IsPointOnLine(PointF lineStart, PointF lineEnd, PointF point)
    {
        var dx = lineEnd.Y - lineStart.Y;
        var dy = lineStart.X - lineEnd.X;
        return (dx * (point.X - lineStart.X) + dy * (point.Y - lineStart.Y)) == 0;
    }
}
