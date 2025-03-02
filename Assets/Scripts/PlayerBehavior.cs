using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    
    [SerializeField] ParticleSystem damageParticles;
    float timePassed = 0;
    [SerializeField] float maxTime = 0.2f;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Gradient healthColor;
    [SerializeField] public ParticleSystem healthParticles;
    float healthPercent;
    GameObject gameStart;
    GameStart startScript;
    public Vector3 point;
    public Vector2 mouse;
    Camera cam;
    public float angle;
    public Vector2 direction;
    PlayerAttributes playerAttributesScript;
    EnemyBehavior enemyMovement;


    // Start is called before the first frame update
    void Start()
    {
        
        playerAttributesScript = gameObject.GetComponent<PlayerAttributes>();
        cam = Camera.main;
        playerAttributesScript.health = playerAttributesScript.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerAttributesScript = gameObject.GetComponent<PlayerAttributes>();
        gameStart = GameObject.FindGameObjectWithTag("Start");
        startScript = gameStart.GetComponent<GameStart>();
        if (startScript.gameStarted == true) 
        {
                      
            if (playerAttributesScript.health > 0)
            {
                if (playerAttributesScript.health > playerAttributesScript.maxHealth) { playerAttributesScript.health = playerAttributesScript.maxHealth; }

                rend.color = healthColor.Evaluate(1f * playerAttributesScript.health / playerAttributesScript.maxHealth);

                point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - cam.transform.position.z));
                //(posición del raton x e y, vector unitario entre el eje z del point y el del cam) 

                Vector2 direction = point - transform.position;//Vector unitario entre el puntero y la posición inicial

                transform.rotation = Quaternion.Euler(0, 0, angle); //aplicar rotación

                angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward); //calcuar el angulo negri
            }

            if (playerAttributesScript.health <= 0)
            {
                GameEvents.PlayerDied.Invoke(); 
            }
            
        }
        
    }
    void StopParticles()
    {
     damageParticles.Stop();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAttributesScript = gameObject.GetComponent<PlayerAttributes>();
            enemyMovement = collision.gameObject.GetComponent<EnemyBehavior>();
            playerAttributesScript.health = playerAttributesScript.health - enemyMovement.damage;
            damageParticles.Play();
            Invoke("StopParticles", 1f);

        }
    }
}
