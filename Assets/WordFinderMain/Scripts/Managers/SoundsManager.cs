using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource levelCompliteSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource letterAddedSound;
    [SerializeField] private AudioSource letterRemovedSound;
    [SerializeField] private AudioSource backgroundSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        InputManager.onLetterAdded += PlayLetterAddedSound;
        InputManager.onLetterRemoved += PlayLetterRemovedSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        InputManager.onLetterAdded -= PlayLetterAddedSound;
        InputManager.onLetterRemoved -= PlayLetterRemovedSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1.0f;
        levelCompliteSound.volume = 1.0f;
        gameOverSound.volume = 1.0f;
        letterAddedSound.volume = 1.0f;
        letterRemovedSound.volume = 1.0f;
    }

    public void DisnableSounds()
    {
        buttonSound.volume = 0f;
        levelCompliteSound.volume = 0f;
        gameOverSound.volume = 0f;
        letterAddedSound.volume = 0f;
        letterRemovedSound.volume = 0f;
    }

    public void PlayBackgroundSound()
    {
        backgroundSound.volume = 1f;
    }

    public void StopBackgroundSound()
    {
        backgroundSound.volume = 0f;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                PlayLevelCompleteSound();
                break;

            case GameState.GameOver:
                PlayGameOverSound();
                break;
        }
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    private void PlayLetterAddedSound()
    {
        letterAddedSound.Play();
    }

    private void PlayLetterRemovedSound()
    {
        letterRemovedSound.Play();
    }

    private void PlayLevelCompleteSound()
    {
        levelCompliteSound.Play();
    }

    private void PlayGameOverSound()
    {
        gameOverSound.Play();
    }
}
