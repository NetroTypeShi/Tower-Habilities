using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability4 : ParentClass
{
    float cooldownTime = 0f;
    GameObject gameStart;
    GameStart startScript;
    HabilityHolder habilityScript;
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
                gameObject.transform.position = new Vector2(Random.Range(-8.27f, 8.27f), Random.Range(-3.10f, 4.23f));
                habilityScript = gameObject.GetComponent<HabilityHolder>();
                habilityScript.habilitiesOn = false;
                Invoke("RetunToOriginalPosition", 3f);

                cooldownTime = cooldown;
                icon.fillAmount = 0;
            }
        }
        
    }
    void RetunToOriginalPosition()
    {
        gameObject.transform.position = new Vector2(0, 0);
        habilityScript = gameObject.GetComponent<HabilityHolder>();
        habilityScript.habilitiesOn = true;
    }
}
