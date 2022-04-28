using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    Transform platformParent;
    public Transform levelGeneratorPoint;

    private float distanceMin;
    private float distanceMax;
    public float distanceBetweenY;

    int SpawnedPlatforms = 0;

    [SerializeField]
    PlatformSelector selector;


    private void Start()
    {
        var size = Camera.main.orthographicSize * Camera.main.aspect;
        distanceMin = -size;
        distanceMax = size;
    }
    private float GetObjWidth(GameObject go)
    {
        var obj = Instantiate(go);
        var size = obj.GetComponent<BoxCollider2D>().bounds.size.x;
        Destroy(obj);
        return size;
    }
    private void Update()
    {

        if (transform.position.y < levelGeneratorPoint.position.y)
        {
            var obj = selector.GetRandomPlatformAtHeight(height: SpawnedPlatforms);
            var objWidth = GetObjWidth(obj.go);
            var spawnY = transform.position.y + distanceBetweenY;

            var spawnX = Random.Range(distanceMin, distanceMax - objWidth) + objWidth/2f;


            transform.position = new Vector3(spawnX, transform.position.y + distanceBetweenY, transform.position.z);
            var go = Instantiate(
                original: obj.go, 
                parent: platformParent,
                position: transform.position, 
                rotation: transform.rotation 
            );
            Platform.Spawned(go);

            SpawnedPlatforms++;
        }
    }


}

[System.Serializable]
class PlatformSelector
{
    [SerializeField]
    DoodlePlatform[] platforms;

    public DoodlePlatform GetRandomPlatformAtHeight(int height)
    {
        //Platformer där platformer spawnar efter höjden
        var relevantPlatforms = platforms.Where(p => p.SpawnAfter <= height);
        //Summerar Outsen av relevanta platformarna
        var outSum = relevantPlatforms.Sum(p => p.Outs);

        var num = Random.Range(0, outSum);

        //Bestämmer vilken platform som spawnar
        var currentOutSum = 0;
        foreach (var platform in relevantPlatforms)
        {
            if (num < currentOutSum + platform.Outs)
                return platform;

            currentOutSum += platform.Outs;
        }
        
        //wont happen
        return null;
    }

}

[System.Serializable]
class DoodlePlatform
{
    [HideInInspector]
    public string Key = "Platform";

    [SerializeField]
    public GameObject go;

    [SerializeField]
    public int SpawnAfter;

    [SerializeField]
    public int Outs;

}
