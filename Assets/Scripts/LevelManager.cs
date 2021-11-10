using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using ExtensionMethods;

public class LevelManager : MonoBehaviour
{
    private MazeGenerator aMazeingLab;
    MazeGenerator.e_flags[,] grid;
    private int HEIGHT;
    private int WIDTH;
    private List<Vector3> deadEndsPosition = new List<Vector3>();
    private List<GameObject> deadEnds = new List<GameObject>();

    [SerializeField]
    private GameObject Wall;

    [SerializeField]
    private GameObject Ground;

    [SerializeField]
    private GameObject GOParent;

    [SerializeField]
    private GameObject GameObjectInACorner;

    [SerializeField]
    private NavMeshAgent myNavMeshAgent;

    [SerializeField]
    private GameObject m_End;

    // Start is called before the first frame update
    void Start()
    {
        aMazeingLab = new MazeGenerator();
        grid = aMazeingLab.GetGrid();
        HEIGHT = aMazeingLab.GetHeight();
        WIDTH = aMazeingLab.GetWidth();

        InstantiateWalls();
        InstanciateGround();
        InstantiateGameObjectInACorner();
        StartCoroutine(FindFarEnd());
    }

    void InstantiateWalls()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.E))
                {
                    GameObject instance = Instantiate(Wall);


                    instance.name = $"wall : {i} {j} E";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.W))
                {
                    GameObject instance = Instantiate(Wall);


                    instance.name = $"wall : {i} {j} W";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, -2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.N))
                {
                    GameObject instance = Instantiate(Wall);
                    

                    instance.name = $"wall : {i} {j} N";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(-2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.S))
                {
                    GameObject instance = Instantiate(Wall);
                    

                    instance.name = $"wall : {i} {j} S";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }
            }
        }
    }

    void InstanciateGround()
    {
        GameObject instance = Instantiate(Ground);
        NavMeshSurface navMeshSurfaceParent;

        instance.transform.localScale = new Vector3(4 * HEIGHT, instance.transform.localScale.y, 4 * WIDTH);
        instance.transform.Translate(new Vector3(((4*HEIGHT)/2) - 2, -0.5f, ((4*WIDTH)/2) - 2));

        instance.transform.parent = GOParent.transform;
        navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
        navMeshSurfaceParent.BuildNavMesh();

    }

    void InstantiateGameObjectInACorner()
    {
        int i = 0;
        for (int x = 0; x < HEIGHT; x++)
        {
            for (int y = 0; y < WIDTH; y++)
            {
                if ((!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)))
                {
                    if(x != 0 || y != 0)
                    {
                        GameObject instance = Instantiate(GameObjectInACorner);
                        Vector3 position = new Vector3(x * 4, 0.0f, y * 4);

                        instance.transform.Translate(position);
                        deadEndsPosition.Add(position);
                        deadEnds.Add(instance);
                        
                        i++;                    
                    }
                }
            }
        }
        
    }

    void DeleteGameObjectAtTheEnd(Vector3 farEnd)
    {
        for(int i = 0; i < deadEnds.Count; i++)
        {
            if (deadEnds[i].transform.position == farEnd)
                Destroy(deadEnds[i]);
        }
    }

    IEnumerator FindFarEnd()
    {
        float distance = 0.0f;
        float farEndDistance = 0.0f;
        Vector3 farEnd = new Vector3(0.0f, 0.0f, 0.0f);
        
        foreach (Vector3 deadEnd in deadEndsPosition)
        {
            myNavMeshAgent.SetDestination(deadEnd);
            while (myNavMeshAgent.pathPending != false)
                yield return null;
            distance = myNavMeshAgent.GetPathRemainingDistance();
            if (distance > farEndDistance)
            {
                farEndDistance = distance;
                farEnd = deadEnd;
            }
        }
        DeleteGameObjectAtTheEnd(farEnd);
        
        GameObject instance = Instantiate(m_End);
        instance.transform.Translate(farEnd);
    }
}
