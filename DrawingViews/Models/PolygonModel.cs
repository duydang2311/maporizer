using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models;

public class PolygonModel : DrawingBaseModel
{
    private PathF _path;
    public float StrokeWidth { get; set; }
    public PolygonModel() : base()
    {
        _path = new PathF();
        StrokeWidth = 5f;
    }
    public void Close()
    {
        if (_path.Points.Count() > 2)
        {
            _path.Close();
        }
    }
    public void Open()
    {
        _path.Open();
    }
    public void Add(PointF point)
    {
        _path.LineTo(point);
    }
    public override void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeSize = StrokeWidth;
        if (FillColor != Colors.Transparent)
        {
            canvas.FillColor = FillColor;
            canvas.FillPath(_path);
        }
        if (StrokeColor != Colors.Transparent)
        {
            canvas.StrokeColor = StrokeColor;
            canvas.DrawPath(_path);
        }
    }
    public void Simplify(float epsilon)
    {
        // Visvalingam-Whyatt algorithm
        var points = _path.Points.ToArray();
        var length = points.Length - 1;
        if (length < 2)
        {
            return;
        }

        float height;
        float baseLength;
        for(int i = 1, segment = 1; i != length; ++i, ++segment)
        {
            height = PointHelper.DistanceToLineSquared(points[i], points[i - 1], points[i + 1]);
            baseLength = PointHelper.DistanceSquared(points[i - 1], points[i + 1]);

            if (height * baseLength < epsilon)
            {
                _path.RemoveSegment(segment--);
            }
        }
    }
    public override bool IsCollidedWith(PointF point)
    {
        float y0 = point.Y;
        float x0 = point.X;
        float dx;
        float dy;
        float dist1;
        float dist2;
        float totalDist;
        float lastX = _path.LastPoint.X;
        float lastY = _path.LastPoint.Y;
        float epsilon = StrokeWidth * StrokeWidth;
        foreach (var p in _path.Points)
        {
            dx = x0 - lastX;
            dy = y0 - lastY;
            dist1 = dx * dx + dy * dy;
            dx = p.X - x0;
            dy = p.Y - y0;
            dist2 = dx * dx + dy * dy;
            dx = p.X - lastX;
            dy = p.Y - lastY;
            totalDist = dx * dx + dy * dy;


            if (dist1 + dist2 + 2 * Math.Sqrt(dist1) * Math.Sqrt(dist2) - totalDist <= epsilon)
            {
                return true;
            }
            lastX = p.X;
            lastY = p.Y;
        }
        return false;
    }
    //cross product is wrong for vertical line
    //public override bool IsCollidedWith(PointF point)
    //{
    //    float y0 = point.Y;
    //    float x0 = point.X;
    //    float dx1;
    //    float dy1;
    //    float dx2;
    //    float dy2;
    //    float cross;
    //    float lastX = _path.LastPoint.X;
    //    float lastY = _path.LastPoint.Y;
    //    float epsilon = StrokeWidth * StrokeWidth * StrokeWidth;
    //    foreach (var p in _path.Points)
    //    {
    //        dx1 = x0 - lastX;
    //        dy1 = y0 - lastY;
    //        dx2 = p.X - lastX;
    //        dy2 = p.Y - lastY;
    //        cross = dx1 * dy2 - dy1 * dx2;
    //        if (Math.Abs(cross) <= epsilon)
    //        {
    //            if (Math.Abs(dx2) > Math.Abs(dy2))
    //            {
    //                if (dx2 > 0 && lastX <= x0 && x0 <= p.X)
    //                {
    //                    return true;
    //                }
    //                else if (dx2 <= 0 && p.X <= x0 && x0 <= lastX)
    //                {
    //                    return true;
    //                }
    //            }
    //            else
    //            {
    //                if (dy2 > 0 && lastY <= y0 && y0 <= p.Y)
    //                {
    //                    return true;
    //                }
    //                else if (dy2 <= 0 && p.Y <= y0 && y0 <= lastY)
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //        lastX = p.X;
    //        lastY = p.Y;
    //    }
    //    return false;
    //}
    public override void Scale(float scale)
    {
        _path = _path.AsScaledPath(scale);
        StrokeWidth *= scale;
    }
}
