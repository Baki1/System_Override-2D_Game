using UnityEngine;
using System.Collections;

public class SistemAcilisTetikleyici : MonoBehaviour
{
    [Header("Bağlantılar")]
    public GameObject anaMenuPaketi;
    public Rigidbody2D oyuncuRb;
    public GameObject glitchVolume;

    [Header("Ayarlar")]
    public float dususYercekimi = 9f;

    // YENİ: Tıklama sayısını aklında tutacak değişken
    private int tiklamaSayisi = 0;

    void Start()
    {
        // 1. Oyun başladığında karakterin yerçekimini SIFIRLA. Havada asılı kalsın.
        oyuncuRb.gravityScale = 0f;
        oyuncuRb.linearVelocity = Vector2.zero;

        // Glitch kapalı, menü açık başlar
        glitchVolume.SetActive(false);
        if (anaMenuPaketi != null) anaMenuPaketi.SetActive(true);
    }

    // Bu fonksiyon objenin üzerindeki Collider'a tıklandığında çalışır
    void OnMouseDown()
    {
        tiklamaSayisi++; // Her tıklamada sayacı 1 artır

        if (tiklamaSayisi == 3)
        {
            // 3. tıklamada sistemi çökert
            StartCoroutine(SistemiCokert());
        }
        else
        {
            // Mühendislik Dokunuşu: İlk iki tıklamada oyuncuya bir şeylerin bozulduğunu hissettir.
            // Buraya ufak bir hata sesi veya titreme eklenebilir. 
            Debug.Log("Sistem zorlanıyor... Tıklama: " + tiklamaSayisi);
        }
    }

    IEnumerator SistemiCokert()
    {
        // Menüyü yok et, Glitch efektini patlat!
        if (anaMenuPaketi != null) anaMenuPaketi.SetActive(false);
        glitchVolume.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        // Glitch'i kapat ve karakterin yerçekimini aç!
        glitchVolume.SetActive(false);
        oyuncuRb.gravityScale = dususYercekimi;
    }
}