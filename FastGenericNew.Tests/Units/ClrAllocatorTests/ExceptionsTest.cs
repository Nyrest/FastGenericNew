#if AllowUnsafeImplementation && NET6_0_OR_GREATER
namespace FastGenericNew.Tests.Units.ClrAllocatorTests;

public class ExceptionsTest
{
    [Test()]
    public void ExceptionInterface()
    {
        try
        {
            ClrAllocator<IEnumerable>.CreateInstance();
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
            ClrAllocator<Stream>.CreateInstance();
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
            ClrAllocator<string>.CreateInstance();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (NotSupportedException)
        {
            // Marked as unsupported
            Assert.Pass();
        }
    }

    [Test()]
    public void ExceptionNotFoundNoParameter()
    {
        try
        {
            ClrAllocator<DemoClassNoParamlessCtor>.CreateInstance();
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (Exception e)
        {
            Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
        }
    }
}
#endif