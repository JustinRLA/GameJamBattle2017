using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Wire : MonoBehaviour
    {

        public GameObject Avatar;
        public List<Transform> Corners;
        RaycastHit2D hit;

        Vector2 rayDirection;


        public float cordLength = 0.0F;             //Current length of cord
        public float cordMaxLength = 100.0F;        //Maximum length of cord
        public float cordStretchThreshold = 80.0F;  //Distance where stretching applies

        public Vector2 lineDirection;   // Direction of the current line
        public float lineDistance;    // Distance of a given line (!!!Convert to array for each new line)
        public int lineCount;           // Number of lines (!!!Convert for array conversion)
        public float totalLineDistance; // Total distance of all the lines
        public Vector3 targetPosition;
        public LayerMask myLayerMask;


        public bool IsHeld {
            get
            {
                return _isHeld;
            }
            set
            {

                _isHeld = value;

                if (_wireParts == null)
                {
                    _wireParts = new List<WirePart>();
                }
                if (_isHeld == true)
                {
                    CreateWirePart();
                }
            }
        }

        public Vector3 CableOriginPos
        {
            get
            {
                if(_wireParts != null)
                {
                    return _wireParts[_wireParts.Count - 1].partOrigin;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }

        // The cable is being carried by the player
        public bool isPlugged = false;  // The cable is attached to a light

        private List<WirePart> _wireParts;
        private WirePart _currentWirePart;
        private bool _isHeld = false;
        private bool test = false;
        void FixedUpdate()
        {
            if (!IsHeld)
            {
                return;
            }
            StretchEffect();
        }

        void Update()
        {
            if (!IsHeld)
            {
                return;
            }
            HandleObstacles();
            DetangleWire();
        }



        //Functions

        private void HandleObstacles()
        {
            _currentWirePart = _wireParts[_wireParts.Count - 1];
            targetPosition = Avatar.transform.position;
            _currentWirePart.partEnd = targetPosition;
            lineDirection = (targetPosition - _currentWirePart.partOrigin);                  // Set direction of line
            lineDistance = Vector2.Distance(targetPosition, _currentWirePart.partOrigin);    // Set distance of line (!!!Convert to array for each new line)
            hit = Physics2D.Raycast(new Vector2(_currentWirePart.partOrigin.x, _currentWirePart.partOrigin.y), lineDirection, lineDistance, myLayerMask);
            //Debug.DrawRay(new Vector2(_currentWirePart.partOrigin.x, _currentWirePart.partOrigin.y), lineDirection, Color.blue);

            if (hit.collider != null && hit.collider.name != null && (_currentWirePart.objToIgnore == null || _currentWirePart.objToIgnore.transform.position != hit.collider.transform.position))
            {
                targetPosition = hit.collider.transform.position;
                _currentWirePart.partEnd = targetPosition;
                _currentWirePart.isStuck = true;

                CreateWirePart(hit.collider.gameObject);
            }
        }

        private void DetangleWire()
        {
            if (_wireParts == null)
            {
                return;
            }
            for (var i = 0; i < _wireParts.Count; i++)
            {
                if(_wireParts[i].isStuck == false)
                {
                    continue;
                }
                var dir1 = _wireParts[i].partEnd - _wireParts[i].partOrigin;
                var dir2 = _wireParts[i + 1].partEnd - _wireParts[i + 1].partOrigin;
                var angle = Vector2.Angle(dir2, dir1);
                Vector3 cross = Vector3.Cross(dir2, dir1);

                if (cross.z > 0)
                    angle = 360 - angle;

                if (angle >= 0 && angle < 180)
                {
                    _wireParts[i].partEnd = _wireParts[i + 1].partEnd;
                    _wireParts[i].isStuck = false;
                    Destroy(_wireParts[i + 1]);
                }
            }
            //cleaning list 
            _wireParts.RemoveAll(item => item == null);
        }

        private void StretchEffect()
        {
            cordLength = 0.0f;
            foreach (var wirePart in _wireParts)
            {
                cordLength += wirePart.partLength;
            }

            //Stretch effects
            if (cordLength >= cordMaxLength)
            {
                //Debug.Log(this.name + "too far");
                Avatar.GetComponent<PlatformerCharacter2D>().ReleaseCable();    // Cable gets cut from player
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 10f;  // Reset to standard
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = false; // Reset to standard
            }
            else if (cordLength >= cordStretchThreshold)
            {
                //Debug.Log(this.name + "stretch");
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 3f;   // Limit player speed
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = true;  // Activate pullback effect
            }
            else
            {
                //Debug.Log(this.name + "isHeld");
                Avatar.GetComponent<PlatformerCharacter2D>().m_MaxSpeed = 10f;  // Reset to standard
                Avatar.GetComponent<PlatformerCharacter2D>().stretched = false; // Reset to standard
            }
        }

        private void CreateWirePart(GameObject objToIgnore = null)
        {
            var temp = gameObject.AddComponent<WirePart>();
            _wireParts.Add(temp);
            if (_wireParts.Count == 1)
            {
                _wireParts[_wireParts.Count - 1].partOrigin = transform.position;
            }
            else
            {
                _wireParts[_wireParts.Count - 1].partOrigin = _wireParts[_wireParts.Count - 2].partEnd;
            }
            _wireParts[_wireParts.Count - 1].partEnd = Avatar.transform.position;
            _wireParts[_wireParts.Count - 1].displayWire = true;
            _wireParts[_wireParts.Count - 1].objToIgnore = objToIgnore;
        }

        public void DestroyWireParts()
        {
            foreach(var wirePart in _wireParts)
            {
                Destroy(wirePart);
            }
            _wireParts = new List<WirePart>();
        }

    }
}
