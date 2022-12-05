using Unity.Entities;

// Instead of directly accessing the Turret component, we are creating an aspect.
// Aspects allows you to provide a customized API for accessing your components.
readonly partial struct TurretAspect : IAspect
{
    // This reference provides read only access to the Turret component.
    // Trying to use ValueRW (instead of ValueRO) on a read-only reference is an error.
    private readonly RefRO<Turret> _turret;
    
    // Note the use of ValueRO in the following properties.
    public Entity CannonBallSpawn => _turret.ValueRO.CannonBallSpawn;
    public Entity CannonBallPrefab => _turret.ValueRO.CannonBallPrefab;
}



