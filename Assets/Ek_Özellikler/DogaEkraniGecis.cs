using UnityEngine;
using System.Collections;
public class DogaEkraniGecis : MonoBehaviour
{
    [Header("Efekt Ayarları")]
public float bozulmaSuresi = 0.6f;
public float sarsintiSiddeti = 0.4f; // Sarsıntıyı artırdım

[Header("Ses Yönetimi")]
public AudioSource glitchSource;
public AudioClip glitchSesi;

private bool islemBasladi = false;
private Vector3 baslangicPozisyonu;
private SpriteRenderer sr;

void Start()
{
    baslangicPozisyonu = transform.localPosition;

    // Değişiklik burada: Hem kendisine bakar, hem de çocuklarına bakar
    sr = GetComponentInChildren<SpriteRenderer>();

    if (sr == null)
    {
        Debug.LogError("HATA: DOGA_EKRANI veya çocuklarında SpriteRenderer bileşeni bulunamadı!");
    }
}

void Update()
{
    if (Input.anyKeyDown && !islemBasladi)
    {
        StartCoroutine(BozulmaVeKapanma());
    }
}

IEnumerator BozulmaVeKapanma()
{
    islemBasladi = true;

    // SES: Sadece bir kez çal (PlayOneShot)
    if (glitchSource != null && glitchSesi != null)
    {
        glitchSource.PlayOneShot(glitchSesi);
    }

    float gecenSure = 0f;
    while (gecenSure < bozulmaSuresi)
    {
        // SARSINTI: Şiddeti artırdık
        transform.localPosition = baslangicPozisyonu + (Vector3)Random.insideUnitCircle * sarsintiSiddeti;

        // FLAŞ (Siyah Ekran): 0.1 saniyede bir ekranı gizleyip aç (bozuk monitör efekti)
        sr.enabled = !sr.enabled;

        // RENK: Rastgele renklerle boz
        sr.color = new Color(Random.value, Random.value, Random.value);

        gecenSure += Time.deltaTime;
        yield return new WaitForSeconds(0.05f); // Biraz daha sert geçişler için
    }

    // --- SON ---
    gameObject.SetActive(false);
}
}