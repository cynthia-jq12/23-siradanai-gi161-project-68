using UnityEngine;
using UnityEngine.UI; // สำคัญ! ต้องมีบรรทัดนี้ถึงจะใช้ Slider ได้

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider; // ลาก Slider มาใส่ตรงนี้
    [SerializeField] private Gradient gradient; // (Optional) สีหลอดเลือดเปลี่ยนตามพลังชีวิต
    [SerializeField] private Image fill;        // (Optional) รูปตัวเนื้อในหลอดเลือด

    // ตั้งค่าเริ่มต้น (เลือดเต็ม)
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        // ถ้าใส่ Gradient สีจะเขียวเต็มหลอด
        if (gradient != null && fill != null)
            fill.color = gradient.Evaluate(1f);
    }

    // อัปเดตค่าเลือดปัจจุบัน
    public void SetHealth(float health)
    {
        slider.value = health;

        // เปลี่ยนสีตามเลือดที่เหลือ (เขียว -> เหลือง -> แดง)
        if (gradient != null && fill != null)
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}