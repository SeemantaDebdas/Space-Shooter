using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    Material material;
    [SerializeField] float backgroundScrollerSpeed = 0.3f;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        offset = new Vector2(0f, backgroundScrollerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
