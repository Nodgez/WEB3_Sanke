using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    static LoadingScreen instance;

    [SerializeField] private Image loadingIcon;
    [SerializeField] private TMPro.TextMeshProUGUI loadingText;

    public static LoadingScreen Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null) { 
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        Hide();
    }

    private void Update()
    {
        loadingIcon.transform.Rotate(transform.forward);
    }

    public void Show(string text)
    {
        loadingText.text = text;
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject .SetActive(false);
    }
}
