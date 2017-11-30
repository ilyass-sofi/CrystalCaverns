using UnityEngine;

public class TextDamage : MonoBehaviour
{
    private float scale = 0.5f;
    private float scaleSpeed = 0.4f;
    private Vector3 randomDirection;
    private float destroyTime = 1.1f;

    void Start ()
    {   
        //Each text takes a different direction (left or right) on a different up speed
        randomDirection = new Vector3(Random.Range(-0.005f, 0.005f), Random.Range(0.001f, 0.01f) , 0);
        //Destroys after a time
        Destroy(gameObject, destroyTime);
    }
	
	void Update ()
    {
        if (Time.timeScale != 0)
        {
            //Scale it up 
            scale -= scaleSpeed * Time.deltaTime;
            transform.localScale = new Vector3(scale, scale, scale);
            //Move it on that random direction
            transform.Translate(randomDirection);
            //Look at the player (rotation is necessary)
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }

  
}
