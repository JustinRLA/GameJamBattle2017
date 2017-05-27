using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Wire : MonoBehaviour
    {

        public GameObject Avatar;
        public List<Transform> Corners;

        static Material lineMaterial;

        RaycastHit2D hit;

        Vector2 rayDirection;


        public float cordLength = 0.0F;             //Current length of cord
        public float cordMaxLength = 100.0F;        //Maximum length of cord
        public float cordStretchThreshold = 80.0F;  //Distance where stretching applies

        public Vector2 lineDirection;   // Direction of the current line
        public float lineDistance;    // Distance of a given line (!!!Convert to array for each new line)
        public int lineCount;           // Number of lines (!!!Convert for array conversion)
        public float totalLineDistance; // Total distance of all the lines

        public bool isHeld = false;     // The cable is being carried by the player
        public bool isPlugged = false;  // The cable is attached to a light

        void FixedUpdate()
        {
            if (isHeld)
            {
                lineDirection = (Avatar.transform.position - this.transform.position);                  // Set direction of line
                lineDistance = Vector2.Distance(Avatar.transform.position, this.transform.position);    // Set distance of line (!!!Convert to array for each new line)
                //totalLineDistance = sum of lineDistance array

                //Debug.Log(new Vector2(this.transform.position.x, this.transform.position.y));
                //Debug.Log(new Vector2(Avatar.transform.position.x, Avatar.transform.position.y));
                hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), lineDirection, lineDistance);
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), lineDirection, Color.blue);
                if (hit.collider != null && hit.collider.name != null)
                {
                    //Debug.Log(hit.collider.name);
                }
                //Ray ray = new Ray(transform.position, Avatar.transform.position);

                /*
                RaycastHit hit3D;
                if (GetComponent<Collider>().Raycast(ray, out hit3D,100f))
                {
                    Debug.Log(hit3D.collider.name);
                }*/

                //!!! Modify to be distance of all Raycasts from Generator to Player including ones generated between anchors
                cordLength = lineDistance;
            }

            //Stretch effects
            if (cordLength >= cordMaxLength && isHeld)
            {
                Avatar.GetComponent<PlatformerCharacter2D>().ReleaseCable();    // Cable gets cut from player
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 10f;  // Reset to standard
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = false; // Reset to standard
            }
            else if (cordLength >= cordStretchThreshold && isHeld)
            {
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 3f;   // Limit player speed
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = true;  // Activate pullback effect
            }
            else
            {
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 10f;  // Reset to standard
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = false; // Reset to standard
            }
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
            if (isHeld)
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
                GL.Vertex3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                GL.End();
                GL.PopMatrix();
            }
        }
    }
}
