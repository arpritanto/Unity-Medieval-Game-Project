using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource; //Deklarasi audioSource dalam bentuk AudioSource
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Mengambil komponen   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
