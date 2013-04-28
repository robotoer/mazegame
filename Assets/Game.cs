using UnityEngine;
using System.Collections;

namespace GoldBlastGames
{
  public class Game : MonoBehaviour
  {
    // Maze and Chunk sizes.
    int mazeWidth = 9;
    int mazeHeight = 9;
    int chunkWidth = 3;
    int chunkHeight = 3;
    int numChunks = 3;
    
    // Maze and MazeObject.
    Maze maze;
    
    // Chunks deck for wizard.
    Maze[] chunksDeck;
    
    // MazeRunners.
    public Transform MazeRunner;
    public Transform[] mazeRunners = new Transform[4];
    MazeRunnerMove[] mazeRunnerMovers = new MazeRunnerMove[4];

    // ------ Initialization ------
    void Start ()
    {
      // Create maze.
      maze = Maze.generateMaze(mazeWidth, mazeHeight);
      
      // Create chunks for wizard.
      chunksDeck = new Maze[numChunks];
      for (int i = 0; i < numChunks; i ++) {
        chunksDeck[i] = Maze.generateMaze(chunkWidth, chunkHeight);
      }
      
      // TODO: render maze object; render chunks.
      
      // Create maze runners.
      for (int i = 0; i < 4; i ++) {
        Transform runner = (Transform)Instantiate (MazeRunner);
        mazeRunners [i] = runner;
        mazeRunnerMovers [i] = (MazeRunnerMove)runner.GetComponent ("MazeRunnerMove");
      }
    }

    // ------ Update ------
    void Update ()
    {
      MazeRunnerMove move1 = mazeRunnerMovers [0];
      MazeRunnerMove move2 = mazeRunnerMovers [1];
      MazeRunnerMove move3 = mazeRunnerMovers [2];
      MazeRunnerMove move4 = mazeRunnerMovers [3];

      if (Input.GetButtonDown ("1Up")) {
        move1.moveUp ();
      } else if (Input.GetButtonDown ("1Down")) {
        move1.moveDown ();
      } else if (Input.GetButtonDown ("1Left")) {
        move1.moveLeft ();
      } else if (Input.GetButtonDown ("1Right")) {
        move1.moveRight ();
      }


      if (Input.GetButtonDown ("2Up")) {
        move2.moveUp ();
      } else if (Input.GetButtonDown ("2Down")) {
        move2.moveDown ();
      } else if (Input.GetButtonDown ("2Left")) {
        move2.moveLeft ();
      } else if (Input.GetButtonDown ("2Right")) {
        move2.moveRight ();
      }

      if (Input.GetButtonDown ("3Up")) {
        move3.moveUp ();
      } else if (Input.GetButtonDown ("3Down")) {
        move3.moveDown ();
      } else if (Input.GetButtonDown ("3Left")) {
        move3.moveLeft ();
      } else if (Input.GetButtonDown ("3Right")) {
        move3.moveRight ();
      }

      if (Input.GetButtonDown ("4Up")) {
        move4.moveUp ();
      } else if (Input.GetButtonDown ("4Down")) {
        move4.moveDown ();
      } else if (Input.GetButtonDown ("4Left")) {
        move4.moveLeft ();
      } else if (Input.GetButtonDown ("4Right")) {
        move4.moveRight ();
      }
    }
  }
}
