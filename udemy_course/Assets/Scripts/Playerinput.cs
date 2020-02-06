using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public AudioClip[] SmashSounds;
     AudioSource FuenteAudio;
    public GameObject Blood_efect;
    // Start is called before the first frame update
    void Start()
    {
        FuenteAudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null) {
                if (hit.collider.tag == "Zombie")
                {
                    FuenteAudio.PlayOneShot(SmashSounds[Random.Range(0,SmashSounds.Length)],0.5f);
                    gameObject.GetComponent<GameMa>().killEnmey();
                    animacion(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
        
    }
    public void animacion(Vector2 posicion) {
        Blood_efect.transform.position = posicion;
        Blood_efect.GetComponent<Animator>().SetTrigger("Smash");
    }
}
