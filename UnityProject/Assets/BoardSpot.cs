namespace JackSParrot.Navigation.Pathfinding
{
    public struct BoardSpot
    {
        public BoardPoint position;
        public bool walkable;
        public int g;
        public int h;
        public int f;
        public int previousIdx;

        public System.Collections.Generic.List<int> Neighbours;
        public void InitNeighbours(int boardCols, int boardRows, bool diagonalLinks)
        {
            Neighbours = new System.Collections.Generic.List<int>();
            if (position.x < boardCols - 1)
            {
                Neighbours.Add(position.y * boardCols + position.x + 1);
            }
            if (position.x > 0)
            {
                Neighbours.Add(position.y * boardCols + position.x - 1);
            }
            if (position.y < boardRows - 1)
            {
                Neighbours.Add((position.y + 1) * boardCols + position.x);
            }
            if (position.y > 0)
            {
                Neighbours.Add((position.y - 1) * boardCols + position.x);
            }
            if (diagonalLinks)
            {
                if (position.x > 0 && position.y > 0)
                {
                    Neighbours.Add((position.y - 1) * boardCols + position.x - 1);
                }
                if (position.x < boardCols - 1 && position.y < boardRows - 1)
                {
                    Neighbours.Add((position.y + 1) * boardCols + position.x + 1);
                }
                if (position.x < boardCols - 1 && position.y > 0)
                {
                    Neighbours.Add((position.y - 1) * boardCols + position.x + 1);
                }
                if (position.x > 0 && position.y < boardRows - 1)
                {
                    Neighbours.Add((position.y + 1) * boardCols + position.x - 1);
                }
            }
        }

        public UnityEngine.GameObject prefab;
        UnityEngine.GameObject obj;
        UnityEngine.Renderer rend;

        public void Show(UnityEngine.Color color)
        {
            if (obj == null)
            {
                obj = UnityEngine.Object.Instantiate(prefab);
                obj.transform.name = position.x + " : " + position.y;
                obj.transform.position = new UnityEngine.Vector3(position.x, 0f, position.y);
                rend = obj.GetComponent<UnityEngine.Renderer>();
                rend.material = new UnityEngine.Material(rend.material);
            }
            rend.material.color = color;
        }
    }
}