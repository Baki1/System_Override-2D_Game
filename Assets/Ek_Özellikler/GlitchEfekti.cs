using UnityEngine;
using System.Collections;

public class GlitchEfekti : MonoBehaviour
{
    [Header("Glitch Ayarları")]
    public GameObject systemYazisi; // Yanıp sönecek olan yazı/resim objesi
    public float minBekleme = 0.05f; // Çok hızlı kırpışma
    public float maxBekleme = 0.3f;  // Daha uzun duraksama

    void Start()
    {
        // Oyun başladığı an döngüyü tetikle
        StartCoroutine(BozukDöngü());
    }

    IEnumerator BozukDöngü()
    {
        while (true) // Sonsuza kadar devam et
        {
            // Rastgele bir süre bekle (Örn: 0.1 saniye)
            yield return new WaitForSeconds(Random.Range(minBekleme, maxBekleme));

            // Eğer objemiz varsa, durumunu tam tersine çevir (Açıksa kapat, kapalıysa aç)
            if (systemYazisi != null)
            {
                systemYazisi.SetActive(!systemYazisi.activeSelf);
            }
        }
    }
}