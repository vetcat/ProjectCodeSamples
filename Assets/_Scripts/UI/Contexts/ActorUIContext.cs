using Slash.Unity.DataBind.Core.Data;
using Context = Slash.Unity.DataBind.Core.Data.Context;

public class ActorUIContext : Context
{
    private readonly Property<string> textCountProperty = new Property<string>();
    public string TextCount
    {   get { return this.textCountProperty.Value; }
        set { this.textCountProperty.Value = value; }
    }
}
