namespace QuasarFramework.TWCLib.Definitions
{
    internal struct WorldFold
    {
        public Rectangle FoldDimensions { get; set; }

        public WorldPlane planeToTravelTo;

        public WorldFold(WorldPlane toPlane, Rectangle foldDimensions) 
        { 
            planeToTravelTo = toPlane;

            this.FoldDimensions = foldDimensions;
        }

        public WorldFold(WorldPlane toPlane, int x, int y, int height, int width)
        {
            planeToTravelTo = toPlane;

            FoldDimensions = new(x, y, width, height);
        }
    }
}