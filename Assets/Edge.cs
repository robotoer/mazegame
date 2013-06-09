using System;

namespace GoldBlastGames {
  public class Edge {
    public static Edge randomEdge(Random rand) {
      return new Edge(rand.Next(100) <= 45);
    }

    private bool mIsWall;

    public bool IsWall {
      get { return mIsWall; }
      set { mIsWall = value; }
    }

    public Edge(bool isWall) {
      mIsWall = isWall;
    }

    public Edge(Edge old) : this(old.IsWall) {}
  }
}
