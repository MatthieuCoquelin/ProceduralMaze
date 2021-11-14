using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using ExtensionMethods;

public class LevelManager : MonoBehaviour
{
    private MazeGenerator m_aMazeingLab;
    MazeGenerator.e_flags[,] m_grid;
    private int m_HEIGHT;
    private int m_WIDTH;
    private List<Vector3> m_deadEndsPosition = new List<Vector3>();
    private List<GameObject> m_deadEnds = new List<GameObject>();

    [SerializeField]
    private GameObject m_wall;

    [SerializeField]
    private GameObject m_ground;

    [SerializeField]
    private GameObject m_gameObjectParent;

    [SerializeField]
    private GameObject m_gameObjectInACorner;

    [SerializeField]
    private NavMeshAgent m_navMeshAgent;

    [SerializeField]
    private GameObject m_end;

    // Start is called before the first frame update
    void Start()
    {
        m_aMazeingLab = new MazeGenerator();
        m_grid = m_aMazeingLab.GetGrid();
        m_HEIGHT = m_aMazeingLab.GetHeight();
        m_WIDTH = m_aMazeingLab.GetWidth();

        InstantiateWalls();
        InstanciateGround();
        InstantiateGameObjectInACorner();
        StartCoroutine(FindFarEnd());
    }

    void InstantiateWalls()
    {
        for (int i = 0; i < m_HEIGHT; i++)
        {
            for (int j = 0; j < m_WIDTH; j++)
            {
                //Positon and rotation of the wall oriented East
                if (!m_grid[j, i].HasFlag(MazeGenerator.e_flags.E))
                {
                    GameObject instance = Instantiate(m_wall);

                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }

                //Positon and rotation of the wall oriented Weast
                if (!m_grid[j, i].HasFlag(MazeGenerator.e_flags.W))
                {
                    GameObject instance = Instantiate(m_wall);

                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, -2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }

                //Positon and rotation of the wall oriented North
                if (!m_grid[j, i].HasFlag(MazeGenerator.e_flags.N))
                {
                    GameObject instance = Instantiate(m_wall);
                    
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(-2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }

                //Positon and rotation of the wall oriented South
                if (!m_grid[j, i].HasFlag(MazeGenerator.e_flags.S))
                {
                    GameObject instance = Instantiate(m_wall);
                    
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }
            }
        }
    }

    void InstanciateGround()
    {
        GameObject instance = Instantiate(m_ground);
        NavMeshSurface navMeshSurfaceParent;

        //Modification of the scale and position of the wall
        instance.transform.localScale = new Vector3(4 * m_HEIGHT, instance.transform.localScale.y, 4 * m_WIDTH);
        instance.transform.Translate(new Vector3(((4* m_HEIGHT) /2) - 2, -0.5f, ((4* m_WIDTH) /2) - 2));

        //Give child groud for GOParent gameObect in the scene
        instance.transform.parent = m_gameObjectParent.transform;
        navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
        navMeshSurfaceParent.BuildNavMesh();

    }

    //instantiate Asteroid in each deadend exept the player position (0, 0, 0)
    void InstantiateGameObjectInACorner()
    {
        int i = 0;
        for (int x = 0; x < m_HEIGHT; x++)
        {
            for (int y = 0; y < m_WIDTH; y++)
            {
                //check each deadend orientation
                if ((!m_grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.E)) ||
                    (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.S)) ||
                    (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.W)) ||
                    (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!m_grid[y, x].HasFlag(MazeGenerator.e_flags.N)))
                {
                    if(x != 0 || y != 0)
                    {
                        GameObject instance = Instantiate(m_gameObjectInACorner);
                        Vector3 position = new Vector3(x * 4, 0.0f, y * 4);

                        instance.transform.Translate(position);
                        m_deadEndsPosition.Add(position);
                        m_deadEnds.Add(instance);
                        
                        i++;                    
                    }
                }
            }
        }
        
    }

    //Delete Asteroid at the farest deadend
    void DeleteGameObjectAtTheEnd(Vector3 farEnd)
    {
        for(int i = 0; i < m_deadEnds.Count; i++)
        {
            if (m_deadEnds[i].transform.position == farEnd)
                Destroy(m_deadEnds[i]);
        }
    }

    //Find the farest distance of phe player by using navMeshAgent on it
    IEnumerator FindFarEnd()
    {
        float distance = 0.0f;
        float farEndDistance = 0.0f;
        Vector3 farEnd = new Vector3(0.0f, 0.0f, 0.0f);
        
        foreach (Vector3 deadEnd in m_deadEndsPosition)
        {
            m_navMeshAgent.SetDestination(deadEnd);
            while (m_navMeshAgent.pathPending != false)
                yield return null;
            distance = m_navMeshAgent.GetPathRemainingDistance();
            if (distance > farEndDistance)
            {
                farEndDistance = distance;
                farEnd = deadEnd;
            }
        }
        DeleteGameObjectAtTheEnd(farEnd);
        m_end.transform.position = farEnd;
    }
}
