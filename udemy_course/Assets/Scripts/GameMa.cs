using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMa : MonoBehaviour
{
    public GameObject[] zombies;
    bool isRaising = false;
    bool isFailing = false;
    int AcctiveZombieIndex = 0;
    Vector2 startPosition;
    [SerializeField]
    float riseSpeed;
    [SerializeField]
    float top_limit;
    [SerializeField]
    int zombies_Smashed;
    [SerializeField]
    int livesRemaining;
    bool GAME_OVER;
    // Start is called before the first frame update
    void Start()
    {
        pickNewZombie();
        zombies_Smashed = 0;
        GAME_OVER = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GAME_OVER) {
            revisaZombiees();
        }

    }
    public void revisaZombiees (){
        if (isRaising)
        {
            if (zombies[AcctiveZombieIndex].transform.position.y - startPosition.y >= top_limit)
            {
                isRaising = false;
                isFailing = true;
            }
            else
            {
                zombies[AcctiveZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
            }
        }
        else if (isFailing)
        {
            if (zombies[AcctiveZombieIndex].transform.position.y - startPosition.y <= 0)
            {
                isRaising = false;
                isFailing = false;
                livesRemaining--;// Si no mata zombie baja vida
                if (livesRemaining == 0)
                {
                    GAME_OVER = true;
                }
            }
            else
            {
                zombies[AcctiveZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
            }
        }
        else
        {
            zombies[AcctiveZombieIndex].transform.position = startPosition;
            pickNewZombie();
        }

    }

    private void pickNewZombie() {
        isRaising = true;
        isFailing = false;
        AcctiveZombieIndex = Random.Range(0, zombies.Length);
        startPosition = zombies[AcctiveZombieIndex].transform.position;
    
    }
    public void killEnmey() {
        zombies_Smashed++;
        zombies[AcctiveZombieIndex].transform.position = startPosition;
        pickNewZombie();
    }
}
