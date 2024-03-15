using UnityEngine;
using UnityEngine.UI;

public class Discord : MonoBehaviour
{
    [SerializeField] private Button _discordButton;

    private readonly string _discordURL = "https://discord.gg/nr4Y2dNzKM";

    private void Awake()
    {
        _discordButton.onClick.AddListener(OnDiscordClick);
    }

    private void OnDiscordClick()
    {
        Application.OpenURL(_discordURL);
    }
}
