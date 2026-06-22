using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OlumYoneticisi : MonoBehaviour
{
    [Header("Bağlantılar")]
    public GameObject olumEkraniResmi; // Yeni kurduğumuz tam ekran COKUS_CANVAS resmimiz
    public float beklemeSuresi = 2.5f;

    private bool olduMu = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Karakter uçurumun dibindeki görünmez ağa çarptığı an...
        if (other.gameObject.CompareTag("Player") && !olduMu)
        {
            olduMu = true;
            StartCoroutine(YenidenBaslatmaDöngüsü());
        }
    }

    IEnumerator YenidenBaslatmaDöngüsü()
    {
        // 1. Ölüm ekranını anında patlat! 
        // Monitör camına yapışık (Overlay) olduğu için her şeyin önünde duracak.
        if (olumEkraniResmi != null)
        {
            olumEkraniResmi.SetActive(true);
        }

        // 2. Karakter arka planda, o kırmızı ekranın arkasında düşmeye devam ederken
        // jüriye hatayı göstermek için 2.5 saniye kronometreyi sayıyoruz.
        yield return new WaitForSeconds(beklemeSuresi);

        // 3. Süre dolunca sahneyi tertemiz bir şekilde baştan yüklüyoruz.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}