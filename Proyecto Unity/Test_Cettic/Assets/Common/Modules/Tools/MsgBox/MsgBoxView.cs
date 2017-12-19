using UnityEngine;
using UnityEngine.UI;

namespace _ToolModules
{
    public class MsgBoxView : MonoBehaviour
    {
        [Header("Scripts")]
        public MsgBox _msgBox;
        [Header("Elements")]
        public Text txtTitle;

        public void OnClick_Answer(string Answer)
        {
            _msgBox.To_Answer(Answer);
        }
    }
}
