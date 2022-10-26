using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;

    // Start is called before the first frame update
    void Start()
    {
        startPanel.SetActive(true);
    }
}
