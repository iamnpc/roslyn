﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

// csc /t:library CSMethods.cs

public class C1
{
    public class SameName
    { 
    }

    public void sameName()
    { 
    }

    public void SameName2()
    {
    }

    public void SameName2(int x)
    {
    }

    public void sameName2(double x)
    {
    }

}



public abstract class Modifiers1
{
    public abstract void M1();

    public virtual void M2()
    {}

    public void M3()
    {}

    public virtual void M4()
    {}
}

public abstract class Modifiers2
    : Modifiers1
{
    public sealed override void M1()
    {}

    public abstract override void M2();

    public virtual new void M3()
    { }
}

public abstract class Modifiers3
    : Modifiers1
{
    public override void M1()
    {}

    public new void M3()
    { }

    public abstract new void M4();

}

public class DefaultParameterValues
{
    public static void M(
            string text,
            string path = "",
            DefaultParameterValues d = null,
            System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken)) { }
}

public class MultiDimArrays
{
    public static void Foo(int[,] x) { }
}
