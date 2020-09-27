using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   //Instance kretanja igraca
    public List<PlayerMovement> Players = new List<PlayerMovement>();
    //Da mozemo razaznati
    public GameObject AiPlayer;

    private void Start()
    {   //Ako je MP desable Ai
        if (GameValues.IsMultyplayer)
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = true;
            AiPlayer.GetComponent<AiScript>().enabled = false;
        }
        else
        {
            AiPlayer.GetComponent<PlayerMovement>().enabled = false;
            AiPlayer.GetComponent<AiScript>().enabled = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            //Kretanje svakog playera
            foreach (var player  in Players)
            {
                //Ako je dodirnut prstom pokreni igraca
                if (player.LockedFingerID  == null)
                {                                   //Upravo smo kliknuli na ekran
                    if  (Input.GetTouch(i).phase == TouchPhase.Began &&
                        player.PlayerCollider.OverlapPoint(touchWorldPos))
                    {
                        player.LockedFingerID = Input.GetTouch(i).fingerId;
                    }
                }
                else if (player.LockedFingerID == Input.GetTouch(i).fingerId)
                {
                    player.MoveToPosition(touchWorldPos);
                    if (Input.GetTouch(i).phase == TouchPhase.Ended ||
                        Input.GetTouch(i).phase == TouchPhase.Canceled)
                        player.LockedFingerID = null;
                }
            }
        }
    }
}
