using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public Animator exp;
    SpriteRenderer spr;
    AudioSource snd;
    // Start is called before the first frame update
    void Start()
    {
        exp = GetComponent<Animator>();
        //exp.Play("Explosion", );
        //exp.Play("Explosion");
        //exp.wrapMode = WrapMode.Once;
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;

        snd = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode()
    {
        exp.SetTrigger("Explode");
        spr.enabled = true;
        //exp.Play("Explosion");
        snd.Play();
    }
}
