using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OyunSonuYoneticisi : MonoBehaviour
{
    [Header("Resim Bağlantıları")]
    public GameObject resetResmi;   // İlk çıkacak resim (Sistem Sıfırlanıyor)
    public GameObject stabilResmi;  // İkinci çıkacak resim (Sistem Stabil)

    [Header("Zaman Ayarları")]
    public float resetSuresi = 2.5f;  // İlk resim ekranda kaç saniye kalsın?
    public float stabilSuresi = 2.5f; // İkinci resim ekranda kaç saniye kalsın?

    private bool oyunBittiMi = false;

    // Karakter bitiş çizgisine (Trigger'a) girdiğinde tetiklenir
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !oyunBittiMi)
        {
            oyunBittiMi = true;

            // 1. OYUNCUYU DONDUR (Aşağı düşmeye veya yürümeye devam etmesin)
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static; // Fizik motorundan tamamen kopar
            }

            // 2. Kapanış Sekansını (Zamanlayıcıyı) Başlat
            StartCoroutine(KapanisSekansi());
        }
    }

    IEnumerator KapanisSekansi()
    {
        // --- 1. AŞAMA: SİSTEMİ SIFIRLAMA (Kırmızı/Sarı Ekran) ---
        if (resetResmi != null)
        {
            resetResmi.SetActive(true);
        }

        // Jürinin yazıyı okuması için belirlediğimiz süre kadar bekle
        yield return new WaitForSeconds(resetSuresi);

        // --- 2. AŞAMA: SİSTEM STABİL (Yeşil Ekran) ---
        // Önce eski resmi kapatıyoruz ki arkada kalabalık yapmasın
        if (resetResmi != null) resetResmi.SetActive(false);

        // Sonra stabil resmini açıyoruz
        if (stabilResmi != null)
        {
            stabilResmi.SetActive(true);
        }

        // Tekrar bekle
        yield return new WaitForSeconds(stabilSuresi);

        // --- 3. AŞAMA: ANA MENÜYE DÖNÜŞ ---
        // Sistem tamamen temizlendi, oyunu en başa (Build Index 0) sar!
        SceneManager.LoadScene(0);
    }
}