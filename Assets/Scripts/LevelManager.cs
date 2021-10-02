using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private MazeGenerator aMazeingLab;
    MazeGenerator.e_flags[,] grid;

    [SerializeField]
    private GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        aMazeingLab = new MazeGenerator();
        grid = aMazeingLab.GetGrid();
        InstantiateLab();
    }

    void InstantiateLab()
    {
        //for(int i = 0; i < 15; i++)
        //{
        //    for (int j = 0; j < 15; j++)
        //    {
        //        if(grid[j, i].HasFlag(MazeGenerator.e_flags.E))
        //        {
        //            GameObject instance = Instantiate(Wall);
        //            instance.transform.Translate(new Vector3(i, 0.0f, j));
        //            instance.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
        //            instance.transform.Translate(new Vector3(0.5f, 0.0f, 0));
        //        }
        //        if (grid[j, i].HasFlag(MazeGenerator.e_flags.N))
        //        {
        //            GameObject instance = Instantiate(Wall);
        //            instance.transform.Translate(new Vector3(i, 0.0f, j));
        //            instance.transform.Translate(new Vector3(-0.5f, 0.0f, 0.0f));
        //        }
        //        if (grid[j, i].HasFlag(MazeGenerator.e_flags.S))
        //        {
        //            GameObject instance = Instantiate(Wall);
        //            instance.transform.Translate(new Vector3(i, 0.0f, j - 1));
        //            instance.transform.Translate(new Vector3(-0.5f, 0.0f, 0.0f));
        //        }
        //        if(grid[j, i].HasFlag(MazeGenerator.e_flags.W))
        //        {
        //            GameObject instance = Instantiate(Wall);
        //            instance.transform.Translate(new Vector3(i-1, 0.0f, j));
        //            instance.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
        //            instance.transform.Translate(new Vector3(0.5f, 0.0f, 0));
        //        }
        //    }
        //}
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.E))
                {
                    GameObject instance = Instantiate(Wall);
                    instance.name = $"wall : {i} {j} E";
                    instance.transform.Translate(new Vector3(i, 0.0f, j));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, 0.5f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.W))
                {
                    GameObject instance = Instantiate(Wall);
                    instance.name = $"wall : {i} {j} W";
                    instance.transform.Translate(new Vector3(i, 0.0f, j));
                    instance.transform.Translate(new Vector3(0.0f, 0.0f, -0.5f));
                    instance.transform.Rotate(Vector3.up, -90f);
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.N))
                {
                    GameObject instance = Instantiate(Wall);
                    instance.name = $"wall : {i} {j} N";
                    instance.transform.Translate(new Vector3(i, 0.0f, j));
                    instance.transform.Translate(new Vector3(-0.5f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }
                if (!grid[j, i].HasFlag(MazeGenerator.e_flags.S))
                {
                    GameObject instance = Instantiate(Wall);
                    instance.name = $"wall : {i} {j} S";
                    instance.transform.Translate(new Vector3(i, 0.0f, j));
                    instance.transform.Translate(new Vector3(0.5f, 0.0f, 0.0f));
                    instance.transform.Rotate(Vector3.up, -90f);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
