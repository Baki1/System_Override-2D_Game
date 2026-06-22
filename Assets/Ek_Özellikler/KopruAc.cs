using UnityEngine;
using System.Collections;

public class KopruAc : MonoBehaviour
{
    public Transform uzayanParca;

    // YENİ: Uzayan parçanın fizik kutusunu (Collider) kontrol etmek için
    public BoxCollider2D uzayanFizik;

    public float acilmaMesafesi = 3.5f;
    public float hiz = 8f;
    public float acikKalmaSuresi = 3f;

    private bool acildiMi = false;
    private Vector3 kapaliLokalKonum;

    void Start()
    {
        if (uzayanParca != null)
        {
            kapaliLokalKonum = uzayanParca.localPosition;

            // Oyun başladığında köprü kapalı olduğu için fiziğini de kapalı tut
            uzayanFizik.enabled = false;
        }
    }

    void OnMouseDown()
    {
        if (!acildiMi && uzayanParca != null)
        {
            acildiMi = true;

            // Köprü açılmaya başladığı an üstüne basılabilsin diye fiziğini aç!
            uzayanFizik.enabled = true;

            StartCoroutine(SagaKaydirVeKapat());
        }
    }

    IEnumerator SagaKaydirVeKapat()
    {
        // 1. FAZ: AÇILMA
        Vector3 hedefLokalKonum = new Vector3(kapaliLokalKonum.x + acilmaMesafesi, kapaliLokalKonum.y, kapaliLokalKonum.z);
        while (Vector3.Distance(uzayanParca.localPosition, hedefLokalKonum) > 0.01f)
        {
            uzayanParca.localPosition = Vector3.MoveTowards(uzayanParca.localPosition, hedefLokalKonum, hiz * Time.deltaTime);
            yield return null;
        }
        uzayanParca.localPosition = hedefLokalKonum;

        // 2. FAZ: BEKLEME
        yield return new WaitForSeconds(acikKalmaSuresi);

        // 3. FAZ: KAPANMA
        while (Vector3.Distance(uzayanParca.localPosition, kapaliLokalKonum) > 0.01f)
        {
            uzayanParca.localPosition = Vector3.MoveTowards(uzayanParca.localPosition, kapaliLokalKonum, hiz * Time.deltaTime);
            yield return null;
        }
        uzayanParca.localPosition = kapaliLokalKonum;

        // İŞTE BÜYÜ BURADA: Tamamen kapandığında fiziğini tekrar kapat ki fare tıklamamızı engellemesin!
        uzayanFizik.enabled = false;

        acildiMi = false;
    }
}