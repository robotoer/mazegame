/*using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GoldBlastGames {
  /// <summary>
  /// Maze object that provides rendering support for Maze.
  ///  - x axis is horizontal
  ///  - y axis is vertical
  /// This also makes the assumption that creating a Prefab instance will also cause
  /// Unity to know about it and render it properly.
  /// </summary>
  public class MazeObject : MonoBehaviour {
    static float wallWidth = 0.05f;
    static float tileWidth = 1.0f;
    static float tileHeight = 1.0f;
    static int mazeWidth = 10;
    static int mazeHeight = 10;

    Maze mMaze = Maze.generateMaze(mazeHeight, mazeWidth);
    Dictionary<Edge, GameObject> mWalls = new Dictionary<Edge, GameObject>();

    // Use this for initialization
    void Start () {
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

      printThings();
    }

    // Update is called once per frame
    void Update () {
    }

    private GameObject Tile(Vector3 center, int x, int y) {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.localScale = new Vector3(tileWidth, tileHeight, 0.5f);
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f) + 0.5f) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y - 0.5f) * tileHeight,
          center.z);
      cube.renderer.material.color = new Color(0.0f, 0.0f, 1.0f);

      print ("center: " + center + ", x: " + x + ", y: " + y + " ===> " + cube.transform.position);

      return cube;
    }

    private GameObject TopWall(Vector3 center, int x, int y) {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      //cube.AddComponent<Rigidbody>();
      cube.transform.localScale = new Vector3(tileWidth, wallWidth, 1.0f);
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f) + 0.5f) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y) * tileHeight,
          center.z);
      cube.renderer.material.color = new Color(1.0f, 0.0f, 0.0f);

      return cube;
    }

    private GameObject BottomWall(Vector3 center, int x, int y) {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      //cube.AddComponent<Rigidbody>();
      cube.transform.localScale = new Vector3(tileWidth, wallWidth, 1.0f);
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f) + 0.5f) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y - 1.0f) * tileHeight,
          center.z);
      cube.renderer.material.color = new Color(1.0f, 0.0f, 0.0f);

      return cube;
    }

    //0.0f + (0 - 2 - 0.5) * 1.0f / 2.0f =
    private GameObject LeftWall(Vector3 center, int x, int y) {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      //cube.AddComponent<Rigidbody>();
      cube.transform.localScale = new Vector3(wallWidth, tileHeight, 1.0f);
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f)) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y - 0.5f) * tileHeight,
          center.z);
      cube.renderer.material.color = new Color(1.0f, 0.0f, 0.0f);

      return cube;
    }

    private GameObject RightWall(Vector3 center, int x, int y) {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      //cube.AddComponent<Rigidbody>();
      cube.transform.localScale = new Vector3(wallWidth, tileHeight, 1.0f);
      cube.transform.position = new Vector3(
          center.x + (x - (mazeWidth / 2.0f) + 1.0f) * tileWidth,
          center.y + ((mazeHeight / 2.0f) - y - 0.5f) * tileHeight,
          center.z);
      cube.renderer.material.color = new Color(1.0f, 0.0f, 0.0f);

      return cube;
    }

    private void printThings() {
      int xDim = mMaze.Tiles.GetLength(0);
      int yDim = mMaze.Tiles.GetLength(1);

      bool[,] dispMatrix = new bool[3 * xDim, 3 * yDim];

      for (int y = 0; y < yDim; y++) {
        for (int x = 0; x < xDim; x++) {
          Tile tile = mMaze.Tiles[x, y];

          // Always draw corners.
          dispMatrix[3 * x, 3 * y] = true;
          dispMatrix[3 * x + 2, 3 * y] = true;
          dispMatrix[3 * x, 3 * y + 2] = true;
          dispMatrix[3 * x + 2, 3 * y + 2] = true;

          // up
          if (tile.Up.IsWall) {
            dispMatrix[3 * x + 1, 3 * y] = true;
          } else {
            dispMatrix[3 * x + 1, 3 * y] = false;
          }

          // down
          if (tile.Down.IsWall) {
            dispMatrix[3 * x + 1, 3 * y + 2] = true;
          } else {
            dispMatrix[3 * x + 1, 3 * y + 2] = false;
          }

          // left
          if (tile.Left.IsWall) {
            dispMatrix[3 * x, 3 * y + 1] = true;
          } else {
            dispMatrix[3 * x, 3 * y + 1] = false;
          }

          // right
          if (tile.Right.IsWall) {
            dispMatrix[3 * x + 2, 3 * y + 1] = true;
          } else {
            dispMatrix[3 * x + 2, 3 * y + 1] = false;
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
  }
}
*/