namespace FastGenericNew.Benchmarks;

public class DemoClass { }

public struct DemoStruct { }

public class DemoClassParam
{
    private int sum = 0;

    public DemoClassParam(int p1)
    {
        sum = p1;
    }

    public DemoClassParam(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9, int p10)
    {
        sum = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9 + p10;
    }
}

public struct DemoStructParam
{
    private int sum = 0;

    public DemoStructParam(int p1)
    {
        sum = p1;
    }

    public DemoStructParam(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9, int p10)
    {
        sum = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9 + p10;
    }
}