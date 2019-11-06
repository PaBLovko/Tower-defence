using UnityEngine;

public enum projectTipe
{
    rock, arrow,fireball
};

public class ProjectTile : MonoBehaviour
{
    [SerializeField]
    int atackDamage;

    [SerializeField]
    projectTipe pType;

    public int GetAttackDamage
    {
        get
        {
            return atackDamage;
        }
    }

    public projectTipe GetProjectTipe
    {
        get
        {
            return pType;
        }
    }
}
