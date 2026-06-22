using UnityEngine;
using System.Collections; // Zamanlayıcı (Coroutine) kullanabilmek için gereken kütüphane

public class KameraSarsinti : MonoBehaviour
{
    /* Öğretmen Notu: IEnumerator (Coroutine), oyunun akışını durdurmadan arka planda
       zamanlayıcı çalıştırmamızı sağlayan özel bir fonksiyondur. Normal bir kod 
       saniyenin binde birinde okunup biterken, bu kod verdiğimiz süre boyunca çalışır. */

    public IEnumerator SarsintiYap(float sure, float siddet)
    {
        // Kameranın sarsılmadan önceki orijinal, kusursuz konumunu aklımızda tutuyoruz
        Vector3 orijinalKonum = transform.localPosition;

        float gecenSure = 0f;

        // Belirlediğimiz süre dolana kadar döngüyü çalıştır
        while (gecenSure < sure)
        {
            // X ve Y eksenlerinde şiddetimize göre rastgele, kaotik bir sapma noktası bul
            float x = Random.Range(-1f, 1f) * siddet;
            float y = Random.Range(-1f, 1f) * siddet;

            // Kamerayı bu rastgele bozuk noktaya ışınla
            transform.localPosition = new Vector3(orijinalKonum.x + x, orijinalKonum.y + y, orijinalKonum.z);

            // Geçen süreyi Unity'nin iç saatiyle güncelle
            gecenSure += Time.deltaTime;

            // Sistemi kilitlememek için "bu karelik işim bitti, bir sonraki kareye kadar bekle" de
            yield return null;
        }

        // Süre dolduğunda, kamerayı eski kusursuz ve sabit yerine geri koy
        transform.localPosition = orijinalKonum;
    }
}