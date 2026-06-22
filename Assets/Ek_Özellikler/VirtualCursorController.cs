using UnityEngine;

public class VirtualCursorController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [Tooltip("Farenin hareket hassasiyeti. Glitch hissi için değiştirilebilir.")]
    public float sensitivity = 0.1f;

    [Header("Hapishane Sınırları (Empty Objeler)")]
    public Transform topLeftBoundary;
    public Transform bottomRightBoundary;

    void Start()
    {
        // 1. KURAL: Gerçek fareyi yok et ve işletim sisteminin dışına çıkmasını engelle.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        Vector3 newPosition = transform.position + new Vector3(mouseX, mouseY, 0);

        // KURAL: Min ve Max değerlerini dinamik hesapla. 
        // Böylece sahnede Empty objelerin yerini yanlışlıkla ters koysan bile sistem çökmez.
        float minX = Mathf.Min(topLeftBoundary.position.x, bottomRightBoundary.position.x);
        float maxX = Mathf.Max(topLeftBoundary.position.x, bottomRightBoundary.position.x);

        float minY = Mathf.Min(topLeftBoundary.position.y, bottomRightBoundary.position.y);
        float maxY = Mathf.Max(topLeftBoundary.position.y, bottomRightBoundary.position.y);

        // X ve Y eksenlerini garantilenmiş sınırlar arasına hapset
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}