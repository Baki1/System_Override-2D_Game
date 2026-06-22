using System.Collections;
using UnityEngine;

// 1. KURAL: Güvenlik. Bu kodun çalışması için bir SpriteRenderer'a ihtiyacı var. 
// Bu satır, kodu objeye attığında Unity'nin otomatik olarak SpriteRenderer eklemesini (veya varsa bulmasını) sağlar. Kaza riskini sıfırlar.
[RequireComponent(typeof(SpriteRenderer))]
public class VisualGlitchEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Glitch Ayarları")]
    [Tooltip("Glitch efektinin minimum bekleme süresi")]
    public float minWaitTime = 2f;
    [Tooltip("Glitch efektinin maksimum bekleme süresi")]
    public float maxWaitTime = 6f;
    [Tooltip("Glitch'in ekranda kalma süresi (çok kısa olmalı ki 'bozuk' hissi versin)")]
    public float glitchDuration = 0.05f;

    void Start()
    {
        // Başlangıçta bileşenimizi bulup orijinal rengi (muhtemelen pürüzsüz beyaz) hafızaya kazıyoruz.
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // Glitch döngüsünü başlatıyoruz.
        StartCoroutine(GlitchRoutine());
    }

    // 2. KURAL: Performans. Neden Update() fonksiyonunu kullanmadık?
    // Çünkü Update saniyede 60-144 kere çalışır. Sadece birkaç saniyede bir renk değiştirecek bir sistem için 
    // her karede "Zaman doldu mu?" diye sormak işlemciye yüktür (Memory Leak temalı oyun yapıyoruz ama gerçekten memory leak yaratmayalım).
    // Coroutine (IEnumerator) kendi içinde bekleyebilir ve sistemi yormaz.
    private IEnumerator GlitchRoutine()
    {
        // Sonsuz döngü: Obje sahnede olduğu sürece bu bozukluk devam edecek.
        while (true)
        {
            // Rastgele bir süre bekle. Öngörülemezlik, organik bir 'sistem arızası' hissi için şarttır.
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // GLITCH BAŞLAR: Rengi boz
            // Klasik sistem çöküşü (Mavi Ekran veya Kırık Piksel) hissi için Cyan (Cam Göbeği) veya Magenta (Pembe/Mor) kullanıyoruz.
            // Random.value 0 ile 1 arası değer üretir. %50 ihtimalle Cyan, %50 ihtimalle Magenta olacak.
            Color glitchColor = Random.value > 0.5f ? Color.cyan : Color.magenta;
            spriteRenderer.color = glitchColor;

            // O minicik "stutter" (mikro takılma) anı kadar bekle. Gözü sadece rahatsız etmeli, kör etmemeli.
            yield return new WaitForSeconds(glitchDuration);

            // GLITCH BİTER: Sistemi hemen steril, orijinal rengine geri döndür.
            spriteRenderer.color = originalColor;
        }
    }
}