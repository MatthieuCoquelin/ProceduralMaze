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
    private List<Vector3> deadEnds = new List<Vector3>();

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

    public GameObject myGameObject;

    // Start is called before the first frame update
    void Start()
    {
        aMazeingLab = new MazeGenerator();
        grid = aMazeingLab.GetGrid();
        InstantiateWalls();
        InstanciateGround();
        InstantiateGameObjectInACorner();
        StartCoroutine(FindFarEnd());
    }

    void InstantiateWalls()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.E))
                {
                    GameObject instance = Instantiate(Wall);


                    instance.name = $"wall : {i} {j} E";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);

                    //NavMeshSurface navMeshSurfaceParent;

                    //instance.transform.parent = GOParent.transform;
                    //navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
                    //navMeshSurfaceParent.BuildNavMesh();
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.W))
                {
                    GameObject instance = Instantiate(Wall);


                    instance.name = $"wall : {i} {j} W";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, -2.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);

                    //NavMeshSurface navMeshSurfaceParent;

                    //instance.transform.parent = GOParent.transform;
                    //navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
                    //navMeshSurfaceParent.BuildNavMesh();
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.N))
                {
                    GameObject instance = Instantiate(Wall);
                    

                    instance.name = $"wall : {i} {j} N";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(-2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);

                    //NavMeshSurface navMeshSurfaceParent;

                    //instance.transform.parent = GOParent.transform;
                    //navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
                    //navMeshSurfaceParent.BuildNavMesh();
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.S))
                {
                    GameObject instance = Instantiate(Wall);
                    

                    instance.name = $"wall : {i} {j} S";
                    instance.transform.Translate(new Vector3(i*4, 0.0f, j*4));
                    instance.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);

                    //NavMeshSurface navMeshSurfaceParent;

                    //instance.transform.parent = GOParent.transform;
                    //navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
                    //navMeshSurfaceParent.BuildNavMesh();
                }
            }
        }
    }

    void InstanciateGround()
    {
        GameObject instance = Instantiate(Ground);
        NavMeshSurface navMeshSurfaceParent;

        instance.transform.Translate(new Vector3(18f, -0.5f, 18f));

        instance.transform.parent = GOParent.transform;
        navMeshSurfaceParent = instance.GetComponentInParent<NavMeshSurface>();
        navMeshSurfaceParent.BuildNavMesh();

    }

    void InstantiateGameObjectInACorner()
    {
        int i = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if ((!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.E)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) ||
                    (!grid[y, x].HasFlag(MazeGenerator.e_flags.S)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.W)) && (!grid[y, x].HasFlag(MazeGenerator.e_flags.N)))
                {
                    if(x != 0 || y != 0)
                    {
                        GameObject instance = Instantiate(GameObjectInACorner);
                        instance.transform.Translate(new Vector3(x*4, -0.5f, y*4));
                        deadEnds.Add(new Vector3(x * 4, -0.5f, y * 4));
                        
                        i++;                    
                    }
                }
            }
        }
        
    }

    //float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    //{
    //    if (navMeshAgent.pathPending || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || navMeshAgent.path.corners.Length == 0)
    //        return 1.0f;
    //    float distance = 0.0f;
    //    for (int i = 0; i < navMeshAgent.path.corners.Length - 1; i++)
    //    {
    //        distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
    //    }
    //    return distance;
    //}

    IEnumerator FindFarEnd()
    {
        float distance = 0.0f;
        float farEndDistance = 0.0f;
        Vector3 farEnd = new Vector3(0.0f, 0.0f, 0.0f);

        //https://answers.unity.com/questions/732181/nav-remaining-distance-wrong.html
        
        foreach (Vector3 deadEnd in deadEnds)
        {
            //NavMesh.CalculatePath(new Vector3(0.0f, -0.5f, 0.0f), deadEnd, myNavMeshAgent.areaMask, myNavMeshAgent.path);
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
        GameObject instance = Instantiate(myGameObject);
        instance.transform.Translate(farEnd);
    }


    void Update()
    {
        
    }
}
