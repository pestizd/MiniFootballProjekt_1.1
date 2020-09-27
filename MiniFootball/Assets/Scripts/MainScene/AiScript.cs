using UnityEngine;

public class AiScript : MonoBehaviour, IResetable
{
    //Polja koja ce koristiti AI
    public float MaxMovementSpeed; //Postavljeno u Unityu na 20
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    public Rigidbody2D Ball;

    //Referenca na rigidbody od lopte
    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform BallBoundaryHolder;
    private Boundary ballBoundary;

    private Vector2 targetPosition;

    //Protivnik je pre precizan, trebamo ga napraviti lošijim haha --> offset
    private bool isFirstTimeInOpponentsHalf = true;
    private float offsetXFromTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        //Postavljamo Boundary na kordinate
        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                      PlayerBoundaryHolder.GetChild(2).position.x,
                                      PlayerBoundaryHolder.GetChild(3).position.x);

        //Postavljamo Boundary na kordinate
        ballBoundary = new Boundary(BallBoundaryHolder.GetChild(0).position.y,
                                      BallBoundaryHolder.GetChild(1).position.y,
                                      BallBoundaryHolder.GetChild(2).position.x,
                                       BallBoundaryHolder.GetChild(3).position.x);

        UiManager.Instance.ResetableGameObjects.Add(this);

        //Da nam rade Togglei
        switch (GameValues.Difficulty)
        {
            case GameValues.Difficulties.Easy:
                MaxMovementSpeed = 10;
                break;
        
            case GameValues.Difficulties.Medium:
                MaxMovementSpeed = 15;
                break;

            case GameValues.Difficulties.Hard:
                MaxMovementSpeed = 20;
                break;
        }

    }
    //Koristimo fixed update da nam se ne stvaraju problemi
    private void FixedUpdate()
    {
        if(!BallScript.WasGoal) { 
        float movementSpeed;

        if (Ball.position.y < ballBoundary.Down) {
            if (isFirstTimeInOpponentsHalf)
            {
                isFirstTimeInOpponentsHalf = false;
                offsetXFromTarget = Random.Range(-1f, 1f);
            }
            //Brzina ce biti manja od max speeda i random
            movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
            //Ako je lopta na donjoj polovici zelimo samo po x-u je kretati
            targetPosition = new Vector2(Mathf.Clamp(Ball.position.x + offsetXFromTarget, playerBoundary.Left,
                                                     playerBoundary.Right),
                                         startingPosition.y);
        }
        //Ako je lopta na strani AI-a kretat ce se prema njoj
        else
        {
            isFirstTimeInOpponentsHalf = true;
            //Kretnja za AI
            movementSpeed = Random.Range(MaxMovementSpeed * 0.4F, MaxMovementSpeed);
            targetPosition = new Vector2(Mathf.Clamp(Ball.position.x, playerBoundary.Left,
                                         playerBoundary.Right),
                                         Mathf.Clamp(Ball.position.y, playerBoundary.Down,
                                         playerBoundary.Up));
        }
        //Pokrecemo rigidbody
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition,
                        movementSpeed * Time.fixedDeltaTime));
    }
    }
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}
