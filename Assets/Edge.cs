using System;

public class Edge {
  public static Edge randomEdge(Random rand) {
    return new Edge(rand.Next(100) % 2 == 0);
  }

  public bool mIsWall;

  public Edge(bool isWall) {
    mIsWall = isWall;
  }
}
