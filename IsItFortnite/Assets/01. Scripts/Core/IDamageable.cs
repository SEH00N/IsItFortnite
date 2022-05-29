using System;

public interface IDamageable
{
    /// <summary>
    /// 데미지 인터페이스
    /// </summary>
    public void OnDamage(float dmg, Action freeze = null);
}
