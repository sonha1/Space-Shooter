using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;

    private Renderer renderer;
    private Vector2 saveOffset;
   
    // private Collider2D quadCollider;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        // quadCollider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Repeat(Time.time * speed, 1);
        Vector2  offset = new Vector2 (0, x);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

     void LateUpdate()
    {
        // Lấy tất cả các đối tượng có cùng Sorting Layer và Order in Layer với quad
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.sortingLayerName == renderer.sortingLayerName &&
                renderer.sortingOrder > renderer.sortingOrder)
            {
                // Nếu có đối tượng nằm trên quad, ẩn quad bằng cách vô hiệu hoá renderer
                renderer.enabled = false;
                return;
            }
        }

        // Nếu không có đối tượng nằm trên quad, hiển thị quad
        renderer.enabled = true;
    }
    
}
