using UnityEngine;

public class OptionsSistemTetikleyici : MonoBehaviour
{
    void Start()
    {
        // Oyun başladığı an şalteri sıfırla ve asansörü gizle!
        acikMi = false;
        if (asansorParcalari != null)
        {
            asansorParcalari.SetActive(false);
        }
    }
    // Anne objenin altındaki parçaları buraya sürükleyeceğiz
    public GameObject asansorParcalari; // (Örn: Bir Empty Obje içinde Cizgi ve Kulpu barındıran obje)

    private bool acikMi = false;

    void OnMouseDown()
    {
        if (asansorParcalari != null)
        {
            // Şalteri tersine çevir
            acikMi = !acikMi;

            // Parçaların aktifliğini şaltere göre ayarla (Açıksa kapat, kapalıysa aç)
            asansorParcalari.SetActive(acikMi);
        }
    }
}