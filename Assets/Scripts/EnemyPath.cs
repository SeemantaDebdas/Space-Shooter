using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIdx = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIdx].transform.position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIdx <= waypoints.Count - 1)
        {
            if(transform.position != waypoints[waypointIdx].transform.position)
                transform.position = Vector2.MoveTowards(this.transform.position, 
                                                        waypoints[waypointIdx].transform.position,
                                                        waveConfig.MoveSpeed * Time.deltaTime);
            else 
                waypointIdx++;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
