﻿namespace Maporizer.DrawingViews.Models;

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
        _path.Close();
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
        canvas.StrokeColor = StrokeColor;
        canvas.FillColor = FillColor;
        canvas.FillPath(_path, WindingMode.EvenOdd);
        canvas.DrawPath(_path);
    }
    public void Simplify()
    {
        // TODO: simplify polygon vectices using QuadTo
    }
    public override bool IsCollidedWith(PointF point)
    {
        // TODO: implement
        return false;
    }
    public override void Scale(float scale)
    {
        _path = _path.AsScaledPath(scale);
        StrokeWidth *= scale;
    }
}
