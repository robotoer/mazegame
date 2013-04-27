using System;
using System.Collections;

public class Edge {
  public static Edge randomEdge(Random rand) {
    return new Edge(rand.Next(100) % 2 == 0);
  }

  public bool mIsWall;

  public Edge(bool isWall) {
    mIsWall = isWall;
  }
}

public class Tile {
  private Edge mUp;
  private Edge mDown;
  private Edge mLeft;
  private Edge mRight;
  private int mUnionGroup;

  public Edge Up {
    get { return mUp; }
  }

  public Edge Down {
    get { return mDown; }
  }

  public Edge Left {
    get { return mLeft; }
  }

  public Edge Right {
    get { return mRight; }
  }

  public int UnionGroup {
    get { return mUnionGroup; }
    set { mUnionGroup = value; }
  }


  public Tile(Edge up, Edge down, Edge left, Edge right) {
    mUp = up;
    mDown = down;
    mLeft = left;
    mRight = right;
  }
}

public class Maze {
  public int mWidth;
  public int mHeight;
  public Tile[,] mTiles;

  public Maze(int height, int width) {
    mHeight = height;
    mWidth = width;
    mTiles = new Tile[height, width];
    for (int i=0; i < height; i++) {
      for (int j=0; j < width; j++) {
        mTiles[i,j] = new Tile(new Edge(true), new Edge(true), new Edge(false), new Edge(false));
      }
    }
  }

  public static Maze generateMaze(int xDim, int yDim) {
    Random rand = new Random();

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
        maze.mTiles[x, y] = current;
      }
    }

    return maze;
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
        if (t.Up.mIsWall == false && i > 0) {
          union (t.UnionGroup, mTiles[i-1,j].UnionGroup);
        }
        if (t.Down.mIsWall == false && i < mHeight-1) {
          union (t.UnionGroup, mTiles[i+1,j].UnionGroup);
        }
        if (t.Left.mIsWall == false && j > 0) {
          union (t.UnionGroup, mTiles[i,j-1].UnionGroup);
        }
        if (t.Right.mIsWall == false && j < mWidth-1) {
          union (t.UnionGroup, mTiles[i,j+1].UnionGroup);
        }
      }
    }
  }

  public bool find(Tile start, Tile end) {
    return start.UnionGroup == end.UnionGroup;
  }
}
