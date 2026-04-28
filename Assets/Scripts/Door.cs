using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject areaToSpawn;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenDoor");

            areaToSpawn.SetActive(true);
        }
    }
}
