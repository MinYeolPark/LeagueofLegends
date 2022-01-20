using UnityEngine;

public interface IDamagable
{
    void OnDamage(BaseUnits unit, float damage);
    void OnDamage(BaseUnits unit, float damage, Vector3 hitPoint, Vector3 hitNormal);
}
