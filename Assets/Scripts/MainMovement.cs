using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;
using TMPro;


public class MainMovement : MonoBehaviour
{
    public float speed = 5f;
    public Camera mainCamera;
    public string nextSceneName;
    public GameObject instructionPanel;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float halfWidth = 0.6f;
    private float halfHeight =0.6f;
    private Vector2 maxBounds;
    private Vector2 minBounds;
    private bool canMove = false;
    private float cameraHalfHeight;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (mainCamera == null) 
        {
            mainCamera = Camera.main;
        }

        CalculateCameraBounds();

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        halfWidth = boxCollider.bounds.extents.x;
        halfHeight = boxCollider.bounds.extents.y;

        instructionPanel.gameObject.SetActive(true);

        cameraHalfHeight = mainCamera.orthographicSize;

     }

    // Update is called once per frame
    void Update()
    {

        if (!canMove && Input.anyKeyDown)
        {
            instructionPanel.gameObject.SetActive(false);
            canMove = true;
        }

        if (canMove)
        {
            movementDirection.x = Input.GetAxis("Horizontal");
            movementDirection.y = Input.GetAxis("Vertical");

            if (movementDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                rb.rotation = angle - 90f; // Ajuste para que el personaje esté perpendicular al vector de dirección
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movementDirection * speed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        rb.MovePosition(newPosition);
    }

    void LateUpdate()
    {
        // Actualizar la posición de la cámara para que siga al protagonista en el eje Y
        if (mainCamera != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;

            if (transform.position.y >= 11f)
            {
                // Mantener la cámara en el cuadrado definido por (-9, 6, 0) y (9, 16, 0)
                
                cameraPosition.y = 11f;
            }
            else if (transform.position.y >= 0f)
            {
                // Seguir al protagonista suavemente
                cameraPosition.y = transform.position.y;
            }
            else
            {
                // Mantener la cámara en la posición inicial
                cameraPosition.y = 0f;
            }

            mainCamera.transform.position = cameraPosition;
        }
    }

    public Vector2 GetMovementDirection() 
    {
        return movementDirection;
    }

    void CalculateCameraBounds()
    {
        minBounds = new Vector3(-9,-5,0); // Esquina inferior izquierda
        maxBounds = new Vector3(9,16,0); // Esquina superior derecha
    }

    // TODO: Implement LoadNextScene
    void LoadNextScene()
    {
        Debug.Log("End of the level");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            LoadNextScene();
        }
    }
}
