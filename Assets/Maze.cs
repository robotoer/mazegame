using System;
using System.Collections;

namespace GoldBlastGames {
  public class Maze {
    public static Maze generateMaze(int xDim, int yDim, Game game) {
      Random rand = new Random();

      // Accumulator holding the previous row's bottom edges.
      Edge[] ups = new Edge[xDim];
      for (int x = 0; x < xDim; x++) {
        ups[x] = Edge.randomEdge(rand);
      }

      // Populate a maze instance.
      Maze maze = new Maze(yDim, xDim, game);
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
    private Game mGame;
    private int mXExit;
    private int mYExit;
  
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

    public Game Game {
      get { return mGame; }
    }

    public int XExit {
      get { return mXExit; }
      set { mXExit = value; }
    }

    public int YExit {
      get { return mYExit; }
      set { mYExit = value; }
    }
  
    public Maze(int height, int width, Game game) {
      mHeight = height;
      mWidth = width;
      mTiles = new Tile[height, width];
      mGame = game;
    }
  
    public Maze(Maze old) : this(old.Height, old.Width, old.Game) {}

    public void deepCopyTiles(Maze old) {
      for (int i=0; i < mHeight; i++) {
        for (int j=0; j < mWidth; j++) {
          mTiles[i,j] = new Tile(old.Tiles[i,j]);
          if (i > 0) {
            mTiles[i,j].Up = mTiles[i-1, j].Down;
          }
          if (j > 0) {
            mTiles[i,j].Left = mTiles[i, j-1].Right;
          }
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

    public bool validateChunk(Maze chunk, int xCoord, int yCoord) {
      if (mHeight - chunk.Height - yCoord < 0) {
        return false;
      }
      if (mWidth - chunk.Width - xCoord < 0) {
        return false;
      }
      Maze copy = new Maze(this);
      copy.deepCopyTiles(this);
      copy.initialGroups();

      for (int x=0; x < chunk.Width; x++) {
        for (int y=0; y < chunk.Height; y++) {
          copy.Tiles[y + yCoord, x + xCoord] = new Tile(chunk.Tiles[y, x]);
        }
      }
      for (int x=copy.Width; x > 0; x--) {
        for (int y=copy.Height; y > 0; y--) {
          if (y < copy.Height) {
            copy.Tiles[y, x].Down = copy.Tiles[y+1, x].Up;
          }
          if (x < mWidth) {
            copy.Tiles[y,x].Right = copy.Tiles[y, x+1].Left;
          }
        }
      }
      bool retVal = false;
      foreach (MazeRunnerMove runner in mGame.mazeRunnerMovers) {
        retVal = retVal || find(mTiles[runner.YCoord, runner.XCoord], mTiles[mYExit, mXExit]);
      }
      return retVal;
    }
  }
}