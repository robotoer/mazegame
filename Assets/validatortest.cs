using UnityEngine;
using System.Collections;

public class validatortest : MonoBehaviour {

  // Use this for initialization
  void Start () {
    Maze maze = Maze.generateMaze(2, 1);
    maze.initialGroups();
    print(maze.find(maze.mTiles[0,0], maze.mTiles[0,1]));
  }

  // Update is called once per frame
  void Update () {
    
  }
}