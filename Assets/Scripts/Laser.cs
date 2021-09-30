using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float laserSpeed;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);

        if (transform.position.y > Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y
            || 
            transform.position.y<Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y)
            Destroy(this.gameObject);
    }
}
