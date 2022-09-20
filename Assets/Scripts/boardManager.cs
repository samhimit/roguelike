using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class boardManager : MonoBehaviour
{
    public int col = 10;
    public int row = 10;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemy;
    private List<Vector3> grid = new List<Vector3>();
    private Transform boardHolder;
    // Start is called before the first frame update
    private void instiateGrid()
    {
        for(int i = 0; i < col-1; i++)
        {
            for(int j = 0; j<row-1; j++)
            {
                grid.Add(new Vector3(i+.5f,j+.5f,0f));
            }
        }
    }

    public void setUpBoard(int level)
    {
        boardHolder = new GameObject ("Board").transform;
        instiateGrid();
        for(int y = -1; y < row; y++)
            for(int x = -1; x < col; x++){
                GameObject toInstantiate = floorTiles[Random.Range(0,floorTiles.Length)];
                if(x == -1 || x == col-1 || y == -1 || y == row-1)
                    toInstantiate = wallTiles[Random.Range(0,wallTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x+0.5f,y+0.5f+0f),Quaternion.identity) as GameObject;
                instance.transform.SetParent (boardHolder);
            }

        for(int i = 0; i <= level; i++)
        {
            int index = Random.Range(0,grid.Count);
            RaycastHit2D hit = Physics2D.Raycast(grid[index], Vector2.zero);
            if(hit.collider !=null && hit.collider.tag=="Player"){
                i--;
            }
            else{
                Instantiate(enemy[Random.Range(0,enemy.Length)], grid[index], Quaternion.identity);
            }
            grid.RemoveAt(index);
        }

    }
}
