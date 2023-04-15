using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SelectOnDefault : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().Select();
        }
    }
}
