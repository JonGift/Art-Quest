using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationOverlord : MonoBehaviour
{
    public List<GameObject> notifications;
    public GameObject target;

    public float frequency = 9f;
    public float speedIncrease = .16f;
    public float cap = .5f;
    public float speedReducer = .75f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnNotifications", 10f);
        target = Camera.main.gameObject;
    }

    void SpawnNotifications() {
        int choice = Random.Range(0, notifications.Count);
        Vector3 randomPos = new Vector3(Random.Range(-1.3f, 1.3f), Random.Range(.3f, 2.2f), Random.Range(-1.3f, 1.7f));

        GameObject temp = Instantiate(notifications[choice], randomPos, Quaternion.identity);
        temp.GetComponent<NotificationController>().UpdateTarget(target);
        frequency -= speedIncrease;
	speedIncrease *= speedReducer;
        if (frequency < cap)
            frequency = cap;
        Invoke("SpawnNotifications", frequency);
    }
}
