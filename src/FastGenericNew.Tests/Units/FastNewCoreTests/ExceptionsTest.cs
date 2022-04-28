namespace FastGenericNew.Tests.Units.FastNewCoreTests;

public class ExceptionsTest
{
    [Test()]
    public void ExceptionInterface()
    {
        try
        {
            FastNew<IEnumerable>.CompiledDelegate();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("Cannot create an instance of an interface"));
        }
    }

    [Test()]
    public void ExceptionAbstract()
    {
        try
        {
            FastNew<Stream>.CompiledDelegate();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("Cannot create an abstract class"));
        }
    }

    [Test()]
    public void ExceptionPLString()
    {
        try
        {
            FastNew<string>.CompiledDelegate();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
        }
    }

    [Test()]
    public void ExceptionNotFoundNoParameter()
    {
        try
        {
            FastNew<DemoClassNoParamlessCtor>.CompiledDelegate();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
        }
    }

    [Test()]
    public void ExceptionNotFoundWithParameter()
    {
        try
        {
            FastNew<DemoClass, DBNull>.CompiledDelegate(DBNull.Value);
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
        }
    }
}
