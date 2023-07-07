using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MenuButtonController menuButtonController; //Objek menuButtonController
    void PlaySound(AudioClip whichSound) //Fungsi PlaySound dengan Parameter AudioClip
    {
        menuButtonController.audioSource.PlayOneShot(whichSound); //Memutar AudioClip
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
