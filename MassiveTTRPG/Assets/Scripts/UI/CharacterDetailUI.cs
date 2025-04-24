using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField roleInput;
    public TMP_InputField publicDescInput;
    public TMP_InputField privateDescInput;
    public Toggle publicToggle;
    public TMP_Text ownerText;
    public Button saveButton;

    private CharacterData currentData;

    public void Open(CharacterData data, bool isEditable)
    {
        gameObject.SetActive(true);
        currentData = data;

        nameInput.text = data.name;
        roleInput.text = data.role;
        publicDescInput.text = data.public_description;
        privateDescInput.text = data.private_description;
        publicToggle.isOn = data.isPublic;
        ownerText.text = data.assignedToPlayerId != null ? $"Assigned to: {data.assignedToPlayerId}" : "Unassigned";

        nameInput.interactable = roleInput.interactable = publicDescInput.interactable = isEditable;
        privateDescInput.interactable = publicToggle.interactable = isEditable;
        privateDescInput.gameObject.SetActive(isEditable); // Hide from players
        saveButton.gameObject.SetActive(isEditable);
    }

    public void OnSave()
    {
        currentData.name = nameInput.text;
        currentData.role = roleInput.text;
        currentData.public_description = publicDescInput.text;
        currentData.private_description = privateDescInput.text;
        currentData.isPublic = publicToggle.isOn;
        gameObject.SetActive(false);
    }

    public void Close() => gameObject.SetActive(false);
}
