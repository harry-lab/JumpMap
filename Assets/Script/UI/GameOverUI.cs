using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel; // ���� ���� �г�
    public Button retryButton; // ��Ʈ���� ��ư
    private bool isGameOver = false; // ���� ���� ���� Ȯ��

    private void Start()
    {
        gameOverPanel.SetActive(false); // ���� �� ��Ȱ��ȭ
        retryButton.onClick.AddListener(RetryGame);
    }

    // ���� ���� UI Ȱ��ȭ
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true; // ���� ���� ���� Ȱ��ȭ
    }

    // �� �ٽ� �ε�
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        // ���� ���� ������ �� �ƹ� Ű�� ������ �� �ٽ� ����
        if (isGameOver && Input.anyKeyDown)
        {
            RetryGame();
        }
    }
}

