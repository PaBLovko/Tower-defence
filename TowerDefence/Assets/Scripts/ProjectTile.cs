using UnityEngine;

public enum projectTipe
{
    rock, arrow,fireball
};

public class ProjectTile : MonoBehaviour
{
    [SerializeField]
    float atackDamage;

    [SerializeField]
    projectTipe pType;

    public float GetAttackDamage
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
