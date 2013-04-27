using System;
using System.Collections;

public class Edge {
  public bool isWall;

  public static Edge randomEdge(Random rand) {
    Edge retVal = new Edge();
    retVal.isWall = rand.Next(100) % 2 == 0;
    return retVal;
  }
}

public class Tile {
  public Edge up;
  public Edge down;
  public Edge left;
  public Edge right;
}

public class Maze {
  public Tile[,] tiles;

  public static Maze generateMaze(int xDim, int yDim) {
    Random rand = new Random();

    // Accumulator holding the previous row's bottom edges.
    Edge[] ups = new Edge[xDim];
    for (int x = 0; x < xDim; x++) {
      ups[x] = Edge.randomEdge(rand);
    }

    // Populate a maze instance.
    Maze maze = new Maze();
    maze.tiles = new Tile[xDim, yDim];
    for (int y = 0; y < yDim; y++) {
      Edge last = Edge.randomEdge(rand);

      // Create a new row.
      for (int x = 0; x < xDim; x++) {
        Tile current = new Tile();

        // Assign upper edge.
        current.up = ups[x];

        // Create a new bottom edge.
        Edge bottom = Edge.randomEdge(rand);
        current.down = bottom;
        ups[x] = bottom;

        // Assign left edge.
        current.left = last;

        // Create a new right edge.
        Edge next = Edge.randomEdge(rand);
        current.right = next;
        last = next;

        // Store the tile in the maze.
        maze.tiles[x, y] = current;
      }
    }

    return maze;
  }
}
