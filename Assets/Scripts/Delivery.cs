using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float destoyDelay;
    [SerializeField] Color32 defaultColor;
    [SerializeField] Color32 pickUpColor;
    SpriteRenderer spriteRenderer;
    bool hasPackage = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("package picked up...");
            hasPackage = true;
            Destroy(other.gameObject, destoyDelay);
            spriteRenderer.color = pickUpColor;
        }
        else if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("package delivered...");
            hasPackage = false;
            Destroy(other.gameObject, destoyDelay);
            spriteRenderer.color = defaultColor;
        }
    }
}
