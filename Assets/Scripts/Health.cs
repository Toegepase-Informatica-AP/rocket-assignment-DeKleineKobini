using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Health : MonoBehaviour
{

    public enum DeathAction { LoadLevel, Nothing };

    public float healthPoints = 1f;
    public float respawnHealthPoints = 1f;      //base health points

    public int numberOfLives = 1;                   //lives and variables for respawning
    public bool isAlive = true;

    public GameObject explosionPrefab;

    public DeathAction onLivesGone = DeathAction.Nothing;

    public string LevelToLoad = "";

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;


    // Use this for initialization
    void Start()
    {
        // store initial position as respawn location
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        if (LevelToLoad == "") // default to current scene 
            LevelToLoad = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {               // if the object is 'dead'
            numberOfLives--;                    // decrement # of lives, update lives GUI

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if (numberOfLives > 0)
            { // respawn
                transform.position = respawnPosition;   // reset the player to respawn position
                transform.rotation = respawnRotation;
                healthPoints = respawnHealthPoints; // give the player full health again
            }
            else
            { // here is where you do stuff once ALL lives are gone)
                isAlive = false;

                switch (onLivesGone)
                {
                    case DeathAction.LoadLevel:
                        SceneManager.LoadScene(LevelToLoad);
                        break;
                    case DeathAction.Nothing:
                        // do nothing, death must be handled in another way elsewhere
                        break;
                }
                Destroy(gameObject);
            }
        }
    }

    public void ApplyDamage(float amount)
    {
        healthPoints = healthPoints - amount;
    }

    public void ApplyHeal(float amount)
    {
        healthPoints = healthPoints + amount;
    }

    public void ApplyBonusLife(int amount)
    {
        numberOfLives = numberOfLives + amount;
    }

    public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation)
    {
        respawnPosition = newRespawnPosition;
        respawnRotation = newRespawnRotation;
    }
}
