using UnityEditor;
using UnityEngine;
[CreateAssetMenu (menuName = "Enemy", fileName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int level;
    public int attackDMG;
    public int armor;
    public float movementSpeed;
    public float attackSpeed;
    public int[] lootDropsID;
    public enum AttackType {None, Shock , Fire,Cold,Poison}
    public enum Vulnerability {None, Fire,Poison ,Cold}
    public Vulnerability enemyVulnerability;
    public Animation[] enemyAnim;
    public enum Habitat { Forest,Mountain,GrassLands ,Deserts};
    public Habitat[] habitats;
}
