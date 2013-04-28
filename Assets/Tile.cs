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
