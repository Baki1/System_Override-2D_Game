using UnityEngine;

public class SystemGlitch : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isBroken = false;
    private int temasSayisi = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red; // Başlangıçta parlak kırmızı
    }

    void OnCollisionEnter2D(Collision2D temas)
    {
        if (temas.gameObject.name == "Player")
        {
            temasSayisi++;

            if (temas.rigidbody.mass > 5f && temas.relativeVelocity.y < -5f && !isBroken)
            {
                isBroken = true;
                sr.color = Color.cyan; // Kırılınca Neon Mavi
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddTorque(20f);
            }
            else if (!isBroken && temasSayisi == 1)
            {
                // TİTREME ÇÖZÜMÜ: Şekli bozmak yerine rengi "Koyu Kırmızı" yapıyoruz.
                // Bu oyuncuya butonun aktifleştiği hissini %100 verecek ama fizikle kavga etmeyecek.
                sr.color = new Color(0.5f, 0f, 0f);
            }
        }
    }

    void OnCollisionExit2D(Collision2D temas)
    {
        if (temas.gameObject.name == "Player")
        {
            temasSayisi--;

            if (!isBroken && temasSayisi <= 0)
            {
                temasSayisi = 0;
                // Üstünden inince tekrar eski parlak kırmızıya dön
                sr.color = Color.red;
            }
        }
    }

    void OnMouseDrag()
    {
        if (isBroken)
        {
            Vector3 fareKonumu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(fareKonumu.x, fareKonumu.y, transform.position.z);
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}