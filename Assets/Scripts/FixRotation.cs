using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public GameObject player;
    public float xOffset;
    public float yOffset;
    private Vector3 offset;

    public void LateUpdate() {
        offset = new Vector3(player.transform.position.x + xOffset,
                player.transform.position.y + yOffset, -.2F);
        transform.position = offset;
    }
}