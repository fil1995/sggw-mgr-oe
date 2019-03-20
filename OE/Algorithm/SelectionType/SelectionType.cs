using System;
using System.Collections.Generic;
using System.Text;


abstract class SelectionType
{
    protected Algorithm a;
    protected Random r;
    public string SelectionTypeName => this.GetType().Name;

    public void Initialize(Algorithm a,Random r)
    {
        this.a = a;
        this.r = r;
    }

    public virtual Organism Select()
    {
        throw new NotImplementedException();
    }
}
