using UnityEngine;
using System.Collections;

namespace GoldBlastGames {
  public class validatortest : MonoBehaviour {
    // Use this for initialization
    void Start () {
      Maze maze = Maze.generateMaze(2, 1);
      maze.initialGroups();
      print(maze.find(maze.mTiles[0,0], maze.mTiles[0,1]));
    }
  }
}
