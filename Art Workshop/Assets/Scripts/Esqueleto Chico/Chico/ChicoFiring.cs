using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChicoFiring : MonoBehaviour
{
    [Header("Spell Stats")]
    [SerializeField] float castingDistance;
    [SerializeField] GameObject spellObject;
    [SerializeField] float spellDamage;
    [SerializeField] float spellSpeed;
    [SerializeField] float spellScatter;
    [SerializeField] float spellDestructionTime;
    [SerializeField] float spellKnockback;
    [SerializeField] float spellRate;
    [SerializeField] bool allDirections;
    float currentSpellCooldown;

    private Vector2 firingDir; 
    private bool firing = false;

    [SerializeField] private UnityEvent OnShoot;

    private void Update()
    {
        if (currentSpellCooldown > 0)
        {
            currentSpellCooldown -= Time.deltaTime;
        }
        else if (firing)
        {
            CastSpell();
        }
    }

    public void SetFiringDirection(Vector2 firingDir) 
    {
        firing = true;
        this.firingDir = firingDir;
    }

    public void StopFiring() 
    {
        firing = false;
    }

    private void CastSpell()
    {
        float xComponent = firingDir.x;
        float yComponent = firingDir.y;

        Vector2 desiredShootVector;
        Vector2 strongVector;

        if (!allDirections) 
        {
            if (Mathf.Abs(xComponent) > Mathf.Abs(yComponent))
            {
                //Shoot Horizontaly
                strongVector = new Vector2(xComponent, 0).normalized;
                desiredShootVector = new Vector2(strongVector.x, Random.Range(-spellScatter, spellScatter));
            }
            else
            {
                //Shoot Verticaly
                strongVector = new Vector2(0, yComponent).normalized;
                desiredShootVector = new Vector2(Random.Range(-spellScatter, spellScatter), strongVector.y);
            }
        }
        else 
        {
            strongVector = firingDir.normalized;
            desiredShootVector = new Vector2(strongVector.x + Random.Range(-spellScatter, spellScatter), strongVector.y + Random.Range(-spellScatter, spellScatter));
        }

        Vector2 castingLocation = new Vector2(transform.position.x, transform.position.y) + castingDistance * strongVector;
        GameObject invokedSpell = Instantiate(spellObject, castingLocation, Quaternion.identity);
        invokedSpell.GetComponent<ChicoSpell>().SetUp("Enemy", spellDamage, spellSpeed * desiredShootVector, spellDestructionTime, spellKnockback);
        currentSpellCooldown = 1 / spellRate;
        OnShoot.Invoke();
    }
}
