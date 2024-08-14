namespace FastGenericNew.Tests.Units.GetFastNewTests;

public class ExceptionsTest
{
    [Test()]
    public void ExceptionInterface()
    {
        try
        {
            FastNew.GetCreateInstance<IEnumerable>(typeof(IEnumerable))();
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
            FastNew.GetCreateInstance<Stream>(typeof(Stream))();
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
            FastNew.GetCreateInstance<string>(typeof(string))();
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
            FastNew.GetCreateInstance<DemoClassNoParamlessCtor>(typeof(DemoClassNoParamlessCtor))();
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
            FastNew.GetCreateInstance<DemoClass, DBNull>(typeof(DemoClass), typeof(DBNull))(DBNull.Value);
            Assert.Fail("The expected exception is not thrown.");
        }
        catch (MissingMethodException e)
        {
            Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
        }
    }
}
