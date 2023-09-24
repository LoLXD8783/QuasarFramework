namespace QuasarFramework.TWCLib.Definitions
{
    internal class World_Plane
    {
        public Tile[] tileGrid;

        public static Vector2 planeCenter = new(0, 0); //we are NOT making 0, 0 the top left anymore.

        public Vector2 planeSize;

        public Vector2 planeMaxSize;

        public void LoadPlaneData()
        {

        }

        public void SavePlaneData(TagCompound tC)
        {

        }

        public void UpdatePlane(TagCompound tC)
        {

        }
    }
}