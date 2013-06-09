using UnityEngine;
using System.Collections;

namespace GoldBlastGames {
  public class WallBehaviour : MonoBehaviour {
    Game game;
    Maze maze;

    // Coordinates of the tile this wall belongs to, and the direction
    // relative to that tile.
    int tileXCoord;
    int tileYCoord;
    Maze.Direction direction;

  	// Use this for initialization
  	void Start () {
      game = GameObject.Find("Game").GetComponent<Game>();
      maze = game.maze;
  	}

  	// Update is called once per frame
  	void Update () {
  	}

    // When wall is clicked.
    void OnMouseDown () {
      Debug.Log ("Wall clicked");
      game.updateMazeWall(tileXCoord, tileYCoord, direction);
    }

    public void setPosition (
        float width,
        float height,
        Vector3 physicalCoord,
        Vector3 mazeRenderOffset,
        Vector3 offset) {
      this.transform.localScale = new Vector3(width, height, 1.0f);
      this.transform.position = physicalCoord + mazeRenderOffset + offset;
      this.renderer.material.color = new Color(1.0f, 0.0f, 0.0f);
    }

    public void initializePosition(int x, int y, Maze.Direction direction) {
      tileXCoord = x;
      tileYCoord = y;
      this.direction = direction;
    }
  }
}