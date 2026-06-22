using UnityEngine;

public class AsansorKulpDikey : MonoBehaviour
{
    // Rayın en üstüne ve en altına koyduğumuz boş objeler (Empty)
    public Transform ustSinir;
    public Transform altSinir;

    // Sesi kontrol etmek için müzik motorumuzun hoparlörü
    private AudioSource oyunMuzigi;

    void Start()
    {
        // Oyun başladığında sahnede "MuzikMotoru"nu bul ve içindeki hoparlörü hafızaya al
        MuzikMotoru motor = FindObjectOfType<MuzikMotoru>();
        if (motor != null)
        {
            oyunMuzigi = motor.GetComponent<AudioSource>();
        }
    }

    void OnMouseDrag()
    {
        if (ustSinir == null || altSinir == null) return;

        // 1. Farenin ekrandaki yerini, oyun evrenindeki (Dünya) yerine çevir
        Vector3 fareDunyaKonumu = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. Sınır objelerinin HİÇ YANILMAYAN dünya (World) Y pozisyonlarını al
        float minY = Mathf.Min(altSinir.position.y, ustSinir.position.y);
        float maxY = Mathf.Max(altSinir.position.y, ustSinir.position.y);

        // 3. Farenin Y eksenindeki (aşağı-yukarı) konumunu, iki sınır arasına hapset
        float sinirliY = Mathf.Clamp(fareDunyaKonumu.y, minY, maxY);

        // 4. Kulpu hareket ettir (Sadece Dünya Y ekseninde hareket eder)
        transform.position = new Vector3(transform.position.x, sinirliY, transform.position.z);

        // 5. MÜHENDİSLİK DOKUNUŞU: Kulpun Konumuna Göre Sesi Ayarla!
        if (oyunMuzigi != null)
        {
            // InverseLerp: Kulp en alttayken bize 0 verir, en üstteyken 1 verir. Ortadaysa 0.5 verir.
            float sesOrani = Mathf.InverseLerp(minY, maxY, sinirliY);

            // Çıkan bu yüzdeyi direkt müziğin ses seviyesine eşitliyoruz
            oyunMuzigi.volume = sesOrani;
        }
    }
}