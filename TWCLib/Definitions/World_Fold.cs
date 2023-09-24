namespace QuasarFramework.TWCLib.Definitions
{
    internal class World_Fold
    {
        public Rectangle FoldDimensions { get; set; }

        public World_Plane planeToTravelTo;

        public World_Fold(World_Plane toPlane, Rectangle foldDimensions) 
        { 
            planeToTravelTo = toPlane;

            this.FoldDimensions = foldDimensions;
        }

        public World_Fold(World_Plane toPlane, int x, int y, int height, int width)
        {
            planeToTravelTo = toPlane;

            FoldDimensions = new(x, y, width, height);
        }
    }
}