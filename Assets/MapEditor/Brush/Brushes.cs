public static class Brushes
{
    // stores an instance of all the brushes statically
    public static readonly Brush[] brushes = {
        new Pencil(BrushType.pencil), 
        new Eraser(BrushType.erase), 
        new LineTool(BrushType.line), 
        new FillTool(BrushType.fill),
        new SelectTool(BrushType.select),
    };
}