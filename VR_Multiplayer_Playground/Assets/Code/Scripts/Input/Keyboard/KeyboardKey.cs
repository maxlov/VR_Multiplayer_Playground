using UnityEngine;
using TMPro;

namespace Max.VRTools.Input.Keyboard
{
    public class KeyboardKey : MonoBehaviour
    {
        Keyboard keyboard;
        TextMeshProUGUI buttonText;

        private void Start()
        {
            keyboard = GetComponentInParent<Keyboard>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText.text.Length == 1)
            {
                NameToButtonText();
                GetComponentInChildren<PhysicsButton>().onReleased
                    .AddListener(delegate { keyboard.InsertChar(buttonText.text); } );
            }
        }

        public void NameToButtonText()
        {
            buttonText.text = gameObject.name;
        }
    }
}
