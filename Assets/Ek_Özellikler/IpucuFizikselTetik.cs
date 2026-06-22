using UnityEngine;
using System.Collections;

public class IpucuFizikselTetik : MonoBehaviour
{
    [Header("Resim Bağlantısı")]
    public GameObject ipucuResmi; // Oyun dünyasındaki kapalı PNG (Sprite)

    [Header("Yanıp Sönme Ayarları")]
    public float yanipSonmeHizi = 0.4f; // Kırpışma hızı (0.2 çok hızlı, 0.8 yavaş)

    private bool acildiMi = false;

    void Start()
    {
        // Oyun başında resmi kesinlikle gizle
        if (ipucuResmi != null) ipucuResmi.SetActive(false);
    }

    // Karakter görünmez ağa (Trigger) değdiği an...
    void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece oyuncu çarptığında ve daha önce açılmadıysa çalışır
        if (other.gameObject.CompareTag("Player") && !acildiMi)
        {
            acildiMi = true; // Sistemin bir daha tetiklenmesini engelle

            // Resmi açmakla kalma, yanıp sönme zamanlayıcısını başlat!
            if (ipucuResmi != null)
            {
                StartCoroutine(YanipSonmeVeAcilisDongusu());
            }

            Debug.Log("FİZİKSEL AĞA ÇARPILDI: RESİM AÇILDI VE YANIP SÖNÜYOR!");
        }
    }

    IEnumerator YanipSonmeVeAcilisDongusu()
    {
        // 1. AŞAMA: Çarpar çarpmaz resmi ilk kez görünür yap
        ipucuResmi.SetActive(true);

        // 2. AŞAMA: SONSUZ YANIP SÖNME DÖNGÜSÜ
        while (true)
        {
            // Belirlediğin hız kadar (örn: 0.4 saniye) bekle
            yield return new WaitForSeconds(yanipSonmeHizi);

            // Resmin aktiflik durumunu tam tersine çevir (Açıksa kapat, kapalıysa aç)
            if (ipucuResmi != null)
            {
                ipucuResmi.SetActive(!ipucuResmi.activeSelf);
            }
        }
    }
}