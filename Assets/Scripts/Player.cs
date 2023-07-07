using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health;
    public int health;
    public GameObject playerRig;
    public GameObject checkpoint;
    public GameObject SavePanel;
    public GameObject PausePanel;
    public CharacterController characterController;
    public float x, y, z;

    private void Update()
    {
        health = Health.health;
        Debug.Log(health);
    }

    void Start()
    {
        if (SceneLoader.loadStatus)
        {
            LoadPlayer();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

        string path = Application.persistentDataPath + "/player.pemgame";
        Debug.Log("Save berhasil : " + path);
        PausePanel.SetActive(false);
        SavePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Health.health = data.health;

        Vector3 position = new Vector3();
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        
        transform.position = position;

        characterController.enabled = false;
        playerRig.transform.position = position;
        characterController.enabled = true;
        Debug.Log(position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            checkpoint.SetActive(true);
        }
    }
}
