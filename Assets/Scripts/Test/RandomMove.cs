using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class RandomMove : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private GameManager _manager;

    private float duration = 1.5f;
    private float durationForLook = 1f;
    private bool snapping = false;

    private void Start()
    { 
        _manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        StartMove();
    }

    private void StartMove()
    {
        if (transform.position == _target.transform.position)
        {
            CubeMove();
        }
    }

    public void CubeMove()
    {
        _target.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));

        transform.DOLookAt(_target.transform.position, durationForLook, AxisConstraint.None, Vector3.up).OnComplete(
            () =>
                transform.DOMove(_target.transform.position, duration, snapping).SetEase(Ease.Linear));
        StartCoroutine(BoostCube());
    }

    private IEnumerator BoostCube()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        duration = 0.5f;
        yield return new WaitForSeconds(1);
        duration = 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            _manager.Score++;
            _manager.ScoreText.text = _manager.Score.ToString();
            gameObject.SetActive(false);
            Invoke("ActiveGameObj", Random.Range(1, 3));
        }
    }
    private void ActiveGameObj()
    {
        gameObject.SetActive(true);
    }
    
}