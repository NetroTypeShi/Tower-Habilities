using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability2 : ParentClass
{
    float cooldownTime = 0f;
    [SerializeField] ParticleSystem healthParticles;
    GameObject gameStart;
    GameStart startScript;
    GameObject player;
    PlayerBehavior triangleScript;
    PlayerAttributes playerAttributesScript;
    private void FixedUpdate()
    {
        if (cooldownTime > 0f)
        {
            cooldownTime -= Time.deltaTime;
            icon.fillAmount = 1 - (cooldownTime / cooldown);
        }
    }
    public override void Trigger()
    {
        gameStart = GameObject.FindGameObjectWithTag("Start");
        startScript = gameStart.GetComponent<GameStart>();
        if (startScript.gameStarted == true)
        {
            if (cooldownTime <= 0)
            {
                healthParticles.Play();
                player = GameObject.FindGameObjectWithTag("Player");
                triangleScript = player.GetComponent<PlayerBehavior>();
                playerAttributesScript = player.GetComponent<PlayerAttributes>();
                playerAttributesScript.health = playerAttributesScript.health + playerAttributesScript.maxHealth / 2;
                Invoke("StopParticles", 1f);

                cooldownTime = cooldown;
                icon.fillAmount = 0;
            }
        }
    }
    void StopParticles()
    {
        healthParticles.Stop();
    }
}
