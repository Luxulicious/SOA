using SOA.Base;
using SOA.Common.Primitives;

public class Test : RegisteredMonoBehaviour
{
    [DisablePersistence] public FloatReference someFloat = new FloatReference();

    public override void Register()
    {
        someFloat.Register(this);
    }


}