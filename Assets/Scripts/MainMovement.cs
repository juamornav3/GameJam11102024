using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;


public class MainMovement : MonoBehaviour
{
    public float speed = 5f;
    public Camera mainCamera;
    public string nextSceneName;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float halfWidth = 0.6f;
    private float halfHeight =0.6f;
    private Vector2 maxBounds;
    private Vector2 minBounds;


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
     }

    // Update is called once per frame
    void Update()
    {
        movementDirection.x = Input.GetAxis("Horizontal");
        movementDirection.y = Input.GetAxis("Vertical");

        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f; // Ajuste para que el personaje esté perpendicular al vector de dirección
        }

    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movementDirection * speed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        rb.MovePosition(newPosition);
    }

    public Vector2 GetMovementDirection() 
    {
        return movementDirection;
    }

    void CalculateCameraBounds()
    {
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)); // Esquina inferior izquierda
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)); // Esquina superior derecha
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
