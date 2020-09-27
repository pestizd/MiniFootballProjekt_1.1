using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Klasa za loptu
public class BallScript : MonoBehaviour, IResetable
{
    //Treba nam increment iz Score skripte, ali nije static pa nam treba instanca
    public ScoreScript ScoreScriptInstance;
    //Je li ili nije gol
    public static bool WasGoal { get; private set; }
    public float MaxSpeed;

    public AudioManager audioManager;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
        UiManager.Instance.ResetableGameObjects.Add(this);
    }
    //Built in metoda od Unitya
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "AiGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                //Tu trebamo pustiti zvuk
                //audioManager.PlayGoal;
                StartCoroutine(ResetBall(false));
            }
            else if (other.tag == "PlayerGoal")
            {                                   //Ai je zabio
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                //audioManager.PlayGoal;
                StartCoroutine(ResetBall(true));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //audioManager.PlayBallCollision;
    }
    //Pomocu Corutina mozemo cekati odredeno vrijeme da se nesto desi
    private IEnumerator ResetBall (bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        //Reset
        rb.velocity = rb.position = new Vector2(0, 0);
        //Moramo paziti da se lopta nakon gola spawna na strani primatelja gola
        if (didAiScore)
            rb.position = new Vector2(0, -1);
        else
            rb.position = new Vector2(0, 1);
    }
    //Reset lopte nakon zavrsene partije
    public void ResetPosition()
    {
        rb.position = new Vector2(0, 0);
    }

    //Limitiramo brzinu lopte
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
