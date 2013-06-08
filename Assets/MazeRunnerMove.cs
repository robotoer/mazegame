using UnityEngine;
using System.Collections;

namespace GoldBlastGames {
  public class MazeRunnerMove : MonoBehaviour {
    private int mXCoord;
    private int mYCoord;
    
    Game game;
    Maze maze;
  
    public int XCoord {
      get { return mXCoord; }
      set { mXCoord = value; }
    }
  
    public int YCoord {
      get { return mYCoord; }
      set { mYCoord = value; }
    }
  
    // Check if a possible location is in the maze bounds.   
    bool inBounds(int x, int y) {
      return (x >= 0 && y >= 0
          && x < Game.mazeWidth && y < Game.mazeHeight);
    }
    
  	// Use this for initialization
  	void Start () {
      game = GameObject.Find("Game").GetComponent<Game>();
      maze = game.maze;
      mXCoord = maze.Width / 2;
      mYCoord = maze.Height / 2;
      gameObject.transform.position = new Vector3(0, 0, -0.5f);

      updateCamera();
  	}

    // Moving in the 4 cardinal directions.
    public void moveUp () {
      if (inBounds (mXCoord, mYCoord - 1)
          && !(maze.Tiles[mYCoord,mXCoord].Up.IsWall)) {
        mYCoord -= 1;
        gameObject.transform.Translate(Vector3.up * Game.tileHeight);
      }
    }
    
    public void moveDown () {
      if (inBounds (mXCoord, mYCoord + 1)
          && !(maze.Tiles[mYCoord,mXCoord].Down.IsWall)) {
        mYCoord += 1;
        gameObject.transform.Translate (-Vector3.up * Game.tileHeight);
      }
    }
    
    public void moveLeft() {
      if (inBounds (mXCoord - 1, mYCoord)
          && !(maze.Tiles[mYCoord,mXCoord].Left.IsWall)) {
        mXCoord -= 1;
        gameObject.transform.Translate (-Vector3.right * Game.tileWidth);
      }
    }
    
    public void moveRight() {
      if (inBounds (mXCoord + 1, mYCoord)
          && !(maze.Tiles[mYCoord,mXCoord].Right.IsWall)) {
        mXCoord += 1;
        gameObject.transform.Translate (Vector3.right * Game.tileWidth);
      }
    }
  }
}