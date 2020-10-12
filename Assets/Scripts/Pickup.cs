using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject player;

    private ScoreHolder scoreHolder;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");

        scoreHolder = player.GetComponent<ScoreHolder>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scoreHolder.score++;
            Destroy(gameObject);
        }
    }

}
