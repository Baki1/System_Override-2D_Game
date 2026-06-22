using UnityEngine;

public class AnaMenuQuit : MonoBehaviour
{
    // FARE İLE TIKLANDIĞINDA ÇALIŞIR
    void OnMouseDown()
    {
        // Unity editöründe oyun kapanmaz, bu yüzden konsola bir kanıt yazdırıyoruz
        Debug.Log("SİSTEM KAPATILDI! (Oyun .exe olarak alındığında tamamen kapanacaktır.)");

        // Gerçek oyunu kapatma komutu
        Application.Quit();
    }
}