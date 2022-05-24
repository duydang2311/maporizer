using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models;

public class PolygonModel : DrawingBaseModel
{
    private PathF _path;
    public float StrokeWidth { get; set; }
    public PathF Path { get => _path; private set => _path = value; }
    public PolygonModel() : base()
    {
        _path = new PathF();
        StrokeWidth = 5f;
    }
    private (float, float, float, float)? GetCollidedLineWithInternal(PointF point, float? epsilon = null)
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
        epsilon ??= StrokeWidth * StrokeWidth * 2;
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
                return (lastX, lastY, p.X, p.Y);
            }
            lastX = p.X;
            lastY = p.Y;
        }
        return null;
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
        bool keepSimplifing = true;
        while(keepSimplifing)
        {
            keepSimplifing = false;
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
                height = GeometryHelper.DistanceToLineSquared(points[i], points[i - 1], points[i + 1]);
                baseLength = GeometryHelper.DistanceSquared(points[i - 1], points[i + 1]);

                if (height * baseLength < epsilon)
                {
                    _path.RemoveSegment(segment--);
                    keepSimplifing = true;
                }
            }
        }
    }
    public override bool HasPointOn(PointF point, float? epsilon = null)
    {
        return GetCollidedLineWithInternal(point, epsilon) is not null;
    }
    public override bool HasPointIn(PointF point)
    {
        return GeometryHelper.IsPointInsidePath(_path, point);
    }
    public override PointF? GetIntersectionPoint(PointF point, float? epsilon = null)
    {
        var points = GetCollidedLineWithInternal(point, epsilon);
        if (points is null)
        {
            return null;
        }
        var dx = points.Value.Item4 - points.Value.Item2;
        var dy = points.Value.Item1 - points.Value.Item3;
        var intersect = GeometryHelper.GetIntersectionPointBetweenLines(new PointF(points.Value.Item1, points.Value.Item2), new PointF(points.Value.Item3, points.Value.Item4), point, new PointF(point.X - dx, point.Y - dy));
        if (intersect != PointF.Zero)
        {
            return intersect;
        }
        return null;
    }
    public override void Scale(float scale)
    {
        _path = _path.AsScaledPath(scale);
        StrokeWidth *= scale;
    }
    public bool TryClip(PolygonModel drawing)
    {
        var intersect1 = drawing.GetIntersectionPoint(_path.FirstPoint);
        var intersect2 = drawing.GetIntersectionPoint(_path.LastPoint);
        if (intersect1 is null || intersect2 is null)
        {
            return false;
        }
        var points = drawing.Path.Points.ToArray();
        var polygon = new List<PointF>();
        var reverse = _path.Points.Reverse();
        bool reversed = false;
        byte count = 0;
        int start = -1;
        int end = -1;
        for (int i = 0, length = points.Length; i != length; ++i)
        {
            if (count == 1)
            {
                polygon.AddRange(reversed ? reverse : _path.Points);
                ++count;
                continue;
            }
            var previous = i - 1;
            if (previous < 0)
            {
                previous = length - 1;
            }
            if 
            (
                end == -1 &&
                (GeometryHelper.IsPointOnLine(points[i], points[previous], (PointF)intersect1)
                || GeometryHelper.IsPointOnLine(points[i], points[previous], (PointF)intersect2))
            )
            {
                ++count;
                if (start == -1)
                {
                    start = i;
                    if (GeometryHelper.IsPointOnLine(points[i], points[previous], (PointF)intersect2))
                    {
                        reversed = true;
                    }
                    continue;
                }
                end = i;
            }
            if (count != 2)
            {
                polygon.Add(points[i]);
            }
        }
        if (end == -1)
        {
            return false;
        }
        drawing.Path.Dispose();
        drawing.Path = new PathF();
        foreach (var i in polygon)
        {
            drawing.Add(i);
        }
        drawing.Path.Close();

        polygon = new List<PointF>();
        for (int i = start; i != end; ++i)
        {
            polygon.Add(points[i]);
        }
        polygon.AddRange(reversed ? _path.Points : reverse);
        _path.Dispose();
        _path = new PathF();
        foreach (var i in polygon)
        {
            _path.LineTo(i);
        }
        return true;
    }
    public override void Translate(SizeF offset)
    {
        _path.Move(offset.Width, offset.Height);
    }
    protected override void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
            }
            _path.Dispose();
            disposed = true;
        }
    }
}
