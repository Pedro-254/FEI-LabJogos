using UnityEngine;

public class EventoNaveMae : MonoBehaviour
{
    public GameObject objectPrefab;    // Prefab do objeto que será instanciado
    public float moveSpeed = 5f;       // Velocidade de movimento para a direita
    public float endXPosition = 10f;   // Posição X onde o objeto vai desaparecer
    public Vector3 startPosition;      // Posição inicial do objeto
    public float delayBeforeStart = 2f; // Tempo de espera antes de instanciar e começar o movimento

    private GameObject instantiatedObject; // Referência ao objeto instanciado
    private bool objectStartedMoving = false; // Verifica se o objeto começou a se mover

    void Start()
    {
        // Começa a rotina para instanciar o objeto após o tempo de espera
        Invoke("InstantiateObject", delayBeforeStart);
    }

    void Update()
    {
        // Se o objeto foi instanciado e começou a se mover
        if (objectStartedMoving && instantiatedObject != null)
        {
            // Move o objeto para a direita (direção positiva no eixo X)
            instantiatedObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // Verifica se o objeto passou da posição final e, se sim, o desativa
            if (instantiatedObject.transform.position.x >= endXPosition)
            {
                Destroy(instantiatedObject); // Destrói o objeto após passar da posição final
            }
        }
    }

    void InstantiateObject()
    {
        // Instancia o objeto na posição inicial especificada
        instantiatedObject = Instantiate(objectPrefab, startPosition, Quaternion.identity);
        objectStartedMoving = true; // Define que o objeto começou a se mover
    }
}
