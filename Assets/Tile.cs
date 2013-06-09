namespace GoldBlastGames {
  /// <summary>
  /// Represents a Tile within a maze. This tile holds references to the 4 edges it borders.
  /// </summary>
  public class Tile {
    private Edge mUp;
    private Edge mDown;
    private Edge mLeft;
    private Edge mRight;
    private int mUnionGroup;
  
    /// <summary>
    /// Gets the upper edge of this tile.
    /// </summary>
    /// <value>
    /// Upper edge of this tile.
    /// </value>
    public Edge Up {
      get { return mUp; }
      set { mUp = value; }
    }
  
    /// <summary>
    /// Gets the lower edge of this tile.
    /// </summary>
    /// <value>
    /// Lower edge of this tile.
    /// </value>
    public Edge Down {
      get { return mDown; }
      set { mDown = value; }
    }
  
    /// <summary>
    /// Gets the left edge of this tile.
    /// </summary>
    /// <value>
    /// Left edge of this tile.
    /// </value>
    public Edge Left {
      get { return mLeft; }
      set { mLeft = value; }
    }
  
    /// <summary>
    /// Gets the right edge of this tile.
    /// </summary>
    /// <value>
    /// Right edge of this tile.
    /// </value>
    public Edge Right {
      get { return mRight; }
      set { mRight = value; }
    }
  
    /// <summary>
    /// Gets or sets the union group of this tile.
    /// </summary>
    /// <value>
    /// The union group of this tile.
    /// </value>
    public int UnionGroup {
      get { return mUnionGroup; }
      set { mUnionGroup = value; }
    }
  
    /// <summary>
    /// Initializes a new instance of the <see cref="Tile"/> class.
    /// </summary>
    /// <param name='up'>
    /// Upper edge.
    /// </param>
    /// <param name='down'>
    /// Lower edge.
    /// </param>
    /// <param name='left'>
    /// Left edge.
    /// </param>
    /// <param name='right'>
    /// Right edge.
    /// </param>
    public Tile(Edge up, Edge down, Edge left, Edge right) {
      mUp = up;
      mDown = down;
      mLeft = left;
      mRight = right;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tile"/> class.
    /// </summary>
    /// <param name='old'>
    /// Tile object of which to make a deep copy.
    /// </param>
    public Tile(Tile old) : this(new Edge(old.Up), new Edge(old.Down), new Edge(old.Left), new Edge(old.Right)) {}

    /// <summary>
    /// Returns a boolean representing whether or not all four of the tile's edges are closed.
    /// </summary>
    public bool isClosed () {
      return mUp.IsWall && mDown.IsWall && mLeft.IsWall && mRight.IsWall;
    }
  }
}
