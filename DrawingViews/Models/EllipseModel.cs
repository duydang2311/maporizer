﻿namespace Maporizer.DrawingViews.Models;

public class EllipseModel : DrawingBaseModel
{
    public PointF Location { get; set; }
    public SizeF Size { get; set; }
    public EllipseModel() : base()
    {
        Location = new PointF();
        Size = new SizeF();
    }
    public override void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Rect rect = new(Location, Size);
        canvas.StrokeSize = (float)Size.Width / 25f;
        canvas.StrokeColor = StrokeColor;
        canvas.FillColor = FillColor;
        canvas.FillEllipse(rect);
        canvas.DrawEllipse(rect);
    }
    public override bool IsCollidedWith(PointF point)
    {
        var radius = (Size.Width + Size.Width / 25f) / 2;
        var center = new PointF(Location.X + radius, Location.Y + radius);
        var dx = point.X - center.X;
        var dy = point.Y - center.Y;
        var distanceSquared = dx * dx + dy * dy;
        return distanceSquared <= radius * radius;
    }
}
