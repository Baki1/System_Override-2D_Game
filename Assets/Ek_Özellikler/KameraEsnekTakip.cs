using UnityEngine;

public class KameraEsnekTakip : MonoBehaviour
{
    // Takip edilecek hedef (Player)
    public Transform hedef;

    // Görünmez kutunun (ölü bölgenin) genişliği ve yüksekliği
    // Karakter bu sınırların içinde kaldığı sürece kamera sabit durur
    public float sinirX = 4f;
    public float sinirY = 2.5f;

    // Kameranın kayma yumuşaklığı (Düşük değer = daha sinematik ve yavaş kayma)
    public float kaymaHizi = 4f;

    private Vector3 hedefPozisyon;

    void Start()
    {
        // Başlangıçta kameranın Z eksenini (derinliği) korumamız lazım (-10 gibi)
        hedefPozisyon = transform.position;
    }

    void LateUpdate()
    {
        if (hedef == null) return;

        // Karakter ile kameranın merkezi arasındaki mesafe
        float mesafeX = hedef.position.x - transform.position.x;
        float mesafeY = hedef.position.y - transform.position.y;

        // X EKSENİ KONTROLÜ: Karakter sağ veya sol sınırı aştı mı?
        if (Mathf.Abs(mesafeX) > sinirX)
        {
            // Eğer karakter sağdaysa hedefi sağa, soldaysa sola çek
            if (mesafeX > 0)
                hedefPozisyon.x = hedef.position.x - sinirX;
            else
                hedefPozisyon.x = hedef.position.x + sinirX;
        }

        // Y EKSENİ KONTROLÜ: Karakter üst veya alt sınırı aştı mı?
        if (Mathf.Abs(mesafeY) > sinirY)
        {
            if (mesafeY > 0)
                hedefPozisyon.y = hedef.position.y - sinirY;
            else
                hedefPozisyon.y = hedef.position.y + sinirY;
        }

        // KAMERAYI HAREKET ETTİR: Lerp ile kamerayı yeni hedefine pürüzsüzce kaydır
        // Z eksenini sabit tutuyoruz ki oyun ekranı kaybolmasın
        transform.position = Vector3.Lerp(transform.position, new Vector3(hedefPozisyon.x, hedefPozisyon.y, transform.position.z), kaymaHizi * Time.deltaTime);
    }
}