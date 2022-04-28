using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyObject : MonoBehaviour
{
    BuoyancyCalculation world;

    public float mass;
    public float density;

    [HideInInspector]
    public float cylinderVolume;
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public float LowestPoint => (transform.position.y - transform.localScale.y / 2);

    [HideInInspector]
    public Vector3 velocity;

    public void Awake()
    {
        world = GameObject.Find("Physics Handler").GetComponent<BuoyancyCalculation>();

        BuoyancyCalculation.bObj.Add(this);
        radius = gameObject.GetComponent<Renderer>().bounds.size.x / 2;
        height = gameObject.GetComponent<Renderer>().bounds.size.y / 2;
        cylinderVolume = mass / density;
    }

    public void ApplyForce()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public bool IsUnderWater(WaterBody body)
    {
        return LowestPoint < body.SurfaceHeight;
    }
}
