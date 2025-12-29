using NUnit.Framework;

namespace safecodes;

[TestFixture]
public class Tests
{
    [Test]
    public void Test_ReadCodesFromStream()
    {
        using (var stream = GetTestStream())
        {
            var lines = Program.ReadCodesFromStream(stream).ToList();
            Assert.That(lines.Count, Is.EqualTo(10));
            Assert.That(lines[0], Is.EqualTo("L68"));
            Assert.That(lines[9], Is.EqualTo("L82"));
        }
    }

    [Test]
    public void Test_ParseCodes()
    {
        using (var stream = GetTestStream())
        {
            var lines = Program.ReadCodesFromStream(stream);
            var codes = Program.ParseCodes(lines).ToList();

            Assert.That(codes.Count, Is.EqualTo(10));
            Assert.That(codes[0], Is.EqualTo(new Code(Direction.Left, 68)));
            Assert.That(codes[1], Is.EqualTo(new Code(Direction.Left, 30)));
            Assert.That(codes[2], Is.EqualTo(new Code(Direction.Right, 48)));
            Assert.That(codes[3], Is.EqualTo(new Code(Direction.Left, 5)));
            Assert.That(codes[4], Is.EqualTo(new Code(Direction.Right, 60)));
            Assert.That(codes[5], Is.EqualTo(new Code(Direction.Left, 55)));
            Assert.That(codes[6], Is.EqualTo(new Code(Direction.Left, 1)));
            Assert.That(codes[7], Is.EqualTo(new Code(Direction.Left, 99)));
            Assert.That(codes[8], Is.EqualTo(new Code(Direction.Right, 14)));
            Assert.That(codes[9], Is.EqualTo(new Code(Direction.Left, 82)));
        }
    }

    [Test]
    public void Test_ProcessCodes()
    {
        var safePosition = new SafePosition();
        using (var stream = GetTestStream())
        {
            Program.ProcessCodes(safePosition, stream);
        }

        Assert.That(safePosition.Zeroes, Is.EqualTo(3));
    }

    private static Stream GetTestStream()
    {
                string testData = """
L68
L30
R48
L5
R60
L55
L1
L99
R14
L82
""";
        var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        {
            writer.Write(testData);
            writer.Flush();
            stream.Position = 0;
        }
        return stream;
    }
}