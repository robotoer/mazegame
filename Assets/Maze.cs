using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

          if (current.isClosed()) {
            switch (rand.Next(4)) {
            case 0:
              current.Up.IsWall = false;
              break;
            case 1:
              current.Down.IsWall = false;
              break;
            case 2:
              current.Left.IsWall = false;
              break;
            case 3:
              current.Right.IsWall = false;
              break;
            default:
              break;
            }
          }
  
          ups[x] = current.Down;
          last = current.Right;
  
          // Store the tile in the maze.
          maze.mTiles[y, x] = current;
        }
      }

      // Ensure no part of the maze is inaccessible.
      maze.initialGroups();
      while (maze.unionGroups().Count > 1) {
        HashSet<int>.Enumerator groupIterator = maze.unionGroups().GetEnumerator();
        groupIterator.MoveNext();
        int first = groupIterator.Current;
        HashSet<Edge> sharedEdges = new HashSet<Edge>();

        while (sharedEdges.Count == 0) {
          groupIterator.MoveNext();
          int second = groupIterator.Current;
          sharedEdges = maze.adjacentEdges(first, second);
        }

        sharedEdges.ToArray()[rand.Next(sharedEdges.Count())].IsWall = false;
        maze.initialGroups();
      }

      return maze;
    }

    public HashSet<Edge> adjacentEdges(int a, int b) {
      HashSet<Edge> aEdges = new HashSet<Edge>();
      HashSet<Edge> bEdges = new HashSet<Edge>();
      foreach (Tile t in mTiles) {
        if (t.UnionGroup == a) {
          aEdges.Add(t.Up);
          aEdges.Add(t.Down);
          aEdges.Add(t.Left);
          aEdges.Add(t.Right);
        }
        if (t.UnionGroup == b) {
          bEdges.Add(t.Up);
          bEdges.Add(t.Down);
          bEdges.Add(t.Left);
          bEdges.Add(t.Right);
        }
      }
      HashSet<Edge> sharedEdges = new HashSet<Edge>();
      foreach (Edge e in aEdges) {
        if (bEdges.Contains(e)) {
          sharedEdges.Add(e);
        }
      }
      return sharedEdges;
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

    public enum Direction { Up, Down, Left, Right };

    public void updateWall(int tileX, int tileY, Direction direction) {
      Tile updatedTile = mTiles[tileX, tileY];
      switch (direction)
      {
        case Direction.Up:
          updatedTile.Up.IsWall = !updatedTile.Up.IsWall;
          break;
        case Direction.Down:
          updatedTile.Down.IsWall = !updatedTile.Down.IsWall;
          break;
        case Direction.Left:
          updatedTile.Left.IsWall = !updatedTile.Left.IsWall;
          break;
        case Direction.Right:
          updatedTile.Right.IsWall = !updatedTile.Right.IsWall;
          break;
      }
    }

    public HashSet<int> unionGroups() {
      HashSet<int> retVal = new HashSet<int>();
      foreach (Tile t in mTiles) {
        retVal.Add(t.UnionGroup);
      }
      return retVal;
    }
  }
}