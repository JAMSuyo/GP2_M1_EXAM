using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public float boostDuration = 5f; 
    public float speedMultiplier = 2f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            
            ThirdPersonCamera playerMovement = other.GetComponent<ThirdPersonCamera>();
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost(boostDuration, speedMultiplier); 
            }

            Destroy(gameObject);
        }
    }
}
