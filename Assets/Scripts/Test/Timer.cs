using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _maxTimer;
    [SerializeField] private TextMeshProUGUI _textDynamic;

    public float MaxTimer
    {
        get => _maxTimer;
        set => _maxTimer = value;
    }

    public TextMeshProUGUI TextDynamic => _textDynamic;


    private void Start()
    {
        _textDynamic.text = _maxTimer.ToString("F");
    }
    
    public void StartTimer()
    {
        _maxTimer -= Time.deltaTime;
        _textDynamic.text = _maxTimer.ToString("F");
    }
}
