using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text roleText;
    public Button viewButton;
    public Button assignButton;

    private CharacterData characterData;
    private System.Action<CharacterData> onViewCallback;
    private System.Action<CharacterData> onAssignCallback;

    public void Setup(CharacterData data, System.Action<CharacterData> onView, System.Action<CharacterData> onAssign = null)
    {
        characterData = data;
        onViewCallback = onView;
        onAssignCallback = onAssign;

        nameText.text = data.name;
        roleText.text = data.role;

        viewButton.onClick.RemoveAllListeners();
        viewButton.onClick.AddListener(() => onViewCallback?.Invoke(characterData));

        if (assignButton != null)
        {
            assignButton.gameObject.SetActive(onAssign != null);
            assignButton.onClick.RemoveAllListeners();
            assignButton.onClick.AddListener(() => onAssignCallback?.Invoke(characterData));
        }
    }
}
