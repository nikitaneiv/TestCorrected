using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private TextMeshPro _scoreText;

    private int _score = 0;

    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public TextMeshPro ScoreText => _scoreText;


    private void Awake()
    {
        _scoreText.text = Score.ToString();
        InstantiateEnemy();
    }

    public void InstantiateEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f)); 
        
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Enemy, pos, Quaternion.identity);
        }
    }
    
    
}
