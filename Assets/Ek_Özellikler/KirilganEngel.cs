using UnityEngine;

public class KirilganEngel : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D temas)
    {
        if (temas.gameObject.name == "Player")
        {
            // Eğer karakterimiz ağır formdaysa ve hızla düşüyorsa...
            if (temas.rigidbody.mass > 5f && temas.relativeVelocity.y < -5f)
            {
                // SİSTEME MÜDAHALE: Sahnede "Main Camera" etiketli objeyi bul, 
                // içindeki "KameraSarsinti" kodunu al.
                KameraSarsinti kamera = Camera.main.GetComponent<KameraSarsinti>();

                // Eğer kod başarıyla bulunduysa sarsıntıyı başlat (Süre: 0.2 saniye, Şiddet: 0.4)
                if (kamera != null)
                {
                    kamera.StartCoroutine(kamera.SarsintiYap(0.2f, 0.4f));
                }

                // Son olarak engeli (kendini) sahnede yok et!
                Destroy(gameObject);
            }
        }
    }
}