using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        slider.maxValue = player.Health;
        slider.value = player.Health;
    }

    private void Update()
    {
        slider.value = player.Health;
    }
}
