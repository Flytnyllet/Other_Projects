using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyCalculation : MonoBehaviour
{
    public static List<BuoyancyObject> bObj = new List<BuoyancyObject>();

    private WaterBody water;

    [Range(-10, 10)]
    public float gravity = 9.82f;

    private void Awake()
    {
        water = GameObject.Find("Water").GetComponent<WaterBody>();
    }

    void FixedUpdate()
    {
        int loopCounter = 1;
        foreach (BuoyancyObject obj in bObj)
        {
            for (int i = loopCounter; i <= bObj.Count; i++)
            {
                if (obj.IsUnderWater(water))
                {
                    Debug.Log(obj.IsUnderWater(water));
                    BuoyancyForce(obj);
                }
                else
                {
                    Gravity(obj);
                }
            }
            ApplyForces(obj);
            loopCounter++;
        }
    }

    void ApplyForces(BuoyancyObject temp)
    {
        temp.ApplyForce();
    }

    private void Gravity(BuoyancyObject obj)
    {
        obj.velocity.y -= gravity * Time.deltaTime;
    }

    private void BuoyancyForce(BuoyancyObject obj)
    {
        Vector3 center = obj.gameObject.GetComponent<Renderer>().bounds.center;

        float distToSurface = Mathf.Abs(water.SurfaceHeight - obj.LowestPoint);
        float cylinderUnderWater = distToSurface / obj.transform.localScale.y;

        if (cylinderUnderWater > 1f)
        {
            cylinderUnderWater = 1f;
        }
        float volumeUnderWater = obj.cylinderVolume * cylinderUnderWater;
        float buoyForce = volumeUnderWater *  water.fluidDensity * gravity;
        float acceleration = (buoyForce / obj.mass) + gravity;

        obj.velocity.y += acceleration * Time.deltaTime;
    }

    //public float CircleAreaUnderWater(float volume, float posY, float waterBoundsYMax, BuoyancyObject obj)
    //{
    //    if (posY + obj.radius < waterBoundsYMax) // Return full volume (area) if fully submerged
    //        return volume;
    //    obj.height = obj.radius - (posY - waterBoundsYMax); // calculate circle's height under water
    //    float depth = obj.radius * obj.radius * Mathf.Acos((obj.radius - obj.height) / obj.radius) - (obj.radius - obj.height) * Mathf.Pow(2f * obj.radius * obj.height - obj.height * obj.height, 0.5f);
    //    return depth;
    //}

    //public float CylinderVolumeUnderWater(BuoyancyObject obj)
    //{
    //    float cylinderDepth = CircleAreaUnderWater(obj.cylinderVolume, , , obj);

    //    float volumeUnderWater = obj.height * (obj.radius * obj.radius *
    //        Mathf.Acos(obj.radius - cylinderDepth / obj.radius) - obj.radius - obj.cylinderDepth)
    //        * (Mathf.Pow(2 * obj.radius * cylinderDepth - (cylinderDepth * cylinderDepth), 0.5f));

    //    return volumeUnderWater;
    //}
}
