using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class MazeGenerator
{
    [Flags]
    public enum e_flags
    {
        N = 1, S = 2, E = 4, W = 8
    };

    //e_flags myVar;

    //void myMethod()
    //{
    //    myVar = e_flags.E | e_flags.N;
    //    //myVar.HasFlag()
    //    print(myVar);
    //}

    int WIDTH = 10, HEIGHT = 10;

    int[] DX = new int[9];
    int[] DY = new int[9];
    e_flags[] OPPOSITE = new e_flags[9];

    e_flags[,] grid = new e_flags[10, 10];

    public MazeGenerator()
    {
        OPPOSITE[1] = e_flags.S;
        OPPOSITE[4] = e_flags.W;
        OPPOSITE[2] = e_flags.N;
        OPPOSITE[8] = e_flags.E;

        DX[1] = 0;
        DX[4] = 1;
        DX[2] = 0;
        DX[8] = -1;

        DY[1] = -1;
        DY[4] = 0;
        DY[2] = 1;
        DY[8] = 0;

        //myMethod();
        carve_passage(0, 0);
    }

    void writeOnFile()
    {
        //Open the File
        StreamWriter sw = new StreamWriter("D:\\Test3.txt", true, Encoding.ASCII);

        int x, y;
        sw.Write(" ");
        for (x = 0; x < (WIDTH   ); x++)
        {
            //sw.Write("_"); 
            sw.Write((grid[x , 0].HasFlag(e_flags.N)) ? "  " : "__");
        }
        sw.Write("\n");

        for (y = 0; y < HEIGHT; y++)
        {
            //sw.Write("|");
            sw.Write(grid[0, y].HasFlag(e_flags.W) ? " " : "|");
            for (x = 0; x < WIDTH; x++)
            {
                sw.Write(grid[x, y].HasFlag(e_flags.S) ? " " : "_");
                if ((grid[x, y] & e_flags.E) != 0)
                {
                    sw.Write(((grid[x, y] | grid[x, y]).HasFlag(e_flags.S)) ? " " : "_");
                }
                else
                {
                    //sw.Write("|");
                    sw.Write(((grid[x, y].HasFlag(e_flags.E))) ? " " : "|");
                }
            }
            sw.Write("\n");
        }
            //close the file
            sw.Close();
        
    }

    void carve_passage (int cx, int cy)
    {
        int dx, dy, nx, ny;
        e_flags[] directions = { e_flags.N, e_flags.E, e_flags.S, e_flags.W };
        int i;
        for (i = 0; i < (4 - 1); i++)
        {
            int r = UnityEngine.Random.Range(i, 4 - i);
            e_flags temp = directions[i];
            directions[i] = directions[r];
            directions[r] = temp;
        }


        for (i = 0; i < 4; i++)
        {
            dx = DX[(int)directions[i]];
            dy = DY[(int)directions[i]];
           
            // check if the cell is valid
            nx = cx + dx;
            ny = cy + dy;
            // check if we are on valid grid
            if (((nx < WIDTH) & (nx >= 0)) & ((ny < HEIGHT) & (ny >= 0)))
            {
                if (grid[nx,ny] == 0)
                {
                    grid[cx, cy] = grid[cx, cy] | directions[i];
                    grid[nx, ny] = grid[nx, ny] | OPPOSITE[(int)directions[i]];
                    carve_passage(nx, ny);
                }
            }

        }
        writeOnFile();
    }

    public e_flags[,] GetGrid()
    {
        return grid;
    }
}
