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

    public int getAttackDamage
    {
        get
        {
            return atackDamage;
        }
    }

    public projectTipe getProjectTipe
    {
        get
        {
            return pType;
        }
    }
}
