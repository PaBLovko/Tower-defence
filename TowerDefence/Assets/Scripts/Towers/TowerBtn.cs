using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    GameObject towerObject;
    [SerializeField]
    Sprite dragSprite;

    public GameObject GetTowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public TowerControlle GetTowerControll
    {
        get
        {
            return towerObject.GetComponent<TowerControlle>();
        }
    }

    public Sprite GetDragSprite
    {
        get
        {
            return dragSprite;
        }
    }

}
