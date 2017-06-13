using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableLeftUi : MonoBehaviour {

    public Wire currentWire;

    public bool cableIsHeld = false;
    public float maxCableLength = 0.0f;
    public float currentCableLength = 0.0f;

    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color notVisibleColor = new Color(1, 1, 1, 0);
    private PlatformerCharacter2D avatar;
    Image circle;
    // Use this for initialization
    void Start () {
        avatar = FindObjectOfType<PlatformerCharacter2D>();
        circle = gameObject.GetComponentInChildren<Image>();
        circle.color = notVisibleColor;

    }

    private void Update()
    {
        if (currentWire != null && currentWire.IsHeld)
        {
            circle.color = visibleColor;
            var posX = avatar.transform.position.x;
            var posY = avatar.transform.position.y + 1;
            transform.position = new Vector3(posX, posY, 0);
            circle.fillAmount = (currentWire.cordMaxLength - currentWire.cordLength) / currentWire.cordMaxLength ;
            Debug.Log((currentWire.cordMaxLength - currentWire.cordLength) / currentWire.cordMaxLength);
        }
        else
        {
            circle.color = notVisibleColor;
        }

    }

}
