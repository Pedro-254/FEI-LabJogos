using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveMaeController : MonoBehaviour
{
    // Referência ao prefab de explosão (opcional, se você quiser uma explosão visual)
    public GameObject explosionPrefab;

    void Start()
    {
        // Inicializações, se necessário
    }

    void Update()
    {
        // Lógica de movimentação ou outros comportamentos
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            // Destrói a "Nave Mãe"
            Destroy(gameObject);
            ScoreManager.AddScore(100);
        }
    }
}
