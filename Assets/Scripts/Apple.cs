using UnityEngine;

public class Apple : MonoBehaviour
{
    public int HealingPoints { get; private set; } = 45;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
