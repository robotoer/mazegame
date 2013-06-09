using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GoldBlastGames
{
  public class Game : MonoBehaviour
  {
    // Maze and Chunk sizes.
    public static float wallWidth = 0.10f;
    public static float tileWidth = 1.0f;
    public static float tileHeight = 1.0f;
    public static int mazeWidth = 31;
    public static int mazeHeight = 31;
    public static int chunkWidth = 3;
    public static int chunkHeight = 3;
    public static int numChunks = 5;
    
    // Maze and MazeObject.
    private Maze mMaze;
    public Maze maze {
      get { return mMaze; }
    }

    // Keep track of the wall GameObjects so that they can be updated.
    Dictionary<Edge, GameObject> mWalls = new Dictionary<Edge, GameObject>();
    
    // Chunks deck for wizard.
    Maze[] chunksDeck;

    // Walls object.
    public GameObject Wall;
    
    // MazeRunners.
    public Transform MazeRunner;
    public Transform[] mazeRunners = new Transform[4];
    public MazeRunnerMove[] mazeRunnerMovers = new MazeRunnerMove[4];

    // ------------- Maze Rendering --------------
    private void initializeMazeRender() {
      // TODO: Validate that the object this is attached to is a 2D rectangle.

      Tile[,] tiles = mMaze.Tiles;
      int yDim = tiles.GetLength(0);
      int xDim = tiles.GetLength(1);

      // Add the left most wall in the top row.
      GameObject upperLeftWall = LeftWall(transform.position, 0, 0);
      upperLeftWall.renderer.enabled = tiles[0, 0].Left.IsWall;
      mWalls[tiles[0, 0].Left] = upperLeftWall;

      // Add the top most row of walls.
      for (int x = 0; x < xDim; x++) {
        GameObject topWall = TopWall(transform.position, x, 0);
        topWall.renderer.enabled = tiles[0, x].Up.IsWall;
        mWalls[tiles[0, x].Up] = topWall;
      }

      // Add rows.
      for (int y = 0; y < yDim; y++) {
        // Add left most wall in the row.
        GameObject leftWall = LeftWall(transform.position, 0, y);
        leftWall.renderer.enabled = tiles[y, 0].Left.IsWall;
        mWalls[tiles[y, 0].Left] = leftWall;

        // Add cells (within row).
        for (int x = 0; x < xDim; x++) {
          GameObject rightWall = RightWall(transform.position, x, y);
          rightWall.renderer.enabled = tiles[y, x].Right.IsWall;
          mWalls[tiles[y, x].Right] = rightWall;

          GameObject bottomWall = BottomWall(transform.position, x, y);
          bottomWall.renderer.enabled = tiles[y, x].Down.IsWall;
          mWalls[tiles[y, x].Down] = bottomWall;

          Tile(transform.position, x, y);
        }
      }

      // For debugging purposes, print a string version of the maze.
      printThings();
    }

    private static Vector3 mazeRenderOffset = new Vector3(0.5f, -0.5f, 0.0f);
    private Vector3 projectGameCoords(Vector3 center, Vector2 coord) {
      return new Vector3(
          center.x + (coord.x - (mazeWidth / 2.0f)) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - coord.y) * tileHeight,
          center.z);
    }

    private GameObject Tile(Vector3 center, int x, int y) {
      Vector3 physicalCoord = projectGameCoords(transform.position, new Vector2(x, y));

      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.localScale = new Vector3(tileWidth, tileHeight, 0.5f);
      cube.transform.position = physicalCoord + mazeRenderOffset;
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f) + 0.5f) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y - 0.5f) * tileHeight,
          center.z + 0.5f);
      cube.renderer.material.color = new Color(0.0f, 0.0f, 1.0f);

      return cube;
    }

    private GameObject TopWall(Vector3 center, int x, int y) {
      Vector3 physicalCoord = projectGameCoords(transform.position, new Vector2(x, y));
      Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);

      GameObject wall = (GameObject) Instantiate(Wall);
      WallBehaviour wallBehaviour = (WallBehaviour) wall.GetComponent("WallBehaviour");
      wallBehaviour.setPosition(
          tileWidth,
          wallWidth,
          physicalCoord,
          mazeRenderOffset,
          offset);

      wallBehaviour.initializePosition(x, y, Maze.Direction.Up);

      return wall;
    }

    private GameObject BottomWall(Vector3 center, int x, int y) {
      Vector3 physicalCoord = projectGameCoords(transform.position, new Vector2(x, y));
      Vector3 offset = new Vector3(0.0f, -0.5f, 0.0f);

      GameObject wall = (GameObject) Instantiate(Wall);
      WallBehaviour wallBehaviour = (WallBehaviour) wall.GetComponent("WallBehaviour");
      wallBehaviour.setPosition(
          tileWidth,
          wallWidth,
          physicalCoord,
          mazeRenderOffset,
          offset);

      wallBehaviour.initializePosition(x, y, Maze.Direction.Down);

      return wall;
    }

    private GameObject LeftWall(Vector3 center, int x, int y) {
      Vector3 physicalCoord = projectGameCoords(transform.position, new Vector2(x, y));
      Vector3 offset = new Vector3(-0.5f, 0.0f, 0.0f);

      GameObject wall = (GameObject) Instantiate(Wall);
      WallBehaviour wallBehaviour = (WallBehaviour) wall.GetComponent("WallBehaviour");
      wallBehaviour.setPosition(
          wallWidth,
          tileHeight,
          physicalCoord,
          mazeRenderOffset,
          offset);

      wallBehaviour.initializePosition(x, y, Maze.Direction.Left);

      return wall;
    }

    private GameObject RightWall(Vector3 center, int x, int y) {
      Vector3 physicalCoord = projectGameCoords(transform.position, new Vector2(x, y));
      Vector3 offset = new Vector3(0.5f, 0.0f, 0.0f);

      GameObject wall = (GameObject) Instantiate(Wall);
      WallBehaviour wallBehaviour = (WallBehaviour) wall.GetComponent("WallBehaviour");
      wallBehaviour.setPosition(
          wallWidth,
          tileHeight,
          physicalCoord,
          mazeRenderOffset,
          offset);

      wallBehaviour.initializePosition(x, y, Maze.Direction.Right);

      return wall;
    }

    private void updateMaze() {
      foreach (KeyValuePair<Edge, GameObject> entry in mWalls) {
        entry.Value.renderer.enabled = entry.Key.IsWall;
      }
    }

    private void printThings() {
      int xDim = mMaze.Tiles.GetLength(1);
      int yDim = mMaze.Tiles.GetLength(0);

      bool[,] dispMatrix = new bool[3 * xDim, 3 * yDim];

      for (int y = 0; y < yDim; y++) {
        for (int x = 0; x < xDim; x++) {
          Tile tile = mMaze.Tiles[y, x];

          // Always draw corners.
          dispMatrix[3 * y, 3 * x] = true;
          dispMatrix[3 * y + 2, 3 * x] = true;
          dispMatrix[3 * y, 3 * x + 2] = true;
          dispMatrix[3 * y + 2, 3 * x + 2] = true;

          // up
          if (tile.Up.IsWall) {
            dispMatrix[3 * y, 3 * x + 1] = true;
          } else {
            dispMatrix[3 * y, 3 * x + 1] = false;
          }

          // down
          if (tile.Down.IsWall) {
            dispMatrix[3 * y + 2, 3 * x + 1] = true;
          } else {
            dispMatrix[3 * y + 2, 3 * x + 1] = false;
          }

          // left
          if (tile.Left.IsWall) {
            dispMatrix[3 * y + 1, 3 * x] = true;
          } else {
            dispMatrix[3 * y + 1, 3 * x] = false;
          }

          // right
          if (tile.Right.IsWall) {
            dispMatrix[3 * y + 1, 3 * x + 2] = true;
          } else {
            dispMatrix[3 * y + 1, 3 * x + 2] = false;
          }
        }
      }

      string mazestr = "\n";
      for (int y = 0; y < yDim * 3; y++) {
        for (int x = 0; x < xDim * 3; x++) {
          if (dispMatrix[x, y]) {
            mazestr += "*";
          } else {
            mazestr += " ";
          }
        }
        mazestr += "\n";
      }

      print (mazestr);
    }

    // ------ Initialization ------
    void Start ()
    {
      // Create maze.
      mMaze = Maze.generateMaze(mazeWidth, mazeHeight, this);
      initializeMazeRender();

      // Create chunks for wizard.
      chunksDeck = new Maze[numChunks];
      for (int i = 0; i < numChunks; i ++) {
        chunksDeck[i] = Maze.generateMaze(chunkWidth, chunkHeight, this);
      }
      
      // TODO: Render chunks.
      
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
      updateMaze();

      MazeRunnerMove move1 = mazeRunnerMovers [0];
      MazeRunnerMove move2 = mazeRunnerMovers [1];
      MazeRunnerMove move3 = mazeRunnerMovers [2];
      MazeRunnerMove move4 = mazeRunnerMovers [3];

      if (Input.GetButtonDown ("1Up")) {
        move1.moveUp ();
      } else if (Input.GetButtonDown ("1Down")) {
        move1.moveDown ();
      } else if (Input.GetButtonDown ("1Left")) {
        move1.moveLeft();
      } else if (Input.GetButtonDown ("1Right")) {
        move1.moveRight ();
      }
      
      if (Input.GetButtonDown ("2Up")) {
        move2.moveUp ();
      } else if (Input.GetButtonDown ("2Down")) {
        move2.moveDown ();
      } else if (Input.GetButtonDown ("2Left")) {
        move2.moveLeft();
      } else if (Input.GetButtonDown ("2Right")) {
        move2.moveRight ();
      }

      /*
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
      }*/
    }

    // Updates a maze wall (toggles it on or off).
    public void updateMazeWall(int tileX, int tileY, Maze.Direction direction) {
      mMaze.updateWall(tileY, tileX, direction);
      updateMaze();
    }
  }
}
