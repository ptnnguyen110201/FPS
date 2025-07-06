using UnityEngine;
public class WeaponRaycastHandler
{   
    protected CharacterCtrl CharacterCtrl;
    protected WeaponDamageSender WeaponDamageSender;
    protected WeaponData WeaponData;

    [Inject] protected IEffectManager EffectManager;
    public WeaponRaycastHandler(WeaponData WeaponData, CharacterCtrl CharacterCtrl, WeaponDamageSender WeaponDamageSender)
    {
        this.WeaponData = WeaponData;
        this.CharacterCtrl = CharacterCtrl;
        this.WeaponDamageSender = WeaponDamageSender;
        GameContext.Instance.InjectInto(this);
    }

    public void ShootRay()
    {
        Vector3 ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitPoint, 100))
        {
            if (hitPoint.collider.gameObject.layer == LayerMask.NameToLayer("Character")) return;

            this.ShootImpactLayers(hitPoint);
            this.ShootHitDamageLayers(hitPoint);
        }
    }

    public void ShootHitDamageLayers(RaycastHit hitPoint) 
    {
        if (hitPoint.collider.gameObject.layer == LayerMask.NameToLayer("DamageLayers")) 
        {
            IDamageReceiver DamageReceiver = hitPoint.transform.GetComponent<IDamageReceiver>();
            if (DamageReceiver == null) return;
            DamageData damageData = new DamageData()
            {
                SendObj = this.CharacterCtrl.transform,
                Damage = this.WeaponData.Damage
            };
            DamageReceiver.SetShootObj(this.CharacterCtrl.transform);
            this.WeaponDamageSender.SetDamageData(damageData);
            this.WeaponDamageSender.Send(DamageReceiver, hitPoint.point);
        }
    }
    public void ShootImpactLayers(RaycastHit hitPoint)
    {  
        string HoleName = "HoleHit";
        if (hitPoint.collider.gameObject.layer == LayerMask.NameToLayer("ImpactLayers"))
        {
            Vector3 Pos = hitPoint.point + hitPoint.normal * 0.01f;
            Quaternion Rot = Quaternion.LookRotation(hitPoint.normal);
            this.EffectManager.EffectSpawner.SpawnEffect(HoleName, Pos, Rot);
        }
    }
}