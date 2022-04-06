using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationOverlord : MonoBehaviour
{
    public List<GameObject> notifications;
    public GameObject target;

    public float frequency = 9f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnNotifications", 0f);
        target = Camera.main.gameObject;
    }

    void SpawnNotifications() {
        int choice = Random.Range(0, notifications.Count);
        Vector3 randomPos = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(.1f, 2.3f), Random.Range(-1.5f, 1.9f));

        GameObject temp = Instantiate(notifications[choice], randomPos, Quaternion.identity);
        temp.GetComponent<NotificationController>().UpdateTarget(target);
        frequency -= .125f;
        if (frequency < 1f)
            frequency = 1f;
        Invoke("SpawnNotifications", frequency);
    }
}
