using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePart : MonoBehaviour {

    public Vector3 partOrigin;
    public Vector3 partEnd;
    public bool displayWire = false;
    public bool isStuck = false;
    public GameObject objToIgnore;
    public LightBulbe LightPluggedTo;
    public bool looksToRight;

    public float partLength
    {
        get
        {
            return Vector2.Distance(partEnd, partOrigin);
        }
    }

    private static Material lineMaterial;

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
        if (displayWire)
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
            GL.Color(Color.yellow);
            if(LightPluggedTo != null)
            {
                GL.Vertex3(LightPluggedTo.transform.position.x, LightPluggedTo.transform.position.y, LightPluggedTo.transform.position.z);
            }
            else
            {
                GL.Vertex3(partEnd.x, partEnd.y, partEnd.z);
            }
            GL.Vertex3(partOrigin.x, partOrigin.y, partOrigin.z);
            GL.End();
            GL.PopMatrix();
        }
    }
}
