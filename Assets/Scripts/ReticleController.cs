using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float reticleSpeed;
    public float reticleDepth;
    public float responsivenessPercentage; // As Creamboy becomes meltier, the aiming becomes less responcive

    void Update() {
        Vector3 worldMousePosition = GetMouseWorldPosition();
        Vector3 nextPosition = Vector3.Lerp(this.transform.position, worldMousePosition, responsivenessPercentage / 100);

        this.transform.position = nextPosition;
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = reticleDepth;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
