using UnityEngine;

public interface IDamagable
{
    public void OnDamage(BaseUnits unit, float damage, Vector3 hitPoint, Vector3 hitNormal);
}
