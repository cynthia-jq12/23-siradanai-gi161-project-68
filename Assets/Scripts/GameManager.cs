using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ตัวแปรนี้ทำให้ Player เรียกใช้ได้ง่ายๆ

    [Header("UI References")]
    public Text scoreText;       // ลาก Text คะแนนมาใส่
    public GameObject gameOverPanel; // ลากหน้าต่างจบเกมมาใส่

    public int Score { get; private set; }

    void Awake()
    {
        // Singleton Pattern: ทำให้มี GameManager ได้แค่ตัวเดียวในเกม
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        if (scoreText) scoreText.text = $"Relics: {Score}";
    }

    public void TriggerGameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true); // เปิดหน้าต่างจบเกม
        Time.timeScale = 0; // หยุดเวลาเกม
    }

    // เอาฟังก์ชันนี้ไปใส่ปุ่ม Restart ใน Canvas
    public void RestartGame()
    {
        Time.timeScale = 1; // คืนเวลาให้เดินปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดฉากเดิมใหม่
    }
}