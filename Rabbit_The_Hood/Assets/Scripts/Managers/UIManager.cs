using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text treasuresFoundText;
    [SerializeField] private PlayerPickup playerPickup;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private Button play;
    [SerializeField] private Button quit;
    [SerializeField] private Button credits;

    private void Awake()
    {
        mainMenuPanel.SetActive(true);
        inGamePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        creditsPanel.SetActive(false);
        Time.timeScale = 0f;
    }
    public void Play()
    {
        Debug.Log("play");
        mainMenuPanel.SetActive(false);
        inGamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
          Time.timeScale = 1f;
    }
    public void Credits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    public void GameWon()
    {
        Cursor.lockState = CursorLockMode.None;
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void GameLost()
    {
        Cursor.lockState = CursorLockMode.None;
        inGamePanel.SetActive(false);
        losePanel.SetActive(true); 
    }
    public void RetryGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        treasuresFoundText.text = "Treasures found: 0 / 2";
    }

    public void UpdateText()
    {
        if(playerPickup.unlockedChestCollected || playerPickup.lockedChestCollected)
        {
            treasuresFoundText.text = "Treasures found: 1 / 2";
        }
        if (playerPickup.unlockedChestCollected && playerPickup.lockedChestCollected)
        {
            treasuresFoundText.text = "Treasures found: 2 / 2";
        }
    }
}
