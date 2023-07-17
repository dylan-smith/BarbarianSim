using Moq;

namespace BarbarianSim.Tests;

public static class TestHelpers
{
    public static Mock<T> CreateMock<T>() where T : class
    {
        var ctor = typeof(T).GetConstructors().First();
        var argCount = ctor.GetParameters().Length;
        var args = new object[argCount];

        return new Mock<T>(args);
    }
}
