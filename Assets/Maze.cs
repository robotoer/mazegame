using System;
using System.Collections;

namespace GoldBlastGames {
  public class Maze {
    public static Maze generateMaze(int xDim, int yDim) {
      System.Random rand = new System.Random();

      // Accumulator holding the previous row's bottom edges.
      Edge[] ups = new Edge[xDim];
      for (int x = 0; x < xDim; x++) {
        ups[x] = Edge.randomEdge(rand);
      }

      // Populate a maze instance.
      Maze maze = new Maze(yDim, xDim);
      for (int y = 0; y < yDim; y++) {
        Edge last = Edge.randomEdge(rand);

        // Create a new row.
        for (int x = 0; x < xDim; x++) {
          Tile current = new Tile(
              ups[x],
              Edge.randomEdge(rand),
              last,
              Edge.randomEdge(rand));
  
          ups[x] = current.Down;
          last = current.Right;
  
          // Store the tile in the maze.
          maze.mTiles[y, x] = current;
        }
      }

      return maze;
    }

    private int mWidth;
    private int mHeight;
    private Tile[,] mTiles;
  
    public int Width {
      get { return mWidth; }
    }
  
    public int Height {
      get { return mHeight; }
    }
  
    public Tile[,] Tiles {
      get { return mTiles; }
      set { mTiles = value; }
    }
  
    public Maze(int height, int width) {
      mHeight = height;
      mWidth = width;
      mTiles = new Tile[height, width];
    }
  
    public Maze(Maze old) : this(old.Height, old.Width) {}

    public void deepCopyTiles(Maze old) {
      for (int i=0; i < mHeight; i++) {
        for (int j=0; j < mWidth; j++) {
          mTiles[i,j] = new Tile(old.Tiles[i,j]);
        }
      }
    }

    private void union(int startGroup, int endGroup) {
      foreach (Tile t in mTiles) {
        if (t.UnionGroup == endGroup) {
          t.UnionGroup = startGroup;
        }
      }
    }

    public void initialGroups() {
      int uid = 0;
      foreach (Tile t in mTiles) {
        t.UnionGroup = uid;
        uid++;
      }
      for (int i=0; i < mHeight; i++) {
        for (int j=0; j < mWidth; j++) {
          Tile t = mTiles[i,j];
          if (i > 0 && t.Up.IsWall == false) {
            union (t.UnionGroup, mTiles[i-1,j].UnionGroup);
          }
          if (i < mHeight-1 && t.Down.IsWall == false) {
            union (t.UnionGroup, mTiles[i+1,j].UnionGroup);
          }
          if (j > 0 && t.Left.IsWall == false) {
            union (t.UnionGroup, mTiles[i,j-1].UnionGroup);
          }
          if (j < mWidth-1 && t.Right.IsWall == false) {
            union (t.UnionGroup, mTiles[i,j+1].UnionGroup);
          }
        }
      }
    }

    public bool find(Tile start, Tile end) {
      return start.UnionGroup == end.UnionGroup;
    }
  }
}