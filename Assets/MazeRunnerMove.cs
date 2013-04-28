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
      Debug.Log ("Checking bounds: " + x + "," + y);
      bool result = (x >= 0 && y >= 0
          && x < Game.mazeWidth && y < Game.mazeHeight);
      Debug.Log (result);
      return result;
    }
    
  	// Use this for initialization
  	void Start () {
      game = GameObject.Find("Game").GetComponent<Game>();
      maze = game.maze;
      mXCoord = maze.Width / 2;
      mYCoord = maze.Height / 2;
      gameObject.transform.position = new Vector3(0, 0, 0);
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	}
  
    // Moving in the 4 cardinal directions.
    public void moveUp () {
      Debug.Log ("Trying to move up.");
      if (inBounds (mXCoord, mYCoord + 1)
            && !(maze.Tiles[mYCoord,mXCoord].Up.IsWall)) {
          Debug.Log ("Moving up");
          mYCoord += 1;
          gameObject.transform.Translate(Vector3.up * Game.tileHeight);
        }
    }
    
    public void moveDown () {
      Debug.Log ("Trying to move down.");
      if (inBounds (mXCoord, mYCoord - 1)
            && !(maze.Tiles[mYCoord,mXCoord].Down.IsWall)) {
          Debug.Log ("Moving down");
          mYCoord -= 1;
          gameObject.transform.Translate (-Vector3.up * Game.tileHeight);
        }
    }
    
    public void moveLeft() {
      Debug.Log ("Trying to move left.");
      if (inBounds (mXCoord - 1, mYCoord)
          && !(maze.Tiles[mYCoord,mXCoord].Left.IsWall)) {
        Debug.Log ("Moving left");
        mXCoord -= 1;
        gameObject.transform.Translate (-Vector3.right * Game.tileWidth);
      }
    }
    
    public void moveRight() {
      Debug.Log ("Trying to move right.");
     if (inBounds (mXCoord + 1, mYCoord)
          && !(maze.Tiles[mYCoord,mXCoord].Right.IsWall)) {
        Debug.Log ("Moving right");
        mXCoord += 1;
        gameObject.transform.Translate (Vector3.right * Game.tileWidth);
      }
    }
  }
}