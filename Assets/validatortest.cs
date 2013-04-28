using UnityEngine;
using System.Collections;

namespace GoldBlastGames {

  public class validatortest : MonoBehaviour {

    // Use this for initialization
    void Start () {
      Maze maze = Maze.generateMaze(3, 3);
      maze.initialGroups();
      print("there is a path in maze1: " + maze.find(maze.Tiles[0,0], maze.Tiles[2,2]));
      Maze maze2 = new Maze(maze);
      maze2.deepCopyTiles(maze);
      maze2.initialGroups();
      print("there is a path in maze2: " + maze2.find(maze2.Tiles[0,0], maze2.Tiles[2,2]));
    }
  }
}
