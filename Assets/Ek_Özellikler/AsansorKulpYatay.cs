using UnityEngine;

public class AsansorKulpYatay : MonoBehaviour
{
    [Header("Sınırlar")]
    public Transform solSinir;
    public Transform sagSinir;

    private AudioSource oyunMuzigi;

    void Start()
    {
        // Oyun başladığında müzik motorunu bul
        MuzikMotoru motor = FindObjectOfType<MuzikMotoru>();
        if (motor != null)
        {
            oyunMuzigi = motor.GetComponent<AudioSource>();
        }
    }

    void OnMouseDrag()
    {
        if (solSinir == null || sagSinir == null) return;

        // 1. Farenin dünya pozisyonunu al
        Vector3 fareDunyaKonumu = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. Sol ve sağ sınırların sadece X (yatay) değerlerini hesapla
        float minX = Mathf.Min(solSinir.position.x, sagSinir.position.x);
        float maxX = Mathf.Max(solSinir.position.x, sagSinir.position.x);

        // 3. Farenin X konumunu, iki sınır arasına hapset
        float sinirliX = Mathf.Clamp(fareDunyaKonumu.x, minX, maxX);

        // 4. KULPU HAREKET ETTİR (IŞINLANMAYI ÖNLEYEN KISIM)
        // Sadece X değeri değişir. Y ve Z (derinlik) olduğu gibi kalır, böylece kulp kaybolmaz!
        transform.position = new Vector3(sinirliX, transform.position.y, transform.position.z);

        // 5. SES SEVİYESİNİ AYARLA
        if (oyunMuzigi != null)
        {
            // InverseLerp: Kulp en soldayken 0, en sağdayken 1 değerini verir.
            float sesOrani = Mathf.InverseLerp(minX, maxX, sinirliX);
            oyunMuzigi.volume = sesOrani;
        }
    }
}