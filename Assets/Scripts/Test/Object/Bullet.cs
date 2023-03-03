using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Plane>())
        {
            gameObject.SetActive(false);
            Invoke("DestroyObj", 5f);
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
