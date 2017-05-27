﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    public Transform Avatar;
    public List<Transform> Corners;

    static Material lineMaterial;

    RaycastHit2D hit;

    Vector2 rayDirection;

    void Update()
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(Avatar.transform.position.x, Avatar.transform.position.y), Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Avatar.transform.position.x, Avatar.transform.position.y)));
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(Avatar.transform.position.x, Avatar.transform.position.y), Color.red);
        if (hit.collider != null && hit.collider.name != null)
        {
            Debug.Log(hit.collider.name);
        }
        /*Ray ray = new Ray(transform.position, Avatar.transform.position);
        RaycastHit hit3D;
        if (GetComponent<Collider>().Raycast(ray, out hit3D,100f))
        {
            Debug.Log(hit3D.collider.name);
        }*/
    }
    
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }



    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        // GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        //Draw X axis
        GL.Color(Color.black);
        GL.Vertex3(Avatar.transform.position.x, Avatar.transform.position.y, Avatar.transform.position.z);
        GL.Vertex3(transform.position.x, transform.position.y, transform.position.z);
        GL.End();
        GL.PopMatrix();
    }
}