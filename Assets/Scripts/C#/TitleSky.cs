using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSky : MonoBehaviour {

    public float scrolSpeed = 0.10f;
    private float scrollX;

    void Start()
    {

    }

    void Update()
    {
        scrollX += Time.deltaTime * scrolSpeed;
        Renderer render = GetComponent<Renderer>();
        render.material.mainTextureOffset = new Vector2(scrollX, 1.0f);
    }
}
