using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    public float mass;
    public float density;
    [Range(0, 1)]
    public float waterDrag;
    [Range(0, 1)]
    public float airDrag;
    public float gravity;
    public Transform[] floatingPoints;

    protected Waves waves;

    protected float volume;
    protected float waterLine;
    protected Vector3[] waterLinePoints;

    protected Vector3 centerOffset;
    protected Vector3 smoothVectorRotation;
    protected Vector3 targetUp;
    protected Vector3 gravityVector;
    protected Vector3 velocity;

    public Vector3 Center { get { return transform.position + centerOffset; } }

    private void Awake()
    {
        volume = mass / density;
        gravityVector = new Vector3(0, gravity, 0);
        waves = FindObjectOfType<Waves>();

        waterLinePoints = new Vector3[floatingPoints.Length];
        for (int i = 0; i < floatingPoints.Length; i++)
        {
            waterLinePoints[i] = floatingPoints[i].position;
        }
        centerOffset = GetCenter(waterLinePoints) - transform.position;
    }

    public void Update()
    {
        float newWaterLine = 0f;
        bool pointUnderWater = false;

        for (int i = 0; i < floatingPoints.Length; i++)
        {
            waterLinePoints[i] = floatingPoints[i].position;
            waterLinePoints[i].y = waves.GetHeight(floatingPoints[i].position);
            newWaterLine += waterLinePoints[i].y / floatingPoints.Length;
            if (waterLinePoints[i].y > floatingPoints[i].position.y)
                pointUnderWater = true;
        }

        float waterLineDelta = newWaterLine - waterLine;
        waterLine = newWaterLine;

        if (waterLine > Center.y)
        {
            velocity *= waterDrag;
            BuoyancyForce();
        }
        else
        {
            velocity *= airDrag;
            velocity -= gravityVector * Time.deltaTime;
        }
        transform.position += velocity * Time.deltaTime;

        targetUp = GetNormal(waterLinePoints);
        if (waterLine >= Center.y)
        {
            return;
        }
        else if (pointUnderWater)
        {
            targetUp = Vector3.SmoothDamp(transform.up, targetUp, ref smoothVectorRotation, 0.2f);
            transform.rotation = Quaternion.FromToRotation(transform.up, targetUp) * transform.rotation;
        }
    }

    private void BuoyancyForce()
    {
        float buoyForce = volume * waves.fluidDensity * gravity;
        Vector3 acceleration = new Vector3(0, (buoyForce / mass) - gravity, 0);

        velocity += acceleration * Time.deltaTime;
    }

    public static Vector3 GetCenter(Vector3[] points)
    {
        var center = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
            center += points[i] / points.Length;
        return center;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (floatingPoints == null)
            return;

        for (int i = 0; i < floatingPoints.Length; i++)
        {
            if (floatingPoints[i] == null)
                continue;

            if (waves != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(waterLinePoints[i], Vector3.one * 0.3f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(floatingPoints[i].position, 0.1f);
        }

        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, waterLine, Center.z), Vector3.one * 1f);
        }
    }

    public static Vector3 GetNormal(Vector3[] points)
    {
        //https://www.ilikebigbits.com/2015_03_04_plane_from_points.html
        if (points.Length < 3)
            return Vector3.up;

        var center = GetCenter(points);

        float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

        for (int i = 0; i < points.Length; i++)
        {
            var r = points[i] - center;
            xx += r.x * r.x;
            xy += r.x * r.y;
            xz += r.x * r.z;
            yy += r.y * r.y;
            yz += r.y * r.z;
            zz += r.z * r.z;
        }

        var det_x = yy * zz - yz * yz;
        var det_y = xx * zz - xz * xz;
        var det_z = xx * yy - xy * xy;

        if (det_x > det_y && det_x > det_z)
            return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
        if (det_y > det_z)
            return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
        else
            return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;
    }
}
