using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Görsel ve Animasyon")]
    public Animator buyucuAnim;

    [Header("Form Boyutları (Scale)")]
    public Vector3 kucukBoyut = new Vector3(0.5f, 0.5f, 1f); // KENDİ BOYUTUNU INSPECTOR'DAN GİR
    public Vector3 buyukBoyut = new Vector3(1f, 1f, 1f);

    [Header("Küçük Form Ayarları")]
    public float kucukHiz = 8f;
    public float kucukZiplayis = 12f;
    public float kucukKutle = 2f;
    public float kucukYercekimi = 2f;

    [Header("Büyük Form Ayarları")]
    public float buyukHiz = 5f;
    public float buyukZiplayis = 35f;
    public float buyukKutle = 9f;
    public float buyukYercekimi = 9f;

    [Header("Görsel Efektler")]
    public ParticleSystem ziplamaTozu; // Unity'den sürükleyip bırakacağımız yuva

    private float aktifHiz;
    private float aktifZiplayis;
    private float horizontalInput;
    private Rigidbody2D rb;
    private bool isHeavy = false;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        KucukFormaGec();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (buyucuAnim != null)
        {
            buyucuAnim.SetFloat("KosuHizi", Mathf.Abs(horizontalInput));

            // Sağa sola dönerken mevcut Scale'i bozmadan sadece X eksenini eksi/artı yapar
            if (horizontalInput > 0)
                buyucuAnim.transform.localScale = new Vector3(Mathf.Abs(buyucuAnim.transform.localScale.x), buyucuAnim.transform.localScale.y, 1f);
            else if (horizontalInput < 0)
                buyucuAnim.transform.localScale = new Vector3(-Mathf.Abs(buyucuAnim.transform.localScale.x), buyucuAnim.transform.localScale.y, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, aktifZiplayis);
            isGrounded = false;

            if (buyucuAnim != null) buyucuAnim.SetBool("YerdeMi", false);

            // Senin mevcut zıplama kodun (Örnek: rb.velocity = new Vector2(...))

            // --- YENİ EKLENEN KISIM ---
            if (ziplamaTozu != null)
            {
                ziplamaTozu.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleForm();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * aktifHiz, rb.linearVelocity.y);
    }

    void ToggleForm()
    {
        isHeavy = !isHeavy;
        if (isHeavy) BuyukFormaGec();
        else KucukFormaGec();
       
    }

    void KucukFormaGec()
    {
        transform.localScale = kucukBoyut;
        rb.mass = kucukKutle;
        rb.gravityScale = kucukYercekimi;
        aktifHiz = kucukHiz;
        aktifZiplayis = kucukZiplayis;
    }

    void BuyukFormaGec()
    {
        transform.localScale = buyukBoyut;
        rb.mass = buyukKutle;
        rb.gravityScale = buyukYercekimi;
        aktifHiz = buyukHiz;
        aktifZiplayis = buyukZiplayis;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpışmanın gerçekleştiği noktaları (temas yüzeylerini) tek tek incele
        foreach (ContactPoint2D temas in collision.contacts)
        {
            // Eğer temas yüzeyinin dik açısı (Normal'i) yukarıyı gösteriyorsa (0.5'ten büyükse)
            // Bu demektir ki ayaklarımızın altındaki bir zemine bastık!
            if (temas.normal.y > 0.5f)
            {
                isGrounded = true;
                if (buyucuAnim != null) buyucuAnim.SetBool("YerdeMi", true);

                // ŞOK DÜŞÜŞÜ DÜZELTİCİ
                if (!isHeavy && rb.gravityScale > kucukYercekimi)
                {
                    KucukFormaGec();
                }

                // Zemini bulduğumuz için diğer temas noktalarına bakmaya gerek yok, döngüden çık
                break;
            }
        }
    }
}