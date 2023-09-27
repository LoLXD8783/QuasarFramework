namespace QuasarFramework.TWCLib.Definitions
{
    internal class WorldPlane
    {
        public Tile[] tileGrid;

        public static Vector2 planeCenter = new(0, 0); //we are NOT making 0, 0 the top left anymore.

        public Vector2 planeSize;

        public Vector2 planeMaxSize;

        public static Vector2 smallPlane = new(400, 200);

        public static Vector2 mediumPlane = new(1000, 450);

        public static Vector2 largePlane = new(2000, 900);

        public WorldPlane()
        {

        }

        public void GeneratePlane()
        {

        }

        public void CheckPlane()
        {

        }

        public void LoadPlaneData(TagCompound tC)
        {

        }

        public void SavePlaneData(TagCompound tC)
        {

        }

        public void UpdatePlane()
        {

        }
    }
}