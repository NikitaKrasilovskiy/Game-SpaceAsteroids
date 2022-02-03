using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFly : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer quadRenderer;
    public GameObject quadGameObject;

    [SerializeField] private float scrollSpeed = 0.5f;

    void Start()
    {
        quadRenderer = quadGameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}
