using UnityEngine;

public class DeathLaser : MonoBehaviour
{
    public Transform target;
    public GameObject laserPrefab;
    public GameObject laserCollider;
    
    public bool isLaserOn;
    public bool laserMove;

    private void Start()
    {
        laserCollider.SetActive(false);
    }

    private void Update()
    {
        if(laserMove)
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);

        if (isLaserOn)
        {
            laserPrefab.SetActive(true);
            return;
        }
        laserPrefab.SetActive(false);
        laserMove = true;
    }

    public void stopMovingLaser()
    {
        laserMove = false;
    }

    public void stopLaser()
    {
        isLaserOn = false;
    }

    public void activateCollider()
    {
        laserCollider.SetActive(true);
    }

    public void deactivateCollider()
    {
        laserCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if(other.CompareTag("Player"))
        {
            MusguerianKnight knight = other.gameObject.GetComponent<MusguerianKnight>();
            knight?.TakeDamage();
            print("Laser Damage");
        }
    }
}
