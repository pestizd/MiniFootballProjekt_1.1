using UnityEngine;
//Klasa za kretnju igraca
public class PlayerMovement : MonoBehaviour, IResetable
{
    //bool wasJustClicked;
    //bool canMove;
    //Sprema x i y osi
    //Vector2 playerSize;

    Rigidbody2D rb;
    Vector2 startingPosition;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

    //Treba nam novi collider2D
    public Collider2D PlayerCollider { get; private set; }

    public PlayerController Controller;

    //Nullable int
    public int? LockedFingerID { get; set; }

    // Start is called before the first frame update
    void Start()
    {   //Dohvacamo velicinu objekta
        //playerSize = GetComponent<SpriteRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        PlayerCollider = GetComponent<Collider2D>();
 
        //Postavljamo Boundary na kordinate djece (up,down,left,right)
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    private void OnEnable()
    {
        Controller.Players.Add(this);
        UiManager.Instance.ResetableGameObjects.Add(this);//Dodajemo instancu
    }
    private void OnDisable()
    {
        Controller.Players.Remove(this);
        UiManager.Instance.ResetableGameObjects.Remove(this);//Micemo instancu
    }

    // Update is called once per frame ==> Nepotrebno
    /*
    void Update()
        
    {   //Gledamo jel pritisnut ljevi klik na misu 0 = ljevi
        if (Input.GetMouseButton(0))
        {   //Dohvacamo poziciju misa na ovaj nacin (Kordinate svijeta ne kordinatu ekrana) jer razliciti ekrani imaju razlicite rezolucije
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Provjera je li igrac kliknuo
            if (wasJustClicked)
            {   //Moramo ga postaviti na false jer se update izvrsava stalno pa vi se if konstantno vrtio
                wasJustClicked = false;
                //Provjera je li klik na playeru
                if (playerCollider.OverlapPoint(mousePos)) // Toliko jednostavije
                {   //Da ne skace po ekranu nego da se krece
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
 
            if (canMove)
            {   //Prava ristrikcija kretanja sa Clampom (Unity metoda)
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                  playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                  playerBoundary.Up));
                //Kreci se prema misu
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {   //Ako igrac ne klika sljedeci klik ce sigurno biti prvi klik
            wasJustClicked = true;
        }
    }*/

    public void MoveToPosition(Vector2 position)
    {   //Prava ristrikcija kretanja sa Clampom (Unity metoda)
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(position.x, playerBoundary.Left,
                                                          playerBoundary.Right),
                                              Mathf.Clamp(position.y, playerBoundary.Down,
                                                          playerBoundary.Up));
        //Kreci se prema misu
        rb.MovePosition(clampedMousePos);

    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}

//Provjera je li klik na playeru ==> OVAJ NACIN JE DOSTA KOMPLICIRAN
/*if ((mousePos.x >= transform.position.x && mousePos.x<transform.position.x + playerSize.x ||
mousePos.x <= transform.position.x && mousePos.x> transform.position.x - playerSize.x) &&
(mousePos.y >= transform.position.y && mousePos.y<transform.position.y + playerSize.y ||
mousePos.y <= transform.position.y && mousePos.y> transform.position.y - playerSize.y))*/
