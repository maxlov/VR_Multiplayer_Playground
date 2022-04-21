using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Max.VRTools.Input.Keyboard
{
    public class Keyboard : MonoBehaviour
    {
        public TMP_InputField inputField;
        public GameObject keysLower;
        public GameObject keysUpper;
        private bool caps;

        [SerializeField] private UnityEvent submitEvent;

        public void InsertChar(string c)
        {
            inputField.text += c;
        }

        public void DeleteChar(string c)
        {
            if (inputField.text.Length > 0)
                inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }

        public void InsertSpace()
        {
            inputField.text += " ";
        }

        public void ToggleCapslock()
        {
            if (!caps)
            {
                keysLower.SetActive(false);
                keysUpper.SetActive(true);
            }
            else
            {
                keysUpper.SetActive(false);
                keysLower.SetActive(true);
            }
            caps = !caps;
        }

        public void Submit()
        {
            submitEvent.Invoke();
        }

    }
}
