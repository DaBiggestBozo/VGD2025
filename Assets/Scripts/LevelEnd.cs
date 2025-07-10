using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HighDefinition.ScalableSettingLevelParameter;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(2); //2 is credits scene right now
        }
    }
}
