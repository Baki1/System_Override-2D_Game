using UnityEngine;
using UnityEngine.SceneManagement; // Sahne değiştirmek için zorunlu kütüphane

public class BolumGecisButonu : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer butona çarpan şey "Player" etiketli oyuncumuz ise...
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unity'nin Build ayarlarındaki sıraya göre BİR SONRAKİ sahneyi yükle (+1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}