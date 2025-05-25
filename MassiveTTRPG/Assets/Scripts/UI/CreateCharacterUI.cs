using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreateCharacterUI : PanelBase
{
    public TMP_InputField nameInput, roleInput, publicDescInput, privateDescInput, artworkUrlInput;
    public Button saveButton, cancelButton, loadPreviewButton;
    public Toggle publicToggle;
    public UnityEngine.UI.Image previewImage; 

    private void Awake()
    {
        saveButton.onClick.AddListener(OnSave);
        cancelButton.onClick.AddListener(OnCancel);
        loadPreviewButton.onClick.AddListener(OnLoadPreview);
    }

    public override void RefreshPanel()
    {
        // clear fields...
        artworkUrlInput.text = "";
        previewImage.sprite = null;
    }

    public void OnLoadPreview()
    {
        string url = artworkUrlInput.text.Trim();
        if (string.IsNullOrEmpty(url)) return;
        StartCoroutine(LoadImageCoroutine(url));
    }

    private IEnumerator LoadImageCoroutine(string url)
    {
        using (var uwr = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                var tex = UnityEngine.Networking.DownloadHandlerTexture.GetContent(uwr);
                var sprite = Sprite.Create(tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f));
                previewImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning($"Failed to load art: {uwr.error}");
            }
        }
    }

    public void OnSave()
    {
        // ... validate name ...
        var character = new CharacterData
        {
            id = System.Guid.NewGuid().ToString(),
            name = nameInput.text.Trim(),
            role = roleInput.text.Trim(),
            public_description = publicDescInput.text.Trim(),
            private_description = privateDescInput.text.Trim(),
            isPublic = publicToggle.isOn,
            assignedToPlayerId = new List<string>(),
            artworkUrl = artworkUrlInput.text.Trim()
        };
        GameManager.Instance.currentGame.characters.Add(character);
        GameManager.Instance.SaveCurrentState();
        NavigationManager.Instance.OpenPanel("GM - Panel");
    }

    public void OnCancel()
    {

    }

}
