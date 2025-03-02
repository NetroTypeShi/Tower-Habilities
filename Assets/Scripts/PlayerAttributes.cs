using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    float timePassed = 0;
    [SerializeField] float maxTime = 0.2f;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth = 20;
    [SerializeField] public float strength = 1;
    [SerializeField] public float maxStrength = 2.5f;
    [SerializeField] public float upgradeHealing = 0.2f;
    [SerializeField] public float upgradeStrength = 0.1f;
    GameObject player;
    GameObject gameStart;
    GameStart startScript;
    PlayerBehavior triangleScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameStart = GameObject.FindGameObjectWithTag("Start");
        startScript = gameStart.GetComponent<GameStart>();
        triangleScript = player.GetComponent<PlayerBehavior>();
        if (startScript.gameStarted == true)
        {
            timePassed = timePassed + Time.deltaTime;

            if (timePassed >= 0.5f) { timePassed = 0; }
            if (timePassed == 0)
            {              
                health = health + upgradeHealing;
                
            }
            if (strength >= maxStrength) { strength = maxStrength; }
            
        }
    }
}
