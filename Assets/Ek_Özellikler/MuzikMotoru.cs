using UnityEngine;

public class MuzikMotoru : MonoBehaviour
{
    private static MuzikMotoru instance;

    void Awake()
    {
        // Eğer sahnede zaten bir müzik motoru varsa, yenisini yok et (çift ses çıkmasın)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Eğer bu ilk müzik motoruysa, onu korumaya al ve sahneler arası geçişte silinmesini engelle!
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}