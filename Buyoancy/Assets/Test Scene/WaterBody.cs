using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBody : MonoBehaviour
{
    public float fluidDensity = 1f;
    public float SurfaceHeight => (transform.position.y + transform.localScale.y / 2);

}
