using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody rb;
    private bool focused = false;
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set
        {
            target = value;
            focused = true;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.gameObject.GetComponent<Character>().Health = -damage;
            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()
    {
        if (focused)
        {
            if (target)
            {   
                transform.LookAt(target.transform);
                rb.velocity = transform.forward * speed;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}