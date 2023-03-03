using UnityEngine;

public class BulletInUI : MonoBehaviour
{
    public Transform[] _bullet = new Transform[5];
    private Shoot _shoot;

    private void Awake()
    {
        AddBullet();
    }

    private void AddBullet()
    {
        _shoot = FindObjectOfType<Shoot>();
        for (int i = 0; i < _bullet.Length; i++)
        {
            _bullet[i] = transform.GetChild(i);
        }
    }
    
    public void Refresh()
    {
        for (int i = 0; i < _bullet.Length; i++)
        {
            if (i < _shoot.Bullet) _bullet[i].gameObject.SetActive(true);
            else _bullet[i].gameObject.SetActive(false);
        }
    }
}
