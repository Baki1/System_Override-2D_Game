using UnityEngine;
using System.Collections;

public class SistemYoneticisi : MonoBehaviour
{
    [Header("UI ve Ekranlar")]
    public GameObject dogaEkrani;       // Arkadaşının yaptığı ormanlı "Sistem Stabil" ekranı
    public GameObject glitchMenuEkrani; // Senin pikselli START, OPTIONS butonlarını tutan paket
    public GameObject glitchVolume;     // Ekranı titreten Global Volume efekti

    [Header("Oyuncu (Virüs)")]
    public Rigidbody2D oyuncuRb;
    public float dususYercekimi = 9f;   // Karakterin gülle gibi düşmesi için

    private bool menudeMiyiz = false;

    void Start()
    {
        // AŞAMA 1: DOĞA (Huzur)
        // Oyun başlar başlamaz sadece doğa ekranı görünür.
        dogaEkrani.SetActive(true);
        glitchMenuEkrani.SetActive(false);
        glitchVolume.SetActive(false);

        // Karakter havada donmuş halde bekler.
        oyuncuRb.gravityScale = 0f;
        oyuncuRb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        // AŞAMA 2: VİRÜSÜN DOKUNUŞU (Herhangi bir tuşa basılırsa)
        if (!menudeMiyiz && dogaEkrani.activeSelf && Input.anyKeyDown)
        {
            StartCoroutine(SistemiBozVeMenuyuGetir());
        }
    }

    IEnumerator SistemiBozVeMenuyuGetir()
    {
        menudeMiyiz = true;

        // Doğa gider, Glitch patlar (Arkadaşının iğrenç sesini de buraya koyabilirsin)
        dogaEkrani.SetActive(false);
        glitchVolume.SetActive(true);

        yield return new WaitForSeconds(0.5f); // 0.5 saniye sistem can çekişir

        // AŞAMA 3: MEMORY LEAK MENÜSÜ
        // Glitch biter, senin siyah ekranlı START menün gelir. Karakter HALA havada.
        glitchVolume.SetActive(false);
        glitchMenuEkrani.SetActive(true);
    }

    // AŞAMA 4: OYUNA DÜŞÜŞ (Bu metodu START butonunun OnClick veya OnMouseDown eventine bağla)
    public void OyunuBaslat()
    {
        StartCoroutine(DususSekansi());
    }

    IEnumerator DususSekansi()
    {
        // Menü kaybolur, son bir glitch tokatı yeriz
        glitchMenuEkrani.SetActive(false);
        glitchVolume.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        // Glitch biter, YERÇEKİMİ AÇILIR! Virüs sisteme düşer.
        glitchVolume.SetActive(false);
        oyuncuRb.gravityScale = dususYercekimi;
    }
}