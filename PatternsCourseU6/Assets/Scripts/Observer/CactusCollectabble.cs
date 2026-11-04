using UnityEngine;
using System;
public class CactusCollectabble : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float rotationSpeed = 2f;

    public static event Action coinCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * 360, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCollected?.Invoke();
            AudioManager.Instance.PlayCactusSound();
            Destroy(gameObject);
        }
    }
}