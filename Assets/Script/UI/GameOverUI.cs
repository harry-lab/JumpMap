using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel; // 게임 오버 패널
    public Button retryButton; // 리트라이 버튼
    private bool isGameOver = false; // 게임 오버 상태 확인

    private void Start()
    {
        gameOverPanel.SetActive(false); // 시작 시 비활성화
        retryButton.onClick.AddListener(RetryGame);
    }

    // 게임 오버 UI 활성화
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true; // 게임 오버 상태 활성화
    }

    // 씬 다시 로드
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        // 게임 오버 상태일 때 아무 키나 누르면 씬 다시 시작
        if (isGameOver && Input.anyKeyDown)
        {
            RetryGame();
        }
    }
}

