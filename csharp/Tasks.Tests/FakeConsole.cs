using System.IO;

namespace Tasks.Tests;

public class FakeConsole : IConsole
{
    private readonly TextReader _inputReader;
    private readonly TextWriter _inputWriter;

    private readonly TextReader _outputReader;
    private readonly TextWriter _outputWriter;

    public FakeConsole() 
    {
        Stream inputStream = new BlockingStream(new ProducerConsumerStream());
        _inputReader = new StreamReader(inputStream);
        _inputWriter = new StreamWriter(inputStream) { AutoFlush = true };

        Stream outputStream = new BlockingStream(new ProducerConsumerStream());
        _outputReader = new StreamReader(outputStream);
        _outputWriter = new StreamWriter(outputStream) { AutoFlush = true };
    }

    public string ReadLine()
    {
        return _inputReader.ReadLine();
    }

    public void Write(string format)
    {
        _outputWriter.Write(format);
    }

    public void WriteLine(string format)
    {
        _outputWriter.WriteLine(format);
    }

    public void WriteLine()
    {
        _outputWriter.WriteLine();
    }

    public void SendInput(string input)
    {
        _inputWriter.Write(input);
    }

    public string RetrieveOutput(int length)
    {
        var buffer = new char[length];
        _outputReader.ReadBlock(buffer, 0, length);
        return new string(buffer);
    }
}