using UnityEngine;

public class AnaMenuOptions : MonoBehaviour
{
    // FARE İLE TIKLANDIĞINDA ÇALIŞIR (BoxCollider2D şarttır!)
    void OnMouseDown()
    {
        // Ölümsüz müzik motorumuzu sahnede bul
        MuzikMotoru motor = FindObjectOfType<MuzikMotoru>();

        if (motor != null)
        {
            AudioSource hoparlor = motor.GetComponent<AudioSource>();
            if (hoparlor != null)
            {
                // Sesi döngüye sok: Tam ses ise sustur, sessizse yarım yap, yarım ise tam yap!
                if (hoparlor.volume >= 1f)
                {
                    hoparlor.volume = 0f; // Sesi tamamen kapat
                }
                else if (hoparlor.volume == 0f)
                {
                    hoparlor.volume = 0.5f; // Sesi %50 aç
                }
                else
                {
                    hoparlor.volume = 1f; // Sesi %100 aç
                }
            }
        }
    }
}