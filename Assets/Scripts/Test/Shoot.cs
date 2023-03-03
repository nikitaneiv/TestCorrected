using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private BulletInUI _bullet;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Timer _timer;

    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float height;
    [SerializeField] private float duration;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 endPoint;
    private bool hasEndPoint;
    private float elapsedTime;
    private bool timerIsActive = false;

    private int bullet = 5;
    private float _currentDelay;

    public int Bullet
    {
        get { return bullet; }
        set
        {
            if (value <= 5) bullet = value;
            _bullet.Refresh();
        }
    }

    private void Start()
    {
        StartCoroutine(AddBullet());
    }

    private void Update()
    {
        Shot();
        TimerIsActive();
    }

    private IEnumerator AddBullet()
    {
        for (;;)
        {
            Bullet++;
            yield return new WaitForSeconds(1f);
            if (Bullet == 5)
            {
                timerIsActive = false;
            }
        }
    }

    private void Shot()
    {
        DecrementTimer(Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && CanShoot() && Bullet != 0)
        {
            Bullet--;
            timerIsActive = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject ball = Instantiate(_bulletPrefab, startPoint.position, Quaternion.identity);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                endPoint = hit.point;
                SetDelay();
                StartCoroutine(MoveBall(ball.transform, endPoint, duration));
            }
        }
    }

    IEnumerator MoveBall(Transform ballTransform, Vector3 targetPosition, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            ballTransform.position = CalculatePosition(startPoint.position, endPoint, height, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ballTransform.position = targetPosition;
    }

    private Vector3 CalculatePosition(Vector3 startPoint, Vector3 endPoint, float height, float t)
    {
        float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
        float y = Mathf.Lerp(startPoint.y, endPoint.y, t) + height * Mathf.Sin(Mathf.PI * t);
        float z = Mathf.Lerp(startPoint.z, endPoint.z, t);
        return new Vector3(x, y, z);
    }

    private void DecrementTimer(float deltaTime) =>
        _currentDelay -= deltaTime;

    private bool CanShoot() =>
        _currentDelay <= 0f;

    private void SetDelay() =>
        _currentDelay = _shootDelay;

    private void TimerIsActive()
    {
        if (timerIsActive)
        {
            _timer.StartTimer();

            if (_timer.MaxTimer <= 0)
            {
                Bullet++;
                timerIsActive = false;
                _timer.MaxTimer = 1f;
                _timer.TextDynamic.text = _timer.MaxTimer.ToString("F");
            }
        }
    }
}
