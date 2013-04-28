using UnityEngine;
using System.Collections;

namespace GoldBlastGames {
  public class MazeRunnerMove : MonoBehaviour {
    private int mXCoord = 0;
    private int mYCoord = 0;
    
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
  
  	// Use this for initialization
  	void Start () {
      game = GameObject.Find("Game").GetComponent<Game>();
      maze = game.maze;
  	}
  	
  	// Update is called once per frame
  	void Update () {
  	}
      
        
    bool inBounds(int x, int y) {
      Debug.Log ("Checking bounds: " + x + "," + y);
      bool result = (x >= 0 && y >= 0 && x < game.mazeWidth && y < game.mazeHeight);
      Debug.Log (result);
      return result;
    }
  
    public void moveUp () {
      if (inBounds (mXCoord, mYCoord + 1)
            && !(maze.Tiles[mXCoord,mYCoord].Up.IsWall)) {
          Debug.Log ("Moving up");
          mYCoord += 1;
          gameObject.transform.Translate(Vector3.up);
        }
    }
    
    public void moveDown () {
      if (inBounds (mXCoord, mYCoord - 1)
            && !(maze.Tiles[mXCoord,mYCoord].Down.IsWall)) {
          Debug.Log ("Moving down");
          mYCoord -= 1;
          gameObject.transform.Translate (-Vector3.up);
        }
    }
    
    public void moveLeft() {
        if (inBounds (mXCoord - 1, mYCoord)
            && !(maze.Tiles[mXCoord,mYCoord].Left.IsWall)) {
          Debug.Log ("Moving left");
          mXCoord -= 1;
          gameObject.transform.Translate (-Vector3.right);
        }
        
    }
    
    public void moveRight() {
       if (inBounds (mXCoord + 1, mYCoord)
            && !(maze.Tiles[mXCoord,mYCoord].Right.IsWall)) {
          Debug.Log ("Moving right");
          mXCoord += 1;
          gameObject.transform.Translate (Vector3.right);
        }
    }
  }
}