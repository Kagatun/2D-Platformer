using UnityEngine;

public class SpawnPointCoin : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    void Start()
    {
        Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
        coin.transform.position = transform.position;
    }
}
