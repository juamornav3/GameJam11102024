using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GripperController : MonoBehaviour
{
    public AudioClip correct;
    public AudioClip sound;
    private Vector3 initialPosition;
    private int movementsLeft = 8;
    public float velocity = 100;
    private Rigidbody2D rb;
    private bool moving = false;
    private bool horizontal = false;
    public GameObject dropdown;
    public PuzzleData puzzle;
    public GameObject transition;
    public Result result;
    public GameObject music;
    public GameObject button;
    public TextMeshProUGUI contText;
    public int idPuzzle;
    public int idScene;
    void Start()
    {
        Cursor.visible = true;
        movementsLeft = 7;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        movementsLeft = 7;
        contText.text = "Llamadas restantes: " + movementsLeft;
        transform.position = initialPosition;
        moving = false;
        rb.velocity = new Vector2(0.27f, 0f) * 0;
        StopAllCoroutines();
    }
    public void MoveGripper()
    {
        switch (dropdown.GetComponent<TMP_Dropdown>().value)
        {
            case 0:
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.27f, 0f);
                velocity = 150;
                horizontal = true;
                break;
            case 1:
                velocity = -150;
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.27f, 0f);
                horizontal = true;
                break;
            case 2:
                velocity = 150;
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.27f);
                horizontal = false;
                break;
            case 3:
                velocity = -150;
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.27f);
                horizontal = false;
                break;
            default:
                break;
        }
        movementsLeft--;
        contText.text = "Llamadas restantes: "+movementsLeft;
        moving = true;
    }
    void Update()
    {
        if (moving)
        {
            button.GetComponent<Button>().enabled = false;
            Vector2 direction = horizontal ? Vector2.right : Vector2.up;
            rb.velocity = direction * velocity * Time.fixedDeltaTime;

            if (movementsLeft < 0)
            {
                
                Reset();
            }
        }
        else
        {
            button.GetComponent<Button>().enabled=true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Spines"))
        {
            rb.velocity = Vector2.zero;
            Reset();
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        moving = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            StartCoroutine(WaitToSolve());
        }
        button.GetComponent<Button>().enabled = true;
    }

    IEnumerator WaitToSolve()
    {
        yield return new WaitForSeconds(0.5f);
        puzzle.finished = true;
        Solve();
    }

    public void Solve()
    {
        SoundManager.instance.playSound(sound, 0.6f);
        music.GetComponent<AudioSource>().enabled = false;
        LoadResult();
        result.result = puzzle.finished;
        if (puzzle.finished)
        {
            transition.GetComponent<SceneController>().SolvePuzzle(correct, puzzle.finished);
        }
    }
    private void LoadResult()
    {
        result.result = puzzle.finished;
        result.idPuzle = idPuzzle;
        result.idScene = idScene;
        result.well = "¡Bien hecho!, ¡Con esa tarjeta podrás seguir avanzando!";
        result.bad = "Que lástima... mira bien el número de veces que se repite cada acción...";
    }


}
