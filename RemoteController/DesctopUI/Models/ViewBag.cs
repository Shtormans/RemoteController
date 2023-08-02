using System.Dynamic;

namespace DesktopUI.Models;

public class ViewBag : DynamicObject
{
    private readonly Dictionary<string, object> members = new();

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        if (value is not null)
        {
            members[binder.Name] = value;
            return true;
        }

        return false;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        result = null;

        if (members.ContainsKey(binder.Name))
        {
            result = members[binder.Name];
            return true;
        }

        return false;
    }
}
