using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider energySlider;

    public GameObject PausePanel;
    public GameObject SavePanel;
    public static bool GameIsPaused = false;

    public Health PlayerHealth;
    public Health EnemyHealth;
    public Player playerInstance;
    public int healthPlayer;
    public int healthEnemy;
    public float delay = 3;
    float timer;

    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject WinnerScene;

    private void Start()
    {
        SetMaxHealth(Health.maxHealth);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        SetHealth();
        ShowPauseMenu();
        gameOver();
        Winner();

        healthEnemy = EnemyHealth.health;
        Debug.Log(healthEnemy);
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth()
    {
        healthSlider.value = PlayerHealth.health;
    }

    public void SetMaxEnergy(float energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    public void SetEnergy(float energy)
    {
        energySlider.value = energy;
    }

    private void ShowPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ContinueSave()
    {
        SavePanel.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void gameOver()
    {
        healthPlayer = PlayerHealth.health;
        if (healthPlayer < 1) 
        {
            timer+= Time.deltaTime;
            if (timer>delay)
            {
                GameOverMenu.SetActive(true);
                GameIsPaused = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;  
            }
        }
    }

    public void Winner()
    {
        healthEnemy = EnemyHealth.health;
        if (healthEnemy < 1) 
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                WinnerScene.SetActive(true);
                GameIsPaused = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}