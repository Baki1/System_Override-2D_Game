using UnityEngine;
using System.Collections;

public class BaslangicMotoru : MonoBehaviour
{
    [Header("Şalter Bağlantıları")]
    public PlayerMovement oyuncuKodu;
    public Rigidbody2D oyuncuRb;
    public KameraEsnekTakip kameraTakipKodu;

    [Header("Arayüz ve İpucu Ayarları")]
    public GameObject startYazisiObjesi; // Havada kalan o START yazısı
    public GameObject ipucuResmi;        // Ekrana çıkacak ve yanıp sönecek PNG resmi
    public float ipucuGecikmeSuresi = 4f; // Kaç saniye sonra resim ilk kez çıksın?
    public float yanipSonmeHizi = 0.4f;  // Resmin yanıp sönme hızı (Saniye)

    public float dususYercekimi = 9f;
    private int tiklamaSayisi = 0;

    void Start()
    {
        oyuncuKodu.enabled = false;
        if (kameraTakipKodu != null) kameraTakipKodu.enabled = false;

        oyuncuRb.gravityScale = 0f;
        oyuncuRb.linearVelocity = Vector2.zero;

        // Oyun başında ipucu resmini gizle
        if (ipucuResmi != null) ipucuResmi.SetActive(false);
    }

    void OnMouseDown()
    {
        tiklamaSayisi++;

        if (tiklamaSayisi == 3)
        {
            SistemiCokertVeDus();
        }
    }

    void SistemiCokertVeDus()
    {
        oyuncuKodu.enabled = true;
        if (kameraTakipKodu != null) kameraTakipKodu.enabled = true;
        oyuncuRb.gravityScale = dususYercekimi;

        // Kutunun resmini ve çarpışmasını kapatıyoruz
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;

        // Havada kalan START yazısını kapatıyoruz
        if (startYazisiObjesi != null) startYazisiObjesi.SetActive(false);

        // Zamanlayıcı döngüsünü başlatıyoruz
        StartCoroutine(IpucuResminiGetirVeYakit());
    }

    IEnumerator IpucuResminiGetirVeYakit()
    {
        // 1. AŞAMA: Oyuncu düştükten sonra belirlenen süre kadar (örn: 4 saniye) sessizce bekle
        yield return new WaitForSeconds(ipucuGecikmeSuresi);

        // 2. AŞAMA: Süre dolunca resmi ilk kez görünür yap
        if (ipucuResmi != null)
        {
            ipucuResmi.SetActive(true);
        }

        // 3. AŞAMA: SONSUZ YANIP SÖNME DÖNGÜSÜ
        // Kod buraya ulaştığında bir daha asla durmayacak bir döngüye girer
        while (true)
        {
            // Belirlediğin yanıp sönme hızı kadar (örn: 0.4 saniye) bekle
            yield return new WaitForSeconds(yanipSonmeHizi);

            if (ipucuResmi != null)
            {
                // Resmin o anki aktiflik durumunu tam tersine çevir!
                // Açıksa kapatır, kapalıysa açar. Böylece kusursuz bir yanıp sönme oluşur.
                ipucuResmi.SetActive(!ipucuResmi.activeSelf);
            }
        }
    }
}