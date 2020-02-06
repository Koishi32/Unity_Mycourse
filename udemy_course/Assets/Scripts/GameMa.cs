using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    //[SerializeField]
    int livesRemaining; //De preferencia 3
    bool GAME_OVER;
    public Button botonReinicio;
    public Image vidallena1;
    public Image vidallena2;
    public Image vidallena3;
    public Text puntachos;
    // Start is called before the first frame update
    void Start()
    {
        pickNewZombie();
        livesRemaining = 3;
        zombies_Smashed = 0;
        puntachos.text = "0";
        GAME_OVER = false;
        botonReinicio.gameObject.SetActive(false);
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
                ActualizaUI();
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
    void ActualizaUI() {
        switch (livesRemaining) {
            case 3:
                vidallena1.gameObject.SetActive(true);
                vidallena2.gameObject.SetActive(true);
                vidallena3.gameObject.SetActive(true);
                break;
            case 2:
                vidallena3.gameObject.SetActive(false);
                break;
            case 1:
                vidallena2.gameObject.SetActive(false);
                break;
            case 0:
                vidallena1.gameObject.SetActive(false);
                GAME_OVER = true;
                botonReinicio.gameObject.SetActive(true);
                break;
        
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
        aumentaVel();
        puntachos.text = zombies_Smashed.ToString();
        zombies[AcctiveZombieIndex].transform.position = startPosition;
        pickNewZombie();
    }
    [SerializeField]
     int ScoreThershold;
    void aumentaVel() {
        if (zombies_Smashed >= ScoreThershold) {
            riseSpeed++;
            ScoreThershold *= 2;
        }
    }
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Main_Menu() {
        SceneManager.LoadScene(0);
    }
}
