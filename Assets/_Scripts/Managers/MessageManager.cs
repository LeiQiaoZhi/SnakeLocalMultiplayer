using _Scripts.Helpers;
using _Scripts.UI;
using UnityEngine;

// SINGLETON

namespace _Scripts.Managers
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private bool testMessages;
        [SerializeField] GameObject messageObjectPrefab;
        [SerializeField] GameObject popupObjectPrefab;
        [SerializeField] GameObject infoMessageObjectPrefab;
        [SerializeField] private GameObject messageCanvas;

        public static MessageManager Instance;


        private void Start()
        {
            if (testMessages)
            {
                // test message
                DisplayMessage("Test Message", null, 2);

                // test popup
                var popup = DisplayPopup("TITLE", "test message");
                popup.AddButton("Confirm", null, () =>
                {
                    XLogger.Log(Category.UI, "confirm button pressed");
                    Destroy(gameObject);
                });
                popup.AddButton("Cancel", null, () =>
                {
                    XLogger.Log(Category.UI, "cancel button pressed");
                    Destroy(gameObject);
                });
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void DisplayInfoMessage(string text, Color? textColor = null, float? duration = null, Vector2 position = default)
        {
            GameObject messageGo = Instantiate(infoMessageObjectPrefab);
            messageGo.transform.position = position;
            InfoObject m = messageGo.GetComponent<InfoObject>();
            m.Init(text, textColor, duration);
            Destroy(messageGo, 10f);
        }

        public void DisplayMessage(string text, Color? textColor = null, float? duration = null)
        {
            GameObject messageGo = Instantiate(messageObjectPrefab, messageCanvas.transform);
            Message m = messageGo.GetComponent<Message>();
            m.Init(text, textColor, duration);
            Destroy(messageGo, 10f);
        }

        public Popup DisplayPopup(string title = "", string text = "", Color? titleColor = null)
        {
            GameObject messageGo = Instantiate(popupObjectPrefab, messageCanvas.transform);
            Popup m = messageGo.GetComponent<Popup>();
            m.Init(title, text, titleColor);
            return m;
        }
    }
}