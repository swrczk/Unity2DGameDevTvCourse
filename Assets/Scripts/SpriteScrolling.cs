using UnityEngine;

public class SpriteScrolling : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;
    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        var offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}