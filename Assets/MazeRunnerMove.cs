using UnityEngine;
using System.Collections;

public class MazeRunnerMove : MonoBehaviour {

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
