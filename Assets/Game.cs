using UnityEngine;
using System.Collections;

namespace GoldBlastGames {
    public class Game : MonoBehaviour {
        Maze maze;
        
        public Transform MazeRunner;
        public Transform[] mazeRunners = new Transform[4];
        MazeRunnerMove[] mazeRunnerMovers = new MazeRunnerMove[4];
    
    	// Use this for initialization
    	void Start () {
            // create maze runners
            for (int i = 0; i < 4; i ++) {
                Transform runner = (Transform) Instantiate (MazeRunner);
                mazeRunners[i] = runner;
                mazeRunnerMovers[i] = (MazeRunnerMove) runner.GetComponent ("MazeRunnerMove");
            }
    	}
    	
    	// Update is called once per frame
    	void Update () {
            MazeRunnerMove move1 = mazeRunnerMovers[0];
            MazeRunnerMove move2 = mazeRunnerMovers[1];
            MazeRunnerMove move3 = mazeRunnerMovers[2];
            MazeRunnerMove move4 = mazeRunnerMovers[3];
            
    	    if (Input.GetButtonDown("1Up")) {
                move1.moveUp();
            } else if (Input.GetButtonDown("1Down")) {
                move1.moveDown();
            } else if (Input.GetButtonDown("1Left")) {
                move1.moveLeft();
            } else if (Input.GetButtonDown("1Right")) {
                move1.moveRight();
            }
     
                    
            if (Input.GetButtonDown("2Up")) {
                move2.moveUp();
            } else if (Input.GetButtonDown("2Down")) {
                move2.moveDown();
            } else if (Input.GetButtonDown("2Left")) {
                move2.moveLeft();
            } else if (Input.GetButtonDown("2Right")) {
                move2.moveRight();
            }
            
            if (Input.GetButtonDown("3Up")) {
                move3.moveUp();
            } else if (Input.GetButtonDown("3Down")) {
                move3.moveDown();
            } else if (Input.GetButtonDown("3Left")) {
                move3.moveLeft();
            } else if (Input.GetButtonDown("3Right")) {
                move3.moveRight();
            }
            
            if (Input.GetButtonDown("4Up")) {
                move4.moveUp();
            } else if (Input.GetButtonDown("4Down")) {
                move4.moveDown();
            } else if (Input.GetButtonDown("4Left")) {
                move4.moveLeft();
            } else if (Input.GetButtonDown("4Right")) {
                move4.moveRight();
            }
    	}
    }
}
