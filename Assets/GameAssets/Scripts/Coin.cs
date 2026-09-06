using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.Instance.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
