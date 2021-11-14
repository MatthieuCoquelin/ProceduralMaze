using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingSideBySide : MonoBehaviour
{
    //Personalize cube by using uv mapping. 
    
    //See more here:
    //https://answers.unity.com/questions/542787/change-texture-of-cube-sides.html
    void Start()
    {
        Mesh m_mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] m_UVs = new Vector2[m_mesh.vertices.Length];
        // Front
        m_UVs[0] = new Vector2(0.0f, 0.0f);
        m_UVs[1] = new Vector2(0.333f, 0.0f);
        m_UVs[2] = new Vector2(0.0f, 0.333f);
        m_UVs[3] = new Vector2(0.333f, 0.333f);
        // Top
        m_UVs[4] = new Vector2(0.334f, 0.333f);
        m_UVs[5] = new Vector2(0.666f, 0.333f);
        m_UVs[8] = new Vector2(0.334f, 0.0f);
        m_UVs[9] = new Vector2(0.666f, 0.0f);
        // Back
        m_UVs[6] = new Vector2(1.0f, 0.0f);
        m_UVs[7] = new Vector2(0.667f, 0.0f);
        m_UVs[10] = new Vector2(1.0f, 0.333f);
        m_UVs[11] = new Vector2(0.667f, 0.333f);
        // Bottom
        m_UVs[12] = new Vector2(0.0f, 0.334f);
        m_UVs[13] = new Vector2(0.0f, 0.666f);
        m_UVs[14] = new Vector2(0.333f, 0.666f);
        m_UVs[15] = new Vector2(0.333f, 0.334f);
        // Left
        m_UVs[16] = new Vector2(0.334f, 0.334f);
        m_UVs[17] = new Vector2(0.334f, 0.666f);
        m_UVs[18] = new Vector2(0.666f, 0.666f);
        m_UVs[19] = new Vector2(0.666f, 0.334f);
        // Right        
        m_UVs[20] = new Vector2(0.667f, 0.334f);
        m_UVs[21] = new Vector2(0.667f, 0.666f);
        m_UVs[22] = new Vector2(1.0f, 0.666f);
        m_UVs[23] = new Vector2(1.0f, 0.334f);
        m_mesh.uv = m_UVs;
    }
}
