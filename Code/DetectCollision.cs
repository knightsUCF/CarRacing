using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DetectCollision : MonoBehaviour
{
    
    State state;
    Score score;


    private void Start()
    {
        state = FindObjectOfType<State>();
        score = FindObjectOfType<Score>();
    }


    private void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.tag == "Car") state.game = State.Game.Over;


        if (c.gameObject.tag == "Loot")
        {
            score.Set(state.score += 100);
            Destroy(c.gameObject);
        }

        // later we will have different types of tags, so LootBonusPoints, LootHealth, etc, and just add things here based on the type
    }

}
