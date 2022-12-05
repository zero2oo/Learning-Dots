using Unity.Entities;

// Authoring MonoBehaviours are regular GameObject components.
// They constitute the inputs for the baking systems which generates ECS data.
class CannonBallAuthoring : UnityEngine.MonoBehaviour
{
}

// Bakers convert authoring MonoBehaviours into entities and components.
class CannonBallBaker : Baker<CannonBallAuthoring>
{
    public override void Bake(CannonBallAuthoring authoring)
    {
        // By default, components are zero-initialized.
        // So in this case, the Speed field in CannonBall will be float3.zero.
        AddComponent<CannonBall>();
    }

    public void ChangeAuthor()
    {
        
    }
}