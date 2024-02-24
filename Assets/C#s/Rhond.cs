using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhond : MonoBehaviour
{
    public float rotationSpeed = 15f; // ‰ñ“]‚Ì‘¬‚³
    public float rotationRange = 15f; // ‰ñ“]‚Ì”ÍˆÍ

    private Quaternion initialRotation;

    void Start()
    {
        // ‰Šú‚Ì‰ñ“]‚ğ•Û‘¶
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Y²‰ñ“]‚ğ•Ï‰»‚³‚¹‚é
        float newYRotation = initialRotation.eulerAngles.y + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // Z²‰ñ“]‚ğ•Ï‰»‚³‚¹‚é
        float newZRotation = initialRotation.eulerAngles.z + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // V‚µ‚¢‰ñ“]’l‚ğƒZƒbƒg
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, newYRotation, newZRotation);
    }
}
