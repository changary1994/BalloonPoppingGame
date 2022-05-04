using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    const int NUM_BALLOONS = 5;
    [SerializeField] GameObject balloon;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBalloon();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void SpawnBalloon()
    {

        int xMin = -6;
        int xMax = 50;
        int yMin = 9;
        int yMax = 26;

        for (int i = 0; i < NUM_BALLOONS; i++)
        {
            Vector2 position = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            Vector3 flipped = new Vector3(0, 180, 0);
            if (Random.value < 0.5f)
            {
                Instantiate(balloon, position, transform.rotation * Quaternion.Euler(0, 180, 0));
            }
            else
            {
                Instantiate(balloon, position, Quaternion.identity);
            }
       

        }
    }

}
