using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBelowScreen : MonoBehaviour {

    private float halfHeight;
    
    private void Start()
    {
        halfHeight = Camera.main.orthographicSize;
    }

    void LateUpdate ()
    {
        var cameraBot = Camera.main.transform.position.y - halfHeight;
        foreach (Transform child in transform)
        {
            var halfHeight = child.GetComponent<SpriteRenderer>().size.y / 2f;

            if (child.position.y + halfHeight < cameraBot)
                Destroy(child.gameObject);
        }
	}
}
