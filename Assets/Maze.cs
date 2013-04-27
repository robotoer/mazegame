using System.Collections;

public class Edge {
    bool isWall;
}

public class Tile {
    Edge up;
    Edge down;
    Edge left;
    Edge right;
}

public class Maze {
    Tile[,] tiles;
}
