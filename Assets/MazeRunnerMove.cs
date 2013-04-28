using UnityEngine;
using System.Collections;

public class MazeRunnerMove : MonoBehaviour {
  private int mXCoord = 0;
  private int mYCoord = 0;

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
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public void moveUp () {
        gameObject.transform.Translate(Vector3.forward);
    }
    
    public void moveDown () {
        gameObject.transform.Translate (-Vector3.forward);
    }
    
    public void moveLeft() {
        gameObject.transform.Translate (-Vector3.right);
    }
    
    public void moveRight() {
        gameObject.transform.Translate (Vector3.right);
    }
}
