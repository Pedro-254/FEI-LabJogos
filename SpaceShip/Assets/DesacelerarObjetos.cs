using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesacelerarObjetos : MonoBehaviour
{
    public float fatorDesaceleracao = 0.9f; // Fator de desaceleração (90%)
    public float tempoDesaceleracao = 0.1f; // Intervalo de tempo entre desacelerações

    private bool desacelerando = false; // Estado da desaceleração
    private List<Rigidbody2D> objetosDesacelerados = new List<Rigidbody2D>(); // Lista de objetos desacelerados

    void Update()
    {
        // Verifica se a tecla "C" foi pressionada
        if (Input.GetKeyDown(KeyCode.C))
        {
            desacelerando = !desacelerando; // Alterna o estado de desaceleração

            if (desacelerando)
            {
                StartCoroutine(Desacelerar()); // Inicia a coroutine para desacelerar
            }
            else
            {
                StopCoroutine(Desacelerar()); // Para a coroutine de desaceleração
            }
        }

        // Adiciona objetos visíveis na câmera à lista de desaceleração
        AdicionarObjetosNaCamera();
    }

    // Adiciona objetos visíveis na câmera à lista de desaceleração
    private void AdicionarObjetosNaCamera()
    {
        GameObject[] todosObjetos = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject objeto in todosObjetos)
        {
            Rigidbody2D rb = objeto.GetComponent<Rigidbody2D>();
            if (rb != null && IsObjectVisibleFromCamera(objeto) && !objetosDesacelerados.Contains(rb))
            {
                objetosDesacelerados.Add(rb); // Adiciona o objeto à lista se não estiver já presente
            }
        }
    }

    // Verifica se o objeto está visível na câmera
    bool IsObjectVisibleFromCamera(GameObject objeto)
    {
        Vector3 posicaoNaTela = Camera.main.WorldToViewportPoint(objeto.transform.position);
        return posicaoNaTela.x >= 0 && posicaoNaTela.x <= 1 && posicaoNaTela.y >= 0 && posicaoNaTela.y <= 1;
    }

    // Coroutine para desacelerar todos os objetos
    private IEnumerator Desacelerar()
    {
        while (desacelerando)
        {
            // Desacelera todos os objetos na lista
            for (int i = 0; i < objetosDesacelerados.Count; i++)
            {
                Rigidbody2D rb = objetosDesacelerados[i];
                if (rb != null)
                {
                    rb.velocity *= (1 - fatorDesaceleracao); // Aplica a desaceleração
                }
            }
            yield return new WaitForSeconds(tempoDesaceleracao); // Aguarda o intervalo
        }
    }
}
