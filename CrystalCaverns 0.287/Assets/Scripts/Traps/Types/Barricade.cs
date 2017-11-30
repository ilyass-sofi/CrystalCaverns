using UnityEngine;

public class Barricade : MonoBehaviour
{
    [Header("Trap Stats")]
    [Tooltip("The initial life of the trap")]
    [SerializeField] private float life;

    [Header("Trap Current Stats")]
    [Tooltip("The current life of the trap")]
    [SerializeField] private float currentLife;

    /// <summary>
    /// 
    /// </summary>

    void Awake()
    {
        //Here we will catch the sound component and all the components we will need 
    }

    /// <summary>
    /// 
    /// </summary>

    void Start()
    {
        currentLife = life;
        //Here will be the sound that it make this trap
    }

    /// <summary>
    /// 
    /// </summary>

    void Update()
    {
        Destroy();
    }

    /// <summary>
    /// 
    /// </summary>

    private void Destroy() // The trap is destroy when his currentLife is equal or less than 0
    {
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
