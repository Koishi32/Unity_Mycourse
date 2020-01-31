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
    // Start is called before the first frame update
    void Start()
    {
        pickNewZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaising)
        {
            if (zombies[AcctiveZombieIndex].transform.position.y - startPosition.y >= top_limit)
            {
                isRaising = false;
                isFailing = true;
            }
            else {
                zombies[AcctiveZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
            }
        }
        else if (isFailing)
        {
            if (zombies[AcctiveZombieIndex].transform.position.y - startPosition.y <= 0)
            {
                isRaising = false;
                isFailing = false;
            }
            else {
                zombies[AcctiveZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
            }
        }
        else {
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
}
