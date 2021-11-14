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
        N = 1, S = 2, E = 4, W = 8, DeadEnd = 16
    };

    int m_WIDTH = 10, m_HEIGHT = 10;

    int[] m_DX = new int[9];
    int[] m_DY = new int[9];
    e_flags[] m_OPPOSITE = new e_flags[9];

    e_flags[,] m_grid = new e_flags[20, 20];
    
    //check the dificulty's level of the maze and modifie attributes
    private void InitializeSize()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        if( difficulty == "Easy")
        {
            m_WIDTH = 7;
            m_HEIGHT = 7;
        }
        else if (difficulty == "Normal")
        {
            m_WIDTH = 10;
            m_HEIGHT = 10;
        }
        else if (difficulty == "Difficult")
        {
            m_WIDTH = 15;
            m_HEIGHT = 15;
        }
        else if (difficulty == "Random")
        {
            m_WIDTH = UnityEngine.Random.Range(7, 20);
            m_HEIGHT = UnityEngine.Random.Range(7, 20);
        }
    }

    public MazeGenerator()
    {
        m_OPPOSITE[1] = e_flags.S;
        m_OPPOSITE[4] = e_flags.W;
        m_OPPOSITE[2] = e_flags.N;
        m_OPPOSITE[8] = e_flags.E;

        m_DX[1] = 0;
        m_DX[4] = 1;
        m_DX[2] = 0;
        m_DX[8] = -1;

        m_DY[1] = -1;
        m_DY[4] = 0;
        m_DY[2] = 1;
        m_DY[8] = 0;

        InitializeSize();
        carve_passage(0, 0);
        CheckDeadEnd();
    }

    //method test to disp the created correct(or not) maze generated
    void writeOnFile()
    {
        //Open the File
        StreamWriter sw = new StreamWriter("D:\\Test3.txt", true, Encoding.ASCII);

        int x, y;
        sw.Write(" ");
        for (x = 0; x < (m_WIDTH); x++)
        {
            sw.Write((m_grid[x , 0].HasFlag(e_flags.N)) ? "  " : "__");
        }
        sw.Write("\n");

        for (y = 0; y < m_HEIGHT; y++)
        {
            sw.Write(m_grid[0, y].HasFlag(e_flags.W) ? " " : "|");
            for (x = 0; x < m_WIDTH; x++)
            {
                sw.Write(m_grid[x, y].HasFlag(e_flags.S) ? " " : "_");
                if ((m_grid[x, y] & e_flags.E) != 0)
                {
                    sw.Write(((m_grid[x, y] | m_grid[x, y]).HasFlag(e_flags.S)) ? " " : "_");
                }
                else
                {
                    sw.Write(((m_grid[x, y].HasFlag(e_flags.E))) ? " " : "|");
                }
            }
            sw.Write("\n");
        }
            //close the file
            sw.Close();
        
    }

    //check the deadends
    void CheckDeadEnd()
    {
        for (int x = 0; x < m_HEIGHT; x++)
        {
            for (int y = 0; y < m_WIDTH; y++)
            {
                if ((m_grid[x, y].HasFlag(e_flags.W)) && (m_grid[x, y].HasFlag(e_flags.N)) && (m_grid[x, y].HasFlag(e_flags.E)) ||
                    (m_grid[x, y].HasFlag(e_flags.N)) && (m_grid[x, y].HasFlag(e_flags.E)) && (m_grid[x, y].HasFlag(e_flags.S)) ||
                    (m_grid[x, y].HasFlag(e_flags.E)) && (m_grid[x, y].HasFlag(e_flags.S)) && (m_grid[x, y].HasFlag(e_flags.W)) ||
                    (m_grid[x, y].HasFlag(e_flags.S)) && (m_grid[x, y].HasFlag(e_flags.W)) && (m_grid[x, y].HasFlag(e_flags.N)))
                {
                    m_grid[x, y].HasFlag(e_flags.DeadEnd);
                }
            }
        }
    }

    //recursive method to generate the maze
    void carve_passage (int cx, int cy)
    {
        int dx, dy, nx, ny;
        e_flags[] directions = { e_flags.N, e_flags.E, e_flags.S, e_flags.W };
        int i;
        for (i = 0; i < (4 - 1); i++)
        {
            int r = i + UnityEngine.Random.Range(0, 4 - i);
            e_flags temp = directions[i];
            directions[i] = directions[r];
            directions[r] = temp;
        }


        for (i = 0; i < 4; i++)
        {
            dx = m_DX[(int)directions[i]];
            dy = m_DY[(int)directions[i]];
           
            // check if the cell is valid
            nx = cx + dx;
            ny = cy + dy;
            // check if we are on valid grid
            if (((nx < m_WIDTH) & (nx >= 0)) & ((ny < m_HEIGHT) & (ny >= 0)))
            {
                if (m_grid[nx,ny] == 0)
                {
                    m_grid[cx, cy] = m_grid[cx, cy] | directions[i];
                    m_grid[nx, ny] = m_grid[nx, ny] | m_OPPOSITE[(int)directions[i]];
                    carve_passage(nx, ny);
                }
            }

        }
        //writeOnFile();
    }

    public e_flags[,] GetGrid()
    {
        return m_grid;
    }

    public int GetHeight()
    {
        return m_HEIGHT;
    }

    public int GetWidth()
    {
        return m_WIDTH;
    }
}
